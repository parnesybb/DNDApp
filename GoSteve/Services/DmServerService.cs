using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using GoSteve.Screens;
using System.Threading;
using Server;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.IO;
using Java.Net;

namespace GoSteve.Services
{
    [Service(Exported=true)]
    [IntentFilter(new String[] { DmServerService.IntentFilter })]
    public class DmServerService : Service
    {
        DmServerBinder binder;

        private static bool _isServiceUp = false;
        private static DmServerService _service=null;

        public static bool IsServiceRunning { get { return _isServiceUp; } }
        public static DmServerService Service { get { return _service; } }

        private DmStopServiceReceiver _stopServiceReceiver;

        private volatile bool _isServerUp;

        private Thread _serverThread;
        private GSNsdHelper _nsd;
        private TcpListener _server;

        private CharacterSheet cs = null;

        public const string IntentFilter = "com.xamarin.DmServerService";
        public const string CharSheetUpdatedAction = "CharacterSheetUpdated";
        public const string ServerStateChangedAction = "ServerStateChanged";

        public const int notifyNewCharID = 0;

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {

            if(_isServiceUp)
            {
                StopSelf();
                return StartCommandResult.Sticky;
            }

            setServerRunning(true);

            _stopServiceReceiver = new DmStopServiceReceiver();
            var stopServIntentFilter = new IntentFilter(ShutdownDmServerService.StopServerServiceAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_stopServiceReceiver, stopServIntentFilter);

            Log.Debug("DmServerService", "DemoService started");

            StartServiceInForeground();

            DoWork();

            return StartCommandResult.Sticky;
        }

        void StartServiceInForeground()
        {
            /*
            var ongoing = new Notification(Resource.Drawable.Icon, "DmServerService in foreground");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            ongoing.SetLatestEventInfo(this, "DmServerService", "DmServerService is running in the foreground", pendingIntent);
            */

            //var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);

            var pendingIntent = PendingIntent.GetService(this, 0, new Intent(ShutdownDmServerService.IntentFilter), 0);

            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle("DmServerService")
                .SetContentText("Dm Server is running Click to stop")
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetContentIntent(pendingIntent);

            // Build the notification:
            Notification ongoing = builder.Build();

            ongoing.Flags = NotificationFlags.AutoCancel;

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            //const int notificationId = 0;

            StartForeground((int)NotificationFlags.ForegroundService, ongoing);
        }

        public override void OnDestroy()
        {
            Log.Debug("DmServerService", "DmServerService stopped");
            base.OnDestroy();

            StopService();
        }

        void SendNotification()
        {
            /*
            var nMgr = (NotificationManager)GetSystemService(NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, "New CharacterSheet uploaded!");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            notification.SetLatestEventInfo(this, "Dm Server Service Notification", "New CharacterSheet uploaded", pendingIntent);
            nMgr.Notify(0, notification);
            */


            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle("Go Steve DND Update")
                .SetContentText("New CharacterSheet uploaded!")
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetContentIntent(pendingIntent);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            notification.Flags = NotificationFlags.AutoCancel;

            // Publish the notification:

            notificationManager.Notify(notifyNewCharID, notification);
        }

        void BroadcastNewCharacterToActivity()
        {
            var characterIntent = new Intent(CharSheetUpdatedAction);

            SendOrderedBroadcast(characterIntent, null);
        }

        void OnStateChanged()
        {
            var stateIntent = new Intent(ServerStateChangedAction);

            SendOrderedBroadcast(stateIntent, null);
        }

        private void setServerRunning(bool state)
        {
            if(state)
            {
                _isServiceUp = true;
                _service = this;
            }
            else
            {
                _isServiceUp = false;
                _service = null;
            }
            OnStateChanged();
        }

        public void DoWork()
        {
            Toast.MakeText(this, "The dm service has started", ToastLength.Long).Show();

            StartServer();
        }

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            binder = new DmServerBinder(this);
            return binder;
        }

        public CharacterSheet GetCharacterSheets()
        {
            return cs;
        }

        private void StartServerThread(int port)
        {
            _server = null;
            Log.Info("DmScreenBase", "server TCPListener startup");
            try
            {
                var format = new BinaryFormatter();
                _server = new TcpListener(System.Net.IPAddress.Any, port);

                Log.Info("DmScreenBase", "PORT: " + port);

                foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    Log.Info("DmScreenBase", "IP: " + ip);
                }

                MemoryStream ms = null;
                byte[] buffer = null;
                byte[] retToClient = null;

                _server.Start();
                _isServerUp = true;

                while (_isServerUp)
                {
                    // Get/Await data from TCP Client.
                    var client = _server.AcceptTcpClient();
                    var stream = client.GetStream();

                    try
                    {
                        // Information about data from client.
                        var dataRead = 0;
                        var msgLength = 0;
                        var msgByteLength = new byte[4];

                        // Get the size of the incoming object.
                        stream.Read(msgByteLength, 0, 4);
                        msgLength = BitConverter.ToInt32(msgByteLength, 0);
                        buffer = new byte[msgLength];

                        // Read the object into byte buffer.
                        do
                        {
                            dataRead += stream.Read(buffer, dataRead, msgLength - dataRead);
                        } while (dataRead < msgLength);

                        // Try to deserialize the object and update GUI.
                        ms = new MemoryStream(buffer);
                        cs = format.Deserialize(ms) as CharacterSheet;

                        // Create new ID if there's not one for the player.
                        if (String.IsNullOrWhiteSpace(cs.ID))
                        {
                            cs.ID = System.Guid.NewGuid().ToString(); ;
                        }

                        // Update UI.
                        //RunOnUiThread(() => Update(cs));

                        // Return the ID to client.
                        retToClient = ASCIIEncoding.ASCII.GetBytes(cs.ID);
                        stream.Write(retToClient, 0, retToClient.Length);

                        Log.Info("DMScreenBase", "Receive character ID:" + cs.ID);

                        stream.Close();
                        SendNotification();
                        Log.Info("DMScreenBase", "Is character null:" + (cs == null));
                        BroadcastNewCharacterToActivity();

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        client.Close();
                        buffer = null;
                        ms = null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _server.Stop();
                Log.Info("DmScreenBase", "server TCPListener shutdown");
                _server = null;

                StopForeground(true);

                StopSelf();
            }
        }

        private void StopServer()
        {
            _isServerUp = false;
            StopNSD();
            if (_server != null)
            {
                _server.Stop();
                _serverThread.Join();
                _serverThread = null;
                _server = null;
                cs = null;
            }
        }

        private void StopNSD()
        {
            //_nsd.StopDiscovery();
            if (_nsd != null)
            {
                _nsd.UnregisterService();
                _nsd = null;
            }
        }

        private void StopService()
        {
            UnregisterReceiver(_stopServiceReceiver);
            StopServer();

            setServerRunning(false);

            NotificationManager nManager =
               GetSystemService(Context.NotificationService) as NotificationManager;
            nManager.CancelAll();
        }

        private void StartServer()
        {
            if (!_isServerUp && _serverThread == null)
            {
                _isServerUp = false;

                var socket = new ServerSocket(0);
                var port = socket.LocalPort;
                socket.Close();

                if (_serverThread != null && _server != null)
                {
                    _server.Stop();
                }

                StopServer();

                _serverThread = new Thread(() => StartServerThread(port));
                _serverThread.Start();

                // announce server/port
                StartNSD(port);
            }
        }

        private void StartNSD(int port)
        {
            _nsd = new GSNsdHelper(this);
            _nsd.StartHelper();
            _nsd.RegisterService(port);
        }

        class DmStopServiceReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                DmServerService service;

                service = ((DmServerService)context);

                if (service != null)
                {
                    service.StopService();
                }

                InvokeAbortBroadcast();
            }
        }
    }

    public class DmServerBinder : Binder
    {
        DmServerService service;

        public DmServerBinder(DmServerService service)
        {
            this.service = service;
        }

        public DmServerService GetDmServerService()
        {
            return service;
        }

        public bool IsServiceRunning()
        {
            return DmServerService.IsServiceRunning;
        }
    }
}
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
using GoSteve.Structures;

namespace GoSteve.Services
{
    /// <summary>
    /// This service provides a IP network Server and mDNS multicast Domain Name Service
    /// for DMScreenBase Activity. It accepts clients and receives charactersheets then
    /// sends character update messages to the DMScreenBase Activity. It also stores character sheet
    /// data between instances of DMScreenBase Activity.
    /// </summary>
    [Service(Exported = true)]
    [IntentFilter(new String[] { DmServerService.IntentFilter })]
    public class DmServerService : Service
    {
        DmServerBinder binder;

        private bool _isServiceUp = false;
        private static DmServerService _service = null;
        private Campaign _campaign;
        private CharacterSheet _cs = null;

        public bool IsServiceRunning { get { return _isServiceUp; } }
        public static DmServerService Service { get { return _service; } }

        private DmStopServiceReceiver _stopServiceReceiver;

        private volatile bool _isServerUp;

        private Thread _serverThread;
        private GSNsdHelper _nsd;
        private TcpListener _server;

        public const string IntentFilter = "com.GoSteve.DmServerService";
        public const string CharSheetUpdatedAction = "CharacterSheetUpdated";
        public const string ServerStateChangedAction = "ServerStateChanged";
        private const string TAG = "DmServerService";

        public const int notifyNewCharID = 0;

        /// <summary>
        /// Called by Android OS when the service is created
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug(TAG, "OnCreate called");
            _service = this;
            _campaign = new Campaign();
        }

        /// <summary>
        /// Called by calls from StartService. It starts the service thread.
        /// </summary>
        /// <param name="intent">start Intent</param>
        /// <param name="flags">start flags</param>
        /// <param name="startId"></param>
        /// <returns>Bind flag</returns>
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            //if(intent.GetBooleanExtra("shutdownService", false))
            //{
            //    StopService();
            //}
            if (_isServiceUp)
            {
                StopSelf();
                return StartCommandResult.Sticky;
            }

            _stopServiceReceiver = new DmStopServiceReceiver();
            var stopServIntentFilter = new IntentFilter(ShutdownDmServerService.StopServerServiceAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_stopServiceReceiver, stopServIntentFilter);

            Log.Debug(TAG, "DemoService started");

            StartServiceInForeground();

            Toast.MakeText(this, "The dm service has started", ToastLength.Long).Show();
            StartServer();

            setServerRunning(true);

            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// Called by OnStartCommand to start the service in foreground so the OS does not kill it
        /// to gain more resources.
        /// </summary>
        void StartServiceInForeground()
        {
            /*
            var ongoing = new Notification(Resource.Drawable.Icon, "DmServerService in foreground");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            ongoing.SetLatestEventInfo(this, "DmServerService", "DmServerService is running in the foreground", pendingIntent);
            */

            //var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);


            //var curIntent = new Intent(this, typeof(DmServerService));
            //curIntent.PutExtra("shutdownService", true);
            //var pendingIntent = PendingIntent.GetService(this, 0, curIntent, 0);


            var pendingIntent = PendingIntent.GetService(this, 0, new Intent(ShutdownDmServerService.IntentFilter), 0);

            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle("DM Server Running")
                .SetContentText("DM server is running, click to stop")
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

        /// <summary>
        /// Called by Anroid OS when the service is destroyed. This is usually called
        /// by StopService or unbind
        /// </summary>
        public override void OnDestroy()
        {
            Log.Debug(TAG, "DmServerService stopped");
            base.OnDestroy();

            if (_isServiceUp)
            {
                StopService();
            }
        }

        /// <summary>
        /// Send a notification to the user when a charactersheet has been uploaded by a client
        /// </summary>
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
                .SetContentText("New character sheet uploaded!")
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

        /// <summary>
        /// Send a Broadcast to the DmScreenBase activity that a character sheet has been
        /// uploaded.
        /// </summary>
        void BroadcastNewCharacterToActivity()
        {
            var characterIntent = new Intent(CharSheetUpdatedAction);

            SendOrderedBroadcast(characterIntent, null);
        }

        /// <summary>
        /// Send a Broadcast to the DmScreenBase activity that the start/stop state has changed
        /// </summary>
        void OnStateChanged()
        {
            var stateIntent = new Intent(ServerStateChangedAction);

            SendOrderedBroadcast(stateIntent, null);
        }

        /// <summary>
        /// Set the server running status variables and send broadcast
        /// to DMScreenBase activity.
        /// </summary>
        /// <param name="state"></param>
        private void setServerRunning(bool state)
        {
            if (state)
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

        /// <summary>
        /// Create binder for the service and DmScreenBase Interprocess communication
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            binder = new DmServerBinder(this);
            return binder;
        }

        /// <summary>
        /// Gets the campaign data from the service
        /// </summary>
        /// <returns>Updated Campaign</returns>
        public Campaign GetCampaign()
        {
            return _campaign;
        }

        public CharacterSheet GetCharacterSheet()
        {
            return _cs;
        }

        public void ResetCharacterSheet()
        {
            _cs = null;
        }

        /// <summary>
        /// Method for the IP network server. Called by new thread.
        /// </summary>
        /// <param name="port"></param>
        private void StartServerThread(int port)
        {
            // announce server/port
            StartNSD(port);
            _server = null;
            Log.Info(TAG, "server TCPListener startup");
            try
            {
                var format = new BinaryFormatter();
                _server = new TcpListener(System.Net.IPAddress.Any, port);

                Log.Info(TAG, "PORT: " + port);

                foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    Log.Info(TAG, "IP: " + ip);
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

                    Log.Info(TAG, "Connected to Client.");

                    try
                    {
                        // Information about data from client.
                        var dataRead = 0;
                        var msgLength = 0;
                        var msgByteLength = new byte[4];

                        // Get the size of the incoming object.
                        dataRead = stream.Read(msgByteLength, 0, 4);
                        msgLength = BitConverter.ToInt32(msgByteLength, 0);
                        buffer = new byte[msgLength];

                        Log.Info(TAG, "Recieved "+ dataRead + " Bytes from client.");
                        Log.Info(TAG, "Setup to read charactersheet that is " + msgLength + " Bytes.");

                        dataRead = 0;

                        // Read the object into byte buffer.
                        do
                        {
                            dataRead += stream.Read(buffer, dataRead, msgLength - dataRead);
                            Log.Info(TAG, "Recieved " + dataRead + " Bytes from client.");
                        } while (dataRead < msgLength);

                        // Try to deserialize the object and update GUI.
                        ms = new MemoryStream(buffer);
                        _cs = format.Deserialize(ms) as CharacterSheet;

                        // Create new ID if there's not one for the player.
                        if (String.IsNullOrWhiteSpace(_cs.ID))
                        {
                            _cs.ID = System.Guid.NewGuid().ToString(); ;
                        }

                        // Update UI.
                        //RunOnUiThread(() => Update(_cs));

                        // Return the ID to client.
                        retToClient = ASCIIEncoding.ASCII.GetBytes(_cs.ID);
                        stream.Write(retToClient, 0, retToClient.Length);

                        Log.Info(TAG, "Receive character ID:" + _cs.ID);

                        stream.Close();
                        UpdateCharacterSheet(_cs);
                        SendNotification();
                        Log.Info(TAG, "Is character null:" + (_cs == null));
                        BroadcastNewCharacterToActivity();
                        Log.Info(TAG, "Disconnect client");

                        while(_cs!=null)
                        {
                            Thread.Sleep(250);
                        }


                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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
                Log.Info(TAG, "server TCPListener shutdown");
                _server = null;
                StopNSD();

                StopForeground(true);

                StopSelf();
            }
        }

        /// <summary>
        /// Stop the IP network server and wait for thread to finish.
        /// </summary>
        public void StopServer()
        {
            _isServerUp = false;
            if (_server != null)
            {
                _server.Stop();
                _serverThread.Join();
                _serverThread = null;
                _server = null;
                setServerRunning(false);
            }
        }

        /// <summary>
        /// Stop NSD (Network Service Discovery) Allows IP server and clients
        /// to find each other without knowing the server IP address before hand.
        /// </summary>
        private void StopNSD()
        {
            //_nsd.StopDiscovery();
            if (_nsd != null)
            {
                _nsd.UnregisterService();
                _nsd = null;
            }
        }

        /// <summary>
        /// Stop the service. Called by OnDestroy()
        /// </summary>
        public void StopService()
        {
            Toast.MakeText(this, "The dm service has stopped", ToastLength.Long).Show();
            NotificationManager nManager =
               GetSystemService(Context.NotificationService) as NotificationManager;
            nManager.CancelAll();

            if (_stopServiceReceiver != null)
            {
                UnregisterReceiver(_stopServiceReceiver);
            }
            StopServer();
        }

        /// <summary>
        /// Called to start the IP network thread and NSD
        /// </summary>
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
            }
        }

        /// <summary>
        /// Start the NSD (Network Service Discovery) Allows Clients to find
        /// servers without knowing an IP before hand.
        /// </summary>
        /// <param name="port"></param>
        private void StartNSD(int port)
        {
            _nsd = new GSNsdHelper(this);
            _nsd.StartHelper();
            _nsd.RegisterService(port);
        }

        /// <summary>
        /// Called by the Activity OnSaveInstance. Allows the activity to save 
        /// the campaign data in service memory between activity instances.
        /// </summary>
        /// <param name="campaign"></param>
        public void UpdateCampaign(Campaign campaign)
        {
            int i = 0;
            foreach (var pair in campaign)
            {
                i++;
                _campaign[pair.Key]=pair.Value;
            }
            Log.Debug(TAG, "UpdateCampaign Called. "+i+" characters saved!");
        }

        /// <summary>
        /// Called when a client sends a character sheet. Add it to the campaign.
        /// </summary>
        /// <param name="cs"></param>
        private void UpdateCharacterSheet(CharacterSheet cs)
        {
            if(cs == null)
            {
                return;
            }

            if (!_campaign.IsMember(cs.ID))
            {
                // Need an ID.
                if (String.IsNullOrWhiteSpace(cs.ID))
                {
                    return;
                }

                _campaign.AddPlayer(cs.ID,cs);
            }
            else
            {
                _campaign[cs.ID] = cs;
            }
        }

        /// <summary>
        /// A broadcast receiver to stop the service. Called by the Click to Stop notification.
        /// </summary>
        class DmStopServiceReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get DmServerService
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

    /// <summary>
    /// DmServerBinder allows Interprocess Communication between Service and activity.
    /// </summary>
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
            return service.IsServiceRunning;
        }
    }
}
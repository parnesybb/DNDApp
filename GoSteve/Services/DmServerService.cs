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

namespace GoSteve.Services
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.DmServerService" })]
    public class DmServerService : Service
    {
        DmServerBinder binder;

        private volatile bool _isServerUp;
        private Thread _serverThread;
        private GSNsdHelper _nsd;
        private TcpListener _server;

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug("DmServerService", "DemoService started");

            StartServiceInForeground();

            DoWork();

            return StartCommandResult.Sticky;
        }

        void StartServiceInForeground()
        {
            var ongoing = new Notification(Resource.Drawable.Icon, "DmServerService in foreground");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            ongoing.SetLatestEventInfo(this, "DmServerService", "DmServerService is running in the foreground", pendingIntent);

            StartForeground((int)NotificationFlags.ForegroundService, ongoing);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            Log.Debug("DmServerService", "DemoService stopped");
        }

        void SendNotification()
        {
            var nMgr = (NotificationManager)GetSystemService(NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, "Message from demo service");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(DmScreenBase)), 0);
            notification.SetLatestEventInfo(this, "Demo Service Notification", "Message from demo service", pendingIntent);
            nMgr.Notify(0, notification);
        }

        public void DoWork()
        {
            Toast.MakeText(this, "The demo service has started", ToastLength.Long).Show();

            var t = new Thread(() => {

                SendNotification();

                Thread.Sleep(5000);

                Log.Debug("DmServerService", "Stopping foreground");
                StopForeground(true);

                StopSelf();
            }
            );

            t.Start();
        }

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            binder = new DmServerBinder(this);
            return binder;
        }

        public List<CharacterSheet> GetCharacterSheets()
        {
            return null;
        }

        private void StartServer(int port)
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

                CharacterSheet cs = null;
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
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        client.Close();
                        buffer = null;
                        cs = null;
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
            if (_server != null)
            {
                _server.Stop();
                _serverThread.Join();
            }
        }

        private void StopNSD()
        {
            //_nsd.StopDiscovery();
            _nsd.UnregisterService();
        }

        private void StartNSD()
        {
            _nsd = new GSNsdHelper(this);
            _nsd.StartHelper();
            _nsd.RegisterService(port);
        }
    }

    public class DmServerBinder : Binder
    {
        DmServerService service;

        public DmServerBinder(DmServerService service)
        {
            this.service = service;
        }

        public DmServerService GetDemoService()
        {
            return service;
        }
    }
}
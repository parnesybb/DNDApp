
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using GoSteve;
using GoSteve.Buttons;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Server
{
    [Activity(Label = "Client")]
    public class GSClient : Activity
    {
        private BinaryFormatter _bf;
        //private string _serverHost;
        //private int _serverPort;
        private GSNsdHelper _nsd;
        private Dictionary<string, ServerButton> list;
        private LinearLayout _layout;

        public const string TAG = "GSClient";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            list = new Dictionary<string, ServerButton>();

            _bf = new BinaryFormatter();
            _nsd = new GSNsdHelper(this);

            this._layout = new LinearLayout(this);
            this._layout.Orientation = Orientation.Vertical;
            SetContentView(this._layout);

            Log.Info("GSClient", "Check it out I'm running I'm running: ");

            _nsd.ServiceFound += (sender, args) =>
            {
                AddServerButton(args.UpdatedNsdServiceInfo.Host.HostName, args.UpdatedNsdServiceInfo.Port);

                //AlertDialog.Builder b = new AlertDialog.Builder(this);
                //b.SetMessage("Server Found: " + args.UpdatedNsdServiceInfo.Host + "\nPort: " + args.UpdatedNsdServiceInfo.Port);
                //b.Show();
                //Log.Info("GSClient", "Server Found: " + args.UpdatedNsdServiceInfo.Host + "\nPort: " + args.UpdatedNsdServiceInfo.Port);

                //_serverHost = args.UpdatedNsdServiceInfo.Host.HostName;
                //_serverPort = args.UpdatedNsdServiceInfo.Port;
                //var cs = this.CreateFakeRequest();
                //SendUpdate(cs);
            };

            _nsd.StartHelper();
            _nsd.DiscoverServices();
        }

        private void AddServerButton(string hostName, int port)
        {
            RunOnUiThread(() =>
            {
                ServerButton btn = new ServerButton(this, hostName, port);
                btn.Click += (btnSender, btnArgs) =>
                {
                    var cs = this.CreateFakeRequest();
                    SendUpdate(btn.HostName, btn.Port, cs);
                };
                list.Add(hostName,btn);
                _layout.AddView(btn);
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _nsd.StopDiscovery();
        }

        private void SendUpdate(string serverHost, int serverPort, CharacterSheet cs)
        {
            // make connection
            var server = new TcpClient();
            try
            {
                server.Connect(serverHost, serverPort);
            }
            catch(SocketException ex)
            {
                AlertDialog.Builder failAlert = new AlertDialog.Builder(this);
                failAlert.SetMessage("Error Could not connect to Host: " + serverHost + "\nPort: " + serverPort);
                failAlert.Show();
                Log.Debug(TAG, "Exception Occurred:"+ex);
                _layout.RemoveView(list[serverHost]);
                list.Remove(serverHost);
                return;
            }
            var stream = server.GetStream();

            // serialize
            byte[] csBytes = null;
            var ms = new System.IO.MemoryStream();
            _bf.Serialize(ms, cs);
            csBytes = ms.ToArray();
            ms.Close();

            // send via tcp
            var dataLength = BitConverter.GetBytes((Int32)csBytes.Length);
            stream.Write(dataLength, 0, 4);
            stream.Write(csBytes, 0, csBytes.Length);

            // get response...should be character id.
            byte[] resp = new byte[256];
            var respText = String.Empty;
            stream.Read(resp, 0, resp.Length);
            respText = ASCIIEncoding.ASCII.GetString(resp);
            cs.ID = respText;

            // close
            stream.Close();
            server.Close();

            AlertDialog.Builder b = new AlertDialog.Builder(this);
            b.SetMessage("Successfully Sent character sheet to Server: " + serverHost + "\nPort: " + serverPort);
            b.Show();
        }

        private CharacterSheet CreateFakeRequest()
        {
            var cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            cs.SetRace(KnownValues.Race.DWARF, true);
            cs.Background = KnownValues.Background.SOLDIER;
            cs.CharacterName = "Flaf";

            return cs;
        }
    }
}

using Android.App;
using Android.OS;
using Android.Util;
using GoSteve;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Server
{
    [Activity(Label = "Client")]
    public class GSClient : Activity
    {
        private BinaryFormatter _bf;
        private string _serverHost;
        private int _serverPort;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _bf = new BinaryFormatter();
            var nsd = new GSNsdHelper(this);

            Log.Info("GSClient", "Check it out I'm running I'm running: ");

            nsd.ServiceFound += (sender, args) =>
            {
                AlertDialog.Builder b = new AlertDialog.Builder(this);
                b.SetMessage("Server Found: " + args.UpdatedNsdServiceInfo.Host + "\nPort: " + args.UpdatedNsdServiceInfo.Port);
                b.Show();
                Log.Info("GSClient", "Server Found: " + args.UpdatedNsdServiceInfo.Host + "\nPort: " + args.UpdatedNsdServiceInfo.Port);

                _serverHost = args.UpdatedNsdServiceInfo.Host.HostName;
                _serverPort = args.UpdatedNsdServiceInfo.Port;
                var cs = this.CreateFakeRequest();
                SendUpdate(cs);
            };

            nsd.StartHelper();
            nsd.DiscoverServices();
        }

        private void SendUpdate(CharacterSheet cs)
        {
            // make connection
            var server = new TcpClient();
            server.Connect(_serverHost, _serverPort);
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

            stream.Flush();

            // close
            stream.Close();
            server.Close();
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
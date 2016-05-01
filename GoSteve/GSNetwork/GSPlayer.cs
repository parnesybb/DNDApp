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
using Server;
using GoSteve.Buttons;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoSteve.GSNetwork
{
    public delegate void NewDMFound(object sender, DMFoundEvent args);

    public class DMFoundEvent : EventArgs
    {
        public string DmIdentity { get; set; }
    }

    public class GSPlayer
    {
        private GSNsdHelper _nsd;
        private Dictionary<string, GSDmInfo> _servers;
        private Context _context;
        private CharacterSheet _cs;
        private BinaryFormatter _bf;

        public event NewDMFound OnDmDetected;

        public GSPlayer(Context context, CharacterSheet cs)
        {
            _nsd = new GSNsdHelper(context);
            _servers = new Dictionary<string, GSDmInfo>();
            _context = context;
            _cs = cs;
            _bf = new BinaryFormatter();

            _nsd.ServiceFound += (sender, args) =>
            {
                AddServerButton(args.UpdatedNsdServiceInfo.Host.HostName, args.UpdatedNsdServiceInfo.Port);
            };
        }

        public bool SendUpdate(string dm)
        {
            if (_servers.ContainsKey(dm))
            {
               return SendUpdate(_servers[dm].HostName, _servers[dm].Port, _cs);
            }

            return false;
        }

        public GSNsdHelper NsdHelper
        {
            get
            {
                return _nsd;
            }
        }

        private void AddServerButton(string hostName, int port)
        {
            GSDmInfo dm = new GSDmInfo(hostName, port);

            if (!_servers.ContainsKey(hostName))
            {
                _servers.Add(hostName, dm);

                if (OnDmDetected != null)
                {
                    var e = new DMFoundEvent();
                    e.DmIdentity = hostName;
                    OnDmDetected(this, e);
                }
            } 
        }

        private bool SendUpdate(string serverHost, int serverPort, CharacterSheet cs)
        {
            // make connection
            var server = new TcpClient();
            try
            {
                server.Connect(serverHost, serverPort);
            }
            catch (SocketException ex)
            {
                AlertDialog.Builder failAlert = new AlertDialog.Builder(_context);
                failAlert.SetMessage("Error Could not connect to Host: " + serverHost + "\nPort: " + serverPort);
                failAlert.Show();
                return false;
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

            AlertDialog.Builder b = new AlertDialog.Builder(_context);
            b.SetMessage("Successfully Sent character sheet to Server: " + serverHost + "\nPort: " + serverPort);
            b.Show();
            return true;
        }
    }
}
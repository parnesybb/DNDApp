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
using GoSteve.Structures;
using System.Runtime.Serialization.Formatters.Binary;
using Server;
using Java.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Android.Util;

namespace GoSteve.Screens
{
    [Activity(Label = "DmScreenBase")]
    public class DmScreenBase : Activity
    {
        private volatile bool _isServerUp;
        private Thread _serverThread;
        private GSNsdHelper _nsd;
        private Dictionary<string, CharacterSheet> _charSheets;
        private int _buttonCount;
        private LinearLayout _layout;
        private Button _broadcastBtn;

        // new Thread variables
        private TcpListener _server;

        public DmScreenBase()
        {
            this._charSheets = new Dictionary<string, CharacterSheet>();
            this._buttonCount = 0;
        }

        public void Update(CharacterSheet cs)
        {
            if (!_charSheets.Keys.Contains(cs.ID))
            {
                // Need an ID.
                if (String.IsNullOrWhiteSpace(cs.ID))
                {
                    return;
                }

                // New player.
                _charSheets.Add(cs.ID, cs);

                var b = new CharacterButton(this)
                {
                    Id = ++_buttonCount,
                    CharacterID = cs.ID,
                    Text = cs.CharacterName
                };

                b.Click += (sender, args) =>
                {
                    // Screen to call. This will be an instance of Mike's character screen.
                    var charScreen = new Intent(this, typeof(TestScreen));

                    // For serialzation.
                    byte[] csBytes = null;
                    var ms = new System.IO.MemoryStream();
                    var formatter = new BinaryFormatter();

                    // Serialize the character sheet.
                    formatter.Serialize(ms, _charSheets[b.CharacterID]);
                    csBytes = ms.ToArray();
                    ms.Close();

                    // Send data to new character sheet screen.
                    charScreen.PutExtra("charSheet", csBytes);
                    StartActivity(charScreen);
                };

                this._layout.AddView(b);
            }
            else
            {
                // Update the dictionary for existing player.
                this._charSheets[cs.ID] = cs;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this._layout = new LinearLayout(this);
            this._layout.Orientation = Orientation.Vertical;
            SetContentView(this._layout);

            _broadcastBtn = new Button(this);
            _broadcastBtn.Id = Button.GenerateViewId();
            _broadcastBtn.Text = "Start Broadcast Session";
            _broadcastBtn.Click += (sender, args) =>
            {
                ToggleServerButtonState();
            };

            _layout.AddView(_broadcastBtn);
        }

        protected override void OnDestroy()
        {
            Log.Info("DmScreenBase","OnDestroy called!");
            if (_nsd != null)
            {
                _nsd.UnregisterService();
            }
            StopServer();

            base.OnDestroy();
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
                    Log.Info("DmScreenBase", "IP: "+ip);
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
                        RunOnUiThread(() => Update(cs));

                        // Return the ID to client.
                        retToClient = ASCIIEncoding.ASCII.GetBytes(cs.ID);
                        stream.Write(retToClient, 0, retToClient.Length);

                        Log.Info("DMScreenBase","Receive character ID:"+cs.ID);

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

        private void ToggleServerButtonState()
        {
            if(_serverThread!=null && _serverThread.IsAlive)
            {
                StopNSD();
                StopServer();
                _broadcastBtn.Text = "Start Broadcast Session";
            }
            else
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

                _serverThread = new Thread(() => StartServer(port));
                _serverThread.Start();

                // announce server/port
                _nsd = new GSNsdHelper(this);
                _nsd.StartHelper();
                _nsd.RegisterService(port);

                _broadcastBtn.Text = "Stop Broadcast Session";
            }
        }

    }
}
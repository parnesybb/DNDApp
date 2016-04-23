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
using GoSteve.Services;

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

        public bool IsBound { get; set; }
        public DmServerServiceConnection ServiceConnection { set; get; }
        public DmServerBinder Binder { get; set; }

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

        protected override void OnStart()
        {
            base.OnStart();

            var dmServerServiceIntent = new Intent(DmServerService.IntentFilter);
            ServiceConnection = new DmServerServiceConnection(this);
            ApplicationContext.BindService(dmServerServiceIntent, ServiceConnection, Bind.AutoCreate);
        }

        protected override void OnDestroy()
        {
            Log.Info("DmScreenBase", "OnDestroy called!");

            base.OnDestroy();
        }

        private void ToggleServerButtonState()
        {
            if(IsBound)
            {
                //stop
                _broadcastBtn.Text = "Start Broadcast Session";
            }
            else
            {
                _broadcastBtn.Text = "Stop Broadcast Session";
            }
        }

        private void StopService()
        {

        }

        private void StartService()
        {

        }

        class DmServiceReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                ((DmServerService)context).GetCharacterSheets();

                InvokeAbortBroadcast();
            }
        }

    }

    public class DmServerServiceConnection : Java.Lang.Object, IServiceConnection
    {
        DmScreenBase activity;

        public DmServerBinder Binder { get; set; }

        public DmServerServiceConnection(DmScreenBase activity)
        {
            this.activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var demoServiceBinder = service as DmServerBinder;
            if (demoServiceBinder != null)
            {
                var binder = (DmServerBinder)service;
                activity.Binder = binder;
                activity.IsBound = true;

                // keep instance for preservation across configuration changes
                this.Binder = (DmServerBinder)service;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.IsBound = false;
        }
    }
}
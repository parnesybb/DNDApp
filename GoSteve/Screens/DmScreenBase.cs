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
    [Activity(Label = "DmScreenBase", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class DmScreenBase : Activity
    {
        private Dictionary<string, CharacterSheet> _charSheets;
        private int _buttonCount;
        private LinearLayout _layout;
        private Button _broadcastBtn;
        private DmServiceReceiver _characterReceiver;
        //private DmStopServiceReceiver _stopServiceReceiver;
        private DmServerStateChangedReceiver _serverStateReceiver;
        private Intent _dmServerServiceIntent;

        public bool IsBound { get; set; }
        public bool IsServiceUp { get; set; }
        public DmServerServiceConnection ServiceConnection { set; get; }
        public DmServerBinder Binder { get; set; }

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
                    var gsMsg = new GSActivityMessage();

                    gsMsg.Message = CharacterSheet.GetBytes(_charSheets[b.CharacterID]);
                    charScreen.PutExtra(gsMsg.CharacterMessage, gsMsg.Message);
                    StartActivity(charScreen);

                    //// For serialzation.
                    //byte[] csBytes = null;
                    //var ms = new System.IO.MemoryStream();
                    //var formatter = new BinaryFormatter();

                    //// Serialize the character sheet.
                    //formatter.Serialize(ms, _charSheets[b.CharacterID]);
                    //csBytes = ms.ToArray();
                    //ms.Close();

                    //// Send data to new character sheet screen.
                    //charScreen.PutExtra("charSheet", csBytes);
                    //StartActivity(charScreen);
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

            _characterReceiver = new DmServiceReceiver();
            //_stopServiceReceiver = new DmStopServiceReceiver();
            _serverStateReceiver = new DmServerStateChangedReceiver();

            _broadcastBtn = new Button(this);
            _broadcastBtn.Id = Button.GenerateViewId();
            _broadcastBtn.Text = "Start Broadcast Session";
            _broadcastBtn.Click += (sender, args) =>
            {
                ToggleServerButtonState();
            };

            UpdateButton();

            _layout.AddView(_broadcastBtn);

            //TEST
            //var cs = new CharacterSheet();
            //cs.CharacterName = "TEST";
            //cs.SetRace(KnownValues.Race.DRAGONBORN, true);
            //cs.Background = KnownValues.Background.ACOLYTE;
            //cs.SetClass(KnownValues.ClassType.BARBARIAN, true);
            //cs.ID = new Guid().ToString();
            //CharacterSheet.WriteToFile(cs);
            //var csFromFile = CharacterSheet.ReadFromFile(cs.CharacterName);
            //this.Update(csFromFile);
        }

        protected override void OnStart()
        {
            base.OnStart();

            var intentFilter = new IntentFilter(DmServerService.CharSheetUpdatedAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_characterReceiver, intentFilter);

            //var stopServIntentFilter = new IntentFilter(ShutdownDmServerService.StopServerServiceAction) { Priority = (int)IntentFilterPriority.HighPriority };
            //RegisterReceiver(_stopServiceReceiver, stopServIntentFilter);

            var serverStateIntentFilter = new IntentFilter(DmServerService.ServerStateChangedAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_serverStateReceiver, serverStateIntentFilter);

            _dmServerServiceIntent = new Intent(this, typeof(DmServerService));
            ServiceConnection = new DmServerServiceConnection(this);
            ApplicationContext.BindService(_dmServerServiceIntent, ServiceConnection, Bind.AutoCreate);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Log.Info("DmScreenBase", "OnDestroy called!");

            UnregisterReceiver(_characterReceiver);
            //UnregisterReceiver(_stopServiceReceiver);
            UnregisterReceiver(_serverStateReceiver);


            UnbindDmServerService();
        }

        private void ToggleServerButtonState()
        {
            if(IsServiceUp)
            {
                //stop service
                StopDmServerService();
            }
            else
            {

                //start service
                StartDmServerService();
            }
        }

        public void UpdateButton()
        {
            RunOnUiThread(() =>
            {
                IsServiceUp = DmServerService.IsServiceRunning;
                if (IsServiceUp)
                {
                    //stop service
                    _broadcastBtn.Text = "Stop Broadcast Session";
                }
                else
                {
                    //start service
                    _broadcastBtn.Text = "Start Broadcast Session";
                }
            });
        }

        private void BindDmServerService()
        {
            if (!IsBound)
            {
                ApplicationContext.BindService(_dmServerServiceIntent, ServiceConnection, Bind.AutoCreate);
                IsBound = true;
            }
        }

        private void UnbindDmServerService()
        {
            if (IsBound)
            {
                ApplicationContext.UnbindService(ServiceConnection);
                IsBound = false;
            }
        }

        private void StopDmServerService()
        {
            _broadcastBtn.Text = "Start Broadcast Session";
            UnbindDmServerService();
            //StopService(new Intent(DmServerService.IntentFilter));
            ApplicationContext.StopService(new Intent(this, typeof(DmServerService)));
        }

        private void StartDmServerService()
        {
            _broadcastBtn.Text = "Stop Broadcast Session";
            BindDmServerService();
            ApplicationContext.StartService(new Intent(this, typeof(DmServerService)));
        }

        void UpdateCharacterSheets()
        {
            if (IsBound)
            {
                RunOnUiThread(() => {
                    var locCs = DmServerService.Service.GetCharacterSheets();

                    if (locCs != null)
                    {
                        Update(locCs);
                    }
                    else
                    {
                        Log.Debug("DmScreenBase", "charactersheet is null");
                    }
                }
                );
            }
        }

        class DmServiceReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                ((DmScreenBase)context).UpdateCharacterSheets();

                InvokeAbortBroadcast();
            }
        }

        /*
        class DmStopServiceReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                ((DmScreenBase)context).RunOnUiThread(() =>
                {
                    ((DmScreenBase)context).StopDmServerService();
                });

                InvokeAbortBroadcast();
            }
        }
        */

        class DmServerStateChangedReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                ((DmScreenBase)context).UpdateButton();

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
                activity.UpdateButton();

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
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
    [Activity(Label = "DM Mode", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class DmScreenBase : Activity
    {
        private Campaign _campaign;
        private int _buttonCount;
        private LinearLayout _layout;
        private Button _genEncounterBtn;
        private Button _broadcastBtn;
        private DmServiceReceiver _characterReceiver;
        //private DmStopServiceReceiver _stopServiceReceiver;
        private DmServerStateChangedReceiver _serverStateReceiver;
        private Intent _dmServerServiceIntent;

        public bool IsBound { get; set; }
        public bool IsServiceUp { get; set; }
        public DmServerServiceConnection ServiceConnection { set; get; }
        public DmServerBinder Binder { get; set; }

        public const string CharacterDictionaryMessage = "CharacterDictionaryMessage";

        public DmScreenBase()
        {
            _campaign = new Campaign();
            this._buttonCount = 0;
        }

        public void Update(CharacterSheet cs)
        {
            if (!_campaign.IsMember(cs.ID))
            {
                // Need an ID.
                if (String.IsNullOrWhiteSpace(cs.ID))
                {
                    return;
                }

                // New player.
                _campaign.AddPlayer(cs.ID, cs);

                var b = new CharacterButton(this)
                {
                    Id = ++_buttonCount,
                    CharacterID = cs.ID,
                    Text = cs.CharacterName
                };

                b.Click += (sender, args) =>
                {
                    // Screen to call. This will be an instance of Mike's character screen.
                    var charScreen = new Intent(this, typeof(CharacterScreen));
                    var gsMsg = new GSActivityMessage();

                    gsMsg.Message = CharacterSheet.GetBytes(_campaign[b.CharacterID]);
                    charScreen.PutExtra(gsMsg.CharacterMessage, gsMsg.Message);
                    StartActivity(charScreen);
                };

                // Remove the player.
                b.LongClick += (sender, args) =>
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Remove Player?");
                    alert.SetMessage("Do you want to remove this player");

                    alert.SetPositiveButton("Yes", (s, e) =>
                    {
                        b.Visibility = ViewStates.Invisible;
                        _campaign.RemovePlayer(b.CharacterID);
                        _layout.RemoveView(b);
                    });

                    alert.SetNegativeButton("No", (s, e) => { });

                    RunOnUiThread(() => { alert.Show(); });
                };

                this._layout.AddView(b);
            }
            else
            {
                // Update the dictionary for existing player.
                this._campaign[cs.ID] = cs;
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

            // Encounter button
            _genEncounterBtn = new Button(this);
            _genEncounterBtn.Id = Button.GenerateViewId();
            _genEncounterBtn.Text = "Generate an Encounter";
            _genEncounterBtn.Click += (s, e) =>
            {
                // popup menu
                var menu = new PopupMenu(this, _genEncounterBtn);
                menu.Menu.Add("1 Player per Monster");
                menu.Menu.Add("2 Players per Monster");
                menu.Menu.Add("1 Player per 2 Monsters");
                menu.MenuItemClick += (ss, ee) =>
                {
                    var alert = new AlertDialog.Builder(this);
                    var userChoice = Campaign.DesiredDifficulty.M1P1;
                    var encounter = String.Empty; alert.SetTitle("Created An Encounter");

                    switch (ee.Item.ToString())
                    {
                        case "1 Player per Monster":
                            userChoice = Campaign.DesiredDifficulty.M1P1;
                            break;
                        case "2 Players per Monster":
                            userChoice = Campaign.DesiredDifficulty.M1P2;
                            break;
                        case "1 Player per 2 Monsters":
                            userChoice = Campaign.DesiredDifficulty.M2P1;
                            break;
                    }

                    // show result
                    encounter = _campaign.GenerateEncounter(userChoice);
                    alert.SetMessage(encounter);
                    alert.SetPositiveButton("Okay", (sss, eee) => { });
                    RunOnUiThread(() => { alert.Show(); });
                };

                menu.Show();
            };
            _layout.AddView(_genEncounterBtn);

            //TEST
            var csx = new CharacterSheet();
            csx.CharacterName = "TEST";
            csx.SetRace(KnownValues.Race.DRAGONBORN, true);
            csx.Background = KnownValues.Background.ACOLYTE;
            csx.SetClass(KnownValues.ClassType.BARBARIAN, true);
            csx.ID = new Guid().ToString();
            csx.Level = 5;
            //CharacterSheet.WriteToFile(cs);
            //var csFromFile = CharacterSheet.ReadFromFile(cs.CharacterName);
            //this.Update(csFromFile);
            this.Update(csx);

            if (savedInstanceState != null)
            {
                byte[] csBytes = null;
                BinaryFormatter _formatter = new BinaryFormatter();

                var gsMsg = new GSActivityMessage();
                gsMsg.Message = savedInstanceState.GetByteArray(CharacterDictionaryMessage);
                var ms = new System.IO.MemoryStream(gsMsg.Message);
                var cs = _formatter.Deserialize(ms) as CharacterSheet;
                ms.Close();
                /*
                var gsMsg = new GSActivityMessage();
                gsMsg.Message = CharacterSheet.GetBytes(_charSheets[b.CharacterID]);
                charScreen.PutExtra(gsMsg.CharacterMessage, gsMsg.Message);

                _counter = savedInstanceState.GetExtra("click_count", 0);
                Log.Debug(GetType().FullName, "Activity A - Recovered instance state");
                */
            }
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

        protected override void OnSaveInstanceState(Bundle outState)
        {
            byte[] csBytes = null;
            BinaryFormatter _formatter = new BinaryFormatter();
            var ms = new System.IO.MemoryStream();

            // Serialize the character sheet.
            _formatter.Serialize(ms, _campaign);
            csBytes = ms.ToArray();
            outState.PutByteArray(CharacterDictionaryMessage, csBytes);
            ms.Close();

            Log.Debug(GetType().FullName, "Activity A - Saving instance state");

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
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
            if (IsServiceUp)
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
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
using GoSteve.Buttons;

namespace GoSteve.Screens
{
    /// <summary>
    /// This class displays the DM mode screen. It lauches a background service
    /// that runs a IP network server and DNS Domain Name Service. This allows 
    /// clients to connect and update charactersheets
    /// </summary>
    [Activity(Label = "DM Mode", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class DmScreenBase : Activity
    {
        private Campaign _campaign;
        private int _buttonCount;
        private LinearLayout _controlsLayout;
        private LinearLayout _characterLayout;
        private Button _genEncounterBtn;
        private Button _broadcastBtn;
        public DmServerService ServerService { get; set; }
        private DmServiceCharacterReceiver _characterReceiver;
        //private DmStopServiceReceiver _stopServiceReceiver;
        private DmServerStateChangedReceiver _serverStateReceiver;
        private Intent _dmServerServiceIntent;

        public bool IsBound { get; set; }
        public bool IsServiceUp { get; set; }
        public DmServerServiceConnection ServiceConnection { set; get; }
        public DmServerBinder Binder { get; set; }

        public const string CharacterCampaignMessage = "CharacterCampaignMessage";
        private const string TAG = "DmScreenBase";

        public DmScreenBase()
        {
            _campaign = new Campaign();
            this._buttonCount = 0;
            
        }

        /// <summary>
        /// Update charactersheets in the campaign and add a button if it is a new
        /// charactersheet.
        /// </summary>
        /// <param name="cs">Charactersheet to add or update</param>
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
                        _characterLayout.RemoveView(b);
                    });

                    alert.SetNegativeButton("No", (s, e) => { });

                    RunOnUiThread(() => { alert.Show(); });
                };

                this._characterLayout.AddView(b);
            }
            else
            {
                // Update the dictionary for existing player.
                this._campaign[cs.ID] = cs;
            }
        }

        /// <summary>
        /// Called by Android OS when the activity is created
        /// </summary>
        /// <param name="savedInstanceState">Contains previous instance data</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DmScreenBase);

            CharacterScreen.IsDM = true;

            _characterLayout = FindViewById<LinearLayout>(Resource.Id.dmScreenBaseCharacterLayout);
            _controlsLayout = FindViewById<LinearLayout>(Resource.Id.dmScreenBaseControlsLayout);

            //Initialize broadcast receivers
            _characterReceiver = new DmServiceCharacterReceiver();
            //_stopServiceReceiver = new DmStopServiceReceiver();
            _serverStateReceiver = new DmServerStateChangedReceiver();

            // Intent to start the Server Service Connection
            // This allows the activity to get the binder from the Service
            _dmServerServiceIntent = new Intent(this, typeof(DmServerService));
            ServiceConnection = new DmServerServiceConnection(this);

            _broadcastBtn = new Button(this);
            _broadcastBtn.Id = Button.GenerateViewId();
            _broadcastBtn.Text = "Start Broadcast Session";
            _broadcastBtn.Click += (sender, args) =>
            {
                ToggleServerButtonState();
            };

            _controlsLayout.AddView(_broadcastBtn);

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
            _controlsLayout.AddView(_genEncounterBtn);

            // Load the previously saved data
            /*
            if (savedInstanceState != null)
            {
                BinaryFormatter _formatter = new BinaryFormatter();

                var gsMsg = new GSActivityMessage();
                gsMsg.Message = savedInstanceState.GetByteArray(CharacterCampaignMessage);
                var ms = new System.IO.MemoryStream(gsMsg.Message);
                var tempCampaign = _formatter.Deserialize(ms) as Campaign;
                ms.Close();

                foreach (var pair in tempCampaign)
                {
                    Update(pair.Value);
                }
            }*/

            UpdateButton();
        }

        /// <summary>
        /// Called by Android OS when the activity is started
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            // Register Broadcast Recievers
            // This allows the service send the activity messages
            var intentFilter = new IntentFilter(DmServerService.CharSheetUpdatedAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_characterReceiver, intentFilter);

            //var stopServIntentFilter = new IntentFilter(ShutdownDmServerService.StopServerServiceAction) { Priority = (int)IntentFilterPriority.HighPriority };
            //RegisterReceiver(_stopServiceReceiver, stopServIntentFilter);

            var serverStateIntentFilter = new IntentFilter(DmServerService.ServerStateChangedAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_serverStateReceiver, serverStateIntentFilter);

            // "Bind" to service
            // This setups up the ServerServiceConnection.
            // Also it gives us the binder for the service
            BindDmServerService();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// Called by Android OS when an activity should save data between instances
        /// </summary>
        /// <param name="outState">State data</param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            /*
            byte[] csBytes = null;
            BinaryFormatter _formatter = new BinaryFormatter();
            var ms = new System.IO.MemoryStream();

            // Serialize the character sheet.
            _formatter.Serialize(ms, _campaign);
            csBytes = ms.ToArray();
            outState.PutByteArray(CharacterCampaignMessage, csBytes);
            ms.Close();
            */

            Log.Debug(TAG, "Activity A - Saving instance state");

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }

        /// <summary>
        /// Called by Android OS when an activity is destroyed
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            Log.Info(TAG, "OnDestroy called!");

            if (IsServiceRunning())
            {
                ServerService.UpdateCampaign(_campaign);
            }

            // Unregister Broadcast receivers
            UnregisterReceiver(_characterReceiver);
            //UnregisterReceiver(_stopServiceReceiver);
            UnregisterReceiver(_serverStateReceiver);

            //Unbind the service
            UnbindDmServerService();
        }

        /// <summary>
        /// Determines if the DM Server Service is running
        /// </summary>
        /// <returns>true if is running, else it is not running</returns>
        private bool IsServiceRunning()
        {
            return IsBound && ServerService != null && ServerService.IsServiceRunning;
        }

        /// <summary>
        /// Called when the user clicks the Start/Stop Broadcast button
        /// </summary>
        private void ToggleServerButtonState()
        {
            if (IsServiceRunning())
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

        /// <summary>
        /// Called by the DM server service when the service has been started or stopped
        /// </summary>
        public void UpdateButton()
        {
            RunOnUiThread(() =>
            {
                if (IsServiceRunning())
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

        /// <summary>
        /// Called when the activity wants to bind to the DM server service
        /// </summary>
        private void BindDmServerService()
        {
            if (!IsBound)
            {
                ApplicationContext.BindService(_dmServerServiceIntent, ServiceConnection, 0);
                IsBound = true;
            }
        }

        /// <summary>
        /// Called when the activity wants to unbind to the DM server service
        /// </summary>
        private void UnbindDmServerService()
        {
            if (IsBound)
            {
                ApplicationContext.UnbindService(ServiceConnection);
                IsBound = false;
            }
        }

        /// <summary>
        /// Called when the activity wants to stop the DM server service
        /// </summary>
        private void StopDmServerService()
        {
            _broadcastBtn.Text = "Start Broadcast Session";
            UnbindDmServerService();
            //StopService(new Intent(DmServerService.IntentFilter));
            Log.Debug(TAG, "User tried Stopping The DMServerService!");
            StopService(new Intent(this, typeof(DmServerService)));
            //ApplicationContext.StopService(new Intent(DmServerService.IntentFilter));
        }

        /// <summary>
        /// Called when the activity wants to stop the DM server service
        /// </summary>
        private void StartDmServerService()
        {
            _broadcastBtn.Text = "Stop Broadcast Session";
            ApplicationContext.StartService(new Intent(this, typeof(DmServerService)));
            //ApplicationContext.StartService(new Intent(DmServerService.IntentFilter));
            BindDmServerService();
        }

        /// <summary>
        /// Called when the service received a new character sheet
        /// </summary>
        public void LoadCampaign()
        {
            Log.Debug(TAG,"LoadCampaign Reached");
            if (IsServiceRunning())
            {
                RunOnUiThread(() => {
                    var locCampaign = ServerService.GetCampaign();

                    if (locCampaign != null)
                    {
                        int i = 0;
                        foreach (var pair in locCampaign)
                        {
                            i++;
                            Update(pair.Value);
                        }
                        Log.Debug(TAG, i + " Characters loaded!");
                        
                    }
                    else
                    {
                        Log.Debug(TAG, "campaign is null");
                    }
                }
                );
            }
        }

        /// <summary>
        /// Called when the service received a new character sheet
        /// </summary>
        public void LoadCharacterSheet()
        {
            Log.Debug(TAG, "LoadCharacterSheet Reached");
            if (IsServiceRunning())
            {
                RunOnUiThread(() => {
                    var locCs = ServerService.GetCharacterSheet();

                    if (locCs != null)
                    {
                        Update(locCs);
                        ServerService.ResetCharacterSheet();
                        Log.Debug(TAG, "Character loaded!");

                    }
                    else
                    {
                        Log.Debug(TAG, "charactersheet is null");
                    }
                }
                );
            }
        }

        /// <summary>
        /// A Broadcast Receiver that is called by DM Server Service when
        /// a charactersheet has been updated
        /// </summary>
        class DmServiceCharacterReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                // Get Character sheets
                ((DmScreenBase)context).LoadCharacterSheet();

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

        /// <summary>
        /// A Broadcast receiver called by the DM Server Service start/stop
        /// state has changed
        /// </summary>
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

    /// <summary>
    /// Called by Android OS from Bind and Unbinding. This sets up the DM Server Service binder.
    /// The Binder allows the activity to call service methods.
    /// </summary>
    public class DmServerServiceConnection : Java.Lang.Object, IServiceConnection
    {
        DmScreenBase activity;

        public DmServerBinder Binder { get; set; }

        /// <summary>
        /// Called by Android OS when a connection between the service and activity has
        /// been estabhlished.
        /// </summary>
        /// <param name="activity">DmScreenBase activity instance</param>
        public DmServerServiceConnection(DmScreenBase activity)
        {
            this.activity = activity;
        }
        
        /// <summary>
        /// Called by Android OS when a service has been connected by
        /// Bind
        /// </summary>
        /// <param name="name"></param>
        /// <param name="service"></param>
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var demoServiceBinder = service as DmServerBinder;
            if (demoServiceBinder != null)
            {
                var binder = (DmServerBinder)service;
                activity.Binder = binder;
                activity.IsBound = true;
                activity.ServerService = activity.Binder.GetDmServerService();
                activity.UpdateButton();

                // keep instance for preservation across configuration changes
                this.Binder = (DmServerBinder)service;
                activity.LoadCampaign();
            }
        }

        /// <summary>
        /// Called by Android OS when a service has been disconnected by 
        /// Unbind
        /// </summary>
        /// <param name="name"></param>
        public void OnServiceDisconnected(ComponentName name)
        {
            activity.IsBound = false;
        }
    }
}
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

namespace GoSteve.Screens
{
    [Activity(Label = "Character Screen")]
    public class CharacterScreen : Activity
    {
        private static CharacterSheet _cs;
        public static bool isDM;
        private static readonly string TAG = "CharacterScreen";

        private readonly string[] _tabNames = { "Stats/Skills", "Health/Attacks", "Features/Traits", "Prof/Langs", "Equip", "Info"};
        private Fragment[] _fragments;
        private System.Timers.Timer _timer;     

        /// <summary>
        /// Gets the character sheet being used in CharacterScreen instance.
        /// To set this, pass a character sheet by intent to CharacterScreen.
        /// </summary>
        public static CharacterSheet CharacterSheet
        {
            get
            {
                return _cs;
            }
            private set
            {
                _cs = value;
            }
        }

        //TODO
        // TIMED SAVE
        // BUTTON TO SEND TO DM
        // NEED TO SEND A BOOLEAN OR SOMETHING HERE TO SET isDM. RECEIVEMESSAGE SHOULD HANDLE THIS.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CharacterBaseScreen);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetHomeButtonEnabled(true);

            ReceiveMessage();

            _fragments = new Fragment[]
          {
                new StatsSkillsFragment(),
                new AbilitiesFragment(),
                new FeaturesTraitsFragment(),
                new ProfsLangsFragment(),
                new EquipFragment(),
                new InfoFragment()
          };

            CreateTabs();

            // XP
            var view = FindViewById<EditText>(Resource.Id.characterScreenXP);
            view.Text = _cs.Xp.ToString();
            view.TextChanged += (s, e) =>
            {
                var sender = s as EditText;
                if (!String.IsNullOrEmpty(sender.Text))
                    _cs.Xp = Int32.Parse(sender.Text);
            };

            // LEVEL
            view = FindViewById<EditText>(Resource.Id.characterScreenLevel);
            view.Text = _cs.Level.ToString();
            view.TextChanged += (s, e) =>
            {
                var sender = s as EditText;
                if (!String.IsNullOrEmpty(sender.Text))
                    _cs.Level = Int32.Parse(sender.Text);
            };

            // OTHER PERSISTENT VALUES IN VIEW
            FindViewById<TextView>(Resource.Id.characterScreenClass).Text = _cs.ClassInstance.Type.ToString();
            FindViewById<TextView>(Resource.Id.characterScreenPlayerName).Text = _cs.PlayerName;
            FindViewById<TextView>(Resource.Id.characterScreenName).Text = _cs.CharacterName;
            FindViewById<TextView>(Resource.Id.characterScreenBackground).Text = _cs.Background.ToString();
            FindViewById<TextView>(Resource.Id.characterScreenAlignment).Text = _cs.Alignment;
            if (_cs.RaceInstance.SubRace != KnownValues.SubRace.NONE)
            {
                FindViewById<TextView>(Resource.Id.characterScreenRace).Text = _cs.RaceInstance.Race.ToString() + " " +
                    _cs.RaceInstance.SubRace.ToString();
            }
            else
            {
                FindViewById<TextView>(Resource.Id.characterScreenRace).Text = _cs.RaceInstance.Race.ToString();
            }

            // Timed writer.
            _timer = new System.Timers.Timer();
            _timer.Interval = 60000; // 1 min = 60000
            _timer.AutoReset = true;
            _timer.Elapsed += TimedSave;
            _timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (_timer != null)
            {
                _timer.Enabled = false;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (_timer != null)
            {
                _timer.Enabled = true;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _timer.Stop();
            _timer.Dispose();
        }

        private void ReceiveMessage()
        {
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            _cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);
        }

        private void CreateTabs()
        {
            for (int i = 0; i < _tabNames.Length; i++)
            {
                var tab = ActionBar.NewTab();
                tab.SetText(_tabNames[i]);
                tab.TabSelected += TabClick;
                ActionBar.AddTab(tab);
            }
        }

        private void TimedSave(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_cs != null && !isDM)
            {
                CharacterSheet.WriteToFile(_cs);
            }
        }

        private void TabClick(object sender, ActionBar.TabEventArgs e)
        {
            var tab = sender as ActionBar.Tab;
            Log.Debug(TAG, "TabClick -> {0}", tab.Text);

            try
            {
                e.FragmentTransaction.Replace(Resource.Id.characterScreenDisplay, _fragments[tab.Position]);
            }
            catch (Exception)
            {/*Supress for now*/}
        }
    }
}
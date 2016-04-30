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
        private static readonly string TAG = "CharacterScreen";
        private readonly string[] _tabNames = { "Stats/Skills", "Attributes",  "Prof/Langs", "Equip", "Info"};
        private Fragment[] _fragments;
        private static CharacterSheet _cs;
        private bool isDM;

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

        private void TabClick(object sender, ActionBar.TabEventArgs e)
        {
            var tab = sender as ActionBar.Tab;
            Log.Debug(TAG, "TabClick -> {0}", tab.Text);

            //try
            //{
            //    e.FragmentTransaction.Replace(Resource.Id.characterScreenDisplay, _fragments[tab.Position]);
            //}
            //catch (Exception)
            //{/*Supress for now*/}

            switch (tab.Position)
            {
                case 0:
                    e.FragmentTransaction.Replace(Resource.Id.characterScreenDisplay, _fragments[0]);
                    break;
                case 1:
                    e.FragmentTransaction.Replace(Resource.Id.characterScreenDisplay, _fragments[1]);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;

                default:
                    break;
            }
        }
    }
}
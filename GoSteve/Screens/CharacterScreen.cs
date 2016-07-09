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
using GoSteve.Buttons;
using Server;
using GoSteve.GSNetwork;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using GoSteve.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace GoSteve.Screens
{
    [Activity(Theme = "@style/AppTheme", Label = "Character Screen")]
    public class CharacterScreen : BaseFragmentActivity
    {
        private static CharacterSheet _cs = null;
        private static bool _isDM = false;
        private static readonly string TAG = "CharacterScreen";

        //private readonly string[] _tabNames = { "Stats/Skills", "Health/Attacks", "Features/Traits", "Prof/Langs", "Equip", "Info"};
        private Fragment[] _fragments;
        private System.Timers.Timer _timer;
        private TabLayout _tabLayout;
        private ViewPager _viewPager;

        internal GSPlayer _gsPlayer;

        private IMenu _dmMenu;

        public CharacterScreen()
        {
        }

        /// <summary>
        /// Set this after creating a new instance of CharacterScreen.
        /// A value of false is default.
        /// </summary>
        public static bool IsDM
        {
            get
            {
                return _isDM;
            }
            set
            {
                _isDM = value;
            }
        }

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.CharacterBaseScreen);
            base.OnCreate(savedInstanceState);

            _tabLayout = FindViewById<TabLayout>(Resource.Id.tablayout1);
            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager1);
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
                FindViewById<TextView>(Resource.Id.characterScreenRace).Text = _cs.RaceInstance.SubRace.ToString();
            }
            else
            {
                FindViewById<TextView>(Resource.Id.characterScreenRace).Text = _cs.RaceInstance.Race.ToString();
            }

            // Player only.
            if (_cs != null && !IsDM)
            {
                _gsPlayer = new GSPlayer(this, _cs);
                _gsPlayer.NsdHelper.StartHelper();
                _gsPlayer.NsdHelper.DiscoverServices();
                _gsPlayer.OnDmDetected += (s, e) =>
                {
                    RunOnUiThread(() => 
                    {
                        if (_dmMenu != null)
                        {
                            _dmMenu.AddSubMenu(e.DmIdentity);
                        }
                    });
                };

                // Timed writer.
                _timer = new System.Timers.Timer();
                _timer.Interval = 60000; // 1 min = 60000
                _timer.AutoReset = true;
                _timer.Elapsed += TimedSave;
                _timer.Start();
            }       
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (!IsDM)
            {
                _dmMenu = menu;
                //_dmMenu = menu.AddSubMenu("Upload");
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (!_isDM)
            {
                var result = _gsPlayer.SendUpdate(item.TitleFormatted.ToString());

                if (!result)
                {
                    _dmMenu.RemoveItem(item.ItemId);
                }

                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }      
        }

        protected override void OnPause()
        {
           
            if (_gsPlayer != null)
            {
                _gsPlayer.NsdHelper.StopDiscovery();
            }

            if (_timer != null)
            {
                _timer.Enabled = false;
            }

            base.OnPause();
        }

        protected override void OnResume()
        {

            if (_gsPlayer != null)
            {
                _gsPlayer.NsdHelper.DiscoverServices();
            }

            if (_timer != null)
            {
                _timer.Enabled = true;
            }

            base.OnResume();
        }

        protected override void OnDestroy()
        {
            if (_gsPlayer!=null && _gsPlayer.NsdHelper != null)
            {
                _gsPlayer.NsdHelper.StopDiscovery();
                _gsPlayer.NsdHelper.UnregisterService();
            }

            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            base.OnDestroy();  
        }

        private void ReceiveMessage()
        {
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            _cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);
        }

        private void CreateTabs()
        {
            var _tabNames = CharSequence.ArrayFromStringArray(new[] { "Stats/Skills", "Health/Attacks", "Features/Traits", "Prof/Langs", "Equip", "Info" });
            _viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, _fragments, _tabNames);
            _tabLayout.SetupWithViewPager(_viewPager);
        }

        private void TimedSave(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_cs != null && !_isDM)
            {
                CharacterSheet.WriteToFile(_cs);
                Toast.MakeText(this, "Saved", ToastLength.Long).Show();
            }
        }
    }
}
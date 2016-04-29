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
        private CharacterSheet _cs;
        private bool isDM;

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
                new StatsSkillsFragment(_cs),
                new AbilitiesFragment(_cs),
                new ProfsLangsFragment(_cs),
                new EquipFragment(_cs),
                new InfoFragment(_cs)
          };

            CreateTabs();
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
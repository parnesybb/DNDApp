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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CharacterBaseScreen);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetHomeButtonEnabled(true);

            RecieveMessage();

            _fragments = new Fragment[]
          {
                new StatsSkillsFragment(_cs)
          };

            CreateTabs();
        }

        private void RecieveMessage()
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

        private void ClearView()
        {
            var viewer = FindViewById<FrameLayout>(Resource.Id.characterScreenDisplay);
            viewer.RemoveAllViews();
        }

        private void TabClick(object sender, ActionBar.TabEventArgs e)
        {
            var tab = sender as ActionBar.Tab;
            Log.Debug(TAG, "TabClick -> {0}", tab.Text);

           //ClearView();

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
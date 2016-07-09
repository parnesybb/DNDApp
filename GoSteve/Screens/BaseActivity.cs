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
using Android.Support.V7.App;

namespace GoSteve.Screens
{
    public interface BaseActivityInterface
    {
        void OnCreate(Bundle savedInstanceState);
        void OnCreateOptionsMenu(IMenu menu);
        void OnOptionsItemSelected(IMenuItem item);
    }

    public class BaseActivityImplementation : BaseActivityInterface
    {
        public Activity Activity;

        public BaseActivityImplementation(Activity activity)
        {
            Activity = activity;
        }


        public void OnCreate(Bundle savedInstanceState)
        {
            if (Activity != null)
            {
                var toolbar = Activity.FindViewById<Toolbar>(Resource.Id.baseToolbar);

                Activity.SetActionBar(toolbar);
            }
        }

        // Menu icons are inflated just as they were with actionbar
        public void OnCreateOptionsMenu(IMenu menu)
        {
            if (Activity != null)
            {
                // Inflate the menu; this adds items to the action bar if it is present.
                Activity.MenuInflater.Inflate(Resource.Menu.BaseMenu, menu);
            }
        }

        public void OnOptionsItemSelected(IMenuItem item)
        {
            if (Activity != null)
            {
                switch (item.ItemId)
                {
                    case Resource.Id.bmDMModeMenu:
                        var dmModeMenu = new Intent(Activity, typeof(DmScreenBase));
                        Activity.StartActivity(dmModeMenu);
                        break;
                    case Resource.Id.bmPlayerModeMenu:
                        var playerModeMenu = new Intent(Activity, typeof(CharacterSelectScreen));
                        Activity.StartActivity(playerModeMenu);
                        break;
                }
            }
        }
    }

    [Activity(Label = "BaseActivity")]
    public class BaseActivity : Activity
    {
        private BaseActivityImplementation _bai;

        public BaseActivity()
        {
            _bai = new BaseActivityImplementation(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _bai.OnCreate(savedInstanceState);
        }

        // Menu icons are inflated just as they were with actionbar
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            _bai.OnCreateOptionsMenu(menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            _bai.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
    }
}
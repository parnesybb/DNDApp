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
using Android.Support.V4.App;

namespace GoSteve.Screens
{
    [Activity(Label = "BaseActivityFragment")]
    public class BaseFragmentActivity : FragmentActivity
    {
        private BaseActivityImplementation _bai;

        public BaseFragmentActivity()
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
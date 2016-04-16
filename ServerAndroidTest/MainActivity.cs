using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ServerAndroidTest
{
    [Activity(Label = "ServerAndroidTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button discoveryBtn = FindViewById<Button>(Resource.Id.discoverBtn);
            discoveryBtn.Click += onClickDiscoveryBtn;
            Button connectBtn = FindViewById<Button>(Resource.Id.connectBtn);
            connectBtn.Click += onClickConnectBtn;
            Button registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            registerBtn.Click += onClickRegisterBtn;
        }

        public void onClickDiscoveryBtn(Object sender, EventArgs e)
        {

        }

        public void onClickConnectBtn(Object sender, EventArgs e)
        {

        }

        public void onClickRegisterBtn(Object sender, EventArgs e)
        {

        }
    }

}


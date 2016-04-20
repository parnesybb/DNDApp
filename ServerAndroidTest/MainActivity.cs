using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GoSteve.Network;
using Android.Util;
using Android.Net.Nsd;

namespace ServerAndroidTest
{
    [Activity(Label = "ServerAndroidTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView searchListView;

        AndroidNDSServer mNsdHelper;

        private TextView mStatusView;
        private Handler mUpdateHandler;

        public const String TAG = "NsdChat";

        ChatConnection mConnection;


        class MyHandler : Handler
        {
            public MainActivity Activity { get; set; }

            public MyHandler(MainActivity activity)
            {
                Activity = activity;
            }

            public void handleMessage(Message msg)
            {
                string chatLine = msg.Data.GetString(msg.ToString());
                Activity.addChatLine(chatLine);
            }
        }


    protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mConnection = new ChatConnection(mUpdateHandler);

            mNsdHelper = new AndroidNDSServer(this);
            mNsdHelper.initializeNsd();

            // Get our button from the layout resource,
            // and attach an event to it
            Button discoveryBtn = FindViewById<Button>(Resource.Id.discoverBtn);
            discoveryBtn.Click += onClickDiscoveryBtn;
            Button connectBtn = FindViewById<Button>(Resource.Id.connectBtn);
            connectBtn.Click += onClickConnectBtn;
            Button registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            registerBtn.Click += onClickRegisterBtn;

            searchListView = FindViewById<ListView>(Resource.Id.ServiceListView);
        }

        public void onClickDiscoveryBtn(Object sender, EventArgs e)
        {
            mNsdHelper.discoverServices();
        }

        public void onClickConnectBtn(Object sender, EventArgs e)
        {
            NsdServiceInfo service = mNsdHelper.getChosenServiceInfo();
            if (service != null)
            {
                Log.Debug(TAG, "Connecting.");
                mConnection.connectToServer(service.Host,
                        service.Port);
            }
            else
            {
                Log.Debug(TAG, "No service to connect to!");
            }
        }

        public void onClickRegisterBtn(Object sender, EventArgs e)
        {
            if (mConnection.getLocalPort() > -1)
            {
                mNsdHelper.registerService(mConnection.getLocalPort());
            }
            else
            {
                Log.Debug(TAG, "ServerSocket isn't bound.");
            }
        }

        public void addChatLine(String line)
        {
            mStatusView.Append("\n" + line);
        }

        protected void onPause()
        {
            if (mNsdHelper != null)
            {
                mNsdHelper.stopDiscovery();
            }
            base.OnPause();
        }

        override protected void OnResume()
        {
            base.OnResume();
            if (mNsdHelper != null)
            {
                mNsdHelper.discoverServices();
            }
        }

        protected void onDestroy()
        {
            mNsdHelper.tearDown();
            mConnection.tearDown();
            base.OnDestroy();
        }
    }

}


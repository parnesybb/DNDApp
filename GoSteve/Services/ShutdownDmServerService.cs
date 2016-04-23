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

namespace GoSteve.Services
{
    [Service]
    [IntentFilter(new String[] { ShutdownDmServerService.IntentFilter })]
    class ShutdownDmServerService : IntentService
    {
        public const string IntentFilter = "com.xamarin.ShutdownDmServerService";
        public const string StopServerServiceAction = "StopServerService";

        protected override void OnHandleIntent(Intent intent)
        {
            var stopServerIntent = new Intent(ShutdownDmServerService.StopServerServiceAction);

            SendOrderedBroadcast(stopServerIntent, null);

            StopSelf();
        }
    }
}
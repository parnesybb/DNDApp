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
using Android.Net.Nsd;
using Android.Util;

namespace Server
{
    public class GSResolveListener : Java.Lang.Object, NsdManager.IResolveListener
    {
        private readonly GSNsdHelper _nsdHelper;

        public GSResolveListener(GSNsdHelper nsd)
        {
            _nsdHelper = nsd;
        }

        public void OnResolveFailed(NsdServiceInfo serviceInfo, [GeneratedEnum] NsdFailure errorCode)
        {
            Console.WriteLine(GSNsdHelper.TAG, "Resolve Failed: " + errorCode);
        }

        public void OnServiceResolved(NsdServiceInfo serviceInfo)
        {
            Console.WriteLine(GSNsdHelper.TAG, "Resolve Success: " + serviceInfo);

            if (serviceInfo.ServiceName.Equals(_nsdHelper.ServiceName))
            {
                Console.WriteLine(GSNsdHelper.TAG, "Same IP");
            }

            _nsdHelper.NsdServiceInfo = serviceInfo;
        }
    }
}
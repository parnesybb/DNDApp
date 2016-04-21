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

namespace Server
{
    public class GSRegistrationListener : Java.Lang.Object, NsdManager.IRegistrationListener
    {
        private readonly GSNsdHelper _nsdHelper;

        public GSRegistrationListener(GSNsdHelper nsd)
        {
            _nsdHelper = nsd;
        }

        public void OnRegistrationFailed(NsdServiceInfo serviceInfo, [GeneratedEnum] NsdFailure errorCode)
        {
        }

        public void OnServiceRegistered(NsdServiceInfo serviceInfo)
        {
            _nsdHelper.ServiceName = serviceInfo.ServiceName;
        }

        public void OnServiceUnregistered(NsdServiceInfo serviceInfo)
        {
        }

        public void OnUnregistrationFailed(NsdServiceInfo serviceInfo, [GeneratedEnum] NsdFailure errorCode)
        {
        }
    }
}
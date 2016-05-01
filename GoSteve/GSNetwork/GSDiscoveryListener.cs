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
    public class GSDiscoveryListener : Java.Lang.Object, NsdManager.IDiscoveryListener
    {
        private readonly GSNsdHelper _nsdHelper;

        public GSDiscoveryListener(GSNsdHelper nsd)
        {
            this._nsdHelper = nsd;
        }

        public void OnDiscoveryStarted(string serviceType)
        {
            Console.WriteLine(GSNsdHelper.TAG + " Discovery Started");
        }

        public void OnDiscoveryStopped(string serviceType)
        {
            Console.WriteLine(GSNsdHelper.TAG + " Discovery Stopped");
        }

        public void OnServiceFound(NsdServiceInfo serviceInfo)
        {
            Console.WriteLine(GSNsdHelper.TAG + " Service Found");

            if (!serviceInfo.ServiceType.Equals(GSNsdHelper.SERVICE_TYPE))
            {
                Console.WriteLine(GSNsdHelper.TAG + "Unknown Service Type " + serviceInfo.ServiceType);
            }
            else if (serviceInfo.ServiceName.Contains(_nsdHelper.ServiceName))
            {
                try
                {
                    _nsdHelper.NsdManager.ResolveService(serviceInfo, _nsdHelper.NsdResolveListener);
                }
                catch (Exception ex)
                {
                    Log.Error(GSNsdHelper.TAG, ex.Message);
                } 
            }
        }

        public void OnServiceLost(NsdServiceInfo serviceInfo)
        {
            Console.WriteLine(GSNsdHelper.TAG + "Service Lost: " + serviceInfo);

            if (_nsdHelper.NsdServiceInfo == serviceInfo)
            {
                _nsdHelper.NsdServiceInfo = null;
            }
        }

        public void OnStartDiscoveryFailed(string serviceType, [GeneratedEnum] NsdFailure errorCode)
        {
            Console.WriteLine(GSNsdHelper.TAG, "Discovery Failed: " + serviceType + " " + errorCode);
            _nsdHelper.NsdManager.StopServiceDiscovery(this);
        }

        public void OnStopDiscoveryFailed(string serviceType, [GeneratedEnum] NsdFailure errorCode)
        {
            Console.WriteLine(GSNsdHelper.TAG, "Discovery Failed: " + serviceType + " " + errorCode);
            _nsdHelper.NsdManager.StopServiceDiscovery(this);
        }
    }
}
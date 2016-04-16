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

namespace GoSteve
{
    class MyDiscoveryListener : NsdManager.IDiscoveryListener
    {
        public NsdData NSDData
        {
            get; set;
        }

        public IntPtr Handle
        {
            get; set;
        }

        public MyDiscoveryListener(NsdData NsdData)
        {
            NSDData = NsdData;
        }

        public void OnDiscoveryStarted(String regType)
        {
            Log.Debug(NSDData.TAG, "Service discovery started");
        }

        public void OnServiceFound(NsdServiceInfo service)
        {
            Log.Debug(NSDData.TAG, "Service discovery success" + service);
            if (!NSDData.Service.ServiceType.Equals(NSDData.ServiceType))
            {
                Log.Debug(NSDData.TAG, "Unknown Service Type: " + service.ServiceType);
            }
            else if (NSDData.Service.ServiceType.Equals(NSDData.ServiceType))
            {
                Log.Debug(NSDData.TAG, "Same machine: " + NSDData.ServiceName);
            }
            else if (NSDData.Service.ServiceType.Contains(NSDData.ServiceType))
            {
                NSDData.NsdManager.ResolveService(service, NSDData.ResolveListener);
            }
        }

        public void OnServiceLost(NsdServiceInfo service)
        {
            Log.Error(NSDData.TAG, "service lost" + service);
            if (NSDData.Service == service)
            {
                NSDData.Service = null;
            }
        }

        public void OnDiscoveryStopped(String serviceType)
        {
            Log.Info(NSDData.TAG, "Discovery stopped: " + serviceType);
        }

        public void OnStartDiscoveryFailed(String serviceType, NsdFailure errorCode)
        {
            Log.Error(NSDData.TAG, "Discovery failed: Error code:" + errorCode);
            NSDData.NsdManager.StopServiceDiscovery(this);
        }

        public void OnStopDiscoveryFailed(String serviceType, NsdFailure errorCode)
        {
            Log.Error(NSDData.TAG, "Discovery failed: Error code:" + errorCode);
            NSDData.NsdManager.StopServiceDiscovery(this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
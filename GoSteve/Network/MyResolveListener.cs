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

namespace GoSteve.Network
{
    class MyResolveListener : NsdManager.IResolveListener
    {
        public NsdData NSDData
        {
            set; get;
        }

        public IntPtr Handle
        {
            get; set;
        }

        public MyResolveListener(NsdData NsdData)
        {
            NSDData = NsdData;
        }

        public void OnResolveFailed(NsdServiceInfo serviceInfo, NsdFailure errorCode)
        {
            Log.Error(NSDData.TAG, "Resolve failed" + errorCode);
        }

        public void OnServiceResolved(NsdServiceInfo serviceInfo)
        {
            Log.Error(NSDData.TAG, "Resolve Succeeded. " + serviceInfo);

            if (serviceInfo.ServiceName.Equals(NSDData.ServiceName))
            {
                Log.Debug(NSDData.TAG, "Same IP.");
                return;
            }
            NSDData.Service = serviceInfo;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
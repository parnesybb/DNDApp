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

namespace GoSteve.Network
{
    class MyRegistrationListener : NsdManager.IRegistrationListener
    {
        public NsdData NSDData
        {
            get; set;
        }

        public IntPtr Handle
        {
            get;
            set;
        }

        public MyRegistrationListener(NsdData NsdData)
        {
            NSDData = NsdData;
        }

        public void OnServiceRegistered(NsdServiceInfo NsdServiceInfo)
        {
            NSDData.ServiceName = NsdServiceInfo.ServiceName;
        }

        public void OnRegistrationFailed(NsdServiceInfo arg0, NsdFailure arg1)
        {
        }

        public void OnServiceUnregistered(NsdServiceInfo arg0)
        {
        }

        public void OnUnregistrationFailed(NsdServiceInfo serviceInfo, NsdFailure errorCode)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
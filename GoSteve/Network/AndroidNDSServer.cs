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
    class AndroidNDSServer
    {
        Context Context
        {
            get; set;
        }


        NsdManager.IDiscoveryListener mDiscoveryListener;
        NsdManager.IRegistrationListener mRegistrationListener;

        NsdData NSDData { get; set; }

        public AndroidNDSServer(Context context)
        {
            Context = context;
            NSDData.NsdManager = (NsdManager)context.GetSystemService(Context.NsdService);
        }

        public void initializeNsd()
        {
            initializeResolveListener();
            initializeDiscoveryListener();
            initializeRegistrationListener();

            //mNsdManager.init(mContext.getMainLooper(), this);

        }

        public void initializeDiscoveryListener()
        {
            mDiscoveryListener = new MyDiscoveryListener(NSDData);
        }

        public void initializeResolveListener()
        {
            NSDData.ResolveListener = new MyResolveListener(NSDData);
        }

        public void initializeRegistrationListener()
        {
            mRegistrationListener = new MyRegistrationListener(NSDData);
        }

        public void registerService(int port)
        {
            NsdServiceInfo serviceInfo = new NsdServiceInfo();
            serviceInfo.Port=port;
            serviceInfo.ServiceName=NSDData.ServiceName;
            serviceInfo.ServiceType = NSDData.ServiceType;

           NSDData.NsdManager.RegisterService(
                    serviceInfo, NsdManager.ProtocolDnsSd, mRegistrationListener);

        }

        public void discoverServices()
        {
            NSDData.NsdManager.DiscoverServices(
                    NSDData.ServiceType, NsdManager.ProtocolDnsSd, mDiscoveryListener);
        }

        public void stopDiscovery()
        {
            NSDData.NsdManager.StopServiceDiscovery(mDiscoveryListener);
        }

        public NsdServiceInfo getChosenServiceInfo()
        {
            return NSDData.Service;
        }

        public void tearDown()
        {
            NSDData.NsdManager.UnregisterService(mRegistrationListener);
        }
    }
}
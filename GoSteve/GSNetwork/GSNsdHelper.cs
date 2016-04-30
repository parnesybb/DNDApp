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
    public delegate void ServiceFoundDelegate(object sender, ServiceFoundEventArgs args);

    public class ServiceFoundEventArgs : EventArgs
    {
        public NsdServiceInfo UpdatedNsdServiceInfo { get; set; }
    }

    public class GSNsdHelper : Java.Lang.Object
    {
        private NsdManager _nsdManager;
        private NsdManager.IResolveListener _nsdResolveListener;
        private NsdManager.IDiscoveryListener _nsdDiscoveryListener;
        private NsdManager.IRegistrationListener _nsdRegistrationListener;
        private NsdServiceInfo _nsdServiceInfo;

        public event ServiceFoundDelegate ServiceFound;
        public static readonly string SERVICE_TYPE = "_dnd._tcp.";
        public static readonly string TAG = "GSNsdHelper";

        public GSNsdHelper(Context context)
        {
            this._nsdManager = (NsdManager)context.GetSystemService(Context.NsdService);
            this.ServiceName = Build.Manufacturer+'_'+ Build.Model;
        }

        public void StartHelper()
        {
            this.NsdDiscoveryListener = new GSDiscoveryListener(this);
            this.NsdResolveListener = new GSResolveListener(this);
            this.NsdRegistrationListener = new GSRegistrationListener(this);
        }

        public void RegisterService(int port)
        {
            var serviceInfo = new NsdServiceInfo();
            serviceInfo.Port = port;
            serviceInfo.ServiceName = this.ServiceName;
            serviceInfo.ServiceType = GSNsdHelper.SERVICE_TYPE;

            this.NsdManager.RegisterService(serviceInfo, NsdProtocol.DnsSd, this.NsdRegistrationListener);
        }

        public void DiscoverServices()
        {
            this.NsdManager.DiscoverServices(GSNsdHelper.SERVICE_TYPE, NsdProtocol.DnsSd, this.NsdDiscoveryListener);
        }

        public void StopDiscovery()
        {
            this.NsdManager.StopServiceDiscovery(this.NsdDiscoveryListener);
        }

        public void UnregisterService()
        {
            this.NsdManager.UnregisterService(this.NsdRegistrationListener);
        }

        public string ServiceName
        {
            get; set;
        }

        public NsdManager NsdManager
        {
            get
            {
                return _nsdManager;
            }

            set
            {
                _nsdManager = value;
            }
        }

        public NsdManager.IResolveListener NsdResolveListener
        {
            get
            {
                return _nsdResolveListener;
            }

            set
            {
                _nsdResolveListener = value;
            }
        }

        public NsdManager.IDiscoveryListener NsdDiscoveryListener
        {
            get
            {
                return _nsdDiscoveryListener;
            }

            set
            {
                _nsdDiscoveryListener = value;
            }
        }

        public NsdManager.IRegistrationListener NsdRegistrationListener
        {
            get
            {
                return _nsdRegistrationListener;
            }

            set
            {
                _nsdRegistrationListener = value;
            }
        }

        public NsdServiceInfo NsdServiceInfo
        {
            get
            {
                return _nsdServiceInfo;
            }

            set
            {
                if (_nsdServiceInfo != value)
                {
                    _nsdServiceInfo = value;

                    if (ServiceFound != null)
                    {
                        var args = new ServiceFoundEventArgs();
                        args.UpdatedNsdServiceInfo = value;
                        ServiceFound(this, args);
                    }
                }
            }
        }
    }
}
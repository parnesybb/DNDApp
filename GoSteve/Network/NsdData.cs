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

namespace GoSteve
{
    class NsdData
    {
        public NsdManager NsdManager { set; get; }

        public const string SERVICE_TYPE = "_http._tcp.";
        
        public string ServiceType
        {
            get
            {
                return SERVICE_TYPE;
            }
        }


        public const string tag = "AndroidNDSServer";

        public string TAG
        {
            get
            {
                return tag;
            }
        }


        private string serviceName = "NsdGoSteve";

        public string ServiceName
        {
            get
            {
                return serviceName;
            }

            set
            {
                serviceName = value;
            }
        }

        public NsdServiceInfo Service { get; set; }

        public NsdManager.IResolveListener ResolveListener
        {
            get; set;
        }
    }
}
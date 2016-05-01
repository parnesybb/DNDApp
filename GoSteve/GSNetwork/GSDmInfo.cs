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

namespace GoSteve.GSNetwork
{
    public class GSDmInfo
    {
        public GSDmInfo() { }
        public GSDmInfo(string hostName, int port)
        {
            HostName = hostName;
            Port = port;
        }

        private string _hostName;
        private int _port;

        public string HostName
        {
            get { return _hostName; }
            set
            {
                _hostName = value;
            }
        }

        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
            }
        }
    }
}
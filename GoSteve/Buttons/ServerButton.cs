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

namespace GoSteve.Buttons
{
    /// <summary>
    /// This is a button for the GSClient to store hostname and port.
    /// </summary>
    class ServerButton : Button
    {
        public ServerButton(Context context) : base (context){ }
        public ServerButton(Context context, string hostName, int port) : base (context)
        {
            HostName = hostName;
            Port = port;
            updateText();
        }
        private string _hostName;
        private int _port;

        public string HostName {
            get { return _hostName; }
            set {
                _hostName = value;
                updateText();
            }
        }

        public int Port {
            get { return _port; }
            set
            {
                _port = value;
                updateText();
            }
        }

        private void updateText()
        {
            Text = "Host: " + HostName + " Port: " + Port;
        }
    }
}
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
using Java.Net;
using Android.Util;
using Java.IO;

namespace GoSteve.Network
{
    public class ChatConnection
    {
        public Handler UpdateHandler;
        public ChatServer ChatServer { get; set; }
        public ChatClient ChatClient { set; get; }

        private static string tag = "ChatConnection";

        public string TAG
        {
            get {
                return tag;
            }

            set {
                tag = value;
            }
        }

        private Socket mSocket;
        private int mPort = -1;

        public ChatConnection(Handler handler)
        {
            UpdateHandler = handler;
            ChatServer = new ChatServer(handler, this);
        }

        public void tearDown()
        {
            ChatServer.tearDown();
            //mChatClient.tearDown();
        }

        public void connectToServer(InetAddress address, int port)
        {
            //mChatClient = new ChatClient(address, port);
        }

        public void sendMessage(String msg)
        {
            if (ChatClient != null)
            {
                ChatClient.sendMessage(msg);
            }
        }

        public int getLocalPort()
        {
            return mPort;
        }

        public void setLocalPort(int port)
        {
            mPort = port;
        }


        public void updateMessages(String msg, bool local)
        {
            Log.Error(TAG, "Updating message: " + msg);

            if (local)
            {
                msg = "me: " + msg;
            }
            else
            {
                msg = "them: " + msg;
            }

            Bundle messageBundle = new Bundle();
            messageBundle.PutString("msg", msg);

            Message message = new Message();
            message.Data=messageBundle;
            UpdateHandler.SendMessage(message);

        }

        public void setSocket(Socket socket)
        {
            Log.Debug(TAG, "setSocket being called.");
            if (socket == null)
            {
                Log.Debug(TAG, "Setting a null socket.");
            }
            if (mSocket != null)
            {
                if (mSocket.IsConnected)
                {
                    try
                    {
                        mSocket.Close();
                    }
                    catch (IOException e)
                    {
                        // TODO(alexlucas): Auto-generated catch block
                        e.PrintStackTrace();
                    }
                }
            }
            mSocket = socket;
        }

        public Socket getSocket()
        {
            return mSocket;
        }
    }
}
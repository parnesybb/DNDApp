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
using Java.Lang;
using System.IO;
using Android.Util;

namespace GoSteve.Network
{
    public class ChatServer
    {
        ServerSocket ServerSocket = null;
        Thread mThread = null;

        public const string TAG = "ChatServer";

        public ChatConnection ChatConnection
        {
            get; set;
        }

        public ChatServer(Handler handler, ChatConnection chatConnect)
        {
            ChatConnection = chatConnect;
            mThread = new Thread(new ServerThread(chatConnect));
            mThread.Start();
        }

        public void tearDown()
        {
            mThread.Interrupt();
            try
            {
                ServerSocket.Close();
            }
            catch (IOException ioe)
            {
                Log.Error(TAG, "Error when closing server socket.");
            }
        }

        class ServerThread : IRunnable
        {
            public const string TAG = "ChatServer";
            ServerSocket ServerSocket;

            public ChatConnection ChatConnection
            {
                get; set;
            }

            public ServerThread(ChatConnection chatConnection)
            {
                ChatConnection = chatConnection;
            }

            public IntPtr Handle
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            void IRunnable.Run()
            {

                try
                {
                    // Since discovery will happen via Nsd, we don't need to care which port is
                    // used.  Just grab an available one  and advertise it via Nsd.
                    ServerSocket = new ServerSocket(0);
                    ChatConnection.setLocalPort(ServerSocket.LocalPort);

                    while (!Thread.CurrentThread().IsInterrupted)
                    {
                        Log.Debug(TAG, "ServerSocket Created, awaiting connection");
                        ChatConnection.setSocket(ServerSocket.Accept());
                        Log.Debug(TAG, "Connected.");
                        if (ChatConnection.ChatClient == null)
                        {
                            int port = ChatConnection.getSocket().Port;
                            InetAddress address = ChatConnection.getSocket().InetAddress;
                            ChatConnection.connectToServer(address, port);
                        }
                    }
                }
                catch (IOException e)
                {
                    Log.Error(TAG, "Error creating ServerSocket: ", e);
                    //e.PrintStackTrace();
                }
            }
        }
    }
}
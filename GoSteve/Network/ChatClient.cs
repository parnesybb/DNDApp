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
using Android.Util;
using Java.Util.Concurrent;

namespace GoSteve.Network
{
    public class ChatClient
    {
        class ChatClientData
        {
            public InetAddress Address { get; set; }
            public int PORT { get; set; }

            public const string client_tag = "ChatClient";
            public string CLIENT_TAG { get { return client_tag; } }
            public ChatConnection ChatConnection { get; set; }
        }

        ChatClientData ChatData { get; set; }

        private Thread mSendThread;
        private Thread mRecThread;

        public ChatConnection ChatConnection { get; set; }

        public ChatClient(ChatConnection chatConnection, InetAddress address, int port)
        {
            ChatConnection = chatConnection;
            Log.Debug(ChatData.CLIENT_TAG, "Creating chatClient");
            ChatData.Address = address;
            ChatData.PORT = port;

            mSendThread = new Thread(new SendingThread(ChatConnection));
            mSendThread.Start();
        }

        class SendingThread : IRunnable
        {
            public ChatConnection ChatConnection;
            IBlockingQueue mMessageQueue;
            private int QUEUE_CAPACITY = 10;

            public SendingThread(ChatConnection chatConnection)
            {
                ChatConnection = chatConnection;
                mMessageQueue = new ArrayBlockingQueue(QUEUE_CAPACITY);
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
                    if (ChatConnection.getSocket() == null)
                    {
                        ChatConnection.setSocket(new Socket(mAddress, PORT));
                        Log.Debug(CLIENT_TAG, "Client-side socket initialized.");

                    }
                    else
                    {
                        Log.d(CLIENT_TAG, "Socket already initialized. skipping!");
                    }

                    mRecThread = new Thread(new ReceivingThread());
                    mRecThread.start();

                }
                catch (UnknownHostException e)
                {
                    Log.d(CLIENT_TAG, "Initializing socket failed, UHE", e);
                }
                catch (IOException e)
                {
                    Log.d(CLIENT_TAG, "Initializing socket failed, IOE.", e);
                }

            while (true)
            {
                try
                {
                    String msg = mMessageQueue.take();
                    sendMessage(msg);
                }
                catch (InterruptedException ie)
                {
                    Log.d(CLIENT_TAG, "Message sending loop interrupted, exiting");
                }
            }
        }
    }

    class ReceivingThread implements Runnable
    {

        @Override
            public void run()
    {

        BufferedReader input;
        try
        {
            input = new BufferedReader(new InputStreamReader(
                    mSocket.getInputStream()));
            while (!Thread.currentThread().isInterrupted())
            {

                String messageStr = null;
                messageStr = input.readLine();
                if (messageStr != null)
                {
                    Log.d(CLIENT_TAG, "Read from the stream: " + messageStr);
                    updateMessages(messageStr, false);
                }
                else
                {
                    Log.d(CLIENT_TAG, "The nulls! The nulls!");
                    break;
                }
            }
            input.close();

        }
        catch (IOException e)
        {
            Log.e(CLIENT_TAG, "Server loop error: ", e);
        }
    }
}
}
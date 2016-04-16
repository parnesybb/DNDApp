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
using Java.Lang;
using Java.IO;
using Android.Util;
using Java.Net;

namespace ServerAndroidTest
{
    class ChatConnection
    {
        private Handler mUpdateHandler;
        private ChatServer mChatServer;
        private ChatClient mChatClient;

        private const string TAG = "ChatConnection";

        private Socket mSocket;
        private int mPort = -1;

        public ChatConnection(Handler handler)
        {
            mUpdateHandler = handler;
            mChatServer = new ChatServer(handler);
        }

        public void tearDown()
        {
            mChatServer.tearDown();
            mChatClient.tearDown();
        }

        public void connectToServer(InetAddress address, int port)
        {
            mChatClient = new ChatClient(address, port);
        }

        public void sendMessage(String msg)
        {
            if (mChatClient != null)
            {
                mChatClient.sendMessage(msg);
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


        public void updateMessages(string msg, bool local)
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
            mUpdateHandler.SendMessage(message);

        }

        private void setSocket(Socket socket)
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

        private Socket getSocket()
        {
            return mSocket;
        }


        private class ChatServer
        {
            public ChatConnection ChatConnection
            {
                get; set;
            }

            ServerSocket mServerSocket = null;
            Thread mThread = null;

            public ChatServer(Handler handler)
            {
                mThread = new Thread(new ServerThread());
                mThread.Start();
            }

            public void tearDown()
            {
                mThread.Interrupt();
                try
                {
                    mServerSocket.Close();
                }
                catch (IOException ioe)
                {
                    Log.Error(TAG, "Error when closing server socket.");
                }
            }

            class ServerThread : Java.Lang.IRunnable
            {
                public IntPtr Handle
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                public ChatServer ChatServer
                {
                    get; set;
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
                        ChatServer.mServerSocket = new ServerSocket(0);
                        ChatServer.ChatConnection.setLocalPort(ChatServer.mServerSocket.LocalPort);

                        while (!Thread.CurrentThread().IsInterrupted)
                        {
                            Log.Debug(TAG, "ServerSocket Created, awaiting connection");
                            ChatServer.ChatConnection.setSocket(ChatServer.mServerSocket.Accept());
                            Log.Debug(TAG, "Connected.");
                            if (ChatServer.ChatConnection.mChatClient == null)
                            {
                                int port = ChatServer.ChatConnection.mSocket.Port;
                                InetAddress address = ChatServer.ChatConnection.mSocket.InetAddress;
                                ChatServer.ChatConnection.connectToServer(address, port);
                            }
                        }
                    }
                    catch (IOException e)
                    {
                        Log.Error(TAG, "Error creating ServerSocket: ", e);
                        e.PrintStackTrace();
                    }
                }
            }
        }

        private class ChatClient
        {

            private InetAddress mAddress;
            private int PORT;

            private const string CLIENT_TAG = "ChatClient";

            private Thread mSendThread;
            private Thread mRecThread;

            public ChatConnection ChatConnect {
                get;
                set;
            }

            public ChatClient(InetAddress address, int port)
            {

                Log.Debug(CLIENT_TAG, "Creating chatClient");
                this.mAddress = address;
                this.PORT = port;

                mSendThread = new Thread(new SendingThread());
                mSendThread.Start();
            }

            class SendingThread : IRunnable
            {

                Java.Util.Concurrent.IBlockingQueue mMessageQueue;
                private int QUEUE_CAPACITY = 10;

                public SendingThread()
                {
                    mMessageQueue = new Java.Util.Concurrent.ArrayBlockingQueue(QUEUE_CAPACITY);
                }

                void IRunnable.Run()
                {
                    try
                    {
                        if (ChatConnect.getSocket() == null)
                        {
                            setSocket(new Socket(mAddress, PORT));
                            Log.d(CLIENT_TAG, "Client-side socket initialized.");

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

    public void tearDown()
    {
        try
        {
            getSocket().close();
        }
        catch (IOException ioe)
        {
            Log.e(CLIENT_TAG, "Error when closing server socket.");
        }
    }

    public void sendMessage(String msg)
    {
        try
        {
            Socket socket = getSocket();
            if (socket == null)
            {
                Log.d(CLIENT_TAG, "Socket is null, wtf?");
            }
            else if (socket.getOutputStream() == null)
            {
                Log.d(CLIENT_TAG, "Socket output stream is null, wtf?");
            }

            PrintWriter out = new PrintWriter(
                    new BufferedWriter(
                            new OutputStreamWriter(getSocket().getOutputStream())), true);
                out.println(msg);
                out.flush();
            updateMessages(msg, true);
        }
        catch (UnknownHostException e)
        {
            Log.d(CLIENT_TAG, "Unknown Host", e);
        }
        catch (IOException e)
        {
            Log.d(CLIENT_TAG, "I/O Exception", e);
        }
        catch (Exception e)
        {
            Log.d(CLIENT_TAG, "Error3", e);
        }
        Log.d(CLIENT_TAG, "Client sent message: " + msg);
    }
}
}
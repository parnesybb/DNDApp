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
using Java.IO;

namespace GoSteve.Network
{
    public class ChatClient
    {
        class ChatClientData
        {
            public InetAddress Address { get; set; }
            public int PORT { get; set; }
            public Socket Socket { get; set; }

            public const string client_tag = "ChatClient";
            public string CLIENT_TAG { get { return client_tag; } }
            public ChatConnection ChatConnection { get; set; }
            public ChatClient ChatClient { get; set; }

            public ReceivingThread RecRunnable { get; set; }

            public Thread SendThread { get; set; }
            public Thread RecThread { get; set; }
        }

        ChatClientData ChatData { get; set; }

        public ChatClient(ChatConnection chatConnection, InetAddress address, int port)
        {
            ChatData.ChatConnection = chatConnection;
            Log.Debug(ChatData.CLIENT_TAG, "Creating chatClient");
            ChatData.Address = address;
            ChatData.PORT = port;

            ChatData.SendThread = new Thread(new SendingThread(ChatData));
            ChatData.SendThread.Start();
        }

        class SendingThread : IRunnable
        {
            public ChatClientData ChatData { set; get; }
            IBlockingQueue mMessageQueue;
            private int QUEUE_CAPACITY = 10;

            public SendingThread(ChatClientData chatData)
            {
                ChatData = chatData;
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
                    if (ChatData.ChatConnection.getSocket() == null)
                    {

                        ChatData.ChatConnection.setSocket(new Socket(ChatData.Address, ChatData.PORT));
                        Log.Debug(ChatData.CLIENT_TAG, "Client-side socket initialized.");

                    }
                    else
                    {
                        Log.Debug(ChatData.CLIENT_TAG, "Socket already initialized. skipping!");
                    }

                    ChatData.RecRunnable = new ReceivingThread(ChatData);

                    ChatData.RecThread = new Thread(ChatData.RecRunnable);
                    ChatData.RecThread.Start();

                }
                catch (UnknownHostException e)
                {
                    Log.Debug(ChatData.CLIENT_TAG, "Initializing socket failed, UHE", e);
                }
                catch (IOException e)
                {
                    Log.Debug(ChatData.CLIENT_TAG, "Initializing socket failed, IOE.", e);
                }

                while (true)
                {
                    try
                    {
                        string msg = (string)mMessageQueue.Take();
                        ChatData.ChatClient.sendMessage(msg);
                    }
                    catch (InterruptedException ie)
                    {
                        Log.Debug(ChatData.CLIENT_TAG, "Message sending loop interrupted, exiting");
                    }
                }
            }
        }

        class ReceivingThread : IRunnable
        {
            public ChatClientData ChatData { get; set; }

            public IntPtr Handle
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public ReceivingThread(ChatClientData chatData)
            {
                ChatData = chatData;
            }

            void IRunnable.Run()
            {

                BufferedReader input;
                try
                {
                    input = new BufferedReader(new InputStreamReader(
                           ChatData.Socket.InputStream));
                    while (!Thread.CurrentThread().IsInterrupted)
                    {

                        string messageStr = null;
                        messageStr = input.ReadLine();
                        if (messageStr != null)
                        {
                            Log.Debug(ChatData.CLIENT_TAG, "Read from the stream: " + messageStr);
                            ChatData.ChatConnection.updateMessages(messageStr, false);
                        }
                        else
                        {
                            Log.Debug(ChatData.CLIENT_TAG, "The nulls! The nulls!");
                            break;
                        }
                    }
                    input.Close();

                }
                catch (IOException e)
                {
                    Log.Error(ChatData.CLIENT_TAG, "Server loop error: ", e);
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public void tearDown()
        {
            try
            {
                ChatData.ChatConnection.getSocket().Close();
            }
            catch (IOException ioe)
            {
                Log.Error(ChatData.CLIENT_TAG, "Error when closing server socket.");
            }
        }

        public void sendMessage(string msg)
        {
            try
            {
                Java.Net.Socket socket = ChatData.ChatConnection.getSocket();

                if (socket == null)
                {
                    Log.Debug(ChatData.CLIENT_TAG, "Socket is null, wtf?");
                }
                else if (socket.OutputStream == null)
                {
                    Log.Debug(ChatData.CLIENT_TAG, "Socket output stream is null, wtf?");
                }

                /*
                PrintWriter out = new PrintWriter(
                        new BufferedWriter(
                                new OutputStreamWriter(ChatData.ChatConnection.getSocket().OutputStream)), true);
                out.println(msg);
                out.flush();
                */


                System.IO.Stream stream = ChatData.ChatConnection.getSocket().OutputStream;
                byte[] byteAry = Encoding.ASCII.GetBytes(msg);
                stream.Write(byteAry, 0, byteAry.Length);

                ChatData.ChatConnection.updateMessages(msg, true);
            }
            catch (UnknownHostException e)
            {
                Log.Debug(ChatData.CLIENT_TAG, "Unknown Host", e);
            }
            catch (IOException e)
            {
                Log.Debug(ChatData.CLIENT_TAG, "I/O Exception", e);
            }
            catch (Java.Lang.Exception e)
            {
                Log.Debug(ChatData.CLIENT_TAG, "Error3", e);
            }
            Log.Debug(ChatData.CLIENT_TAG, "Client sent message: " + msg);
        }
    }
}
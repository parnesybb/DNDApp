using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Tests
{
    public class ServerTest
    {
        // used to pass state information to delegate
        internal class StateObject
        {
            internal byte[] sBuffer;
            internal Socket sSocket;
            internal StateObject(int size, Socket sock)
            {
                sBuffer = new byte[size];
                sSocket = sock;
            }
        }
        static void Main()
        {
            IPAddress ipAddress =
              Dns.Resolve(Dns.GetHostName()).AddressList[0];

            IPEndPoint ipEndpoint =
              new IPEndPoint(ipAddress, 1800);

            Socket listenSocket =
              new Socket(AddressFamily.InterNetwork,
                         SocketType.Stream,
                         ProtocolType.Tcp);

            listenSocket.Bind(ipEndpoint);
            listenSocket.Listen(1);
            IAsyncResult asyncAccept = listenSocket.BeginAccept(
              new AsyncCallback(ServerTest.acceptCallback),
              listenSocket);

            // could call listenSocket.EndAccept(asyncAccept) here
            // instead of in the callback method, but since 
            // EndAccept blocks, the behavior would be similar to 
            // calling the synchronous Accept method

            Console.Write("Connection in progress.");
            if (writeDot(asyncAccept) == true)
            {
                // allow time for callbacks to
                // finish before the program ends 
                Thread.Sleep(3000);
            }
        }

        public static void
          acceptCallback(IAsyncResult asyncAccept)
        {
            Socket listenSocket = (Socket)asyncAccept.AsyncState;
            Socket serverSocket =
              listenSocket.EndAccept(asyncAccept);

            // arriving here means the operation completed
            // (asyncAccept.IsCompleted = true) but not
            // necessarily successfully
            if (serverSocket.Connected == false)
            {
                Console.WriteLine(".server is not connected.");
                return;
            }
            else Console.WriteLine(".server is connected.");

            listenSocket.Close();

            StateObject stateObject =
              new StateObject(16, serverSocket);

            // this call passes the StateObject because it 
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              serverSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(receiveCallback),
                stateObject);

            Console.Write("Receiving data.");
            writeDot(asyncReceive);
        }

        public static void
          receiveCallback(IAsyncResult asyncReceive)
        {
            StateObject stateObject =
              (StateObject)asyncReceive.AsyncState;
            int bytesReceived =
              stateObject.sSocket.EndReceive(asyncReceive);

            Console.WriteLine(
              ".{0} bytes received: {1}",
              bytesReceived.ToString(),
              Encoding.ASCII.GetString(stateObject.sBuffer));

            byte[] sendBuffer =
              Encoding.ASCII.GetBytes("Goodbye");
            IAsyncResult asyncSend =
              stateObject.sSocket.BeginSend(
                sendBuffer,
                0,
                sendBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(sendCallback),
                stateObject.sSocket);

            Console.Write("Sending response.");
            writeDot(asyncSend);
        }

        public static void sendCallback(IAsyncResult asyncSend)
        {
            Socket serverSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = serverSocket.EndSend(asyncSend);
            Console.WriteLine(
              ".{0} bytes sent.{1}{1}Shutting down.",
              bytesSent.ToString(),
              Environment.NewLine);

            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }

        // times out after 20 seconds but operation continues
        internal static bool writeDot(IAsyncResult ar)
        {
            int i = 0;
            while (ar.IsCompleted == false)
            {
                if (i++ > 40)
                {
                    Console.WriteLine("Timed out.");
                    return false;
                }
                Console.Write(".");
                Thread.Sleep(500);
            }
            return true;
        }
    }
}

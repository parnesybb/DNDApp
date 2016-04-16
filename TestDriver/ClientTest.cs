using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TestDriver
{
    public class ClientTest
    {

        // used to pass state information to delegate
        class StateObject
        {
            internal byte[] sBuffer;
            internal Socket sSocket;
            internal StateObject(int size, Socket sock)
            {
                sBuffer = new byte[size];
                sSocket = sock;
            }
        }

        public static void run()
        {
            run("localhost", 8000);
        }

        public static void run(string hostName, int port)
        {
            IPAddress ipAddress =
              Dns.GetHostEntry(hostName).AddressList[0];

            Console.WriteLine("Client connecting to server IP: "+ ipAddress.ToString());

            IPEndPoint ipEndpoint =
              new IPEndPoint(ipAddress, port);

            Socket clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(
              ipEndpoint,
              new AsyncCallback(connectCallback),
              clientSocket);

            Console.Write("Connection in progress.");
            if (writeDot(asyncConnect) == true)
            {
                // allow time for callbacks to
                // finish before the program ends
                Thread.Sleep(3000);
            }
        }

        public static void
          connectCallback(IAsyncResult asyncConnect)
        {
            Socket clientSocket =
              (Socket)asyncConnect.AsyncState;
            clientSocket.EndConnect(asyncConnect);

            // arriving here means the operation completed
            // (asyncConnect.IsCompleted = true) but not
            // necessarily successfully
            if (clientSocket.Connected == false)
            {
                Console.WriteLine(".client is not connected.");
                return;
            }
            else Console.WriteLine(".client is connected.");

            byte[] sendBuffer = Encoding.ASCII.GetBytes("Hello");
            IAsyncResult asyncSend = clientSocket.BeginSend(
              sendBuffer,
              0,
              sendBuffer.Length,
              SocketFlags.None,
              new AsyncCallback(sendCallback),
              clientSocket);

            Console.Write("Sending data.");
            writeDot(asyncSend);
        }

        public static void sendCallback(IAsyncResult asyncSend)
        {
            Socket clientSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = clientSocket.EndSend(asyncSend);
            Console.WriteLine(
              ".{0} bytes sent.",
              bytesSent.ToString());

            StateObject stateObject =
              new StateObject(16, clientSocket);

            // this call passes the StateObject because it
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              clientSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(receiveCallback),
                stateObject);

            Console.Write("Receiving response.");
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
              ".{0} bytes received: {1}{2}{2}Shutting down.",
              bytesReceived.ToString(),
              Encoding.ASCII.GetString(stateObject.sBuffer),
              Environment.NewLine);

            stateObject.sSocket.Shutdown(SocketShutdown.Both);
            stateObject.sSocket.Close();
        }

        // times out after 2 seconds but operation continues
        internal static bool writeDot(IAsyncResult ar)
        {
            int i = 0;
            while (ar.IsCompleted == false)
            {
                if (i++ > 20)
                {
                    Console.WriteLine("Timed out.");
                    return false;
                }
                Console.Write(".");
                Thread.Sleep(100);
            }
            return true;
        }
    }
}

using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using GoSteve;
using Zeroconf;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDriver.Network
{
    [TestClass]
    public class ClientTest
    {
        private IObservable<IZeroconfHost> list;
        public const string DNDSERVICETYPE = "_dnd._tcp.local.";
        private Random rnd = new Random();
        private IPAddressPort service;

        public class IPAddressPort
        {
            public string Address { get; set; }
            public int Port { get; set; }
        }

        public ClientTest()
        {
        }

        public void EnumerateAllServicesFromAllHosts()
        {
            list = ZeroconfResolver.Resolve(DNDSERVICETYPE);

            IObservable<IZeroconfHost> listener = ZeroconfResolver.Resolve(DNDSERVICETYPE);

            Action<IZeroconfHost> myaction = new Action<IZeroconfHost>( (IZeroconfHost host) => {
                Console.WriteLine("Found Host "+host);
                if (service == null)
                {
                    service = new IPAddressPort();
                    service.Address = host.IPAddress;
                    service.Port = host.Services.First().Value.Port;
                }
            });
            listener.Subscribe(myaction) ;
        }

        [TestMethod]
        public void SendCharacterSheetToServer()
        {
            EnumerateAllServicesFromAllHosts();
            while (service == null)
            {
                Thread.Sleep(500);
            }

            CharacterSheet cs;
            cs = CreateFakeRequest();

            connectToServer(cs, service.Address, service.Port);
        }

        public void connectToServer(CharacterSheet cs, string ipAddress, int port)
        {
            Console.WriteLine("Connecting to: {0}:{1}", ipAddress, port);
            IPEndPoint ipEndpoint =
              new IPEndPoint(IPAddress.Parse(ipAddress), port);

            Socket clientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            Console.Write("Connection in progress.");

            clientSocket.Connect(ipAddress, port);

            if (clientSocket.Connected)
            {
                Console.Write("Connected.");

                BinaryFormatter bf = new BinaryFormatter();
                byte[] csBytes = null;
                var ms = new System.IO.MemoryStream();
                bf.Serialize(ms, cs);
                csBytes = ms.ToArray();
                ms.Close();
                int bytesRead = 0;

                var dataLength = BitConverter.GetBytes(csBytes.Length);
                Console.WriteLine("Setup to send a charactersheet that is " + csBytes.Length + " Bytes");
                bytesRead = clientSocket.Send(dataLength, 4, SocketFlags.None);
                Console.WriteLine("Sent {0} Bytes to the Server.", bytesRead);
                bytesRead = clientSocket.Send(csBytes);
                Console.WriteLine("Sent {0} Bytes to the Server.", bytesRead);

                // get response...should be character id.
                byte[] resp = new byte[256];
                var respText = string.Empty;
                clientSocket.Receive(resp, resp.Length - 1, SocketFlags.None);
                respText = ASCIIEncoding.ASCII.GetString(resp).TrimEnd('\0');
                if (!cs.ID.Equals(respText))
                {
                    Console.WriteLine("Failed to send data. ID mismatch");
                }
                Console.WriteLine("You sent character ID: '{0}' you recieved ID: '{1}'", cs.ID, respText);
                clientSocket.Close();
            }
            else
            {
                Console.WriteLine("Could not connect to Server!!!");
            }
            Thread.Sleep(1000);
        }

        private CharacterSheet CreateFakeRequest()
        {
            var cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            cs.SetRace(KnownValues.Race.DWARF, true);
            cs.Background = KnownValues.Background.SOLDIER;
            cs.ID = "" + rnd.Next();
            cs.CharacterName = "Flaf";

            return cs;
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestDriver
{
    class ServerClientTest
    {
        public static void run()
        {
            /*
            Thread serverThread = new Thread(ServerTest.run);
            Thread clientThread = new Thread(ClientTest.run);


            serverThread.Start();
            Thread.Sleep(500);
            clientThread.Start();

            serverThread.Join();
            clientThread.Join();
            */

            Process current = Process.GetCurrentProcess();
            Console.WriteLine("ProcessName: "+current.ProcessName);
            Console.WriteLine("FileName: " + current.MainModule.FileName);
            Process clientCmd = new Process();
            Process serverCmd = new Process();

            try
            {
                serverCmd.StartInfo.UseShellExecute = true;
                // You can start any process, HelloWorld is a do-nothing example.
                serverCmd.StartInfo.FileName = "cmd.exe";
                serverCmd.StartInfo.CreateNoWindow = false;
                serverCmd.StartInfo.Arguments = "/C \""+current.MainModule.FileName+" ServerTest & pause\"";
                serverCmd.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                clientCmd.StartInfo.UseShellExecute = true;
                // You can start any process, HelloWorld is a do-nothing example.
                clientCmd.StartInfo.FileName = "cmd.exe";
                clientCmd.StartInfo.CreateNoWindow = false;
                clientCmd.StartInfo.Arguments = "/C \"" + current.MainModule.FileName + " ClientTest & pause\"";
                clientCmd.Start();
                // This code assumes the process you are starting will terminate itself. 
                // Given that is is started without a window so you cannot terminate it 
                // on the desktop, it must terminate itself or you can do it programmatically
                // from this application using the Kill method.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /*
            Task.Factory.StartNew(delegate
            {
                ServerTest.run(1800);
            });
            Task.Factory.StartNew(delegate
            {
                ClientTest.run("localhost", 1800);       
            });
            */
        }
    }
}

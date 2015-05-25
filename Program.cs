using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace LKCamelotV2
{
    class Program
    {
        static readonly int gameport = 1811;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(App_ThreadException);
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            //var test1 = Encoding.GetEncoding(949).GetString(new byte[] { 0xB3, 0xAA, 0xB9, 0xAB, 0x30, 0x35 });

            Log.LogLine("Starting time ticker");
            World.World.tickcount = new System.Diagnostics.Stopwatch();
            World.World.tickcount.Start();

            Log.LogLine("Loading Scripts");
            Scripts.Script.Load();

            Log.LogLine("Starting tick thread machine");
            Engine.StartEngine();

            Log.LogLine(string.Format("Starting Game Server on port: {0}", gameport));
            IPEndPoint epuser = new IPEndPoint(IPAddress.Any, gameport);
            var userserver = new Thixi.Server(2000, 4096, 20);
            userserver.OnNewConnection = OnNewUserConnection;
            userserver.Start(epuser, SocketOptionName.NoDelay);

            string inp;
            while ((inp = Console.ReadLine()) != "x") ;

            World.World.WorldSave();
            System.Environment.Exit(1);
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            //    Console.WriteLine("Exiting");
            //    Thread.Sleep(10000);
        }

        static Thixi.Connection OnNewUserConnection(Thixi.Server serv, Socket socket)
        {
            var nconn = new Network.GameConnection(serv, socket);
            // ConnectedUsers[nconn.Id] = nconn;
            return nconn;
        }

        static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static void App_ThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.LogException((e.ExceptionObject as Exception));
            System.Console.ReadLine(); //Keep from closing
        }
    }
}

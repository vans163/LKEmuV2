using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace LKCamelotV2
{
    static class Log
    {
      //  static Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      //  static IPEndPoint sending_end_point = new IPEndPoint(IPAddress.Parse(Prog.logserver), Prog.logport);

        private static System.Threading.Mutex lockexception = new System.Threading.Mutex();
        private static System.Threading.Mutex locklog = new System.Threading.Mutex();
        public static void LogException(Exception e, string message = "no message")
        {
            string text = "Exception: " + message + " : " + e.Message + System.Environment.NewLine
                    + e.StackTrace + System.Environment.NewLine;
            lock (lockexception)
            {
                System.IO.File.AppendAllText("Exceptions.log", text);

                //Send to remote logger
                var sende = text;
            //    sending_socket.SendTo(System.Text.Encoding.ASCII.GetBytes(sende), sending_end_point);
            }
            Console.WriteLine(text);
        }

        public static void LogLine(string line, ConsoleColor colr = ConsoleColor.Gray)
        {
            string text = TimeStamp + " " + line;
            lock (locklog)
            {
                System.IO.File.AppendAllText("Logs.log", text);
            }
            Console.ForegroundColor = colr;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static string TimeStamp
        {
            get
            {
                var stamp = DateTime.Now.ToShortDateString();
                var stamp2 = DateTime.Now.ToShortTimeString();
                return stamp + " " + stamp2;
            }
        }
    }
}

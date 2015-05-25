using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace LKCamelotV2
{
    public static class Engine
    {
        static Thread Tickr;
        static long lastWorldSave = 0;

        public static void StartEngine()
        {
            Tickr = new Thread(Ticker);
            Tickr.Start();
        }

        public static void Ticker()
        {
            while (true)
            {
                Console.Title = string.Format("Players Online: {0}", World.World.PlayersOnline());
				var ctick = World.World.tickcount.ElapsedMilliseconds;
                if (lastWorldSave + 600000 < ctick)
                {
                    lastWorldSave = ctick;
                    try
                    {
                        World.World.WorldSave();
                    }
                    catch (Exception e)
                    {
                        Log.LogException(e);
                    }
                }

                System.Threading.Tasks.Parallel.ForEach(World.World.Maps, x => x.Value.Tick(ctick));
                Thread.Sleep(100);
            }
        }

        static string _MD5;
        public static string MD5
        {
            get
            {
                if (_MD5 == null)
                {
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        using (var stream = File.OpenRead(System.Reflection.Assembly.GetExecutingAssembly().Location))
                        {
                            _MD5 = Util.ToHexString(md5.ComputeHash(stream));
                        }
                    }
                }
                return _MD5;
            }
        }
    }
}

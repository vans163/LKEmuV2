using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ProtoBuf;

namespace LKCamelotV2.World
{
    public static class World
    {
        static ConcurrentDictionary<string, Player.Player> Players = new ConcurrentDictionary<string, Player.Player>();
        public static Dictionary<string, Map> Maps = new Dictionary<string, Map>();
        public static Stopwatch tickcount;

        internal delegate void OnChat(Player.Player playr, string msg, int type);
        internal static event OnChat _ChatMsg;

        public delegate void ProcessCommand(Player.Player playr, string command, string[] suffix);
        public static ProcessCommand _ProcCommand;

      //  public static Board.MessageBoard SysOpBoard = DB.Loader.LoadBoard("sysop", false);
        public static Board.MessageBoard SysOpBoard;
        public static Board.MessageBoard NoticeBoard = DB.Loader.LoadBoard("notice");


        public static void ChangeMap(Player.Player playr, Map entermap, int enterx, int entery)
        {
            playr.Position.CurMap.Exit(playr);
            playr.Position.X = enterx;
            playr.Position.Y = entery;
            entermap.Enter(playr);
        }

        public static Player.Player GetPlayer(string user)
        {
            Player.Player outv;
            Players.TryGetValue(user, out outv);
            return outv;
        }

        public static int PlayersOnline()
        {
            return Players.Count;
        }

        public static Map GetMap(string mapn)
        {
            Map outv;
            Maps.TryGetValue(mapn, out outv);
            return outv;
        }

        public static void OnChatMsg(Player.Player playr, string msg, int type = -1)
        {
            _ChatMsg(playr, msg, type);
        }

        public static void Enter(Player.Player playr)
        {
            if (Players.TryAdd(playr.Identity.UsernameLower, playr))
            {
                _ChatMsg += new OnChat(playr.OnCustomSpeak);
                Map outm;
                if (World.GetMap(playr.Position.CurMap.Name) == null)
                {
                    playr.Position.CurMap.Name = "Rest";
                    playr.Position.X = 15;
                    playr.Position.Y = 15;
                }
                if (Maps.TryGetValue(playr.Position.CurMap.Name, out outm))
                {
                    outm.Enter(playr);
                }
            }
        }

        public static void Exit(Player.Player playr)
        {
            _ChatMsg -= new OnChat(playr.OnCustomSpeak);
            Player.Player outp;
            if (!Players.TryRemove(playr.Identity.UsernameLower, out outp))
            {
                Log.LogException(new Exception(), "Failed to remove player from world " + playr.Identity.UsernameLower);
            }
        }

        public static void WorldSave()
        {
            Log.LogLine("World Saving", ConsoleColor.Green);
            foreach (var plyr in Players)
                DB.Loader.SavePlayer(plyr.Value);
           // DB.Loader.SaveBoard(SysOpBoard, "sysop");
            DB.Loader.SaveBoard(NoticeBoard, "notice");
        }

        #region Script
        public static Map AddMap(string name, string diskName, Map.E_MapType mtype = Map.E_MapType.Combat, string tilet = Map.tileLk, int time = 1, int lr = 3)
        {
            Log.LogLine(string.Format("Adding Map: {0} diskName: {1}", name, diskName), ConsoleColor.DarkYellow);
            var newmap = new Map(name, diskName, mtype, tilet);
            newmap.Time = time;
            if (time == -1)
            {
                newmap.Time = 0;
                newmap.TimeCycle = true;
            }
            newmap.LightRadius = lr;
            newmap.Init();
            Maps.Add(name, newmap);
            return newmap;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.IO;
using ProtoBuf;

namespace LKCamelotV2.World
{
    [ProtoContract]
    public class Map
    {
        public const string tileVV = "VV.Til,vv.obj";
        public const string tileWeak = "weaktil.til,weakobj.obj";
        public const string tilePS = "pstil.til,psobj.obj";
        public const string tileLk = "LkTil.Til,LkObj.Obj";
        public const string tileBone = "bonetil.Til,boneobj.Obj";

        public Map()
        {
        }

        public Map(string name, string diskName, E_MapType mtype = Map.E_MapType.Combat, string tilet = tileLk)
        {
            Name = name;
            this.diskName = diskName;
            MapType = mtype;
            Events = new MapEvents(this);
            MapTiles = tilet;
        }

        [ProtoMember(1)]
        public string Name;

        public string MapTiles = tileLk;
        public E_MapType MapType;

        public string diskName;
        public int sizeX;
        public int sizeY;
        public int EnterX;
        public int EnterY;

        public Tile[,] Tiles;
        // public Dictionary<Point2D, Tile> Tiles = new Dictionary<Point2D, Tile>();

        public int LightRadius = 3;
        public int Time;
        public bool TimeCycle = false;
        public long lastTimeCycle = 0;
        ConcurrentDictionary<string, Player.Player> Players = new ConcurrentDictionary<string, Player.Player>();
        ConcurrentDictionary<int, Object.Monster> Monsters = new ConcurrentDictionary<int, Object.Monster>();
        public ConcurrentDictionary<int, Object.Monster> DeadMonsters = new ConcurrentDictionary<int, Object.Monster>();
        public ConcurrentDictionary<int, Object.Mineral> DeadMinerals = new ConcurrentDictionary<int, Object.Mineral>();
        public ConcurrentDictionary<int, Object.Item> Items = new ConcurrentDictionary<int, Object.Item>();
        public ConcurrentDictionary<int, Object.Mineral> Minerals = new ConcurrentDictionary<int, Object.Mineral>();
        public ConcurrentDictionary<int, Object.Craft> Craft = new ConcurrentDictionary<int, Object.Craft>();
        public ConcurrentDictionary<int, Object.Npc> Npcs = new ConcurrentDictionary<int, Object.Npc>();

        ConcurrentDictionary<int, Object.Object> Objects = new ConcurrentDictionary<int, Object.Object>();

        public MapEvents Events;

        public Object.Object GetObject(int id)
        {
            Object.Object outo;
            Objects.TryGetValue(id, out outo);
            return outo;
        }

        public Player.Player[] PlayersList
        {
            get
            {
                return Players.Skip(0).Select(x => x.Value).ToArray();
            }
        }

        public List<Object.Living> TargetsInAoE(Point2D loc, int range, bool pkon = false)
        {
            List<Object.Living> retl = new List<Object.Living>();
            List<Player.Player> plys = new List<Player.Player>();
            var mobs = Monsters.Skip(0).Where(xe => Point2D.Distance(loc.X, loc.Y, xe.Value.Position.X, xe.Value.Position.Y) < range).Select(xe => xe.Value).ToList();
            if (pkon)
                plys = Players.Skip(0).Where(xe => Point2D.Distance(loc.X, loc.Y, xe.Value.Position.X, xe.Value.Position.Y) < range).Select(xe => xe.Value).ToList();
            retl.AddRange(mobs);
            retl.AddRange(plys);
            return retl;
        }

        public List<Object.Living> TileLivingT(int x, int y, bool pkon = false)
        {
            List<Object.Living> retl = new List<Object.Living>();
            List<Player.Player> plys = new List<Player.Player>();
            var mobs = Monsters.Skip(0).Where(xe => xe.Value.Position.X == x && xe.Value.Position.Y == y).Select(xe => xe.Value).ToList();
            if (pkon)
                plys = Players.Skip(0).Where(xe => xe.Value.Position.X == x && xe.Value.Position.Y == y).Select(xe => xe.Value).ToList();
            retl.AddRange(mobs);
            retl.AddRange(plys);
            return retl;
        }

        public List<Object.Mobile> TileCraftingT(int x, int y, int type)
        {
            List<Object.Mobile> retl = new List<Object.Mobile>();
            if (type == 1)
            {
                var mins = Minerals.Skip(0).Where(xe => xe.Value.Position.X == x && xe.Value.Position.Y == y).Select(xe => xe.Value).ToList();
                retl.AddRange(mins);
                var cauldron = Craft.Skip(0).Where(xe => xe.Value.Position.X == x && xe.Value.Position.Y == y && xe.Value._Name == "CAULDRON").Select(xe => xe.Value).ToList();
                retl.AddRange(cauldron);
            }
            else if (type == 2)
            {
                var anvil = Craft.Skip(0).Where(xe => xe.Value.Position.X == x && xe.Value.Position.Y == y && xe.Value._Name == "ANVIL").Select(xe => xe.Value).ToList();
                retl.AddRange(anvil);
            }
            return retl;
        }

        public Tile[,] CopyTiles
        {
            get
            {
                var ret = new Tile[sizeX, sizeY];
                for (int xx = 0; xx < sizeX; xx++)
                {
                    for (int yy = 0; yy < sizeY; yy++)
                    {
                        var oldtile = Tiles[xx, yy];
                        ret[xx, yy] = new Tile(xx, yy, oldtile.WalkFlags, oldtile.Occupied);
                    }
                }
                for (int xx1 = 0; xx1 < sizeX; xx1++)
                {
                    for (int yy1 = 0; yy1 < sizeY; yy1++)
                    {
                        var oldtile = Tiles[xx1, yy1];
                        var newtile = ret[xx1, yy1];
                        for (int x = 0; x < oldtile.Neighbours.Count; x++)
                        {
                            var oldnei = oldtile.Neighbours[x];
                            newtile.Neighbours.Add(ret[oldnei.X, oldnei.Y]);
                        }
                    }
                }   

                /*
                Tile temtile;
                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        var tile = ret[x, y];

                        if (y - 1 >= 0)
                        {
                            temtile = ret[x, y - 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x + 1 < sizeX && y - 1 >= 0)
                        {
                            temtile = ret[x + 1, y - 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x + 1 < sizeX)
                        {
                            temtile = ret[x + 1, y];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x + 1 < sizeX && y + 1 < sizeY)
                        {
                            temtile = ret[x + 1, y + 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (y + 1 < sizeY)
                        {
                            temtile = ret[x, y + 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x - 1 >= 0 && y + 1 < sizeY)
                        {
                            temtile = ret[x - 1, y + 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x - 1 >= 0)
                        {
                            temtile = ret[x - 1, y];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            temtile = ret[x - 1, y - 1];
                            if ((temtile.WalkFlags & 1) != 0)
                                tile.Neighbours.Add(temtile);
                        }
                    }
                }*/
                return ret;
            }
        }

        internal Tile GetRandomTile(bool walkable = true)
        {
			for (int itr = 0; itr < sizeX * sizeY; itr++)
			{
				var randomx = Dice.Random(0, sizeX - 1);
				var randomy = Dice.Random(0, sizeY - 1);
				var ttile = Tiles[randomx, randomy];
				if (!walkable)
					return ttile;
				
				if ((ttile.WalkFlags & (int)Tile.E_WalkFlags.Walkable) != 0)
					return ttile;
				else 
					continue;
			}
            return null;
        }

        internal Tile GetTile(int x, int y)
        {
            Tile tile = null;
            try
            {
                tile = Tiles[x, y];
            }
            catch { tile = null; }
            return tile;
        }

		public void Tick(long time)
        {
            try
            {
                foreach (var mob in Monsters.Skip(0).Select(x => x).ToArray())
                {
                    try
                    {
						mob.Value.Think(time);
                    }
                    catch (Exception e)
                    {
						Log.LogException(e, "mob.Value.Think(time)");
                    }
                }

                foreach (var mob in DeadMonsters.Skip(0).Select(x => x).ToArray())
                {
                    if ((mob.Value.RespawnTime + mob.Value.KilledTime < time) || mob.Value.KilledTime == 0)
                    {
                        Object.Monster outm;
                        if (DeadMonsters.TryRemove(mob.Key, out outm))
                        {
                            //Spawn it
                            outm.Position.CurMap = this;
                            var spawnp = outm.SpawnPoint;
                            if (spawnp != null)
                            {
                                outm.Position.X = spawnp.X;
                                outm.Position.Y = spawnp.Y;
                                outm.HPCur = outm.HP;
                                outm.Died = false;
                                Enter(outm);
                            }
                            else
                            {
                                if (!DeadMonsters.TryAdd(outm.objId, outm))
                                    Log.LogLine("Failed to add mob after spawn failure");
                            }
                        }
                    }
                }

                foreach (var mine in DeadMinerals.Skip(0).Select(x => x).ToArray())
                {
                    if ((mine.Value.RespawnTime + mine.Value.KilledTime < time) || mine.Value.KilledTime == 0)
                    {
                        Object.Mineral outm;
                        if (DeadMinerals.TryRemove(mine.Key, out outm))
                        {
                            //Spawn it
                            outm.Position.CurMap = this;
                            var spawnp = outm.SpawnPoint;
                            if (spawnp != null)
                            {
                                outm.Position.X = spawnp.X;
                                outm.Position.Y = spawnp.Y;
                                outm.THits = outm.MTHits;
                                outm.Died = false;
                                Enter(outm);
                            }
                            else
                            {
                                if (!DeadMinerals.TryAdd(outm.objId, outm))
                                    Log.LogLine("Failed to add mineral after spawn failure");
                            }
                        }
                    }
                }

                foreach (var NPC in Npcs.Values)
                {
                    if (NPC.ChatPhrases.Count == 0)
                        continue;

                    if (NPC.lastSpeakTime + 30000 > time)
                        continue;

                    var speak = NPC.ChatPhrases[NPC.lastPhrase];
                    var plays = TargetsInAoE(new Point2D() { X = NPC.Position.X, Y = NPC.Position.Y }, 13, true);
                    foreach (var play in plays)
                    {
                        if (!(play is Player.Player))
                            continue;
                        (play as Player.Player).OnMobileSpeak(NPC, speak);
                    }

                    NPC.lastSpeakTime = time;
                    NPC.lastPhrase++;
                    if (NPC.lastPhrase >= NPC.ChatPhrases.Count)
                        NPC.lastPhrase = 0;
                }

                foreach (var playr in Players)
                {
                    if (Name == "Rest")
                        playr.Value.State.TickRegen(time);
                    if (playr.Value.State.autohit)
                        playr.Value.SwingAttack(time);
                    foreach (var buf in playr.Value.Buffs)
                        buf.Value.Tick(playr.Value);
                }

                foreach (var craft in Craft)
                {
                    craft.Value.Tick();
                }

                if (TimeCycle)
                {
                    long cyclet = 2400000;
                    if (Time > 1)
                        cyclet = 600000;
                    if (lastTimeCycle + cyclet < time)
                    {
                        lastTimeCycle = time;
                        Time++;
                        if (Time > 7)
                            Time = 0;
                        Events.OnTimeCyclee(Time);
                    }
                }

                if (lastItemCheck + 60000 < time)
                {
                    lastItemCheck = time;
                    var items = Items.Skip(0).Select(xe => xe).ToArray();
                    foreach (var itm in items)
                    {
                        if (!itm.Value.DroppedTime.HasValue)
                            continue;
                        if (itm.Value.DroppedTime.Value + 900000 < time)
                            Exit(itm.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogException(e);
            }
        }

        long lastItemCheck;

        public void Enter(Object.Object obj)
        {
            if (!Objects.TryAdd(obj.objId, obj))
            {
                Log.LogLine(string.Format("Trying to add object {0}, it already exists", obj.objId));
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
                return;
            }

            if (obj is Object.Mobile)
            {
                var mobl = obj as Object.Mobile;
                if (obj is Player.Player)
                {
                    var playr = obj as Player.Player;
                    if (Players.TryAdd(playr.Identity.UsernameLower, playr))
                    {
                        playr.LightRadius = LightRadius;
                        playr.Position.CurMap = this;
                        System.Threading.Thread.MemoryBarrier();
                        playr.OnChangedMap(playr);
                        //Ensure old mobiles reveal
                        foreach (var plyr in Objects.Skip(0).Select(x => x.Value).ToArray()) playr.OnEnterMap(plyr);

                        Events.RegisterListeners(playr);
                        Events.OnEnter(playr);
                    }
                }
                else if (obj is Object.Monster)
                {
                    if (Monsters.TryAdd(obj.objId, obj as Object.Monster))
                    {
                        mobl.Position.CurMap = this;
                        Events.OnEnter(obj as Object.Monster);
                    }
                }
                else if (obj is Object.Item)
                {
                    if (Items.TryAdd(obj.objId, obj as Object.Item))
                    {
                        mobl.Position.CurMap = this;
                        Events.OnEnter(obj as Object.Item);
                    }
                }
                else if (obj is Object.Npc)
                {
                    if (Npcs.TryAdd(obj.objId, obj as Object.Npc))
                    {
                        mobl.Position.CurMap = this;
                        Events.OnEnter(obj as Object.Npc);
                    }
                }
                else if (obj is Object.Mineral)
                {
                    if (Minerals.TryAdd(obj.objId, obj as Object.Mineral))
                    {
                        mobl.Position.CurMap = this;
                        Events.OnEnter(obj as Object.Mineral);
                    }
                }
                else if (obj is Object.Craft)
                {
                    if (Craft.TryAdd(obj.objId, obj as Object.Craft))
                    {
                        mobl.Position.CurMap = this;
                        Events.OnEnter(obj as Object.Craft);
                    }
                }
            }
        }

        public void Exit(Object.Object obj)
        {
            Object.Object outo;
            if (!Objects.TryRemove(obj.objId, out outo))
            {
                Log.LogLine(string.Format("Trying to remove object {0}, failed", obj.objId));
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
                return;
            }

            if (obj is Player.Player)
            {
                Player.Player outp;
                var playr = obj as Player.Player;
                if (Players.TryRemove(playr.Identity.UsernameLower, out outp))
                {
                    Events.OnLeave(playr);
                    Events.RemoveListeners(playr);
                }
                else
                {
                    Log.LogException(new Exception(), "Failed to remove player from world " + playr.Identity.UsernameLower);
                }
            }
            else if (obj is Object.Monster)
            {
                Object.Monster outm;
                if (Monsters.TryRemove(obj.objId, out outm))
                {
                    Events.OnLeave(obj);
                }
                else
                {
                    Log.LogException(new Exception(), "Failed to remove monster from world " + obj.objId);
                }
            }
            else if (obj is Object.Item)
            {
                Object.Item outi;
                if (Items.TryRemove(obj.objId, out outi))
                {
                    Events.OnLeave(obj);
                }
                else
                {
                    Log.LogException(new Exception(), "Failed to remove item from world " + obj.objId);
                }
            }
            else if (obj is Object.Mineral)
            {
                Object.Mineral outi;
                if (Minerals.TryRemove(obj.objId, out outi))
                {
                    Events.OnLeave(obj);
                }
                else
                {
                    Log.LogException(new Exception(), "Failed to remove mineral from world " + obj.objId);
                }
            }
            else if (obj is Object.Craft)
            {
                Object.Craft outi;
                if (Craft.TryRemove(obj.objId, out outi))
                {
                    Events.OnLeave(obj);
                }
                else
                {
                    Log.LogException(new Exception(), "Failed to remove craft from world " + obj.objId);
                }
            }
        }

        public enum E_MapType
        {
            Safe = 1,
            Combat = 2,
        }

        static int offset = 76;
        public void Init()
        {
            var mappath = Path.Combine("map", diskName);
            var fileStr = File.Exists(mappath);
            if (!fileStr)
            {
                throw new Exception("Map File not found");
            }

            var input = File.ReadAllBytes(mappath);
            byte[] temp = new byte[4];

            Array.Copy(input, 72, temp, 0, 4);
            int objStrCnt = BitConverter.ToInt32(temp, 0);


            sizeX = input[0x20];
            sizeY = input[0x24];
            int cnt = sizeX * sizeY;
            int itr = 0, res2 = 0, res1 = 0;
            Tiles = new Tile[sizeX, sizeY];

            while (itr != cnt)
            {
                Array.Copy(input, offset + (itr * 4), temp, 0, 4);
                temp[3] = 0;
                if (itr % sizeX == 0 && itr != 0)
                {
                    res2++;
                    res1 = 0;
                }
                if (Unwalkable.Where(xe => xe[0] == temp[0] && xe[1] == temp[1] && xe[2] == temp[2]).FirstOrDefault() != null)
                {
                    var tile = new Tile(res1, res2, 0);
                    Tiles[res1++, res2] = tile;
                }
                else
                {
                    var tile = new Tile(res1, res2, (int)Tile.E_WalkFlags.Walkable);
                    Tiles[res1++, res2] = tile;
                }
                itr++;
            }
            AddNeighbours();

            var strOff = offset + cnt * 4;
            byte[] tempObj = new byte[32];
            itr = 0;
            while (itr != objStrCnt)
            {
                Array.Copy(input, strOff + (itr * 32), tempObj, 0, 32);
                int oX = tempObj[0];
                int oY = tempObj[4];

                var objName = Encoding.GetEncoding(949).GetString(tempObj, 16, 16);
                var oName = objName.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries)[0];
                MapObjects.SetCollisions(oName, Tiles, MapTiles, oX, oY);

                itr++;
            }
        }

        List<byte[]> Unwalkable
        {
            get
            {
                var ret = new List<byte[]>();
                switch (MapTiles)
                {
                    case "VV.Til,vv.obj":
                        ret.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x04, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x06, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x07, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x08, 0x00 });

                        break;
                    case "weaktil.til,weakobj.obj":
                        ret.Add(new byte[] { 0x01, 0x00, 0x02, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x03, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x04, 0x00 });
                        ret.Add(new byte[] { 0x02, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x03, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x04, 0x00, 0x00, 0x00 });
                        break;
                    case "pstil.til,psobj.obj":
                        ret.Add(new byte[] { 0x01, 0x00, 0x01, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x02, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x03, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x04, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x07, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x08, 0x00 });
                        ret.Add(new byte[] { 0x01, 0x00, 0x09, 0x00 });
                        break;
                    case "LkTil.Til,LkObj.Obj":
                        ret.Add(new byte[] { 0x6A, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x6B, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x6C, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x6D, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x6E, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x6F, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x65, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x66, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x67, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x68, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x70, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x72, 0x00, 0x00, 0x00 });

                        ret.Add(new byte[] { 0x02, 0x00, 0x03, 0x00 });
                        ret.Add(new byte[] { 0x03, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x03, 0x00, 0x07, 0x00 });
                        ret.Add(new byte[] { 0x03, 0x00, 0x08, 0x00 });
                        ret.Add(new byte[] { 0x05, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x05, 0x00, 0x0A, 0x00 });
                        ret.Add(new byte[] { 0x05, 0x00, 0x01, 0x00 });
                        ret.Add(new byte[] { 0x05, 0x00, 0x04, 0x00 });
                        ret.Add(new byte[] { 0x09, 0x00, 0x00, 0x00 });
                        ret.Add(new byte[] { 0x09, 0x00, 0x02, 0x00 });
                        ret.Add(new byte[] { 0x09, 0x00, 0x07, 0x00 });
                        ret.Add(new byte[] { 0x09, 0x00, 0x08, 0x00 });

                        break;
                    case "bonetil.Til,boneobj.Obj":
                        ret.Add(new byte[] { 0x01, 0x00, 0x00, 0x00 });
                        break;
                }

                return ret;
            }
        }

        public void AddNeighbours()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var tile = Tiles[x, y];
                    if (y - 1 >= 0)
                        tile.Neighbours.Add(Tiles[x, y - 1]);
                    if (x + 1 < sizeX && y - 1 >= 0)
                        tile.Neighbours.Add(Tiles[x + 1, y - 1]);
                    if (x + 1 < sizeX)
                        tile.Neighbours.Add(Tiles[x + 1, y]);
                    if (x + 1 < sizeX && y + 1 < sizeY)
                        tile.Neighbours.Add(Tiles[x + 1, y + 1]);
                    if (y + 1 < sizeY)
                        tile.Neighbours.Add(Tiles[x, y + 1]);
                    if (x - 1 >= 0 && y + 1 < sizeY)
                        tile.Neighbours.Add(Tiles[x - 1, y + 1]);
                    if (x - 1 >= 0)
                        tile.Neighbours.Add(Tiles[x - 1, y]);
                    if (x - 1 >= 0 && y + 1 < sizeY)
                        tile.Neighbours.Add(Tiles[x - 1, y]);
                }
            }
        }
    }
}

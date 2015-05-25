using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Mineral : Mobile
    {
        public int THits;
        public int MTHits = 200;

        public string OreDropped;
        public string GemDrop;

        public int RespawnTime = 30000;
        public long KilledTime;
        public Point2D spwnBndAA;
        public Point2D spwnBndBB;
        public int XPGranted = 0;
        public bool Died = false;
        public int Level = 1;

        public Mineral(string name, string OreDropped, int sprid, int XPG, int level, int MHits = 200, int RETime = 30000)
        {
            this.Name = name;
            this.OreDropped = OreDropped;
            this.SprId = sprid;
            this.XPGranted = XPG;
            this.MTHits = MHits + Dice.RandomMinMax(-50,50);
            this.Level = level;
            this.RespawnTime = RETime;
        }

        public void Hit(Player.Player plyr)
        {
            if (plyr.State.MinerLevel < Level)
            {
                plyr.WriteWarn(string.Format("You must be at least level {0} to mine {1}.", Level, Name));
                return;
            }

            THits--;
            if (THits % 15 == 0)
            {
                var gradechance = plyr.State.MinerLevel - Level;
                var roll = Dice.Random(0, 20);
                int grade = 1;
                if (roll < gradechance + 3)
                    grade = 2;
                if (roll < gradechance)
                    grade = 3;

                var ssize = Dice.RandomMinMax(1, 3);

                string GradeStr = " PB";
                if (grade == 2)
                    GradeStr = " PN";
                else if (grade == 3)
                    GradeStr = " PG";
                Ore newitem = Scripts.Items.CreateItem(OreDropped + GradeStr, ssize) as Ore;

                newitem.Position.X = plyr.Position.X;
                newitem.Position.Y = plyr.Position.Y;
                Position.CurMap.Enter(newitem);

                if (roll == 3)
                {
                    if (!string.IsNullOrEmpty(GemDrop))
                    {
                        var gemroll = Dice.Random(0, 20);
                        if (roll <= gradechance)
                        {
                            Item newgem = Scripts.Items.CreateItem(GemDrop);
                            newgem.Position.X = plyr.Position.X;
                            newgem.Position.Y = plyr.Position.Y;
                            Position.CurMap.Enter(newgem);
                            plyr.WriteWarn(string.Format("You expertly extract a rare gem.", XPGranted));
                        }
                    }
                }

                plyr.State.MinerXP += XPGranted;
                plyr.WriteWarn(string.Format("You have gained {0} experience.", XPGranted));
            }
            if (THits < 15)
            {
                Die();
            }
        }

        public void Die()
        {
            Died = true;
            KilledTime = World.World.tickcount.ElapsedMilliseconds;
            Position.CurMap.Exit(this);
            Position.CurMap.DeadMinerals.TryAdd(this.objId, this);
        }

        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 0x0B;
                ret[9] = (byte)SprId;
                return ret;
            }
        }

        public Point2D SpawnPoint
        {
            get
            {
                int x, y;
                if (spwnBndAA == null || spwnBndBB == null)
                {
                    x = Dice.RandomMinMax(0, (Position.CurMap.sizeX - 1));
                    y = Dice.RandomMinMax(0, (Position.CurMap.sizeY - 1));
                }
                else
                {
                    x = Dice.RandomMinMax(spwnBndAA.X, spwnBndBB.X);
                    y = Dice.RandomMinMax(spwnBndAA.Y, spwnBndBB.Y);
                }
                var tile = Position.CurMap.Tiles[x, y];
                if ((tile.WalkFlags & 1) != 0)
                {
                 //   tile.Occupied = true;
                    return new Point2D() { X = x, Y = y };
                }
                else
                    return null;
            }
        }
    }
}

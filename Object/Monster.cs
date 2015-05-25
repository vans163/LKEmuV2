using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Monster : Living
    {
        public Monster(string name, int sprid, int HP, int AC, int Hit, int Dam, int xp)
        {
            this.Name = name;
            this.SprId = sprid;
            this.HP = HP;
            this.AC = AC;
            this.Hit = Hit;
            this.Dam = Dam;
            this.XPGranted = xp;
			Attribs [E_Attribute.HPCur] = HP;
			Attribs [E_Attribute.MPCur] = MP;
        }

        public int AggroRange = 11;
        public int WalkSpeed = 650;
        public int AttackSpeed = 650;
        public int RespawnTime = 30000;
        public long KilledTime;
        public int FrameType = 1;
        public Point2D spwnBndAA;
        public Point2D spwnBndBB;
        public int XPGranted = 0;

        public World.Tile CurTargetLastPos;
        public Mobile CurTarget;
        public List<World.Tile> WalkPath;
        public ConcurrentDictionary<int, int> Threat = new ConcurrentDictionary<int, int>();
        public Dictionary<string, float> DropTable = new Dictionary<string, float>();
        NavMesh NavMesh;
        Brain Brain = new Brain();

        public override void Die()
        {
            Died = true;
            KilledTime = World.World.tickcount.ElapsedMilliseconds;
            DropLoot();
            Position.CurMap.Exit(this);
            if (!Position.CurMap.DeadMonsters.TryAdd(this.objId, this))
                Log.LogLine("Failed to add monster after it died to dead collection");
        }

        public void DropLoot()
        {
            foreach (var item in DropTable)
            {
                var roll = Dice.Random(0, 100000);
                var totcha = item.Value * 1000;
                if (totcha >= roll)
                {
                    Item newitem = null;
                    if (item.Key[0] == 'g')
                    {
                        var groll = item.Key.Split(':')[1];
                        var amount = Dice.Roll(groll);
                        newitem = new Gold() { Amount = amount };
                    }
                    else
                    {
                        newitem = Scripts.Items.CreateItem(item.Key);
                    }
                    newitem.Position.X = Position.X;
                    newitem.Position.Y = Position.Y;
                    newitem.DroppedTime = World.World.tickcount.ElapsedMilliseconds;
                    Position.CurMap.Enter(newitem);
                }
            }
        }

		public bool TryAttack(Mobile target, int dist, long ctick)
		{
			if (dist <= 1) 
			{
				if (LastAttack + AttackSpeed < ctick)
				{
					LastAttack = ctick;

					// mob.Value.Attack(mob.Value.TargetP);
					Position.Face = (int)Util.GetAngle(Position.X, Position.Y, CurTarget.Position.X, CurTarget.Position.Y);
					Position.CurMap.Events.OnSwing(this, this.Position.Face);

					var curtile = Position.CurrentTile;
					if (curtile == null)
						return false;
					var tartile = Util.AdjecentTile(curtile, Position.Face, Position.CurMap);
					if (tartile == null)
						return false;
					var targets = Position.CurMap.TileLivingT(tartile.X, tartile.Y, true);
					if (targets.Count > 0)
					{
						foreach (var t in targets)
						{
							if (t is Player.Player)
							{
								t.TakeDamage(this);
								return true;
							}
						}
					}
				}
				return true;
			}

			return false;
		}

		public void Think(long ctick)
        {
            if (CurTarget != null && CurTarget.Position.CurMap == Position.CurMap)
            {
				var dist = Point2D.Distance(CurTarget.Position.X, CurTarget.Position.Y, Position.X, Position.Y);

				if (TryAttack(CurTarget, dist, ctick))
					return;

                if (dist > 18 || CurTarget.Position.CurMap != Position.CurMap)
                {
                    CurTarget = null;
                    return;
                }

                if (LastWalk + WalkSpeed < ctick)
                {
                    LastWalk = ctick;
                    NavMesh = new NavMesh(this.Position.CurMap);
                    CurTargetLastPos = CurTarget.Position.CurrentTile;
                    WalkPath = FindPath(NavMesh.Tiles[Position.CurrentTile.X, Position.CurrentTile.Y],
                        NavMesh.Tiles[CurTarget.Position.CurrentTile.X, CurTarget.Position.CurrentTile.Y]);
                    DoMove();
                }
            }

            if (CurTarget == null || CurTarget.Position.CurMap != Position.CurMap)
            {
                bool onscreen = false;
                foreach (var plyr in Position.CurMap.PlayersList)
                {
                    var dist = Point2D.Distance(plyr.Position.X, plyr.Position.Y, Position.X, Position.Y);
                    if (dist <= AggroRange)
                    {
                        CurTarget = plyr;
                        return;
                    }
                    if (dist <= 15)
                        onscreen = true;
                }
                if ((CurTarget == null || CurTarget.Position.CurMap != Position.CurMap) && onscreen)
                {
                    if (LastWalk + WalkSpeed < ctick)
                    {
                        LastWalk = ctick;
                        NavMesh = new NavMesh(this.Position.CurMap);
                        var rndMoveTile = Position.CurrentTile.Neighbours[Dice.Random(Position.CurrentTile.Neighbours.Count - 1)];
                        WalkPath = FindPath(NavMesh.Tiles[Position.CurrentTile.X, Position.CurrentTile.Y],
                            NavMesh.Tiles[rndMoveTile.X, rndMoveTile.Y]);
                        DoMove();
                    }
                }
            }
        }

        World.Tile RandomMoveTile(World.Tile pos)
        {
            return null;
        }

        void DoMove()
        {
            var pop = WalkPath.FirstOrDefault();
            if (pop == null)
                return;
            WalkPath.RemoveAt(0);

            Position.Face = (int)Util.GetAngle(Position.X, Position.Y, pop.X, pop.Y);
            Position.CurMap.Events.OnWalk(this);
            Position.X = pop.X;
            Position.Y = pop.Y;
        }

        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 1;
                ret[9] = (byte)SprId;
                return ret;
            }
        }

        public List<World.Tile> FindPath(World.Tile fir, World.Tile sec)
        {
            AStar<World.Tile> astar = new AStar<World.Tile>(
                delegate(World.Tile t1, World.Tile t2)
                {
                    var xdiff = (t1.X - t2.X);
                    var ydiff = (t1.Y - t2.Y);
                    return Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                });

            astar.FindPath(fir, sec);//, runindex);

            return astar.Path;
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
                    //tile.Occupied = true;
                    return new Point2D() { X = x, Y = y };
                }
                else
                    return null;
            }
        }
    }
}

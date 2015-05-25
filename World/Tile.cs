using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.World
{
    public class Tile : IAStarNode<Tile>
    {
        public int X;
        public int Y;
        public int WalkFlags;
        public Portal Portal;
        public bool Occupied;

        public enum E_WalkFlags
        {
            Walkable = 1,
            Projectile = 2,
            Flyable = 4,
        }

        public List<Tile> Neighbours { get; set; }
        public double Cost { get; set; }
        public Tile ParentNode { get; set; }
        public bool inopenlist { get; set; }
        public bool inclosedlist { get; set; }
      //  public long runindex { get; set; }

        public Tile()
        {
            this.Neighbours = new List<Tile>();
        }

        public Tile(int x, int y, int WalkFlags)
        {
            X = x;
            Y = y;
            this.WalkFlags = WalkFlags;
            this.Neighbours = new List<Tile>();
        }

        public Tile(int x, int y, int WalkFlags, bool occupied)
        {
            X = x;
            Y = y;
            this.WalkFlags = WalkFlags;
            this.Neighbours = new List<Tile>();
            this.Occupied = occupied;
        }
    }
}

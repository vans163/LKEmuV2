using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [ProtoContract]
    public class Position
    {
        [ProtoMember(1)]
        internal int X;
        [ProtoMember(2)]
        internal int Y;
        [ProtoMember(3)]
        internal int Face;
        [ProtoMember(4)]
        World.Map _CurMap;

        internal World.Map CurMap
        {
            get
            {
                if (_CurMap == null)
                    return World.World.GetMap("Rest");
                return _CurMap;
            }
            set
            {
                _CurMap = value;
            }
        }

        internal int DistanceTo(int x, int y)
        {
            return (int)Math.Sqrt((x - this.X) * (x - this.X) + (y - this.Y) * (y - this.Y));
        }

        //Bad dont do this, fix it
        internal World.Tile CurrentTile
        {
            get
            {
                World.Tile tile = null; 
                try
                {
                    tile = CurMap.Tiles[X, Y];
                }
                catch { tile = null; }
                return tile;
            }
        }
    }
}

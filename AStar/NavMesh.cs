using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2
{
    public class NavMesh
    {
        public World.Tile[,] Tiles;

        public NavMesh(World.Map map)
        {
            Tiles = map.CopyTiles;
        }
    }
}

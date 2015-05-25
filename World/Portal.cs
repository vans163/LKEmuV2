using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.World
{
    public class Portal
    {
        public string destMap;
        public int destX;
        public int destY;

        public Portal(string dmap, int dx, int dy)
        {
            this.destMap = dmap;
            this.destX = dx;
            this.destY = dy;
        }
    }
}

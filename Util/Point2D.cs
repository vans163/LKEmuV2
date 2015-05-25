using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2
{
    public class Point2D
    {
        public int X;
        public int Y;

        public Point2D()
        {
        }

        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static int Distance(int x, int y, int x2, int y2)
        {
            return (int)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        }
    }
}

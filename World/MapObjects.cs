using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.World
{
    public class MapObject
    {
        public MapObject()
        {
        }

        public MapObject(List<Point2D> unwalk)
        {
            UnwalkableOffset = unwalk;
        }

        public string Name;
        public List<Point2D> UnwalkableOffset;
        //Object starts counting in bottom left corner. 
        public void ApplyOffsets(Tile[,] tiles, int pX, int pY)
        {
            foreach (var pnt in UnwalkableOffset)
            {
                tiles[pX + pnt.X, pY - pnt.Y].WalkFlags = 0;
            }
        }
    }

    public static class MapObjects
    {
        public static void SetCollisions(string obj, Tile[,] tiles, string tilset, int pX, int pY)
        {
            var objlist = Objects(tilset);
            MapObject outo;
            if (objlist.TryGetValue(obj, out outo))
            {
                outo.ApplyOffsets(tiles, pX, pY);
            }
        }

        static MapObject MPo(int x, int y)
        {
            return new MapObject() { UnwalkableOffset = new List<Point2D>() { new Point2D(x, y) } };
        }

        static MapObject MPoT(int x, int y)
        {
            var ret = new MapObject();
            ret.UnwalkableOffset = new List<Point2D>();
            for (int cx = 0; cx < x; cx++)
            {
                for (int cy = 0; cy < y; cy++)
                {
                    ret.UnwalkableOffset.Add(new Point2D(cx, cy));
                }
            }
            return ret;
        }

        static MapObject MPoTree(int fx, int fy, int x, int y)
        {
            var ret = new MapObject();
            ret.UnwalkableOffset = new List<Point2D>();
            for (int cx = fx; cx < fx + x; cx++)
            {
                for (int cy = fy; cy < fy + y; cy++)
                {
                    ret.UnwalkableOffset.Add(new Point2D(cx, cy));
                }
            }
            return ret;
        }

        static Dictionary<string, MapObject> Objects(string tilset)
        {
            var ret = new Dictionary<string, MapObject>();
            switch (tilset)
            {
                case Map.tileBone:
                    break;
                case Map.tileLk:
                    ret.Add("나무01", MPo(2, 0));
                    ret.Add("나무02", MPo(2, 0));
                    ret.Add("나무03", MPo(1, 0));
                    ret.Add("나무04", MPo(2, 0));
                    ret.Add("나무05", MPo(3, 0));
                    ret.Add("나무06", MPoTree(2, 0, 2, 2));
                    ret.Add("나무07", MPoTree(2, 0, 2, 2));
                    ret.Add("나무08", MPoTree(2, 0, 2, 2));
                    ret.Add("나무09", MPoTree(2, 0, 2, 2));
                    ret.Add("나무10", MPoTree(2, 0, 2, 2));
                    ret.Add("나무11", MPo(3, 0));
                    ret.Add("나무12", MPoTree(2, 0, 2, 2));
                    ret.Add("나무13", MPoTree(2, 0, 2, 2));
                    ret.Add("나무14", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 2),
                        new Point2D(2, 1), new Point2D(2, 2), new Point2D(2, 3), new Point2D(3, 2),
                        new Point2D(3, 3),
                    }));

                    ret.Add("담돌01", new MapObject(new List<Point2D>() { new Point2D(0, 0), new Point2D(0, 1), new Point2D(0, 2), new Point2D(1, 2), new Point2D(2, 2) }));
                    ret.Add("담돌02", MPoT(3, 1));
                    ret.Add("담돌03", new MapObject(new List<Point2D>() { new Point2D(0, 1), new Point2D(1, 1), new Point2D(2, 1), new Point2D(2, 0) }));
                    ret.Add("담돌04", MPoT(2, 1));
                    ret.Add("담돌05", MPoT(2, 1));
                    ret.Add("담돌06", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(0, 1), new Point2D(0, 2), new Point2D(0, 3),
                        new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0),
                    }));
                    ret.Add("담돌07", MPoT(3, 1));
                    ret.Add("담돌08", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(3, 1), new Point2D(3, 2), new Point2D(3, 3),
                        new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0),
                    }));
                    ret.Add("담울01", MPoT(2, 1));
                    ret.Add("담울02", MPoT(2, 1));
                    ret.Add("담울03", MPoT(2, 1));
                    ret.Add("담울04", MPoT(1, 2));
                    ret.Add("담울05", MPoT(1, 2));
                    ret.Add("담울06", MPoT(2, 1));
                    ret.Add("담울07", MPoT(2, 1));
                    ret.Add("담울08", MPoT(2, 1));
                    ret.Add("담울10", MPoT(3, 2));
                    ret.Add("담울11", MPoT(3, 2));
                    ret.Add("담울12", MPoT(3, 2));
                    ret.Add("담울13", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0),
                        new Point2D(0, 1), new Point2D(1, 1), new Point2D(2, 1), new Point2D(3, 1),
                        new Point2D(0, 2), new Point2D(1, 2), 
                        new Point2D(0, 3), new Point2D(1, 3),
                    }));
                    ret.Add("담울14", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0),
                        new Point2D(2, 1), new Point2D(2, 2),
                    }));
                    ret.Add("담울15", MPoT(3, 3));
                    ret.Add("담울16", MPoT(3, 3));
                    ret.Add("담울17", MPoT(3, 3));
                    ret.Add("담울18", MPoT(3, 3));
                    ret.Add("담울19", MPoT(3, 3));

                    ret.Add("돌01", MPoT(2, 2));
                    ret.Add("돌02", MPoT(3, 2));
                    ret.Add("돌03", MPoT(2, 2));
                    ret.Add("돌04", MPoT(2, 2));
                    ret.Add("돌05", MPoT(2, 1));
                    ret.Add("돌06", MPoT(3, 3));
                    ret.Add("돌07", MPoT(4, 2));
                    ret.Add("돌08", MPoT(3, 2));
                    ret.Add("돌09", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0),
                        new Point2D(1, 1), new Point2D(2, 1), new Point2D(3, 1), new Point2D(4, 1),
                        new Point2D(3, 2)
                    }));
                    ret.Add("돌10", MPoT(3, 3));
                    ret.Add("돌11", MPoT(1, 1));
                    ret.Add("돌12", MPoT(2, 1));
                    ret.Add("돌13", MPoT(2, 1));
                    ret.Add("돌14", MPoT(2, 2));
                    ret.Add("돌15", MPoT(1, 1));
                    ret.Add("돌16", MPoT(1, 1));
                    ret.Add("돌17", MPoT(3, 4));
                    ret.Add("돌18", MPoT(2, 5));
                    ret.Add("돌19", MPoT(4, 4));
                    ret.Add("돌20", MPoT(2, 3));
                    ret.Add("돌21", MPoT(2, 2));
                    ret.Add("돌22", MPoT(2, 2));
                    ret.Add("돌23", MPoT(3, 2));

                    ret.Add("아놀드01", MPoT(1, 1));
                    ret.Add("아놀드02", MPoT(1, 1));
                    ret.Add("아놀드03", MPoT(1, 1));
                    ret.Add("아놀드04", MPoT(1, 1));
                    ret.Add("아놀드05", MPoT(1, 1));
                    ret.Add("아놀드06", MPoT(1, 1));
                    ret.Add("아놀드07", MPoT(1, 1));
                    ret.Add("아놀드08", MPoT(1, 1));
                    ret.Add("아놀드09", MPoT(1, 1));
                    ret.Add("아놀드10", MPoT(1, 1));
                    ret.Add("아놀드11", MPoT(1, 1));

                    ret.Add("입구01", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0), new Point2D(4, 0),
                        new Point2D(0, 1), new Point2D(1, 1), new Point2D(2, 1), new Point2D(3, 1), new Point2D(4, 1),
                        new Point2D(0, 2), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(4, 2),
                        new Point2D(1, 3), new Point2D(2, 3), new Point2D(3, 3),
                    }));

                    ret.Add("입구02", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0), new Point2D(3, 0), new Point2D(4, 0),
                        new Point2D(0, 1), new Point2D(1, 1), new Point2D(2, 1), new Point2D(3, 1), new Point2D(4, 1),
                        new Point2D(0, 2), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(4, 2),
                        new Point2D(1, 3), new Point2D(2, 3), new Point2D(3, 3),
                    }));
                    ret.Add("입구03", MPoT(5, 5));

                    ret.Add("입구04", new MapObject(new List<Point2D>() { 
                        new Point2D(0, 0), new Point2D(2, 0), 
                        new Point2D(0, 1), new Point2D(2, 1),
                        new Point2D(0, 2), new Point2D(1, 2), new Point2D(2, 2),
                    }));

                    ret.Add("입구05", MPoT(5, 5));

                    ret.Add("입구06", new MapObject(new List<Point2D>() { 
                        new Point2D(3, 0),
                        new Point2D(0, 1), new Point2D(1, 1), new Point2D(3, 1), new Point2D(4, 1), 
                        new Point2D(0, 2), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(4, 2),
                        new Point2D(0, 3), new Point2D(1, 3), new Point2D(2, 3), new Point2D(3, 3), new Point2D(4, 3),
                        new Point2D(2, 4), new Point2D(3, 4)
                    }));
                    ret.Add("집건물", MPoT(4, 8));
                    ret.Add("집대장간", MPoT(12,7));
                    ret.Add("집시약상", MPoT(5,6));
                    ret.Add("집아놀드", MPoT(8, 4));

                    ret.Add("하검뎅이", MPoT(2,2));
                    ret.Add("하공간", MPoT(1,2));
                    ret.Add("하과일통", MPoT(1, 1));
                    ret.Add("하석상", MPoT(2, 2));
                    ret.Add("하수레", MPoT(3, 3));
                    ret.Add("하통", MPoT(1, 1));
                    break;
                case Map.tilePS:
                    AddWall(ret, "벽", 12);
                    AddWall(ret, "벽기타", 2);
                    ret.Add("침대", MPoT(4, 3));
                    break;
                case Map.tileVV:
                    break;
                case Map.tileWeak:
                    break;
            }
            return ret;
        }

        static void AddWall(Dictionary<string, MapObject> coll, string pref, int amt)
        {
            for (int x = 1; x < amt+1; x++)
            {
                coll.Add(string.Format("{0}{1:00}",pref,x), MPoT(1,3));
            }
        }
    }
}

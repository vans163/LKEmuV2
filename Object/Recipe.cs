using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Recipe
    {
        public string Result;
        public int HitsReq = 10;
        public int Chance = 100;
        public int XPGain;
        public int LevelReq;
        public int Warn = 0;
        public Equipment refine;

        public Recipe(string result, int xpg, int lvlr, int chance = 100, int hits = 10)
        {
            this.Result = result;
            this.Chance = chance;
            this.HitsReq = hits;
            XPGain = xpg;
            this.LevelReq = lvlr;
        }
    }
}
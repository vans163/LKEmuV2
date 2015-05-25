using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Minerals
    {
        public static void Load()
        {
            CreateMineral(20, "Miner0", "IRON", 4, 5, 0, "TOPAZ");
            CreateMineral(15, "Miner0", "COPPER", 3, 6, 2, "SILVER");
            CreateMineral(10, "Miner0", "WAX", 3, 9, 4, "QUARTZ");
            CreateMineral(5, "Miner0", "ALUMINUM", 3, 13, 6, "RUBY");
        }

        static void CreateMineral(int times, string map, string name, int sprid, int xp, int lvl, string gem = "")
        {
            for (int x = 0; x < times; x++)
            {
                var newore = new Object.Mineral(name, name, sprid, xp, lvl);
                if (!string.IsNullOrEmpty(gem))
                    newore.GemDrop = gem;
                World.World.Maps[map].DeadMinerals.TryAdd(newore.objId, newore);
            }
        }
    }
}

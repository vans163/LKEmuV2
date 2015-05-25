using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static partial class Recipe
    {
        public static void Load()
        {
        }

        public static void Smelt(Player.Player plyr, Object.Craft cauldron)
        {
            int XPGained = 0;
            var content = cauldron.Contents.Skip(0).Select(xe => xe.Value).ToList();

            foreach (var itm in content)
            {
                if (itm.StackSize == 0)
                    continue;

                Object.Item result = null;
                int pb = GetPb(itm._Name);

                switch (itm._Name)
                {
                    case "IRON PB":
                    case "IRON PN":
                    case "IRON PG":
                        result = SmeltOre(itm, "IRON BAR", pb, plyr, 0, 5, ref XPGained);
                        break;

                    case "COPPER PB":
                    case "COPPER PN":
                    case "COPPER PG":
                        result = SmeltOre(itm, "COPPER BAR", pb, plyr, 2, 6, ref XPGained);
                        break;

                    case "WAX PB":
                    case "WAX PN":
                    case "WAX PG":
                        result = SmeltOre(itm, "WAX BAR", pb, plyr, 4, 9, ref XPGained);
                        break;

                    case "ALUMINUM PB":
                    case "ALUMINUM PN":
                    case "ALUMINUM PG":
                        result = SmeltOre(itm, "ALUMINUM BAR", pb, plyr, 6, 13, ref XPGained);
                        break;

                    case "COPPER BAR":
                    case "IRON BAR":
                        if (!AssertLevel(plyr, 1, "BRONZE BAR"))
                            return;

                        var copbar = cauldron.Contents.Skip(0).Where(xe=>xe.Value._Name == "COPPER BAR").FirstOrDefault();
                        var ironbar = cauldron.Contents.Skip(0).Where(xe => xe.Value._Name == "IRON BAR").FirstOrDefault();
                        if (copbar.Value != null && ironbar.Value != null && copbar.Value.StackSize > 0 && ironbar.Value.StackSize > 0)
                        {
                            result = SmeltBronze(ironbar.Value, copbar.Value, ref XPGained);
                            if (copbar.Value.StackSize == 0)
                                cauldron.RemoveItem(copbar.Value);
                            if (ironbar.Value.StackSize == 0)
                                cauldron.RemoveItem(ironbar.Value);
                        }

                        break;
                }

                if (result != null)
                {
                    result.Position.X = plyr.Position.X;
                    result.Position.Y = plyr.Position.Y;
                    result.Position.CurMap = plyr.Position.CurMap;
                    plyr.Position.CurMap.Enter(result);
                }

                if (itm is Object.Ore)
                {
                    if (itm.StackSize == 0)
                        cauldron.RemoveItem(itm);
                }
            }
            if (XPGained > 0)
            {
                plyr.State.MinerXP += XPGained;
                plyr.WriteWarn(string.Format("You have gained {0} experience.", XPGained));
            }
        }

        static Object.Item SmeltBronze(Object.Item ibar, Object.Item cbar, ref int XPGained)
        {
            var ironstack = ibar.StackSize;
            var copstack = cbar.StackSize;
            var amt = ironstack <= copstack ? ironstack : copstack;

            ibar.StackSize -= amt;
            cbar.StackSize -= amt;
            XPGained += amt * 5;

            return Scripts.Items.CreateItem("BRONZE BAR", amt);
        }

        static Object.Item SmeltOre(Object.Item ore, string barname, int pb, Player.Player plyr, int lvl, int xp, ref int XPGained)
        {
            if (!AssertLevel(plyr, lvl, barname))
                return null;

            var amt = ore.StackSize / pb;
            if (amt == 0)
                return null;

            ore.StackSize -= amt * pb;
            XPGained += amt * xp;

            return Scripts.Items.CreateItem(barname, amt);
        }

        static int GetPb(string name)
        {
            var spl = name.Split(' ');
            if (spl.Length == 1)
                return 0;
            switch (spl[1])
            {
                case "PB":
                    return 3;
                case "PN":
                    return 2;
                case "PG":
                    return 1;
            }
            return 0;
        }

        static bool AssertLevel(Player.Player plyr, int lvl, string ore)
        {
            if (plyr.State.MinerLevel < lvl)
            {
                plyr.WriteWarn(string.Format("You need to be atleast level {0} to smelt {1}.", lvl, ore));
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static partial class Recipe
    {
        public static Object.Recipe GetSmithRecipe(List<Object.Item> content)
        {
            switch (content.Count)
            {
                case 1:
                    if (HasRecipe(content, BRe("IRON BAR", 3)))
                        return new Object.Recipe("HATCHET", 40, 0);
                    if (HasRecipe(content, BRe("BRONZE BAR", 3)))
                        return new Object.Recipe("CAP", 176, 4);
                    break;
                case 2:
                    if (HasRecipe(content, BRe("FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0)))
                        return new Object.Recipe("RING OF ENERGY", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0)))
                        return new Object.Recipe("RING OF WITCHCRAFT", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("MAGIC DRUG", 0, "MAGIC DRUG", 0)))
                        return new Object.Recipe("RING OF TECHNIQUE", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("LIFE DRUG", 0, "LIFE DRUG", 0)))
                        return new Object.Recipe("RING OF ACTIVITY", 0, 3, 6, 120);

                    if (HasRecipe(content, BRe("IRON BAR", 1, "ALUMINUM BAR", 1)))
                        return new Object.Recipe("BUCKLER", 40, 0);
                    if (HasRecipe(content, BRe("IRON BAR", 1, "COPPER BAR", 2)))
                        return new Object.Recipe("KNIFE", 40, 0);
                    if (HasRecipe(content, BRe("IRON BAR", 1, "SQUARE BAR", 1)))
                        return new Object.Recipe("CLUB", 40, 0);
                    if (HasRecipe(content, BRe("IRON BAR", 2, "WAX BAR", 2)))
                        return new Object.Recipe("SICKLE", 120, 2);
                    if (HasRecipe(content, BRe("COPPER BAR", 1, "RUBBER", 1)))
                        return new Object.Recipe("HOOD", 40, 0);
                    if (HasRecipe(content, BRe("COPPER BAR", 2, "ALUMINUM BAR", 1)))
                        return new Object.Recipe("SUIT", 64, 1);
                    if (HasRecipe(content, BRe("WAX BAR", 1, "RUBBER", 1)))
                        return new Object.Recipe("RAG", 40, 0);
                    if (HasRecipe(content, BRe("SQUARE BAR", 2, "RUBBER", 1)))
                        return new Object.Recipe("BAMBOO KNIFE", 40, 1);
                    if (HasRecipe(content, BRe("BRANCH", 2, "SQUARE BAR", 3)))
                        return new Object.Recipe("WOODEN SWORD", 152, 3);
                    if (HasRecipe(content, BRe("IRON BAR", 2, "WAX BAR", 4)))
                        return new Object.Recipe("SAW", 320, 7);
                    if (HasRecipe(content, BRe("IRON BAR", 4, "COPPER BAR", 2)))
                        return new Object.Recipe("SHORT SWORD", 352, 8);

                    //refines
                    if (HasRefine(content, "TOPAZ", 1))
                        return new Object.Recipe("UPGRADE", 0, 1, 100, 10) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "QUARTZ", 2))
                        return new Object.Recipe("UPGRADE", 0, 10, 90, 15) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "CRYSTAL", 3))
                        return new Object.Recipe("UPGRADE", 0, 20, 80, 20) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "GOLD", 4))
                        return new Object.Recipe("UPGRADE", 0, 30, 70, 25) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "LEOPARD JASPER", 5))
                        return new Object.Recipe("UPGRADE", 0, 40, 60, 30) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "EMERALD", 6))
                        return new Object.Recipe("UPGRADE", 0, 50, 50, 35) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "Z-STONE", 7))
                        return new Object.Recipe("UPGRADE", 0, 70, 40, 40) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "SAPPHIRE", 8))
                        return new Object.Recipe("UPGRADE", 0, 80, 30, 45) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "AMETHYST", 9))
                        return new Object.Recipe("UPGRADE", 0, 90, 20, 50) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "DIAMOND", 10))
                        return new Object.Recipe("UPGRADE", 0, 99, 10, 55) { refine = content[0] as Object.Equipment };

                    if (HasRefine(content, "SILVER", 1))
                        return new Object.Recipe("UPGRADE", 0, 1, 100, 10) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "RUBY", 2))
                        return new Object.Recipe("UPGRADE", 0, 10, 90, 15) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "MOON STONE", 3))
                        return new Object.Recipe("UPGRADE", 0, 20, 80, 20) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "SUN STONE", 4))
                        return new Object.Recipe("UPGRADE", 0, 30, 70, 25) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "BLOOD STONE", 5))
                        return new Object.Recipe("UPGRADE", 0, 40, 60, 30) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "TIGER'S EYE", 6))
                        return new Object.Recipe("UPGRADE", 0, 50, 50, 35) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "HELIODOR", 7))
                        return new Object.Recipe("UPGRADE", 0, 70, 40, 40) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "DARK STONE", 8))
                        return new Object.Recipe("UPGRADE", 0, 80, 30, 45) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "CAT'S EYE", 9))
                        return new Object.Recipe("UPGRADE", 0, 90, 20, 50) { refine = content[0] as Object.Equipment };
                    if (HasRefine(content, "BLACK PEARL", 10))
                        return new Object.Recipe("UPGRADE", 0, 99, 10, 55) { refine = content[0] as Object.Equipment };

                    break;
                case 3:
                    if (HasRecipe(content, BRe("FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("RING OF FORCE", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("RING OF SOUL", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("MAGIC DRUG", 0, "MAGIC DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("RING OF ACCURACY", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("LIFE DRUG", 0, "LIFE DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("RING OF STAMINA", 0, 3, 2, 120);

                    if (HasRecipe(content, BRe("FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0)))
                        return new Object.Recipe("AMULET OF ENERGY", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0)))
                        return new Object.Recipe("AMULET OF WITCHCRAFT", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("LIFE DRUG", 0, "LIFE DRUG", 0, "LIFE DRUG", 0)))
                        return new Object.Recipe("AMULET OF ACTIVITY", 0, 3, 6, 120);
                    if (HasRecipe(content, BRe("MAGIC DRUG", 0, "MAGIC DRUG", 0, "MAGIC DRUG", 0)))
                        return new Object.Recipe("AMULET OF TECHNIQUE", 0, 3, 6, 120);

                    if (HasRecipe(content, BRe("COPPER BAR", 1, "BRONZE BAR", 2, "RUBBER", 2)))
                        return new Object.Recipe("FULL DRESS", 208, 4);
                    if (HasRecipe(content, BRe("COPPER BAR", 1, "WAX BAR", 2, "BRANCH", 1)))
                        return new Object.Recipe("ROUND SHIELD", 96, 2);
                    if (HasRecipe(content, BRe("IRON BAR", 3, "COPPER BAR", 2, "ALUMINUM BAR", 1)))
                        return new Object.Recipe("DAGGER", 232, 5);
                    if (HasRecipe(content, BRe("IRON BAR", 3, "WAX BAR", 1, "BRONZE BAR", 2)))
                        return new Object.Recipe("SMALL AXE", 264, 6);
                    if (HasRecipe(content, BRe("ALUMINUM BAR", 1, "SAW DUST", 3, "RUBBER", 2)))
                        return new Object.Recipe("SURPLICE", 296, 6);
                    if (HasRecipe(content, BRe("WAX BAR", 3, "ALUMINUM BAR", 2, "BRONZE BAR", 2)))
                        return new Object.Recipe("SMALL SHIELD", 376, 8);
                    if (HasRecipe(content, BRe("IRON BAR", 2, "COPPER BAR", 2, "ALUMINUM BAR", 3)))
                        return new Object.Recipe("FLEURET", 432, 10);
                    if (HasRecipe(content, BRe("IRON BAR", 2, "WAX BAR", 1, "BRONZE BAR", 4)))
                        return new Object.Recipe("HEADPIECE", 464, 10);

                    break;
                case 4:
                    if (HasRecipe(content, BRe("FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0, "FULL LIFE DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("AMULET OF FORCE", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0, "FULL MAGIC DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("AMULET OF SOUL", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("LIFE DRUG", 0, "LIFE DRUG", 0, "LIFE DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("AMULET OF STAMINA", 0, 3, 2, 120);
                    if (HasRecipe(content, BRe("MAGIC DRUG", 0, "MAGIC DRUG", 0, "MAGIC DRUG", 0, "COMBINATION DRUG", 0)))
                        return new Object.Recipe("AMULET OF ACCURACY", 0, 3, 2, 120);

                    if (HasRecipe(content, BRe("COPPER BAR", 3, "RUBBER", 2, "PAPER", 1, "SKIN", 1)))
                        return new Object.Recipe("CAPE", 408, 9);
                    break;
            }
            return null;
        }

        static bool HasRefine(List<Object.Item> content, string gem, int grade)
        {
            if (content.Count != 2)
                return false;
            
            if (!(content[0] is Object.Equipment))
                return false;
            var con0 = content[0] as Object.Equipment;
            if (con0.EquipmentGrade == grade - 1 && content[1]._Name == gem)
                return true;

            return false;
        }

        static bool HasRecipe(List<Object.Item> content, List<Tuple<string,int>> reqs)
        {
            if (content.Count != reqs.Count)
                return false;

            for (int x = 0; x < reqs.Count; x++)
            {
                if (content[x]._Name == reqs[x].Item1)
                {
                    if (content[x].StackSize == reqs[x].Item2)
                        continue;
                }
                return false;
            }

            return true;
        }
        
        static List<Tuple<string, int>> BRe(string s, int i)
        {
            var ret = new List<Tuple<string, int>>();
            ret.Add(new Tuple<string, int>(s, i));
            return ret;
        }

        static List<Tuple<string, int>> BRe(string s, int i, string s1, int i1)
        {
            var ret = new List<Tuple<string, int>>();
            ret.Add(new Tuple<string, int>(s, i));
            ret.Add(new Tuple<string, int>(s1, i1));
            return ret;
        }

        static List<Tuple<string, int>> BRe(string s, int i, string s1, int i1, string s2, int i2)
        {
            var ret = new List<Tuple<string, int>>();
            ret.Add(new Tuple<string, int>(s, i));
            ret.Add(new Tuple<string, int>(s1, i1));
            ret.Add(new Tuple<string, int>(s2, i2));
            return ret;
        }

        static List<Tuple<string, int>> BRe(string s, int i, string s1, int i1, string s2, int i2, string s3, int i3)
        {
            var ret = new List<Tuple<string, int>>();
            ret.Add(new Tuple<string, int>(s, i));
            ret.Add(new Tuple<string, int>(s1, i1));
            ret.Add(new Tuple<string, int>(s2, i2));
            ret.Add(new Tuple<string, int>(s3, i3));
            return ret;
        }
    }
}

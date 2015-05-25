using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Monsters
    {
        static Dictionary<string, Object.Monster> Mobs = new Dictionary<string, Object.Monster>();
        public static Object.Monster Create(string name)
        {
            Object.Monster tmob;
            switch (name)
            {
                case "PIGMY":
                    tmob = new Object.Monster("PIGMY", 15, 25, 1, 16, 2, 15)
                    {
                        WalkSpeed = 1250,
                        AggroRange = 5,
                        Race = Object.E_Race.Demon,
                        DropTable = new Dictionary<string, float>()
                    {
                        {"HATCHET", 1},
                        {"HOOD", 2},
                        {"BUCKLER", 3},
                        {"RAG", 3},
                        {"g:1d10+1", 20},
                    }
                    };
                    PotionPack(5, 0.1f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.5f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "PIGMY RED":
                    tmob = new Object.Monster("PIGMY", 15, 40, 4, 37, 2, 24)
                    {
                        WalkSpeed = 1150,
                        AggroRange = 7,
                        Color = Player.E_Color.Red,
                        Race = Object.E_Race.Demon,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"DAGGER", 0.5f},
                                {"SHORT SWORD", 3},
                                {"g:2d10+1", 20},
                            }
                    };
                    PotionPack(7, 0.1f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.5f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.1f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "PIGMY GREEN":
                    tmob = new Object.Monster("PIGMY", 15, 40, 6, 39, 4, 27)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Color = Player.E_Color.Green,
                        Race = Object.E_Race.Demon,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"HATCHET", 0.2f},
                                {"CLUB", 0.2f},
                                {"SICKLE", 0.2f},
                                {"SAW", 0.2f},
                                {"SHORT SWORD", 3},
                                {"g:3d10+1", 20},
                            }
                    };
                    PotionPack(7, 0.1f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.7f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.1f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "PIGMY BLUE":
                    tmob = new Object.Monster("PIGMY", 15, 60, 6, 41, 6, 29)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Color = Player.E_Color.Blue,
                        Race = Object.E_Race.Demon,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"HATCHET", 1f},
                                {"CLUB", 1f},
                                {"SICKLE", 0.5f},
                                {"SUIT", 0.2f},
                                {"SHORT SWORD", 5},
                                {"g:4d10+2", 20},
                            }
                    };
                    PotionPack(7, 0.1f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.7f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    GearPack(0.5f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);

                    return tmob;
                case "SMALL KING":
                    tmob = new Object.Monster("SMALL KING", 16, 300, 7, 44, 30, 44)
                    {
                        WalkSpeed = 650,
                        RespawnTime = 720000,
                        AggroRange = 9,
                        Race = Object.E_Race.Demon,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"FLEURET", 15f},
                                {"EPEE", 15f},
                                {"MACE", 15f},
                                {"HEADGEAR", 15f},
                                {"g:1d100+100", 100},
                            }
                    };
                    PotionPack(70, 0.5f, true).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(2f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    SpellbookPack(1f, 2).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    tmob.DropTable["BOOK OF THUNDER CROSS"] = 5f;
                    tmob.DropTable["BOOK OF FLAME ROUND"] = 5f;
                    GearPack(5f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "ZOMBIE":
                    tmob = new Object.Monster("ZOMBIE", 3, 50, 11, 30, 8, 33)
                    {
                        WalkSpeed = 1550,
                        AggroRange = 6,
                        Race = Object.E_Race.Undead,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"SUIT", 0.1f},
                                {"SMALL AXE", 1f},
                                {"CAP", 0.5f},
                                {"SMALL SHIELD", 0.5f},
                                {"g:3d10+15", 25},
                            }
                    };
                    PotionPack(3, 0.2f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    GearPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                case "SKELETON":
                    tmob = new Object.Monster("SKELETON", 1, 100, 5, 30, 15, 32)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Race = Object.E_Race.Undead,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"SMALL AXE", 0.5f},
                                {"CAP", 0.5f},
                                {"SMALL SHIELD", 0.5f},
                                {"g:4d10+15", 25},
                            }
                    };
                    PotionPack(4, 0.2f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    GearPack(0.3f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                case "SKELETON RED":
                    tmob = new Object.Monster("SKELETON", 1, 120, 10, 30, 22, 42)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Race = Object.E_Race.Undead,
                        Color = Player.E_Color.Red,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"SABER", 0.5f},
                                {"RAPIER", 0.5f},
                                {"SMALL AXE", 1f},
                                {"CAP", 0.5f},
                                {"EPEE", 0.1f},
                                {"ROUND SHIELD", 0.5f},
                                {"g:5d10+15", 25},
                            }
                    };
                    PotionPack(4, 0.2f).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    SpellbookPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    SpellbookPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                case "SKELETON GREEN":
                    tmob = new Object.Monster("SKELETON", 1, 120, 12, 38, 29, 78)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Race = Object.E_Race.Undead,
                        Color = Player.E_Color.Green,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"SABER", 0.5f},
                                {"RAPIER", 0.5f},
                                {"SMALL AXE", 1f},
                                {"CAP", 0.5f},
                                {"EPEE", 0.1f},
                                {"ROUND SHIELD", 0.5f},
                                {"g:6d10+15", 25},
                            }
                    };
                    PotionPack(4, 0.2f).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    SpellbookPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    SpellbookPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                case "SKELETON BLUE":
                    tmob = new Object.Monster("SKELETON", 1, 130, 12, 44, 31, 107)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Race = Object.E_Race.Undead,
                        Color = Player.E_Color.Blue,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"CAP", 0.5f},
                                {"EPEE", 0.1f},
                                {"ROUND SHIELD", 0.5f},
                                {"g:7d10+15", 25},
                            }
                    };
                    PotionPack(5, 0.3f).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.2f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "LARGE SKEL":
                    tmob = new Object.Monster("LARGE SKEL", 2, 170, 15, 51, 44, 251)
                    {
                        WalkSpeed = 1050,
                        AggroRange = 8,
                        Race = Object.E_Race.Undead,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"CAP", 0.5f},
                                {"EPEE", 0.1f},
                                {"SHORT STAFF", 0.6f},
                                {"SHORT SPEAR", 0.6f},
                                {"g:9d10+20", 25},
                            }
                    };
                    PotionPack(10, 0.5f, true).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    return tmob;
                case "GENERAL SKEL":
                    tmob = new Object.Monster("GENERAL SKEL", 25, 350, 33, 67, 59, 1408)
                    {
                        WalkSpeed = 950,
                        AggroRange = 9,
                        Race = Object.E_Race.Undead,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"CAP", 5f},
                                {"EPEE", 5f},
                                {"CAPE", 2f},
                                {"SHORT STAFF", 6f},
                                {"SHORT SPEAR", 6f},
                                {"g:3d100+100", 25},
                                
                            }
                    };
                    PotionPack(70, 1f, true).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(15f, 1).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(15f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(3f, 3).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                    /*
                                        new LootPackEntry(0.2, typeof(script.item.FireShotBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.2, typeof(script.item.AssassinBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.LongStaff), "15d10+225", 1, 1, 1),
                    new LootPackEntry(0.5, typeof(script.item.Harpoon), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.0, typeof(script.item.DemonDeathBook), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.5, typeof(script.item.ToughLeather), "15d10+225", 1, 1, 1),
                    new LootPackEntry(1.5, typeof(script.item.LargeShield), "15d10+225", 1, 1, 1),
                    
                    new LootPackEntry(15.0, typeof(script.item.Gold), "5d10+80", 40, 1, 1),*/
                case "STONE":
                    tmob = new Object.Monster("STONE", 6, 350, 69, 100, 83, 681)
                    {
                        WalkSpeed = 1950,
                        AggroRange = 8,
                        Race = Object.E_Race.Magical,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"g:9d10+50", 25},
                            }
                    };
                    PotionPack(10, 0.5f, true).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 3).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    GearPack(0.2f, 2).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
                case "STONE RED":
                    tmob = new Object.Monster("STONE", 6, 430, 73, 105, 89, 744)
                    {
                        WalkSpeed = 1950,
                        AggroRange = 8,
                        Color = Player.E_Color.Red,
                        Race = Object.E_Race.Magical,
                        DropTable = new Dictionary<string, float>()
                            {
                                {"g:9d10+50", 35},
                            }
                    };
                    PotionPack(15, 0.5f, true).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.3f, 2).ToList().ForEach(x => tmob.DropTable.Add(x.Key, x.Value));
                    SpellbookPack(0.4f, 3).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    GearPack(0.3f, 2).ToList().ForEach(x => tmob.DropTable[x.Key] = x.Value);
                    return tmob;
            }
            return null;
        }

        public static void Load()
        {
            SpawnMonster("PIGMY", 2, "St. Andover", new Point2D(63, 120), new Point2D(71, 130));

            SpawnMonster("PIGMY", 25, "Weakly1");
            SpawnMonster("PIGMY RED", 3, "Weakly1");

            SpawnMonster("PIGMY", 8, "Weakly2");
            SpawnMonster("PIGMY RED", 16, "Weakly2");
            SpawnMonster("PIGMY GREEN", 8, "Weakly2");

            SpawnMonster("PIGMY RED", 10, "Weakly3");
            SpawnMonster("PIGMY GREEN", 10, "Weakly3");
            SpawnMonster("PIGMY BLUE", 15, "Weakly3");
            SpawnMonster("SMALL KING", 1, "Weakly3");


            SpawnMonster("ZOMBIE", 25, "Skel1");
            SpawnMonster("SKELETON", 5, "Skel1");

            SpawnMonster("SKELETON", 15, "Skel2");
            SpawnMonster("SKELETON RED", 12, "Skel2");
            SpawnMonster("SKELETON GREEN", 3, "Skel2");

            SpawnMonster("SKELETON BLUE", 7, "Skel3");
            SpawnMonster("SKELETON GREEN", 8, "Skel3");
            SpawnMonster("LARGE SKEL", 9, "Skel3");
            SpawnMonster("GENERAL SKEL", 1, "Skel3");

            SpawnMonster("STONE", 15, "Skel4");
            SpawnMonster("STONE RED", 10, "Skel4");
        }

        static Dictionary<string, float> PotionPack(float chance, float chancec, bool full = false)
        {
            Dictionary<string, float> ret = new Dictionary<string, float>();
            if (full)
            {
                if (chance > 0)
                {
                    ret.Add("FULL MAGIC DRUG", chance);
                    ret.Add("FULL LIFE DRUG", chance);
                }
                if (chancec > 0)
                ret.Add("FULL COMBINATION DRUG", chancec);
            }
            else
            {
                if (chance > 0)
                {
                    ret.Add("MAGIC DRUG", chance);
                    ret.Add("LIFE DRUG", chance);
                }
                if (chancec > 0)
                ret.Add("COMBINATION DRUG", chancec);
            }
            return ret;
        }

        static Dictionary<string, float> GearPack(float chance, int type)
        {
            Dictionary<string, float> ret = new Dictionary<string, float>();
            switch (type)
            {
                case 1:
                    ret.Add("HATCHET", chance);
                    ret.Add("CLUB", chance);
                    ret.Add("SICKLE", chance);
                    ret.Add("SAW", chance);
                    ret.Add("SHORT SWORD", chance);
                    ret.Add("SMALL SHIELD", chance);
                    ret.Add("RAG", chance);
                    ret.Add("HOOD", chance);
                    ret.Add("WOODEN SWORD", chance);
                    ret.Add("BAMBOO KNIFE", chance);
                    ret.Add("ROD", chance);
                    ret.Add("BAMBOO SPEAR", chance);
                    break;
                case 2:
                    ret.Add("TOUGH LEATHER", chance);
                    ret.Add("LARGE SHIELD", chance);
                    break;
            }
            return ret;
        }

        static Dictionary<string, float> SpellbookPack(float chance, int type)
        {
            Dictionary<string, float> ret = new Dictionary<string, float>();
            switch (type)
            {
                case 1:
                    ret.Add("BOOK OF VIEW", chance);
                    ret.Add("BOOK OF ELECTRONIC BALL", chance);
                    ret.Add("BOOK OF FIRE BALL", chance);
                    ret.Add("BOOK OF MOON LIGHT", chance);
                    ret.Add("BOOK OF FLAME WAVE", chance);
                    ret.Add("BOOK OF FLAME ROUND", chance / 2);
                    break;
                case 2:
                    ret.Add("BOOK OF LIGHTNING", chance);
                    ret.Add("BOOK OF SHOOTING STAR", chance);
                    ret.Add("BOOK OF MORNING STAR", chance);
                    ret.Add("BOOK OF THUNDER CROSS", chance / 2);
                    break;
                case 3:
                    ret.Add("BOOK OF GHOST HUNTER", chance);
                    ret.Add("BOOK OF SORCERER HUNTER", chance);
                    ret.Add("BOOK OF THUNDER BOLT", chance);
                    ret.Add("BOOK OF THUNDER CROSS", chance);
                    break;

            }
            return ret;
        }
        static void SpawnMonster(string mob, int times, string map, Point2D baa = null, Point2D bbb = null)
        {
            for (int x = 0; x < times; x++)
            {
                Object.Monster nmob = Create(mob);
                if (baa != null && bbb != null)
                {
                    nmob.spwnBndAA = baa;
                    nmob.spwnBndBB = bbb;
                }
                World.World.Maps[map].DeadMonsters.TryAdd(nmob.objId, nmob);
            }
        }
    }
}

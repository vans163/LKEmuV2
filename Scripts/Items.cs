using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKCamelotV2.Object;
using LKCamelotV2.Player;

namespace LKCamelotV2.Scripts
{
    public static class Items
    {
        public static Dictionary<string, Item> GItems = new Dictionary<string, Item>();

        public static Object.Item CreateItem(string name, int stacksize = 1)
        {
            switch (name)
            {
                case "BUCKLER":
                    return new Equipment(E_ET.Shield, name, 17) { AC = 5, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "ROUND SHIELD":
                    return new Equipment(E_ET.Shield, name, 17) { AC = 8, StrReq = 15, BuyPrice = 2000, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "SMALL SHIELD":
                    return new Equipment(E_ET.Shield, name, 17) { AC = 11, StrReq = 22, BuyPrice = 3000, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "SQUARE SHIELD":
                    return new Equipment(E_ET.Shield, name, 17) { AC = 15, StrReq = 93, BuyPrice = 4000, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "TRIANGLE SHIELD":
                    return new Equipment(E_ET.Shield, name, 17) { AC = 26, StrReq = 151, DexReq = 45, BuyPrice = 4500, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "LARGE SHIELD":
                    return new Equipment(E_ET.Shield, name, 18) { AC = 38, StrReq = 209, BuyPrice = 5000, ClassReq = (int)E_Class.Beginner | (int)E_Class.Knight };
                case "TOWER SHIELD":
                    return new Equipment(E_ET.Shield, name, 18) { AC = 50, StrReq = 267, BuyPrice = 7000, ClassReq = (int)E_Class.Knight };
                case "COATING SHIELD":
                    return new Equipment(E_ET.Shield, name, 18) { AC = 59, StrReq = 325, DexReq = 92, BuyPrice = 11000, ClassReq = (int)E_Class.Knight }; 
                case "BATTLE SHIELD":
                    return new Equipment(E_ET.Shield, name, 18) { AC = 68, StrReq = 384, BuyPrice = 12000, ClassReq = (int)E_Class.Knight }; 
                case "ARM SHIELD":
                    return new Equipment(E_ET.Shield, name, 19) { AC = 84, StrReq = 442, DexReq = 125, BuyPrice = 25000, ClassReq = (int)E_Class.Knight };
                case "FAMILY MARK SHIELD":
                    return new Equipment(E_ET.Shield, name, 19) { AC = 94, StrReq = 501, DexReq = 125, BuyPrice = 30000, ClassReq = (int)E_Class.Knight };
                case "GRAND SHIELD":
                    return new Equipment(E_ET.Shield, name, 19) { AC = 95, StrReq = 560, BuyPrice = 32000, ClassReq = (int)E_Class.Knight };
                case "KITE SHIELD":
                    return new Equipment(E_ET.Shield, name, 19) { AC = 101, StrReq = 560, BuyPrice = 35000, ClassReq = (int)E_Class.Knight };
                case "HOLY DEFENDER":
                    return new Equipment(E_ET.Shield, name, 183) { AC = 129, StrReq = 1100, DexReq = 765, BuyPrice = 100000, ClassReq = (int)E_Class.Knight };



                case "HOOD":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 4, BuyPrice = 1500, ClassReq = (int)E_Class.Any };
                case "CAP":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 7, BuyPrice = 2000, ClassReq = (int)E_Class.Any };
                case "HEADPIECE":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 10, StrReq = 60, DexReq = 18, BuyPrice = 3000, ClassReq = (int)E_Class.Any };
                case "MASK":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 15, StrReq = 101, BuyPrice = 5000, ClassReq = (int)E_Class.Any };
                case "BAMBOO HAT":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 19, StrReq = 121, BuyPrice = 5000, ClassReq = (int)E_Class.Shaman };
                case "HEADGEAR":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 21, StrReq = 143, BuyPrice = 6000, ClassReq = (int)E_Class.Any };
                case "HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 27, StrReq = 185, BuyPrice = 6500, ClassReq = (int)E_Class.Any };
                case "HORNED HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 29, StrReq = 206, BuyPrice = 7000, ClassReq = (int)E_Class.Any };
                case "CROWN":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 33, StrReq = 227, BuyPrice = 7500, ClassReq = (int)E_Class.Knight | (int)E_Class.Wizard };
                case "FIELD CAP":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 41, StrReq = 322, DexReq = 92, BuyPrice = 10000, ClassReq = (int)E_Class.Knight | (int)E_Class.Swordsman };
                case "FULL HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 49, StrReq = 417, DexReq = 120, BuyPrice = 11000, ClassReq = (int)E_Class.Knight | (int)E_Class.Swordsman };
                case "GRANDEUR HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 57, StrReq = 513, DexReq = 146, BuyPrice = 13000, ClassReq = (int)E_Class.Knight };
                case "VIKING HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 63, StrReq = 650, DexReq = 200, BuyPrice = 15000, ClassReq = (int)E_Class.Any };

                case "SHINE HELMET":
                    return new Equipment(E_ET.Helmet, name, 4) { AC = 61, StrReq = 274, BuyPrice = 25000, ClassReq = (int)E_Class.Wizard };
                case "PIG BASKET":
                    return new Equipment(E_ET.Helmet, name, 44) { AC = 70, StrReq = 438, DexReq = 312, MenReq = 308, BuyPrice = 80000, ClassReq = (int)E_Class.Shaman };
                case "LAUREL CROWN":
                    return new Equipment(E_ET.Helmet, name, 47) { AC = 90, StrReq = 325, DexReq = 725, MenReq = 1025, BuyPrice = 200000, ClassReq = (int)E_Class.Wizard };
                case "GLADIATOR":
                    return new Equipment(E_ET.Helmet, name, 180) { AC = 80, StrReq = 765, DexReq = 612, LevelReq = 90, BuyPrice = 150000, ClassReq = (int)E_Class.Any };
                case "GRANDEUR PRIDE":
                    return new Equipment(E_ET.Helmet, name, 180) { AC = 100, StrReq = 1025, DexReq = 765, LevelReq = 100, BuyPrice = 200000, ClassReq = (int)E_Class.Knight | (int)E_Class.Swordsman };
                case "UNICORN PROTECTORIA":
                    return new Equipment(E_ET.Helmet, name, 45) { AC = 100, StrReq = 450, DexReq = 5000, MenReq = 7500, LevelReq = 220, BuyPrice = 800000, ClassReq = (int)E_Class.Wizard | (int)E_Class.Shaman };


                case "KNIFE":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 3, ClassReq = (int)E_Class.KnightSwords | (int)E_Class.Beginner };
                case "BAMBOO KNIFE":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 4, BuyPrice = 100, ClassReq = (int)E_Class.KnightSwords | (int)E_Class.Beginner };
                case "WOODEN SWORD":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 5, BuyPrice = 150, ClassReq = (int)E_Class.KnightSwords | (int)E_Class.Beginner };
                case "DAGGER":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 8, BuyPrice = 200, ClassReq = (int)E_Class.KnightSwords | (int)E_Class.Beginner };
                case "SHORT SWORD":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 11, BuyPrice = 250, ClassReq = (int)E_Class.KnightSwords | (int)E_Class.Beginner };
                case "FLEURET":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 15, StrReq = 55, DexReq = 20, BuyPrice = 2000, ClassReq = (int)E_Class.KnightSwords };
                case "RAPIER":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 16, StrReq = 64, DexReq = 22, BuyPrice = 1500, ClassReq = (int)E_Class.KnightSwords };
                case "SABER":
                    return new Equipment(E_ET.Sword, name, 10) { Dam = 19, StrReq = 83, DexReq = 26, BuyPrice = 1500, ClassReq = (int)E_Class.KnightSwords };
                case "EPEE":
                    return new Equipment(E_ET.Sword, name, 11) { Dam = 26, StrReq = 103, DexReq = 33, BuyPrice = 3000, ClassReq = (int)E_Class.KnightSwords };

                case "HATCHET":
                    return new Equipment(E_ET.Axe, name, 14) { Dam = 13, LevelReq = 5, StrReq = 50, ClassReq = (int)E_Class.KnightSwords };
                case "SMALL AXE":
                    return new Equipment(E_ET.Axe, name, 14) { Dam = 18, StrReq = 74, LevelReq = 7, BuyPrice = 3000, ClassReq = (int)E_Class.KnightSwords };

                case "ROD":
                    return new Equipment(E_ET.Cane, name, 32) { Dam = 21, AC = 12, StrReq = 18, MenReq = 51, Hit = 8, MP = 26, BuyPrice = 1500, ClassReq = (int)E_Class.Wizard };
                case "SHORT STAFF":
                    return new Equipment(E_ET.Cane, name, 32) { Dam = 34, AC = 24, StrReq = 24, MenReq = 92, Hit = 25, MP = 48, BuyPrice = 15000, ClassReq = (int)E_Class.Wizard };

                case "BAMBOO SPEAR":
                    return new Equipment(E_ET.Spear, name, 34) { Dam = 25, AC = 10, StrReq = 21, MenReq = 42, Hit = 5, MP = 24, BuyPrice = 1500, ClassReq = (int)E_Class.Shaman };
                case "SHORT SPEAR":
                    return new Equipment(E_ET.Spear, name, 34) { Dam = 46, AC = 24, StrReq = 35, MenReq = 68, Hit = 16, MP = 33, BuyPrice = 15000, ClassReq = (int)E_Class.Shaman };



                case "MINING SUIT":
                    return new Equipment(E_ET.Chest, name, 5) { BuyPrice = 5000, ArmorStage = -1, ClassReq = (int)E_Class.Any };
                case "BLACKSMITH SUIT":
                    return new Equipment(E_ET.Chest, name, 5) { BuyPrice = 15000, ArmorStage = -2, ClassReq = (int)E_Class.Any };

                case "RAG":
                    return new Equipment(E_ET.Chest, name, 5) { AC = 8, BuyPrice = 500,  ArmorStage = 0, ClassReq = (int)E_Class.Any };
                case "SUIT":
                    return new Equipment(E_ET.Chest, name, 5) { AC = 13, ArmorStage = 1, StrReq = 15, BuyPrice = 5000, ClassReq = (int)E_Class.Any };
                case "FULL DRESS":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 19, ArmorStage = 1, StrReq = 18, BuyPrice = 4000, ClassReq = (int)E_Class.Any };
                case "SURPLICE":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 22, ArmorStage = 1, StrReq = 24, BuyPrice = 7000, ClassReq = (int)E_Class.Any };
                case "CAPE":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 30, ArmorStage = 1, StrReq = 93, BuyPrice = 9500, ClassReq = (int)E_Class.Any };
                case "MANTLE":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 39, ArmorStage = 1, ArmorStageMage = 1, StrReq = 118, BuyPrice = 10500, ClassReq = (int)E_Class.Any };
                case "CLOAK":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 52, ArmorStage = 1, ArmorStageMage = 1, StrReq = 143, DexReq = 42, BuyPrice = 10500, ClassReq = (int)E_Class.Any };
                case "COPE":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 57, ArmorStage = 1, ArmorStageMage = 1, StrReq = 169, BuyPrice = 11500, ClassReq = (int)E_Class.Any };
                case "QUILTED ARMOR":
                    return new Equipment(E_ET.Chest, name, 5) { AC = 66, ArmorStage = 1, ArmorStageMage = 2, StrReq = 194, DexReq = 56, BuyPrice = 11500, ClassReq = (int)E_Class.Any };
                case "ROBE":
                    return new Equipment(E_ET.Chest, name, 5) { AC = 82, ArmorStage = 1, ArmorStageMage = 1, StrReq = 220, BuyPrice = 13000, ClassReq = (int)E_Class.Any };
                case "LEATHER ARMOR":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 83, ArmorStage = 1, ArmorStageMage = 2, StrReq = 251, BuyPrice = 13500, ClassReq = (int)E_Class.Any };
                case "HARD LEATHER":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 92, ArmorStage = 1, ArmorStageMage = 2, StrReq = 282, BuyPrice = 14500, ClassReq = (int)E_Class.Any };
                case "ENAMEL LEATHER":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 101, ArmorStage = 1, ArmorStageMage = 2, StrReq = 313, DexReq = 90, BuyPrice = 15000, ClassReq = (int)E_Class.Any };
                case "TOUGH LEATHER":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 117, ArmorStage = 1, ArmorStageMage = 2, StrReq = 345, BuyPrice = 150000, ClassReq = (int)E_Class.Any };
                case "CHEST GUARD":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 119, ArmorStage = 2, ArmorStageMage = 1, StrReq = 373, DexReq = 107, BuyPrice = 130000, ClassReq = (int)E_Class.Any };
                case "BATTLE ARMOR":
                    return new Equipment(E_ET.Chest, name, 6) { AC = 128, ArmorStage = 2, ArmorStageMage = 1, StrReq = 401, BuyPrice = 17000, ClassReq = (int)E_Class.Any };
                case "RING MAIL":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 146, ArmorStage = 2, ArmorStageMage = 1, StrReq = 457, BuyPrice = 19000, ClassReq = (int)E_Class.Any };
                case "BONE MAIL":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 150, ArmorStage = 2, ArmorStageMage = 1, StrReq = 429, BuyPrice = 19000, ClassReq = (int)E_Class.Any };
                case "CHAIN MAIL":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 155, ArmorStage = 2, ArmorStageMage = 1, StrReq = 485, BuyPrice = 17000, ClassReq = (int)E_Class.Any };
                case "SCALE MAIL":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 165, ArmorStage = 3, ArmorStageMage = 2, StrReq = 513, DexReq = 146, BuyPrice = 19000, ClassReq = (int)E_Class.Any };
                case "BREAST PLATE":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 177, ArmorStage = 3, ArmorStageMage = 2, StrReq = 533, BuyPrice = 24000, ClassReq = (int)E_Class.Any };
                case "MAIL PLATE":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 190, ArmorStage = 3, ArmorStageMage = 2, StrReq = 593, BuyPrice = 27000, ClassReq = (int)E_Class.Any };

                case "SAMURAI PLATE":
                    return new Equipment(E_ET.Chest, name, 7) { AC = 202, StrReq = 633, ArmorStage = 3, ArmorStageMage = 2, BuyPrice = 55000, ClassReq = (int)E_Class.Any };
                case "WIDE PLATE":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 215, StrReq = 673, DexReq = 190, ArmorStage = 3, ArmorStageMage = 2, BuyPrice = 40000, ClassReq = (int)E_Class.Any };
                case "FULL PLATE":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 228, StrReq = 714, ArmorStage = 3, ArmorStageMage = 2, BuyPrice = 70000, ClassReq = (int)E_Class.Any };

                case "GOLD PLATE":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 300, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 100, BuyPrice = 100000, ClassReq = (int)E_Class.Knight };
                case "DARK PLATE":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 300, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 100, BuyPrice = 100000, ClassReq = (int)E_Class.Swordsman };
                case "PROTECTORIA":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 120, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 100, BuyPrice = 100000, ClassReq = (int)E_Class.Wizard };
                case "PYTHON SCALE":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 120, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 100, BuyPrice = 100000, ClassReq = (int)E_Class.Shaman };

                case "GOLIATH PLATE":
                    return new Equipment(E_ET.Chest, name, 182) { AC = 350, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 110, BuyPrice = 130000, ClassReq = (int)E_Class.KnightSwords };
                case "DIAMOND ARMOR":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 200, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 110, BuyPrice = 130000, ClassReq = (int)E_Class.WizardSham };
                case "CELESTIAL ARMOR":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 165, Dex = 500, Men = 700, DexReq = 2500, MenReq = 5000, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 200, BuyPrice = 800000, ClassReq = (int)E_Class.WizardSham };
                case "ANGELIC ARMOR":
                    return new Equipment(E_ET.Chest, name, 8) { AC = 190, Dex = 1000, Men = 1500, DexReq = 3500, MenReq = 7500, ArmorStage = 4, ArmorStageMage = 3, LevelReq = 220, BuyPrice = 1900000, ClassReq = (int)E_Class.WizardSham };
                case "AMITY PLATE":
                    return new Equipment(E_ET.Chest, name, 184) { AC = 400, ArmorStage = 4, ArmorStageMage = 3, BuyPrice = 900000, ClassReq = (int)E_Class.Any };
                case "GOLD VEST":
                    return new Equipment(E_ET.Chest, name, 185) { AC = 800, ArmorStage = 4, ArmorStageMage = 3,LevelReq = 160, BuyPrice = 2000000, ClassReq = (int)E_Class.Beginner };

                case "CLUB":
                    return new Equipment(E_ET.Hammer, name, 14) { Dam = 7, StrReq = 18, BuyPrice = 150, ClassReq = (int)E_Class.KnightSwords };
                case "SICKLE":
                    return new Equipment(E_ET.Hammer, name, 14) { Dam = 9, StrReq = 30, BuyPrice = 200, ClassReq = (int)E_Class.Knight };
                case "SAW":
                    return new Equipment(E_ET.Hammer, name, 35) { Dam = 12, StrReq = 40, BuyPrice = 250, ClassReq = (int)E_Class.KnightSwords };
                case "SPIKED CLUB":
                    return new Equipment(E_ET.Hammer, name, 35) { Dam = 21, StrReq = 93, DexReq = 30, BuyPrice = 1000, ClassReq = (int)E_Class.KnightSwords };
                case "MORNING STAR":
                    return new Equipment(E_ET.Hammer, name, 35) { Dam = 35, StrReq = 131, DexReq = 39, BuyPrice = 1500, ClassReq = (int)E_Class.KnightSwords };
                case "MACE":
                    return new Equipment(E_ET.Hammer, name, 35) { Dam = 37, StrReq = 170, DexReq = 50, BuyPrice = 1900, ClassReq = (int)E_Class.KnightSwords };


                #region Jewelery

                case "S-RING":
                    return new Equipment(E_ET.Ring, name, 1) { AC = 150, HP = 250, BuyPrice = 250000 };
                case "S-AMULET":
                    return new Equipment(E_ET.Amulet, name, 2) { Dam = 150, MP = 250, Hit = 150, BuyPrice = 250000 };

                case "RING OF ENERGY":
                    return new Equipment(E_ET.Ring, name, 1) { Str = Dice.RandomMinMax(16, 20) };
                case "AMULET OF ENERGY":
                    return new Equipment(E_ET.Amulet, name, 2) { Str = Dice.RandomMinMax(16, 20) };
                case "RING OF FORCE":
                    return new Equipment(E_ET.Ring, name, 1) { Str = Dice.RandomMinMax(21, 25) };
                case "AMULET OF FORCE":
                    return new Equipment(E_ET.Amulet, name, 2) { Str = Dice.RandomMinMax(21, 25) };

                case "RING OF WITCHCRAFT":
                    return new Equipment(E_ET.Ring, name, 1) { Men = Dice.RandomMinMax(16, 20) };
                case "AMULET OF WITCHCRAFT":
                    return new Equipment(E_ET.Amulet, name, 2) { Men = Dice.RandomMinMax(16, 20) };
                case "RING OF SOUL":
                    return new Equipment(E_ET.Ring, name, 1) { Men = Dice.RandomMinMax(21, 25) };
                case "AMULET OF SOUL":
                    return new Equipment(E_ET.Amulet, name, 2) { Men = Dice.RandomMinMax(21, 25) };

                case "RING OF TECHNIQUE":
                    return new Equipment(E_ET.Ring, name, 1) { Dex = Dice.RandomMinMax(16, 20) };
                case "AMULET OF TECHNIQUE":
                    return new Equipment(E_ET.Amulet, name, 2) { Dex = Dice.RandomMinMax(16, 20) };
                case "RING OF ACCURACY":
                    return new Equipment(E_ET.Ring, name, 1) { Dex = Dice.RandomMinMax(21, 25) };
                case "AMULET OF ACCURACY":
                    return new Equipment(E_ET.Amulet, name, 2) { Dex = Dice.RandomMinMax(21, 25) };

                case "RING OF ACTIVITY":
                    return new Equipment(E_ET.Ring, name, 1) { Vit = Dice.RandomMinMax(16, 20) };
                case "AMULET OF ACTIVITY":
                    return new Equipment(E_ET.Amulet, name, 2) { Vit = Dice.RandomMinMax(16, 20) };
                case "RING OF STAMINA":
                    return new Equipment(E_ET.Ring, name, 1) { Vit = Dice.RandomMinMax(21, 25) };
                case "AMULET OF STAMINA":
                    return new Equipment(E_ET.Amulet, name, 2) { Vit = Dice.RandomMinMax(21, 25) };
                #endregion

                #region Craft
                case "CAULDRON":
                    return new Item(name, 37) { BuyPrice = 5000 };
                case "ANVIL":
                    return new Item(name, 38) { BuyPrice = 15000 };

                case "MATCH":
                    return new Item(name, 131) { StackSize = stacksize } ;
                case "WOOD":
                    return new Item(name, 115) { StackSize = stacksize };
                case "SHELL":
                    return new Item(name, 127) { StackSize = stacksize };
                case "RUBBER":
                    return new Item(name, 111) { StackSize = stacksize };
                case "PAPER":
                    return new Item(name, 116) { StackSize = stacksize };
                case "LEATHER":
                    return new Item(name, 71) { StackSize = stacksize };
                case "SAW DUST":
                    return new Item(name, 117) { StackSize = stacksize };
                case "GRANITE":
                    return new Item(name, 130) { StackSize = stacksize };


                case "IRON PB":
                case "IRON PN":
                case "IRON PG":
                    return new Ore(name, 90) { StackSize = stacksize };
                case "COPPER PB":
                case "COPPER PN":
                case "COPPER PG":
                    return new Ore(name, 90) { StackSize = stacksize };
                case "WAX PB":
                case "WAX PN":
                case "WAX PG":
                    return new Ore(name, 93) { StackSize = stacksize };
                case "ALUMINUM PB":
                case "ALUMINUM PN":
                case "ALUMINUM PG":
                    return new Ore(name, 90) { StackSize = stacksize };

                case "IRON BAR":
                    return new Item(name, 51) { StackSize = stacksize };
                case "SQUARE BAR":
                    return new Item(name, 110) { StackSize = stacksize };
                case "BRONZE BAR":
                    return new Item(name, 79) { StackSize = stacksize };
                case "COPPER BAR":
                    return new Item(name, 79) { StackSize = stacksize };
                case "WAX BAR":
                    return new Item(name, 51) { StackSize = stacksize };
                case "ALUMINUM BAR":
                    return new Item(name, 51) { StackSize = stacksize };

                //Armor Refine
                case "TOPAZ":
                    return new Item(name, 53) { StackSize = stacksize };
                case "QUARTZ":
                    return new Item(name, 76) { StackSize = stacksize };

                //Weapon Refine
                case "SILVER":
                    return new Item(name, 62) { StackSize = stacksize };
                case "RUBY":
                    return new Item(name, 58) { StackSize = stacksize };

                #endregion
                #region Drugs
                case "LIFE DRUG":
                    return new Consumable(name, 20) { BuyPrice = 250, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.4f); } };
                case "MAGIC DRUG":
                    return new Consumable(name, 21) { BuyPrice = 250, _Consume = delegate(Player.Player pl) { pl.MPCur += (int)(pl.MP * 0.4f); } };
                case "FULL LIFE DRUG":
                    return new Consumable(name, 22) { BuyPrice = 500, _Consume = delegate(Player.Player pl) { pl.HPCur += pl.HP; } };
                case "FULL MAGIC DRUG":
                    return new Consumable(name, 23) { BuyPrice = 500, _Consume = delegate(Player.Player pl) { pl.MPCur += pl.MP; } };
                case "COMBINATION DRUG":
                    return new Consumable(name, 24) { BuyPrice = 1250, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } };
                case "FULL COMBINATION DRUG":
                    return new Consumable(name, 25) { BuyPrice = 2500, _Consume = delegate(Player.Player pl) { pl.HPCur += pl.HP; pl.MPCur += pl.MP; } };
                #endregion
                #region Spellbook
                case "BOOK OF TELEPORT":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 60, MenReqPl = 16, BuyPrice = 25000, DexReq = 92, DexReqPl = 4 };
                case "BOOK OF VIEW":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 15, MenReqPl = 5, BuyPrice = 1000 };
                case "BOOK OF TRANSPARENCY":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Swordsman | (int)E_Class.Wizard) { MenReq = 120, MenReqPl = 10, BuyPrice = 20000, LevelReq = 50 };
                case "BOOK OF ELECTRONIC TUBE":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Shaman | (int)E_Class.Wizard) { MenReq = 482, MenReqPl = 13, BuyPrice = 40000 };


                case "BOOK OF FLAME ROUND":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 38, MenReqPl = 9, BuyPrice = 5000 };
                case "BOOK OF THUNDER CROSS":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 53, MenReqPl = 13, BuyPrice = 7000 };
                case "BOOK OF ZIG ZAG":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 17, MenReqPl = 3, BuyPrice = 800 };
                case "BOOK OF ELECTRONIC BALL":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 24, MenReqPl = 4, BuyPrice = 1000 };
                case "BOOK OF FIRE BALL":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 28, MenReqPl = 7, BuyPrice = 1000 };
                case "BOOK OF FLAME WAVE":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 30, MenReqPl = 7, BuyPrice = 1500 };
                case "BOOK OF LIGHTNING":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 38, MenReqPl = 8, BuyPrice = 2000 };
                case "BOOK OF MOON LIGHT":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 25, MenReqPl = 5, BuyPrice = 1500 };
                case "BOOK OF SHOOTING STAR":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Knight | (int)E_Class.Wizard) { MenReq = 53, MenReqPl = 5, BuyPrice = 3000 };
                case "BOOK OF MORNING STAR":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Swordsman | (int)E_Class.Shaman) { MenReq = 67, MenReqPl = 4, BuyPrice = 3000 };
                case "BOOK OF THUNDER BOLT":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 48, MenReqPl = 12, BuyPrice = 3500 };
                case "BOOK OF HONEST BOLT":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 80, MenReqPl = 22, BuyPrice = 10000 };
                case "BOOK OF FIRE SHOT":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 98, MenReqPl = 26, BuyPrice = 12000 };
                case "BOOK OF SIDEWINDER":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Swordsman | (int)E_Class.Shaman) { MenReq = 105, MenReqPl = 15, BuyPrice = 30000 };


                case "BOOK OF HEAL":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 22, MenReqPl = 4, BuyPrice = 4000 };
                case "BOOK OF PLUS HEAL":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")]) { MenReq = 200, MenReqPl = 5, BuyPrice = 10000 };

                //Knight
                case "BOOK OF MAGIC ARMOR":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Knight) { MenReq = 24, MenReqPl = 4, BuyPrice = 10000 };
                case "BOOK OF MENTAL SWORD":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Knight) { MenReq = 30, MenReqPl = 5, LevelReq = 31, BuyPrice = 30000 };

                //Swordman
                case "BOOK OF GUARDIAN SWORD":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Swordsman) { DexReq = 98, DexReqPl = 8, BuyPrice = 10000 };

                //Shaman
                case "BOOK OF GHOST HUNTER":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Shaman) { MenReq = 102, MenReqPl = 6, BuyPrice = 5000 };
                case "BOOK OF TEAGUE SHIELD":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Shaman) { MenReq = 112, MenReqPl = 9, DexReq = 45, DexReqPl = 5, BuyPrice = 10000 };
                case "BOOK OF FIRE PROTECTOR":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Shaman) { MenReq = 164, MenReqPl = 14, DexReq = 65, DexReqPl = 8, BuyPrice = 30000 };

                //Wizard
                case "BOOK OF SORCERER HUNTER":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Wizard) { MenReq = 170, MenReqPl = 8, BuyPrice = 5000 };
                case "BOOK OF MAGIC SHIELD":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Wizard) { MenReq = 140, MenReqPl = 14, BuyPrice = 10000 };
                case "BOOK OF RAINBOW ARMOR":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Wizard) { MenReq = 312, MenReqPl = 24, BuyPrice = 50000 };
                
                case "BOOK OF DEADLY BOOM":
                    return new Spellbook(name, Spells.SpellList[name.Replace("BOOK OF ", "")], (int)E_Class.Wizard) { MenReq = 450, MenReqPl = 10, BuyPrice = 25000 };



                #endregion
            }

            Log.LogLine("CreateItem Requested :" + name + " It did not exist");
            return new Item() { Name = "SOMETHING WRONG ITEM!", SprId = 131 }; ;
        }

        public static void Load()
        {
            //str mag vit dex
            GItems.Add("GOLD ELIXIR", new Consumable() { Name = "GOLD ELIXIR", SprId = 148, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("SILVER ELIXIR", new Consumable() { Name = "SILVER ELIXIR", SprId = 151, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("SCARLET ELIXIR", new Consumable() { Name = "SCARLET ELIXIR", SprId = 142, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("VIOLET ELIXIR", new Consumable() { Name = "VIOLET ELIXIR", SprId = 158, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });

            GItems.Add("BLACK OIL", new Consumable() { Name = "BLACK OIL", SprId = 140, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });

            GItems.Add("PINK POISON", new Consumable() { Name = "PINK POISON", SprId = 146, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("SKY-BLUE POISON", new Consumable() { Name = "SKY-BLUE POISON", SprId = 147, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("GRAY POISON", new Consumable() { Name = "GRAY POISON", SprId = 152, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });
            GItems.Add("LIGHT GREEN POISON", new Consumable() { Name = "LIGHT GREEN POISON", SprId = 159, _Consume = delegate(Player.Player pl) { pl.HPCur += (int)(pl.HP * 0.5f); pl.MPCur += (int)(pl.MP * 0.5f); } });






            #region Crafting

            GItems.Add("TOPAZ", new Item() { Name = "TOPAZ", SprId = 53 });
            GItems.Add("QUARTZ", new Item() { Name = "QUARTZ", SprId = 76 });
            GItems.Add("CRYSTAL", new Item() { Name = "CRYSTAL", SprId = 67 });
            GItems.Add("GOLD", new Item() { Name = "GOLD", SprId = 52 });
            GItems.Add("LEOPARD JASPER", new Item() { Name = "LEOPARD JASPER", SprId = 60 });
            GItems.Add("EMERALD", new Item() { Name = "EMERALD", SprId = 68 });
            GItems.Add("Z-STONE", new Item() { Name = "Z-STONE", SprId = 56 });
            GItems.Add("SAPPHIRE", new Item() { Name = "SAPPHIRE", SprId = 63 });
            GItems.Add("AMETHYST", new Item() { Name = "AMETHYST", SprId = 65 });
            GItems.Add("DIAMOND", new Item() { Name = "DIAMOND", SprId = 54 });

            GItems.Add("SILVER", new Item() { Name = "SILVER", SprId = 62 });
            GItems.Add("RUBY", new Item() { Name = "RUBY", SprId = 58 });
            GItems.Add("MOON STONE", new Item() { Name = "MOON STONE", SprId = 61 });
            GItems.Add("SUN STONE", new Item() { Name = "SUN STONE", SprId = 77 });
            GItems.Add("BLOOD STONE", new Item() { Name = "BLOOD STONE", SprId = 57 });
            GItems.Add("TIGER'S EYE", new Item() { Name = "TIGER'S EYE", SprId = 50 });
            GItems.Add("HELIODOR", new Item() { Name = "HELIODOR", SprId = 53 });
            GItems.Add("DARK STONE", new Item() { Name = "DARK STONE", SprId = 69 });
            GItems.Add("CAT'S EYE", new Item() { Name = "CAT'S EYE", SprId = 81 });
            GItems.Add("BLACK PEARL", new Item() { Name = "BLACK PEARL", SprId = 80 });


            #endregion

            var match = CreateItem("MATCH", 1); 
            match.Static = true;
            match.Position.X = 9;
            match.Position.Y = 21;
            World.World.Maps["Rest"].Enter(match);

            var wood = CreateItem("WOOD", 1);
            wood.Static = true;
            wood.Position.X = 11;
            wood.Position.Y = 21;
            World.World.Maps["Rest"].Enter(wood);

            var sawdust = CreateItem("SAW DUST", 1);
            sawdust.Static = true;
            sawdust.Position.X = 13;
            sawdust.Position.Y = 21;
            World.World.Maps["Rest"].Enter(sawdust);

            var squareb = CreateItem("SQUARE BAR", 1);
            squareb.Static = true;
            squareb.Position.X = 15;
            squareb.Position.Y = 21;
            World.World.Maps["Rest"].Enter(squareb);

            var paper = CreateItem("PAPER", 1);
            paper.Static = true;
            paper.Position.X = 17;
            paper.Position.Y = 21;
            World.World.Maps["Rest"].Enter(paper);

            var rubber = CreateItem("RUBBER", 1);
            rubber.Static = true;
            rubber.StackSize = 1;
            rubber.Position.X = 11;
            rubber.Position.Y = 23;
            World.World.Maps["Rest"].Enter(rubber);

            var leather = CreateItem("LEATHER", 1);
            leather.Static = true;
            leather.Position.X = 13;
            leather.Position.Y = 23;
            World.World.Maps["Rest"].Enter(leather);

            var shell = CreateItem("SHELL", 1);
            shell.Static = true;
            shell.Position.X = 15;
            shell.Position.Y = 23;
            World.World.Maps["Rest"].Enter(shell);

            var granite = CreateItem("GRANITE", 1);
            granite.Static = true;
            granite.Position.X = 17;
            granite.Position.Y = 23;
            World.World.Maps["Rest"].Enter(granite);
        }
        /*
        public static Object.Ore CreateOre(string name, int Grade, int StackSize)
        {
            string GradeStr = "PB";
            if (Grade == 2)
                GradeStr = "PN";
            else if (Grade == 3)
                GradeStr = "PG";

            switch (name)
            {
                case "COPPER":
                    return new Ore() { Name = "COPPER " + GradeStr, SprId = 90, StackSize = StackSize, Grade = Grade };
                case "IRON":
                    return new Ore() { Name = "IRON " + GradeStr, SprId = 90, StackSize = StackSize, Grade = Grade };
                case "WAX":
                    return new Ore() { Name = "WAX " + GradeStr, SprId = 93, StackSize = StackSize, Grade = Grade };
                case "ALUMINUM":
                    return new Ore() { Name = "ALUMINUM " + GradeStr, SprId = 90, StackSize = StackSize, Grade = Grade };
            }
            return null;
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Maps
    {
        public static void Load()
        {
            var Rest = World.World.AddMap("Rest", "erest.map", World.Map.E_MapType.Safe, time: -1);
            Rest.Tiles[19, 13].Portal = new World.Portal("St. Andover", 50, 50);

            var village1 = World.World.AddMap("St. Andover", "32.map", World.Map.E_MapType.Safe, time: -1);
            village1.Tiles[139, 155].Portal = new World.Portal("Loen", 6, 14);
            village1.Tiles[65, 158].Portal = new World.Portal("Arnold", 5, 17);
            village1.Tiles[65, 122].Portal = new World.Portal("Weakly1", 11, 19);
            village1.Tiles[139, 134].Portal = new World.Portal("Skel1", 50, 13);
            village1.Tiles[70, 66].Portal = new World.Portal("ItemVillage", 93, 110);
            village1.Tiles[135, 55].Portal = new World.Portal("Miro1", 1, 1);
            village1.Tiles[169, 119].Portal = new World.Portal("Biggun1", 98, 78);


            var Loen = World.World.AddMap("Loen", "loen.map", World.Map.E_MapType.Safe);
            Loen.Tiles[5, 15].Portal = new World.Portal("St. Andover", 139, 157);
            var Arnold = World.World.AddMap("Arnold", "anold.map", World.Map.E_MapType.Safe);
            Arnold.Tiles[4, 17].Portal = new World.Portal("St. Andover", 65, 160);

            var Miner0 = World.World.AddMap("Miner0", "miner0.map", World.Map.E_MapType.Safe, tilet: World.Map.tileWeak, time: 6, lr: 3);
            var Miner1 = World.World.AddMap("Miner1", "miner1.map", World.Map.E_MapType.Safe, tilet: World.Map.tileWeak, time: 6, lr: 3);
            var Miner2 = World.World.AddMap("Miner2", "miner2.map", World.Map.E_MapType.Safe, tilet: World.Map.tileWeak, time: 6, lr: 3);
            var Miner3 = World.World.AddMap("Miner3", "miner3.map", World.Map.E_MapType.Safe, tilet: World.Map.tileWeak, time: 6, lr: 3);
            var PS1 = World.World.AddMap("PS1", "ps01.map", World.Map.E_MapType.Safe, tilet: World.Map.tilePS);
            var PS2 = World.World.AddMap("PS2", "ps02.map", World.Map.E_MapType.Safe, tilet: World.Map.tilePS);
            var PS3 = World.World.AddMap("PS3", "ps03.map", World.Map.E_MapType.Safe, tilet: World.Map.tilePS);

            #region Weakly
            var weakly1 = World.World.AddMap("Weakly1", "weakly1.map", time: 4, lr: 3);
            weakly1.Tiles[11, 18].Portal = new World.Portal("St. Andover", 66, 123);
            weakly1.Tiles[50, 45].Portal = new World.Portal("Weakly2", 51, 14);

            var weakly2 = World.World.AddMap("Weakly2", "weakly2.map", time: 4, lr: 3);
            weakly2.Tiles[51, 13].Portal = new World.Portal("Weakly1", 50, 46);
            weakly2.Tiles[38, 53].Portal = new World.Portal("Weakly3", 53, 15);

            var weakly3 = World.World.AddMap("Weakly3", "weakly3.map", time: 4, lr: 3);
            weakly3.Tiles[53, 13].Portal = new World.Portal("Weakly2", 38, 55);


            var weakly4 = World.World.AddMap("Weakly4", "weakly4.map", time: 4, lr: 3);
            var weakly5 = World.World.AddMap("Weakly5", "weakly5.map", time: 4, lr: 3);
            #endregion

            #region Skel
            var Skel1 = World.World.AddMap("Skel1", "skel1.map", time: 4, lr: 3);
            Skel1.Tiles[50, 11].Portal = new World.Portal("St. Andover", 138, 136);
            Skel1.Tiles[13, 47].Portal = new World.Portal("Skel2", 12, 14);


            var Skel2 = World.World.AddMap("Skel2", "skel2.map", time: 4, lr: 3);
            Skel2.Tiles[12, 12].Portal = new World.Portal("Skel1", 13, 49);
            Skel2.Tiles[48, 42].Portal = new World.Portal("Skel3", 34, 14);

            var Skel3 = World.World.AddMap("Skel3", "skel3.map", time: 4, lr: 3);
            Skel3.Tiles[34, 12].Portal = new World.Portal("Skel2", 48, 44);
            Skel3.Tiles[33, 49].Portal = new World.Portal("Skel4", 33, 24);

            var Skel4 = World.World.AddMap("Skel4", "skel4.map", time: 4, lr: 3);
            Skel4.Tiles[33, 22].Portal = new World.Portal("Skel3", 33, 51);
            Skel4.Tiles[32, 35].Portal = new World.Portal("Skel5", 14, 21);

            var Skel5 = World.World.AddMap("Skel5", "skel5.map", time: 4, lr: 3);
            Skel5.Tiles[14, 19].Portal = new World.Portal("Skel4", 32, 37);
            #endregion

            #region Beginner
            var beginner = World.World.AddMap("Beginner", "estart2.map", World.Map.E_MapType.Safe);

            #endregion

            #region Biggun
            var biggun1 = World.World.AddMap("Biggun1", "estart4.map");
            biggun1.Tiles[99, 79].Portal = new World.Portal("St. Andover", 167, 119);

            var biggun2 = World.World.AddMap("Biggun2", "estart3.map");
            #endregion

            #region Level
            var elevel = World.World.AddMap("ELevel", "elevel.map");
            elevel.Tiles[29, 30].Portal = new World.Portal("St. Andover", 99, 130);
            elevel.Tiles[11, 42].Portal = new World.Portal("010", 73, 8);
            elevel.Tiles[11, 30].Portal = new World.Portal("020", 7, 8);
            elevel.Tiles[11, 19].Portal = new World.Portal("030", 14, 12);
            elevel.Tiles[22, 12].Portal = new World.Portal("040", 16, 18);
            elevel.Tiles[35, 12].Portal = new World.Portal("050", 87, 46);
            elevel.Tiles[47, 18].Portal = new World.Portal("060", 105, 78);
            elevel.Tiles[47, 30].Portal = new World.Portal("070", 12, 10);
            elevel.Tiles[47, 42].Portal = new World.Portal("080", 17, 9);


            var L010 = World.World.AddMap("010", "010map.map", time: -1);
            L010.Tiles[73, 6].Portal = new World.Portal("ELevel", 11, 44);

            var L020 = World.World.AddMap("020", "020map.map", time: -1);
            L020.Tiles[7, 6].Portal = new World.Portal("ELevel", 11, 32);

            var L030 = World.World.AddMap("030", "030map.map", time: -1);
            L030.Tiles[14, 10].Portal = new World.Portal("ELevel", 11, 21);

            var L040 = World.World.AddMap("040", "040map.map", time: -1);
            L040.Tiles[16, 16].Portal = new World.Portal("ELevel", 22, 14);

            var L050 = World.World.AddMap("050", "050map.map", time: -1);
            L050.Tiles[87, 44].Portal = new World.Portal("ELevel", 35, 14);

            var L060 = World.World.AddMap("060", "060map.map", time: -1);
            L060.Tiles[105, 76].Portal = new World.Portal("ELevel", 47, 20);
            L060.Tiles[12, 7].Portal = new World.Portal("061", 12, 12);
            var L061 = World.World.AddMap("061", "061map.map", time: -1);
            L061.Tiles[12, 10].Portal = new World.Portal("060", 12, 9);

            var L070 = World.World.AddMap("070", "070map.map", time: -1);
            L070.Tiles[12, 8].Portal = new World.Portal("ELevel", 47, 32);
            var L071 = World.World.AddMap("071", "071map.map", time: -1);

            var L080 = World.World.AddMap("080", "080map.map", time: -1);
            L080.Tiles[17, 7].Portal = new World.Portal("ELevel", 47, 44);
            var L081 = World.World.AddMap("081", "081map.map", time: -1);
            #endregion

            #region VV
            var VV1 = World.World.AddMap("VV1", "vv1.map", tilet: World.Map.tileVV, time: 5, lr: 3);
            VV1.Tiles[97, 133].Portal = new World.Portal("VV2", 46, 55);

            var VV2 = World.World.AddMap("VV2", "vv2.map", tilet: World.Map.tileVV, time: 5, lr: 3);
            VV2.Tiles[45, 54].Portal = new World.Portal("VV1", 96, 132);

            var VV3 = World.World.AddMap("VV3", "vv3.map", tilet: World.Map.tileVV, time: 5, lr: 3);
            var VV4 = World.World.AddMap("VV4", "vv4.map", tilet: World.Map.tileVV, time: 5, lr: 3);
            var VV5 = World.World.AddMap("VV5", "vv5.map", tilet: World.Map.tileVV, time: 5, lr: 3);
            #endregion

            #region Cave
            var Cave = World.World.AddMap("Cave", "Cave.map", tilet: World.Map.tileWeak, time: 4, lr: 2);
            var Cave1 = World.World.AddMap("Cave1", "cave1.map", tilet: World.Map.tileWeak, time: 4, lr: 2);
            var Cave2 = World.World.AddMap("Cave2", "cave2.map", tilet: World.Map.tileWeak, time: 4, lr: 2);
            #endregion

            #region Golem
            var Golem = World.World.AddMap("Golem", "golem.map");
            var Golem1 = World.World.AddMap("Golem1", "golem1.map");
            var Golem2 = World.World.AddMap("Golem2", "golem2.map");
            var Golem3 = World.World.AddMap("Golem3", "golem3.map");
            var Golem13 = World.World.AddMap("Golem12", "golem12.map");
            #endregion

            #region Miro
            var Miro = World.World.AddMap("Miro1", "miro1.map", time: -1);
            Miro.Tiles[0, 0].Portal = new World.Portal("St. Andover", 135, 57);

            #endregion

            #region Venture
            var Venture4 = World.World.AddMap("Venture4", "estart4.map", time: -1);
            #endregion

            var Great = World.World.AddMap("Great", "great.map", time: -1);

            #region ItemVillage
            var Itemvillage = World.World.AddMap("ItemVillage", "ivillage.map");
            Itemvillage.Tiles[93, 108].Portal = new World.Portal("St. Andover", 70, 68);
            Itemvillage.Tiles[2, 18].Portal = new World.Portal("ItemPigmy1", 16, 30);
            Itemvillage.Tiles[25, 5].Portal = new World.Portal("ItemZomby1", 13, 39);
            Itemvillage.Tiles[186, 4].Portal = new World.Portal("ItemSkel1", 21, 29);
            Itemvillage.Tiles[186, 4].Portal = new World.Portal("ItemButcher1", 20, 23);
            Itemvillage.Tiles[193, 186].Portal = new World.Portal("ItemMummy1", 18, 19);
            Itemvillage.Tiles[177, 160].Portal = new World.Portal("ItemStone1", 16, 14);
            Itemvillage.Tiles[23, 135].Portal = new World.Portal("ItemDummy1", 18, 19);
            Itemvillage.Tiles[38, 142].Portal = new World.Portal("ItemWarDummy1", 18, 19);
            Itemvillage.Tiles[13, 45].Portal = new World.Portal("ItemHardboil1", 18, 19);
            Itemvillage.Tiles[7, 178].Portal = new World.Portal("ItemGolem1", 18, 19);


            #region ItemPigmy
            var ItemPigmy1 = World.World.AddMap("ItemPigmy1", "ipigmy1.map");
            ItemPigmy1.Tiles[16, 29].Portal = new World.Portal("ItemVillage", 2, 20);

            var ItemPigmy2 = World.World.AddMap("ItemPigmy2", "ipigmy2.map");
            var ItemPigmy3 = World.World.AddMap("ItemPigmy3", "ipigmy3.map");
            #endregion

            #region ItemZmoby
            var ItemZomby1 = World.World.AddMap("ItemZomby1", "izomby1.map");
            ItemZomby1.Tiles[12, 39].Portal = new World.Portal("ItemVillage", 25, 7);

            var ItemZomby2 = World.World.AddMap("ItemZomby2", "izomby2.map");
            var ItemZomby3 = World.World.AddMap("ItemZomby3", "izomby3.map");
            #endregion

            #region ItemSkel
            var ItemSkel1 = World.World.AddMap("ItemSkel1", "iskel1.map");
            ItemSkel1.Tiles[20, 29].Portal = new World.Portal("ItemVillage", 186, 6);
            var ItemSkel2 = World.World.AddMap("ItemSkel2", "iskel2.map");
            var ItemSkel3 = World.World.AddMap("ItemSkel3", "iskel3.map");
            #endregion

            #region ItemButcher
            var ItemButcher1 = World.World.AddMap("ItemButcher1", "ibut1.map");
            ItemButcher1.Tiles[19, 23].Portal = new World.Portal("ItemVillage", 190, 31);
            var ItemButcher2 = World.World.AddMap("ItemButcher2", "ibut2.map");
            var ItemButcher3 = World.World.AddMap("ItemButcher3", "ibut3.map");
            #endregion

            #region ItemMummy
            var ItemMummy1 = World.World.AddMap("ItemMummy1", "imummy1.map");
            ItemMummy1.Tiles[18, 17].Portal = new World.Portal("ItemVillage", 193, 188);
            var ItemMummy2 = World.World.AddMap("ItemMummy2", "imummy2.map");
            var ItemMummy3 = World.World.AddMap("ItemMummy3", "imummy3.map");
            #endregion

            #region ItemStone
            var ItemStone1 = World.World.AddMap("ItemStone1", "istone1.map");
            ItemStone1.Tiles[16, 12].Portal = new World.Portal("ItemVillage", 165, 190);
            var ItemStone2 = World.World.AddMap("ItemStone2", "istone2.map");
            var ItemStone3 = World.World.AddMap("ItemStone3", "istone3.map");
            var ItemStone4 = World.World.AddMap("ItemStone4", "istone4.map");
            #endregion

            #region ItemDummy
            var ItemDummy1 = World.World.AddMap("ItemDummy1", "imummy1.map");
            ItemDummy1.Tiles[18, 17].Portal = new World.Portal("ItemVillage", 24, 135);
            var ItemDummy2 = World.World.AddMap("ItemDummy2", "imummy2.map");
            var ItemDummy3 = World.World.AddMap("ItemDummy3", "imummy3.map");
            var ItemDummy4 = World.World.AddMap("ItemDummy4", "istone3.map");
            var ItemDummy5 = World.World.AddMap("ItemDummy5", "istone4.map");
            #endregion

            #region ItemWarDummy
            var ItemWarDummy1 = World.World.AddMap("ItemWarDummy1", "imummy1.map");
            ItemDummy1.Tiles[18, 17].Portal = new World.Portal("ItemVillage", 39, 142);
            var ItemWarDummy2 = World.World.AddMap("ItemWarDummy2", "imummy2.map");
            var ItemWarDummy3 = World.World.AddMap("ItemWarDummy3", "imummy3.map");
            var ItemWarDummy4 = World.World.AddMap("ItemWarDummy4", "istone3.map");
            var ItemWarDummy5 = World.World.AddMap("ItemWarDummy5", "istone4.map");
            #endregion

            #region ItemHardboild
            var ItemHardboil1 = World.World.AddMap("ItemHardboil1", "imummy1.map");
            ItemHardboil1.Tiles[18, 17].Portal = new World.Portal("ItemVillage", 14, 45);

            var ItemHardboil2 = World.World.AddMap("ItemHardboil2", "imummy2.map");
            var ItemHardboil3 = World.World.AddMap("ItemHardboil3", "imummy3.map");
            var ItemHardboil4 = World.World.AddMap("ItemHardboil4", "istone3.map");
            var ItemHardboil5 = World.World.AddMap("ItemHardboil5", "istone4.map");
            #endregion

            #region ItemGolem
            var ItemGolem1 = World.World.AddMap("ItemGolem1", "imummy1.map");
            ItemHardboil1.Tiles[18, 17].Portal = new World.Portal("ItemVillage", 8, 178);
            var ItemGolem2 = World.World.AddMap("ItemGolem2", "imummy2.map");
            var ItemGolem3 = World.World.AddMap("ItemGolem3", "imummy3.map");
            var ItemGolem4 = World.World.AddMap("ItemGolem4", "istone3.map");
            var ItemGolem5 = World.World.AddMap("ItemGolem5", "istone4.map");
            #endregion
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Npcs
    {
        public static void Load()
        {
            var Aron = new Object.Npc()
            {
                Name = "ARON",
                SprId = 4,
                Position = new Object.Position() { X = 98, Y = 57 },
                aSpeed = 2,
                aFrames = 7,
                ChatPhrases = new List<string>() { 
                "Type \"make me a knight\" to be a knight.",
                "Type \"make me a swordman\" to be a swordman.",
                "Type \"make me a shaman\" to be a shaman.",
                "Type \"make me a wizard\" to be a wizard.",}
            };
            World.World.GetMap("St. Andover").Enter(Aron);
            var Alias = new Object.Npc()
            {
                Name = "ALIAS",
                SprId = 5,
                Position = new Object.Position() { X = 90, Y = 173 },
                aSpeed = 2,
                aFrames = 7,
                ChatPhrases = new List<string>() { 
                "Welcome to the Last Kingdom.",
                "Say \"give me the life drug\" if you need the Life Drug.",
                }
            };
            World.World.GetMap("St. Andover").Enter(Alias);

            var Trader = new Object.Npc()
            {
                Name = "TRADER",
                SprId = 6,
                Position = new Object.Position() { X = 14, Y = 19 },
                aSpeed = 1,
                aFrames = 1,
                ChatPhrases = new List<string>() { 
              /*  "Greetings traveller.",
                "I am the trader, you must of heard about me.",
                "I once killed a GWRINN with my bare hands, revived it, then nursed it back to health",
                "I don't eat food, does anyone?",
                "She sells sea shells by the sea shore.",
                "A match, no match box. Andover patent office here I come.",
                "Im surprized all this vegetation grows around here, when was the last time it rained..",
                "So the other day I was walking around the village and found a toy sword..",*/
                }
            };
            World.World.GetMap("Rest").Enter(Trader);

            var Boy = new Object.Npc()
            {
                Name = "BOY",
                SprId = 7,
                Position = new Object.Position() { X = 13, Y = 11 },
                aSpeed = 1,
                aFrames = 1,
                ChatPhrases = new List<string>() { 
                "Where is my toy sword.",
                }
            };
            World.World.GetMap("Arnold").Enter(Boy);

            var Arnold = new Object.Npc()
            {
                Name = "ARNOLD",
                SprId = 1,
                Position = new Object.Position() { X = 9, Y = 9 },
                aSpeed = 2,
                aFrames = 7,
                ChatPhrases = new List<string>() {
                "Click on me to see the menu."}
            };
            Arnold.Gump = new Object.GUMP(Arnold.objId, 0xff85, 0x03ff, 0x70, "Menu", new List<Object.Item>
            {
                Scripts.Items.CreateItem("SUIT"),
                Scripts.Items.CreateItem("SURPLICE"),
                Scripts.Items.CreateItem("CAPE"),
                Scripts.Items.CreateItem("ROBE"),
                Scripts.Items.CreateItem("FULL DRESS"),
                Scripts.Items.CreateItem("BAMBOO KNIFE"),
                Scripts.Items.CreateItem("WOODEN SWORD"),
                Scripts.Items.CreateItem("SAW"),
                Scripts.Items.CreateItem("SPIKED CLUB"),
                Scripts.Items.CreateItem("SMALL SHIELD"),
                Scripts.Items.CreateItem("HOOD"),
                Scripts.Items.CreateItem("BLACKSMITH SUIT"),
                Scripts.Items.CreateItem("MINING SUIT"),
                Scripts.Items.CreateItem("CAULDRON"),
                Scripts.Items.CreateItem("ANVIL"),
            });
            World.World.GetMap("Arnold").Enter(Arnold);

            var Employee = new Object.Npc()
            {
                Name = "EMPLOYEE",
                SprId = 2,
                Position = new Object.Position() { X = 128, Y = 90 },
                aSpeed = 1,
                aFrames = 1,
                ChatPhrases = new List<string>() {
                "Click on me to see the menu."}
            };
            Employee.Gump = new Object.GUMP(Employee.objId, 0xff85, 0x03ff, 0x70, "Menu", new List<Object.Item>
            {
                Scripts.Items.CreateItem("BOOK OF HEAL"),
                Scripts.Items.CreateItem("BOOK OF PLUS HEAL"),
                Scripts.Items.CreateItem("BOOK OF ELECTRONIC BALL"),
                Scripts.Items.CreateItem("BOOK OF FIRE BALL"),
                Scripts.Items.CreateItem("BOOK OF MAGIC ARMOR"),
                Scripts.Items.CreateItem("BOOK OF GUARDIAN SWORD"),
                Scripts.Items.CreateItem("BOOK OF FIRE PROTECTOR"),
                Scripts.Items.CreateItem("BOOK OF TEAGUE SHIELD"),
                Scripts.Items.CreateItem("BOOK OF MAGIC SHIELD"),
                Scripts.Items.CreateItem("BOOK OF ZIG ZAG"),
                Scripts.Items.CreateItem("BOOK OF SIDEWINDER"),
            });
            World.World.GetMap("St. Andover").Enter(Employee);

            var Loen = new Object.Npc()
            {
                Name = "LOEN",
                SprId = 3,
                Position = new Object.Position() { X = 13, Y = 8 },
                aSpeed = 2,
                aFrames = 7,
                ChatPhrases = new List<string>() {
                "Click on me to see the menu."}
            }; 
            Loen.Gump = new Object.GUMP(Loen.objId, 0xff85, 0x03ff, 0x70, "Menu", new List<Object.Item>
            {
                Scripts.Items.CreateItem("LIFE DRUG"),
                Scripts.Items.CreateItem("MAGIC DRUG"),
                Scripts.Items.CreateItem("FULL LIFE DRUG"),
                Scripts.Items.CreateItem("FULL MAGIC DRUG"),
            });
            World.World.GetMap("Loen").Enter(Loen);
        }
    }
}

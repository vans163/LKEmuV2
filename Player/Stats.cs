using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using LKCamelotV2.Object;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class Stats
    {
        public Player playerLink;

        internal int Str
        {
            get
            {
                var bonus = 0;
                switch (playerLink.Class)
                {
                    case (E_Class.Beginner):
                        bonus += 5; break;
                    case (E_Class.Knight):
                        bonus += 30; break;
                    case (E_Class.Swordsman):
                        bonus += 20; break;
                    case (E_Class.Wizard):
                        bonus += 15; break;
                    case (E_Class.Shaman):
                        bonus += 15; break;
                }
				return playerLink.Attribs[E_Attribute.Str] + bonus;
            }
            set
            {
				playerLink.Attribs[E_Attribute.Str] = value;
            }
        }

        internal int Men
        {
            get
            {
                var bonus = 0;
                switch (playerLink.Class)
                {
                    case (E_Class.Beginner):
                        bonus += 5; break;
                    case (E_Class.Knight):
                        bonus += 10; break;
                    case (E_Class.Swordsman):
                        bonus += 5; break;
                    case (E_Class.Wizard):
                        bonus += 30; break;
                    case (E_Class.Shaman):
                        bonus += 25; break;
                }
				return playerLink.Attribs[E_Attribute.Men] + bonus;
            }
            set
            {
				playerLink.Attribs[E_Attribute.Men] = value;
            }
        }

        internal int Dex
        {
            get
            {
                var bonus = 0;
                switch (playerLink.Class)
                {
                    case (E_Class.Beginner):
                        bonus += 5; break;
                    case (E_Class.Knight):
                        bonus += 15; break;
                    case (E_Class.Swordsman):
                        bonus += 30; break;
                    case (E_Class.Wizard):
                        bonus += 15; break;
                    case (E_Class.Shaman):
                        bonus += 20; break;
                }
				return playerLink.Attribs[E_Attribute.Dex] + bonus;
            }
            set
            {
				playerLink.Attribs[E_Attribute.Dex] = value;
            }
        }

        internal int Vit
        {
            get
            {
                var bonus = 0;
                switch (playerLink.Class)
                {
                    case (E_Class.Beginner):
                        bonus += 5; break;
                    default:
                        bonus += 10; break;
                }
				return playerLink.Attribs[E_Attribute.Vit] + bonus;
            }
            set
            {
				playerLink.Attribs[E_Attribute.Vit] = value;
            }
        }

        internal int StrTotal
        {
            get
            {
                int bonus = 0;
                foreach (var itm in playerLink.Inventory.EquippedItems)
                    bonus += itm.Value.Str;
                return Str + bonus;
            }
        }

        internal int MenTotal
        {
            get
            {
                int bonus = 0;
                foreach (var itm in playerLink.Inventory.EquippedItems)
                    bonus += itm.Value.Men;
                return Men + bonus;
            }
        }

        internal int DexTotal
        {
            get
            {
                int bonus = 0;
                foreach (var itm in playerLink.Inventory.EquippedItems)
                    bonus += itm.Value.Dex;
                return Dex + bonus;
            }
        }

        internal int VitTotal
        {
            get
            {
                int bonus = 0;
                foreach (var itm in playerLink.Inventory.EquippedItems)
                    bonus += itm.Value.Vit;
                return Vit + bonus;
            }
        }

        internal int Extra
        {
            get
            {
				return playerLink.Attribs [E_Attribute.Extra];
            }
            set
            {
				playerLink.Attribs [E_Attribute.Extra] = value;
            }
        }

        public bool AddStat(E_Attribute astat)
        {
			var stat = playerLink.Attribs[astat];
            if (Extra == 0 || stat == ushort.MaxValue)
                return false;

            Extra--;

			playerLink.Attribs[astat]++;

            return true;
        }
    }
}

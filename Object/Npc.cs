using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class GUMP
    {
        public int ID;
        public ushort Titlec;
        public ushort Boxc;
        public byte Time;
        public string ShopName;
        public List<Item> SellList;

        public GUMP()
        {
        }

        public GUMP(int ID, ushort titlec, ushort boxc, byte time, string shopname, List<Item> SellList)
        {
            this.ID = ID;
            this.Titlec = titlec;
            this.Boxc = boxc;
            this.Time = time;
            this.ShopName = shopname;
            this.SellList = SellList;
        }

        public byte[] ItemIDArray
        {
            get
            {
                byte[] list = new byte[32];
                int itr = 0;
                foreach (var sell in SellList)
                {
                    list[itr + 1] = (byte)(sell.SprId >> 8);
                    list[itr] = (byte)sell.SprId;

                    itr += 2;
                }
                return list;
            }
        }

        public string ItemNameString
        {
            get
            {
                string temp = "";
                foreach (var item in SellList)
                {
                    temp += item.BuyPrice + " " + item.Name + " ,";
                }
                return temp;
            }
        }

        public byte[] ItemNameArray
        {
            get
            {
                byte[] temp = new byte[512];
                int itr = 0;
                foreach (var item in SellList)
                {
                    var stemp = item.Name;
                    if (item != SellList.Last())
                        stemp += ",";
                    System.Buffer.BlockCopy(stemp.ToCharArray(), 0, temp, itr, stemp.Length);
                    itr += stemp.Length;
                }

                Array.Resize(ref temp, itr);
                return temp;
            }
        }
    }

    public class Npc : Mobile
    {
        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 0x0A;
                ret[9] = (byte)SprId;
                return ret;
            }
        }

        public List<string> ChatPhrases = new List<string>();
        public int lastPhrase = 0;
        public long lastSpeakTime;

        public GUMP Gump;
        public int aSpeed = 1;
        public int aFrames = 1;

        public void Buy(Player.Player playr, int item)
        {
            var fs = playr.Inventory.FreeSlot;
            if (!fs.Key)
                return;

            Item itm = Gump.SellList[item];

            if (playr.State.Gold < itm.BuyPrice)
                return;

            playr.State.Gold -= itm.BuyPrice;

            var newi = Scripts.Items.CreateItem(itm._Name, itm.StackSize);
            playr.OnPickUp(newi);
        }
    }
}

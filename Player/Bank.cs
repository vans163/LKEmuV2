using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class Bank
    {
        public Player playerLink;
        [ProtoMember(1)]
        public int Gold;

        [ProtoMember(2)]
        internal ConcurrentDictionary<int, Object.Item> BankItems = new ConcurrentDictionary<int, Object.Item>();

        internal KeyValuePair<bool, int> FreeSlot
        {
            get
            {
                for (int x = 0; x < 12; x++)
                {
                    if (!BankItems.ContainsKey(x))
                        return new KeyValuePair<bool, int>(true, x);
                }
                return new KeyValuePair<bool, int>(false, 0);
            }
        }

        internal void Deposit(int invslot)
        {
            Object.Item oute;
            var fs = FreeSlot;
            if (!fs.Key)
                return;

            if (playerLink.Inventory.InventItems.TryRemove(invslot, out oute))
            {
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(invslot).Compile());
                if (BankItems.TryAdd(fs.Value, oute))
                {
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToEntrust(oute, fs.Value).Compile());
                }
            }
        }

        internal void Withdraw(int entslot)
        {
            var fs = playerLink.Inventory.FreeSlot;
            if (!fs.Key)
                return;

            Object.Item outv;
            if (BankItems.TryRemove(entslot, out outv))
            {
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteEntrustSlot(entslot).Compile());

                if (playerLink.Inventory.InventItems.TryAdd(fs.Value, outv))
                {
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(outv, fs.Value).Compile());
                }
            }
        }

        internal void RedrawBank()
        {
            foreach (var itm in BankItems)
                playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToEntrust(itm.Value, itm.Key).Compile());
        }
    }
}

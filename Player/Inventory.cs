using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Concurrent;
using ProtoBuf;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class Inventory
    {
        public Player playerLink;

        [ProtoMember(1)]
        internal ConcurrentDictionary<int, Object.Item> InventItems = new ConcurrentDictionary<int, Object.Item>();
        [ProtoMember(2)]
        internal ConcurrentDictionary<int, Object.Equipment> EquippedItems = new ConcurrentDictionary<int, Object.Equipment>();

        internal KeyValuePair<bool, int> FreeSlot
        {
            get
            {
                for (int x = 0; x < 24; x++)
                {
                    if (!InventItems.ContainsKey(x))
                        return new KeyValuePair<bool, int>(true, x);
                }
                return new KeyValuePair<bool, int>(false, 0);
            }
        }

        internal bool AddItem(Object.Item objt)
        {
            var freeslot = FreeSlot;
            if (freeslot.Key == true)
            {
                Object.Item tempo = objt;
                if (objt.Static)
                    tempo = Scripts.Items.CreateItem(objt._Name, objt.StackSize);
                InventItems.TryAdd(freeslot.Value, tempo);
                playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(tempo, freeslot.Value).Compile());
                return true;
            }
            return false;
        }

        internal Object.Item GetItem(int slot)
        {
            Object.Item outi;
            InventItems.TryGetValue(slot, out outi);
            return outi;
        }

        internal KeyValuePair<int, Object.Item> ContainsItem(string name)
        {
            foreach (var itm in InventItems)
            {
                if (itm.Value._Name == name)
                {
                    return itm;
                }
            }
            return new KeyValuePair<int, Object.Item>();
        }

        internal Object.Item Drop(int slot)
        {
            Object.Item outi = null;
            if (InventItems.TryRemove(slot, out outi))
            {
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(slot).Compile());
                outi.Position.X = playerLink.Position.X;
                outi.Position.Y = playerLink.Position.Y;
                playerLink.Position.CurMap.Enter(outi);
            }
            return outi;
        }

        internal void Delete(int slot, bool oneof = false)
        {
            Object.Item outi = null;
            if (InventItems.TryRemove(slot, out outi))
            {
                if (oneof && outi.StackSize > 1)
                {
                    outi.StackSize--;
                    InventItems.TryAdd(slot, outi);
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(outi, slot).Compile());

                }
                else
                    playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(slot).Compile());
            }
        }

        internal void Sell(int slot)
        {
            Object.Item outi = null;
            if (InventItems.TryGetValue(slot, out outi))
            {
                if (outi.SellPrice == 0)
                    return;

                playerLink.State.Gold += outi.SellPrice;
                InventItems.TryRemove(slot, out outi);
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(slot).Compile());
            }
        }

        internal void DropDied()
        {
            foreach (var itm in InventItems.Skip(0).Select(x => x.Key).ToArray())
                Drop(itm);
            if (playerLink.State.Gold > 0)
            {
                int goldtodrop = (int)(playerLink.State.Gold * 0.10);
                var gold = new Object.Gold() { Amount = goldtodrop };
                playerLink.State.Gold -= goldtodrop;
                gold.Position.X = playerLink.Position.X;
                gold.Position.Y = playerLink.Position.Y;
                playerLink.Position.CurMap.Enter(gold);
            }
        }

        internal void Swap(int froms, int tos)
        {
            Object.Item fromss;
            Object.Item toss;
            if (InventItems.TryGetValue(froms, out fromss))
            {
                if (InventItems.TryGetValue(tos, out toss))
                {
                    InventItems[froms] = toss;
                    InventItems[tos] = fromss;
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(toss, froms).Compile());
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(fromss, tos).Compile());
                }
                else
                {
                    if (fromss.StackSize > 1)
                    {
                        playerLink.WriteWarn(string.Format("How many do you wish to move? [0-{0}]", fromss.StackSize));
                        playerLink.gameLink.stackmoveCallback = new Func<int, bool>((amt) =>
                            {
                                if (fromss.StackSize < amt || amt <= 0)
                                    return false;

                                if (InventItems.TryRemove(froms, out fromss))
                                {
                                    Object.Item newi = Scripts.Items.CreateItem(fromss._Name, amt);
                                    fromss.StackSize -= amt;
                                    if (InventItems.TryAdd(tos, newi))
                                    {
                                        if (fromss.StackSize == 0)
                                            playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(froms).Compile());
                                        else
                                        {
                                            InventItems.TryAdd(froms, fromss);
                                            playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(fromss, froms).Compile());
                                        }

                                        playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(newi, tos).Compile());
                                    }
                                }

                                return true;
                            });
                        return;
                    }

                    if (InventItems.TryRemove(froms, out fromss))
                    {
                        if (InventItems.TryAdd(tos, fromss))
                        {
                            playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(froms).Compile());
                            playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(fromss, tos).Compile());
                        }
                    }
                }
            }
        }

        Object.Item Split(Object.Item itm, int amt)
        {
            if (amt <= 0)
                return null;
            if (itm.StackSize < amt)
                return null;

            Object.Item newi = Scripts.Items.CreateItem(itm._Name, amt);
            itm.StackSize -= amt;
            return newi;
        }

        internal void Unequip(int eqslot, bool force = false)
        {
            Object.Equipment oute;
            var fs = FreeSlot;
            var frslot = fs.Value;
            if (!fs.Key && !force)
                return;
            if (!fs.Key && force)
                frslot = 0;

            if (EquippedItems.TryRemove(eqslot, out oute))
            {
                if (oute.ArmorStage == -1 || oute.ArmorStage == -2)
                    playerLink.State.ProfessionSuit = 0;

                if (InventItems.TryAdd(frslot, oute))
                {
                    playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(oute, frslot).Compile());
                }
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteEquipItem(eqslot).Compile());
                playerLink.gameLink.Send(new Network.GameOutMessage.UpdateCharStats(playerLink).Compile());
                playerLink.Position.CurMap.Events.OnChgObjSprit(playerLink);
            }
        }

        internal void Equip(int invslot)
        {
            Object.Item outv;
            if (InventItems.TryGetValue(invslot, out outv))
            {
                if (!outv.CanUse(playerLink))
                    return;
                var eqslot = (outv as Object.Equipment).EquipSlot;
                if (eqslot == -1)
                    return;

                Object.Equipment eq;
                if (EquippedItems.TryRemove(eqslot, out eq))
                {
                    if (eq.ArmorStage == -1 || eq.ArmorStage == -2)
                        playerLink.State.ProfessionSuit = 0;

                    if (InventItems.TryRemove(invslot, out outv))
                    {
                        if (InventItems.TryAdd(invslot, eq) && EquippedItems.TryAdd((outv as Object.Equipment).EquipSlot, outv as Object.Equipment))
                        {
                            playerLink.gameLink.Send(new Network.GameOutMessage.EquipItem(outv as Object.Equipment).Compile());
                            playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(eq, invslot).Compile());
                        }
                    }
                }
                else
                {
                    if (InventItems.TryRemove(invslot, out outv))
                    {
                        if (EquippedItems.TryAdd(eqslot, outv as Object.Equipment))
                        {
                            playerLink.gameLink.Send(new Network.GameOutMessage.DeleteItemSlot(invslot).Compile());
                            playerLink.gameLink.Send(new Network.GameOutMessage.EquipItem(outv as Object.Equipment).Compile());
                        }
                    }
                }
                if (outv.ArmorStage == -1)
                    playerLink.State.ProfessionSuit = 1;
                else if (outv.ArmorStage == -2)
                    playerLink.State.ProfessionSuit = 2;

                playerLink.gameLink.Send(new Network.GameOutMessage.UpdateCharStats(playerLink).Compile());
                playerLink.Position.CurMap.Events.OnChgObjSprit(playerLink);
            }
        }

        internal void RedrawInventory()
        {
            foreach (var itm in InventItems)
                playerLink.gameLink.Send(new Network.GameOutMessage.AddItemToInventory(itm.Value, itm.Key).Compile());
            foreach (var itm in EquippedItems)
                playerLink.gameLink.Send(new Network.GameOutMessage.EquipItem(itm.Value).Compile());
        }
    }
}

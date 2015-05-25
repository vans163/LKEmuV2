using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class LeafSpell
    {
        [ProtoMember(1)]
        public int Level;
        [ProtoMember(2)]
        public int SubLevel;
        [ProtoMember(3)]
        public string Name;

        public long CastedTime = 0;
        public Object.Spell Spell;
    }

    [ProtoContract]
    public class Spells
    {
        public Player playerLink;
        public void Link()
        {
            foreach (var spl in LearnedSpells)
            {
                Object.Spell outs;
                if (Scripts.Spells.SpellList.TryGetValue(spl.Value.Name, out outs))
                {
                    spl.Value.Spell = outs;
                }
            }
        }

        [ProtoMember(1)]
        internal ConcurrentDictionary<int, LeafSpell> LearnedSpells = new ConcurrentDictionary<int, LeafSpell>();

        internal KeyValuePair<bool, int> FreeSlot
        {
            get
            {
                for (int x = 0; x < 40; x++)
                {
                    if (!LearnedSpells.ContainsKey(x))
                        return new KeyValuePair<bool, int>(true, x);
                }
                return new KeyValuePair<bool, int>(false, 0);
            }
        }

        internal LeafSpell GetSpell(int slot)
        {
            LeafSpell outs;
            if (LearnedSpells.TryGetValue(slot, out outs))
            {
                return outs;
            }
            return null;
        }

        internal KeyValuePair<int, LeafSpell> HasSpell(Object.Spell spell)
        {
            return LearnedSpells.Where(xe => xe.Value.Name == spell.Name).FirstOrDefault();
        }

        internal KeyValuePair<int, LeafSpell> HasSpell(string spell)
        {
            return LearnedSpells.Where(xe => xe.Value.Name == spell).FirstOrDefault();
        }

        internal bool DelayCost(LeafSpell spell)
        {
            if (playerLink.LastCast + playerLink.State.CastSpeed > World.World.tickcount.ElapsedMilliseconds)
                return false;

            var fullcost = spell.Spell.FullManaCost(playerLink, spell);
            if (playerLink.MPCur < fullcost)
                return false;

            if (Dice.Random(0, (50 * spell.SubLevel)) == 5)
            {
                spell.SubLevel++;
                playerLink.Spells.RedrawSpells();
            }

            playerLink.MPCur -= fullcost;
            playerLink.LastCast = World.World.tickcount.ElapsedMilliseconds;
            return true;
        }

        internal void Cast(LeafSpell spell)
        {
            if (!DelayCost(spell))
                return;

            playerLink.Position.CurMap.Events.OnCurvMagic(playerLink, 0, 0, spell.Spell.SpellSeq);
            spell.Spell.Cast(spell, playerLink, null, null);
        }

        internal void Cast(LeafSpell spell, Point2D tarloc)
        {
            if (playerLink.Position.DistanceTo(tarloc.X, tarloc.Y) > spell.Spell.Range)
                return;

            if (playerLink.Position.CurMap.MapType != World.Map.E_MapType.Safe)
                return;

            if (!DelayCost(spell))
                return;

            playerLink.Position.CurMap.Events.OnCurvMagic(playerLink, tarloc.X, tarloc.Y, spell.Spell.SpellSeq);
            spell.Spell.Cast(spell, playerLink, null, tarloc);
        }

        internal void Cast(LeafSpell spell, Object.Mobile tar)
        {
            if (tar is Object.Npc)
                return;
            if (tar is Object.Item)
                return;

            if (playerLink.Position.DistanceTo(tar.Position.X, tar.Position.Y) > spell.Spell.Range)
                return;

            if (!DelayCost(spell))
                return;

            playerLink.Position.CurMap.Events.OnCurvMagic(playerLink, tar.Position.X, tar.Position.Y, spell.Spell.SpellSeq);
            spell.Spell.Cast(spell, playerLink, tar, null);
        }

        internal bool Learn(Object.Spellbook spellb)
        {
            var fs = FreeSlot;
            if (!fs.Key)
                return false;
            if (!spellb.CanUse(playerLink))
            {
                playerLink.WriteWarn("You fail to learn " + spellb.Name + ".");
                return false;
            }

            var hasspell = HasSpell(spellb.SpellTaught);
            if (hasspell.Value != null)
            {
                if (playerLink.State.Level < hasspell.Value.Level * spellb.LvlReqPl + spellb.LevelReq)
                    return false;
                if (playerLink.State.Stats.StrTotal < hasspell.Value.Level * spellb.StrReqPl + spellb.StrReq)
                    return false;
                if (playerLink.State.Stats.DexTotal < hasspell.Value.Level * spellb.DexReqPl + spellb.DexReq)
                    return false;
                if (playerLink.State.Stats.MenTotal < hasspell.Value.Level * spellb.MenReqPl + spellb.MenReq)
                    return false;
                if (hasspell.Value.Level >= hasspell.Value.Spell.MaxLevel)
                    return false;

                hasspell.Value.Level++;
                playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(hasspell.Value, hasspell.Key).Compile());
            }
            else
            {
                var leafspell = new LeafSpell() { Level = 1, SubLevel = 0, Name = spellb.SpellTaught.Name, Spell = spellb.SpellTaught };
                LearnedSpells[fs.Value] = leafspell;
                playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(leafspell, fs.Value).Compile());
            }
            return true;
        }

        internal void Swap(int s1, int s2)
        {
            LeafSpell fromss;
            LeafSpell toss;
            if (LearnedSpells.TryGetValue(s1, out fromss))
            {
                if (LearnedSpells.TryGetValue(s2, out toss))
                {
                    LearnedSpells[s1] = toss;
                    LearnedSpells[s2] = fromss;
                    playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(toss, s1).Compile());
                    playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(fromss, s2).Compile());
                }
                else
                {
                    if (LearnedSpells.TryRemove(s1, out fromss))
                    {
                        if (LearnedSpells.TryAdd(s2, fromss))
                        {
                            playerLink.gameLink.Send(new Network.GameOutMessage.DeleteSlotMagic(s1).Compile());
                            playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(fromss, s2).Compile());
                        }
                    }
                }
            }
        }

        internal void Forget(int s)
        {
            LeafSpell outi;
            if (LearnedSpells.TryRemove(s, out outi))
            {
                playerLink.gameLink.Send(new Network.GameOutMessage.DeleteSlotMagic(s).Compile());
            }
        }

        internal void RedrawSpells()
        {
            foreach (var spel in LearnedSpells)
                playerLink.gameLink.Send(new Network.GameOutMessage.CreateSlotMagic(spel.Value, spel.Key).Compile());
        }
    }
}
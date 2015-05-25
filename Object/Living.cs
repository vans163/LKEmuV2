using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [ProtoContract]
    [ProtoInclude(13, typeof(Player.Player))]
    public class Living : Mobile
    {
        internal int _HP;
        internal virtual int HP
        {
            get
            {
                return _HP;
            }
            set
            {
                _HP = value;
            }
        }
        internal int _MP;
        internal virtual int MP
        {
            get
            {
                return _MP;
            }
            set
            {
                _MP = value;
            }
        }

        internal int HPCur
        {
            get
            {
				return Attribs [E_Attribute.HPCur];
            }
            set
            {
                if (value > HP)
                    value = HP;

                if (value < HPCur)
                {
                    if (value <= 0)
                    {
                        value = 1;
                        Die();
                    }
					Attribs [E_Attribute.HPCur] = value;
                    Position.CurMap.Events.OnTakeDamage(this);
                }
                else
                {
					Attribs [E_Attribute.HPCur] = value;
                }
                if (this is Player.Player)
                    (this as Player.Player).gameLink.Send(new Network.GameOutMessage.SetHP(this as Player.Player).Compile());
            }
        }

        internal int MPCur
        {
            get
            {
				return Attribs [E_Attribute.MPCur];
            }
            set
            {
                if (value != MPCur)
                {
                    if (value > MP)
                        value = MP;
					Attribs [E_Attribute.MPCur] = value;
                    if (this is Player.Player)
                    {
                        (this as Player.Player).gameLink.Send(new Network.GameOutMessage.SetMP(this as Player.Player).Compile());
                    }
                }
            }
        }
        public E_Race Race = E_Race.Human;

        public ConcurrentDictionary<string, LeafBuff> Buffs = new ConcurrentDictionary<string, LeafBuff>();

        public void SetBuff(Player.LeafSpell spell)
        {
            Buffs[spell.Name] = new LeafBuff(spell, spell.Spell.Duration(spell));
            spell.CastedTime = World.World.tickcount.ElapsedMilliseconds;
        }

        public void RemoveBuff(Player.LeafSpell spell)
        {
            LeafBuff outv;
            Buffs.TryRemove(spell.Name, out outv);
            Position.CurMap.Events.OnChgObjSprit(this);
        }

        public void RemoveBuff(string spell)
        {
            LeafBuff outv;
            Buffs.TryRemove(spell, out outv);
            Position.CurMap.Events.OnChgObjSprit(this);

        }

        public bool HasBuffEffect(BuffEffect buffe)
        {
            foreach (var b in Buffs)
            {
                if ((b.Value.Buff.Spell as Buff).BuffEffect == buffe)
                    return true;
            }
            return false;
        }

        public byte[] BuffArray
        {
            get
            {
                byte[] buffs = new byte[4] { 0, 0, 0, 0 };
                int itrx = 0;
                foreach (var buff in Buffs)
                {
                    if (buff.Value.Buff.Name == "MENTAL SWORD")
                        continue;
                    if (buff.Value.Buff.Spell.SpellSeq.OnCastSprite == 0)
                        continue;
                    if (itrx > 3)
                        break;

                    buffs[itrx++] = (byte)buff.Value.Buff.Spell.SpellSeq.OnCastSprite;
                }
                /* var aura = Equipped2.Where(xe => (xe.Value is script.item.IAura)).FirstOrDefault();
                 if (aura.Value != null)
                 {
                     buffs[3] = (byte)(aura.Value as script.item.IAura).Aura();
                 }*/

                return buffs;
            }
        }

        public void LoseHP(int amount)
        {
            if (HasBuffEffect(BuffEffect.ManaAsHp))
            {
                if (amount <= MPCur)
                {
                    MPCur -= amount;
                    amount = 0;
                }
                else
                {
                    amount = amount - MPCur;
                    MPCur = 0;
                }
            }

            HPCur -= amount;
        }

        public bool Died = false;
        public virtual void Die()
        {
            Died = true;
        }

        public virtual void TakeDamage(Living attackr)
        {
            float h = ((float)attackr.Hit / ((float)attackr.Hit + (float)Hit)) * 200;

            //Missed
            if (!(h >= 100 || new Random().Next(0, 100) < (int)h))
                return;

            int ACroll = AC;
            if (this is Player.Player)
                ACroll = Dice.Random(1 + (AC / 2), AC);

            int dam = attackr.Dam - ACroll;
            if (dam <= 0)
                dam = 1;

            LoseHP(dam);

            if (Died)
            {
                if (this is Monster && attackr is Player.Player)
                    (attackr as Player.Player).State.XP += (this as Monster).XPGranted;
            }
        }

        public virtual void TakeDamage(Living caster, Player.LeafSpell spell)
        {
            var dam = spell.Spell.Dam + spell.Spell.DamPl * spell.Level;
            var tdam = dam - AC;
            if (tdam <= 0)
                tdam = 1;

            LoseHP(tdam);

            if (Died)
            {
                if (this is Monster && caster is Player.Player)
                    (caster as Player.Player).State.XP += (this as Monster).XPGranted;
            }
        }

        internal long LastAction;
        internal long LastAttack;
        internal long LastWalk;
        internal long LastCast;
    }
}

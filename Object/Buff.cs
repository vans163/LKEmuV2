using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class LeafBuff
    {
        public Player.LeafSpell Buff;
        public int Duration = 0;

        public LeafBuff(Player.LeafSpell buff, int duration)
        {
            Buff = buff;
            Duration = duration;
        }

        public void Tick(Player.Player plyr)
        {
            Duration -= 100;
            if (Duration == 60000)
            {
                plyr.WriteWarn(string.Format("{0} magic remains 60 seconds.", Buff.Name));
            }
            else if (Duration == 10000)
            {
                plyr.WriteWarn(string.Format("{0} magic remains 10 seconds.", Buff.Name));

            }
            else if (Duration <= 0)
            {
                plyr.RemoveBuff(Buff.Name);
            }
        }
    }

    public class Buff : Spell
    {
        public Buff(string name, int icon, E_MagicType magType, SpellSequence spellseq)
            : base(name, icon, magType, spellseq)
        {
        }

        public int AC;
        public int ACpl;
        public float FAC;
        public float FACpl;
        public float FDam;
        public float FDampl;
        public int Hit;
        public int Hitpl;
        public int FHit;
        public int FHitpl;

        public int ACBonus(float baseac, int lvl)
        {
            if (FAC != 0)
            {
                return (int)(baseac * (FAC + FACpl * lvl));
            }
            else
                return AC + ACpl * lvl;
        }

        public int DamBonus(float basedam, int lvl)
        {
            if (FDam != 0)
            {
                return (int)(basedam * (FDam + FDampl * lvl));
            }
            else
                return Dam + DamPl * lvl;
        }

        public int HitBonus(int lvl)
        {
            return AC + ACpl * lvl;
        }

        public int BuffIcon;
        public BuffEffect BuffEffect;
    }

    public enum BuffEffect
    {
        ManaAsHp,
        Debuff,
        StoneCurse,
    }
}

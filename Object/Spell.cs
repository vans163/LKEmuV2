using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [Serializable]
    [ProtoContract]
    public class Spell
    {
        public Spell(string name, int icon, E_MagicType magType, SpellSequence spellseq)
        {
            this.Name = name;
            this.Icon = icon;
            this.magType = magType;
            this.SpellSeq = spellseq;
        }

        public Spell Clone()
        {
            var cop = new Spell(this.Name, this.Icon, this.magType,
                new SpellSequence(SpellSeq.OnCastSprite, SpellSeq.MovingSprite,
                    SpellSeq.OnImpactSprite, SpellSeq.Thickness, SpellSeq.Type, SpellSeq.Speed, SpellSeq.Streak));
            cop.Cast = Cast;
            foreach (var att in Attributes)
            {
                cop.Attributes.Add(att.Key, new Attribute() { SubKey = att.Value.SubKey, Value = att.Value.Value });
            }
            return cop;
        }

        [ProtoMember(1)]
        internal Dictionary<E_Attribute, Attribute> Attributes = new Dictionary<E_Attribute, Attribute>();

        internal int GAtt(E_Attribute att)
        {
            if (!Attributes.ContainsKey(att))
                Attributes[att] = new Attribute() { Value = 0 };
            return Attributes[att].Value;
        }

        internal void SAtt(E_Attribute att, int v)
        {
            if (!Attributes.ContainsKey(att))
                Attributes[att] = new Attribute();
            Attributes[att].Value = v;
        }

        [ProtoMember(2)]
        public string Name;
        public int Level
        {
            get { return GAtt(E_Attribute.Level); }
            set { SAtt(E_Attribute.Level, value); }
        }
        public int SubLevel
        {
            get { return GAtt(E_Attribute.SubLevel); }
            set { SAtt(E_Attribute.SubLevel, value); }
        }
        public int Icon;
        public E_MagicType magType;

        public int ClassReq;
        public int Dam;
        public int DamPl;
        public int ManaCost;
        public int ManaCostPl;
        public float FManaCost;
        public float FManaCostPl;
        public Func<Player.LeafSpell, int> Duration = new Func<Player.LeafSpell, int>((ls) => { return 120000 + ls.Level * 60000; });
        public int FullManaCost(Player.Player caster, Player.LeafSpell cspell)
        {
            int retv = 0;
            if (FManaCost != 0)
            {
                var fc = FManaCost - FManaCostPl * cspell.Level;
                retv = (int)(fc * caster.MP);
            }
            else
            {
                retv = ManaCost - ManaCostPl * cspell.Level;
            }
            return retv;
        }
        public int MaxLevel = 12;
        public int Range = 10;
        public SpellSequence SpellSeq;

        public Action<Player.LeafSpell, Player.Player, Mobile, Point2D> Cast =
            new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>((Player.LeafSpell s, Player.Player p, Mobile b, Point2D x) => { });
    }

    [Serializable]
    public class SpellSequence
    {
        public int OnCastSprite, MovingSprite, OnImpactSprite, Thickness, Speed, Streak, Type;

        public SpellSequence(int castsprite, int movingsprite, int impactsprite, int Thickness, int Type, int Speed, int Streak)
        {
            this.OnCastSprite = castsprite;
            this.MovingSprite = movingsprite;
            this.OnImpactSprite = impactsprite;
            this.Thickness = Thickness;
            this.Type = Type;
            this.Speed = Speed;
            this.Streak = Streak;
        }
    }
}

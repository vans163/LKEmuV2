using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Spell2
    {
        public Spell2(string name, int icon, E_MagicType magType, SpellSequence spellseq)
        {
            this.Name = name;
            this.Icon = icon;
            this.magType = magType;
            this.SpellSeq = spellseq;
        }
    }
}

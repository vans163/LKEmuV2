using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Spellbook : Item
    {
        public Spellbook()
        {
        }

        public Spellbook(string name, Spell spelltaught, int classreq = (int)Player.E_Class.Any)
        {
            SprId = 3;
            this.Name = name;
            spelltaught.ClassReq = classreq;
            this.SpellTaught = spelltaught;
            this.ClassReq = classreq;
        }

        Spell _SpellTaught;
        public Spell SpellTaught
        {
            get
            {
                if (_SpellTaught == null)
                {
                    _SpellTaught = (Scripts.Items.CreateItem(Name) as Spellbook).SpellTaught;
                }
                return _SpellTaught;
            }
            set
            {
                _SpellTaught = value;
            }
        }
        public int MenReqPl
        {
            get
            {
				return Attribs [E_Attribute.MenReqPl];
            }
            set
            {
				Attribs [E_Attribute.MenReqPl] = value;
            }
        }

        public int DexReqPl
        {
			get
			{
				return Attribs [E_Attribute.DexReqPl];
			}
			set
			{
				Attribs [E_Attribute.DexReqPl] = value;
			}
        }
			
        public int StrReqPl
        {
			get
			{
				return Attribs [E_Attribute.StrReqPl];
			}
			set
			{
				Attribs [E_Attribute.StrReqPl] = value;
			}
        }

        public int LvlReqPl
        {
			get
			{
				return Attribs [E_Attribute.LevelReqPl];
			}
			set
			{
				Attribs [E_Attribute.LevelReqPl] = value;
			}
        }
    }
}

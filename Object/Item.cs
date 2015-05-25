using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
	[ProtoContract]
	[ProtoInclude (14, typeof(Equipment))]
	[ProtoInclude (15, typeof(Spellbook))]
	[ProtoInclude (16, typeof(Consumable))]
	[ProtoInclude (17, typeof(Ore))]
	public class Item : Mobile
	{
		public bool Static = false;

		public Item ()
		{
		}

		public Item (string name, int sprid)
		{
			this.Name = name;
			this.SprId = sprid;
		}

		public long? DroppedTime;

		public override byte[] Appearance {
			get {
				byte[] ret = new byte[11];
				ret [0] = 2;
				ret [9] = (byte)SprId;
				return ret;
			}
		}

		internal int StackSize {
			get {
				return Attribs [E_Attribute.StackSize];
			}
			set {
				Attribs [E_Attribute.StackSize] = value;
			}
		}

		internal int StrReq {
			get {
				return Attribs [E_Attribute.StrReq];
			}
			set {
				Attribs [E_Attribute.StrReq] = value;
			}
		}

		internal int MenReq {
			get {
				return Attribs [E_Attribute.MenReq];
			}
			set {
				Attribs [E_Attribute.MenReq] = value;
			}
		}

		internal int DexReq {
			get {
				return Attribs [E_Attribute.DexReq];
			}
			set {
				Attribs [E_Attribute.DexReq] = value;
			}
		}

		internal int ClassReq {
			get {
				var classr = Attribs [E_Attribute.ClassReq];
				if (classr == 0) {
					Attribs [E_Attribute.ClassReq] = (int)Player.E_Class.Any;
					classr = (int)Player.E_Class.Any;
				}
				return classr;
			}
			set {
				Attribs [E_Attribute.ClassReq] = value;
			}
		}

		internal int LevelReq {
			get {
				return Attribs [E_Attribute.LevelReq];
			}
			set {
				Attribs [E_Attribute.LevelReq] = value;
			}
		}

		internal int ArmorStage {
			get {
				return Attribs [E_Attribute.ArmorStage];
			}
			set {
				Attribs [E_Attribute.ArmorStage] = value;
			}
		}

		internal int ArmorStageMage {
			get { return Attribs [E_Attribute.ArmorStageMage]; }
			set { Attribs [E_Attribute.ArmorStageMage] = value; }
		}

		int? _SellPrice;

		public int SellPrice {
			get {
				if (!_SellPrice.HasValue)
					_SellPrice = (int)(BuyPrice * 0.10f);
				return _SellPrice.Value;
			}
		}

		public int BuyPrice {
			get { return Attribs [E_Attribute.BuyPrice]; }
			set { Attribs [E_Attribute.BuyPrice] = value; }
		}

		public int Str {
			get { return Attribs [E_Attribute.Str]; }
			set { Attribs [E_Attribute.Str] = value; }
		}

		public int Men {
			get { return Attribs [E_Attribute.Men]; }
			set { Attribs [E_Attribute.Men] = value; }
		}

		public int Dex {
			get { return Attribs [E_Attribute.Dex]; }
			set { Attribs [E_Attribute.Dex] = value; }
		}

		public int Vit {
			get { return Attribs [E_Attribute.Vit]; }
			set { Attribs [E_Attribute.Vit] = value; }
		}

		public override int Dam {
			get {
				int dam_att = Attribs [E_Attribute.Dam];

				int bonus = 0;
				if (this is Equipment) {
					for (int x = 0; x < (this as Equipment).EquipmentGrade; x++)
						bonus += (int)(dam_att * 0.1);
				}
				return dam_att + bonus;
			}
			set {
				Attribs [E_Attribute.Dam] = value;
			}
		}

		public override int AC {
			get {
				int ac_att = Attribs [E_Attribute.AC];

				int bonus = 0;
				if (this is Equipment) {
					for (int x = 0; x < (this as Equipment).EquipmentGrade; x++)
						bonus += (int)(ac_att * 0.1);
				}
				return ac_att + bonus;
			}
			set {
				Attribs [E_Attribute.AC] = value;
			}
		}

		public override int Hit {
			get {
				return Attribs [E_Attribute.Hit];
			}
			set {
				Attribs [E_Attribute.Hit] = value;
			}
		}

		public int HP {
			get {
				return Attribs [E_Attribute.HP];
			}
			set {
				Attribs [E_Attribute.HP] = value;
			}
		}

		public int MP {
			get {
				return Attribs [E_Attribute.MP];
			}
			set {
				Attribs [E_Attribute.MP] = value;
			}
		}

		public int CastSpeed {
			get {
				return Attribs [E_Attribute.CastSpeed];
			}
			set {
				Attribs [E_Attribute.CastSpeed] = value;
			}
		}

		public string ClassReqString {
			get {
				StringBuilder sb = new StringBuilder ();
				if (ClassReq == 31)
					return "All";
				if ((ClassReq & (int)Player.E_Class.Beginner) != 0)
					sb.Append ("Beginner - ");
				if ((ClassReq & (int)Player.E_Class.Knight) != 0)
					sb.Append ("Knight - ");
				if ((ClassReq & (int)Player.E_Class.Swordsman) != 0)
					sb.Append ("Swordsman - ");
				if ((ClassReq & (int)Player.E_Class.Wizard) != 0)
					sb.Append ("Wizard - ");
				if ((ClassReq & (int)Player.E_Class.Shaman) != 0)
					sb.Append ("Shaman - ");
				sb.Remove (sb.Length - 3, 3);
				return sb.ToString ();
			}
		}

		public string TextStats {
			get {
				int[] reqPl = new int[3];
				if (this is Spellbook) {
					var sb = this as Spellbook;
					reqPl [0] = sb.StrReqPl;
					reqPl [1] = sb.MenReqPl;
					reqPl [2] = sb.DexReqPl;
				}

				StringBuilder ret = new StringBuilder ();
				ret.Append (Name + "\n\t");
				if (this is Spellbook) {
					var sb = this as Spellbook;
					if (StrReq != 0)
						ret.Append (string.Format ("Strength Required: {0} ({1})\n\t", StrReq, sb.StrReqPl));
					if (MenReq != 0)
						ret.Append (string.Format ("Mentality Required: {0} ({1})\n\t", MenReq, sb.MenReqPl));
					if (DexReq != 0)
						ret.Append (string.Format ("Dexterity Required: {0} ({1})\n\t", DexReq, sb.DexReqPl));
				} else {
					if (StrReq != 0)
						ret.Append (string.Format ("Strength Required: {0}\n\t", StrReq));
					if (MenReq != 0)
						ret.Append (string.Format ("Mentality Required: {0}\n\t", MenReq));
					if (DexReq != 0)
						ret.Append (string.Format ("Dexterity Required: {0}\n\t", DexReq));
				}
				if (Dam != 0)
					ret.Append ("Damage: " + Dam + "\n\t");
				if (AC != 0)
					ret.Append ("AC: " + AC + "\n\t");
				if (Hit != 0)
					ret.Append ("Hit: " + Hit + "\n\t");
				if (Str != 0)
					ret.Append ("Str: " + Str + "\n\t");
				if (Men != 0)
					ret.Append ("Men: " + Men + "\n\t");
				if (Dex != 0)
					ret.Append ("Dex: " + Dex + "\n\t");
				if (Vit != 0)
					ret.Append ("Vit: " + Vit + "\n\t");
				if (HP != 0)
					ret.Append ("HP: " + HP + "\n\t");
				if (MP != 0)
					ret.Append ("MP: " + MP + "\n\t");
				if (CastSpeed != 0)
					ret.Append ("Cast Speed: " + (float)(CastSpeed * 0.01) + "\n\t");
				//if (Stage != 0)
				//    ret += "Stage: " + Stage + "\n\t";
				if (ClassReq != 31)
					ret.Append ("Class Required: " + ClassReqString + "\n\t");
				if (LevelReq != 0)
					ret.Append ("Level Required: " + LevelReq + "\n\t");
				if (SellPrice != 0)
					ret.Append ("Sell Price: " + SellPrice + "\n\t");
				// if (DescText != null)
				//     ret += "Special: " + DescText + "\n\t";
				// if (FlavorText != null)
				//     ret += "\n\t  " + FlavorText + "\n\t";

				if (ret.Length > 0)
					ret.Remove (ret.Length - 2, 2);
				return ret.ToString ();
			}
		}

		internal bool CanUse (Player.Player tar)
		{
			if (!(this is Consumable || this is Equipment || this is Spellbook))
				return false;
			if ((ClassReq & (int)tar.Class) == 0)
				return false;
			if (StrReq > tar.State.Stats.StrTotal || DexReq > tar.State.Stats.DexTotal || MenReq > tar.State.Stats.MenTotal)
				return false;
			if (LevelReq > tar.State.Level)
				return false;

			return true;
		}

		public override string Name {
			get {
				if (StackSize > 0)
					return string.Format ("{0} : {1}", _Name, StackSize);
				return _Name;
			}
			set {
				_Name = value;
			}
		}
	}
}

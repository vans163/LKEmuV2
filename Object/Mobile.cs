using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
	[ProtoContract]
	[ProtoInclude (10, typeof(Living))]
	[ProtoInclude (12, typeof(Item))]
	public class Mobile : Object
	{
		[ProtoMember (1)]
		internal Position Position = new Position ();
		[ProtoMember (2)]
		internal Attributes Attribs = new Attributes ();
		//internal Dictionary<E_Attribute, Attribute> Attributes = new Dictionary<E_Attribute, Attribute>();

		internal int SprId {
			get {
				return Attribs [E_Attribute.SprId];
			}
			set {
				Attribs [E_Attribute.SprId] = value;
			}
		}

		internal Player.E_Color Color {
			get {
				return  (Player.E_Color)Attribs [E_Attribute.Color];
			}
			set {
				Attribs [E_Attribute.Color] = (int)value;
			}
		}

		internal int LightRadius {
			get {
				return Attribs [E_Attribute.LightRadius];
			}
			set {
				Attribs [E_Attribute.LightRadius] = value;
			}
		}

		internal int Transparency {
			get {
				return Attribs [E_Attribute.Transparency];
			}
			set {
				//if (_Transparency.Value > 0 && value == 0)
				//    Position.CurMap.Events.OnChgObjSprit(this);
				//else if (_Transparency.Value == 0 && value > 0)
				Position.CurMap.Events.OnChgObjSprit (this);
				Attribs [E_Attribute.Transparency] = value;
			}
		}
		/*
        public virtual Mobile Clone()
        {
            Mobile ret = new Mobile();
            if (this is Consumable)
            {
                ret = new Consumable();
                (ret as Consumable)._Consume = (this as Consumable)._Consume;
            }
            else if (this is Spellbook)
                ret = new Spellbook((this as Spellbook).SpellTaught, (this as Spellbook).ClassReq);
            else if (this is Equipment)
                ret = new Equipment((this as Equipment).EquipmentType);
            else
                ret = new Item();

            ret.Position.X = this.Position.X;
            ret.Position.Y = this.Position.Y;
            ret.Position.Face = this.Position.Face;
            ret.Position.CurMap = this.Position.CurMap;
            ret._Name = this._Name;
            ret.Attributes.Clear();
            foreach (var att in Attributes)
            {
                ret.Attributes.Add(att.Key, new Attribute() { SubKey = att.Value.SubKey, Value = att.Value.Value });
            }
            return ret;
        }

        public virtual Mobile Clone(Mobile ret)
        {
            ret.Position.X = this.Position.X;
            ret.Position.Y = this.Position.Y;
            ret.Position.Face = this.Position.Face;
            ret.Position.CurMap = this.Position.CurMap;
            ret._Name = this._Name;
            ret.Attributes.Clear();
            foreach (var att in Attributes)
            {
                ret.Attributes.Add(att.Key, new Attribute() { SubKey = att.Value.SubKey, Value = att.Value.Value });
            }
            return ret;
        }
        */
		public void Destroy ()
		{
			Position.CurMap.Exit (this);
		}

		[ProtoMember (4)]
		public string _Name = "Unknown";

		public virtual string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
			}
		}

		public virtual byte FrameType {
			get {
				return 1;
			}
		}

		public virtual byte[] Appearance {
			get {
				return new byte[11];
			}
		}

		int _AC;

		public virtual int AC {
			get {
				return _AC;
			}
			set {
				_AC = value;
			}
		}

		int _Hit;

		public virtual int Hit {
			get {
				return _Hit;
			}
			set {
				_Hit = value;
			}
		}

		int _Dam;

		public virtual int Dam {
			get {
				return _Dam;
			}
			set {
				_Dam = value;
			}
		}
	}
}

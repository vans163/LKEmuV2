using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [ProtoContract]
    public class Equipment : Item
    {
        public Equipment()
        {
        }

        public Equipment(E_ET type, string name, int sprid)
            : base(name, sprid)
        {
            EquipmentType = type;
        }

        internal E_ET EquipmentType
        {
            get
            {
				return (E_ET)Attribs [E_Attribute.EquipmentType];
            }
            set
            {
				Attribs [E_Attribute.EquipmentType] = (int)value;
            }
        }

        string _EQGradeStr;
        int _EQGradeInt = 0;
        internal string EQGradeStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_EQGradeStr) || _EQGradeInt != EquipmentGrade)
                {
                    _EQGradeInt = EquipmentGrade;
                    switch (EquipmentType)
                    {
                        case E_ET.Amulet:
                        case E_ET.Chest:
                        case E_ET.Helmet:
                        case E_ET.Ring:
                        case E_ET.Shield:
                            switch (EquipmentGrade)
                            {
                                case 0:
                                    _EQGradeStr = ""; break;
                                case 1:
                                    _EQGradeStr = "GRACEFUL"; break;
                                case 2:
                                    _EQGradeStr = "GLORIOUS"; break;
                                case 3:
                                    _EQGradeStr = "BRILLIANCE"; break;
                                case 4:
                                    _EQGradeStr = "HONESTY"; break;
                                case 5:
                                    _EQGradeStr = "VENERABLE"; break;
                                case 6:
                                    _EQGradeStr = "VALUABLE"; break;
                                case 7:
                                    _EQGradeStr = "LOYAL"; break;
                                case 8:
                                    _EQGradeStr = "HOLY"; break;
                                case 9:
                                    _EQGradeStr = "SACROSANT"; break;
                                case 10:
                                    _EQGradeStr = "GODLY"; break;
                            }
                            break;

                        default:
                            switch (EquipmentGrade)
                            {
                                case 0:
                                    _EQGradeStr = ""; break;
                                case 1:
                                    _EQGradeStr = "SAVAGE"; break;
                                case 2:
                                    _EQGradeStr = "DEADLY"; break;
                                case 3:
                                    _EQGradeStr = "REVENGEFUL"; break;
                                case 4:
                                    _EQGradeStr = "DREADFUL"; break;
                                case 5:
                                    _EQGradeStr = "ANGRY"; break;
                                case 6:
                                    _EQGradeStr = "AWESOME"; break;
                                case 7:
                                    _EQGradeStr = "TREMENDOUS"; break;
                                case 8:
                                    _EQGradeStr = "BLOODY"; break;
                                case 9:
                                    _EQGradeStr = "INVINCIBLE"; break;
                                case 10:
                                    _EQGradeStr = "ANGELIC"; break;
                            }
                            break;
                    }
                }
                return _EQGradeStr;
            }
        }

        internal int EquipmentGrade
        {
            get
            {
				return Attribs [E_Attribute.EquipmentGrade];
            }
            set
            {
				Attribs [E_Attribute.EquipmentGrade] = value;
            }
        }

        int? _EquipSlot;
        public int EquipSlot
        {
            get
            {
                if (!_EquipSlot.HasValue)
                {
                    switch (EquipmentType)
                    {
                        case E_ET.Helmet:
                            _EquipSlot = 0;
                            break;
                        case E_ET.Amulet:
                            _EquipSlot = 1;
                            break;
                        case E_ET.Chest:
                            _EquipSlot = 3;
                            break;
                        case E_ET.Shield:
                            _EquipSlot = 4;
                            break;
                        case E_ET.Ring:
                            _EquipSlot = 5;
                            break;
                        case E_ET.Axe:
                        case E_ET.Axe2H:
                        case E_ET.Sword:
                        case E_ET.Sword2H:
                        case E_ET.Cane:
                        case E_ET.Cane2:
                        case E_ET.Hammer:
                        case E_ET.Spear:
                        case E_ET.Spear2:
                            _EquipSlot = 2;
                            break;
                        default:
                            Log.LogLine("wtf no equipslot");
                            _EquipSlot = -1;
                            break;
                    }
                }
                return _EquipSlot.Value;
            }
        }

        public override string Name
        {
            get
            {
                if (EquipmentGrade == 0)
                    return _Name;
                return string.Format("{0} {1}", EQGradeStr, _Name);
            }
            set
            {
                base.Name = value;
            }
        }
    }
}

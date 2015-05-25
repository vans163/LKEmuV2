using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    public enum ColorCodes
    {
        clBlack = 0,
        clMedGray = 0x14a5, //a514
        clGray = 0x1084,
        clWhisper = 0xadb5, //c6f8
        clLime = 0x07E0,
        clSilver = 0xC618,
        clYellow = 0xF7A0, //FFe0
        clBlue = 0x001F,
        clWhite = 0xFFFF,
        clCream = 0xFFDE,
        clGrayText = 0x6D6B,
        clMenuHighlight = 0x34DF,
    }

    public enum E_Race
    {
        Magical = 1,
        Animal = 2,
        Undead = 4,
        Demon = 8,
        Human = 16
    }

    [ProtoContract]
    public enum E_Attribute
    {
        Str = 1,
        Men = 2,
        Dex = 3, 
        Vit = 4,
        Extra = 5,

        Hit = 6,
        Dam = 7,
        AC = 8,

        HP = 9,
        MP = 10,
        HPCur = 11,
        MPCur = 12,

        PK = 13,
        CastSpeed = 14,
        Level = 15,
        SubLevel = 16,
        DamPl = 17,
        ManaCost = 18,
        ManaCostPl = 19,
        FManaCost = 20,
        FManaCostPl = 21,

        Color = 22,
        LightRadius = 23,
        Transparency = 24,

        BuyPrice = 25,
        SprId,
        EquipmentType,
        ArmorStage,
        LevelReq,
        StrReq,
        MenReq,
        DexReq,
        ClassReq,
        StrReqPl,
        MenReqPl,
        DexReqPl,
        LevelReqPl,
        Promo,
        Gold,
        XP,
        MinerLevel,
        MinerXP,
        SmithLevel,
        SmithXP,
        ProfessionSuit,
        StackSize,
        EquipmentGrade,
        ArmorStageMage,
    }

    public enum E_MagicType
    {
        Coord = 2, //1B-01-00-1B-00-13-00
        Target = 3, //18-01-00-00-00-00-00-1B-00-1E-00
        Casted = 4, //19-01-00 
        Target2 = 5 //3D-01-00-00-00-00-00-1A-00-13-00
    }

    public enum E_ET
    {
        Shield = 1,
        Chest,
        Helmet,
        Ring,
        Amulet,

        Sword = 6,
        Sword2H,
        Axe,
        Axe2H,
        Cane,
        Spear,
        Hammer,
        Cane2,
        Spear2
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Network
{
    public class GameProtocol
    {
        public enum OPCodes
        {
            OnConnect= 0x02,
            KeepAlive = 0x34,
            Identify = 0x37,
            LeonFind = 0x3A,
            LeonEntrust = 0x36,
            Cast3D = 0x3D,
            Cast19 = 0x19,
            Cast18 = 0x18,
            Swing = 0x17,
            NPCInteract = 0x45,
            NPCBuy = 0x2B,
            NPCSell = 0x35,

            InventoryUse = 0x00,
            InventoryDrop = 0x20,
            PickUp = 0x1F,
            InventoryEquip = 0x1E,
            InventoryUnequip = 0x23,
            InventorySwap = 0x25,
            InventoryDragDrop = 0x24,

            SendChat = 0x16,
            MovementFace = 0x14,
            MovementStep = 0x15,

            AddStr = 0x2C,
            AddMen = 0x2D,
            AddDex = 0x2E,
            AddVit = 0x2F,

            Login = 0x03,
            DeleteMagic = 0x49,
            SwapMagic = 0x26,
            PlayMusic = 0xFF,
            GetRank = 0x0A,
            GetBoard = 0x30,
            GetBoardDetail = 0x31,
            GetBoardDetailSys = 0x3F,
            GetSysBoard = 0x3E,
            WriteBoardNotice = 0x32,
            WriteBoardNoticeSys = 0x40,

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Network
{
    public class WriteBoardNotice : GameMessage
    {
        public string Title;
        public string Content;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            List<string> strs = new List<string>();

            StringBuilder sb = new StringBuilder();
            int itr = 1;
            int c;
            while (itr < msg.Length)
            {
                c = msg[itr++];
                if (c == 0)
                {
                    if (sb.Length > 0)
                    {
                        strs.Add(sb.ToString());
                        sb.Clear();
                    }
                    continue;
                }
                sb.Append((char)c);
            }

            Title = strs[0];
            foreach (var str in strs)
            {
                if (str.Length > 10)
                {
                    if (str == Title)
                        continue;
                    Content = str;
                    break;
                }
            }
          //  int msglen = ReadInt16(msg, 1 + (Title.Length + 1) + 16 + 2);
          //  Content = ReadString(msg, 1 + (Title.Length + 1) + 16, msglen);

            return this;
        }
    }

    public class GetBoardDetail : GameMessage
    {
        public int Id;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Id = ReadInt32(msg, 1);
            return this;
        }
    }

    public class GetSysBoard : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class GetBoard : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class GetRank : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class LeonEntrust : GameMessage
    {
        public int Slot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Slot = msg[1];
            return this;
        }
    }

    public class LeonFind : GameMessage
    {
        public int Slot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Slot = msg[1];
            return this;
        }
    }

    public class NPCSell : GameMessage
    {
        public int SellSlot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            SellSlot = msg[1];
            return this;
        }
    }

    public class NPCBuy : GameMessage
    {
        public int NPCID;
        public int BuySlot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            NPCID = ReadInt32(msg, 1);
            BuySlot = msg[5];
            return this;
        }
    }

    public class NPCInteract : GameMessage
    {
        public int NPCID;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            NPCID = ReadInt32(msg, 1);
            return this;
        }
    }

    public class SpellCastTarget2 : GameMessage
    {
        public int SpellSlot;
        public int TargetId;
        public int TarX;
        public int TarY;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            SpellSlot = ReadInt16(msg, 1);
            TargetId = ReadInt32(msg, 3);
            TarX = ReadInt16(msg, 7);
            TarY = ReadInt16(msg, 9);
            return this;
        }
    }

    public class SpellCastTarget : GameMessage
    {
        public int SpellSlot;
        public int TargetId;
        public int TarX;
        public int TarY;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            SpellSlot = ReadInt16(msg, 1);
            TargetId = ReadInt32(msg, 3);
            TarX = ReadInt16(msg, 7);
            TarY = ReadInt16(msg, 9);
            return this;
        }
    }

    public class SpellCastNoTarget : GameMessage
    {
        public int SpellSlot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            SpellSlot = msg[1];
            return this;
        }
    }

    public class SpellDelete : GameMessage
    {
        public int FromSlot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            FromSlot = msg[1];
            return this;
        }
    }

    public class SpellSwap : GameMessage
    {
        public int FromSlot;
        public int ToSlot = 0;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            FromSlot = msg[1];
            if (msg.Length >= 4)
                ToSlot = msg[3];
            return this;
        }
    }

    public class InventoryDragDrop : GameMessage
    {
        public int Slot;
        public int TargetId;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Slot = msg[1];
            TargetId = ReadInt32(msg, 3);
            return this;
        }
    }

    public class InventorySwap : GameMessage
    {
        public int FromSlot;
        public int ToSlot = 0;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            FromSlot = msg[1];
            if (msg.Length >= 4)
                ToSlot = msg[3];
            return this;
        }
    }

    public class InventoryUnequip : GameMessage
    {
        public int EquipSlot;
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            EquipSlot = msg[1];
            return this;
        }
    }

    public class InventoryUse : GameMessage
    {
        public int InvSlot;
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            InvSlot = msg[1];
            return this;
        }
    }

    public class SendChat : GameMessage
    {
        public string Message;
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];

            StringBuilder sb = new StringBuilder();
            int itr = 3;
            int c;
            while (itr < msg.Length && (c = msg[itr++]) != 0)
                sb.Append((char)c);
            Message = sb.ToString();
            return this;
        }
    }

    public class AddStr : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class AddMen : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class AddDex : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class AddVit : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class InventoryDrop : GameMessage
    {
        public int InvSlot;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            InvSlot = msg[1];
            return this;
        }
    }

    public class PickUp : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];

            return this;
        }
    }

    public class Swing : GameMessage
    {
        public int Direction;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Direction = msg[1];
            return this;
        }
    }

    public class MovementStep : GameMessage
    {
        public int Face;
        public int X;
        public int Y;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Face = ReadInt16(msg, 1);
            X = ReadInt16(msg, 3);
            Y = ReadInt16(msg, 5);

            return this;
        }
    }

    public class MovementFace : GameMessage
    {
        public int Face;
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            Face = msg[1];
            return this;
        }
    }

    public class KeepAlive : GameMessage
    {
        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            return this;
        }
    }

    public class Blob : GameMessage
    {
        public byte[] data;

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];
            data = msg;
            return this;
        }
    }

    public class Login : GameMessage
    {
        public string Username;
        public string Password;

        public enum LoginResponse
        {
            CloseLogin = 1,
            WrongPass = 2,
            WrongId = 3,
            WrongId2 = 4,
            AlreadyOnline = 5,
            NewPlayer = 6
        }

        public override GameMessage Deserialize(byte[] msg)
        {
            OPCode = msg[0];

            byte[] user = new byte[10];
            byte[] pass = new byte[10];
            Buffer.BlockCopy(msg, 1, user, 0, 10);
            Buffer.BlockCopy(msg, 33, pass, 0, 10);

            int usercnt = 0, passcnt = 0;
            for (int x = 0; x < 10; x++)
            {
                if (user[x] == 00)
                    break;
                usercnt++;
            }
            for (int x = 0; x < 10; x++)
            {
                if (pass[x] == 00)
                    break;
                passcnt++;
            }

            Username = Encoding.ASCII.GetString(user, 0, usercnt).Trim();
            Password = Encoding.ASCII.GetString(pass, 0, passcnt);

            return this;
        }
    }

    public class GameMessage
    {
        public int OPCode;

        public virtual byte[] Serialize()
        {
            return null;
        }

        public virtual GameMessage Deserialize(byte[] msg)
        {
            return this;
        }

        public short ReadInt16(byte[] buf, int offset)
        {
            if (offset >= buf.Length)
                return 0;
            if (offset + 1 >= buf.Length)
                return (short)buf[offset];
            return (short)((buf[offset]) | buf[offset + 1] << 8);
        }

        public int ReadInt32(byte[] buf, int offset)
        {
            if (offset + 1 >= buf.Length)
                return buf[offset];
            else if (offset + 2 >= buf.Length)
                return buf[offset] | buf[offset + 1] << 8;
            else if (offset + 3 >= buf.Length)
                return buf[offset] | buf[offset + 1] << 8 | buf[offset + 2] << 16;
            else
                return buf[offset] | buf[offset + 1] << 8 | buf[offset + 2] << 16 | buf[offset + 3] << 24;
        }

        public string ReadStringNull(byte[] msg, int start)
        {
            StringBuilder sb = new StringBuilder();
            int itr = start;
            int c;
            while (itr < msg.Length && (c = msg[itr++]) != 0)
                sb.Append((char)c);
            return sb.ToString();
        }

        public string ReadString(byte[] msg, int start, int bytestoread)
        {
            StringBuilder sb = new StringBuilder();
            int itr = start;
            int c;
            while (itr < bytestoread + start)
            {
                c = msg[itr++];
                sb.Append((char)c);
            }
            return sb.ToString();
        }
    }
}

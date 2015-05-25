using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Network
{
    public class GamePacketBuffer
    {
        public int Position = 0;

        private byte[] _data = new byte[4096];
        private int _length = 0;

        public GameMessage GetNextPacket()
        {
            int skip = Position;
            while (Position != _length)
            {
                //if (_data[Position] == 0x0A && Position - 1 < 0)
                //    System.Diagnostics.Debugger.Break();
                if (_data[Position] == 0x0A)
                    if ((Position - 1) >= 0 && _data[Position - 1] == 0x2E)
                    {
                        //Fix this
                        var msg = _data.Skip(skip).Take(Position - skip + 1).ToArray();
                        var decrypted = Decrypt(msg);
                        var parsed = ParseMessage(decrypted);

                        Position++;
                        return parsed;
                    }
                Position++;
            }
            return null;
        }

        public GameMessage ParseMessage(byte[] bufmsg)
        {
            switch (bufmsg[0])
            {
                case (byte)GameProtocol.OPCodes.Login:
                    return new Login().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.KeepAlive:
                    return new KeepAlive().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.MovementStep:
                    return new MovementStep().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.MovementFace:
                    return new MovementFace().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.Swing:
                    return new Swing().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.PickUp:
                    return new PickUp().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.InventoryDrop:
                    return new InventoryDrop().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.AddStr:
                    return new AddStr().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.AddMen:
                    return new AddMen().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.AddDex:
                    return new AddDex().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.AddVit:
                    return new AddVit().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.SendChat:
                    return new SendChat().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.InventoryEquip:
                    return new InventoryUse().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.InventorySwap:
                    return new InventorySwap().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.InventoryUnequip:
                    return new InventoryUnequip().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.InventoryDragDrop:
                    return new InventoryDragDrop().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.SwapMagic:
                    return new SpellSwap().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.DeleteMagic:
                    return new SpellDelete().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.Cast18:
                    return new SpellCastTarget().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.Cast19:
                    return new SpellCastNoTarget().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.Cast3D:
                    return new SpellCastTarget2().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.NPCInteract:
                    return new NPCInteract().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.NPCBuy:
                    return new NPCBuy().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.NPCSell:
                    return new NPCSell().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.LeonFind:
                    return new LeonFind().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.LeonEntrust:
                    return new LeonEntrust().Deserialize(bufmsg);

                case (byte)GameProtocol.OPCodes.GetRank:
                    return new GetRank().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.GetBoard:
                    return new GetBoard().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.GetSysBoard:
                    return new GetSysBoard().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.GetBoardDetail:
                case (byte)GameProtocol.OPCodes.GetBoardDetailSys:
                    return new GetBoardDetail().Deserialize(bufmsg);
                case (byte)GameProtocol.OPCodes.WriteBoardNotice:
                case (byte)GameProtocol.OPCodes.WriteBoardNoticeSys:
                    return new WriteBoardNotice().Deserialize(bufmsg);

                default:
                    Log.LogLine(string.Format("OPCode: {0:X} unknown", bufmsg[0]));
                    return new Blob().Deserialize(bufmsg);
            }
        }

        public void Consume()
        {
            Array.Copy(_data, Position, _data, 0, _length - Position);

            _length = _length - Position;
            Position = 0;
        }

        public void Append(byte[] msg, int offset, int bytestrans)
        {
            var datalen = _data.Length;
            if (datalen < _length + bytestrans)
                Array.Resize(ref _data, datalen + bytestrans);

            Array.Copy(msg, offset, _data, _length, bytestrans);
            _length = _length + bytestrans;
        }

        public byte[] Decrypt(Byte[] data)
        {
            Byte[] ret = new Byte[512];
            List<Byte> temp = new List<Byte>();

            int mLoopItr = 0;
            int loop3 = 0;
            byte var_f, var_e, var_d, var_c, var_b, var_a;

            mLoopItr = (data.Count() - 1) >> 2;
            for (int x = 0; x < (data.Count() - 1); x++)
            {
                if (data[x] == 0x2E)
                    break;
                data[x] -= 0x3B;
            }

            for (int x = 0; x < mLoopItr; x++)
            {
                var_d = data[loop3];
                try { var_c = data[loop3 + 1]; }
                catch { var_c = 0; }
                try { var_b = data[loop3 + 2]; }
                catch { var_b = 0; }
                try { var_a = data[loop3 + 3]; }
                catch { var_a = 0; }

                var_e = Convert.ToByte((var_d << 2) & 0xFF);
                var_f = Convert.ToByte(var_c >> 4);
                // ret[x * 3] = Convert.ToByte(var_e | var_f);
                temp.Add(Convert.ToByte(var_e | var_f));
                var_e = Convert.ToByte((var_c << 4) & 0xFF);
                var_f = Convert.ToByte(var_b >> 2);
                //    ret[x * 3 + 1] = Convert.ToByte(var_e | var_f);
                temp.Add(Convert.ToByte(var_e | var_f));
                var_e = Convert.ToByte((var_b << 6) & 0xFF);
                var_f = var_a;
                //  ret[x * 3 + 2] = Convert.ToByte(var_e | var_f);
                temp.Add(Convert.ToByte(var_e | var_f));

                loop3 += 4;
            }
            //     int size = Array.IndexOf<Byte>(ret, 0);
            //   ret[size] = 0x00;
            //    Array.Resize(ref ret, size); //Last DWORD byte

            var i = temp.Count - 1;
            while (temp[i] == 0)
            {
                if (temp[0] == 0 && temp[1] == 0)
                {
                    i = 1; break;
                }
                i--;
            }
            temp = temp.Take(i + 2).ToList<Byte>();

            return temp.ToArray<Byte>();
        }
    }
}

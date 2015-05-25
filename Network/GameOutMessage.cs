using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LKCamelotV2.Network
{
    public class GameOutMessage
    {
        #region Objects
        public sealed class CastAnimation : Packet
        {
            public CastAnimation(int objId, int face)
                : base(0x27)
            {
                Write(objId);
                Write((short)face);
            }
        }

        public sealed class CurveMagic : Packet
        {
            public CurveMagic(int objId, int X, int Y, Object.SpellSequence spellani)
                : base(0x28)
            {
                Write(objId);
                Write((short)X);
                Write((short)Y);
                Write((byte)0);
                Write((byte)0);
                Write((byte)spellani.OnCastSprite);
                Write((byte)spellani.MovingSprite);
                Write((byte)spellani.OnImpactSprite);
                Fill(7);
                //   for (int i = 0; i < 7; i++) msg.Add(0x12);
                Write((byte)spellani.Thickness);
                Write((byte)spellani.Type);
                Write((byte)spellani.Speed);
                Write((byte)spellani.Streak);
            }
        }

        public sealed class CreateNPC : Packet
        {
            public CreateNPC(Object.Npc npc)
                : base(0x3A)
            {
                Write((int)npc.objId);
                Write((short)1); //facedir
                Write((short)npc.Position.X);
                Write((short)npc.Position.Y);
                Write(npc.Appearance, 0, 11);
                Fill(3);
                Write((byte)npc.aSpeed);
                Fill(1);
                Write((byte)npc.aFrames);
                Fill(1);
               // Fill(18);
               // Write((short)npc.Name.Length);
               // WriteAsciiNull(npc.Name);
            }
        }

        public sealed class CreateMobile : Packet
        {
            public CreateMobile(Object.Mobile mobile)
                : base(0x1D)
            {
                Write((int)mobile.objId);
                Write((short)mobile.Position.Face);
                Write((short)mobile.Position.X);
                Write((short)mobile.Position.Y);

                Write(mobile.Appearance, 0, 11);

                Write((byte)mobile.Color); // color
                Write((byte)0x18); //thickness
                Write((byte)0);//uk
                Write((byte)mobile.Transparency);//1byte transp

                if (mobile is Object.Living)
                    Write((mobile as Object.Living).BuffArray, 0, 4);
                else
                    Fill(4); //buffs
                Write((byte)0x0); //uk 
                Write((byte)0x0); //uk
                Write((byte)0x0); //uk
                Write((byte)0x0); //uk

                Write((byte)0x01);
                WriteAsciiNull(mobile.Name);
            }
        }

        public sealed class CreateMineral : Packet
        {
            public CreateMineral(Object.Mobile mobile)
                : base(0x3D)
            {
                Write((int)mobile.objId);
                Write((short)1);
                Write((short)mobile.Position.X);
                Write((short)mobile.Position.Y);

                Write(mobile.Appearance, 0, 11);

                if (mobile is Object.Craft)
                {
                    var craft = mobile as Object.Craft;
                    Write((byte)craft.Speed); //speed
                    Write((byte)0);
                    Write((byte)craft.Frames);//frames
                    Write((byte)craft.StartFrame);//start frame
                }
                else
                    Fill(4);

                Fill(20);
                Write((short)mobile.Name.Length);
                WriteAsciiNull(mobile.Name);
            }
        }

        public sealed class ChangeObjectSprite : Packet
        {
            public ChangeObjectSprite(Object.Mobile mobile)
                : base(0x26)
            {
                Write((int)mobile.objId);
                Write(mobile.Appearance, 0, 11);
                Write((byte)mobile.Color);
            }
        }

        public sealed class DeleteObject : Packet
        {
            public DeleteObject(int objId)
                : base(0x1E)
            {
                Write((int)objId);
            }
        }

        public sealed class HitAnimation : Packet
        {
            public HitAnimation(int ser, byte color)
                : base(0x23)
            {
                Write((int)ser);
                Write((byte)color); //0xCB max, 0xCC blue
            }
        }

        public sealed class ChangeFace : Packet
        {
            public ChangeFace(int ser, int face)
                : base(0x22)
            {
                Write((int)ser);
                Write((short)face);
            }
        }

        public sealed class SwingAnimation : Packet
        {
            public SwingAnimation(int ser, int face)
                : base(0x21)
            {
                Write((int)ser);
                Write((short)face);
            }
        }

        public sealed class MoveSpriteWalk : Packet
        {
            public MoveSpriteWalk(int ser, int face, int x, int y)
                : base(0x1f)
            {
                Write((int)ser);
                Write((short)face);
                Write((short)x);
                Write((short)y);
            }
        }

        public sealed class MoveSpriteTele : Packet
        {
            public MoveSpriteTele(int ser, int face, int x, int y)
                : base(0x1c)
            {
                Write((int)ser);
                Write((short)face);
                Write((short)x);
                Write((short)y);
            }
        }

        public sealed class SetObjectEffects : Packet
        {
            public SetObjectEffects(Object.Mobile mobile)
                : base(0x34)
            {
                //  SetObjectEffects = 0x34; // 0x34 4id 1light 1trans 4byffs 1staticmagic
                Write(mobile.objId);
                Write((byte)mobile.LightRadius);
                Write((byte)mobile.Transparency);
                if (mobile is Object.Living)
                {
                    Write((mobile as Object.Living).BuffArray, 0, 4);

                    if ((mobile as Object.Living).Buffs.Values.Where(xe => xe.Buff.Name == "MENTAL SWORD").FirstOrDefault() != null)
                        Write((byte)0x63); //move with char
                    else
                        Write((byte)0); //move with char
                }
                else
                    Fill(5);

                Write((byte)mobile.FrameType); //sprite frame type
                Write((byte)0); // speed
                Write((byte)1);
            }
        }

        public sealed class SetMagicEffect : Packet
        {
            public SetMagicEffect(Object.Mobile mobile, int magicid)
                : base(0x25)
            {
                Write(mobile.objId);
                Write((byte)magicid);
            }
        }

        public sealed class MagicMotion : Packet
        {
            public MagicMotion(Object.Mobile mobile, int dir)
                : base(0x27)
            {
                Write(mobile.objId);
                Write((byte)dir);
            }
        }

        #endregion
        #region Player
        public sealed class CloseAll : Packet
        {
            public CloseAll(Player.Player play)
                : base(0x36)
            {
            }
        }

        public sealed class SetHP : Packet
        {
            public SetHP(Player.Player play)
                : base(0x10)
            {
                Write((ushort)play.HPCur);
            }
        }

        public sealed class SetMP : Packet
        {
            public SetMP(Player.Player play)
                : base(0x11)
            {
                Write((ushort)play.MPCur);
            }
        }
        public sealed class GoldChange : Packet
        {
            public GoldChange(int amount)
                : base(0x12)
            {
                Write((int)amount);
            }
        }
        public sealed class XPChange : Packet
        {
            public XPChange(int amount)
                : base(0x13)
            {
                Write((int)amount);
            }
        }
        public sealed class AddItemToInventory : Packet
        {
            public AddItemToInventory(Object.Item item, int slot)
                : base(0x17)
            {
                Write((byte)slot);
                Write((short)item.SprId);
                Write((byte)02);
                string txtstats = item.TextStats;
                Write((short)txtstats.Length);
                WriteAsciiNull(txtstats);
            }
        }
        public sealed class DeleteItemSlot : Packet
        {
            public DeleteItemSlot(int slot)
                : base(0x18)
            {
                Write((byte)slot);
            }
        }
        public sealed class EquipItem : Packet
        {
            public EquipItem(Object.Equipment item)
                : base(0x15)
            {
                Write((byte)item.EquipSlot);
                WriteAsciiFixed(item.Name, 32);
                Write((byte)0);
                Write((short)item.SprId);
            }
        }

        public sealed class DeleteEquipItem : Packet
        {
            public DeleteEquipItem(int eqslot)
                : base(0x16)
            {
                Write((byte)eqslot);
            }
        }

        public sealed class AddItemToEntrust : Packet
        {
            public AddItemToEntrust(Object.Item item, int slot)
                : base(0x31)
            {
                Write((byte)slot);
                Write((short)item.SprId);
                Write((byte)00);
                string txtstats = item.TextStats;
                Write((short)txtstats.Length);
                WriteAsciiNull(txtstats);
            }
        }

        public sealed class DeleteEntrustSlot : Packet
        {
            public DeleteEntrustSlot(int slot)
                : base(0x32)
            {
                Write((byte)slot);
            }
        }

        public sealed class CreateSlotMagic : Packet
        {
            public CreateSlotMagic(Player.LeafSpell spell, int slot)
                : base(0x19)
            {
                Write((byte)slot);
                Write((byte)spell.Spell.magType);
                Write((byte)spell.Level);
                Write((short)spell.Spell.Icon);
                Fill(3);
                Write((byte)1);
                WriteAsciiNull(spell.Name + " :S" + spell.SubLevel);
            }
        }
        public sealed class DeleteSlotMagic : Packet
        {
            public DeleteSlotMagic(int slot)
                : base(0x1A)
            {
                Write((byte)slot);
            }
        }
        public sealed class SpawnShopGump : Packet
        {
            public SpawnShopGump(Object.GUMP shop)
                : base(0x3E)
            {
                Write(shop.ID);
                Write((byte)shop.SellList.Count());
                Write(shop.Titlec);
                Write(shop.Boxc);
                Fill(1);
                Write(shop.Time);
                Write(shop.ItemIDArray, 0, 32);
                WriteAsciiFixed(shop.ShopName, 32);
                Write((short)shop.ItemNameString.Length);
                WriteAsciiNull(shop.ItemNameString);
            }
        }

        public sealed class AutoMove : Packet
        {
            public AutoMove(int x, int y)
                : base(0x3F)
            {
                Write((short)x);
                Write((short)y);
            }
        }

        /*
         * 0x41 : Attributes ( Miner, Black smith, Helper, PK count )
   - short : string length
   - byte[] : string   (ex. "Helper : 5,Miner:L5:100,SM:L2:10,PK:1" )
         * */
        public sealed class SetProfessions : Packet
        {
            public SetProfessions(string text)
                : base(0x41)
            {
                Write((short)text.Length);
                WriteAsciiNull(text);
            }
        }

        public sealed class ExchangeBox : Packet //Korea online?
        {
            public ExchangeBox(string text)
                : base(0x47)
            {
/*   - byte[4] : item index number ( me )
   - byte[4] : item index number ( you )
   - byte[2] : kn
   - int : gold (me)
   - int : gold (you)
   - byte : chacker
   - byte : chacker
   - short : string length
   - byte[] : string (ex. "User1Name,User2Name")*/
            }
        }

        public sealed class GoldBox : Packet //Korea online?
        {
            public GoldBox(string text)
                : base(0x46)
            {
/*   - int : serial
   - byte : kn
   - byte : kn
   - int : gold
   - short : string length
   - byte[] : string*/
            }
        }

        public sealed class CancelExchange : Packet //Korea online?
        {
            public CancelExchange(string text)
                : base(0x48)
            {
                /*   - byte : kn*/
            }
        }
        #endregion
        #region Login
        public sealed class CloseLogin : Packet
        {
            public CloseLogin()
                : base(0x04)
            {
                Write((byte)0x02);
                Write((byte)0x00);
            }
        }

        public sealed class WrongPass : Packet
        {
            public WrongPass()
                : base(0x05)
            {
                Write((byte)0xff);
                Write((byte)0xff);
            }
        }

        public sealed class WrongId : Packet
        {
            public WrongId()
                : base(0x0A)
            {
                Write((byte)0xff);
                Write((byte)0xff);
            }
        }

        public sealed class AlreadyOnline : Packet
        {
            public AlreadyOnline()
                : base(0x07)
            {
                Write((byte)0xff);
                Write((byte)0xff);
            }
        }
        #endregion
        #region Loading
        public sealed class SetBrightness : Packet
        {
            public SetBrightness(int amt)
                : base(0x39)
            {
                Write((byte)amt);
            }
        }

        public sealed class LoadTiles : Packet
        {
            public LoadTiles(World.Map map)
                : base(0x35)
            {
                Write((short)map.MapTiles.Length);
                WriteAsciiNull(map.MapTiles);
            }
        }

        public sealed class LoadWorld : Packet
        {
            public LoadWorld(Player.Player player, World.Map map)
                : base(0x1B)
            {
                Write((byte)map.Time);
                Write((int)player.objId);
                Write((byte)2);
                Fill(3);
                Write((short)player.Position.Face);
                Write((short)player.Position.X);
                Write((short)player.Position.Y);

                Write(player.Appearance, 0, 11);
                // Write((byte)1);
                // Fill(8);
                // Write((short)15); //sprite

                Write((byte)player.Color); //color
                Write((byte)1);
                Write((byte)player.LightRadius);
                Write((byte)player.Transparency);

                Write(player.BuffArray, 0, 4);

                Write((byte)0); //spawn buff

                Write((byte)player.FrameType); //sprite type

                Write((byte)0x0);
                Write((byte)0x0);

                StringBuilder fmap = new StringBuilder();
                fmap.AppendFormat(string.Format("{0},{1}", map.diskName, map.Name));
                Write((short)fmap.Length);
                WriteAsciiNull(fmap.ToString());
            }
        }

        public sealed class UpdateCharStats : Packet
        {
            public UpdateCharStats(Player.Player player)
                : base(0x14)
            {
                Write((ushort)player.HPCur);
                Write((ushort)player.HP);
                Write((ushort)player.MPCur);
                Write((ushort)player.MP);

                Write((ushort)player.State.Stats.Str);
                Write((ushort)player.State.Stats.Men);
                Write((ushort)player.State.Stats.Dex);
                Write((ushort)player.State.Stats.Vit);
                Write((ushort)player.State.Stats.StrTotal);
                Write((ushort)player.State.Stats.MenTotal);
                Write((ushort)player.State.Stats.DexTotal);
                Write((ushort)player.State.Stats.VitTotal);

                Write((ushort)player.AC);
                Write((ushort)player.Hit);
                Write((ushort)player.Dam);
                Write((ushort)player.State.Stats.Extra);
            }
        }

        public sealed class SetLevelGold : Packet
        {
            public SetLevelGold(Player.Player player)
                : base(0x0F)
            {
                WriteAsciiFixed(player.ClassName, 32);
                Write((byte)player.State.Level);
                Write((int)player.State.XP);
                Write((int)player.State.XPNext);
                Write((int)player.State.Gold);
            }
        }
        #endregion
        #region Chat
        public sealed class UpdateChatBox : Packet
        {
            public UpdateChatBox(ushort colortext, ushort colorbar, string text)
                : base(0x0D)
            {
                Write((ushort)colortext); //01 ff orange ff ff white 
                Write((ushort)colorbar);
                Write((short)text.Count());
                WriteAsciiNull(text);
            }
        }
        public sealed class BubbleChat : Packet
        {
            public BubbleChat(int serial, string text)
                : base(0x20)
            {
                Write((int)serial);
                Write((byte)0);
                Write((short)text.Count());
                WriteAsciiNull(text);
            }
        }
        #endregion
        #region Board
        public sealed class UpdateBoardHead : Packet
        {
            public UpdateBoardHead(Board.Message msg)
                : base(0x29)
            {
                if (msg != null)
                {
                    Write((ushort)msg.Id); //01 ff orange ff ff white 
                    WriteAsciiNull(msg.Title);
                }
            }
        }
        public sealed class UpdateBoardDetail : Packet
        {
            public UpdateBoardDetail(Board.Message msg)
                : base(0x2A)
            {
                Write((short)msg.Content.Length); //01 ff orange ff ff white 
                WriteAsciiNull(msg.Content);
            }
        }
        public sealed class OpenBoard : Packet
        {
            public OpenBoard()
                : base(0x2B)
            {
            }
        }
        #endregion
    }

    public class Packet
    {
        private MemoryStream m_Stream;

        private int m_PacketID;
        private int m_Length;

        public Packet(int packetID, int size = 32)
        {
            m_PacketID = packetID;
            m_Length = 32;

            m_Stream = new MemoryStream(size);

            Write((byte)packetID);
        }

        byte[] m_Buffer = new byte[4];
        public void Write(byte value)
        {
            m_Stream.WriteByte(value);
        }

        public void Write(short value)
        {
            m_Buffer[1] = (byte)(value >> 8);
            m_Buffer[0] = (byte)value;

            m_Stream.Write(m_Buffer, 0, 2);
        }

        public void Write(ushort value)
        {
            m_Buffer[1] = (byte)(value >> 8);
            m_Buffer[0] = (byte)value;

            m_Stream.Write(m_Buffer, 0, 2);
        }

        public void Write(int value)
        {
            m_Buffer[3] = (byte)(value >> 24);
            m_Buffer[2] = (byte)(value >> 16);
            m_Buffer[1] = (byte)(value >> 8);
            m_Buffer[0] = (byte)value;

            m_Stream.Write(m_Buffer, 0, 4);
        }

        public void Write(byte[] buffer, int offset, int size)
        {
            m_Stream.Write(buffer, offset, size);
        }

        public void Fill(int length)
        {
            if (m_Stream.Position == m_Stream.Length)
            {
                m_Stream.SetLength(m_Stream.Length + length);
                m_Stream.Seek(0, SeekOrigin.End);
            }
            else
            {
                m_Stream.Write(new byte[length], 0, length);
            }
        }

        public void WriteAsciiNull(string value)
        {
            if (value == null)
            {
                Log.LogLine("Network: Attempted to WriteAsciiNull() with null value");
                value = String.Empty;
            }

            int length = value.Length;

            m_Stream.SetLength(m_Stream.Length + length + 1);

            Encoding.ASCII.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            m_Stream.Position += length + 1;
        }

        public void WriteAsciiFixed(string value, int size)
        {
            if (value == null)
            {
                Log.LogLine("Network: Attempted to WriteAsciiFixed() with null value");
                value = String.Empty;
            }

            int length = value.Length;

            m_Stream.SetLength(m_Stream.Length + size);

            if (length >= size)
                m_Stream.Position += Encoding.ASCII.GetBytes(value, 0, size, m_Stream.GetBuffer(), (int)m_Stream.Position);
            else
            {
                Encoding.ASCII.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += size;
            }
        }

        public byte[] Compile()
        {
            return Encrypt(m_Stream.ToArray());
        }

        public Byte[] Encrypt(Byte[] data)
        {
            Byte[] ret = new Byte[1024];
            int mLoopItr = 0;
            int loop3 = 0;
            byte var_f, var_e, var_d, var_c = 0, var_b = 0;

            mLoopItr = data.Count() / 3;
            if (data.Count() % 3 != 0)
                mLoopItr++;

            for (int x = 0; x < mLoopItr; x++)
            {
                var_d = data[loop3];
                if (loop3 + 1 < data.Count())
                    try { var_c = data[loop3 + 1]; }
                    catch { var_c = 0; }
                if (loop3 + 2 < data.Count())
                    try { var_b = data[loop3 + 2]; }
                    catch { var_b = 0; }

                ret[x * 4] = Convert.ToByte(var_d >> 2);
                var_e = Convert.ToByte((var_d & 3) << 4);
                var_f = Convert.ToByte(var_c >> 4);
                ret[x * 4 + 1] = Convert.ToByte(var_e | var_f);
                var_e = Convert.ToByte((var_c & 0x0F) << 2);
                var_f = Convert.ToByte(var_b >> 6);
                ret[x * 4 + 2] = Convert.ToByte(var_e | var_f);
                ret[x * 4 + 3] = Convert.ToByte(var_b & 0x3F);

                ret[x * 4] += 0x3B;
                ret[x * 4 + 1] += 0x3B;
                ret[x * 4 + 2] += 0x3B;
                ret[x * 4 + 3] += 0x3B;
                loop3 += 3;
            }
            int size = Array.IndexOf<Byte>(ret, 0);
            ret[size] = 0x2E;
            ret[size + 1] = 0x0A;
            //    ret[size + 2] = 0x00;

            Array.Resize(ref ret, size + 2);
            return ret;
        }
    }
}

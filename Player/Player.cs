using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using LKCamelotV2.Network;
using LKCamelotV2.Object;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class Player : Object.Living
    {
        public Network.GameConnection gameLink;

        public Player()
        {
        }

        public Player(string usr, string usrlower, string passwd)
        {
            Identity = new Identity();
            Identity.Username = usr;
            Identity.Password = passwd;
            Identity.UsernameLower = usrlower;
            State = new State();
            State.Stats = new Stats();
            Inventory = new Inventory();
            Bank = new Bank();
            Spells = new Spells();
            Position = new Object.Position();
        }

        public void Link()
        {
            State.playerLink = this;
            State.Stats.playerLink = this;
            Inventory.playerLink = this;
            Bank.playerLink = this;
            Spells.playerLink = this;
            Spells.Link();
        }

        [ProtoMember(1)]
        public Identity Identity;
        [ProtoMember(2)]
        public State State;
        [ProtoMember(3)]
        public Inventory Inventory;
        [ProtoMember(4)]
        public Bank Bank;
        [ProtoMember(5)]
        public Spells Spells;
        [ProtoMember(6)]
        E_Class _Class = E_Class.Beginner;
        internal E_Class Class
        {
            get
            {
                return _Class;
            }
            set
            {
                if (_Class != value && value != E_Class.Beginner)
                {
                    _Class = value;

                    var removes = Spells.LearnedSpells.Where(xe => xe.Value.Spell.ClassReq != 0 && (xe.Value.Spell.ClassReq & (int)Class) == 0).Select(xe => xe).ToList();
                    foreach (var sp in removes)
                        Spells.Forget(sp.Key);

                    var iremoves = Inventory.EquippedItems.Where(xe => xe.Value.ClassReq != 0 && (xe.Value.ClassReq & (int)Class) == 0).Select(xe => xe).ToList();
                    foreach (var itm in iremoves)
                        Inventory.Unequip(itm.Key);

                    Buffs.Clear();

                    var str = "[Aron]: Now you've become a " + Class.ToString() + ".";
                    WriteMessage(str);
                    UpdateStats();
                    Position.CurMap.Events.OnChgObjSprit(this);
                }
            }
        }

        Board.MessageBoard _CurrentBoard;
        public Board.MessageBoard CurrentBoard
        {
            get
            {
                if (_CurrentBoard == null)
                    _CurrentBoard = World.World.NoticeBoard;
                return _CurrentBoard;
            }
            set
            {
                _CurrentBoard = value;
            }
        }

        public override void Die()
        {
            WriteWarn("You have died.");
            State.autohit = false;
            Transparency = 0;
            Inventory.DropDied();
            var rest = World.World.GetMap("Rest");
            var rdt = rest.GetRandomTile();
            if (rdt != null)
                World.World.ChangeMap(this, rest, rdt.X, rdt.Y);
            else
                World.World.ChangeMap(this, rest, 17, 17);
        }

		internal void SwingAttack(long tick)
        {
            if (LastAttack + State.AttackSpeed > tick)
                return;
            LastAttack = tick;

            Position.CurMap.Events.OnSwing(this, this.Position.Face);

            var postile = Position.CurrentTile;
            if (postile == null)
                return;
            var tartile = Util.AdjecentTile(Position.CurrentTile, this.Position.Face, Position.CurMap);
            if (tartile == null)
                return;

            if (State.ProfessionSuit == 0)
            {
                var targets = Position.CurMap.TileLivingT(tartile.X, tartile.Y, State.PKMode);
                if (targets.Count > 0)
                {
                    targets[0].TakeDamage(this);
                }
            }
            else
            {
                var targets = Position.CurMap.TileCraftingT(tartile.X, tartile.Y, State.ProfessionSuit);
                if (targets.Count > 0)
                {
                    var tar = targets[0];
                    if (tar is Object.Mineral)
                        (tar as Object.Mineral).Hit(this);
                    else if (tar is Object.Craft)
                        (tar as Object.Craft).Hit(this);
                }
            }
        }

        internal override int HP
        {
            get
            {
                float temp = 0;
				switch (Class) {
				case E_Class.Knight:
					temp += (State.Stats.VitTotal + State.Level) * 3.06f;
					break;
	                case E_Class.Swordsman:
					temp += (State.Stats.VitTotal + State.Level) * 2.06f; break;
	                case E_Class.WizardSham:
					temp += (State.Stats.VitTotal + State.Level) * 1.56f; break;
					default:
					temp += (State.Stats.VitTotal + State.Level) * 2.06f; break;
				}

                foreach (var itm in Inventory.EquippedItems)
                    temp += itm.Value.HP;

                return (int)temp;
            }
        }
        internal override int MP
        {
            get
            {
                float temp = 0;
				switch (Class)
				{
	                case E_Class.Knight:
					temp += (State.Stats.MenTotal + State.Level) * 1.56f; break;
	                case E_Class.Swordsman:
					case E_Class.Shaman:
					temp += (State.Stats.MenTotal + State.Level) * 1.86f; break;
	                case E_Class.Wizard:
					temp += (State.Stats.MenTotal + State.Level) * 2.06f; break;
					default:
					temp += (State.Stats.MenTotal + State.Level) * 2.06f; break;
				}

                foreach (var itm in Inventory.EquippedItems)
                    temp += itm.Value.MP;

                return (int)temp;
            }
        }

		public override int AC
        {
            get
            {
                var tempd = 0;
                foreach (var itm in Inventory.EquippedItems)
                    tempd += itm.Value.AC;
                foreach (var buf in Buffs)
                    tempd += (buf.Value.Buff.Spell as Buff).ACBonus(tempd, buf.Value.Buff.Level);
                return tempd;
            }
        }
		public override int Hit
        {
            get
            {
                var temp = State.Stats.DexTotal + State.Stats.StrTotal / 10;
                if (Class == E_Class.Shaman || Class == E_Class.Swordsman)
                    temp += (int)(State.Stats.DexTotal * 0.3);

                foreach (var itm in Inventory.EquippedItems)
                    temp += itm.Value.Hit;

                if (temp > ushort.MaxValue)
                    temp = ushort.MaxValue;

                return temp;
            }
        }
		public override int Dam
        {
            get
            {
                float tempd = 0;
                if (Class == E_Class.Knight)
                    tempd += (State.Stats.StrTotal * 0.46f);
                else
                    tempd += (State.Stats.StrTotal * 0.4f);

                foreach (var itm in Inventory.EquippedItems)
                    tempd += itm.Value.Dam;
                foreach (var buf in Buffs)
                    tempd += (buf.Value.Buff.Spell as Buff).DamBonus(tempd, buf.Value.Buff.Level);
                return (int)tempd;
            }
        }

        public void OnFirstEnter()
        {
            gameLink.Send(new GameOutMessage.UpdateChatBox(0x6525, 5, "Welcome to the Last Kingdom").Compile());
            gameLink.Send(new GameOutMessage.UpdateChatBox(0xff00, 20737, "Version 3.0.1. Beta. March 27, 2014 : 21:33 UTC").Compile());
            gameLink.Send(new GameOutMessage.UpdateChatBox(0xff00, 20737, "Buildhash : " + Engine.MD5).Compile());
            gameLink.Send(new GameOutMessage.UpdateChatBox(0xff00, 20737, "Check the message board, Sysop.").Compile());


            UpdateStats();
            Spells.RedrawSpells();
            Inventory.RedrawInventory();
            Bank.RedrawBank();
            UpdateSecStats();
        }

        public void UpdateStats()
        {
            gameLink.Send((new GameOutMessage.UpdateCharStats(this).Compile()));
            gameLink.Send((new GameOutMessage.SetLevelGold(this).Compile()));
        }

        public void UpdateSecStats()
        {
            gameLink.Send((new GameOutMessage.SetProfessions(State.ProfessionString).Compile()));
        }

        public void OnChangedMap(Player playr)
        {
            World.Map curmap = playr.Position.CurMap;
            if (curmap.MapTiles != null)
                gameLink.Send((new GameOutMessage.LoadTiles(curmap).Compile()));
            gameLink.Send((new GameOutMessage.LoadWorld(this, curmap).Compile()));
            //gameLink.Send((new GameOutMessage.UpdateCharStats(this).Compile()));
            //gameLink.Send((new GameOutMessage.SetLevelGold(this).Compile()));
        }

        public void OnEnterMap(Object.Object obj)
        {
            if (obj is Player)
            {
                var playr = obj as Player;
                if (playr == this)
                {
                    return;
                }
            }
            if (obj is Object.Mobile)
            {
                if (obj is Object.Npc)
                {
                    gameLink.Send(new GameOutMessage.CreateNPC(obj as Object.Npc).Compile());
                }
                else if (obj is Object.Mineral)
                    gameLink.Send(new GameOutMessage.CreateMineral(obj as Object.Mineral).Compile());
                else if (obj is Object.Craft)
                    gameLink.Send(new GameOutMessage.CreateMineral(obj as Object.Craft).Compile());
                else
                {
                    var mobile = obj as Object.Mobile;
                    gameLink.Send(new GameOutMessage.CreateMobile(mobile).Compile());
                    gameLink.Send(new GameOutMessage.SetObjectEffects(mobile).Compile());
                }
            }
        }

        public bool OnPickUp(Object.Item objt)
        {
            if (objt is Gold)
            {
                State.Gold += (objt as Gold).Amount;
                return true;
            }
            else if (objt.StackSize > 0)
            {
                KeyValuePair<int, Object.Item> itm;
                //  if (objt is Ore)
                //      itm = Inventory.ContainsItem(objt._Name, (objt as Ore).Grade);
                //   else
                itm = Inventory.ContainsItem(objt._Name);
                if (itm.Value != null)
                {
                    itm.Value.StackSize += objt.StackSize;
                    gameLink.Send(new GameOutMessage.AddItemToInventory(itm.Value, itm.Key).Compile());
                    return true;
                }
            }

            if (Inventory.AddItem(objt))
                return true;
            return false;
        }

        public void OnLeaveMap(Object.Object objt)
        {
            if (objt == this)
                return;

            gameLink.Send(new GameOutMessage.DeleteObject(objt.objId).Compile());
        }

        public void OnTakeDamage(Object.Living obj)
        {
            byte shade = 0;

            var hp = ((((float)obj.HPCur / (float)obj.HP) * 100) * 1);
            shade = Convert.ToByte(hp);

            gameLink.Send(new GameOutMessage.HitAnimation(obj.objId, shade).Compile());
        }

        public void OnCast()
        {
        }

        public void OnFace(Object.Mobile obj)
        {
            if (obj == this)
                return;

            int face = 0;
            face = (obj as Object.Mobile).Position.Face;

            gameLink.Send(new GameOutMessage.ChangeFace(obj.objId, face).Compile());
        }

        public void OnWalk(Object.Mobile obj)
        {
            if (obj == this)
                return;

            gameLink.Send(new GameOutMessage.MoveSpriteWalk(obj.objId, obj.Position.Face, obj.Position.X, obj.Position.Y).Compile());
        }

        public void OnTele(Object.Mobile obj)
        {
            gameLink.Send(new GameOutMessage.MoveSpriteTele(obj.objId, obj.Position.Face, obj.Position.X, obj.Position.Y).Compile());
        }

        public void OnSwing(Object.Mobile obj, int face)
        {
            if (obj == this)
            {
                if (State.autohit)
                {
                }
                else
                    return;
            }

            gameLink.Send(new GameOutMessage.SwingAnimation(obj.objId, face).Compile());
        }

        public void OnChangeObjSprite(Object.Mobile obj)
        {
            gameLink.Send(new GameOutMessage.ChangeObjectSprite(obj).Compile());
            gameLink.Send(new GameOutMessage.SetObjectEffects(obj).Compile());
        }

        public void OnCurveMagic(Object.Mobile obj, int x, int y, Object.SpellSequence seq)
        {
            gameLink.Send(new GameOutMessage.CurveMagic(obj.objId, x, y, seq).Compile());
        }

        public void OnMagicEffect(Object.Mobile obj, int effect)
        {
            gameLink.Send(new GameOutMessage.SetMagicEffect(obj, effect).Compile());
        }

        public void OnTimeCycle(int time)
        {
            gameLink.Send((new GameOutMessage.SetBrightness(time).Compile()));
        }

        public void OnMobileSpeak(Object.Mobile npc, string msg)
        {
            gameLink.Send(new GameOutMessage.BubbleChat(npc.objId, PrefixedChatMessage(npc.Name, msg)).Compile());
        }

        public void OnCustomSpeak(Player playr, string msg, int type)
        {
            switch (type)
            {
                case 1: //shout
                    gameLink.Send(new GameOutMessage.UpdateChatBox(0xffff, 0x95, msg).Compile());
                    break;
                case 2: //bubble chat
                    if (playr.Position.CurMap == this.Position.CurMap)
                        gameLink.Send(new GameOutMessage.BubbleChat(playr.objId, msg).Compile());
                    break;
            }
        }

        public void OnChatMsg(Player playr, string msg)
        {
            if (msg.Length == 0)
                return;

            switch (msg[0])
            {
                case '/':
                    if (playr != this)
                        return;

                    StringBuilder sendto = new StringBuilder();
                    string msg2;
                    int itr = 1;
                    int chr;
                    while (itr < msg.Length && (chr = msg[itr++]) != ' ')
                        sendto.Append((char)chr);
                    msg2 = msg.Substring(itr);

                    var target = World.World.GetPlayer(sendto.ToString().ToLower());
                    if (target != null)
                    {
                        if (target == this)
                        {
                            playr.WriteWarn("You cant whisper yourself.");
                        }
                        else
                        {
                            target.gameLink.Send(new GameOutMessage.UpdateChatBox((ushort)Object.ColorCodes.clWhisper, 0, PrefixedWhisperMessage(target, playr.Name, msg2)).Compile());
                            playr.gameLink.Send(new GameOutMessage.UpdateChatBox((ushort)Object.ColorCodes.clWhisper, 0, PrefixedWhisperMessage(playr, target.Name, msg2)).Compile());
                        }
                    }
                    else
                        playr.WriteWarn("That player is not online.");

                    break;
                case '@':
                    if (playr != this)
                        return;

                    var command = ExtractPrefix(msg);
                    var suffixes = ExtractSuffixes(msg);
                    World.World._ProcCommand(playr, command, suffixes);
                    break;
                case '!':
                    if (State.Promo > 0 || State.Level >= 1)
                    {
                        World.World.OnChatMsg(this, PrefixedGlobalMessage(playr.Name, msg), 1);
                    }
                    else
                        WriteWarn("You must be level 30 or higher to global chat.");
                    break;
                case 'm':
                    if (Position.CurMap.Name == "St. Andover" && State.Level >= 5 && State.Promo == 0)
                    {
                        var aron = Position.CurMap.Npcs.Values.Where(xe => xe.Name == "ARON").FirstOrDefault();
                        if (aron != null)
                        {
                            if (Position.DistanceTo(aron.Position.X, aron.Position.Y) < 10)
                            {
                                if (msg.IndexOf("make me a ") == 0)
                                {
                                    var clas = ExtractSuffixes(msg);
                                    if (clas.Length == 3)
                                    {
                                        var nmp = clas[2];
                                        switch (nmp)
                                        {
                                            case "knight":
                                                Class = E_Class.Knight;
                                                break;
                                            case "swordman":
                                                Class = E_Class.Swordsman;
                                                break;
                                            case "shaman":
                                                Class = E_Class.Shaman;
                                                break;
                                            case "wizard":
                                                Class = E_Class.Wizard;
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    World.World.OnChatMsg(this, PrefixedChatMessage(playr.Name, msg), 2);
                    break;
                default:
                    World.World.OnChatMsg(this, PrefixedChatMessage(playr.Name, msg), 2);
                    break;
            }
        }

        string ExtractPrefix(string msg)
        {
            StringBuilder sendto = new StringBuilder();
            int itr = 0;
            int chr;
            while (itr < msg.Length && (chr = msg[itr++]) != ' ')
                sendto.Append((char)chr);
            return sendto.ToString();
        }

        string[] ExtractSuffixes(string msg)
        {
            var suffixes = msg.Split(' ');
            var retsuff = new string[suffixes.Length - 1];
            Array.Copy(suffixes, 1, retsuff, 0, retsuff.Length);

            return retsuff;
        }

        string PrefixedChatMessage(string name, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append(": ");
            sb.Append(msg);
            return sb.ToString();
        }

        string PrefixedGlobalMessage(string name, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(name);
            sb.Append("] : ");
            sb.Append(msg.Remove(0, 1));
            return sb.ToString();
        }

        string PrefixedWhisperMessage(Player playr, string sendr, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sendr);
            if (playr == this)
                sb.Append(" ::  ");
            else
                sb.Append(" =>  ");
            sb.Append(msg);
            return sb.ToString();
        }

        public void WriteMessage(string msg, Object.ColorCodes ctext = ColorCodes.clCream, Object.ColorCodes cbar = ColorCodes.clBlack)
        {
            gameLink.Send(new GameOutMessage.UpdateChatBox((ushort)ctext, (ushort)cbar, msg).Compile());
        }

        public void WriteWarn(string msg, Object.ColorCodes ctext = ColorCodes.clYellow, Object.ColorCodes cbar = ColorCodes.clBlack)
        {
            gameLink.Send(new GameOutMessage.UpdateChatBox((ushort)ctext, (ushort)cbar, msg).Compile());
        }

        public void WriteRank()
        {
        }

        public void SetBeginner()
        {
            Class = E_Class.Beginner;
            Position.Face = 4;
            State.Level = 1;
            State.Gold = 0;
            Position.X = 17;
            Position.Y = 17;
			Attribs [E_Attribute.LightRadius] = 3;
			Attribs [E_Attribute.Str] = 0;
			Attribs [E_Attribute.Men] = 0;
			Attribs [E_Attribute.Dex] = 0;
			Attribs [E_Attribute.Vit] = 0;
			Attribs [E_Attribute.Extra] = 0;
			Attribs [E_Attribute.HPCur] = HP;
			Attribs [E_Attribute.MPCur] = MP;
			Attribs [E_Attribute.PK] = 0;
			Attribs [E_Attribute.MinerLevel] = 1;
			Attribs [E_Attribute.SmithLevel] = 1;

            var knife = Scripts.Items.CreateItem("KNIFE");
            OnPickUp(knife);
        }

        public void SetBeginnerGM()
        {
            Class = E_Class.Beginner;
            Position.Face = 4;
            State.Level = 50;
            State.Gold = 50000000;
            Position.X = 15;
            Position.Y = 15;
			Attribs [E_Attribute.LightRadius] = 3;
			Attribs [E_Attribute.Str] = 5000;
			Attribs [E_Attribute.Men] = 5000;
			Attribs [E_Attribute.Dex] = 5000;
			Attribs [E_Attribute.Vit] = 5000;
			Attribs [E_Attribute.Extra] = 0;
			Attribs [E_Attribute.HPCur] = HP;
			Attribs [E_Attribute.MPCur] = MP;
			Attribs [E_Attribute.PK] = 0;
			Attribs [E_Attribute.MinerLevel] = 1;
			Attribs [E_Attribute.SmithLevel] = 1;

            /*
            for (int xt = 0; xt < 12; xt++)
            {
                var book6 = new Spellbook();
                Scripts.Items.GItems["BOOK OF TRANSPARENCY"].Clone(book6);
                OnPickUp(book6);
            }*/
        }

        public override byte FrameType
        {
            get
            {
                if (State.ProfessionSuit != 0)
                    return 2;
                return 0;
            }
        }

        public void Exit()
        {
            if (Position.CurMap != null)
            {
                Position.CurMap.Exit(this);
                Position.CurMap = null;
            }
            World.World.Exit(this);
        }

        public override string Name
        {
            get
            {
                return Identity.Username;
            }
        }

        public override byte[] Appearance
        {
            get
            {
                var temp = new byte[11];
                if (State.ProfessionSuit == 1)
                {
                    temp[1] = 5;
                    return temp;
                }
                else if (State.ProfessionSuit == 2)
                {
                    temp[1] = 6;
                    return temp;
                }
                temp[1] = BaseOutfit;
                Object.Equipment wep;
                Object.Equipment shield;
                Object.Equipment chest;
                if (Inventory.EquippedItems.TryGetValue(3, out chest))
                {
                    if ((Class & E_Class.Beginner) == 0)
                        temp[2] = (byte)chest.ArmorStage;
                    else
                    {
                        if (chest.ArmorStage >= 5)
                            temp[2] = (byte)chest.ArmorStage;
                    }
                }

                if ((Class & E_Class.Knight) != 0 || (Class & E_Class.Beginner) != 0)
                {
                    if (Inventory.EquippedItems.TryGetValue(4, out shield))
                    {
                        temp[3] = 1;
                    }
                }
                if (Inventory.EquippedItems.TryGetValue(2, out wep))
                {
                    if ((Class & E_Class.Knight) != 0 || (Class & E_Class.Beginner) != 0)
                    {
                        if (wep.EquipmentType == E_ET.Sword || wep.EquipmentType == E_ET.Sword2H)
                            temp[4] = 1;
                        if (wep.EquipmentType == E_ET.Hammer)
                            temp[5] = 1;
                        if (wep.EquipmentType == E_ET.Axe || wep.EquipmentType == E_ET.Axe2H)
                            temp[6] = 1;
                    }
                    if ((Class & E_Class.Swordsman) != 0)
                    {

                        if (wep.EquipmentType == E_ET.Sword || wep.EquipmentType == E_ET.Sword2H
                            || wep.EquipmentType == E_ET.Hammer
                            || wep.EquipmentType == E_ET.Axe || wep.EquipmentType == E_ET.Axe2H)
                            temp[4] = 1;
                    }
                    if ((Class & E_Class.Shaman) != 0 || (Class & E_Class.Wizard) != 0)
                    {

                        if (wep.EquipmentType == E_ET.Cane)
                            temp[7] = 1;
                        if (wep.EquipmentType == E_ET.Cane2)
                            temp[7] = 2;
                        if (wep.EquipmentType == E_ET.Spear)
                            temp[7] = 1;
                        if (wep.EquipmentType == E_ET.Spear2)
                            temp[7] = 2;
                    }
                }

                return temp;
            }
        }

        byte BaseOutfit
        {
            get
            {
                if (Class == E_Class.Beginner)
                    return 0;
                if (Class == E_Class.Knight)
                    return 1;
                if (Class == E_Class.Swordsman)
                    return 2;
                if (Class == E_Class.Wizard)
                    return 3;
                if (Class == E_Class.Shaman)
                    return 4;
                return 0;
            }
        }

        internal string ClassName
        {
            get
            {
                if (Class == E_Class.Knight)
                {
                    if (State.Promo == 0) return "Knight";
                    if (State.Promo == 1) return "Dragon";
                    if (State.Promo == 2) return "Nova";
                    if (State.Promo == 3) return "The Lord";
                    if (State.Promo == 4) return "Royal Knight";
                    if (State.Promo == 5) return "Saint Knight";
                    if (State.Promo == 6) return "Crux";
                    if (State.Promo == 7) return "Guardianship";
                    if (State.Promo == 8) return "Paladin";
                    if (State.Promo == 9) return "Black Swordsman";
                    if (State.Promo == 10) return "Guardian Deity";
                    if (State.Promo == 11) return "Skull Knight";
                    if (State.Promo == 12) return "Berserker";
                }
                if (Class == E_Class.Swordsman)
                {
                    if (State.Promo == 0) return "Swordsman";
                    if (State.Promo == 1) return "Knight Swordmaster";
                    if (State.Promo == 2) return "Elementalica";
                    if (State.Promo == 3) return "Dragon Cloud";
                    if (State.Promo == 4) return "Angela";
                    if (State.Promo == 5) return "Hae-DongSamurang";
                    if (State.Promo == 6) return "Back-DuGumsung";
                    if (State.Promo == 7) return "DaeungDaegum";
                    if (State.Promo == 8) return "Kensei";
                    if (State.Promo == 9) return "Royal Assassin";
                    if (State.Promo == 10) return "Fist of the South Star";
                    if (State.Promo == 11) return "Fist of the North Star";
                    if (State.Promo == 12) return "Transcended";
                }
                if (Class == E_Class.Shaman)
                {
                    if (State.Promo == 0) return "Shaman";
                    if (State.Promo == 1) return "Bodhisattva";
                    if (State.Promo == 2) return "Yogi";
                    if (State.Promo == 3) return "Diabolic Taoist";
                    if (State.Promo == 4) return "Zodiac Monk";
                    if (State.Promo == 5) return "Poong Back";
                    if (State.Promo == 6) return "Saint Purgatory";
                    if (State.Promo == 7) return "TaeGukSon";
                    if (State.Promo == 8) return "Saint";
                    if (State.Promo == 9) return "Wild Mage";
                    if (State.Promo == 10) return "Blooming Lotus";
                    if (State.Promo == 11) return "Seeded One";
                    if (State.Promo == 12) return "Eternal";
                }
                if (Class == E_Class.Wizard)
                {
                    if (State.Promo == 0) return "Wizard";
                    if (State.Promo == 1) return "Priest";
                    if (State.Promo == 2) return "Bishop";
                    if (State.Promo == 3) return "Martyr";
                    if (State.Promo == 4) return "Holy Wizard";
                    if (State.Promo == 5) return "Godly Messenger";
                    if (State.Promo == 6) return "Sacrosant";
                    if (State.Promo == 7) return "Glorianship";
                    if (State.Promo == 8) return "Grand Master";
                    if (State.Promo == 9) return "Archangel";
                    if (State.Promo == 10) return "Prophet";
                    if (State.Promo == 11) return "The Fallen";
                    if (State.Promo == 12) return "Godhand";
                }
                if (Class == E_Class.Beginner)
                {
                    if (State.Promo >= 1) return "Memnoch";
                }

                return "Beginner";
            }
        }
    }
}

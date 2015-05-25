using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;

namespace LKCamelotV2.Network
{
	public class GameConnection : Thixi.Connection
	{
		public GameConnection (Thixi.Server serv, Socket socket)
			: base (serv, socket)
		{
		}

		GamePacketBuffer bufr = new GamePacketBuffer ();

		LKCamelotV2.Player.Player Player;

		public override void OnConnect ()
		{
			Log.LogLine (string.Format ("+{0}  connid: {1}", endPointIP, conId), ConsoleColor.Green);
		}

		public override void OnDisconnect ()
		{
			if (System.Threading.Interlocked.CompareExchange (ref DisconnectedFlag, 2, 1) == 1) {
				if (Player != null) {
					DB.Loader.SavePlayer (Player);
					Player.Exit ();
				}
				Player = null;
				RefDispose();
			}
		}

		public override void OnData (byte[] msg, int offset, int bytestrans)
		{
			bufr.Append (msg, offset, bytestrans);
			GameMessage nextmsg;
			while ((nextmsg = bufr.GetNextPacket ()) != null) {
				switch (nextmsg.OPCode) {
				case (byte)GameProtocol.OPCodes.Login:
					OnLogin (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.KeepAlive:
					OnKeepAlive (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.MovementStep:
					OnMovementStep (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.MovementFace:
					OnMovementFace (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.Swing:
					OnSwing (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.PickUp:
					OnPickUp (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.InventoryDrop:
					OnInventoryDrop (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.AddStr:
					OnAddStr (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.AddMen:
					OnAddMen (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.AddDex:
					OnAddDex (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.AddVit:
					OnAddVit (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.SendChat:
					OnSendChat (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.InventoryEquip:
					OnInventoryEquip (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.InventorySwap:
					OnInventorySwap (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.InventoryUnequip:
					OnInventoryUnequip (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.InventoryDragDrop:
					OnInventoryDragDrop (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.SwapMagic:
					OnSpellSwap (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.DeleteMagic:
					OnSpellDelete (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.Cast19:
					OnCastNoTarget (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.Cast18:
					OnCastTarget (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.Cast3D:
					OnCastTarget2 (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.NPCInteract:
					OnNPCInteract (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.NPCBuy:
					OnNPCBuy (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.NPCSell:
					OnNPCSell (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.LeonFind:
					OnLeonFind (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.LeonEntrust:
					OnLeonEntrust (nextmsg);
					break;

				case (byte)GameProtocol.OPCodes.GetRank:
					Player.WriteRank ();
					break;
				case (byte)GameProtocol.OPCodes.GetBoard:
					Player.CurrentBoard = World.World.NoticeBoard;
					Player.CurrentBoard.DrawBoard (Player);
					break;
				case (byte)GameProtocol.OPCodes.GetSysBoard:
					Player.CurrentBoard = World.World.SysOpBoard;
					Player.CurrentBoard.DrawBoard (Player);
					break;
				case (byte)GameProtocol.OPCodes.GetBoardDetail:
				case (byte)GameProtocol.OPCodes.GetBoardDetailSys:
					OnGetBoardDetail (nextmsg);
					break;
				case (byte)GameProtocol.OPCodes.WriteBoardNotice:
				case (byte)GameProtocol.OPCodes.WriteBoardNoticeSys:
					OnWriteBoardNotice (nextmsg);
					break;

				default:
					break;
				}
			}
			bufr.Consume ();
		}

		public void OnCastNoTarget (GameMessage msga)
		{
			SpellCastNoTarget msg = msga as SpellCastNoTarget;

			Player.State.autohit = false;
			if (Player.Transparency > 0)
				Player.Transparency = 0;

			var spell = Player.Spells.GetSpell (msg.SpellSlot);
			if (spell != null && spell.Spell.magType == Object.E_MagicType.Casted) //ensure vs packet mani
                Player.Spells.Cast (spell);
		}

		public void OnCastTarget (GameMessage msga)
		{
			SpellCastTarget msg = msga as SpellCastTarget;
		}

		public void OnCastTarget2 (GameMessage msga)
		{
			SpellCastTarget2 msg = msga as SpellCastTarget2;

			Player.State.autohit = false;
			if (Player.Transparency > 0)
				Player.Transparency = 0;

			var spell = Player.Spells.GetSpell (msg.SpellSlot);
			if (spell == null || spell.Spell.magType != Object.E_MagicType.Target2)  //ensure vs packet mani
                return;

			var point = new Point2D () { X = msg.TarX, Y = msg.TarY };
			if (msg.TargetId == 0)
				Player.Spells.Cast (spell, point);
			else {
				var target = Player.Position.CurMap.GetObject (msg.TargetId);
				if (target != null) {
					if ((Player.State.PKMode && target is Player.Player)
					                   || !(target is Player.Player))
						Player.Spells.Cast (spell, target as Object.Mobile);
				}
			}
		}

		public void OnSpellSwap (GameMessage msga)
		{
			SpellSwap msg = msga as SpellSwap;
			Player.Spells.Swap (msg.FromSlot, msg.ToSlot);
		}

		public void OnSpellDelete (GameMessage msga)
		{
			SpellDelete msg = msga as SpellDelete;
			Player.Spells.Forget (msg.FromSlot);
		}

		public void OnPickUp (GameMessage msga)
		{
			PickUp msg = msga as PickUp;

			var obj = Player.Position.CurMap.Items.Skip (0).Where (x => x.Value.Position.X == Player.Position.X
			                   && x.Value.Position.Y == Player.Position.Y).FirstOrDefault ();
			if (obj.Value != null && Player.OnPickUp (obj.Value)) {
				if (!obj.Value.Static)
					Player.Position.CurMap.Exit (obj.Value);
				if (obj.Value is Object.Item)
					(obj.Value as Object.Item).DroppedTime = null;
			}
		}

		public void OnInventoryEquip (GameMessage msga)
		{
			InventoryUse msg = msga as InventoryUse;

			Object.Item outv;
			if (Player.Inventory.InventItems.TryGetValue (msg.InvSlot, out outv)) {
				if (outv is Object.Equipment)
					Player.Inventory.Equip (msg.InvSlot);
				else if (outv is Object.Spellbook) {
					if (Player.Spells.Learn (outv as Object.Spellbook)) {
						Player.Inventory.InventItems.TryRemove (msg.InvSlot, out outv);
						Player.gameLink.Send (new Network.GameOutMessage.DeleteItemSlot (msg.InvSlot).Compile ());
					}
				} else if (outv is Object.Consumable) {
					var cons = outv as Object.Consumable;
					if (cons._Consume == null)
						cons._Consume = (Scripts.Items.CreateItem (cons._Name) as Object.Consumable)._Consume;
					(outv as Object.Consumable)._Consume (Player);
					Player.Inventory.InventItems.TryRemove (msg.InvSlot, out outv);
					Player.gameLink.Send (new Network.GameOutMessage.DeleteItemSlot (msg.InvSlot).Compile ());
				}
			}
		}

		public void OnInventoryUnequip (GameMessage msga)
		{
			InventoryUnequip msg = msga as InventoryUnequip;
			Player.Inventory.Unequip (msg.EquipSlot);
		}

		public void OnInventorySwap (GameMessage msga)
		{
			InventorySwap msg = msga as InventorySwap;
			Player.Inventory.Swap (msg.FromSlot, msg.ToSlot);
		}

		public void OnInventoryDrop (GameMessage msga)
		{
			InventoryDrop msg = msga as InventoryDrop;

			Player.Inventory.Drop (msg.InvSlot);
		}

		public void OnInventoryDragDrop (GameMessage msga)
		{
			InventoryDragDrop msg = msga as InventoryDragDrop;

			var tar = Player.Position.CurMap.GetObject (msg.TargetId);
			var item = Player.Inventory.GetItem (msg.Slot);
			if (tar == null || item == null)
				return;

			if (tar is Object.Item) {
				var itar = tar as Object.Item;
				if (itar._Name == "CAULDRON" || itar._Name == "ANVIL") {
					if (item.Name.IndexOf ("MATCH :") != -1) {
						Object.Craft crafto = null;
						if (itar._Name == "CAULDRON")
							crafto = new Object.Cauldron ();
						else if (itar._Name == "ANVIL")
							crafto = new Object.Anvil ();

						crafto.Position.X = itar.Position.X;
						crafto.Position.Y = itar.Position.Y;
						crafto.Position.CurMap = itar.Position.CurMap;
						crafto.Upgrade (itar);
						Player.Inventory.Delete (msg.Slot, true);
					} else
						Player.WriteWarn (string.Format ("This {0} is not lit.", itar.Name));
				}
			} else if (tar is Object.Craft) {

				if ((tar as Object.Craft).DropItem (Player, item, msg.Slot)) {
					if ((tar is Object.Cauldron))
						Player.Inventory.Delete (msg.Slot, false);
					else if ((tar is Object.Anvil))
						Player.Inventory.Delete (msg.Slot, true);
				}
			}
		}

		public void OnSwing (GameMessage msga)
		{
			Swing msg = msga as Swing;

			if (Player.Transparency > 0)
				Player.Transparency = 0;

			Player.Position.Face = msg.Direction;
			Player.SwingAttack (World.World.tickcount.ElapsedMilliseconds);
		}

		public void OnAddStr (GameMessage msga)
		{
			if (Player.State.Stats.AddStat (Object.E_Attribute.Str))
				Send (new GameOutMessage.UpdateCharStats (Player).Compile ());
		}

		public void OnAddMen (GameMessage msga)
		{
			if (Player.State.Stats.AddStat (Object.E_Attribute.Men))
				Send (new GameOutMessage.UpdateCharStats (Player).Compile ());
		}

		public void OnAddDex (GameMessage msga)
		{
			if (Player.State.Stats.AddStat (Object.E_Attribute.Dex))
				Send (new GameOutMessage.UpdateCharStats (Player).Compile ());
		}

		public void OnAddVit (GameMessage msga)
		{
			if (Player.State.Stats.AddStat (Object.E_Attribute.Vit))
				Send (new GameOutMessage.UpdateCharStats (Player).Compile ());
		}

		public void OnMovementStep (GameMessage msga)
		{
			MovementStep msg = msga as MovementStep;
			if (Player.Position.X > Player.Position.CurMap.sizeX || Player.Position.Y > Player.Position.CurMap.sizeY)
				return;

			Player.State.autohit = false;
			var curtile = Player.Position.CurrentTile;
			if (curtile == null)
				return;
			curtile.Occupied = false;
			Player.Position.Face = msg.Face;
			Player.Position.X = msg.X;
			Player.Position.Y = msg.Y;
			Player.Position.CurMap.Events.OnWalk (Player);
			parseFace (ref Player.Position);

			var ctile = Player.Position.CurrentTile;
			if (ctile != null && ctile.Portal != null) {
				World.World.ChangeMap (Player, World.World.GetMap (ctile.Portal.destMap), ctile.Portal.destX, ctile.Portal.destY);
			} else if (ctile != null)
				ctile.Occupied = true;
		}

		public void parseFace (ref Object.Position player)
		{
			var face = player.Face;
			if (face == 0) {
				player.Y--;
			}
			if (face == 1) {
				player.X++;
				player.Y--;
			}
			if (face == 2) {
				player.X++;
			}
			if (face == 3) {
				player.X++;
				player.Y++;
			}
			if (face == 4) {
				player.Y++;
			}
			if (face == 5) {
				player.X--;
				player.Y++;
			}
			if (face == 6) {
				player.X--;
			}
			if (face == 7) {
				player.X--;
				player.Y--;
			}
		}

		public void OnMovementFace (GameMessage msga)
		{
			MovementFace msg = msga as MovementFace;
			Player.Position.Face = msg.Face;
			Player.Position.CurMap.Events.OnFace (Player);
		}

		internal Func<int, bool> stackmoveCallback = null;

		public void OnSendChat (GameMessage msga)
		{
			SendChat msg = msga as SendChat;
			if (stackmoveCallback != null) {
				int amt = 0;
				if (int.TryParse (msg.Message, out amt)) {
					stackmoveCallback (amt);
				}
				stackmoveCallback = null;
				return;
			}

			Player.OnChatMsg (Player, msg.Message);
		}

		public void OnGetBoardDetail (GameMessage msga)
		{
			GetBoardDetail msg = msga as GetBoardDetail;
			Player.CurrentBoard.DrawBoardDetail (Player, msg.Id);
		}

		public void OnWriteBoardNotice (GameMessage msga)
		{
			WriteBoardNotice msg = msga as WriteBoardNotice;
			if (string.IsNullOrEmpty (msg.Title) || string.IsNullOrEmpty (msg.Content))
				return;
			Player.CurrentBoard.WriteMessage (Player, msg.Title, msg.Content);
		}

		public void OnLeonFind (GameMessage msga)
		{
			LeonFind msg = msga as LeonFind;

			if (Player.Position.CurMap.Npcs.Values.Where (xe => xe.Name == "LOEN").FirstOrDefault () == null)
				return;

			Player.Bank.Withdraw (msg.Slot);
		}

		public void OnLeonEntrust (GameMessage msga)
		{
			LeonEntrust msg = msga as LeonEntrust;

			if (Player.Position.CurMap.Npcs.Values.Where (xe => xe.Name == "LOEN").FirstOrDefault () == null)
				return;

			Player.Bank.Deposit (msg.Slot);
		}

		public void OnNPCSell (GameMessage msga)
		{
			NPCSell msg = msga as NPCSell;

			if (Player.Position.CurMap.Npcs.Values.Where (xe => xe.Name == "LOEN" || xe.Name == "ARNOLD").FirstOrDefault () != null)
				Player.Inventory.Sell (msg.SellSlot);
		}

		public void OnNPCBuy (GameMessage msga)
		{
			NPCBuy msg = msga as NPCBuy;
			Object.Npc outn;
			if (!Player.Position.CurMap.Npcs.TryGetValue (msg.NPCID, out outn))
				return;

			outn.Buy (Player, msg.BuySlot);
		}

		public void OnNPCInteract (GameMessage msga)
		{
			NPCInteract msg = msga as NPCInteract;
			Object.Npc outn;
			if (!Player.Position.CurMap.Npcs.TryGetValue (msg.NPCID, out outn))
				return;
			if (outn.Gump == null)
				return;

			Send (new GameOutMessage.SpawnShopGump (outn.Gump).Compile ());
		}

		public void OnKeepAlive (GameMessage msg)
		{

		}

		public void OnLogin (GameMessage msg)
		{
			Login gmsg = msg as Login;

			if (!System.Text.RegularExpressions.Regex.IsMatch (gmsg.Username, @"^[a-zA-Z]+$")
                //|| !System.Text.RegularExpressions.Regex.IsMatch(gmsg.Password, @"^[a-zA-Z0-9]+$")
			             || (gmsg.Username.Count () < 3 || gmsg.Password.Count () < 3)) {
				Send (new GameOutMessage.WrongId ().Compile ());
				return;
			}

			var lowerU = gmsg.Username.ToLower ();
			if (World.World.GetPlayer (lowerU) != null) {
				Send (new GameOutMessage.AlreadyOnline ().Compile ());
				return;
			}

			var resp = DB.Loader.LoadPlayer (gmsg.Username, lowerU, gmsg.Password);
			switch (resp.Reslt) {
			case DB.LoadPlayerResponse.Result.WrongPass:
				Send (new GameOutMessage.WrongId ().Compile ());
				break;
			case DB.LoadPlayerResponse.Result.Success:
				Send (new GameOutMessage.CloseLogin ().Compile ());
				Player = resp.Player;
				Player.gameLink = this;
				Player.Link ();
				World.World.Enter (Player);
				Player.OnFirstEnter ();
				break;
			case DB.LoadPlayerResponse.Result.New:
				Send (new GameOutMessage.CloseLogin ().Compile ());
				resp.Player.Link ();
				Player = resp.Player;
				Player.gameLink = this;

				if (endPointIP.ToString ().IndexOf ("127.0.0.1") != -1)
					resp.Player.SetBeginnerGM ();
				else
					resp.Player.SetBeginner ();
				World.World.Enter (Player);
				Player.OnFirstEnter ();
				break;
			}
		}
	}
}
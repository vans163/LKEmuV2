using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class State
    {
        internal Player playerLink;

        [ProtoMember(1)]
        internal Stats Stats;

        internal bool autohit = false;

        internal int Promo
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.Promo];
            }
            set
            {
				playerLink.Attribs [Object.E_Attribute.Promo] = value;
            }
        }

        internal int Level
        {
			get
			{
				return playerLink.Attribs [Object.E_Attribute.Level];
			}
			set
			{
				playerLink.Attribs [Object.E_Attribute.Level] = value;
			}
        }

        internal int XP
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.XP];
            }
            set
            {
                if (value != XP)
                    playerLink.gameLink.Send(new Network.GameOutMessage.XPChange(value).Compile());
				playerLink.Attribs [Object.E_Attribute.XP] = value;

                if (value > XPNext)
                {
                    if (Level <= 98 && Promo == 0)
                    {
                        Level++;
                        Stats.Extra += (ushort)(5 + (Level / 10));
                    }
                    else if (Promo >= 1 && Level < 20)
                    {
                        Level++;
                        switch (Promo)
                        {
                            case 1: Stats.Extra += 30; break;
                            case 2: Stats.Extra += 50; break;
                            case 3: Stats.Extra += 80; break;
                            case 4: Stats.Extra += 120; break;
                            case 5: Stats.Extra += 180; break;
                            case 6: Stats.Extra += 260; break;
                            case 7: Stats.Extra += 360; break;
                        }
                    }
                    else
                    {
						playerLink.Attribs [Object.E_Attribute.XP] = XPNext;
                        return;
                    }

                    playerLink.gameLink.Send(new Network.GameOutMessage.SetLevelGold(playerLink).Compile());
                    playerLink.gameLink.Send(new Network.GameOutMessage.UpdateCharStats(playerLink).Compile());
                }
            }
        }

        internal int Gold
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.Gold];
            }
            set
            {
				playerLink.Attribs [Object.E_Attribute.Gold] = value;
                playerLink.gameLink.Send(new Network.GameOutMessage.GoldChange(value).Compile());
            }
        }

        internal int ProfessionSuit
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.ProfessionSuit];
            }
            set
            {
				playerLink.Attribs [Object.E_Attribute.ProfessionSuit] = value;
            }
        }

        internal int MinerLevel
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.MinerLevel];
            }
            set
            {
				playerLink.Attribs [Object.E_Attribute.MinerLevel] = value;
            }
        }

        internal int MinerXP
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.MinerXP];
            }
            set
            {
				if (value > XPCraftNext(MinerLevel))
                    MinerLevel++;
				playerLink.Attribs [Object.E_Attribute.MinerXP] = value;
                playerLink.UpdateSecStats();
            }
        }

        internal int SmithLevel
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.SmithLevel];
            }
            set
            {
				playerLink.Attribs [Object.E_Attribute.SmithLevel] = value;
            }
        }

        internal int SmithXP
        {
            get
            {
				return playerLink.Attribs [Object.E_Attribute.SmithXP];
            }
            set
            {
                if (value > XPCraftNext(SmithLevel))
                    SmithLevel++;
				playerLink.Attribs [Object.E_Attribute.SmithXP] = value;
                playerLink.UpdateSecStats();
            }
        }

        public string ProfessionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MinerXP > 0)
                    sb.Append(string.Format("Mining:L{0}:{1},", MinerLevel, MinerXP));
                if (SmithXP > 0)
                    sb.Append(string.Format("BSmith:L{0}:{1},", SmithLevel, SmithXP));
                return sb.ToString();
            }
        }

        public long CastSpeed = 1300;
        public long AttackSpeed = 450;


        public long RegenTick = 15000;
        public long lastRegenTick = 0;

		public void TickRegen(long tick)
        {
            if (RegenTick + lastRegenTick < tick)
            {
                lastRegenTick = tick;
                playerLink.HPCur += (int)(playerLink.HP * 0.10);
                playerLink.MPCur += (int)(playerLink.MP * 0.10);
            }
        }

        public bool PKMode
        {
            get
            {
				var pk = playerLink.Attribs[Object.E_Attribute.PK];
                return pk == 0 ? false : true;
            }
            set
            {
				playerLink.Attribs[Object.E_Attribute.PK] = value == false ? 0 : 1;
            }
        }

        internal int XPNext { get { return ExperienceTable[Promo][Level]; } }
        internal int XPCraftNext(int craftlvl)
        {
            return ExperienceTable[0][craftlvl];
        }

        public static Dictionary<int, Dictionary<int, int>> ExperienceTable = new Dictionary<int, Dictionary<int, int>>();
    }
}

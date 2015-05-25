using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LKCamelotV2.Object;
using LKCamelotV2.Player;

namespace LKCamelotV2.Scripts
{
    public static class Spells
    {
        public static Dictionary<string, Spell> SpellList = new Dictionary<string, Spell>();
        public static void Load()
        {
            SpellList.Add("FLAME ROUND", new Spell("FLAME ROUND", 18, E_MagicType.Casted, new SpellSequence
                 (40, 25, 15, 0, 1, 6, 0))
                 {
                     Dam = 64,
                     DamPl = 6,
                     ManaCost = 54,
                     Range = 7,
                     Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                         (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) => { CastAoe(s, p); })
                 });
            SpellList.Add("THUNDER CROSS", new Spell("THUNDER CROSS", 24, E_MagicType.Casted, new SpellSequence
                 (46, 32, 0, 0, 1, 5, 0))
            {
                Dam = 132,
                DamPl = 8,
                ManaCost = 105,
                Range = 7,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) => { CastAoe(s, p); })
            });
            SpellList.Add("ELECTRONIC BALL", new Spell("ELECTRONIC BALL", 58, E_MagicType.Target2, new SpellSequence
                 (45, 32, 0, 0, 0, 9, 0))
            {
                Dam = 30,
                DamPl = 5,
                ManaCost = 25,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("FIRE BALL", new Spell("FIRE BALL", 12, E_MagicType.Target2, new SpellSequence
                 (40, 23, 0, 0, 0, 9, 0))
            {
                Dam = 40,
                DamPl = 6,
                ManaCost = 32,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("ZIG ZAG", new Spell("ZIG ZAG", 21, E_MagicType.Target2, new SpellSequence
                 (0, 32, 0, 0, 0, 7, 0))
            {
                Dam = 18,
                DamPl = 4,
                ManaCost = 16,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("MOON LIGHT", new Spell("MOON LIGHT", 37, E_MagicType.Target2, new SpellSequence
                 (0, 81, 0, 0, 0, 8, 0))
            {
                Dam = 42,
                DamPl = 6,
                ManaCost = 28,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar, race: E_Race.Undead | E_Race.Animal);
                    })
            });
            SpellList.Add("SHOOTING STAR", new Spell("SHOOTING STAR", 37, E_MagicType.Target2, new SpellSequence
                 (0, 81, 0, 0, 0, 8, 0))
            {
                Dam = 88,
                DamPl = 8,
                ManaCost = 32,
                ClassReq = (int)E_Class.Knight | (int)E_Class.Wizard,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar, race: E_Race.Undead | E_Race.Animal);
                    })
            });
            SpellList.Add("MORNING STAR", new Spell("MORNING STAR", 37, E_MagicType.Target2, new SpellSequence
                 (0, 0, 73, 0, 3, 30, 0))
            {
                Dam = 88,
                DamPl = 8,
                ManaCost = 44,
                ClassReq = (int)E_Class.Swordsman | (int)E_Class.Shaman,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                        {
                            p.Position.CurMap.Events.OnMagcEffect(tar, s.Spell.SpellSeq.OnImpactSprite);
                            CastSingleTarget(s, p, tar, race: E_Race.Undead | E_Race.Animal);
                        }
                    })
            });
            SpellList.Add("SORCERER HUNTER", new Spell("SORCERER HUNTER", 37, E_MagicType.Target2, new SpellSequence
                 (0, 0, 73, 0, 3, 30, 0))
            {
                Dam = 152,
                DamPl = 10,
                ManaCost = 125,
                ClassReq = (int)E_Class.Wizard,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                        {
                            p.Position.CurMap.Events.OnMagcEffect(tar, s.Spell.SpellSeq.OnImpactSprite);
                            CastSingleTarget(s, p, tar, race: E_Race.Magical | E_Race.Demon);
                        }
                    })
            });
            SpellList.Add("FLAME WAVE", new Spell("FLAME WAVE", 16, E_MagicType.Target2, new SpellSequence
                 (0, 52, 0, 3, 0, 8, 0))
            {
                Dam = 52,
                DamPl = 6,
                ManaCost = 38,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("LIGHTNING", new Spell("LIGHTNING", 22, E_MagicType.Target2, new SpellSequence
                 (0, 30, 0, 0, 0, 12, 4))
            {
                Dam = 60,
                DamPl = 6,
                ManaCost = 40,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("THUNDER BOLT", new Spell("THUNDER BOLT", 58, E_MagicType.Target2, new SpellSequence
                 (0, 79, 30, 0, 0, 9, 0))
            {
                Dam = 112,
                DamPl = 6,
                ManaCost = 60,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar, E_Race.Undead | E_Race.Animal | E_Race.Demon);
                    })
            });
            SpellList.Add("HONEST BOLT", new Spell("HONEST BOLT", 36, E_MagicType.Target2, new SpellSequence
                 (0, 38, 80, 0, 0, 9, 0))
            {
                Dam = 556,
                DamPl = 11,
                FManaCost = 0.64f,
                FManaCostPl = 0.04f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar, E_Race.Undead | E_Race.Animal | E_Race.Demon);
                    })
            });
            SpellList.Add("GHOST HUNTER", new Spell("GHOST HUNTER", 36, E_MagicType.Target2, new SpellSequence
                 (0, 38, 80, 0, 0, 9, 0))
            {
                Dam = 146,
                DamPl = 10,
                ManaCost = 79,
                ClassReq = (int)E_Class.Shaman,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar, E_Race.Magical | E_Race.Demon);
                    })
            });
            SpellList.Add("FIRE SHOT", new Spell("FIRE SHOT", 13, E_MagicType.Target2, new SpellSequence
                 (41, 26, 20, 0, 0, 9, 0))
            {
                Dam = 352,
                DamPl = 8,
                ManaCost = 180,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });
            SpellList.Add("SIDEWINDER", new Spell("SIDEWINDER", 41, E_MagicType.Target2, new SpellSequence
                 (8, 37, 0, 0, 0, 9, 0))
            {
                Dam = 290,
                DamPl = 10,
                FManaCost = 0.72f,
                FManaCostPl = 0.04f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                            CastSingleTarget(s, p, tar);
                    })
            });

            SpellList.Add("TELEPORT", new Spell("TELEPORT", 53, E_MagicType.Target2, new SpellSequence
                 (0, 0, 0, 0, 0xFF, 0, 0))
            {
                ManaCost = 125,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastTeleport(s, p, tarloc.X, tarloc.Y);
                    })
            });
            SpellList.Add("VIEW", new Spell("VIEW", 9, E_MagicType.Target2, new SpellSequence
                 (0, 0, 0, 0, 0xFF, 0, 0))
            {
                ManaCost = 5,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastView(s, p, tar);
                    })
            });
            SpellList.Add("TRANSPARENCY", new Spell("TRANSPARENCY", 10, E_MagicType.Casted, new SpellSequence
                 (0, 0, 0, 0, 0xFF, 0, 0))
            {
                FManaCost = 0.55f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastTransparency(s, p);
                    })
            });

            SpellList.Add("MAGIC SHIELD", new Buff("MAGIC SHIELD", 27, E_MagicType.Casted, new SpellSequence
                 (5, 0, 0, 0, 0, 3, 0))
            {
                ManaCost = 76,
                AC = 4,
                ACpl = 4,
                BuffEffect = Object.BuffEffect.ManaAsHp,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("TEAGUE SHIELD", new Buff("TEAGUE SHIELD", 28, E_MagicType.Casted, new SpellSequence
                 (6, 0, 0, 0, 0, 3, 0))
            {
                ManaCost = 66,
                Dam = 30,
                DamPl = 5,
                AC = 4,
                ACpl = 4,
                BuffEffect = Object.BuffEffect.ManaAsHp,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("FIRE PROTECTOR", new Buff("FIRE PROTECTOR", 19, E_MagicType.Casted, new SpellSequence
                 (70, 0, 0, 0, 0, 3, 0))
            {
                FManaCost = 0.50f,
                FManaCostPl = 0.02f,
                Dam = 60,
                DamPl = 6,
                FAC = 0.25f,
                FACpl = 0.01f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("GUARDIAN SWORD", new Buff("GUARDIAN SWORD", 31, E_MagicType.Casted, new SpellSequence
                 (3, 0, 0, 0, 0, 3, 0))
            {
                FManaCost = 0.25f,
                FAC = 0.20f,
                FACpl = 0.01f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("ELECTRONIC TUBE", new Buff("ELECTRONIC TUBE", 26, E_MagicType.Casted, new SpellSequence
                 (12, 0, 0, 0, 0, 3, 0))
            {
                FManaCost = 0.70f,
                FManaCostPl = 0.04f,
                Dam = 147,
                DamPl = 10,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("MAGIC ARMOR", new Buff("MAGIC ARMOR", 35, E_MagicType.Casted, new SpellSequence
                 (57, 0, 0, 0, 0, 3, 0))
            {
                FManaCost = 0.20f,
                FAC = 0.16f,
                FACpl = 0.01f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("MENTAL SWORD", new Buff("MENTAL SWORD", 50, E_MagicType.Casted, new SpellSequence
                 (63, 0, 0, 0, 0, 0, 0))
            {
                FManaCost = 0.33f,
                FDam = 0.7f,
                FDampl = 0.1f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });
            SpellList.Add("RAINBOW ARMOR", new Buff("RAINBOW ARMOR", 30, E_MagicType.Casted, new SpellSequence
                 (73, 0, 0, 0, 0, 1, 0))
            {
                FManaCost = 0.53f,
                FManaCostPl = 0.03f,
                FAC = 0.35f,
                FACpl = 0.01f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastBuff(s, p);
                    })
            });

            SpellList.Add("HEAL", new Buff("HEAL", 1, E_MagicType.Casted, new SpellSequence
                 (1, 0, 0, 0, 3, 3, 0))
            {
                ManaCost = 36,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastHeal(s, p);
                    })
            });
            SpellList.Add("PLUS HEAL", new Buff("PLUS HEAL", 2, E_MagicType.Target2, new SpellSequence
                 (0, 1, 0, 0, 0, 6, 3))
            {
                ManaCost = 118,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        CastHeal(s, tar as Living);
                    })
            });

            #region WIZARD
            SpellList.Add("DEADLY BOOM", new Spell("DEADLY BOOM", 69, E_MagicType.Target2, new SpellSequence
                 (0, 0, 63, 0, 3, 30, 0))
            {
                Dam = 541,
                DamPl = 14,
                FManaCost = 0.70f,
                FManaCostPl = 0.05f,
                Cast = new Action<Player.LeafSpell, Player.Player, Mobile, Point2D>(
                    (Player.LeafSpell s, Player.Player p, Mobile tar, Point2D tarloc) =>
                    {
                        if (tar != null)
                        {
                            p.Position.CurMap.Events.OnMagcEffect(tar, s.Spell.SpellSeq.OnImpactSprite);
                            CastSingleTarget(s, p, tar);
                        }
                    })
            });
            #endregion

        }

        public static void CastSingleTarget(Player.LeafSpell spell, Player.Player caster, Object.Mobile target, E_Race race = 0)
        {
            if (!(target is Object.Living))
                return;

            if (race != 0)
            {
                if (((target as Object.Living).Race & race) == 0)
                    return;
            }
            (target as Object.Living).TakeDamage(caster, spell);
        }

        public static void CastSingleTarget(Player.LeafSpell spell, Player.Player caster, int x, int y)
        {
            var Targets = caster.Position.CurMap.TargetsInAoE(new Point2D(caster.Position.X, caster.Position.Y),
                spell.Spell.Range, caster.State.PKMode);
            foreach (var tar in Targets)
            {
                tar.TakeDamage(caster, spell);
            }
        }

        public static void CastTeleport(Player.LeafSpell spell, Player.Player caster, int x, int y)
        {
            var tpdist = (spell.Level / 2) * 2;
            // if (tpdist <= 3) tpdist = 4;
            if (tpdist > 12) tpdist = 12;

            if (Point2D.Distance(caster.Position.X, caster.Position.Y, x, y) > tpdist)
                return;

            var tile = caster.Position.CurMap.GetTile(x, y);
            if (tile == null || tile.WalkFlags == 0)
                return;

            caster.Position.X = x;
            caster.Position.Y = y;

            caster.Position.CurMap.Events.OnTele(caster);
        }

        public static void CastView(Player.LeafSpell spell, Player.Player caster, Object.Mobile target)
        {
            if (target == null)
                return;

            if (target is Craft)
            {
                var cstd = target as Craft;
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("The contents: ", cstd.Name));
                foreach (var ob in cstd.Contents)
                    sb.Append(string.Format("{0} ", ob.Value.Name));
                caster.WriteWarn(sb.ToString());
            }
        }

        public static void CastTransparency(Player.LeafSpell spell, Player.Player caster)
        {
            int amt = 1;
            if (spell.Level >= 12)
                amt = 100;
            caster.Transparency = amt;
        }

        public static void CastBuff(Player.LeafSpell spell, Player.Player caster)
        {
            caster.SetBuff(spell);
            caster.UpdateStats();
            caster.Position.CurMap.Events.OnChgObjSprit(caster);
        }

        public static void CastHeal(Player.LeafSpell spell, Player.Player caster)
        {
            double amt = 0.6f + (spell.Level * 0.02);
            caster.HPCur += (int)(caster.HP * amt);
        }

        public static void CastHeal(Player.LeafSpell spell, Object.Living target)
        {
            double amt = 0.6f + (spell.Level * 0.02);
            target.HPCur += (int)(target.HP * amt);
        }

        public static void CastAoe(Player.LeafSpell spell, Player.Player caster)
        {
            var Targets = caster.Position.CurMap.TargetsInAoE(new Point2D(caster.Position.X, caster.Position.Y),
                spell.Spell.Range, caster.State.PKMode);
            foreach (var tar in Targets)
            {
                tar.TakeDamage(caster, spell);
            }
        }
    }
}

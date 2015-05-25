using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Anvil : Craft
    {
        public Anvil()
        {
            this.Name = "ANVIL";
        }

        public Recipe CurrentRecipe = null;

        public override void Hit(Player.Player plyr)
        {
            //Ensure only 1 player hits it at a time to prevent a dupe
            lock (hitLock)
            {
                if (Contents.Count == 0)
                    return;

                if (CurrentRecipe == null)
                    CurrentRecipe = Scripts.Recipe.GetSmithRecipe(Contents.Skip(0).Select(xe => xe.Value).ToList());

                if (CurrentRecipe == null)
                {
                    plyr.WriteWarn(string.Format("Nothing could be created with this recipe."));
                    return;
                }
                /*
                if (plyr.State.SmithLevel < CurrentRecipe.LevelReq)
                {
                    plyr.WriteWarn(string.Format("You need to be level {0} to create {1}.", CurrentRecipe.LevelReq, CurrentRecipe.Result));
                    return;
                }
                */
                if (THits == 0)
                {
                    if (CurrentRecipe.Warn == 0)
                    {
                        CurrentRecipe.Warn++;
                        if (CurrentRecipe.refine != null)
                            plyr.WriteWarn(string.Format("Starting to refine {0}.", CurrentRecipe.refine.Name));
                        else
                            plyr.WriteWarn(string.Format("Starting to make {0}.", CurrentRecipe.Result));
                    }
                }
                THits++;

                if ((float)THits / (float)CurrentRecipe.HitsReq >= 0.9f)
                {
                    if (CurrentRecipe.Warn == 1)
                    {
                        CurrentRecipe.Warn++;
                        if (CurrentRecipe.refine != null)
                            plyr.WriteWarn(string.Format("Almost refined {0}.", CurrentRecipe.refine.Name));
                        else
                            plyr.WriteWarn(string.Format("Almost made {0}", CurrentRecipe.Result));
                    }
                }
                if (THits >= CurrentRecipe.HitsReq)
                {
                    var roll = Dice.RandomMinMax(1, 100);
                    if (roll <= CurrentRecipe.Chance)
                    {
                        Item result = null;
                        if (CurrentRecipe.refine != null)
                        {
                            CurrentRecipe.refine.EquipmentGrade++;
                            result = CurrentRecipe.refine;
                        }
                        else
                            result = Scripts.Items.CreateItem(CurrentRecipe.Result);

                        result.Position.X = plyr.Position.X;
                        result.Position.Y = plyr.Position.Y;
                        result.Position.CurMap = plyr.Position.CurMap;
                        plyr.Position.CurMap.Enter(result);

                        plyr.State.SmithXP += CurrentRecipe.XPGain;
                        plyr.WriteWarn(string.Format("You have gained {0} experience.", CurrentRecipe.XPGain));
                    }
                    else
                        if (CurrentRecipe.refine != null)
                            plyr.WriteWarn(string.Format("You failed to refine {0}.", CurrentRecipe.refine.Name));
                        else
                            plyr.WriteWarn(string.Format("You failed to create {0}.", CurrentRecipe.Result));

                    foreach (var cnt in Contents.Skip(0).Select(xe => xe).ToArray())
                        RemoveItem(cnt.Value);

                    THits = 0;
                    CurrentRecipe = null;
                }
            }
        }

        public override bool DropItem(Player.Player playr, Item itm, int slot)
        {
            if (itm._Name == "WOOD")
            {
                fireTime += 15000;
                return true;
            }

            if (itm.StackSize > 0)
            {
                Item contain;
                if ((contain = Contains(itm._Name)) != null)
                    contain.StackSize++;
                else
                {
                    Item clone = Scripts.Items.CreateItem(itm._Name);
                    Contents.TryAdd(clone.objId, clone);
                }
                return true;
            }

            if (itm.StackSize == 0)
            {
                Contents.TryAdd(itm.objId, itm);
                return true;
            }

            playr.WriteWarn(string.Format("You cannot place that in the {0}.", Name));
            return false;
        }

        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 0x0B;
                ret[9] = (byte)10;
                return ret;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Craft : Mobile
    {
        public int THits;

        public ConcurrentDictionary<int, Item> Contents = new ConcurrentDictionary<int, Item>();
        public long fireTime;
        public Item baseConsumed;

        public int Speed = 2;
        public int Frames = 20;
        public int StartFrame = 1;

        protected readonly object hitLock = new object();

        public Item Contains(string name)
        {
            return Contents.Skip(0).ToArray().Where(xe => xe.Value._Name == name).FirstOrDefault().Value;
        }

        public void Upgrade(Item basei)
        {
            if (baseConsumed == null)
            {
                baseConsumed = basei;
                Position.CurMap.Exit(baseConsumed);
                fireTime = 15000;
                Position.CurMap.Enter(this);
            }
        }

        public void Downgrade()
        {
            if (baseConsumed != null)
            {
                Position.CurMap.Exit(this);
                Position.CurMap.Enter(baseConsumed);
                foreach (var itm in Contents)
                {
                    itm.Value.Position.X = Position.X;
                    itm.Value.Position.Y = Position.Y;
                    Position.CurMap.Enter(itm.Value);
                }
                Contents.Clear();
                baseConsumed = null;
            }
        }

        public bool RemoveItem(Item itm)
        {
            Item outi;
            if (Contents.TryRemove(itm.objId, out outi))
                return true;
            return false;
        }

        public virtual bool DropItem(Player.Player playr, Item itm, int slot)
        {
            if (itm._Name == "WOOD")
            {
                fireTime += 15000;
                return true;
            }
            else if (this is Cauldron)
            {
                if (itm.StackSize > 0)
                {
                    Contents.TryAdd(itm.objId, itm);
                    return true;
                }
            }

            playr.WriteWarn(string.Format("You cannot place that in the {0}.", Name));
            return false;
        }

        public virtual void Hit(Player.Player plyr)
        {
        }

        public void Tick()
        {
            fireTime -= 100;
            if (fireTime <= 0)
                fireTime = 0;

            if (fireTime > 15000)
            {
                //large flame
            }
            else if (fireTime > 0)
            {
                //small flame
            }
            else
            {
                if (baseConsumed != null)
                    Downgrade();
                // no flame
            }
        }
    }
}

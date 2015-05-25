using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Cauldron : Craft
    {
        public Cauldron()
        {
            this.Name = "CAULDRON";
        }

        public override void Hit(Player.Player plyr)
        {
            //Ensure only 1 player hits it at a time to prevent a dupe
            lock (hitLock)
            {
                THits++;
                if (THits >= 10)
                {
                    THits = 0;
                    Scripts.Recipe.Smelt(plyr, this);
                }
            }
        }

        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 0x0B;
                ret[9] = (byte)9;
                return ret;
            }
        }
    }
}

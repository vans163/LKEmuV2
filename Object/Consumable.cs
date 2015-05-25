using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Consumable : Item
    {
        public delegate void Consume(Player.Player playr);
        public Consume _Consume;

        public Consumable()
        {
        }

        public Consumable(string name, int sprid) : base(name, sprid)
        {
        }
    }
}

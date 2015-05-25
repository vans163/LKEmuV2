using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Object
{
    public class Gold : Item
    {
        public override string Name { get { return Amount + " Gold"; } }

        public byte[] _Appearance;
        public override byte[] Appearance
        {
            get
            {
                if (_Appearance == null)
                {
                    int sprid = 40;
                    if (Amount > 1000000)
                        sprid = 43;
                    if (Amount > 100000)
                        sprid = 42;
                    if (Amount > 10000)
                        sprid = 41;

                    _Appearance = new byte[11];
                    _Appearance[0] = 2;
                    _Appearance[9] = (byte)sprid;
                }
                return _Appearance;
            }
        }

        internal int Amount = 1;
    }
}

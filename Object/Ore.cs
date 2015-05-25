using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [ProtoContract]
    public class Ore : Item
    {
        public Ore()
        {
        }

        public Ore(string name, int sprid) : base(name, sprid)
        {
        }
        
        [ProtoMember(1)]
        int? _Grade = 1;
        public int Grade
        {
            get
            {
                if (!_Grade.HasValue)
                {
                    int grade = 1;
                    if (_Name.IndexOf(" PN") != -1)
                        grade = 2;
                    else if (_Name.IndexOf(" PG") != -1)
                        grade = 3;
                    _Grade = grade;
                }
                return _Grade.Value;
            }
        }
        
        public override byte[] Appearance
        {
            get
            {
                byte[] ret = new byte[11];
                ret[0] = 2;
                ret[9] = (byte)(SprId + (Grade - 1));
                return ret;
            }
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} : {1}", _Name, StackSize);
            }
            set
            {
                _Name = value;
            }
        }
    }
}

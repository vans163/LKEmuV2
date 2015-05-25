using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
    [ProtoContract]
    [ProtoInclude(10, typeof(Mobile))]
    public class Object
    {
        public int objId;
        static int id_Gen;

        public Object()
        {
            objId = System.Threading.Interlocked.Increment(ref id_Gen);
        }
    }
}

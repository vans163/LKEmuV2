using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace LKCamelotV2.Player
{
    [ProtoContract]
    public class Identity
    {
        [ProtoMember(1)]
        public string Username;
        [ProtoMember(2)]
        public string Password;
        [ProtoMember(3)]
        public string UsernameLower;
    }
}

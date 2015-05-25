using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Board
{
    [ProtoContract]
    public class Message
    {
        [ProtoMember(1)]
        public int Id;
        static int genId = 0;
        [ProtoMember(2)]
        public string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value.Length > 25)
                    value = value.Remove(25);
                _Title = value;
            }
        }
        [ProtoMember(3)]
        public string Content;

        public Message()
        {
            Id = System.Threading.Interlocked.Increment(ref genId);
        }


    }
}

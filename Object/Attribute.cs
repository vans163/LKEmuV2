using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Object
{
	[ProtoContract]
	public class Attribute
	{
		[ProtoMember (1)]
		public int SubKey;
		[ProtoMember (2)]
		public int Value;
		internal object write_Lock = new object ();
	}

	[ProtoContract]
	public class Attributes
	{
		[ProtoMember (1)]
		internal ConcurrentDictionary<E_Attribute, Attribute> AttribMap = new ConcurrentDictionary<E_Attribute, Attribute> ();

		public int this [E_Attribute i] {
			get { 
				Attribute outv = AttribMap.GetOrAdd(i, new Attribute () { Value = 0 });
				return outv.Value;
			}
			set {
				Attribute outv = AttribMap.GetOrAdd(i, new Attribute () { Value = value});
				System.Threading.Interlocked.Exchange (ref outv.Value, value);
			}
		}
	}
}
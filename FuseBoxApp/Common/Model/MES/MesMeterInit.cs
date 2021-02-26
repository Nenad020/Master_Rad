using Common.Model.MES.Interface;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Model.MES
{
	[DataContract]
	public class MesMeterInit : IMesInit<long>
	{
		[DataMember]
		public Dictionary<int, long> Values { get; set; }

		public MesMeterInit()
		{
			Values = new Dictionary<int, long>(ushort.MaxValue);
		}

		public void Add(int id, long value)
		{
			Values.Add(id, value);
		}

		public void Clear()
		{
			Values.Clear();
		}
	}
}

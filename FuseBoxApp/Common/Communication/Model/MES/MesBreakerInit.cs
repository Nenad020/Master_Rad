using Common.Communication.Model.MES.Interface;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Communication.Model.MES
{
	[DataContract]
	public class MesBreakerInit : IMesInit<bool>
	{
		[DataMember]
		public Dictionary<int, bool> Values { get; set; }

		public MesBreakerInit()
		{
			Values = new Dictionary<int, bool>(ushort.MaxValue);
		}

		public void Add(int id, bool value)
		{
			Values.Add(id, value);
		}

		public void Clear()
		{
			Values.Clear();
		}
	}
}

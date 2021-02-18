namespace Common.Communication.Model.SCADA
{
	using Common.Communication.Model.SCADA.Interface;
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.Text;

	[DataContract]
	public class ScadaHoldingRegistersAddressChanges : IScadaChanges<long>
	{
		[DataMember]
		public Dictionary<int, long> Values { get; set; }

		public ScadaHoldingRegistersAddressChanges()
		{
			this.Values = new Dictionary<int, long>(ushort.MaxValue);
		}

		public void Update(IReadOnlyList<int> ids, IReadOnlyList<long> values)
		{
			if (ids.Count != values.Count)
			{
				throw new ArgumentException("Count of addresses must be equal to values count");
			}

			if (ids.Count == 0)
			{
				this.Clear();
				return;
			}

			this.Clear();
			for (var i = 0; i < ids.Count; i++)
			{
				this.Values.Add(ids[i], values[i]);
			}
		}

		public void Clear()
		{
			this.Values.Clear();
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder(500);
			foreach (var keyValuePair in this.Values)
			{
				var line = $"\tGid: {keyValuePair.Key}, Value: {keyValuePair.Value}";
				builder.AppendLine(line);
			}

			return builder.ToString();
		}

		public bool Any()
		{
			return this.Values.Count > 0;
		}
	}
}
namespace Common.Communication.Model.SCADA
{
	using Common.Communication.Model.SCADA.Interface;
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.Text;

	[DataContract]
	public class ScadaCoilAddressChanges : IScadaChanges<bool>
	{
		[DataMember]
		public Dictionary<int, bool> Values { get; set; }

		[DataMember]
		public int Meter { get; set; }

		public ScadaCoilAddressChanges()
		{
			this.Meter = 0;
			this.Values = new Dictionary<int, bool>(ushort.MaxValue);
		}

		public void Update(IReadOnlyList<int> ids, IReadOnlyList<bool> values)
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
				var line = $"\tId: {keyValuePair.Key}, Value: {keyValuePair.Value}";
				builder.AppendLine(line);
			}

			return builder.ToString();
		}

		public bool Any()
		{
			return this.Values.Count > 0;
		}

		public void MeterAdd(int value)
		{
			Meter += value;
		}
	}
}
using System.Runtime.Serialization;

namespace Common.Model.UI
{
	[DataContract]
	public class UIElectricityMeter
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public long Value { get; set; }

		public UIElectricityMeter()
		{
		}

		public UIElectricityMeter(int id, long value)
		{
			Id = id;
			Value = value;
		}
	}
}

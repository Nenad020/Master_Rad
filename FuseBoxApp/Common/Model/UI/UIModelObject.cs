using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Model.UI
{
	[DataContract]
	public class UIModelObject
	{
		[DataMember]
		public List<UIBreaker> UIBreakers { get; set; }

		[DataMember]
		public List<UIElectricityMeter> UIElectricityMeters { get; set; }

		[DataMember]
		public List<UIAlarm> UIAlarms { get; set; }

		public UIModelObject()
		{
			UIBreakers = new List<UIBreaker>();
			UIElectricityMeters = new List<UIElectricityMeter>();
			UIAlarms = new List<UIAlarm>();
		}
	}
}

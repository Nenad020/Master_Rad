using System;
using System.Runtime.Serialization;

namespace Common.Model.UI
{
	[DataContract]
	public class UIAlarm
	{
		[DataMember]
		public int BreakerId { get; set; }

		[DataMember]
		public DateTime Timestamp { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public bool State { get; set; }

		public UIAlarm()
		{
		}

		public UIAlarm(int breakerId, DateTime timestamp, string message, bool state)
		{
			BreakerId = breakerId;
			Timestamp = timestamp;
			Message = message;
			State = state;
		}
	}
}

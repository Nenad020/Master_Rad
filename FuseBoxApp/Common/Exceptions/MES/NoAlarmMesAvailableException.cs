using System;

namespace Common.Exceptions.MES
{
	public class NoAlarmMesAvailableException : Exception
	{
		public NoAlarmMesAvailableException()
		{
		}

		public NoAlarmMesAvailableException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

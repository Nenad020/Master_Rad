using System;

namespace Common.Exceptions.MES
{
	public class NoElecticityMeterMesAvailableException : Exception
	{
		public NoElecticityMeterMesAvailableException()
		{
		}

		public NoElecticityMeterMesAvailableException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

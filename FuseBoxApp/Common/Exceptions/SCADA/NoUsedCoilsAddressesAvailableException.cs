using System;

namespace Common.Exceptions.SCADA
{
	public class NoUsedCoilsAddressesAvailableException : Exception
	{
		public NoUsedCoilsAddressesAvailableException()
		{
		}

		public NoUsedCoilsAddressesAvailableException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

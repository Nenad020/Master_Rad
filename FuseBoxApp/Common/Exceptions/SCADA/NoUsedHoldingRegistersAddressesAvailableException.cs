using System;

namespace Common.Exceptions.Scada
{
	public class NoUsedHoldingRegistersAddressesAvailableException : Exception
	{
		public NoUsedHoldingRegistersAddressesAvailableException()
		{
		}

		public NoUsedHoldingRegistersAddressesAvailableException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

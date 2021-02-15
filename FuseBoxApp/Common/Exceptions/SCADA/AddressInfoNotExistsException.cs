using System;

namespace Common.Exceptions.SCADA
{
	public class CoilAddressNotExistsException : Exception
	{
		public CoilAddressNotExistsException()
		{

		}

		public CoilAddressNotExistsException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

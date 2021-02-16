using System;

namespace Common.Exceptions.MES
{
	public class NoBreakerMesAvailableException : Exception
	{
		public NoBreakerMesAvailableException()
		{
		}

		public NoBreakerMesAvailableException(string reason)
		{
			this.Reason = reason;
		}

		public string Reason { get; set; }
	}
}

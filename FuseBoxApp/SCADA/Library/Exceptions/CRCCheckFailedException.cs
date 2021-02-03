namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if CRC Check failed
	/// </summary>
	public class CRCCheckFailedException : ModbusException
	{
		public CRCCheckFailedException()
			: base()
		{
		}

		public CRCCheckFailedException(string message)
			: base(message)
		{
		}

		public CRCCheckFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected CRCCheckFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
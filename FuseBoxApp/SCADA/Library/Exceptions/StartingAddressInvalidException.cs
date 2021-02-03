namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if Modbus Server returns error code "starting adddress and quantity invalid"
	/// </summary>
	public class StartingAddressInvalidException : ModbusException
	{
		public StartingAddressInvalidException()
			: base()
		{
		}

		public StartingAddressInvalidException(string message)
			: base(message)
		{
		}

		public StartingAddressInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected StartingAddressInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
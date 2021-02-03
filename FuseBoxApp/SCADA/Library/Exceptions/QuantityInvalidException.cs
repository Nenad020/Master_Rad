namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if Modbus Server returns error code "quantity invalid"
	/// </summary>
	public class QuantityInvalidException : ModbusException
	{
		public QuantityInvalidException()
			: base()
		{
		}

		public QuantityInvalidException(string message)
			: base(message)
		{
		}

		public QuantityInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QuantityInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
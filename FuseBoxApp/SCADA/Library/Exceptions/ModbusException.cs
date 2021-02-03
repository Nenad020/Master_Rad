namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if Modbus Server returns error code "Function Code not executed (0x04)"
	/// </summary>
	public class ModbusException : Exception
	{
		public ModbusException()
			: base()
		{
		}

		public ModbusException(string message)
			: base(message)
		{
		}

		public ModbusException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ModbusException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if Modbus Server returns error code "Function code not supported"
	/// </summary>
	public class FunctionCodeNotSupportedException : ModbusException
	{
		public FunctionCodeNotSupportedException()
			: base()
		{
		}

		public FunctionCodeNotSupportedException(string message)
			: base(message)
		{
		}

		public FunctionCodeNotSupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected FunctionCodeNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
namespace EasyModbus.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Exception to be thrown if Connection to Modbus device failed
	/// </summary>
	public class ConnectionException : ModbusException
	{
		public ConnectionException()
			: base()
		{
		}

		public ConnectionException(string message)
			: base(message)
		{
		}

		public ConnectionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
using System;
using System.Runtime.Serialization;

namespace EasyModbus.Exceptions
{
	/// <summary>
	/// Exception to be thrown if serial port is not opened
	/// </summary>
	public class SerialPortNotOpenedException : ModbusException
	{
		public SerialPortNotOpenedException()
			: base()
		{
		}

		public SerialPortNotOpenedException(string message)
			: base(message)
		{
		}

		public SerialPortNotOpenedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected SerialPortNotOpenedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
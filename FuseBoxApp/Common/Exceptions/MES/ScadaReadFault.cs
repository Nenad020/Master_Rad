using System.Runtime.Serialization;

namespace Common.Exceptions.MES
{
	[DataContract]
	public class ScadaReadFault
	{
		public ScadaReadFault()
		{
		}

		public ScadaReadFault(string message)
		{
			Message = message;
		}

		[DataMember]
		public string Message { get; set; }
	}
}

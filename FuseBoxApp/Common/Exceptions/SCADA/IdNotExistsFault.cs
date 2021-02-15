using System.Runtime.Serialization;

namespace Common.Exceptions.SCADA
{
	[DataContract]
	public class IdNotExistsFault
	{
		public IdNotExistsFault()
		{
		}

		public IdNotExistsFault(long gid)
		{
			this.GID = gid;
		}

		[DataMember]
		public long GID { get; set; }
	}
}

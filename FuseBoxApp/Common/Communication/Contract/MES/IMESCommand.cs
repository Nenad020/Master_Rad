using System.ServiceModel;

namespace Common.Communication.Contract.MES
{
	[ServiceContract]
	public interface IMESCommand
	{
		[OperationContract]
		bool Open(int id);

		[OperationContract]
		bool Close(int id);
	}
}
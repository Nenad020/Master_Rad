using Common.Exceptions.SCADA;
using System.ServiceModel;

namespace Common.Communication.Contract.SCADA
{
	[ServiceContract]
	public interface ICommand
	{
		[OperationContract]
		[FaultContract(typeof(IdNotExistsFault))]
		bool Open(int id);

		[OperationContract]
		[FaultContract(typeof(IdNotExistsFault))]
		bool Close(int id);
	}
}
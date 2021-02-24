using Common.Communication.Model.MES;
using System.ServiceModel;

namespace Common.Communication.Contract.MES
{
	[ServiceContract]
	public interface IMesInitValue
	{
		[OperationContract]
		MesBreakerInit GetBreakers();

		[OperationContract]
		MesMeterInit GetMeters();
	}
}

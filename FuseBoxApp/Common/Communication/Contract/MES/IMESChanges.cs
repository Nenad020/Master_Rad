using Common.Communication.Model.SCADA;
using System.ServiceModel;

namespace Common.Communication.Contract.MES
{
	[ServiceContract]
	public interface IMESChanges
	{
		[OperationContract]
		void BreakerChange(ScadaCoilAddressChanges coilAddressChanges);
	}
}

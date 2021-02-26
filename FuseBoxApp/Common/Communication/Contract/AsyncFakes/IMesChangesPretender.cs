using Common.Communication.Contract.MES;
using Common.Model.SCADA;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Common.Communication.Contract.AsyncFakes
{
	[ServiceContract(Name = nameof(IMESChanges))]
	public interface IMesChangesPretender : IMESChanges
	{
		[OperationContract]
		Task BreakerChangeAsync(ScadaCoilAddressChanges coilAddressChanges);
	}
}
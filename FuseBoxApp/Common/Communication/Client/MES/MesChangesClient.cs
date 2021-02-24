using Common.Communication.Contract.AsyncFakes;
using Common.Communication.Model.SCADA;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Common.Communication.Client.MES
{
	public class MesChangesClient : ClientBase<IMesChangesPretender>, IMesChangesPretender
    {
        public MesChangesClient()
        {
        }

        public MesChangesClient(string endpointConfigurationName)
           : base(endpointConfigurationName)
        {
        }

		public void BreakerChange(ScadaCoilAddressChanges coilAddressChanges)
		{
            Channel.BreakerChange(coilAddressChanges);
        }

        public Task BreakerChangeAsync(ScadaCoilAddressChanges coilAddressChanges)
        {
            return Channel.BreakerChangeAsync(coilAddressChanges);
        }
	}
}

using Common.Communication.Contract.MES;
using Common.Communication.Model.MES;
using System.ServiceModel;

namespace Common.Communication.Client.MES
{
	public class MesToScadaInitClient : ClientBase<IMesInitValue>, IMesInitValue
	{
		public MesToScadaInitClient()
		{
		}

		public MesToScadaInitClient(string endpointConfigurationName)
		   : base(endpointConfigurationName)
		{
		}

		public MesBreakerInit GetBreakers()
		{
			return Channel.GetBreakers();
		}

		public MesMeterInit GetMeters()
		{
			return Channel.GetMeters();
		}
	}
}

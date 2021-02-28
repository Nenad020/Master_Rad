using Common.Communication.Contract.MES;
using Common.Model.MES;
using MesService.Model;
using System.ServiceModel;

namespace MesService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class MesToScadaInit : IMesInitValue
	{
		private MesModel mesModel;

		private MesBreakerInit mesBreakerInit;

		private MesMeterInit mesMeterInit;

		public MesToScadaInit(MesModel mesModel)
		{
			this.mesModel = mesModel;
			mesBreakerInit = new MesBreakerInit();
			mesMeterInit = new MesMeterInit();
		}

		public MesBreakerInit GetBreakers()
		{
			mesBreakerInit.Clear();
			foreach (var breaker in mesModel.Breakers)
			{
				mesBreakerInit.Add(breaker.Key, breaker.Value.CurrentState);
			}

			return mesBreakerInit;
		}

		public MesMeterInit GetMeters()
		{
			mesMeterInit.Clear();
			foreach (var meter in mesModel.ElecticityMeters)
			{
				mesMeterInit.Add(meter.Key, meter.Value.Value);
			}

			return mesMeterInit;
		}
	}
}

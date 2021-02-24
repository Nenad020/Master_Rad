using Common.Communication;
using Common.Communication.Access.MES;
using Common.Communication.Contract.MES;
using Common.Communication.Model.SCADA;
using MesDbAccess.Model;
using MesService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MesService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class MesChanges : IMESChanges
	{
		private IMesDbAccess<AlarmMe> alarmAccess;

		private IMesDbAccess<BreakerMe> breakerAccess;

		private IMesDbAccess<ElectricityMeterMe> electricityMeterAccess;

		private MesModel mesModel;

		private readonly WCFRequestsQueue<ScadaCoilAddressChanges> scadaChangesQueue;

		private List<AlarmMe> alarmsDb = null;

		private List<BreakerMe> breakersDb = null;

		private List<ElectricityMeterMe> metersDb = null;

		public MesChanges(IMesDbAccess<AlarmMe> alarmAccess, IMesDbAccess<BreakerMe> breakerAccess, IMesDbAccess<ElectricityMeterMe> electricityMeterAccess,
			MesModel mesModel)
		{
			this.alarmAccess = alarmAccess;
			this.breakerAccess = breakerAccess;
			this.electricityMeterAccess = electricityMeterAccess;
			this.mesModel = mesModel;

			alarmsDb = new List<AlarmMe>();
			breakersDb = new List<BreakerMe>();
			metersDb = new List<ElectricityMeterMe>();

			scadaChangesQueue = new WCFRequestsQueue<ScadaCoilAddressChanges>(HandleScadaChanges, 2000);
			scadaChangesQueue.StartReadingThread();
		}

		public void BreakerChange(ScadaCoilAddressChanges coilAddressChanges)
		{
			scadaChangesQueue.Enqueue(coilAddressChanges);
		}

		private async Task HandleScadaChanges(ScadaCoilAddressChanges coilAddressChanges)
		{
			ClearListDbs();

			foreach (var change in coilAddressChanges.Values)
			{
				int id = change.Key;
				bool state = change.Value;
				var breaker = mesModel.GetBreaker(id);

				if (breaker.CurrentState == state)
				{
					throw new InvalidOperationException("MES and SCADA have inconsistent models");
				}
				else
				{
					breaker.LastState = breaker.CurrentState;
					breaker.CurrentState = state;
				}

				mesModel.UpdateBreaker(breaker);
				breakersDb.Add(ModelFactory.CreateBreakerMes(id, breaker.Name, breaker.CurrentState, breaker.LastState));
				//TODO: Dodati taj prekidac u UI model

				var alarm = ModelFactory.CreateAlarmMes(id, CreateAlarmMessage(id, breaker.CurrentState));
				mesModel.UpdateAlarm(alarm);
				alarmsDb.Add(alarm);
				//TODO: Dodati alarm u UI model
			}

			mesModel.UpdateMeter(coilAddressChanges.Meter);
			metersDb.Add(ModelFactory.CreateElectricityMeterMes(1, coilAddressChanges.Meter));
			//TODO: Dodati taj metar u UI model

			alarmAccess.AddEntity(alarmsDb);
			breakerAccess.UpdateEntity(breakersDb);
			electricityMeterAccess.UpdateEntity(metersDb);

			//TODO: Obavesiti UI
			await Task.Delay(2000);
		}

		private void ClearListDbs()
		{
			alarmsDb.Clear();
			breakersDb.Clear();
			metersDb.Clear();
		}

		private string CreateAlarmMessage(int id, bool currentState)
		{
			if (currentState)
			{
				return $"Breaker with ID: {id} has been turned on!";
			}
			else
			{
				return $"Breaker with ID: {id} has been turned off!";
			}
		}
	}
}

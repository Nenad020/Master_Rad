using Common.Communication;
using Common.Communication.Access.MES;
using Common.Communication.Client.UI;
using Common.Communication.Contract.MES;
using Common.Model.SCADA;
using Common.Model.UI;
using MesDbAccess.Model;
using MesService.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
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
			UIModelObject uIModelObject = new UIModelObject();

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
				uIModelObject.UIBreakers.Add(new UIBreaker(id, breaker.Name, breaker.CurrentState, breaker.LastState));

				var alarm = ModelFactory.CreateAlarmMes(id, CreateAlarmMessage(id, breaker.CurrentState));
				mesModel.UpdateAlarm(alarm);
				alarmsDb.Add(alarm);

				if (breaker.CurrentState)
				{
					uIModelObject.UIAlarms.Add(new UIAlarm(alarm.BreakerId, alarm.Timestamp, alarm.Message, true));
				}
				else
				{
					uIModelObject.UIAlarms.Add(new UIAlarm(alarm.BreakerId, alarm.Timestamp, alarm.Message, false));
				}
				
			}

			/*mesModel.UpdateMeter(coilAddressChanges.Meter);
			metersDb.Add(ModelFactory.CreateElectricityMeterMes(1, coilAddressChanges.Meter));
			uIModelObject.UIElectricityMeters.Add(new UIElectricityMeter(1, coilAddressChanges.Meter));*/

			UpdateDbs();

			await SendChangesToUI(uIModelObject).ContinueWith(PublishFinished);
		}

		private void ClearListDbs()
		{
			alarmsDb.Clear();
			breakersDb.Clear();
			metersDb.Clear();
		}

		private void UpdateDbs()
		{
			alarmAccess.AddEntity(alarmsDb);
			breakerAccess.UpdateEntity(breakersDb);
			electricityMeterAccess.UpdateEntity(metersDb);
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

		private async Task SendChangesToUI(UIModelObject uiChanges)
		{
			using (UIChangesClient client = new UIChangesClient())
			{
				client.Open();

				try
				{
					await client.ObjectChangeAsync(uiChanges);
				}
				catch
				{
				}
			}
		}

		private void PublishFinished(Task obj)
		{
		}
	}
}

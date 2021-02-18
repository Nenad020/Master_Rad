using Common.Communication.Access.MES;
using MesDbAccess.Access;
using MesDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesService.Model
{
	public class MesModel
	{
		private IMesDbAccess<AlarmMe> alarmAccess;

		private IMesDbAccess<BreakerMe> breakerAccess;

		private IMesDbAccess<ElectricityMeterMe> electricityMeterAccess;

		public Dictionary<int, AlarmMe> Alarms { get; private set; }

		public Dictionary<int, BreakerMe> Breakers { get; private set; }

		public Dictionary<int, ElectricityMeterMe> ElecticityMeters { get; private set; }

		public MesModel(IMesDbAccess<AlarmMe> alarmAccess, IMesDbAccess<BreakerMe> breakerAccess, IMesDbAccess<ElectricityMeterMe> electricityMeterAccess)
		{
			this.alarmAccess = alarmAccess;
			this.breakerAccess = breakerAccess;
			this.electricityMeterAccess = electricityMeterAccess;
		}

		public void Initialize()
		{
			var allAlarms = alarmAccess.GetAllEntities();
			InitAlarms(allAlarms);

			var allBreakers = breakerAccess.GetAllEntities();
			InitBreakers(allBreakers);

			var allMeters = electricityMeterAccess.GetAllEntities();
			InitMeters(allMeters);
		}

		private void InitAlarms(List<AlarmMe> alarms)
		{
			Alarms = new Dictionary<int, AlarmMe>(alarms.Count);
			foreach (var alarm in alarms)
			{
				AlarmMe alarmMe = null;
				if (Alarms.TryGetValue(alarm.BreakerId, out alarmMe) && alarm.Timestamp > alarmMe.Timestamp)
				{
					Alarms[alarm.BreakerId].Timestamp = alarm.Timestamp;
				}
				else
				{
					Alarms.Add(alarm.BreakerId, alarm);
				}
			}
		}

		private void InitBreakers(List<BreakerMe> breakers)
		{
			Breakers = new Dictionary<int, BreakerMe>(breakers.Count);
			breakers.ForEach(x => Breakers.Add(x.Id, x));
		}

		private void InitMeters(List<ElectricityMeterMe> meters)
		{
			ElecticityMeters = new Dictionary<int, ElectricityMeterMe>(meters.Count);
			meters.ForEach(x => ElecticityMeters.Add(x.Id, x));
		}
	}
}

using Common.Communication.Access.MES;
using MesDbAccess.Model;
using System;
using System.Collections.Generic;

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

		public BreakerMe GetBreaker(int id)
		{
			BreakerMe breaker;
			if (!Breakers.TryGetValue(id, out breaker))
			{
				throw new ArgumentException($"Breaker with id {id} doesn't exist in the model.");
			}

			return breaker;
		}

		public ElectricityMeterMe GetMeter(int id)
		{
			ElectricityMeterMe meter;
			if (!ElecticityMeters.TryGetValue(id, out meter))
			{
				throw new ArgumentException($"Electricity meter with id {id} doesn't exist in the model.");
			}

			return meter;
		}

		public void UpdateAlarm(AlarmMe alarm)
		{
			AlarmMe currentAlarm;
			if (!Alarms.TryGetValue(alarm.BreakerId, out currentAlarm))
			{
				Alarms.Add(alarm.BreakerId, alarm);
			}
			else
			{
				Alarms[alarm.Id] = alarm;
			}
		}

		public void UpdateBreaker(BreakerMe breaker)
		{
			BreakerMe currentBreaker;
			if (!Breakers.TryGetValue(breaker.Id, out currentBreaker))
			{
				throw new ArgumentException($"Breaker with id {breaker.Id} doesn't exist in the model.");
			}

			Breakers[breaker.Id] = breaker;
		}

		public void UpdateMeter(int value)
		{
			ElectricityMeterMe meter;
			if (!ElecticityMeters.TryGetValue(1, out meter))
			{
				throw new ArgumentException($"Electricity meter with id {1} doesn't exist in the model.");
			}

			ElecticityMeters[1].Value = value;
		}
	}
}

using System;

namespace MesDbAccess.Model
{
	public static class ModelFactory
	{
		public static AlarmMe CreateAlarmMes(int breakerId, string message)
		{
			var date = DateTime.Now;

			var alarm = new AlarmMe
			{
				BreakerId = breakerId,
				Timestamp = date,
				Message = message
			};

			return alarm;
		}

		public static BreakerMe CreateBreakerMes(int id, string name, bool currentState, bool lastState)
		{
			var breaker = new BreakerMe
			{
				Id = id,
				Name = name,
				CurrentState = currentState,
				LastState = lastState
			};

			return breaker;
		}

		public static ElectricityMeterMe CreateElectricityMeterMes(int id, long value)
		{
			var meter = new ElectricityMeterMe
			{
				Id = id,
				Value = value
			};

			return meter;
		}
	}
}

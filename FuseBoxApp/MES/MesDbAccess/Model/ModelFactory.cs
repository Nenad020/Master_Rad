using System;

namespace MesDbAccess.Model
{
	public static class ModelFactory
	{
		public static AlarmMe CreateAlarmMes(int id, int breakerId, string message)
		{
			var date = DateTime.Now;

			var alarm = new AlarmMe
			{
				Id = id,
				BreakerId = breakerId,
				Timestamp = date,
				Message = message
			};

			return alarm;
		}

		public static BreakerMe CreateBreakerMes(int id, string name)
		{
			var breaker = new BreakerMe
			{
				Id = id,
				Name = name,
			};

			return breaker;
		}
	}
}

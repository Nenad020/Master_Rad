using MesDbAccess.Model;
using System;
using System.Collections.Generic;

namespace MesDbAccess.Access.Interfaces
{
	public interface IMesDbProceduresAccess
	{
		List<AlarmHistoryFromTo_Result> GetAlarmHistory(DateTime from, DateTime to);
	}
}

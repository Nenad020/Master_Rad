using Common.Communication.Contract.MES;
using Common.Model.Report;
using MesService.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using MesDbAccess.Access;

namespace MesService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class MesReportGenerator : IReports
	{
		private AlarmAccess alarmAccess;

		private MesModel mesModel;

		public MesReportGenerator(AlarmAccess alarmAccess, MesModel mesModel)
		{
			this.alarmAccess = alarmAccess;
			this.mesModel = mesModel;
		}

		public ReportModel ActiveEquipment()
		{
			ReportModel reportModel = new ReportModel(new List<string> { "ID", "Name", "Current state", "Last state" });
			
			var breakers = mesModel.GetBreakers();
			foreach (var breaker in breakers)
			{
				reportModel.AddRow(breaker.Id.ToString(), breaker.Name, breaker.CurrentState.ToString(), breaker.LastState.ToString());
			}

			return reportModel;
		}

		public ReportModel AlarmHistory(DateTime from, DateTime to)
		{
			ReportModel reportModel = new ReportModel(new List<string> { "ID", "Breaker ID", "Timestamp", "Message" });

			var alarms = alarmAccess.GetAlarmHistory(from, to);
			foreach (var alarm in alarms)
			{
				reportModel.AddRow(alarm.Id.ToString(), alarm.BreakerId.ToString(), alarm.Timestamp.ToString(), alarm.Message);
			}

			return reportModel;
		}

		public ReportModel SpecificActiveEquipment(int id)
		{
			ReportModel reportModel = new ReportModel(new List<string> { "ID", "Name", "Current state", "Last state" });

			var breaker = mesModel.GetBreaker(id);
			reportModel.AddRow(breaker.Id.ToString(), breaker.Name, breaker.CurrentState.ToString(), breaker.LastState.ToString());

			return reportModel;
		}
	}
}

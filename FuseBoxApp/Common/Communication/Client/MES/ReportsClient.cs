using System.ServiceModel;
using System;
using Common.Communication.Contract.MES;
using Common.Model.Report;

namespace Common.Communication.Client.MES
{
	public class ReportsClient : ClientBase<IReports>, IReports
	{
		public ReportsClient()
		{
		}

		public ReportsClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public ReportModel ActiveEquipment()
		{
			return Channel.ActiveEquipment();
		}

		public ReportModel AlarmHistory(DateTime from, DateTime to)
		{
			return Channel.AlarmHistory(from, to);
		}

		public ReportModel SpecificActiveEquipment(int id)
		{
			return Channel.SpecificActiveEquipment(id);
		}
	}
}

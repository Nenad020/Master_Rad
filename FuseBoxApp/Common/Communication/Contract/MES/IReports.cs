using Common.Model.Report;
using System;
using System.ServiceModel;

namespace Common.Communication.Contract.MES
{
	[ServiceContract]
    public interface IReports
    {
        [OperationContract]
        ReportModel ActiveEquipment();

        [OperationContract]
        ReportModel SpecificActiveEquipment(int id);

        [OperationContract]
        ReportModel AlarmHistory(DateTime from, DateTime to);
    }
}

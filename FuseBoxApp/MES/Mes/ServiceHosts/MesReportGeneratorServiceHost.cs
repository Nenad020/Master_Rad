using Common.Communication.Contract.MES;
using MesService;
using MesService.Model;
using System;
using System.ServiceModel;
using MesDbAccess.Access;

namespace Mes.ServiceHosts
{
	public class MesReportGeneratorServiceHost
	{
        private readonly ServiceHost host;

        public MesReportGeneratorServiceHost(AlarmAccess alarmAccess, MesModel mesModel)
        {
            var mesReportGenerator = new MesReportGenerator(alarmAccess, mesModel);
            host = new ServiceHost(mesReportGenerator);
            ServiceInstance = mesReportGenerator;
        }

        public IReports ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("MES Report Generator Service Started...");
            Console.WriteLine("Endpoints:");

            foreach (Uri uri in host.BaseAddresses)
            {
                Console.WriteLine(uri);
            }

            Console.WriteLine();
        }

        public void Close()
        {
            host.Close();
            Console.WriteLine("MES Report Generator Service Stopped...");
        }
    }
}

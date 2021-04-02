using MesDbAccess.Access.Interfaces;
using Common.Communication.Contract.MES;
using MesDbAccess.Model;
using MesService;
using MesService.Model;
using System;
using System.ServiceModel;

namespace Mes.ServiceHosts
{
	public class MesChangesServiceHost
	{
        private readonly ServiceHost host;

        public MesChangesServiceHost(IMesDbAccess<AlarmMe> alarmAccess, IMesDbAccess<BreakerMe> breakerAccess, IMesDbAccess<ElectricityMeterMe> electricityMeterAccess,
            MesModel mesModel)
        {
            var mesChanges = new MesChanges(alarmAccess, breakerAccess, electricityMeterAccess, mesModel);
            host = new ServiceHost(mesChanges);
            ServiceInstance = mesChanges;
        }

        public IMESChanges ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("MES Changes Service Started...");
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
            Console.WriteLine("MES Changes Service Stopped...");
        }
    }
}

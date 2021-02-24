using Common.Communication.Contract.MES;
using MesService;
using MesService.Model;
using System;
using System.ServiceModel;

namespace Mes.ServiceHosts
{
	public class MesToScadaInitServiceHost
    {
        private readonly ServiceHost host;

        public MesToScadaInitServiceHost(MesModel mesModel)
        {
            var mesInit = new MesToScadaInit(mesModel);
            host = new ServiceHost(mesInit);
            ServiceInstance = mesInit;
        }

        public IMesInitValue ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("MES to SCADA Init Service Started...");
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
            Console.WriteLine("MES to SCADA Init Service Stopped...");
        }
    }
}

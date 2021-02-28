using Common.Communication.Contract.MES;
using MesService;
using System;
using System.ServiceModel;

namespace Mes.ServiceHosts
{
	public class MesCommandServiceHost
    {
        private readonly ServiceHost host;

        public MesCommandServiceHost()
        {
            var mesCommand = new MesCommand();
            host = new ServiceHost(mesCommand);
            ServiceInstance = mesCommand;
        }

        public IMESCommand ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("MES command Service Started...");
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
            Console.WriteLine("MES command Service Stopped...");
        }
    }
}

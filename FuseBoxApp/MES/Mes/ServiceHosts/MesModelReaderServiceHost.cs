using Common.Communication.Contract.MES;
using MesService;
using MesService.Model;
using System;
using System.ServiceModel;

namespace Mes.ServiceHosts
{
	public class MesModelReaderServiceHost
	{
        private readonly ServiceHost host;

        public MesModelReaderServiceHost(MesModel mesModel)
        {
            var mesReader = new MesModelReader(mesModel);
            host = new ServiceHost(mesReader);
            ServiceInstance = mesReader;
        }

        public IMESModelReader ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("MES model reader Service Started...");
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
            Console.WriteLine("MES model reader Service Stopped...");
        }
    }
}

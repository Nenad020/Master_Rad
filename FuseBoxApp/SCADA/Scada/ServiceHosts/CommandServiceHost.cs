using Common.Communication.Contract.SCADA;
using ScadaService;
using System;
using System.ServiceModel;

namespace Scada.ServiceHosts
{
    public class CommandServiceHost
    {
        private readonly ServiceHost host;

        public CommandServiceHost(ScadaModel scadaModel)
        {
            var command = new Command(scadaModel);
            host = new ServiceHost(command);
            ServiceInstance = command;
        }

        public ICommand ServiceInstance { get; }

        public void Open()
        {
            host.Open();
            Console.WriteLine("Command Service Started...");
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
            Console.WriteLine("Command Service Stopped...");
        }
    }
}
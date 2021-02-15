using Common.Interfaces.SCADA.Access;
using Scada.ServiceHosts;
using ScadaDbAccess.Access;
using ScadaDbAccess.Model;
using ScadaService;
using System;

namespace Scada
{
	internal class Program
	{
		private static CommandServiceHost commandService;

		private static IScadaDbAccess<CoilsAddress> coilAddressesAccess;

		private static IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess;

		private static ScadaModel scadaModel;

		private static Poller poller;

		public static void Main()
		{
			WriteServiceName();
			coilAddressesAccess = new CoilsAddressAccess();
			holdingRegistersAddressAccess = new HoldingRegistersAddressAccess();

			scadaModel = new ScadaModel(coilAddressesAccess, holdingRegistersAddressAccess);
			scadaModel.Initialize();

			poller = new Poller(scadaModel, 1000, coilAddressesAccess, holdingRegistersAddressAccess);
			scadaModel.UsedAddressesUpdated += (sender, args) => poller.UpdateAddressesToPoll();

			commandService = new CommandServiceHost(scadaModel);
			commandService.Open();

			poller.StartPolling();
			Console.ReadLine();
			GracefulShutdown();
		}

		private static void WriteServiceName()
		{
			var serviceName = "SCADA";

			Console.Title = serviceName;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(serviceName);
			Console.ResetColor();
		}

		private static void GracefulShutdown()
		{
			commandService?.Close();
		}
	}
}
using Common.Communication.Access.MES;
using Mes.ServiceHosts;
using MesDbAccess.Access;
using MesDbAccess.Model;
using MesService.Model;
using System;

namespace Mes
{
	class Program
	{
		private static IMesDbAccess<AlarmMe> alarmAccess;

		private static IMesDbAccess<BreakerMe> breakerAccess;

		private static IMesDbAccess<ElectricityMeterMe> electricityMeterAccess;

		private static MesToScadaInitServiceHost mesToScadaInitServiceHost;

		private static MesModel mesModel;

		static void Main(string[] args)
		{
			WriteServiceName();

			alarmAccess = new AlarmAccess();
			breakerAccess = new BreakerAccess();
			electricityMeterAccess = new ElectricityMeterAccess();

			mesModel = new MesModel(alarmAccess, breakerAccess, electricityMeterAccess);
			mesModel.Initialize();

			mesToScadaInitServiceHost = new MesToScadaInitServiceHost(mesModel);
			mesToScadaInitServiceHost.Open();

			Console.ReadLine();
			GracefulShutdown();
		}

		private static void WriteServiceName()
		{
			var serviceName = "MES";

			Console.Title = serviceName;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(serviceName);
			Console.ResetColor();
		}

		private static void GracefulShutdown()
		{
			mesToScadaInitServiceHost?.Close();
		}
	}
}

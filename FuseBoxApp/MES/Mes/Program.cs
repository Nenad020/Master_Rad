using MesDbAccess.Access.Interfaces;
using Mes.ServiceHosts;
using MesDbAccess.Access;
using MesDbAccess.Model;
using MesService.Model;
using System;

namespace Mes
{
	class Program
	{
		private static AlarmAccess alarmAccess;

		private static BreakerAccess breakerAccess;

		private static ElectricityMeterAccess electricityMeterAccess;

		private static MesToScadaInitServiceHost mesToScadaInitServiceHost;

		private static MesChangesServiceHost mesChangesServiceHost;

		private static MesModelReaderServiceHost mesModelReaderServiceHost;

		private static MesCommandServiceHost mesCommandServiceHost;

		private static MesReportGeneratorServiceHost mesReportGeneratorServiceHost;

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

			mesChangesServiceHost = new MesChangesServiceHost(alarmAccess, breakerAccess, electricityMeterAccess, mesModel);
			mesChangesServiceHost.Open();

			mesModelReaderServiceHost = new MesModelReaderServiceHost(mesModel);
			mesModelReaderServiceHost.Open();

			mesCommandServiceHost = new MesCommandServiceHost();
			mesCommandServiceHost.Open();

			mesReportGeneratorServiceHost = new MesReportGeneratorServiceHost(alarmAccess, mesModel);
			mesReportGeneratorServiceHost.Open();

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
			mesChangesServiceHost?.Close();
			mesModelReaderServiceHost?.Close();
			mesCommandServiceHost?.Close();
			mesReportGeneratorServiceHost?.Close();
		}
	}
}

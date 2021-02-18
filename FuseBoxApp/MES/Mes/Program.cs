using Common.Communication.Access.MES;
using MesDbAccess.Access;
using MesDbAccess.Model;
using MesService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes
{
	class Program
	{
		private static IMesDbAccess<AlarmMe> alarmAccess;

		private static IMesDbAccess<BreakerMe> breakerAccess;

		private static IMesDbAccess<ElectricityMeterMe> electricityMeterAccess;

		private static MesModel mesModel;

		static void Main(string[] args)
		{
			WriteServiceName();

			alarmAccess = new AlarmAccess();
			breakerAccess = new BreakerAccess();
			electricityMeterAccess = new ElectricityMeterAccess();

			mesModel = new MesModel(alarmAccess, breakerAccess, electricityMeterAccess);
			mesModel.Initialize();

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
		}
	}
}

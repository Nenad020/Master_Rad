using Common.Communication.Client.SCADA;
using ScadaDbAccess.Access;
using ScadaDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MesDbAccess.Access;
using MesDbAccess.Model;
using Common.Communication.Client.MES;

namespace DummyApp
{
	class Program
	{
		static void Main(string[] args)
		{
			/*BreakerAccess access = new BreakerAccess();
			var entities = new List<BreakerMe>();
			for (int i = 1; i < 16; i++)
			{
				entities.Add(MesDbAccess.Model.ModelFactory.CreateBreakerMes(i, $"Breaker_{i}", false, false));
			}

			access.AddEntity(entities);*/

			using (ReportsClient client = new ReportsClient())
			{
				try
				{
					DateTime from = new DateTime(2021, 3, 5, 13, 35, 0);
					DateTime to = new DateTime(2021, 3, 5, 13, 36, 30);

					var result = client.AlarmHistory(from, to);

					foreach (var item in result.Headers)
					{
						Console.Write($"{item} \t\t");
					}
					Console.WriteLine();

					foreach (var item in result.Rows.Values)
					{
						foreach (var item2 in item)
						{
							Console.Write($"{item2} \t\t");
						}
						Console.WriteLine();
					}
				}
				catch
				{
					throw;
				}
			}

			/*AlarmAccess access = new AlarmAccess();
			access.AddEntity(new List<AlarmMe>()
			{
				MesDbAccess.Model.ModelFactory.CreateAlarmMes(4, "nema"),
				MesDbAccess.Model.ModelFactory.CreateAlarmMes(5, "tu tu")
			});*/

			Console.ReadLine();
		}
	}
}

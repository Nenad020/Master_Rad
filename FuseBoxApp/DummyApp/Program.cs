using Common.Communication.Client.SCADA;
using Common.Communication.Access.SCADA;
using ScadaDbAccess.Access;
using ScadaDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MesDbAccess.Access;
using MesDbAccess.Model;

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
				entities.Add(MesDbAccess.Model.ModelFactory.CreateBreakerMes(i, $"Breaker_{i}"));
			}

			access.AddEntity(entities);*/

			/*using (ScadaCommandClient client = new ScadaCommandClient())
			{
				try
				{
					client.Close(1);
				}
				catch
				{
					throw;
				}
			}*/

			AlarmAccess access = new AlarmAccess();
			access.AddEntity(new List<AlarmMe>()
			{
				MesDbAccess.Model.ModelFactory.CreateAlarmMes(4, "nema"),
				MesDbAccess.Model.ModelFactory.CreateAlarmMes(5, "tu tu")
			});

			Console.ReadLine();
		}
	}
}

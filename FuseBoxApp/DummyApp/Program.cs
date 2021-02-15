using Common.Communication.Client.SCADA;
using Common.Interfaces.SCADA.Access;
using ScadaDbAccess.Access;
using ScadaDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyApp
{
	class Program
	{
		static void Main(string[] args)
		{
			using (ScadaCommandClient client = new ScadaCommandClient())
			{
				try
				{
					client.Close(1);
				}
				catch
				{
					throw;
				}
			}

			Console.ReadLine();
		}
	}
}

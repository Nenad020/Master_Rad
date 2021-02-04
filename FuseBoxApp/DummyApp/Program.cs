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
			IScadaDbAccess<CoilsAddress> scadaDbAccess = new CoilsAddressAccess();

			var result = scadaDbAccess.GetAllUsedEntities();

			scadaDbAccess.UpdateValue(new List<CoilsAddress>() { ModelFactory.CreateCoilsAddress(1, 1, true, true), ModelFactory.CreateCoilsAddress(2, 2, true, true) });
		}
	}
}

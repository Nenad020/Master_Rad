using Common.Exceptions.SCADA;
using Common.Communication.Access.SCADA;
using ScadaDbAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace ScadaDbAccess.Access
{
	public class CoilsAddressAccess : IScadaDbAccess<CoilsAddress>
	{
		public CoilsAddressAccess()
		{
		}

		public List<CoilsAddress> GetAllUsedEntities()
		{
			using (var db = new ScadaDbEntities())
			{
				try
				{
					return db.CoilsAddresses.Where(info => info.Used).ToList();
				}
				catch
				{
					throw new NoUsedCoilsAddressesAvailableException("No used coils addresses.");
				}
			}
		}
	}
}
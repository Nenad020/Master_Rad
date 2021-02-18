using Common.Exceptions.SCADA;
using Common.Communication.Access.SCADA;
using ScadaDbAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace ScadaDbAccess.Access
{
	public class HoldingRegistersAddressAccess : IScadaDbAccess<HoldingRegistersAddress>
	{
		public HoldingRegistersAddressAccess()
		{
		}

		public List<HoldingRegistersAddress> GetAllUsedEntities()
		{
			using (var db = new ScadaDbEntities())
			{
				try
				{
					return db.HoldingRegistersAddresses.Where(info => info.Used).ToList();
				}
				catch
				{
					throw new NoUsedHoldingRegistersAddressesAvailableException("No used holding registers addresses.");
				}
			}
		}
	}
}
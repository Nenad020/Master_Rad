using Common.Exceptions.Scada;
using Common.Interfaces.SCADA.Access;
using ScadaDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
					throw new NoUsedCoilsAddressesAvailableException("No used holding registers addresses.");
				}
			}
		}

		public void UpdateValue(List<HoldingRegistersAddress> entites)
		{
			using (var db = new ScadaDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						var coilAddress = db.HoldingRegistersAddresses.Where(info => info.Address.Equals(entity.Address)).FirstOrDefault();
						if (coilAddress != null)
						{
							coilAddress.Value = entity.Value;

							db.Entry(coilAddress).State = EntityState.Modified;
						}
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to update holding registers entities in SCADA database!");
				}
			}
		}
	}
}

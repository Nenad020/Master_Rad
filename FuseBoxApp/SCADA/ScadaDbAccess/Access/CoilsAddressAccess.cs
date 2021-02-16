using Common.Exceptions.SCADA;
using Common.Communication.Access.SCADA;
using ScadaDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		public void UpdateValue(List<CoilsAddress> entites)
		{
			using (var db = new ScadaDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						var coilAddress = db.CoilsAddresses.Where(info => info.Address.Equals(entity.Address)).FirstOrDefault();
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
					throw new InvalidOperationException("Failed to update coils addresses entities in SCADA database!");
				}
			}
		}
	}
}
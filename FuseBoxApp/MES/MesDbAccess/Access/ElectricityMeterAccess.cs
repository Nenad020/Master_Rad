using MesDbAccess.Access.Interfaces;
using Common.Exceptions.MES;
using MesDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MesDbAccess.Access
{
	public class ElectricityMeterAccess : IMesDbAccess<ElectricityMeterMe>
	{
		public ElectricityMeterAccess()
		{
		}

		public void AddEntity(List<ElectricityMeterMe> entites)
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						db.ElectricityMeterMes.Add(entity);
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to add electicity meter entities in MES database!");
				}
			}
		}

		public List<ElectricityMeterMe> GetAllEntities()
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					return db.ElectricityMeterMes.ToList();
				}
				catch
				{
					throw new NoElecticityMeterMesAvailableException("No electicity meter in MES database");
				}
			}
		}

		public void UpdateEntity(List<ElectricityMeterMe> entites)
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						var dbEntity = db.ElectricityMeterMes.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();
						if (dbEntity != null)
						{
							dbEntity.Value = entity.Value;

							db.Entry(dbEntity).State = EntityState.Modified;
						}
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to update electicity meter entities in MES database!");
				}
			}
		}
	}
}

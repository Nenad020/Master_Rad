using MesDbAccess.Access.Interfaces;
using Common.Exceptions.MES;
using MesDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MesDbAccess.Access
{
	public class BreakerAccess : IMesDbAccess<BreakerMe>
	{
		public BreakerAccess()
		{
		}

		public void AddEntity(List<BreakerMe> entites)
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						db.BreakerMes.Add(entity);
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to add breaker entities in MES database!");
				}
			}
		}

		public List<BreakerMe> GetAllEntities()
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					return db.BreakerMes.ToList();
				}
				catch
				{
					throw new NoAlarmMesAvailableException("No breakers in MES database");
				}
			}
		}

		public void UpdateEntity(List<BreakerMe> entites)
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						var dbEntity = db.BreakerMes.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();
						if (dbEntity != null)
						{
							dbEntity.CurrentState = entity.CurrentState;
							dbEntity.LastState = entity.LastState;

							db.Entry(dbEntity).State = EntityState.Modified;
						}
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to update breaker entities in MES database!");
				}
			}
		}
	}
}

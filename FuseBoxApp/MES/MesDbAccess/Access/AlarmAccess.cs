using Common.Communication.Access.MES;
using Common.Exceptions.MES;
using MesDbAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MesDbAccess.Access
{
	public class AlarmAccess : IMesDbAccess<AlarmMe>
	{
		public AlarmAccess()
		{
		}

		public void AddEntity(List<AlarmMe> entites)
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					foreach (var entity in entites)
					{
						db.AlarmMes.Add(entity);
					}

					db.SaveChanges();
				}
				catch
				{
					throw new InvalidOperationException("Failed to add alarm entities in MES database!");
				}
			}
		}

		public List<AlarmMe> GetAllEntities()
		{
			using (var db = new MesDbEntities())
			{
				try
				{
					return db.AlarmMes.ToList();
				}
				catch
				{
					throw new NoAlarmMesAvailableException("No alarms in MES database");
				}
			}
		}

		public void UpdateEntity(List<AlarmMe> entites)
		{
			AddEntity(entites);
		}
	}
}

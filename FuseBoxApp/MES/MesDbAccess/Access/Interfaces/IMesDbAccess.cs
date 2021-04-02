using System.Collections.Generic;

namespace MesDbAccess.Access.Interfaces
{
	public interface IMesDbAccess<T>
	{
		List<T> GetAllEntities();

		void AddEntity(List<T> entites);

		void UpdateEntity(List<T> entites);
	}
}

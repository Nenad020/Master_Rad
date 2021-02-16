using System.Collections.Generic;

namespace Common.Communication.Access.MES
{
	public interface IMesDbAccess<T>
	{
		List<T> GetAllEntities();

		void AddEntity(List<T> entites);
	}
}

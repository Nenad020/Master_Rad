using System.Collections.Generic;

namespace Common.Interfaces.SCADA.Access
{
	public interface IScadaDbAccess<T>
	{
		List<T> GetAllUsedEntities();

		void UpdateValue(List<T> entites);
	}
}

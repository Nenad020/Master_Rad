using System.Collections.Generic;

namespace Common.Communication.Access.SCADA
{
	public interface IScadaDbAccess<T>
	{
		List<T> GetAllUsedEntities();

		void UpdateValue(List<T> entites);
	}
}

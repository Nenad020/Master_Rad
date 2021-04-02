using System.Collections.Generic;

namespace ScadaDbAccess.Access.Interfaces
{
	public interface IScadaDbAccess<T>
	{
		List<T> GetAllUsedEntities();
	}
}

using System.Collections.Generic;

namespace Common.Interfaces.SCADA.Model
{
	public interface IScadaChanges<T>
	{
		void Update(IReadOnlyList<int> ids, IReadOnlyList<T> values);

		void Clear();

		bool Any();
	}
}

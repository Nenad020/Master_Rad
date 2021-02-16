using System.Collections.Generic;

namespace Common.Communication.Model.SCADA.Interface
{
	public interface IScadaChanges<T>
	{
		void Update(IReadOnlyList<int> ids, IReadOnlyList<T> values);

		void Clear();

		bool Any();
	}
}

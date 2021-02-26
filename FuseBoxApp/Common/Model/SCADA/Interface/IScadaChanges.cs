using System.Collections.Generic;

namespace Common.Model.SCADA.Interface
{
	public interface IScadaChanges<T>
	{
		void Update(IReadOnlyList<int> ids, IReadOnlyList<T> values);

		void Clear();

		bool Any();

		void MeterAdd(int value);
	}
}

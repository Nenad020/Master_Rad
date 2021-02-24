namespace Common.Communication.Model.MES.Interface
{
	public interface IMesInit<T>
	{
		void Add(int id, T value);

		void Clear();
	}
}

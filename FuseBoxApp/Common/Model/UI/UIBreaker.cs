using System.Runtime.Serialization;

namespace Common.Model.UI
{
	[DataContract]
	public class UIBreaker
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public bool CurrentState { get; set; }

		[DataMember]
		public bool LastState { get; set; }

		public UIBreaker()
		{
		}

		public UIBreaker(int id, string name, bool currentState, bool lastState)
		{
			Id = id;
			Name = name;
			CurrentState = currentState;
			LastState = lastState;
		}
	}
}

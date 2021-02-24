using Common.Communication.Contract.MES;
using System.ServiceModel;

namespace Common.Communication.Client.MES
{
	public class MesCommandClient : ClientBase<IMESCommand>, IMESCommand
	{
		public MesCommandClient()
		{
		}

		public MesCommandClient(string endpointConfigurationName)
		   : base(endpointConfigurationName)
		{
		}

		public bool Open(int id)
		{
			return Channel.Open(id);
		}

		public bool Close(int id)
		{
			return Channel.Close(id);
		}
	}
}
using Common.Communication.Contract.MES;
using Common.Model.UI;
using System.ServiceModel;

namespace Common.Communication.Client.MES
{
	public class MESModelReaderClient : ClientBase<IMESModelReader>, IMESModelReader
	{
		public MESModelReaderClient()
		{
		}

		public MESModelReaderClient(string endpointConfigurationName)
		   : base(endpointConfigurationName)
		{
		}

		public UIModelObject GetBreakers()
		{
			return Channel.GetBreakers();
		}
	}
}

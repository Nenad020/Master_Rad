using Common.Model.UI;
using System.ServiceModel;

namespace Common.Communication.Contract.MES
{
	[ServiceContract]
	public interface IMESModelReader
	{
		[OperationContract]
		UIModelObject GetBreakers();
	}
}

using Common.Model.UI;
using System.ServiceModel;

namespace Common.Communication.Contract.UI
{
	[ServiceContract]
	public interface IUIChanges
	{
		[OperationContract]
		void ObjectChange(UIModelObject uIModelObject);
	}
}

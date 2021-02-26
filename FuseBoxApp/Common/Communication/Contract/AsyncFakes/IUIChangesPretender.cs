using Common.Communication.Contract.UI;
using Common.Model.UI;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Common.Communication.Contract.AsyncFakes
{
	[ServiceContract(Name = nameof(IUIChanges))]
	public interface IUIChangesPretender : IUIChanges
	{
		[OperationContract]
		Task ObjectChangeAsync(UIModelObject uIModelObject);
	}
}
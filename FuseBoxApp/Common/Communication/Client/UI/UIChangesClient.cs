using Common.Communication.Contract.AsyncFakes;
using Common.Model.UI;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Common.Communication.Client.UI
{
	public class UIChangesClient : ClientBase<IUIChangesPretender>, IUIChangesPretender
    {
        public UIChangesClient()
        {
        }

        public UIChangesClient(string endpointConfigurationName)
           : base(endpointConfigurationName)
        {
        }

		public void ObjectChange(UIModelObject uIModelObject)
		{
            Channel.ObjectChange(uIModelObject);
		}

		public Task ObjectChangeAsync(UIModelObject uIModelObject)
		{
            return Channel.ObjectChangeAsync(uIModelObject);
		}
	}
}

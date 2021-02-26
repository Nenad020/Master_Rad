using Common.Communication.Contract.UI;
using System.ServiceModel;

namespace FuseBoxUI.ServiceHosts.Hosts
{
	public class UIChangesServiceHost
	{
        private readonly ServiceHost host;

        public UIChangesServiceHost()
        {
            var uiChanges = new UIChanges();
            host = new ServiceHost(uiChanges);
            ServiceInstance = uiChanges;
        }

        public IUIChanges ServiceInstance { get; }

        public void Open()
        {
            host.Open();
        }

        public void Close()
        {
            host.Close();
        }
    }
}

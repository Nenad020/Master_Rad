using Common.Communication.Contract.UI;
using Common.Model.UI;
using System.ServiceModel;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ServiceHosts
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class UIChanges : IUIChanges
	{
		public UIChanges()
		{
		}

		public void ObjectChange(UIModelObject uIModelObject)
		{
			if (uIModelObject.UIAlarms.Count > 0)
			{
				ViewModelApplication.OnAlarmUpdate(uIModelObject.UIAlarms);
			}

			if (uIModelObject.UIBreakers.Count > 0)
			{
				ViewModelApplication.OnBreakerUpdate(uIModelObject.UIBreakers);
			}
		}
	}
}

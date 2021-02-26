using Common.Communication.Contract.UI;
using Common.Model.UI;
using FuseBoxUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
			ProcessBusinessLogic.OnAlarmUpdate(uIModelObject.UIAlarms);
		}
	}
}

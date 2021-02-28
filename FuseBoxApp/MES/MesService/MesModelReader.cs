using Common.Communication.Contract.MES;
using Common.Model.UI;
using MesService.Model;
using System.ServiceModel;

namespace MesService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class MesModelReader : IMESModelReader
	{
		private MesModel mesModel;

		public MesModelReader(MesModel mesModel)
		{
			this.mesModel = mesModel;
		}

		public UIModelObject GetBreakers()
		{
			UIModelObject uIModelObject = new UIModelObject();
			var breakers = mesModel.GetBreakers();

			foreach (var breaker in breakers)
			{
				uIModelObject.UIBreakers.Add(new UIBreaker(breaker.Id, breaker.Name, breaker.CurrentState, breaker.LastState));
			}

			return uIModelObject;
		}
	}
}

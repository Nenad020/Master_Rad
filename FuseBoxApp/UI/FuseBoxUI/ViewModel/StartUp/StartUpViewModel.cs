using FuseBoxUI.ViewModel.Base;
using FuseBoxUI.ViewModel.Elements;
using System.Collections.ObjectModel;
using FuseBoxUI.Helpers;
using System.Linq;
using static FuseBoxUI.DI.DI;
using System.Collections.Generic;
using Common.Model.UI;
using System;
using Common.Communication.Client.MES;

namespace FuseBoxUI.ViewModel.StartUp
{
	public class StartUpViewModel : BaseViewModel
    {
		#region Public properties

        public ObservableCollection<BreakerViewModel> Breakers { get; set; }

        #endregion

        #region Constructor

        public StartUpViewModel()
        {
            Breakers = new ObservableCollection<BreakerViewModel>();

            var mesBreakers = GetInitBreakers();
            if (mesBreakers != null)
			{
				foreach (var mesBreaker in mesBreakers.UIBreakers)
				{
                    var breaker = new BreakerViewModel()
                    {
                        BreakerName = mesBreaker.Name,
                        Id = mesBreaker.Id
					};

                    UpdateBreakerViewPosition(breaker, mesBreaker.CurrentState);
                    Breakers.Add(breaker);
                }
			}

            ViewModelApplication.BreakerUpdateEvent += BreakerUpdate;
        }

		#endregion

		#region Methods

		private void BreakerUpdate(List<UIBreaker> breakers)
		{
            foreach (var breaker in breakers)
            {
                var item = Breakers.FirstOrDefault(a => a.BreakerName == breaker.Name);
                if (item == null)
                {
                    throw new Exception();
                }
                else
                {
                    if (breaker.CurrentState)
                    {
                        UpdateBreakerViewPosition(item, true);
                    }
                    else
					{
                        UpdateBreakerViewPosition(item, false);
					}
                }
            }
        }

        private void UpdateBreakerViewPosition(BreakerViewModel breaker, bool state)
		{
            breaker.FillBackground = BreakerHelper.GetBrushColor(state);
            breaker.TogglePositions = BreakerHelper.GetTogglePositon(state);
            breaker.ToggleButton = state;		
        }

        private UIModelObject GetInitBreakers()
		{
            using (MESModelReaderClient client = new MESModelReaderClient())
            {
                client.Open();

                try
                {
                    return client.GetBreakers();
                }
                catch
                {
                }
            }

            return null;
        }

		#endregion
	}
}

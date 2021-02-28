using FuseBoxUI.ViewModel.Base;
using FuseBoxUI.ViewModel.Elements;
using System.Collections.ObjectModel;
using FuseBoxUI.Helpers;
using System.Linq;
using static FuseBoxUI.DI.DI;
using System.Collections.Generic;
using Common.Model.UI;
using System;

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

            for (int i = 1; i <= 18; i++)
            {
                Breakers.Add(new BreakerViewModel()
                {
                    BreakerName = $"Breaker_{i}",
                    FillBackground = BreakerViewHelper.GetBrushColor(false),
                    TogglePositions = BreakerViewHelper.GetTogglePositon(false),
                    ToggleButton = false
                });
            }

            ViewModelApplication.BreakerUpdateEvent += BreakerUpdate;
        }

        #endregion

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
            breaker.FillBackground = BreakerViewHelper.GetBrushColor(state);
            breaker.TogglePositions = BreakerViewHelper.GetTogglePositon(state);
            breaker.ToggleButton = state;		
        }
	}
}

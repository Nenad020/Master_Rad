using FuseBoxUI.Helpers;
using FuseBoxUI.ViewModel.Base;
using System.Windows.Input;

namespace FuseBoxUI.ViewModel.Elements
{
	public class BreakerViewModel : BaseViewModel
	{
		public string FillBackground { get; set; }

		public double[] TogglePositions { get; set; }

		public string BreakerName { get; set; }

		public bool ToggleButton = true;

		public ICommand ChangeValueCommand { get; set; }

		public BreakerViewModel()
		{
			ChangeValueCommand = new RelayCommand(ToggleChangedValue);
		}

		private void ToggleChangedValue()
		{
			if (!ToggleButton)
			{
				FillBackground = BreakerViewHelper.GetBrushColor(true);
				TogglePositions = BreakerViewHelper.GetTogglePositon(true);
				ToggleButton = true;

				//TODO: Obavestiti SCADU
			}
			else
			{
				FillBackground = BreakerViewHelper.GetBrushColor(false);
				TogglePositions = BreakerViewHelper.GetTogglePositon(false);
				ToggleButton = false;

				//TODO: Obavestiti SCADU
			}
		}
	}
}

using FuseBoxUI.ViewModel.Base;
using System.Windows.Input;

namespace FuseBoxUI.ViewModel.Elements
{
	public class BreakerViewModel : BaseViewModel
	{
		public string FillBackground { get; set; } = "ff4747";

		public double[] TogglePositions { get; set; } = new double[] { 0, 35, 0, 0 };

		public string BreakerName { get; set; } = "Videcemo jos sta cemo raditi";

		private string brushOff = "ff4747";

		private string brushOn = "00c541";

		private double[] toggleOff = new double[] { 0, 35, 0, 0 };

		private double[] toggleOn = new double[] { 0, 0, 0, 35 };

		private bool toggleButton = false;

		public ICommand ChangeValueCommand { get; set; }

		public BreakerViewModel()
		{
			ChangeValueCommand = new RelayCommand(ToggleChangedValue);
		}

		private void ToggleChangedValue()
		{
			if (!toggleButton)
			{
				FillBackground = brushOn;
				TogglePositions = toggleOn;

				//TODO: Obavestiti SCADU

				toggleButton = true;
			}
			else
			{
				FillBackground = brushOff;
				TogglePositions = toggleOff;

				//TODO: Obavestiti SCADU

				toggleButton = false;
			}
		}
	}
}

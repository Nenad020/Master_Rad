using Common.Communication.Client.MES;
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

		public bool ToggleButton { get; set; }

		public int Id { get; set; }

		public ICommand ChangeValueCommand { get; set; }

		public BreakerViewModel()
		{
			ChangeValueCommand = new RelayCommand(ToggleChangedValue);
		}

		private void ToggleChangedValue()
		{
			if (!ToggleButton)
			{
				FillBackground = BreakerHelper.GetBrushColor(true);
				TogglePositions = BreakerHelper.GetTogglePositon(true);
				ToggleButton = true;

				BreakerCommand(true);
			}
			else
			{
				FillBackground = BreakerHelper.GetBrushColor(false);
				TogglePositions = BreakerHelper.GetTogglePositon(false);
				ToggleButton = false;

				BreakerCommand(false);
			}
		}

		private void BreakerCommand(bool state)
		{
			using (MesCommandClient client = new MesCommandClient())
			{
				try
				{
					if (state)
					{
						client.Close(Id);
					}
					else
					{
						client.Open(Id);
					}
				}
				catch
				{
				}
			}
		}
	}
}

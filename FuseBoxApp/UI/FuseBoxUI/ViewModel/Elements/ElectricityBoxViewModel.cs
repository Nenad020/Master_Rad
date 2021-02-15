using FuseBoxUI.ViewModel.Base;
using System.Threading;

namespace FuseBoxUI.ViewModel.Elements
{
	public class ElectricityBoxViewModel : BaseViewModel
	{
		public double UsedWatts { get; set; } = 0; 

		public ElectricityBoxViewModel()
		{
			new Thread(() => Randomizing()).Start();
		}

		public void Randomizing()
		{
			while (true)
			{
				UsedWatts++;

				Thread.Sleep(1);
			}
		}
	}
}

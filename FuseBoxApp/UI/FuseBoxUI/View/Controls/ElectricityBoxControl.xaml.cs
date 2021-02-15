using FuseBoxUI.ViewModel.Elements;
using System.Windows.Controls;

namespace FuseBoxUI.View.Controls
{
	/// <summary>
	/// Interaction logic for ElectricityBoxControl.xaml
	/// </summary>
	public partial class ElectricityBoxControl : UserControl
	{
		public ElectricityBoxControl()
		{
			InitializeComponent();

			DataContext = new ElectricityBoxViewModel();
		}
	}
}

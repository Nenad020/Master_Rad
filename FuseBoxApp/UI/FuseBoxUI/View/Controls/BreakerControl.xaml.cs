using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FuseBoxUI.View.Controls
{
	/// <summary>
	/// Interaction logic for BreakerControl.xaml
	/// </summary>
	public partial class BreakerControl : UserControl
	{
		Thickness ThicknessOff = new Thickness(0, 35, 0, 0);
		Thickness ThicknessOn = new Thickness(0, 0, 0, 35);
		SolidColorBrush BrushOff = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4747"));
		SolidColorBrush BrushOn = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00c541"));
		private bool ToggleButton = false;

		public BreakerControl()
		{
			InitializeComponent();
		}

		private void ToggleChangedValue()
		{
			if(!ToggleButton)
			{
				Pozadina.Fill = BrushOn;
				Krug.Margin = ThicknessOn;

				ToggleButton = true;
			}
			else
			{
				Pozadina.Fill = BrushOff;
				Krug.Margin = ThicknessOff;

				ToggleButton = false;
			}
		}

		private void Pozadina_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ToggleChangedValue();
		}

		private void Krug_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ToggleChangedValue();
		}
	}
}

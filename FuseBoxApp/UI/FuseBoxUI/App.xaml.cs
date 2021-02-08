using Dna;
using FuseBoxUI.DI;
using System.Windows;

namespace FuseBoxUI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			// Let the base application do what it needs
			base.OnStartup(e);

			Framework.Construct<DefaultFrameworkConstruction>()
				.AddPagesViewModel()
				.Build();

			// Show the main window
			Current.MainWindow = new MainWindow();
			Current.MainWindow.Show();
		}
	}
}
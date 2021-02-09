﻿using FuseBoxUI.ViewModel;

namespace FuseBoxUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = new WindowViewModel(this);
		}
	}
}
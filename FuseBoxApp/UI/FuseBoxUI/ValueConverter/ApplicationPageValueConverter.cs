using FuseBoxUI.DataModels;
using FuseBoxUI.Pages;
using System;
using System.Diagnostics;
using System.Globalization;

namespace FuseBoxUI.ValueConverter
{
	/// <summary>
	/// Converts the <see cref="ApplicationPage"/> to an actual view/page
	/// </summary>
	public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Find the appropriate page
			switch ((ApplicationPage)value)
			{
				case ApplicationPage.StartUp:
					return new StartUpPage();

				default:
					Debugger.Break();
					return null;
			}
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
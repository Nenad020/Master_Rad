using FuseBoxUI.DataModels;
using FuseBoxUI.View.Pages;
using FuseBoxUI.ViewModel.StartUp;
using System.Diagnostics;

namespace FuseBoxUI.ValueConverter
{
	/// <summary>
	/// Converts the <see cref="ApplicationPage"/> to an actual view/page
	/// </summary>
	public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.StartUp:
                    return new StartUpPage(viewModel as StartUpViewModel);

                case ApplicationPage.Report:
                    return new ReportPage(viewModel as ReportViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is StartUpPage)
                return ApplicationPage.StartUp;

            if (page is ReportPage)
                return ApplicationPage.Report;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
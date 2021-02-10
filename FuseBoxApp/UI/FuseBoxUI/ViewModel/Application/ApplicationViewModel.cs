using FuseBoxUI.DataModels;
using FuseBoxUI.ViewModel.Base;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ViewModel.Application
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.StartUp;

        public bool SideMenuVisible { get; set; } = true;

        public ICommand MainPageCommand { get; set; }

        public ICommand ReportPageCommand { get; set; }

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        public ApplicationViewModel()
        {
            MainPageCommand = new RelayCommand(MainPage);
            ReportPageCommand = new RelayCommand(ReportPage);
        }

        public void MainPage()
		{
            if (CurrentPage != ApplicationPage.StartUp)
                ViewModelApplication.GoToPage(ApplicationPage.StartUp);
        }

        public void ReportPage()
		{
            if (CurrentPage != ApplicationPage.Report)
                ViewModelApplication.GoToPage(ApplicationPage.Report);
        }

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));
        }
    }
}

using FuseBoxUI.DataModels;
using FuseBoxUI.ServiceHosts.Hosts;
using FuseBoxUI.ViewModel.Base;
using System;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;
using FuseBoxUI.Events;

namespace FuseBoxUI.ViewModel.Application
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.StartUp;

        public bool SideMenuVisible { get; set; } = true;

        public ICommand MainPageCommand { get; set; }

        public ICommand ReportPageCommand { get; set; }

        private UIChangesServiceHost uIChangesServiceHost;

        public ApplicationViewModel()
        {
            MainPageCommand = new RelayCommand(MainPage);
            ReportPageCommand = new RelayCommand(ReportPage);

            uIChangesServiceHost = new UIChangesServiceHost();
            uIChangesServiceHost.Open();
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

        public void GoToPage(ApplicationPage page)
        {
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

using FuseBoxUI.DataModels;
using FuseBoxUI.ServiceHosts.Hosts;
using FuseBoxUI.ViewModel.Base;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;
using System.Collections.Generic;
using Common.Model.UI;

namespace FuseBoxUI.ViewModel.Application
{
	#region Delegates

	public delegate void AlarmDelegate(List<UIAlarm> alarms);

	public delegate void BreakerDelegate(List<UIBreaker> breakers);
	
	#endregion

	public class ApplicationViewModel : BaseViewModel
    {
		#region Public properties

		public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.StartUp;

		public bool SideMenuVisible { get; set; } = true; 
		
		#endregion

		#region Commands

		public ICommand MainPageCommand { get; set; }

		public ICommand ReportPageCommand { get; set; }

		#endregion

		#region Events

		public event AlarmDelegate AlarmUpdateEvent;

		public event BreakerDelegate BreakerUpdateEvent;

		#endregion

		#region Service Hosts

		private UIChangesServiceHost uIChangesServiceHost; 
		
		#endregion

		public ApplicationViewModel()
        {
            MainPageCommand = new RelayCommand(MainPage);
            ReportPageCommand = new RelayCommand(ReportPage);

            uIChangesServiceHost = new UIChangesServiceHost();
            uIChangesServiceHost.Open();
        }

		#region Methods

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

		#endregion

		#region OnEvents Methods

		public void OnAlarmUpdate(List<UIAlarm> alarms)
        {
            AlarmUpdateEvent?.Invoke(alarms);
		}

		public void OnBreakerUpdate(List<UIBreaker> breakers)
		{
			BreakerUpdateEvent?.Invoke(breakers);
		}

		#endregion
	}
}

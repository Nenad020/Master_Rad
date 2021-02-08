using FuseBoxUI.DataModels;
using FuseBoxUI.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ViewModel.Report
{
	public class ReportViewModel : BaseViewModel
    {
        #region Commands

        public ICommand ButtonCommand { get; set; }

        #endregion

        #region Constructor

        public ReportViewModel()
        {
            ButtonCommand = new RelayCommand(async () => await DoSomethingAsync());
        }

        #endregion

        public async Task DoSomethingAsync()
        {
            ViewModelApplication.GoToPage(ApplicationPage.StartUp);

            await Task.Delay(1000);
        }
    }
}

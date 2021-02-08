using FuseBoxUI.DataModels;
using FuseBoxUI.ViewModel.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ViewModel.StartUp
{
	public class StartUpViewModel : BaseViewModel
    {
        #region Commands

        public ICommand ButtonCommand { get; set; }

        #endregion

        #region Constructor

        public StartUpViewModel()
        {
            ButtonCommand = new RelayCommand(async () => await DoSomethingAsync());
        }

        #endregion

        public async Task DoSomethingAsync()
        {
            ViewModelApplication.GoToPage(ApplicationPage.Report);

            await Task.Delay(1000);
        }
    }
}

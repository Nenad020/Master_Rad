using FuseBoxUI.ViewModel.Base;
using FuseBoxUI.ViewModel.Dialog;
using System.Windows.Input;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ViewModel.Alarm
{
    public class AlarmItemViewModel : BaseViewModel
    {
        public string Message { get; set; }

        public string Date { get; set; }

        public ICommand ShowDetailsCommand { get; set; }

        public AlarmItemViewModel()
        {
            ShowDetailsCommand = new RelayCommand(ShowDetails);
        }

        public void ShowDetails()
        {
            UI.ShowMessage(new MessageBoxDialogViewModel()
            {
                Title = "ALERT!",
                Message = Message,
                Date = Date,
                OkText = "OK"
            });
        }
    }
}

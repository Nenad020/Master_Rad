using FuseBoxUI.ViewModel.Base;
using System.Collections.Generic;

namespace FuseBoxUI.ViewModel.Alarm
{
    public class AlarmListItemViewModel : BaseViewModel
    {
        public List<AlarmItemViewModel> Items { get; set; }
    }
}

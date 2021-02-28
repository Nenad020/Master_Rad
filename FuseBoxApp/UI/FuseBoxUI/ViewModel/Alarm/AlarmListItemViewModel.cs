using Common.Model.UI;
using FuseBoxUI.ViewModel.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static FuseBoxUI.DI.DI;

namespace FuseBoxUI.ViewModel.Alarm
{
	public class AlarmListItemViewModel : BaseViewModel
    {
        public ObservableCollection<AlarmItemViewModel> Items { get; set; }

        public AlarmListItemViewModel()
        {
            Items = new ObservableCollection<AlarmItemViewModel>();

            ViewModelApplication.AlarmUpdateEvent += AlarmUpdate;
        }

		private void AlarmUpdate(List<UIAlarm> alarms)
        {
   			foreach (var alarm in alarms)
			{
                var item = Items.FirstOrDefault(a => a.BreakerID == alarm.BreakerId);
                if (item == null)
				{
                    Items.Add(new AlarmItemViewModel
                    {
                        BreakerID = alarm.BreakerId,
                        Date = alarm.Timestamp.ToString(),
                        Message = alarm.Message,
                        ImagePath = GetImagePath(alarm.State)
                    });
				}
                else
				{
                    item.Date = alarm.Timestamp.ToString();
                    item.Message = alarm.Message;
                    item.ImagePath = GetImagePath(alarm.State);
                }
            }
        }

        private string GetImagePath(bool state)
		{
            if (state)
			{
                return "/Images/Alarm/GreenAlert.png";
            }
            else
			{
                return "/Images/Alarm/alert.png";
            }
		}
    }
}

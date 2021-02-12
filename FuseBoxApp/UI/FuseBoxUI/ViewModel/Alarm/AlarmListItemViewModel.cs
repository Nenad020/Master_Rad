using FuseBoxUI.ViewModel.Base;
using System;
using System.Collections.Generic;

namespace FuseBoxUI.ViewModel.Alarm
{
    public class AlarmListItemViewModel : BaseViewModel
    {
        public List<AlarmItemViewModel> Items { get; set; }

        public AlarmListItemViewModel()
        {
            Items = new List<AlarmItemViewModel>();

            for (int i = 1; i <= 20; i++)
            {
                Items.Add(new AlarmItemViewModel
                {
                    Message = $"Breaker with ID: {i} has been turned off!",
                    Date = DateTime.Now.ToString()
                });
            }
        }
    }
}

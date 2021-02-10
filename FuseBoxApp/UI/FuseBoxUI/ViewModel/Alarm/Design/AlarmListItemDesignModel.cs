using System;
using System.Collections.Generic;

namespace FuseBoxUI.ViewModel.Alarm.Design
{
    public class AlarmListItemDesignModel : AlarmListItemViewModel
    {
        #region Singleton

        public static AlarmListItemDesignModel Instance => new AlarmListItemDesignModel();

        #endregion

        #region Constructor

        public AlarmListItemDesignModel()
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

        #endregion
    }
}

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
            Items = new List<AlarmItemViewModel>
            {
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!",
                    Date = DateTime.Now.ToString()
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
                new AlarmItemViewModel
                {
                    Message = "Breaker with ID: 5 has been turned off!"
                },
            };
        }

        #endregion
    }
}

namespace FuseBoxUI.ViewModel.Alarm.Design
{
	public class AlarmItemDesignModel : AlarmItemViewModel
    {
        #region Singleton

        public static AlarmItemDesignModel Instance => new AlarmItemDesignModel();

        #endregion

        #region Constructor

        public AlarmItemDesignModel()
        {
            Message = "Breaker with ID: 5 has been turned off!";
        }

        #endregion
    }
}

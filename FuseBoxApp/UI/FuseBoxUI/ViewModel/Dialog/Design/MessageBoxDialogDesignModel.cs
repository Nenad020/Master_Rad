﻿namespace FuseBoxUI.ViewModel.Dialog.Design
{
	/// <summary>
	/// Details for a message box dialog
	/// </summary>
	public class MessageBoxDialogDesignModel : MessageBoxDialogViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static MessageBoxDialogDesignModel Instance => new MessageBoxDialogDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MessageBoxDialogDesignModel()
        {
            OkText = "OK";
            Message = "Design time messages are fun :)";
            Date = "10-Feb-21 8:05PM";
        }

        #endregion
    }
}

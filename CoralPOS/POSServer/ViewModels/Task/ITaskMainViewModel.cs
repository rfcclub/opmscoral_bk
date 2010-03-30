
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSServer.ViewModels.Task
{
    public interface ITaskMainViewModel : IScreenNode
    {
        #region Fields
			#endregion
		
		#region Methods
		        
        void Login();
        
		        
        void LogOut();
        
		        
        void ChangePassword();
        
			#endregion
    }
}
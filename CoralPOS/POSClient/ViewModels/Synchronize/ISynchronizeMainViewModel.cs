
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSClient.ViewModels.Synchronize
{
    public interface ISynchronizeMainViewModel : IScreenNode
    {
        #region Fields
			#endregion
		
		#region Methods
		        
        void FromMainStock();
        
		        
        void ToMainStock();
        
			#endregion
    }
}
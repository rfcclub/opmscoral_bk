
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSClient.ViewModels.Stock.StockOut
{
    public interface IDepartmentStockOutWaitingConfirmViewModel : IScreenNode
    {
        #region Fields
			#endregion
		
		#region Methods
		        
        void Help();
        
		        
        void Unconfirm();
        
		        
        void Stop();
        
		        
        void Confirm();
        
		        
        void Edit();
        
		        
        void Search();
        
			#endregion
    }
}

			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSClient.ViewModels.Menu.Stock
{
    public interface IDepartmentStockInMenuViewModel : IScreenNode
    {
        #region Fields
			#endregion
		
		#region Methods
		        
        void StockInConfirm();
        
		        
        void StockInList();
        
		        
        void StockInBackList();
        
		        
        void ReStockIn();
        
			#endregion
    }
}
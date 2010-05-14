
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSServer.ViewModels.Menu.Stock
{
    public interface IInventoryMenuViewModel : IScreenNode
    {
        #region Fields
			#endregion
		
		#region Methods
		        
        void InventoryCollector();
        
		        
        void AfterInventoryCollectorProcessing();
        
		        
        void StockSearch();
        
		        
        void StockCategorySearch();
        
			#endregion
    }
}
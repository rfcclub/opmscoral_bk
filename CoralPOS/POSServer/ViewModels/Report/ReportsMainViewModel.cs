			 

			 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AppFrame.Base;
using Caliburn.Core;
using Caliburn.Core.IoC;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;



namespace POSServer.ViewModels.Report
{
    [PerRequest(typeof(IReportsMainViewModel))]
    public class ReportsMainViewModel : PosViewModel,IReportsMainViewModel  
    {

        private IShellViewModel _startViewModel;
        public ReportsMainViewModel(IShellViewModel startViewModel)
        {
            _startViewModel = startViewModel; 
        }
		
		#region Fields
				#endregion
		
		#region List use to fetch object for view
				#endregion
		
		#region List which just using in Data Grid
				#endregion
		
		#region Methods
		        
        public void StockIn()
        {
            
        }
		        
        public void StockInList()
        {
            
        }
		        
        public void StockInBackList()
        {
            
        }
		        
        public void StockInByExcel()
        {
            
        }
		        
        public void button1()
        {
            
        }
				#endregion
		
        
        
    }
}
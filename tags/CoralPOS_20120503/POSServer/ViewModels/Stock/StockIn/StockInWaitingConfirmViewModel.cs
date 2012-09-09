			 

			 

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



namespace POSServer.ViewModels.Stock.StockIn
{
    [PerRequest(typeof(IStockInWaitingConfirmViewModel))]
    public class StockInWaitingConfirmViewModel : PosViewModel,IStockInWaitingConfirmViewModel  
    {

        private IShellViewModel _startViewModel;
        public StockInWaitingConfirmViewModel(IShellViewModel startViewModel)
        {
            _startViewModel = startViewModel; 
        }
		
		#region Fields
				#endregion
		
		#region List use to fetch object for view
		        
        private IList _category;
        public IList Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                NotifyOfPropertyChange(() => Category);
            }
        }
				#endregion
		
		#region List of boolean object
				#endregion
		
		#region List of date object
		        
        private IList _fromDate;
        public IList FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                NotifyOfPropertyChange(() => FromDate);
            }
        }
		        
        private IList _toDate;
        public IList ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                NotifyOfPropertyChange(() => ToDate);
            }
        }
				#endregion
		
		#region List which just using in Data Grid
		        
        private IList _stockInList;
        public IList StockInList
        {
            get
            {
                return _stockInList;
            }
            set
            {
                _stockInList = value;
                NotifyOfPropertyChange(() => StockInList);
            }
        }
		        
        private IList _stockInDetailList;
        public IList StockInDetailList
        {
            get
            {
                return _stockInDetailList;
            }
            set
            {
                _stockInDetailList = value;
                NotifyOfPropertyChange(() => StockInDetailList);
            }
        }
				#endregion
		
		#region Methods
		        
        public void Help()
        {
            
        }
		        
        public void Unconfirm()
        {
            
        }
		        
        public void Stop()
        {
            
        }
		        
        public void Confirm()
        {
            
        }
		        
        public void Edit()
        {
            
        }
		        
        public void Search()
        {
            
        }
				#endregion
		
        
        
    }
}
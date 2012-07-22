			 

			 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AppFrame.Base;
using AppFrame.Utils;
using Caliburn.Core;
using Caliburn.Core.IoC;

using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Filters;
using Caliburn.PresentationFramework.Screens;
using CoralPOS.Models;
using POSServer.BusinessLogic.Common;
using POSServer.BusinessLogic.Implement;


namespace POSServer.ViewModels.Stock.StockOut
{
    
    public class StockOutConfirmViewModel : PosViewModel,IStockOutConfirmViewModel  
    {

        private IShellViewModel _startViewModel;
        public StockOutConfirmViewModel(IShellViewModel startViewModel,bool isViewOnly)
        {
            _startViewModel = startViewModel;
            IsViewOnly = isViewOnly;
        }
		
		#region Fields


        public bool IsViewOnly { get; set; }
        private string _createDate;
        public string CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
                NotifyOfPropertyChange(() => CreateDate);
            }
        }
		        
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private Department _department;
        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                NotifyOfPropertyChange(() => Department);
            }
        }


        public IMainPriceLogic MainPriceLogic { get; set; }
				#endregion
		
		#region List use to fetch object for view
				#endregion
		
		#region List which just using in Data Grid
		        
        private IList _stockOutDetails;
        public IList StockOutDetails
        {
            get
            {
                return _stockOutDetails;
            }
            set
            {
                _stockOutDetails = value;
                NotifyOfPropertyChange(() => StockOutDetails);
            }
        }
				#endregion
		
		#region Methods
		        
        public void Help()
        {
            
        }
		        
        public void Back()
        {
            Flow.Back();
        }
		
        public bool CanSaveConfirm
        {
            get
            {
                return !IsViewOnly;
            }
        }

        [Preview("CanSaveConfirm")]
        public void SaveConfirm()
        {
            GoToNextNode();
        }
		        
        public void Stop()
        {
            Flow.End();
        }
        public override void Initialize()
        {
            base.Initialize();
            CoralPOS.Models.StockOut stockOut = Flow.Session.Get(FlowConstants.SAVE_STOCK_OUT) as CoralPOS.Models.StockOut;
            Description = stockOut.Description;
            stockOut.TotalQuantity = 0;
            foreach (StockOutDetail outDetail in stockOut.StockOutDetails)
            {
                outDetail.StockOut = stockOut;
                stockOut.TotalQuantity += outDetail.Quantity;
            }
            StockOutDetails = ObjectConverter.ConvertFrom(stockOut.StockOutDetails);
            Department = stockOut.Department;
            CreateDate = stockOut.CreateDate.ToString();
            
        }
				#endregion
		
        
        
    }
}
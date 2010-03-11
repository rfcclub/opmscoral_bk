
			 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.Core.Metadata;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSServer.ViewModels.Stock.StockOut
{
    public interface IStockOutViewModel : IScreenNode
    {
        #region Fields
		                
        public string CreateDate
        {
            get;
            set;            
        }
		                
        public string ProductMaster
        {
            get;
            set;            
        }
		                
        public string Description
        {
            get;
            set;            
        }
			#endregion
		
		#region Methods
		        
        public void Help();
        
		        
        public void Recreate();
        
		        
        public void Save();
        
		        
        public void Stop();
        
		        
        public void CreateByBlock();
        
		        
        public void CreateByFile();
        
		        
        public void FixQuantityByAvailable();
        
			#endregion
    }
}
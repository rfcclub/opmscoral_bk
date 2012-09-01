using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;

using AppFrame.CustomAttributes;
using AppFrame.CustomAttributes;
using CoralPOS.Models;
using POSServer.BusinessLogic.Implement;

namespace POSServer.ViewModels.ProductMaster
{
    public interface IExProductSizeViewModel : IScreenNode
    {
        #region Fields
        string Description
        {
            get;
            set;            
        }
		                
        string SizeName
        {
            get;
            set;            
        }
		                
        string SizeId
        {
            get;
            set;            
        }
        ExProductSize SelectedProductSize { get; set; }
        IExProductSizeLogic ProductSizeLogic { get; set; }
			#endregion
		
		#region Methods
		        
        void Help();
        
		        
        void Delete();
        
		        
        void Edit();
        
		        
        void Stop();
        
		        
        void Create();
        
			#endregion
    }
}
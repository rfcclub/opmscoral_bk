
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;
using CoralPOS.Models;

namespace POSServer.ViewModels.Synchronize
{
    public interface ISyncToDepartmentViewModel : IScreenNode
    {
        #region Fields
        IList Departments { get; set; }
        Department SelectedDepartment { get; set; }
        bool DepartmentInfo { get; set; }
        bool ProductMasterInfo { get; set; }
        bool PriceInfo { get; set; }
        IList ResultInfoList { get; set; }
			#endregion
		
		#region Methods
		        
        void SyncToDepartment();
        
		        
        void Quit();
        
			#endregion
    }
}
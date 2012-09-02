
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace POSServer.ViewModels.Management
{
    public interface IEmployeesViewModel : IScreenNode
    {
        #region Fields
		                
        string Address
        {
            get;
            set;            
        }
		                
        string EmployeeName
        {
            get;
            set;            
        }
		                
        string EmployeeId
        {
            get;
            set;            
        }
		                
        string CardId
        {
            get;
            set;            
        }
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
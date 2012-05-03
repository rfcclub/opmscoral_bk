
			 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using AppFrame.CustomAttributes;
using AppFrame.CustomAttributes;

namespace POSServer.ViewModels.Stock.StockOut
{
	public interface IStockOutWaitingConfirmViewModel : IScreenNode
	{
		#region Fields
			#endregion
		
		#region Methods
				
		void Help();
		
				
		void Unconfirm();
		
				
		void Stop();
		
				
		void Confirm();
		
				
		void Edit();
		
				
		void Recreate();
		
			#endregion
	}
}
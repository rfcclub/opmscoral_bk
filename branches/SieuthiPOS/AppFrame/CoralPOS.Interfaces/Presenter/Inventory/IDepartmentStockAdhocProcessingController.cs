using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.View.Inventory;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.Inventory
{
    public interface IDepartmentStockAdhocProcessingController
    {
        IDepartmentStockAdhocProcessingView DepartmentStockAdhocProcessingView { get; set; }
        IProductLogic ProductLogic { get; set; }
        IProductMasterLogic ProductMasterLogic { get; set; }
        IDepartmentStockTempLogic DepartmentStockTempLogic { get; set; }
        IStockOutLogic StockOutLogic { get; set; }

    }
}

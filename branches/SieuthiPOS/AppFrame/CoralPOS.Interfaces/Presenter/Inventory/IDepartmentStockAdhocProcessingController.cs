using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.Inventory;

namespace CoralPOS.Interfaces.Presenter.Inventory
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
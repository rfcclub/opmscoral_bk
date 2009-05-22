using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.View.Inventory;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.Inventory
{
    public interface IDepartmentStockFixingController
    {
        IDepartmentStockFixingView DepartmentStockFixingView { get; set; }
        IProductLogic ProductLogic { get; set; }
        IProductMasterLogic ProductMasterLogic { get; set; }
        IDepartmentStockLogic DepartmentStockLogic { get; set; }
        IStockOutLogic StockOutLogic { get; set; }
        IStockInLogic StockInLogic { get; set; }
        IStockOutDetailLogic StockOutDetailLogic { get; set; }
        IStockInDetailLogic StockInDetailLogic { get; set; }
    }
}

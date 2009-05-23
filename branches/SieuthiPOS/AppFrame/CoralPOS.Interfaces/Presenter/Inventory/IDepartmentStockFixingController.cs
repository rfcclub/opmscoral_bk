using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.Inventory;

namespace CoralPOS.Interfaces.Presenter.Inventory
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
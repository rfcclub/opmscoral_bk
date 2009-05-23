﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.MainStock
{
    public interface IInventoryCheckingController
    {
        #region View
        IInventoryCheckingView InventoryCheckingView { get; set; }
        #endregion
        #region Logic
        IStockLogic StockLogic { get; set; }
        IProductLogic ProductLogic { get; set; }
        IProductMasterLogic ProductMasterLogic { get; set; }
        IStockInLogic StockInLogic { get; set; }
        IStockInDetailLogic StockInDetailLogic { get; set; }
        IStockOutLogic StockOutLogic { get; set; }
        IStockOutDetailLogic StockOutDetailLogic { get; set; }
        #endregion
    }
}
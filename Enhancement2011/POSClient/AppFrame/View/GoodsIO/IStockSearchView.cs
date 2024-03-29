﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Presenter.GoodsIO;
using AppFrame.Common;

namespace AppFrame.View.GoodsIO
{
    public interface IStockSearchView : IView<StockCreateEventArgs>
    {
        #region main controller and event
        IStockSearchController StockSearchController { set; }
        event EventHandler<StockSearchEventArgs> InitStockSearchEvent;
        event EventHandler<StockSearchEventArgs> SearchStockEvent;
        event EventHandler<StockSearchEventArgs> RemainSearchStockEvent;
        event EventHandler<StockSearchEventArgs> BarcodeSearchStockEvent;
        #endregion
    }
}

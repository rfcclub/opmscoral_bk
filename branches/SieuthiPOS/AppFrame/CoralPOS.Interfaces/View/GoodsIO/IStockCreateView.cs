﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IStockCreateView : IView<StockCreateEventArgs>
    {
        #region main controller and event
        IStockCreateController StockCreateController { set; }
        event EventHandler<StockCreateEventArgs> CreateStockEvent;
        event EventHandler<StockCreateEventArgs> SearchStockInDetailEvent;
        #endregion
    }
}
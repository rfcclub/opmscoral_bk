﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Presenter;

namespace AppFrame.View.GoodsIO
{
    public interface IStockInConfirmView
    {
        IStockInConfirmController StockOutConfirmController { get; set; }

        event EventHandler<StockInConfirmEventArgs> ConfirmStockInEvent;
        event EventHandler<StockInConfirmEventArgs> DenyStockInEvent;
        event EventHandler<StockInConfirmEventArgs> LoadStockInsEvent;
    }
}

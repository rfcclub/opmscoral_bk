using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IStockOutConfirmView
    {
        IStockOutConfirmController StockOutConfirmController { get; set; }

        event EventHandler<StockOutConfirmEventArgs> ConfirmStockOutEvent;
        event EventHandler<StockOutConfirmEventArgs> DenyStockOutEvent;
        event EventHandler<StockOutConfirmEventArgs> LoadStockOutsEvent;
    }
}
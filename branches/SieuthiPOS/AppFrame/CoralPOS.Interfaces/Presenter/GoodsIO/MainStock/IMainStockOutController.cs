using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.MainStock
{
    public interface IMainStockOutController : IBaseController<MainStockOutEventArgs>
    {
        IMainStockOutView MainStockOutView { get; set; }
    }
}
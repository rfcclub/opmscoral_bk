using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.MainStock
{
    public interface IMainStockInController : IBaseController<MainStockInEventArgs>
    {
        IMainStockInView MainStockInView { get; set; }

        IProductColorLogic ProductColorLogic { get; set; }
        IProductSizeLogic ProductSizeLogic { get; set; }
    }
}
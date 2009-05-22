using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.View.GoodsIO;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.GoodsIO
{
    public interface IStockCreateController : IBaseController<StockCreateEventArgs>
    {
        #region View use in IStockCreateController
        IStockCreateView StockCreateView { get; set; }
        #endregion

        #region Logic use in IStockCreateController
        IStockLogic StockLogic { get; set; }
        #endregion
    }
}

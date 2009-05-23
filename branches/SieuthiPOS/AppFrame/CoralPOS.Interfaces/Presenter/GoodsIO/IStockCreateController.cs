using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO
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
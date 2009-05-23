using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO
{
    public interface IStockSearchController : IBaseController<StockSearchEventArgs>
    {
        #region View use in IStockSearchController
        IStockSearchView StockSearchView { get; set; }
        #endregion

        #region Logic use in IStockSearchController
        #endregion
    }
}
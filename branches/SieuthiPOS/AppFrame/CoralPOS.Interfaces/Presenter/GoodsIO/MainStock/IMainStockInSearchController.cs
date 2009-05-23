using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.MainStock
{
    public interface IMainStockInSearchController : IBaseController<MainStockInSearchEventArgs>
    {
        #region View use in IDepartmentStockInSearchController
        IMainStockInSearchView StockInSearchView { get; set; }
        #endregion

        #region Logic use in IDepartmentStockInSearchController
        IStockInLogic StockInLogic { get; set; }
        #endregion
    }
}
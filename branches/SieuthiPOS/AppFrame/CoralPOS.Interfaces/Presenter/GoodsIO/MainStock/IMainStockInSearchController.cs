using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.View.GoodsIO;
using AppFrame.View.GoodsIO.DepartmentGoodsIO;
using AppFrame.View.GoodsIO.MainStock;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.GoodsIO.MainStock
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
using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockSearchController : IBaseController<DepartmentStockSearchEventArgs>
    {
        #region View use in IDepartmentStockSearchController
        IDepartmentStockSearchView DepartmentStockSearchView { get; set; }
        #endregion

        #region Logic use in IDepartmentStockSearchController
        #endregion
    }
}
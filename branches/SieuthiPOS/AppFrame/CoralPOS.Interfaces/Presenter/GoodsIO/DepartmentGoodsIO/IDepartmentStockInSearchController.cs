using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInSearchController : IBaseController<DepartmentStockInSearchEventArgs>
    {
        #region View use in IDepartmentStockInSearchController
        IDepartmentStockInSearchView DepartmentStockInSearchView { get; set; }
        #endregion

        #region Logic use in IDepartmentStockInSearchController
        IDepartmentStockInLogic DepartmentStockInLogic { get; set; }
        #endregion
    }
}
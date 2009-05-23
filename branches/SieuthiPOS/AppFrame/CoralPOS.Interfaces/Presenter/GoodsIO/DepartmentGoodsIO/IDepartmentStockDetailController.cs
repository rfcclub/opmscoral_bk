using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockDetailController : IBaseController<DepartmentStockDetailEventArgs>
    {
        #region View use in IDepartmentStockDetailController
        IDepartmentStockDetailView DepartmentStockDetailView { get; set; }
        #endregion

        #region Logic use in IDepartmentStockSearchController
        #endregion
    }
}
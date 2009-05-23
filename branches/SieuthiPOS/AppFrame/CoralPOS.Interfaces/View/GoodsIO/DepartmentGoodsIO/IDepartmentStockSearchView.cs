using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockSearchView : IView<DepartmentStockSearchEventArgs>
    {
        #region main controller and event
        IDepartmentStockSearchController DepartmentStockSearchController { set; }
        event EventHandler<DepartmentStockSearchEventArgs> InitDepartmentStockSearchEvent;
        event EventHandler<DepartmentStockSearchEventArgs> SearchDepartmentStockEvent;
        #endregion
    }
}
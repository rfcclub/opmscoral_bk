using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInSearchView : IView<DepartmentStockInSearchEventArgs>
    {
        #region main controller and event
        IDepartmentStockInSearchController DepartmentStockInSearchController { set; }
        event EventHandler<DepartmentStockInSearchEventArgs> SearchDepartmentStockInEvent;
        #endregion
    }
}
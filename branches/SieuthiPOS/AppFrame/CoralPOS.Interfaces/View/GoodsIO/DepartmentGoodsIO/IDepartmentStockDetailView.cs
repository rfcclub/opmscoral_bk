using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockDetailView : IView<DepartmentStockDetailEventArgs>
    {
        #region main controller and event
        IDepartmentStockDetailController DepartmentStockDetailController { set; }
        event EventHandler<DepartmentStockDetailEventArgs> SaveDepartmentPriceEvent;
        event EventHandler<DepartmentStockDetailEventArgs> SearchDepartmentStockInDetailEvent;
        #endregion
    }
}
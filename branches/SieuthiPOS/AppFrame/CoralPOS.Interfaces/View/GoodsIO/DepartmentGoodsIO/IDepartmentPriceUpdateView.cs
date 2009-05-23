using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentPriceUpdateView : IView<DepartmentPriceUpdateEventArgs>
    {
        #region main controller and event
        IDepartmentPriceUpdateController DepartmentPriceUpdateController { set; }
        event EventHandler<DepartmentPriceUpdateEventArgs> SaveDepartmentPriceEvent;
        event EventHandler<DepartmentPriceUpdateEventArgs> SearchDepartmentPriceEvent;
        event EventHandler<DepartmentPriceUpdateEventArgs> InitDepartmentPriceEvent;
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInView : IView<DepartmentStockInEventArgs>
    {
        #region main controller and event
        IDepartmentStockInController DepartmentStockInController { set; }
        IProductMasterSearchOrCreateController ProductMasterSearchOrCreateController { set; }
        event EventHandler<DepartmentStockInEventArgs> InitDepartmentStockInEvent;
        event EventHandler<ProductMasterSearchOrCreateEventArgs> OpenProductMasterSearchEvent;
        event EventHandler<DepartmentStockInEventArgs> SaveDepartmentStockInEvent;
        event EventHandler<DepartmentStockInEventArgs> FindProductMasterEvent;
        event EventHandler<DepartmentStockInEventArgs> SyncDepartmentStockInEvent;
        event EventHandler<DepartmentStockInEventArgs> FindByBarcodeEvent;
        event EventHandler<DepartmentStockInEventArgs> SaveReDepartmentStockInEvent;
        
        #endregion
        
    }
}
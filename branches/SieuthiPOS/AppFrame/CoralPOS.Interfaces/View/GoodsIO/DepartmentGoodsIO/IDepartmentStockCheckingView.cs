using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockCheckingView
    {
        #region Event
        event EventHandler<DepartmentStockCheckingEventArgs> FillProductMasterToComboEvent;
        event EventHandler<DepartmentStockCheckingEventArgs> LoadGoodsByProductIdEvent;
        event EventHandler<DepartmentStockCheckingEventArgs> SaveInventoryCheckingEvent;
        #endregion

        #region Controller
        IDepartmentStockCheckingController DepartmentStockCheckingController { get; set; }
        #endregion

        event EventHandler<DepartmentStockCheckingEventArgs> SaveTempInventoryCheckingEvent;
        event EventHandler<DepartmentStockCheckingEventArgs> LoadTempInventoryCheckingEvent;
    }
}
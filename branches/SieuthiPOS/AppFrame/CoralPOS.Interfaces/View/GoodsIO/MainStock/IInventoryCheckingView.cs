using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.View.GoodsIO.MainStock
{
    public interface IInventoryCheckingView
    {
        #region Event
        event EventHandler<InventoryCheckingEventArgs> FillProductMasterToComboEvent;
        event EventHandler<InventoryCheckingEventArgs> LoadGoodsByProductIdEvent;
        event EventHandler<InventoryCheckingEventArgs> SaveInventoryCheckingEvent;
        event EventHandler<InventoryCheckingEventArgs> LoadDepartmentGoodsByProductIdEvent;
        event EventHandler<InventoryCheckingEventArgs> SaveDepartmentInventoryCheckingEvent;
        #endregion

        #region Controller
        IInventoryCheckingController InventoryCheckingController { get; set; }
        #endregion 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.Inventory;

namespace CoralPOS.Interfaces.View.Inventory
{
    public interface IDepartmentStockFixingView
    {
        IDepartmentStockFixingController DepartmentStockFixingController { get; set; }
        event EventHandler<DepartmentStockFixingEventArgs> LoadAdhocStocksEvent;
        event EventHandler<DepartmentStockFixingEventArgs> ProcessAdhocStocksEvent;
    }
}
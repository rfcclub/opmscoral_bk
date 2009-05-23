using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.Inventory;

namespace CoralPOS.Interfaces.View.Inventory
{
    public interface IDepartmentStockAdhocProcessingView
    {
        IDepartmentStockAdhocProcessingController DepartmentStockAdhocProcessingController { get; set; }
        event EventHandler<DepartmentStockAdhocProcessingEventArgs> LoadAdhocStocksEvent;
        event EventHandler<DepartmentStockAdhocProcessingEventArgs> ProcessAdhocStocksEvent;
    }
}
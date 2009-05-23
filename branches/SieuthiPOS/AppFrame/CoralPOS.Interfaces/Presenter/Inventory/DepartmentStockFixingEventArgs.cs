using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;

namespace CoralPOS.Interfaces.Presenter.Inventory
{
    public class DepartmentStockFixingEventArgs : BaseEventArgs
    {
        public IList DeptStockAdhocList { get; set; }
        public IList DeptStockProcessedList { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public class DepartmentStockCheckingEventArgs : BaseEventArgs
    {
        public string InputBarcode { get; set; }
        public System.Collections.IList ReturnStockViewList { get; set; }

        public DepartmentStock ScannedStock { get; set; }
        public DepartmentStockView ScannedStockView { get; set; }
        public System.Collections.IList SaveStockList { get; set; }
        public System.Collections.IList SaveStockViewList { get; set; }
        public bool UnconfirmTempBarcode { get; set; }
    }
}
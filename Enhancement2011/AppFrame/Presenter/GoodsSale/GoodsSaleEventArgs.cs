﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.Model;

namespace AppFrame.Presenter.GoodsSale
{
    public class GoodsSaleEventArgs : BaseEventArgs
    {
        public bool NotAvailableInStock { get; set;}

        public PurchaseOrder RefPurchaseOrder { get; set;}
        public PurchaseOrderDetail SelectedPurchaseOrderDetail { get; set; }

        public int SelectedIndex { get; set; }

        public bool IsFillComboBox { get; set; }
        public string ComboBoxDisplayMember { get; set; }
    }
        
}

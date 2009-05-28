using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter.GoodsSale
{
    public class GoodsSaleEventArgs : BaseEventArgs
    {
        public bool HasVAT { get; set;}
        public bool NotAvailableInStock { get; set;}

        public PurchaseOrder RefPurchaseOrder { get; set;}
        public PurchaseOrderDetail SelectedPurchaseOrderDetail { get; set; }

        public int SelectedIndex { get; set; }

        public bool IsFillComboBox { get; set; }
        public string ComboBoxDisplayMember { get; set; }
    }
}
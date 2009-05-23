using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AppFrame;
using AppFrame.Common;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter.GoodsSale
{
    public class GoodsSaleListEventArgs  : BaseEventArgs
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public PurchaseOrderCollection PurchaseOrders { get; set; }
        public ObjectCriteria PurchaseOrderSearchCriteria { get; set; }
        public PurchaseOrder SelectedOrder { get; set; }
        public IList PurchaseOrderViewList { get; set; }
    }
}
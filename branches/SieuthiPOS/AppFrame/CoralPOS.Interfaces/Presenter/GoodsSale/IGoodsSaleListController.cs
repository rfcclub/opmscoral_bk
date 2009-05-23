using System;
using System.Collections.Generic;
using System.Text;
using AppFrame;
using AppFrame.ModelCriteria;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsSale;

namespace CoralPOS.Interfaces.Presenter.GoodsSale
{
    public interface IGoodsSaleListController
    {
        #region View use
        IGoodsSaleListView GoodsSaleListView { get; set; }
        IDayGoodsSaleListView DayGoodsSaleListView { get; set; }
        IMonthGoodsSaleListView MonthGoodsSaleListView { get; set; }
        #endregion

        #region Logic
        IPurchaseOrderLogic PurchaseOrderLogic { get; set; }
        IPurchaseOrderDetailLogic PurchaseOrderDetailLogic { get; set; }
        IProductMasterLogic ProductMasterLogic { get; set; }
        #endregion

        #region Model
        PurchaseOrderCollection PurchaseOrders { get; set; }
        ObjectCriteria PurchaseOrderCriteria { get; set; }
        #endregion

        #region Event

        event EventHandler<GoodsSaleListEventArgs> CompletedGoodsSaleListSearchEvent;
        #endregion
    }
}
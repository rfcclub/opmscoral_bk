using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.View.GoodsIO.MainStock
{
    public interface IMainStockInSearchView : IView<MainStockInSearchEventArgs>
    {
        #region main controller and event
        IMainStockInSearchController MainStockInSearchController { set; }
        event EventHandler<MainStockInSearchEventArgs> SearchStockInEvent;
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.View.GoodsIO.MainStock
{
    public interface IBaseStockOutView
    {
        #region controller
        IBaseStockOutController BaseStockOutController { get; set; }
        event EventHandler<BaseStockOutEventArgs> FillGoodsToCombo;
        event EventHandler<BaseStockOutEventArgs> FillDeptGoodsToCombo;
        
        event EventHandler<BaseStockOutEventArgs> SaveTempStockOut;
        event EventHandler<BaseStockOutEventArgs> LoadGoodsByNameEvent;

        event EventHandler<BaseStockOutEventArgs> SaveDeptTempStockOut;
        event EventHandler<BaseStockOutEventArgs> LoadDeptGoodsByNameEvent;

        bool DepartmentReturnForm { get; set; }
        #endregion
    }
}
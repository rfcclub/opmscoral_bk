using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentPriceUpdateController : IBaseController<DepartmentPriceUpdateEventArgs>
    {
        #region View use in IDepartmentStockDetailController
        IDepartmentPriceUpdateView DepartmentPriceUpdateView { get; set; }
        #endregion

        #region Logic use in IDepartmentPriceUpdateController
        #endregion
    }
}
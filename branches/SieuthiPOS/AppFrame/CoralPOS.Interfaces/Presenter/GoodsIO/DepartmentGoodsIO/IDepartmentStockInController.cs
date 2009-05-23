using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInController : IBaseController<DepartmentStockInEventArgs>
    {
        #region View use in IDepartmentStockInController
        IDepartmentStockInView DepartmentStockInView { get; set; }
        #endregion

        #region Logic use in IDepartmentStockInController

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.View.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInExtraController : IDepartmentStockInController
    {
        IDepartmentStockInExtraView DepartmentStockInExtraView { get; set; }

        IProductColorLogic ProductColorLogic { get; set; }
        IProductSizeLogic ProductSizeLogic { get; set; }
    }
}

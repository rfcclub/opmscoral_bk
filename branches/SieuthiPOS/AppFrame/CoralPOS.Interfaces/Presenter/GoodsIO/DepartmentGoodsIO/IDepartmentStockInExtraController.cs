using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockInExtraController : IDepartmentStockInController
    {
        IDepartmentStockInExtraView DepartmentStockInExtraView { get; set; }

        IProductColorLogic ProductColorLogic { get; set; }
        IProductSizeLogic ProductSizeLogic { get; set; }
    }
}
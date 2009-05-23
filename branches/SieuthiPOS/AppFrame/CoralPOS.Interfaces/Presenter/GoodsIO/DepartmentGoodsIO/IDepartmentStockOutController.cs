using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO
{
    public interface IDepartmentStockOutController : IBaseController<DepartmentStockOutEventArgs>
    {
        IDepartmentStockOutView DepartmentStockOutView { get; set; }
    }
}
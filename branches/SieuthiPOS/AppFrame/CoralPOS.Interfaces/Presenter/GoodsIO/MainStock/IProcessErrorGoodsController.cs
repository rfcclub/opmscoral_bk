using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.Presenter.GoodsIO.MainStock
{
    public interface IProcessErrorGoodsController
    {
        IProcessErrorGoodsView ProcessErrorGoodsView { get; set; }

        IStockOutLogic StockOutLogic { get; set; }
        IStockOutDetailLogic StockOutDetailLogic { get; set; }
        IStockLogic StockLogic { get; set; }

        IDepartmentStockOutLogic DepartmentStockOutLogic { get; set; }
        IDepartmentStockOutDetailLogic DepartmentStockOutDetailLogic { get; set; }
        IDepartmentStockLogic DepartmentStockLogic { get; set; }
    }
}
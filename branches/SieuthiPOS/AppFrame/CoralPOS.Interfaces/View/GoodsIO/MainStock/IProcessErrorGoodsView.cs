using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;

namespace CoralPOS.Interfaces.View.GoodsIO.MainStock
{
    public interface IProcessErrorGoodsView
    {
        IProcessErrorGoodsController ProcessErrorGoodsController { get; set; }

        event EventHandler<ProcessErrorGoodsEventArgs> LoadAllStockDefects;
        event EventHandler<ProcessErrorGoodsEventArgs> SaveStockDefects;


        event EventHandler<ProcessErrorGoodsEventArgs> LoadAllDepartmentStockDefects;
        event EventHandler<ProcessErrorGoodsEventArgs> SaveDepartmentStockDefects;
        
    }
}
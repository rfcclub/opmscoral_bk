using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.GoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IProductMasterEditView
    {
        IProductMasterController ProductMasterController { get;set;}
        event EventHandler<ProductMasterEventArgs> LoadProductMasters;
        event EventHandler<ProductMasterEventArgs> SaveProductMasters;
    }
}
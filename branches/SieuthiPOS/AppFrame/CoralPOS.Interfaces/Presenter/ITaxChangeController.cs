using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO;

namespace CoralPOS.Interfaces.Presenter
{
    public interface ITaxChangeController
    {
        ITaxChangeView TaxChangeView { get; set; }
        ITaxLogic TaxLogic { get; set; }
        IProductMasterLogic ProductMasterLogic { get; set; }
        IProductLogic ProductLogic { get; set; }
    }
}

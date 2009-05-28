using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface ITaxChangeView
    {
        ITaxChangeController TaxChangeController { get; set;}
        event EventHandler<TaxChangeEventArgs> LoadProductMastersEvent;
        event EventHandler<TaxChangeEventArgs> SaveTaxEvent;
    }
}

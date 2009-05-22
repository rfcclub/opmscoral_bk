using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class StockCollection : BaseCollection<Stock>
    {
        public StockCollection(BindingSource source) : base(source)
        {
        }

        public StockCollection()
        {
        }

        public StockCollection(IList<Stock> list) : base(list)
        {
        }
    }
}
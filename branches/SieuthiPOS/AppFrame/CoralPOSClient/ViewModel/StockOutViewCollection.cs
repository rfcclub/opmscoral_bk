using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Collection;

namespace CoralPOS.ViewModel
{
    public class StockOutViewCollection : BaseCollection<StockOutView>
    {
        public StockOutViewCollection(BindingSource source) : base(source)
        {
        }

        public StockOutViewCollection()
        {
        }

        public StockOutViewCollection(IList<StockOutView> list) : base(list)
        {
        }
    }
}

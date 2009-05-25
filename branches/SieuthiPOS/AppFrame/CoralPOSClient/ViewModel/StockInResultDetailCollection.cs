using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Collection;

namespace CoralPOS.ViewModel
{
    public class StockInResultDetailCollection : BaseCollection<StockInResultDetail>
    {
        public StockInResultDetailCollection(BindingSource source) : base(source)
        {
        }

        public StockInResultDetailCollection()
        {
        }

        public StockInResultDetailCollection(IList<StockInResultDetail> list) : base(list)
        {
        }
    }
}

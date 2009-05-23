using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class StockInDetailCollection : BaseCollection<StockInDetail>
    {
        public StockInDetailCollection(BindingSource source) : base(source)
        {
        }

        public StockInDetailCollection()
        {
        }

        public StockInDetailCollection(IList<StockInDetail> list)
            : base(list)
        {
        }
    }
}
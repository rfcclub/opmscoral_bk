using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.ViewModel
{
    public class StockDefectCollection :BaseCollection<StockHistory>
    {
        public StockDefectCollection(BindingSource source) : base(source)
        {
        }

        public StockDefectCollection()
        {
        }

        public StockDefectCollection(IList<StockHistory> list)
            : base(list)
        {

        }
    }
}

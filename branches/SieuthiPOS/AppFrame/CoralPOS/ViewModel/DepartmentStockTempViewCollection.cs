using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Collection;

namespace CoralPOS.ViewModel
{
    public class DepartmentStockTempViewCollection : BaseCollection<DepartmentStockTempView>
    {
        public DepartmentStockTempViewCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockTempViewCollection()
        {
        }

        public DepartmentStockTempViewCollection(IList<DepartmentStockTempView> list) : base(list)
        {
        }
    }
}

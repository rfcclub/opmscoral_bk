using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockTempCollection : BaseCollection<DepartmentStockTemp>
    {
        public DepartmentStockTempCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockTempCollection()
        {
        }

        public DepartmentStockTempCollection(IList<DepartmentStockTemp> list) : base(list)
        {
        }
    }
}
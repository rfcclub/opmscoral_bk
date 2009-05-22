using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockOutCollection : BaseCollection<DepartmentStockOut>
    {
        public DepartmentStockOutCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockOutCollection()
        {
        }

        public DepartmentStockOutCollection(IList<DepartmentStockOut> list) : base(list)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockOutViewDetailCollection : BaseCollection<DepartmentStockOutViewDetailCollection>
    {
        public DepartmentStockOutViewDetailCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockOutViewDetailCollection()
        {
        }

        public DepartmentStockOutViewDetailCollection(IList<DepartmentStockOutViewDetailCollection> list) : base(list)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Collection;

namespace CoralPOS.ViewModel
{
    public class DepartmentStockOutViewCollection : BaseCollection<DepartmentStockOutView>
    {
        public DepartmentStockOutViewCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockOutViewCollection()
        {
        }

        public DepartmentStockOutViewCollection(IList<DepartmentStockOutView> list) : base(list)
        {
        }
    }
}

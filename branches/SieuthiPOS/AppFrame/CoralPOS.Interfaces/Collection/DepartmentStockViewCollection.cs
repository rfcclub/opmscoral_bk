using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockViewCollection : BaseCollection<DepartmentStockView>
    {
        public DepartmentStockViewCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockViewCollection()
        {
        }

        public DepartmentStockViewCollection(IList<DepartmentStockView> list) : base(list)
        {
        }
    }
}
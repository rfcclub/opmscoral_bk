using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockCollection : BaseCollection<DepartmentStock>
    {
        public DepartmentStockCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockCollection()
        {
        }

        public DepartmentStockCollection(IList<DepartmentStock> list) : base(list)
        {
        }
    }
}
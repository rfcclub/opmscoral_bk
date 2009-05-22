using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using AppFrame.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class DepartmentStockOutDetailCollection : BaseCollection<DepartmentStockOutDetail>
    {
        public DepartmentStockOutDetailCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockOutDetailCollection()
        {
        }

        public DepartmentStockOutDetailCollection(IList<DepartmentStockOutDetail> list) : base(list)
        {
        }
    }
}
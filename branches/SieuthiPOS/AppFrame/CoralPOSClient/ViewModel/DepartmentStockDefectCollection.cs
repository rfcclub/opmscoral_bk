using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.ViewModel
{
    public class DepartmentStockDefectCollection : BaseCollection<DepartmentStockHistory>
    {
        public DepartmentStockDefectCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockDefectCollection()
        {
        }

        public DepartmentStockDefectCollection(IList<DepartmentStockHistory> list) : base(list)
        {
        }
    }
}

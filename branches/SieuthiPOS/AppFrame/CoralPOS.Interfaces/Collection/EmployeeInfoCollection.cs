using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using AppFrame.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class EmployeeInfoCollection : BaseCollection<EmployeeInfo>
    {
        public EmployeeInfoCollection(BindingSource source) : base(source)
        {
        }

        public EmployeeInfoCollection()
        {
        }

        public EmployeeInfoCollection(IList<EmployeeInfo> list) : base(list)
        {
        }
    }
}
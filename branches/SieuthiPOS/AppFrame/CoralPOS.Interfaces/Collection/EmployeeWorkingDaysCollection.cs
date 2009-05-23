using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class EmployeeWorkingDaysCollection : BaseCollection<EmployeeWorkingDay>
    {
        public EmployeeWorkingDaysCollection(BindingSource source) : base(source)
        {
        }

        public EmployeeWorkingDaysCollection()
        {
        }

        public EmployeeWorkingDaysCollection(IList<EmployeeWorkingDay> list) : base(list)
        {
        }
    }
}
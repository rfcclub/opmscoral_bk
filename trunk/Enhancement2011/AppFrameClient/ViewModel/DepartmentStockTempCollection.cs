﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using AppFrame.Model;

namespace AppFrameClient.ViewModel
{
    public class DepartmentStockTempCollection : AFBaseCollection<DepartmentStockTemp>
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

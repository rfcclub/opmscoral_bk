﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Collection;

namespace CoralPOS.ViewModel
{
    public class DepartmentStockOutDetailViewCollection : BaseCollection<DepartmentStockOutDetailView>
    {
        public DepartmentStockOutDetailViewCollection(BindingSource source) : base(source)
        {
        }

        public DepartmentStockOutDetailViewCollection()
        {
        }

        public DepartmentStockOutDetailViewCollection(IList<DepartmentStockOutDetailView> list) : base(list)
        {
        }
    }
}

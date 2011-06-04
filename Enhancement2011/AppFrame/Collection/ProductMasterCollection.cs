﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Model;

namespace AppFrame.Collection
{
    public class ProductMasterCollection : AfBaseCollection<ProductMaster>
    {
        public ProductMasterCollection(BindingSource source) : base(source)
        {
        }

        public ProductMasterCollection()
        {
        }

        public ProductMasterCollection(IList<ProductMaster> list) : base(list)
        {
        }
    }
}

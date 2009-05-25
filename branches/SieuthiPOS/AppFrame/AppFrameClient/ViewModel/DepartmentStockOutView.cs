﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.ViewModel
{
    public class DepartmentStockOutView
    {
        public virtual string DepartmentName { get; set; }
        public virtual long TotalQuantity { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DepartmentStockOut DepartmentStockOut { get; set; }
        public virtual Department Department { get; set; }
    }
}

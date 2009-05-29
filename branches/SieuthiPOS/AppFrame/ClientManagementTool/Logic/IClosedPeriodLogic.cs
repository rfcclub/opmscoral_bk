﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientManagementTool.View.Management;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Logic;

namespace ClientManagementTool.Logic
{
    public interface IClosedPeriodLogic
    {
        // view 
        IClosedPeriodView ClosedPeriodView { get; set; }

        // logic
        IPurchaseOrderLogic PurchaseOrderLogic
        {
            get;
            set;
        }

        IPurchaseOrderDetailLogic PurchaseOrderDetailLogic
        {
            get;
            set;
        }

        IProductMasterLogic ProductMasterLogic
        {
            get;
            set;
        }
        
        IReturnPoLogic ReturnPoLogic { get; set; }
        IDepartmentPriceLogic DepartmentPriceLogic { get; set; }
    }
}

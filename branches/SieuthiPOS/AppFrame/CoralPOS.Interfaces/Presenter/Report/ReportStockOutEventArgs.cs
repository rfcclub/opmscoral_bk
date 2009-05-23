using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.View.Reports;

namespace CoralPOS.Interfaces.Presenter.Report
{
    public class ReportStockOutEventArgs : BaseEventArgs
    {
        public ReportDateStockOutParam ReportDateStockOutParam { get; set; }

        public IList ResultStockOutList { get; set; }
        public IList ProductMastersInList { get; set; }
        public IList DepartmentsList { get; set; }
        public Department SelectDepartment { get; set; }

        public IList ConfirmDepartmentStockOutList { get; set; }
        public IList DenyDepartmentStockOutList { get; set; }

        
    }
}
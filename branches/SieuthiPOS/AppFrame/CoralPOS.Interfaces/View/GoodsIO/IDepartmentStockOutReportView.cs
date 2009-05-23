using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.Report;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IDepartmentStockOutReportView
    {
        IReportStockOutController ReportStockOutController { get; set; }

        event EventHandler<ReportStockOutEventArgs> ConfirmStockOutEvent;
        event EventHandler<ReportStockOutEventArgs> DenyStockOutEvent;
        event EventHandler<ReportStockOutEventArgs> LoadDepartmentStockOutsEvent;
    }
}
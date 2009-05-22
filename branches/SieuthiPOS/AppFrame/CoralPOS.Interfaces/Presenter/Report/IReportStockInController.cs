using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.View.Reports;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.Report
{
    public interface IReportStockInController
    {
        IReportStockInParamView ReportStockInParamView { get; set;}
        IReportStockInView ReportStockInView { get; set; }
        IDepartmentStockInReportView DepartmentStockInReportView { get; set; }

    #region logic
        IStockLogic StockLogic { get; set; }
        IStockInLogic StockInLogic { get; set; }
        IStockInDetailLogic StockInDetailLogic { get; set; }
        IDepartmentStockInLogic DepartmentStockInLogic { get; set; }
        IDepartmentStockInDetailLogic DepartmentStockInDetailLogic { get; set; }
        IDepartmentLogic DepartmentLogic { get; set; }
    #endregion
    }
}

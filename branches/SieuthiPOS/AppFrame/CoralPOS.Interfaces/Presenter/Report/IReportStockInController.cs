using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.Reports;

namespace CoralPOS.Interfaces.Presenter.Report
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
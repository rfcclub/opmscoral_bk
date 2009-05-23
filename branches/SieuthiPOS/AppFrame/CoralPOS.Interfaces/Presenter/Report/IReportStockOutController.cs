using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO;
using CoralPOS.Interfaces.View.Reports;

namespace CoralPOS.Interfaces.Presenter.Report
{
    public interface IReportStockOutController
    {
        /*IDepartmentStockInReportView ReportDepartmentStockOutView { get; set; }*/
        IStockOutReportView MainStockOutReportView { get; set; }
        IDepartmentStockOutReportView DepartmentStockOutReportView { get; set; }
        #region logic
        IDepartmentStockInLogic DepartmentStockInLogic { get; set; }
        IDepartmentStockInDetailLogic DepartmentStockInDetailLogic { get; set; }
        IDepartmentLogic DepartmentLogic { get; set; }

        IDepartmentStockOutLogic DepartmentStockOutLogic { get; set; }
        IDepartmentStockOutDetailLogic DepartmentStockOutDetailLogic { get; set; }

        IStockOutLogic StockOutLogic { get; set; }
        IStockOutDetailLogic StockOutDetailLogic { get; set; }
        IStockLogic StockLogic { get; set; }
        

        #endregion
    }
}
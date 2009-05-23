using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter.Report;

namespace CoralPOS.Interfaces.View.Reports
{
    public interface IReportStockInParamView
    {
        #region controller 
        IReportStockInController ReportStockInController {get;set;}
        #endregion

        event EventHandler<ReportStockInEventArgs> CreateReportStockIn;
    }
}
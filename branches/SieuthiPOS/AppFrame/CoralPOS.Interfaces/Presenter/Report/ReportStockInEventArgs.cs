using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View.Reports;

namespace CoralPOS.Interfaces.Presenter.Report
{
    public class ReportStockInEventArgs : BaseEventArgs
    {
        public ReportStockInParam ReportStockInParam { get; set; }

        public IList ResultStockInList { get; set; }
        public IList ProductMastersInList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.Report;
using AppFrame.Utility;
using CoralPOS.Interfaces.View.Reports;

namespace CoralPOSServer.View.Reports
{
    public partial class frmStockinRpt : BaseForm,IReportStockInParamView
    {
        public frmStockinRpt()
        {
            InitializeComponent();
        }

       

        #region IReportStockInParamView Members

        private CoralPOS.Interfaces.Presenter.Report.IReportStockInController reportStockInController;
        public CoralPOS.Interfaces.Presenter.Report.IReportStockInController ReportStockInController
        {
            get
            {
                return reportStockInController;
            }
            set
            {
                reportStockInController = value;
                reportStockInController.ReportStockInParamView = this;

            }
        }

        #endregion

        #region IReportStockInParamView Members


        public event EventHandler<CoralPOS.Interfaces.Presenter.Report.ReportStockInEventArgs> CreateReportStockIn;

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ReportStockInParam reportStockInParam = new ReportStockInParam();
            reportStockInParam.FromDate = dtpReportTimeFrom.Value;
            reportStockInParam.ToDate = dtpReportTimeTo.Value;
            ReportStockInEventArgs eventArgs = new ReportStockInEventArgs();
            eventArgs.ReportStockInParam = reportStockInParam;
            EventUtility.fireEvent(CreateReportStockIn,this,eventArgs);
            Close();
        }
    }
}
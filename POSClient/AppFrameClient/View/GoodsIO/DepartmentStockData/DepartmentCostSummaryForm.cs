﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Presenter.GoodsIO.DepartmentGoodsIO;
using AppFrame.Utility;
using AppFrame.View.GoodsIO.DepartmentGoodsIO;
using AppFrameClient.MasterDBTableAdapters;
using InfoBox;

namespace AppFrameClient.View.GoodsIO.DepartmentStockData
{
    public partial class DepartmentCostSummaryForm : BaseForm,IDepartmentCostSummaryView
    {

        private DateTime reqFromDate, reqToDate;
        private int deptId = 0;
        private Stopwatch watcher = null;

        public DepartmentCostSummaryForm()
        {
            InitializeComponent();
        }

        private void DepartmentCostSummaryForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'masterDB.Department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.masterDB.Department);
            if (CurrentDepartment.Get().DepartmentId != 0)
            {
                cboDepartment.SelectedValue = CurrentDepartment.Get().DepartmentId.ToString();
                cboDepartment.Enabled = false;
            }
            //this.rptDepartmentCost.RefreshReport();
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            deptId = Int32.Parse(cboDepartment.SelectedValue.ToString());
            // just take 3 days before
            reqFromDate = DateUtility.ZeroTime(dtoFrom.Value);
            if (!cboDepartment.Enabled)
            {
                if (DateUtility.ZeroTime(DateTime.Now).Subtract(reqFromDate).Days > 1)
                {
                    reqFromDate = DateUtility.ZeroTime(DateTime.Now).Subtract(new TimeSpan(1, 0, 0, 0, 0));
                }
            }
            reqToDate = DateUtility.MaxTime(dtoTo.Value);

            Enabled = false;
            backgroundWorker.RunWorkerAsync();
            StartShowProcessing();    
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Enabled = true;
            StopShowProcessing();
            rptDepartmentCost.RefreshReport();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DepartmentCostReportTableAdapter adapter = new DepartmentCostReportTableAdapter();
            adapter.ClearBeforeFill = true;
            adapter.Fill(masterDB.DepartmentCostReport, deptId, reqFromDate, reqToDate);
        }

        private IDepartmentCostController departmentCostController; 
        public IDepartmentCostController DepartmentCostController 
        { 
            get
            {
                return departmentCostController;
            }
            set
            {
                departmentCostController.DepartmentCostSummaryView = this;
            }
        }
        public event EventHandler<DepartmentCostEventArgs> CommitDepartmentCostEvent;

        private void btnCommit_Click(object sender, EventArgs e)
        {
            DepartmentCostEventArgs eventArgs = new DepartmentCostEventArgs();
            eventArgs.FromDate = DateTime.Now;
            eventArgs.ToDate = DateTime.Now;

            EventUtility.fireEvent(CommitDepartmentCostEvent,this,eventArgs);
            if(eventArgs.EventResult!=null)
            {
                InfoBox.InformationBox.Show("Chốt sổ thành công !", new AutoCloseParameters(1));
            }
            else
            {
                InfoBox.InformationBox.Show("Chốt sổ thất bại !", new AutoCloseParameters(1));
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using POSReports.posDataSetTableAdapters;

namespace POSReports
{
    public partial class PurchaseDetailsReportViewer : FormBase
    {
        public PurchaseDetailsReportViewer()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PurchaseDetailsReportViewer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'posDataSet.purchaseDetails' table. You can move, or remove it, as needed.
            try
            {
                //this.PurchaseDetailsTableAdapter.Fill(this.posDataSet.purchaseDetails);

                //this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }


        public static DateTime ZeroTime(DateTime value)
        {
            return new DateTime(
                value.Year,
                value.Month,
                value.Day,
                0,
                0,
                0,
                0);
        }

        public static DateTime MaxTime(DateTime value)
        {
            return new DateTime(
                value.Year,
                value.Month,
                value.Day,
                23,
                59,
                59,
                999);
        }

        POSReports.posDataSet aSyncDS = new posDataSet();
        private DateTime reqFromDate, reqToDate;
        private int deptId = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            reqFromDate = ZeroTime(fromDate.Value);
            reqToDate = MaxTime(toDate.Value);
            deptId = Int32.Parse(departments.SelectedValue.ToString());
            Enabled = false;
            backgroundWorker.RunWorkerAsync();
            StartShowProcessing();

            /*try
            {
                object dept = departments.SelectedValue;
                this.purchaseOrderDetailReportTableAdapter1.Fill(this.posDataSet.PurchaseOrderDetailReport, Int32.Parse(dept.ToString()),ZeroTime(fromDate.Value), MaxTime(toDate.Value));

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }*/
        }
        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Enabled = true;
            StopShowProcessing();
            reportViewer1.RefreshReport();
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            aSyncDS.EnforceConstraints = false;
            aSyncDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            POSReports.posDataSetTableAdapters.PurchaseOrderDetailReportTableAdapter adapter = new PurchaseOrderDetailReportTableAdapter();
            adapter.ClearBeforeFill = true;
            //adapter.Fill(aSyncDS.StockStatistic, reqFromDate, reqToDate);
            adapter.Fill(posDataSet.PurchaseOrderDetailReport, deptId, reqFromDate, reqToDate);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'posDataSet.department' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'posDataSet.receiptInGeneral' table. You can move, or remove it, as needed.
            try
            {
                this.departmentTableAdapter1.Fill(this.posDataSet.department);
                if (CurrentDepartment.Get().DepartmentId != 0)
                {
                    departments.SelectedValue = CurrentDepartment.Get().DepartmentId.ToString();
                    departments.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

    }
}

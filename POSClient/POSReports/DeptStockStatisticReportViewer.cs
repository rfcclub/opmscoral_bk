﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using Microsoft.Reporting.WinForms;

namespace POSReports
{
    public partial class DeptStockStatisticReportViewer : Form
    {
        public DeptStockStatisticReportViewer()
        {
            InitializeComponent();
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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ReportParameter[] parameters = new ReportParameter[1];
                
                if(!string.IsNullOrEmpty(txtFilter.Text))
                {
                    parameters[0] = new ReportParameter("StrFilter", txtFilter.Text.Trim());
                    this.deptStockStatisticBindingSource.Filter = "product_name like '%" + txtFilter.Text + "%'";
                }
                else
                {
                    parameters[0] = new ReportParameter("StrFilter", "");
                    this.deptStockStatisticBindingSource.Filter = "";
                }

                object deptId = departmentId.SelectedValue;
                this.DeptStockStatisticTableAdapter.Fill(posDataSet.deptStockStatistic, Int32.Parse(deptId.ToString()),ZeroTime(toDate.Value),MaxTime(toDate.Value) );
                
                this.reportViewer1.LocalReport.SetParameters(parameters);
                this.reportViewer1.RefreshReport();

            } catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra trong khi tạo báo cáo, vui lòng liên hệ người quản trị!");
            }
        }

        private void DeptStockStatisticReportViewer_Load(object sender, EventArgs e)
        {
            departmentTableAdapter.Fill(this.posDataSet.department);
            
            
            // TODO: This line of code loads data into the 'posDataSet.deptStockStatistic' table. You can move, or remove it, as needed.
//            this.DeptStockStatisticTableAdapter.Fill(this.posDataSet.deptStockStatistic);
            BindingSource tempBindingSource = new BindingSource(this.deptStockStatisticBindingSource, "");
            this.reportViewer1.LocalReport.DataSources[0].Value = tempBindingSource;

            if(CurrentDepartment.Get().DepartmentId!=0)
            {
                departmentId.SelectedValue = CurrentDepartment.Get().DepartmentId.ToString();
                departmentId.Enabled = false;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using AppFrame.Model;
using AppFrame.Presenter.Report;
using AppFrame.Utility;
using AppFrame.View.Reports;
using AppFrameClient.ViewModel;

namespace AppFrameClient.View.Reports
{
    public partial class DepartmentGoodsExportReportForm : AppFrame.Common.BaseForm,IDepartmentGoodsExportReportView
    {

        private DepartmentStockInResultDetailCollection pSODetResultList = null;
        private DepartmentStockInDetailCollection pSODetList = null;
        private IList resultList = null;

        public DepartmentGoodsExportReportForm()
        {
            InitializeComponent();
        }

        private void DepartmentGoodsExportReportForm_Load(object sender, EventArgs e)
        {
            pSODetResultList = new DepartmentStockInResultDetailCollection(bdsStockOutResultPM);
            bdsStockOutResultPM.DataSource = pSODetResultList;

            pSODetList = new DepartmentStockInDetailCollection(bdsStockOutResultDetail);
            bdsStockOutResultDetail.DataSource = pSODetList;


            dgvStockProductsDetail.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss";
            ReportStockOutEventArgs stockOutEventArgs = new ReportStockOutEventArgs();
            EventUtility.fireEvent(LoadAllDeparmentEvent,this,stockOutEventArgs);
            BindingSource bdsDepartments = new BindingSource();
            bdsDepartments.DataSource = stockOutEventArgs.DepartmentsList;
            cboDepartments.DataSource = bdsDepartments;
            cboDepartments.DisplayMember = "DepartmentName";
        }

        #region IDepartmentGoodsExportReportView Members

        AppFrame.Presenter.Report.IReportStockOutController reportStockOutController;
        public AppFrame.Presenter.Report.IReportStockOutController ReportStockOutController
        {
            get
            {
                return reportStockOutController;
            }
            set
            {
                reportStockOutController = value;
                reportStockOutController.ReportStockOutView = this;
            }
        }

        public ReportStockOutParam ReportStockOutParam
        {
            get;set;
        }

        public event EventHandler<AppFrame.Presenter.Report.ReportStockOutEventArgs> LoadStockOutByRangeEvent;

        #endregion

        #region IDepartmentGoodsExportReportView Members


        public event EventHandler<AppFrame.Presenter.Report.ReportStockOutEventArgs> LoadAllDeparmentEvent;

        #endregion

        private void ok_Click(object sender, EventArgs e)
        {
            pSODetResultList.Clear();
            ReportStockOutEventArgs eventArgs = new ReportStockOutEventArgs();
            ReportStockOutParam stockInParam = new ReportStockOutParam();
            stockInParam.FromDate = DateUtility.ZeroTime(dtpFrom.Value);
            stockInParam.ToDate = DateUtility.MaxTime(dtpTo.Value);
            eventArgs.ReportStockOutParam = stockInParam;
            if (cboDepartments != null) 
                eventArgs.SelectDepartment = (Department)cboDepartments.SelectedItem;

            EventUtility.fireEvent(LoadStockOutByRangeEvent, this, eventArgs);

            // get result
            resultList = eventArgs.ResultStockOutList;

            System.Collections.IList stockDetailByPMList = eventArgs.ProductMastersInList;

            if (stockDetailByPMList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hàng nào", "Kết quả");
            }

            foreach (var o in stockDetailByPMList)
            {
                System.Collections.IList stockDetailByPM = (IList)o;
                ProductMasterGlobal productMasterGlobal = new ProductMasterGlobal();
                productMasterGlobal.ProductName = ((ProductMaster)stockDetailByPM[0]).ProductName;
                productMasterGlobal.ProductMaster = (ProductMaster)stockDetailByPM[0];
                DepartmentStockInResultDetail stockInResultDetail = new DepartmentStockInResultDetail();
                stockInResultDetail.ProductMasterGlobal = productMasterGlobal;
                stockInResultDetail.StockInQuantities = (long)stockDetailByPM[1];
                stockInResultDetail.StockInTotalAmounts = (long)stockDetailByPM[2];
                pSODetResultList.Add(stockInResultDetail);
            }
            bdsStockOutResultPM.EndEdit();
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            for (int i = 0; i < dgvStockProducts.Rows.Count; i++)
            {
                dgvStockProducts[0, i].Value = i + 1;
            }


            for (int i = 0; i < dgvStockProductsDetail.Rows.Count; i++)
            {
                dgvStockProductsDetail[0, i].Value = i + 1;
            }

        }

        private void bdsStockInResultPM_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvStockProducts_SelectionChanged(object sender, EventArgs e)
        {
            pSODetList.Clear();
            if (resultList == null)
            {
                return;
            }
            DataGridViewSelectedRowCollection selectedRows = dgvStockProducts.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DepartmentStockInResultDetail siRetDetail = pSODetResultList[selectedRows[0].Index];

                foreach (DepartmentStockIn stockIn in resultList)
                {
                    foreach (DepartmentStockInDetail stockInDetail in stockIn.DepartmentStockInDetails)
                    {
                        if (
                            stockInDetail.Product.ProductMaster.ProductName.Equals(
                                siRetDetail.ProductMasterGlobal.ProductName))
                        {
                            pSODetList.Add(stockInDetail);
                        }
                    }
                }
                bdsStockOutResultDetail.EndEdit();
                PopulateGrid();
            }
        }
    }
}

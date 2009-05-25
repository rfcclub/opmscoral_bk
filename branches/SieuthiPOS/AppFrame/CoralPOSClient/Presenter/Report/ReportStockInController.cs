using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Presenter.Report;

namespace CoralPOS.Presenter.Report
{
    public class ReportStockInController : IReportStockInController
    {
        #region IReportStockInController Members

        private CoralPOS.Interfaces.View.Reports.IDepartmentStockInReportView departmentStockInReportView;
        public CoralPOS.Interfaces.View.Reports.IDepartmentStockInReportView DepartmentStockInReportView
        {
            get
            {
                return departmentStockInReportView;
            }
            set
            {
                departmentStockInReportView = value;
                departmentStockInReportView.LoadAllDeparmentEvent += new EventHandler<ReportStockOutEventArgs>(reportStockOutView_LoadAllDeparmentEvent);
                departmentStockInReportView.LoadStockOutByRangeEvent += new EventHandler<ReportStockOutEventArgs>(reportStockOutView_LoadDepartmentStockOutByRangeEvent);
            }
        }

        void reportStockOutView_LoadDepartmentStockOutByRangeEvent(object sender, ReportStockOutEventArgs e)
        {
            ObjectCriteria criteria = new ObjectCriteria();
            criteria.AddBetweenCriteria("CreateDate", e.ReportDateStockOutParam.FromDate, e.ReportDateStockOutParam.ToDate);
            criteria.AddEqCriteria("DepartmentStockInPK.DepartmentId", e.SelectDepartment.DepartmentId);
            IList stockInList = DepartmentStockInLogic.FindAll(criteria);
            e.ResultStockOutList = stockInList;

            IList productMasterList = DepartmentStockInLogic.FindByProductMaster(e.SelectDepartment.DepartmentId, e.ReportDateStockOutParam.FromDate, e.ReportDateStockOutParam.ToDate);
            e.ProductMastersInList = productMasterList;
        }

        void reportStockOutView_LoadAllDeparmentEvent(object sender, ReportStockOutEventArgs e)
        {
            ObjectCriteria criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", (long)0);
            IList departmentList = DepartmentLogic.FindAll(criteria);
            e.DepartmentsList = departmentList;

        }

        private CoralPOS.Interfaces.View.Reports.IReportStockInParamView reportStockInParamView;

        public CoralPOS.Interfaces.View.Reports.IReportStockInParamView ReportStockInParamView
        {
            get
            {
                return reportStockInParamView;
            }
            set
            {
                reportStockInParamView = value;
                reportStockInParamView.CreateReportStockIn += new EventHandler<ReportStockInEventArgs>(reportStockInParamView_CreateReportStockIn);
            }
        }

        void reportStockInParamView_CreateReportStockIn(object sender, ReportStockInEventArgs e)
        {
            
        }

        private CoralPOS.Interfaces.View.Reports.IReportStockInView reportStockInView;
        public CoralPOS.Interfaces.View.Reports.IReportStockInView ReportStockInView
        {
            get
            {
                return reportStockInView;
            }
            set
            {
                reportStockInView = value;
                reportStockInView.LoadStockInByRangeEvent += new EventHandler<ReportStockInEventArgs>(reportStockInView_LoadStockInByRangeEvent);
            }
        }

        void reportStockInView_LoadStockInByRangeEvent(object sender, ReportStockInEventArgs e)
        {
            ObjectCriteria criteria = new ObjectCriteria();
            criteria.AddBetweenCriteria("CreateDate", e.ReportStockInParam.FromDate, e.ReportStockInParam.ToDate);
            IList stockInList = StockInLogic.FindAll(criteria);
            e.ResultStockInList = stockInList;

            IList productMasterList = StockInLogic.FindByProductMaster(e.ReportStockInParam.FromDate, e.ReportStockInParam.ToDate);
            e.ProductMastersInList = productMasterList;

        }

        public IStockLogic StockLogic
        {
            get;set;
            
        }

        public IStockInLogic StockInLogic
        {
            get;set;
            
        }

        public IStockInDetailLogic StockInDetailLogic
        {
            get;set;
        }
        public IDepartmentStockInLogic DepartmentStockInLogic
        {
            get;
            set;

        }

        public IDepartmentStockInDetailLogic DepartmentStockInDetailLogic
        {
            get;
            set;

        }

        public IDepartmentLogic DepartmentLogic
        {
            get;
            set;

        }
        #endregion
    }
}

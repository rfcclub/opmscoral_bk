using System;
using System.Collections.Generic;
using System.Text;
using AppFrame;
using AppFrame.Common;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOS.Presenter.GoodsIO.DepartmentStockData
{
    public class DepartmentStockInSearchController : IDepartmentStockInSearchController
    {
        private IDepartmentStockInSearchView _departmentStockInSearchView;
        public IDepartmentStockInSearchView DepartmentStockInSearchView
        {
            get
            {
                return _departmentStockInSearchView;
            }
            set
            {
                _departmentStockInSearchView = value;
                _departmentStockInSearchView.SearchDepartmentStockInEvent += new System.EventHandler<DepartmentStockInSearchEventArgs>(departmentStockInSearchView_SearchDepartmentStockInEvent);
            }
        }

        public void departmentStockInSearchView_SearchDepartmentStockInEvent(object sender, DepartmentStockInSearchEventArgs e)
        {
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DepartmentStockInPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            criteria.AddGreaterOrEqualsCriteria("StockInDate", e.StockInDateFrom);
            criteria.AddLesserOrEqualsCriteria("StockInDate", e.StockInDateTo);
            criteria.AddLikeCriteria("DepartmentStockInPK.StockInId", e.StockInId + "%");
            criteria.AddEqCriteria("DelFlg", (long)0);
            e.DepartmeneStockInList = DepartmentStockInLogic.FindAll(criteria);
        }

        public IDepartmentStockInLogic DepartmentStockInLogic { get; set; }

        #region Implementation of IBaseController<StockCreateEventArgs>

        public DepartmentStockInSearchEventArgs ResultEventArgs
        {
            get;
            set;
        }

        #endregion
    }
}

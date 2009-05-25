﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.View.GoodsIO.MainStock;

namespace CoralPOS.Presenter.GoodsIO.MainStock
{
    public class MainStockInSearchController : IMainStockInSearchController
    {
        private IMainStockInSearchView _stockInSearchView;
        public IMainStockInSearchView StockInSearchView
        {
            get
            {
                return _stockInSearchView;
            }
            set
            {
                _stockInSearchView = value;
                _stockInSearchView.SearchStockInEvent += new System.EventHandler<MainStockInSearchEventArgs>(departmentStockInSearchView_SearchMainStockInEvent);
            }
        }

        public void departmentStockInSearchView_SearchMainStockInEvent(object sender, MainStockInSearchEventArgs e)
        {
            var criteria = new ObjectCriteria();
            criteria.AddGreaterOrEqualsCriteria("StockInDate", e.StockInDateFrom);
            criteria.AddLesserOrEqualsCriteria("StockInDate", e.StockInDateTo);
            criteria.AddLikeCriteria("StockInId", e.StockInId + "%");
            criteria.AddEqCriteria("DelFlg", (long)0);
            e.StockInList = StockInLogic.FindAll(criteria);
        }

        public IStockInLogic StockInLogic { get; set; }

        #region Implementation of IBaseController<StockCreateEventArgs>

        public MainStockInSearchEventArgs ResultEventArgs
        {
            get;
            set;
        }

        #endregion
    }
}

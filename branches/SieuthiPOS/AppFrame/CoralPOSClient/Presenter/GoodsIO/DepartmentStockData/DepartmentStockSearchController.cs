using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AppFrame;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter.GoodsIO;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.Presenter.SalePoints;
using CoralPOS.Interfaces.Utility;
using CoralPOS.Interfaces.View.GoodsIO;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;
using NHibernate.Criterion;

namespace CoralPOS.Presenter.GoodsIO.DepartmentStockData
{
    public class DepartmentStockSearchController : IDepartmentStockSearchController
    {
        #region View use in IGoodsIOSearchController

        private IDepartmentStockSearchView _departmentStockSearchView;
        public IDepartmentStockSearchView DepartmentStockSearchView
        { 
            get
            {
                return _departmentStockSearchView;
            }
            set
            {
                _departmentStockSearchView = value;
                _departmentStockSearchView.InitDepartmentStockSearchEvent += new System.EventHandler<DepartmentStockSearchEventArgs>(stockSearchView_InitStockSearchEvent);
                _departmentStockSearchView.SearchDepartmentStockEvent += new System.EventHandler<DepartmentStockSearchEventArgs>(stockSearchView_SearchStockEvent);
            }
        }

        #endregion

        public void stockSearchView_SearchStockEvent(object sender, DepartmentStockSearchEventArgs e)
        {
            var objectCriteria = new ObjectCriteria(true);
            objectCriteria.AddEqCriteria("s.DelFlg", (long)0);
            objectCriteria.AddEqCriteria("sdetail.DelFlg", (long)0);
            objectCriteria.AddLikeCriteria("pm.ProductMasterId", e.ProductMasterId + "%");
            objectCriteria.AddLikeCriteria("pm.ProductName", e.ProductMasterName + "%");
            objectCriteria.AddEqCriteria("pm.ProductType", e.ProductType);
            objectCriteria.AddEqCriteria("pm.ProductSize", e.ProductSize);
            objectCriteria.AddEqCriteria("pm.ProductColor", e.ProductColor);
            objectCriteria.AddEqCriteria("pm.Manufacturer", e.Manufacturer);
            objectCriteria.AddEqCriteria("pm.Country", e.Country);
            objectCriteria.AddEqCriteria("s.DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);

            e.DepartmentStockList = DepartmentStockLogic.FindByQuery(objectCriteria);
        }

        public void stockSearchView_InitStockSearchEvent(object sender, DepartmentStockSearchEventArgs e)
        {
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", (long)0);
            e.ProductTypeList = ProductTypeLogic.FindAll(criteria);
            e.ProductTypeList.Insert(0, new ProductType());
            e.ProductSizeList = ProductSizeLogic.FindAll(criteria);
            e.ProductSizeList.Insert(0, new ProductSize());
            e.ProductColorList = ProductColorLogic.FindAll(criteria);
            e.ProductColorList.Insert(0, new ProductColor());
            e.ManufacturerList = ManufacturerLogic.FindAll(criteria);
            e.ManufacturerList.Insert(0, new Manufacturer());
            e.CountryList = CountryLogic.FindAll(criteria);
            e.CountryList.Insert(0, new Country());
        }

        #region Logic use in IStockCreateController
        public ICountryLogic CountryLogic { get; set; }
        public IProductColorLogic ProductColorLogic { get; set; }
        public IProductTypeLogic ProductTypeLogic { get; set; }
        public IProductSizeLogic ProductSizeLogic { get; set; }
        public IManufacturerLogic ManufacturerLogic { get; set; }
        public IDistributorLogic DistributorLogic { get; set; }
        public IPackagerLogic PackagerLogic { get; set; }
        public IDepartmentStockLogic DepartmentStockLogic { get; set; }
        #endregion

        #region Implementation of IBaseController<StockCreateEventArgs>

        public DepartmentStockSearchEventArgs ResultEventArgs
        {
            get; set;
        }

        #endregion
    }
}
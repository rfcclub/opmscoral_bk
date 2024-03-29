﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame;
using AppFrame.Common;
using AppFrame.Logic;
using AppFrame.Model;
using AppFrame.Presenter.GoodsIO.DepartmentGoodsIO;
using AppFrame.Presenter.GoodsIO.MainStock;
using AppFrame.View.GoodsIO.DepartmentGoodsIO;
using AppFrameClient.Utility;

namespace AppFrameClient.Presenter.GoodsIO.DepartmentStockData
{
    public class DepartmentStockInExtraController : DepartmentStockInController, IDepartmentStockInExtraController
    {
        
                #region IDepartmentStockInExtraController Members

                private IDepartmentStockInExtraView departmentStockInExtraView;
                public AppFrame.View.GoodsIO.DepartmentGoodsIO.IDepartmentStockInExtraView DepartmentStockInExtraView
                {
                    get
                    {
                        return departmentStockInExtraView;
                    }
                    set
                    {
                        departmentStockInExtraView = value;

                        departmentStockInExtraView.FillProductToComboEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_FillProductToComboEvent);
                        departmentStockInExtraView.LoadGoodsByIdEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadGoodsByIdEvent);
                        departmentStockInExtraView.LoadGoodsByNameEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadGoodsByNameEvent);

                        departmentStockInExtraView.LoadProductColorEvent += new EventHandler<DepartmentStockInEventArgs>(departmentStockInExtraView_LoadProductColorEvent);
                        departmentStockInExtraView.LoadProductSizeEvent += new EventHandler<DepartmentStockInEventArgs>(departmentStockInExtraView_LoadProductSizeEvent);
                        departmentStockInExtraView.LoadGoodsByNameColorEvent +=
                            new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadGoodsByNameAndColorEvent);
                        departmentStockInExtraView.LoadGoodsByNameColorSizeEvent +=
                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadGoodsByNameColorSizeEvent);
                        departmentStockInExtraView.FillDepartmentEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_FillDepartmentEvent);
                        departmentStockInExtraView.LoadPriceAndStockEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadPriceAndStockEvent);
                        departmentStockInExtraView.LoadDepartemntStockInForExportEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_LoadDepartemntStockInForExportEvent);
                        departmentStockInExtraView.UpdateDepartemntStockInForExportEvent +=
                                    new EventHandler<DepartmentStockInEventArgs>(_departmentStockInView_UpdateDepartemntStockInForExportEvent);
                    }
                }

        public void _departmentStockInView_UpdateDepartemntStockInForExportEvent(object sender, DepartmentStockInEventArgs e)
        {
            e.DepartmentStockIn.ExportStatus = CommonConstants.DEL_FLG_YES;
            e.DepartmentStockIn.UpdateDate = DateTime.Now;
            e.DepartmentStockIn.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            DepartmentStockInLogic.UpdateExportStatus(e.DepartmentStockIn);
        }

        public void _departmentStockInView_LoadDepartemntStockInForExportEvent(object sender, DepartmentStockInEventArgs e)
        {
            DateTime lastSyncTime = e.LastSyncTime;
            e.SyncFromMainToDepartment = new SyncFromMainToDepartment();
            var stockTempCri = new ObjectCriteria();
            stockTempCri.AddEqCriteria("Fixed", CommonConstants.DEL_FLG_YES);
            stockTempCri.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);

            IList processedStockTemps = DepartmentStockTempLogic.FindAll(stockTempCri);
            e.SyncFromMainToDepartment.DepartmentStockTemps = processedStockTemps;
            
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddEqCriteria("DepartmentId", e.Department.DepartmentId);
            criteria.AddGreaterOrEqualsCriteria("CreateDate", lastSyncTime);
            
            e.SyncFromMainToDepartment.StockOutList = StockOutLogic.FindAll(criteria);
            e.SyncFromMainToDepartment.Department = e.Department;
            criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            //criteria.AddEqCriteria("EmployeePK.DepartmentId", e.Department.DepartmentId);
            e.SyncFromMainToDepartment.Department.Employees = EmployeeLogic.FindAll(criteria);
            //foreach (DepartmentStockIn departmentStockIn in e.DepartmentStockInList)
            if (e.SyncFromMainToDepartment.StockOutList == null || e.SyncFromMainToDepartment.StockOutList.Count == 0)
            {
                return; // don't need to get detail
            }
            foreach (StockOut departmentStockIn in e.SyncFromMainToDepartment.StockOutList)
            {

                foreach (StockOutDetail detail in departmentStockIn.StockOutDetails)
                {
                    DepartmentPrice price =
                        DepartmentPriceLogic.FindById(new DepartmentPricePK
                                                          {
                                                              DepartmentId = 0,
                                                              ProductMasterId =
                                                                  detail.Product.ProductMaster.ProductMasterId
                                                          });
                    if (price != null)
                    {
                        detail.Price = price.Price;
                        
                    }
                }
            }
            
        }

        void _departmentStockInView_LoadPriceAndStockEvent(object sender, DepartmentStockInEventArgs e)
        {
            GetRemainStockNumber(e.DepartmentStockInDetailList);
        }

        void _departmentStockInView_FillDepartmentEvent(object sender, DepartmentStockInEventArgs e)
                {
                    var criteria = new ObjectCriteria();
                    criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                    e.DepartmentList = DepartmentLogic.FindAll(criteria);
                }

                void departmentStockInExtraView_LoadProductSizeEvent(object sender, DepartmentStockInEventArgs e)
                {
                    if (e.SelectedDepartmentStockInDetail != null && e.SelectedDepartmentStockInDetail.Product != null && !string.IsNullOrEmpty(e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductName))
                    {
                        var subCriteria = new SubObjectCriteria("ProductMasters");
                        subCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                        subCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductType);
                        subCriteria.AddEqCriteria("ProductName", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductName);
                        subCriteria.AddEqCriteria("ProductColor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductColor);
                        subCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Country);
                        subCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Manufacturer);
                        subCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Distributor);
                        subCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Packager);
                        var criteria = new ObjectCriteria();
                        criteria.AddSubCriteria("ProductMasters", subCriteria);
                        IList productSizes = ProductSizeLogic.FindAll(criteria);
                        e.ProductSizeList = productSizes;
                    }
                }

                void departmentStockInExtraView_LoadProductColorEvent(object sender, DepartmentStockInEventArgs e)
                {
                    if (e.SelectedDepartmentStockInDetail != null && e.SelectedDepartmentStockInDetail.Product != null
                        && !string.IsNullOrEmpty(e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductName))
                    {
                        var subCriteria = new SubObjectCriteria("ProductMasters");
                        subCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                        subCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductType);
                        subCriteria.AddEqCriteria("ProductName", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductName);
                        subCriteria.AddEqCriteria("ProductSize", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductSize);
                        subCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Country);
                        subCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Manufacturer);
                        subCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Distributor);
                        subCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Packager);
                        var criteria = new ObjectCriteria();
                        criteria.AddSubCriteria("ProductMasters",subCriteria);
                        IList productColors = ProductColorLogic.FindAll(criteria);
                        e.ProductColorList = productColors;
                    }
                }

                void _departmentStockInView_LoadGoodsByNameEvent(object sender, DepartmentStockInEventArgs e)
                    {
                    DepartmentStockInDetail detail = e.SelectedDepartmentStockInDetail;
                    ObjectCriteria objectCriteria = new ObjectCriteria();
                    objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                    objectCriteria.AddEqCriteria("ProductName", detail.Product.ProductMaster.ProductName);            
                    IList list  = ProductMasterLogic.FindAll(objectCriteria);
                    e.FoundProductMasterList = list;
                    if (list == null || list.Count == 0)
                    {
                        return;
                    }
                    ProductMaster prodMaster = list[0] as ProductMaster;
                    detail.Product.ProductMaster = prodMaster;
                    e.SelectedDepartmentStockInDetail = detail;
                    IList detailList = new ArrayList();
                    detailList.Add(detail);
                    GetRemainStockNumber(detailList);
                    }

                void _departmentStockInView_LoadGoodsByNameAndColorEvent(object sender, DepartmentStockInEventArgs e)
                {
                    DepartmentStockInDetail detail = e.SelectedDepartmentStockInDetail;
                    ObjectCriteria objectCriteria = new ObjectCriteria();
                    objectCriteria.AddEqCriteria("ProductName", detail.Product.ProductMaster.ProductName);
                    objectCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductType);
                    objectCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Country);
                    objectCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Manufacturer);
                    objectCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Distributor);
                    objectCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Packager);
                    objectCriteria.AddEqCriteria("ProductColor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductColor);
                    IList list = ProductMasterLogic.FindAll(objectCriteria);
                    if (list == null || list.Count == 0)
                    {
                        return;
                    }
                    ProductMaster prodMaster = list[0] as ProductMaster;
                    detail.Product.ProductMaster = prodMaster;
                    e.SelectedDepartmentStockInDetail = detail;
                    departmentStockInExtraView_LoadProductSizeEvent(sender, e);
                    IList detailList = new ArrayList();
                    detailList.Add(detail);
                    GetRemainStockNumber(detailList);
                }

                void _departmentStockInView_LoadGoodsByNameColorSizeEvent(object sender, DepartmentStockInEventArgs e)
                {
                    DepartmentStockInDetail detail = e.SelectedDepartmentStockInDetail;
                    ObjectCriteria objectCriteria = new ObjectCriteria();
                    objectCriteria.AddEqCriteria("ProductName", detail.Product.ProductMaster.ProductName);
                    objectCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductType);
                    objectCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Country);
                    objectCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Manufacturer);
                    objectCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Distributor);
                    objectCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockInDetail.Product.ProductMaster.Packager);
                    objectCriteria.AddEqCriteria("ProductColor", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductColor);
                    objectCriteria.AddEqCriteria("ProductSize", e.SelectedDepartmentStockInDetail.Product.ProductMaster.ProductSize);
                    IList list = ProductMasterLogic.FindAll(objectCriteria);
                    if (list == null || list.Count == 0)
                    {
                        return;
                    }
                    ProductMaster prodMaster = list[0] as ProductMaster;
                    detail.Product.ProductMaster = prodMaster;
                    e.SelectedDepartmentStockInDetail = detail;
                    IList detailList = new ArrayList();
                    detailList.Add(detail);
                    GetRemainStockNumber(detailList);
                }


                void _departmentStockInView_LoadGoodsByIdEvent(object sender, DepartmentStockInEventArgs e)
                {
                    DepartmentStockInDetail detail = e.SelectedDepartmentStockInDetail;
                    ProductMaster prodMaster = ProductMasterLogic.FindById(detail.Product.ProductMaster.ProductMasterId);
                    if (prodMaster == null)
                    {
                        return;
                    }
                    detail.Product.ProductMaster = prodMaster;

                    e.SelectedDepartmentStockInDetail = detail;
                    IList detailList = new ArrayList();
                    detailList.Add(detail);
                    GetRemainStockNumber(detailList);
                }

                void _departmentStockInView_FillProductToComboEvent(object sender, DepartmentStockInEventArgs e)
                {
                    ComboBox comboBox = (ComboBox) sender;
                    string originalText = comboBox.Text;
                    if (e.IsFillToComboBox)
                    {
                        ProductMaster searchPM = e.SelectedDepartmentStockInDetail.Product.ProductMaster;
                        var criteria = new ObjectCriteria(true);
                        criteria.AddEqCriteria("pm.DelFlg", CommonConstants.DEL_FLG_NO);
                        criteria.AddEqCriteria("stock.DelFlg", CommonConstants.DEL_FLG_NO);
                        criteria.AddLikeCriteria("pm.ProductName", "%" + searchPM.ProductName + "%");
                        criteria.MaxResult = 50;
                        IList list = StockLogic.FindByQueryForStockIn(criteria);
//                        if (e.ComboBoxDisplayMember.Equals("ProductMasterId"))
//                        {
//                            result = ProductMasterLogic.FindProductMasterById(searchPM.ProductMasterId, 50,true);
//                        }
//                        else
//                        {
//                            result = ProductMasterLogic.FindProductMasterByName(searchPM.ProductName, 50,true);
//                        }

                        if(list ==null || list.Count == 0)
                        {
                            return;
                        }
                        IList result = new ArrayList();
                        foreach (Stock stock in list)
                        {
                            result.Add(stock.ProductMaster);
                        }
                        IList retlist = RemoveDuplicateName(result);

                        result = new ArrayList();
                        int count = 0;
                        foreach (var p in retlist)
                        {
                            result.Add(p);
                            count++;
                        }

                        BindingList<ProductMaster> productMasters = new BindingList<ProductMaster>();
                        if (result != null)
                        {
                            foreach (ProductMaster master in result)
                            {
                                productMasters.Add(master);
                            }
                        }
                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = productMasters;
                        comboBox.DataSource = bindingSource;
                        comboBox.DisplayMember = "TypeAndName";
                        comboBox.ValueMember = e.ComboBoxDisplayMember;
                        comboBox.DropDownWidth = 300;
                        comboBox.DropDownHeight = 200;
                        // keep the original text
                        comboBox.Text = originalText;
                        //comboBox.SelectedIndex = -1;
                        //comboBox.SelectionStart = comboBox.Text.Length;
                        //comboBox.DroppedDown = true;
                        comboBox.MaxDropDownItems = 10;
                    }
                }
        
        private void GetRemainStockNumber(IList departmentStockIns)
        {
            IList productMasterIds = new ArrayList();
            foreach (DepartmentStockInDetail detail in departmentStockIns)
            {
                if (detail.Product != null && detail.Product.ProductMaster != null && detail.Product.ProductMaster.ProductMasterId != null)
                productMasterIds.Add(detail.Product.ProductMaster.ProductMasterId);
            }
            if (productMasterIds.Count == 0)
            {
                return;
            }
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddSearchInCriteria("ProductMaster.ProductMasterId", productMasterIds);
            IList stockList = StockLogic.FindAll(criteria);
            criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddEqCriteria("DepartmentPricePK.DepartmentId", (long)0);
            criteria.AddSearchInCriteria("DepartmentPricePK.ProductMasterId", productMasterIds);
            IList priceList = DepartmentPriceLogic.FindAll(criteria);
            foreach (DepartmentStockInDetail detail in departmentStockIns)
            {
                detail.StockQuantity = 0;
                if (detail.Product != null
                    && detail.Product.ProductMaster != null 
                    && detail.Product.ProductMaster.ProductMasterId != null)
                {
                    foreach (Stock stock in stockList)
                    {
                        if (detail.Product.ProductMaster.ProductMasterId.Equals(stock.ProductMaster.ProductMasterId))
                        {
                            detail.StockQuantity += stock.Quantity;
                        }
                    }
                    foreach (DepartmentPrice price in priceList)
                    {
                        if (detail.Product.ProductMaster.ProductMasterId.Equals(price.DepartmentPricePK.ProductMasterId))
                        {
                            detail.Price = price.Price;
                        }
                    }
                }
            }
        }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void departmentStockInView_SaveDepartmentStockInEvent(object sender, DepartmentStockInEventArgs e)
        {
            var stockIn = e.DepartmentStockIn;
            if (stockIn.StockInId == 0)
            {
                // if we are doing stock out to department
                if(e.ExportGoodsToDepartment)
                {
                    // execute stock out saving
                    StockOutLogic.Add(stockIn);
                    if (!e.IsForSync)
                    {
                        ClientUtility.Log(logger, stockIn.ToString(), "Xuất hàng ra cửa hàng");
                    }
                    else
                    {
                        ClientUtility.Log(logger, "Đồng bộ về cửa hàng.\r\n" + stockIn.ToString(), "Xuất hàng ra cửa hàng");
                    }

                }
                else
                {
                    DepartmentStockInLogic.Add(stockIn);
                    if (!e.IsForSync)
                    {
                        ClientUtility.Log(logger, stockIn.ToString(), "Nhập kho cửa hàng");
                    }
                    else
                    {
                        ClientUtility.Log(logger, "Đồng bộ về cửa hàng.\r\n" + stockIn.ToString(), "Đồng bộ về cửa hàng");
                    }
                }
                
            }
            else
            {
                DepartmentStockInLogic.Update(stockIn);
                if (!e.IsForSync)
                {
                    ClientUtility.Log(logger, stockIn.ToString(), "Nhập kho cửa hàng");
                }
                else
                {
                    ClientUtility.Log(logger, "Đồng bộ về cửa hàng.\r\n" + stockIn.ToString(), "Đồng bộ về cửa hàng");
                }
            }
            if (stockIn.DepartmentStockInPK != null && e.IsForSync)
            {
                e.DepartmentStockIn = DepartmentStockInLogic.FindById(stockIn.DepartmentStockInPK);
                IList productMasterIds = new ArrayList();
                foreach (DepartmentStockInDetail detail in e.DepartmentStockIn.DepartmentStockInDetails)
                {
                    if (!productMasterIds.Contains(detail.Product.ProductMaster.ProductMasterId))
                    {
                        productMasterIds.Add(detail.Product.ProductMaster.ProductMasterId);
                    }
                }
                var criteria = new ObjectCriteria();
                criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                criteria.AddEqCriteria("DepartmentPricePK.DepartmentId", (long)0);
                criteria.AddSearchInCriteria("DepartmentPricePK.ProductMasterId", productMasterIds);
                IList priceList = DepartmentPriceLogic.FindAll(criteria);
                foreach (DepartmentStockInDetail detail in e.DepartmentStockIn.DepartmentStockInDetails)
                {
                    foreach (DepartmentPrice price in priceList)
                    {
                        if (price.DepartmentPricePK.ProductMasterId.Equals(detail.Product.ProductMaster.ProductMasterId))
                        {
                            detail.Price = price.Price;
                        }
                    }
                }

                // Department information
                e.DepartmentStockIn.Department = DepartmentLogic.FindById(stockIn.DepartmentStockInPK.DepartmentId);
                criteria = new ObjectCriteria();
                criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                criteria.AddEqCriteria("EmployeePK.DepartmentId", stockIn.DepartmentStockInPK.DepartmentId);
                e.DepartmentStockIn.Department.Employees = EmployeeLogic.FindAll(criteria);
            }
            e.EventResult = "Success";
        }

        private IList RemoveDuplicateName(IList prdlist)
        {
            IList list = new ArrayList();
            foreach (ProductMaster productMaster in prdlist)
            {
                if (NotInList(productMaster, list))
                {
                    list.Add(productMaster);
                }
            }
            return list;
        }

        private bool NotInList(ProductMaster master, IList list)
        {
            foreach (ProductMaster productMaster in list)
            {
                if (productMaster.ProductName.Equals(master.ProductName))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

                #region IDepartmentStockInExtraController Members
                public IDepartmentLogic DepartmentLogic
                {
                    get;
                    set;
                }

                public AppFrame.Logic.IProductColorLogic ProductColorLogic
                {
                    get;set;
            
                }

                public AppFrame.Logic.IProductSizeLogic ProductSizeLogic
                {
                    get;set;
                }
                public AppFrame.Logic.IStockLogic StockLogic
                {
                    get;
                    set;
                }
                public AppFrame.Logic.IEmployeeDetailLogic EmployeeDetailLogic
                {
                    get;
                    set;
                }

                public IDepartmentStockTempLogic DepartmentStockTempLogic
                {
                    get; set;
                }
                #endregion
            }
    }


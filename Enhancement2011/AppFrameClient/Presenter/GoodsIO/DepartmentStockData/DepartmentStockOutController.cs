﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using AppFrame;
using AppFrame.Common;
using AppFrame.Exceptions;
using AppFrame.Logic;
using AppFrame.Model;
using AppFrame.Presenter.GoodsIO.DepartmentGoodsIO;
using AppFrame.Presenter.GoodsIO.MainStock;
using AppFrame.Utility;
using AppFrame.View.GoodsIO.DepartmentGoodsIO;
using AppFrame.View.GoodsIO.MainStock;
using AppFrameClient.Common;
using AppFrameClient.Services;
using AppFrameClient.Utility;
using InfoBox;

namespace AppFrameClient.Presenter.GoodsIO.DepartmentStockData
{
    public class DepartmentStockOutController : IDepartmentStockOutController,ServerServiceCallback
    {
        //private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region IDepartmentStockInExtraController Members

        private IDepartmentStockOutView deptStockOutView;
        public IDepartmentStockOutView DepartmentStockOutView
        {
            get
            {
                return deptStockOutView;
            }
            set
            {
                deptStockOutView = value;

                deptStockOutView.FindBarcodeEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_FindBarcodeEvent);
                deptStockOutView.SaveStockOutEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_SaveStockOutEvent);
                deptStockOutView.FillProductToComboEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_FillProductToComboEvent);
                deptStockOutView.LoadStockStatusEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_LoadStockStatusEvent);
                deptStockOutView.LoadGoodsByNameColorSizeEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_LoadGoodsByNameColorSizeEvent);
                deptStockOutView.LoadGoodsByNameEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_LoadGoodsByNameEvent);
                deptStockOutView.LoadProductColorEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_LoadProductColorEvent);
                deptStockOutView.LoadProductSizeEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_LoadProductSizeEvent);
                deptStockOutView.GetSyncDataEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_GetSyncDataEvent);
                deptStockOutView.SyncToMainEvent += new EventHandler<DepartmentStockOutEventArgs>(
                    _departmentStockOutView_SyncToMainEvent);
                deptStockOutView.LoadAllDepartments += new EventHandler<DepartmentStockOutEventArgs>(deptStockOutView_LoadAllDepartments);
                deptStockOutView.DispatchDepartmentStockOut += new EventHandler<DepartmentStockOutEventArgs>(deptStockOutView_DispatchDepartmentStockOut);
                deptStockOutView.PrepareDepartmentStockOutForPrintEvent += new EventHandler<DepartmentStockOutEventArgs>(deptStockOutView_PrepareDepartmentStockOutForPrintEvent);
                deptStockOutView.FindByStockInIdEvent += new EventHandler<DepartmentStockOutEventArgs>(deptStockOutView_FindByStockInIdEvent);

            }
        }

        public event EventHandler<DepartmentStockOutEventArgs> CompletedFindByStockInEvent;

        void deptStockOutView_FindByStockInIdEvent(object sender, DepartmentStockOutEventArgs e)
        {
            if (e.SelectedStockInIds.Count > 0)
            {
                ObjectCriteria objectCriteria = new ObjectCriteria();
                objectCriteria.AddSearchInCriteria("DepartmentStockInDetailPK.StockInId", e.SelectedStockInIds);
                IList list = DepartmentStockInDetailLogic.FindAll(objectCriteria);
                IList stockOutList = new ArrayList();
                foreach (DepartmentStockInDetail inDetail in list)
                {
                    DepartmentStockOutDetail deptDetail = new DepartmentStockOutDetail();
                    deptDetail.CreateDate = DateTime.Now;
                    deptDetail.UpdateDate = DateTime.Now;
                    deptDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    deptDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    //deptDetail. = new DepartmentStockInDetailPK();
                    deptDetail.Product = inDetail.Product;
                    deptDetail.ProductMaster = inDetail.Product.ProductMaster;
                    deptDetail.Quantity = inDetail.Quantity;
                    deptDetail.GoodQuantity = inDetail.Quantity;
                    stockOutList.Add(deptDetail);
                }
                IList stockList = GetRemainStockNumber(stockOutList);
                e.FoundDepartmentStockOutDetailList = stockOutList;
                e.DepartmentStockList = stockList;
            }
            EventUtility.fireEvent(CompletedFindByStockInEvent, this, e);
        }

        private IList GetRemainStockNumber(IList departmentStockIns)
        {
            IList productLists = new ArrayList();
            foreach (DepartmentStockOutDetail detail in departmentStockIns)
            {
                if (detail.Product != null)
                    productLists.Add(detail.Product.ProductId);
            }
            if (productLists.Count == 0)
            {
                return new ArrayList();
            }
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddEqCriteria("DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            criteria.AddSearchInCriteria("DepartmentStockPK.ProductId", productLists);
            IList stockList = DepartmentStockLogic.FindAll(criteria);

            if (stockList != null && stockList.Count > 0)
            {
                foreach (DepartmentStockOutDetail detail in departmentStockIns)
                {
                    foreach (DepartmentStock stock in stockList)
                    {
                        if (stock.DepartmentStockPK.ProductId.Equals(detail.Product.ProductId))
                        {
                            detail.Quantity = stock.Quantity;
                            break;
                        }
                    }
                }
            }

            return stockList;
        }


        void deptStockOutView_PrepareDepartmentStockOutForPrintEvent(object sender, DepartmentStockOutEventArgs e)
        {
            Department destDept = DepartmentLogic.FindById(e.DepartmentStockOut.OtherDepartmentId);
            if (destDept != null)
            {
                foreach (DepartmentStockOutDetail detail in e.DepartmentStockOut.DepartmentStockOutDetails)
                {
                    string prdMasterId = detail.Product.ProductMaster.ProductMasterId;
                    DepartmentPricePK pricePk = new DepartmentPricePK
                    {
                        DepartmentId = 0,
                        ProductMasterId = prdMasterId
                    };
                    detail.DepartmentPrice = DepartmentPriceLogic.FindById(pricePk);
                }
            }            
        }

        void deptStockOutView_DispatchDepartmentStockOut(object sender, DepartmentStockOutEventArgs e)
        {
            
            Department destDept = DepartmentLogic.FindById(e.DepartmentStockOut.OtherDepartmentId);
            if (destDept != null)
            {
                foreach (DepartmentStockOutDetail detail in e.DepartmentStockOut.DepartmentStockOutDetails)
                {
                    string prdMasterId = detail.Product.ProductMaster.ProductMasterId;
                    DepartmentPricePK pricePk = new DepartmentPricePK
                                                    {
                                                        DepartmentId = 0,
                                                        ProductMasterId = prdMasterId
                                                    };
                    detail.DepartmentPrice = DepartmentPriceLogic.FindById(pricePk);
                }

                SyncFromDeptToDept fromDeptToDept = new SyncFromDeptToDept
                                                        {
                                                            DestinationDept = destDept
                                                        };
                fromDeptToDept.DepartmentStockOutList = new ArrayList();
                fromDeptToDept.DepartmentStockOutList.Add(e.DepartmentStockOut);

                CopyToSyncFolder(fromDeptToDept);
                
                ServerServiceClient serverService = new ServerServiceClient(new InstanceContext(this), ClientSetting.ServiceBinding);
                serverService.MakeRawDepartmentStockOut(destDept,e.DepartmentStockOut,new DepartmentPrice());

            }

        }

        private void CopyToSyncFolder(SyncFromDeptToDept fromDeptToDept)
        {
            EnsureSyncInternalPath();
            CopyToExportFolder(fromDeptToDept);
        }

        private void CopyToExportFolder(SyncFromDeptToDept fromDeptToDept)
        {
            string rootPath = Application.CommonAppDataPath;
            string internalPath = ClientSetting.SyncInternalPath;
            if (!internalPath.StartsWith(@"\")) internalPath = @"\" + internalPath;
            rootPath = rootPath + internalPath;
            string syncExportPath = rootPath + ClientSetting.SyncExportPath;
            string syncImportPath = rootPath + ClientSetting.SyncImportPath;
            string syncSuccessPath = rootPath + ClientSetting.SyncSuccessPath;
            string syncErrorPath = rootPath + ClientSetting.SyncErrorPath;
            try
            {
                string fileName = syncExportPath + "\\" + fromDeptToDept.DestinationDept
                                  + "_SyncDown_"
                                  + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")
                                  + CommonConstants.SERVER_SYNC_FORMAT;
                Stream stream = File.Open(fileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, fromDeptToDept);
                stream.Flush();
                stream.Close();
                InformationBox.Show("Chép file vào thư mục đồng bộ thành công !", new AutoCloseParameters(3));
            }
            catch(Exception ex)
            {
                InformationBox.Show("Chép file vào thư mục đồng bộ thất bại !", new AutoCloseParameters(3));
            }
        }

        private void EnsureSyncInternalPath()
        {
            string rootPath = Application.CommonAppDataPath;
            string internalPath = ClientSetting.SyncInternalPath;
            if (!internalPath.StartsWith(@"\")) internalPath = @"\" + internalPath;
            rootPath = rootPath + internalPath;
            string syncExportPath = rootPath + ClientSetting.SyncExportPath;
            string syncImportPath = rootPath + ClientSetting.SyncImportPath;
            string syncSuccessPath = rootPath + ClientSetting.SyncSuccessPath;
            string syncErrorPath = rootPath + ClientSetting.SyncErrorPath;
            EnsurePath(syncExportPath);
            EnsurePath(syncImportPath);
            EnsurePath(syncSuccessPath);
            EnsurePath(syncErrorPath);
        }

        private void EnsurePath(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        void deptStockOutView_LoadAllDepartments(object sender, DepartmentStockOutEventArgs e)
        {
            IList list = DepartmentLogic.FindAll(null);
            e.DepartmentsList = list;
        }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void _departmentStockOutView_SyncToMainEvent(object sender, DepartmentStockOutEventArgs e)
        {
            DepartmentStockOutLogic.SyncToMain(e.SyncFromDepartmentToMain);
            ClientUtility.Log(logger, e.SyncFromDepartmentToMain.ToString(), "Đồng bộ về kho");
            e.EventResult = "Success";
        }

        public void _departmentStockOutView_GetSyncDataEvent(object sender, DepartmentStockOutEventArgs e)
        {
            // first process all timeline
            DepartmentTimelineLogic.ProcessPeriod(e.IsConfirmPeriod);
            // then get data
            e.SyncFromDepartmentToMain = DepartmentStockOutLogic.GetSyncData(e.IsConfirmPeriod,e.LastSyncTime);
        }

        public void _departmentStockOutView_LoadProductSizeEvent(object sender, DepartmentStockOutEventArgs e)
        {
            if (e.SelectedDepartmentStockOutDetail != null && e.SelectedDepartmentStockOutDetail.Product != null && !string.IsNullOrEmpty(e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductName))
            {
                var subCriteria = new SubObjectCriteria("ProductMasters");
                subCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                subCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductType);
                subCriteria.AddEqCriteria("ProductName", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductName);
                subCriteria.AddEqCriteria("ProductColor", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductColor);
                subCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Country);
                subCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Manufacturer);
                subCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Distributor);
                subCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Packager);
                var criteria = new ObjectCriteria();
                criteria.AddSubCriteria("ProductMasters", subCriteria);
                IList productSizes = ProductSizeLogic.FindAll(criteria);
                e.ProductSizeList = productSizes;
            }
        }

        public void _departmentStockOutView_LoadProductColorEvent(object sender, DepartmentStockOutEventArgs e)
        {
            if (e.SelectedDepartmentStockOutDetail != null && e.SelectedDepartmentStockOutDetail.Product != null
                                    && !string.IsNullOrEmpty(e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductName))
            {
                var subCriteria = new SubObjectCriteria("ProductMasters");
                subCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                subCriteria.AddEqCriteria("ProductType", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductType);
                subCriteria.AddEqCriteria("ProductName", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductName);
                subCriteria.AddEqCriteria("ProductSize", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.ProductSize);
                subCriteria.AddEqCriteria("Country", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Country);
                subCriteria.AddEqCriteria("Manufacturer", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Manufacturer);
                subCriteria.AddEqCriteria("Distributor", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Distributor);
                subCriteria.AddEqCriteria("Packager", e.SelectedDepartmentStockOutDetail.Product.ProductMaster.Packager);
                var criteria = new ObjectCriteria();
                criteria.AddSubCriteria("ProductMasters", subCriteria);
                IList productColors = ProductColorLogic.FindAll(criteria);
                e.ProductColorList = productColors;
            }
        }

        public void _departmentStockOutView_LoadGoodsByNameEvent(object sender, DepartmentStockOutEventArgs e)
        {
            DepartmentStockOutDetail detail = e.SelectedDepartmentStockOutDetail;
            ObjectCriteria objectCriteria = new ObjectCriteria();
            objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            objectCriteria.AddEqCriteria("ProductName", detail.Product.ProductMaster.ProductName);
            IList list = ProductMasterLogic.FindAll(objectCriteria);
            e.FoundProductMasterList = list;
            if (list == null || list.Count == 0)
            {
                return;
            }
            ProductMaster prodMaster = list[0] as ProductMaster;
            detail.Product.ProductMaster = prodMaster;
            e.SelectedDepartmentStockOutDetail = detail;
            IList detailList = new ArrayList();
            detailList.Add(detail);
//            GetRemainStockNumber(detailList);
        }

        public void _departmentStockOutView_LoadGoodsByNameColorSizeEvent(object sender, DepartmentStockOutEventArgs e)
        {
            
        }

        public void _departmentStockOutView_LoadStockStatusEvent(object sender, DepartmentStockOutEventArgs e)
        {
            if (e.SelectedProductMasterList == null || e.SelectedProductMasterList.Count == 0) return;

            IList productMasterIds = new ArrayList();
            foreach (ProductMaster master in e.SelectedProductMasterList)
            {
                productMasterIds.Add(master.ProductMasterId);
            }
            ObjectCriteria prdCrit = new ObjectCriteria();
            prdCrit.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            prdCrit.AddSearchInCriteria("ProductMaster.ProductMasterId", productMasterIds);
            IList prdList = ProductLogic.FindAll(prdCrit);

            IList stkPrdIdsList = new ArrayList();
            foreach (Product product in prdList)
            {
                stkPrdIdsList.Add(product.ProductId);
            }

            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddSearchInCriteria("DepartmentStockPK.ProductId", stkPrdIdsList);
            
            IList list = DepartmentStockLogic.FindAll(criteria);
            if (list.Count == 0)
            {
                return;
            }

            
            e.DepartmentStockList = new ArrayList();
            e.FoundDepartmentStockOutDetailList = new ArrayList();
            foreach (DepartmentStock stock in list)
            {
                /*if(stock.Quantity == 0)
                {
                    continue;
                }*/
                DepartmentStockOutDetail detail = new DepartmentStockOutDetail();
                detail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK
                                                        {
                                                            DepartmentId = CurrentDepartment.Get().DepartmentId
                                                        };
                detail.Product = stock.Product;
                detail.GoodQuantity = stock.GoodQuantity;
                detail.ErrorQuantity = stock.ErrorQuantity;
                detail.LostQuantity = stock.LostQuantity;
                detail.DamageQuantity = stock.DamageQuantity;
                detail.UnconfirmQuantity = stock.UnconfirmQuantity;
                detail.Quantity = stock.Quantity;
                e.DepartmentStockList.Add(stock);
                e.FoundDepartmentStockOutDetailList.Add(detail);
            }
        }

        
        public void _departmentStockOutView_FillProductToComboEvent(object sender, DepartmentStockOutEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            string originalText = comboBox.Text;
            if (e.IsFillToComboBox)
            {

                ProductMaster searchPM = e.SelectedDepartmentStockOutDetail.Product.ProductMaster;
                var criteria = new ObjectCriteria(true);
                criteria.AddEqCriteria("pm.DelFlg", CommonConstants.DEL_FLG_NO);
                criteria.AddEqCriteria("stock.DelFlg", CommonConstants.DEL_FLG_NO);
                criteria.AddLikeCriteria("pm.ProductName", "%" + searchPM.ProductName + "%");
                criteria.MaxResult = 50;
                IList list = DepartmentStockLogic.FindByQueryForDeptStock(criteria);
                
                if(list ==null || list.Count == 0)
                {
                    return;
                }
                IList result = new ArrayList();
                foreach (DepartmentStock stock in list)
                {
                    result.Add(stock.Product.ProductMaster);
                }
                IList retlist = RemoveDuplicateName(result);

                result = new ArrayList();
                int count = 0;
                foreach (var p in retlist)
                {
                    if (count == 50)
                    {
                        break;
                    }
                    result.Add(p);
                    count++;
                }

                var productMasters = new BindingList<ProductMaster>();
                foreach (ProductMaster master in result)
                {
                    productMasters.Add(master);
                }

                var bindingSource = new BindingSource();
                bindingSource.DataSource = productMasters;
                comboBox.DataSource = bindingSource;
                comboBox.DisplayMember = "TypeAndName";
                comboBox.ValueMember = e.ComboBoxDisplayMember;
                comboBox.DropDownWidth = 300;
                comboBox.DropDownHeight = 200;
                // keep the original text
                comboBox.Text = originalText;
                comboBox.MaxDropDownItems = 10;
            }
        }

        public void _departmentStockOutView_FindBarcodeEvent(object sender, DepartmentStockOutEventArgs e)
        {
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddEqCriteria("DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            criteria.AddEqCriteria("DepartmentStockPK.ProductId", e.ProductId);
            IList list = DepartmentStockLogic.FindAll(criteria);
            if (list.Count == 0)
            {

                /* begin +++++++++++++++++++ XUAT TRUOT LO CHO KHO PHU +++++++++++++++++++++ */
                if(ClientSetting.IsBlockSliding && ClientSetting.IsSubStock())
                {
                    // just sub-stock has the right to do block sliding
                    if(ClientSetting.IsSubStock())
                    {
                        Product product = ProductLogic.FindById(e.ProductId);
                        if(product == null ) throw new BusinessException("Mã vạch này chưa từng được nhập vào kho.");
                        ProductColor color = product.ProductMaster.ProductColor;
                        ProductSize size = product.ProductMaster.ProductSize;
                        string productName = product.ProductMaster.ProductName;
                        var productCriteria = new ObjectCriteria();
                        IList slidedStockList = DepartmentStockLogic.FindSlidingStock(productName,color.ColorName,size.SizeName);
                        BusinessException emptyStockException = new BusinessException(" Hàng " + productName + " - Màu:" + color.ColorName + " - Size:" + size.SizeName + " đã hết. Xin kiểm tra lại !");
                        if (slidedStockList.Count == 0) throw emptyStockException;
                        foreach (DepartmentStock departmentStock in slidedStockList)
                        {
                            if(departmentStock.Quantity > 0 && departmentStock.GoodQuantity > 0 )
                            {
                                list.Add(departmentStock);
                                break;
                            }
                        }
                        if (list.Count == 0) throw emptyStockException;
                    }
                    else // throw error if not sub stock
                    {
                        throw new BusinessException("Bạn đang dùng chức năng xuất trượt lô cho mã vạch không có trong kho. Chức năng xuất trượt lô chỉ có trong kho phụ.");
                        return;        
                    }
                }
                else
                {
                    throw new BusinessException("Mã vạch này không có trong kho ?!?! Đề nghị kiểm tra lại.");
                    return;    
                }
                /* end +++++++++++++++++++ XUAT TRUOT LO CHO KHO PHU +++++++++++++++++++++ */
            }
            DepartmentStock stock = list[0] as DepartmentStock;
            e.SelectedDepartmentStockOutDetail = new DepartmentStockOutDetail();
            e.SelectedDepartmentStockOutDetail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK
                                                                                {
                                                                                    DepartmentId = CurrentDepartment.Get().DepartmentId
                                                                                };
            e.SelectedDepartmentStockOutDetail.Product = stock.Product;
            e.SelectedDepartmentStockOutDetail.Quantity = stock.Quantity;
            e.SelectedDepartmentStockOutDetail.GoodQuantity = stock.Quantity;
            e.SelectedDepartmentStockOutDetail.ErrorQuantity = stock.ErrorQuantity;
            e.SelectedDepartmentStockOutDetail.LostQuantity = stock.LostQuantity;
            e.SelectedDepartmentStockOutDetail.DamageQuantity = stock.DamageQuantity;
            e.SelectedDepartmentStockOutDetail.UnconfirmQuantity = stock.UnconfirmQuantity;
            e.DepartmentStock = stock;

            e.EventResult = "Success";
        }

        public void _departmentStockOutView_SaveStockOutEvent(object sender, DepartmentStockOutEventArgs e)
        {
            if (e.DepartmentStockOut.DepartmentStockOutPK == null || e.DepartmentStockOut.DepartmentStockOutPK.StockOutId == 0)
            {
                if(e.DepartmentStockOut.DepartmentStockOutDetails!= null && e.DepartmentStockOut.DepartmentStockOutDetails.Count > 0)
                {
                    foreach (DepartmentStockOutDetail detail in e.DepartmentStockOut.DepartmentStockOutDetails)
                    {
                        string prdMasterId = detail.Product.ProductMaster.ProductMasterId;
                        DepartmentPricePK pricePk = new DepartmentPricePK
                        {
                            DepartmentId = 0,
                            ProductMasterId = prdMasterId
                        };
                        detail.DepartmentPrice = DepartmentPriceLogic.FindById(pricePk);
                        if (detail.DepartmentPrice != null)
                        {
                            if ("1".Equals(detail.Description)) // if ban si
                            {
                                if(detail.DepartmentPrice.WholeSalePrice == 0 )
                                {
                                    e.EventResult = " Error !";
                                    throw new BusinessException(" Giá sỉ của " + detail.Product.ProductMaster.ProductName + " là 0 ?!");
                                }
                                detail.Description = detail.DepartmentPrice.WholeSalePrice.ToString();
                            }
                            else
                            {
                                if (ClientSetting.IsSubStock())
                                {
                                    if (detail.DepartmentPrice.Price == 0)
                                    {
                                        e.EventResult = null;
                                        throw new BusinessException(" Giá lẻ của " +
                                                                    detail.Product.ProductMaster.ProductName +
                                                                    " là 0 ?!");
                                    }
                                }
                                detail.Description = detail.DepartmentPrice.Price.ToString();
                            }
                        }
                    }    
                }
                
                DepartmentStockOutLogic.Add(e.DepartmentStockOut);
                ClientUtility.Log(logger, e.DepartmentStockOut.ToString(), "Lưu xuất kho cửa hàng");
                e.EventResult = "Success";
            }
        }
        public IDepartmentStockLogic DepartmentStockLogic
        {
            get;
            set;
        }
        public IDepartmentStockOutLogic DepartmentStockOutLogic
        {
            get;
            set;
        }
        
        public IProductColorLogic ProductColorLogic
        {
            get;
            set;

        }
        public IProductSizeLogic ProductSizeLogic
        {
            get;
            set;
        }
        public IProductMasterLogic ProductMasterLogic
        {
            get;
            set;
        }
        #endregion

        public DepartmentStockOutEventArgs ResultEventArgs
        {
            get ; 
            set ; 
        }
        public IDepartmentTimelineLogic DepartmentTimelineLogic
        {
            get; set;
        }
        public IDepartmentLogic DepartmentLogic
        {
            get;
            set;
        }

        public IDepartmentPriceLogic DepartmentPriceLogic
        {
            get;
            set;
        }

        public IDepartmentStockInDetailLogic DepartmentStockInDetailLogic
        {
            get;
            set;
        }

        public IProductLogic ProductLogic
        {
            get; set;
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

        public void NotifyNewDepartmentStockOut(Department department, DepartmentStockOut stockOut, DepartmentPrice price)
        {
            // do nothing
            //MessageBox.Show("Send back OK !");
        }

        public void NotifyNewDepartmentStockIn(Department department, DepartmentStockIn stockOut)
        {
            // do nothing
        }


        public void NotifyConnected()
        {
            // do nothing            
        }

        public void NotifyStockOutSuccess(long sourceDeptId, long deptDeptId, long stockOutId)
        {
            
        }

        public void NotifyStockInSuccess(Department department, DepartmentStockIn stockIn, long stockOutId)
        {
            
        }

        public void NotifyUpdateStockOutFlag(Department department, DepartmentStockIn stockIn, long stockOutId)
        {
            
        }
        

        public void NotifyRequestDepartmentStockOut(long departmentId)
        {
            
        }

        public void NotifyRequestDepartmentStockIn(long departmentId)
        {
            
        }

        public void NotifyNewMultiDepartmentStockOut(Department department, DepartmentStockOut[] list, DepartmentPrice price)
        {
            
        }

        public void NotifyMultiStockInSuccess(Department department, DepartmentStockIn[] stockInList, long id)
        {
            
        }

        public void NotifyNewMultiDepartmentStockOut(Department department, object[] list, DepartmentPrice price)
        {
            
        }

        public void NotifyMultiStockInSuccess(Department department, object[] stockInList, long id)
        {
            
        }

        public void NotifyStockInFail(Department department, DepartmentStockIn stockIn, long id)
        {
            
        }

        public void NotifyStockOutFail(long sourceId, long destId, long stockId)
        {
            
        }

        public void NotifyInformMessage(long destDeptId, int channel, bool isError, string message)
        {
            
        }
        
    }
}

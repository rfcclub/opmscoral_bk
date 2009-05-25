using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame;
using AppFrame.Common;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Exceptions;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter.GoodsSale;
using AppFrame.Utility;

namespace CoralPOS.Presenter.GoodsSale
{
    public class GoodsSaleReturnController : IGoodsSaleReturnController
    {
        #region IGoodsSaleReturnController Members

        private CoralPOS.Interfaces.View.GoodsSale.IGoodsSaleReturnView goodsSaleReturnView; 
        public CoralPOS.Interfaces.View.GoodsSale.IGoodsSaleReturnView GoodsSaleReturnView
        {
            get
            {
                return goodsSaleReturnView;
            }
            set
            {
                goodsSaleReturnView = value;
                goodsSaleReturnView.LoadPurchaseOrderEvent += new EventHandler<GoodsSaleReturnEventArgs>(goodsSaleReturnView_LoadPurchaseOrderEvent);
                goodsSaleReturnView.SavePurchaseOrderEvent += new EventHandler<GoodsSaleReturnEventArgs>(goodsSaleReturnView_SavePurchaseOrderEvent);
                goodsSaleReturnView.LoadGoodsEvent += new EventHandler<GoodsSaleEventArgs>(goodsSaleReturnView_LoadGoodsEvent);

            }
        }

        void goodsSaleReturnView_LoadGoodsEvent(object sender, GoodsSaleEventArgs e)
        {
            PurchaseOrderDetail detail = e.SelectedPurchaseOrderDetail;
            /*ProductMaster prodMaster = ProductMasterLogic.FindById(e.SelectedPurchaseOrderDetail.ProductMaster.ProductMasterId);
            if (prodMaster == null)
            {
                return;
            }*/
            //Product product = ProductLogic.FindById(e.SelectedPurchaseOrderDetail.Product.ProductId);
            ObjectCriteria objectCriteria = new ObjectCriteria();
            objectCriteria.AddEqCriteria("DepartmentStockPK.ProductId", e.SelectedPurchaseOrderDetail.Product.ProductId);
            objectCriteria.AddEqCriteria("DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            objectCriteria.AddGreaterCriteria("GoodQuantity", (long)0);
            IList result = DepartmentStockLogic.FindAll(objectCriteria);
            if (result == null)
            {
                throw new BusinessException("Mặt hàng này không tồn tại trong kho !");
            }
            DepartmentStock stock = (DepartmentStock)result[0];
            Product product = stock.Product;
            detail.Product = product;
            detail.ProductMaster = product.ProductMaster;
            DepartmentPrice price = DepartmentPriceLogic.FindById(new DepartmentPricePK { DepartmentId = 0, ProductMasterId = detail.ProductMaster.ProductMasterId });
            if (price == null)
            {
                return;
            }
            detail.Price = price.Price;
            e.SelectedPurchaseOrderDetail = detail; 
        }

        void goodsSaleReturnView_SavePurchaseOrderEvent(object sender, GoodsSaleReturnEventArgs e)
        {
            try
                {
            // save return order to ReturnPo
            foreach (PurchaseOrderDetail purchaseOrderDetail in e.ReturnPurchaseOrderDetails)
            {
                
                    ReturnPo po = new ReturnPo();
                    po.CreateDate = DateTime.Now;
                    po.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    po.UpdateDate = DateTime.Now;
                    po.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    ReturnPoPK poPK = new ReturnPoPK
                                          {
                                              DepartmentId = purchaseOrderDetail.PurchaseOrderDetailPK.DepartmentId,
                                              PurchaseOrderId =
                                                  purchaseOrderDetail.PurchaseOrderDetailPK.PurchaseOrderId,
                                              PurchaseOrderDetailId =
                                                  purchaseOrderDetail.PurchaseOrderDetailPK.PurchaseOrderDetailId,
                                              CreateDate = DateTime.Now

                                          };
                    po.ReturnPoPK = poPK;
                    po.Quantity = purchaseOrderDetail.Quantity;
                    po.ReturnDate = DateTime.Now;

                    long originAmount = FindOriginAmount(e.RefPurchaseOrder.PurchaseOrderDetails, purchaseOrderDetail);
                    if (originAmount == 0)
                    {
                        throw new BusinessException("Có lỗi ở hoá đơn gốc, đề nghị kiểm tra");
                    }
                    long returnedQuantity = (long) ReturnPoLogic.FindQuantityById(poPK);
                    long currentReturnQuantity = returnedQuantity + +po.Quantity;
                    if (originAmount < currentReturnQuantity)
                    {
                        throw new BusinessException(
                            "Lỗi :" + purchaseOrderDetail.Product.ProductMaster.ProductName +
                            " .Tổng cộng :" + originAmount +
                            " .Đã trả : " + returnedQuantity +
                            " .Số lượng muốn trả: " + po.Quantity + " !");
                    }
                    
                    ObjectCriteria criteria = new ObjectCriteria();
                    criteria.AddEqCriteria("DepartmentStockPK.ProductId", purchaseOrderDetail.Product.ProductId);
                    IList deptStockList = DepartmentStockLogic.FindAll(criteria);
                    if(deptStockList!=null && deptStockList.Count > 0)
                    {

                        DepartmentStock departmentStock = (DepartmentStock)deptStockList[0];
                        departmentStock.GoodQuantity += po.Quantity;
                        departmentStock.Quantity += po.Quantity;
                        DepartmentStockLogic.Update(departmentStock);
                    }
                    else
                    {
                        throw new BusinessException("Không có mặt hàng này trong kho. Xin vui lòng kiểm tra dữ liệu");
                    }
                    ReturnPoLogic.Add(po);
               
                    }
            if (e.NextPurchaseOrder != null 
                && e.NextPurchaseOrder.PurchaseOrderDetails!=null 
                && e.NextPurchaseOrder.PurchaseOrderDetails.Count > 0)
            {
                PurchaseOrderLogic.Add(e.NextPurchaseOrder);
            }
                    e.HasErrors = false;
                }
            catch (Exception ex )
            {
                e.HasErrors = true;
                throw ex;
            }
            

        }

        private long FindOriginAmount(IList details, PurchaseOrderDetail detail)
        {
            foreach (PurchaseOrderDetail orderDetail in details)
            {
                if(orderDetail.PurchaseOrderDetailPK.DepartmentId == detail.PurchaseOrderDetailPK.DepartmentId
                  && orderDetail.PurchaseOrderDetailPK.PurchaseOrderId == detail.PurchaseOrderDetailPK.PurchaseOrderId
                  && orderDetail.PurchaseOrderDetailPK.PurchaseOrderDetailId == detail.PurchaseOrderDetailPK.PurchaseOrderDetailId)
                {
                    return orderDetail.Quantity;
                }
            }
            return 0;
        }

        void goodsSaleReturnView_LoadPurchaseOrderEvent(object sender, GoodsSaleReturnEventArgs e)
        {
            PurchaseOrderPK pk = new PurchaseOrderPK {DepartmentId = CurrentDepartment.Get().DepartmentId,PurchaseOrderId = e.SearchPurchaseOrderId};
            PurchaseOrder purchaseOrder = PurchaseOrderLogic.FindById(pk);
            e.RefPurchaseOrder = purchaseOrder;
        }

        public IPurchaseOrderLogic PurchaseOrderLogic
        {
            get;set;
            
        }

        public IPurchaseOrderDetailLogic PurchaseOrderDetailLogic
        {
            get;set;
            
        }

        public IProductMasterLogic ProductMasterLogic
        {
            get;set;
            
        }

        public IProductLogic ProductLogic
        {
            get;set;
            
        }

        public CoralPOS.Interfaces.Model.PurchaseOrder ReturnPurchaseOrder
        {
            get;set;
            
        }

        public System.Collections.IList ReturnPOs
        {
            get;set;
        }

        public event EventHandler<GoodsSaleReturnEventArgs> CompletedSavePurchaseOrderEvent;

        #endregion

        #region IGoodsSaleReturnController Members


        public IDepartmentPriceLogic DepartmentPriceLogic
        {
            get ;set;
            
        }

        #endregion

        #region IGoodsSaleReturnController Members


        public IReturnPoLogic ReturnPoLogic
        {
            get;set;
        }

        #endregion

        #region IGoodsSaleReturnController Members


        public PurchaseOrder NextPurchaseOrder
        {
            get;set;
        }

        #endregion

        #region IGoodsSaleReturnController Members


        public IDepartmentStockLogic DepartmentStockLogic
        {
            get;set;
            
        }

        #endregion
    }
}

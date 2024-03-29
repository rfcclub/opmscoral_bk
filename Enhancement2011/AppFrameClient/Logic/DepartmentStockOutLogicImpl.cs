using System;
using System.Collections;
using System.Threading;
using AppFrame.Common;
using AppFrame.Exceptions;
using AppFrame.Utility;
using AppFrameClient.Common;
using AppFrameClient.Utility;
using AppFrameClient.Utility.Mapper;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;
using AppFrame.Model;
using AppFrame.DataLayer;

namespace AppFrame.Logic
{
    public class DepartmentStockOutLogicImpl : IDepartmentStockOutLogic
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IStockInDAO StockInDAO { get; set;}
        private IStockInDetailDAO StockInDetailDAO { get; set; }
        public IDepartmentStockInDetailDAO DepartmentStockInDetailDAO { get; set; }
        public IDepartmentStockDAO DepartmentStockDAO { get; set; }
        public IDepartmentStockTempDAO DepartmentStockTempDAO { get; set; }
        public IStockDAO StockDAO { get; set; }
        public IDepartmentStockOutDAO DepartmentStockOutDAO { get; set; }
        public IDepartmentStockOutDetailDAO DepartmentStockOutDetailDAO { get; set; }
        public IDepartmentStockHistoryDAO DepartmentStockHistoryDAO { get; set; }
        public IPurchaseOrderDAO PurchaseOrderDAO { get; set; }
        public IPurchaseOrderDetailDAO PurchaseOrderDetailDAO { get; set; }
        public IReturnPoDAO ReturnPoDAO { get; set; }
        public IDepartmentStockInDAO DepartmentStockInDAO { get; set; }
        public IDepartmentStockInDetailDAO DepartmentStockInDAODetailDAO { get; set; }
        public IEmployeeMoneyDAO EmployeeMoneyDAO { get; set;}
        public IDepartmentCostDAO DepartmentCostDAO { get; set; }

        public IDepartmentTimelineDAO DepartmentTimelineDAO { get; set; }

        /// <summary>
        /// Find DepartmentStockOut object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentStockOut</param>
        /// <returns></returns>
        public DepartmentStockOut FindById(object id)
        {
            return DepartmentStockOutDAO.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentStockOut to database.
        /// </summary>
        /// <param name="stockOut"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentStockOut Add(DepartmentStockOut stockOut)
        {
            stockOut.CreateDate = DateTime.Now;
            stockOut.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            stockOut.UpdateDate = DateTime.Now;
            stockOut.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            stockOut.DelFlg = 0;
            stockOut.StockOutDate = DateTime.Now;
            long maxStockOutId = this.FindMaxId();
            maxStockOutId = maxStockOutId + 1;

            stockOut.DepartmentStockOutPK = new DepartmentStockOutPK();
            stockOut.DepartmentStockOutPK.StockOutId = maxStockOutId;
            stockOut.DepartmentStockOutPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
            
            DepartmentStockOutDAO.Add(stockOut);
            var maxStockOutDetailIdStr = DepartmentStockOutDetailDAO.SelectSpecificType(null, Projections.Max("DepartmentStockOutDetailPK.StockOutDetailId"));
            long maxStockOutDetailId = maxStockOutDetailIdStr != null ? Int64.Parse(maxStockOutDetailIdStr.ToString()) : 0;
            maxStockOutDetailId = maxStockOutDetailId + 1;
            IList productIds = new ArrayList();
            foreach (DepartmentStockOutDetail stockOutDetail in stockOut.DepartmentStockOutDetails)
            {
                productIds.Add(stockOutDetail.Product.ProductId);
            }
            
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddEqCriteria("DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            criteria.AddSearchInCriteria("Product.ProductId", productIds);
            IList stockList = DepartmentStockDAO.FindAll(criteria);
            foreach (DepartmentStockOutDetail stockOutDetail in stockOut.DepartmentStockOutDetails)
            {
                // check number
                var objectCriteria = new ObjectCriteria();
                objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                objectCriteria.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId);
                DepartmentStock stock = GetStock(stockOutDetail.Product.ProductId, stockList);
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.UnconfirmQuantity = 0;
                if (stockOutDetail.DefectStatus != null) 
                {
                    // xuất qua cửa hàng khác
                    if (stockOutDetail.DefectStatus.DefectStatusId == 7)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stock.GoodQuantity -= stockOutDetail.GoodQuantity;

                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    // xuất tạm để sửa
                    else if (stockOutDetail.DefectStatus.DefectStatusId == 4)
                    {
                        stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;

                        //stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.GoodQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;

                        // check whether it's has temp stockout enough ?


                        long totaltempErrorStockOut = 0;
                        long totalReStockCount = 0;

                        ObjectCriteria crit = new ObjectCriteria();
                        crit.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId)
                            .AddEqCriteria("DefectStatus.DefectStatusId", (long)4)
                            .AddEqCriteria("DelFlg", (long)0);
                        IList tempStockedOutList = DepartmentStockOutDetailDAO.FindAll(crit);
                        if (tempStockedOutList != null)
                        {

                            foreach (DepartmentStockOutDetail outDetail in tempStockedOutList)
                            {
                                totaltempErrorStockOut += outDetail.Quantity;
                            }

                        }

                        IList reStockList = DepartmentStockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                        if (reStockList != null)
                        {
                            foreach (DepartmentStockInDetail stockInDetail in reStockList)
                            {
                                totalReStockCount += stockInDetail.Quantity;
                            }
                        }
                        totaltempErrorStockOut = totaltempErrorStockOut - totalReStockCount;
                        if (stockOutDetail.ErrorQuantity > stock.ErrorQuantity - totaltempErrorStockOut)
                        {
                            throw new BusinessException("Lỗi: Mặt hàng " + stockOutDetail.Product.ProductFullName + ", mã vạch "
                                           + stockOutDetail.Product.ProductId + " có tồn " + stock.ErrorQuantity + ", đã xuất tạm " + totaltempErrorStockOut +
                                           ", và đang xuất " + stockOutDetail.ErrorQuantity);
                        }
                    }
                    // xuất trả về kho chính
                    else if (stockOutDetail.DefectStatus.DefectStatusId == 6)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                        stock.GoodQuantity -= stockOutDetail.GoodQuantity;

                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                    }
                    // xuất hàng mẫu
                    else if (stockOutDetail.DefectStatus.DefectStatusId == 9) 
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        //stock.Quantity -= stockOutDetail.Quantity;
                        //stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                        //stock.GoodQuantity -= stockOutDetail.GoodQuantity;
                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                    }
                }

                if (stockOutDetail.DefectStatus.DefectStatusId != 9)
                {
                    // if does not allow negative export then check quantity in stock.
                    if (!ClientSetting.NegativeExport)
                    {
                        if (stock.Quantity < 0 || stock.GoodQuantity < 0)
                        {
                            throw new BusinessException("Hang trong kho khong du de xuat");
                        }
                    }
                }
                    ClientUtility.Log(logger, stock.ProductId + " remains quantity is " + stock.Quantity);
                    stock.UpdateDate = DateTime.Now;
                    stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

                    DepartmentStockDAO.Update(stock);

                stockOutDetail.DepartmentStockOut = stockOut;
                stockOutDetail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK();
                stockOutDetail.DepartmentStockOutDetailPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
                stockOutDetail.DepartmentStockOutDetailPK.StockOutDetailId = maxStockOutDetailId++;
                stockOutDetail.DepartmentId = CurrentDepartment.Get().DepartmentId;
                stockOutDetail.StockOutId = stockOut.DepartmentStockOutPK.StockOutId;
                stockOutDetail.ProductMaster = stockOutDetail.Product.ProductMaster;
                DepartmentStockOutDetailDAO.Add(stockOutDetail);
            }
            return stockOut;
        }

        [Transaction(ReadOnly = false)]
        private DepartmentStockHistory GetErrorCount(string id, IList list)
        {
            foreach (DepartmentStockHistory stockDefect in list)
            {
                if (stockDefect.Product.ProductId == id)
                {
                    return stockDefect;
                }
            }
            return null;
        }
        [Transaction(ReadOnly = false)]
        private DepartmentStock GetStock(string id, IList list)
        {
            foreach (DepartmentStock stock in list)
            {
                if (stock.Product.ProductId == id)
                {
                    return stock;
                }
            }
            return null;
        }

        [Transaction(ReadOnly = false)]
        public SyncFromDepartmentToMain GetSyncData(bool IsSubmitPeriod,DateTime lastSyncTime)
        {
            // Save the end of period
            // select 5 day nearest
            

        var sync = new SyncFromDepartmentToMain();

            // find unconfirm department stock
            ObjectCriteria deptTempCrit = new ObjectCriteria();
            //deptTempCrit.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);

            IList deptTempList = DepartmentStockTempDAO.FindAll(null);
            sync.DepartmentStockTempList = deptTempList;

            // find timeline
            IList departmentTimelineList = DepartmentTimelineDAO.FindAll(null);
            
            // find po
            IList poList = PurchaseOrderDAO.FindAll(null);

            // find po detail
            foreach (PurchaseOrder po in poList)
            {
                var criteria = new ObjectCriteria();
                criteria.AddEqCriteria("PurchaseOrderDetailPK.PurchaseOrderId", po.PurchaseOrderPK.PurchaseOrderId);
                po.PurchaseOrderDetails = PurchaseOrderDetailDAO.FindAll(criteria);
            }

            // find stock history
            ObjectCriteria incrementalCrit = new ObjectCriteria();
            incrementalCrit.AddGreaterOrEqualsCriteria("CreateDate", lastSyncTime);
            IList stockHistoryList = DepartmentStockHistoryDAO.FindAll(incrementalCrit);

            // find dept stock out
            IList deptStockOutList = DepartmentStockOutDAO.FindAll(incrementalCrit);

            // find dept stock out detail
            foreach (DepartmentStockOut so in deptStockOutList)
            {
                var criteria = new ObjectCriteria();
                criteria.AddEqCriteria("DepartmentStockOut.DepartmentStockOutPK.StockOutId", so.DepartmentStockOutPK.StockOutId);
                so.DepartmentStockOutDetails = DepartmentStockOutDetailDAO.FindAll(criteria);
            }

            // find dept stock
            IList deptStock = DepartmentStockDAO.FindAll(null);

            // find return PO
            IList returnPoList = ReturnPoDAO.FindAll(null);

            // dept restock in
            /*var crit = new ObjectCriteria();
            crit.AddEqCriteria("StockInType", (long) 1);*/
            //IList deptReStockInList = DepartmentStockInDAO.FindAll(crit);
            IList deptStockInList = DepartmentStockInDAO.FindAll(incrementalCrit);
            foreach (DepartmentStockIn si in deptStockInList)
            {
                var criteria = new ObjectCriteria();
                criteria.AddEqCriteria("DepartmentStockIn.DepartmentStockInPK.StockInId", si.DepartmentStockInPK.StockInId);
                si.DepartmentStockInDetails = DepartmentStockInDetailDAO.FindAll(criteria);
            }

            sync.DepartmentStockOutList = deptStockOutList;
            sync.DepartmentStockHistoryList = stockHistoryList;
            sync.DepartmentStockList = deptStock;
            sync.PurchaseOrderList = poList;
            sync.ReturnPoList = returnPoList;
            sync.DepartmentStockInList = deptStockInList;
            sync.DepartmentTimelineList = departmentTimelineList;

            // get money info
            sync.EmployeeMoneyList = EmployeeMoneyDAO.FindAll(null);
            sync.DepartmentCostList = DepartmentCostDAO.FindAll(null);
            // end period confirmation
            //DepartmentTimeline timeline = new DepartmentTimeline();

            return sync;
        }

        [Transaction(ReadOnly = false)]
        public void SyncToMain(SyncFromDepartmentToMain sync)
        {
            // insert department stock temp
            if(sync.DepartmentStockTempList!=null && sync.DepartmentStockTempList.Count > 0)
            {
                foreach (DepartmentStockTemp departmentStockTemp in sync.DepartmentStockTempList)
                {
                    bool failed = false;
                    if (departmentStockTemp != null)
                    {
                        DepartmentStockTemp curDeptTemp = DepartmentStockTempDAO.FindById(departmentStockTemp.DepartmentStockTempPK);
                        if (curDeptTemp == null)
                        {
                            DepartmentStockTempDAO.Add(departmentStockTemp);
                        }
                        else
                        {
                            if(departmentStockTemp.Fixed == 1 && departmentStockTemp.DelFlg == 1)
                            {
                                curDeptTemp.Fixed = 1;
                                curDeptTemp.DelFlg = 1;
                                DepartmentStockTempDAO.Update(curDeptTemp);
                            }
                        }
                    }
                }
            }
            // insert timeline
            if (sync.DepartmentTimelineList != null && sync.DepartmentTimelineList.Count > 0)
            {
                foreach (DepartmentTimeline timeline in sync.DepartmentTimelineList)
                {
                    bool failed = false;
                    if (timeline != null)
                    {
                        DepartmentTimeline curTimeline = DepartmentTimelineDAO.FindById(timeline.DepartmentTimelinePK);
                        if (curTimeline == null)
                        {
                            DepartmentTimelineDAO.Add(timeline);
                        }
                        else
                        {
                            // error
                            //failed = true;
                        }
                    }
                }
            }

            // insert PO, PO Detail
            if (sync.PurchaseOrderList != null && sync.PurchaseOrderList.Count > 0)
            {
                foreach (PurchaseOrder po in sync.PurchaseOrderList)
                {
                    PurchaseOrder poTemp = PurchaseOrderDAO.FindById(po.PurchaseOrderPK);
                    if (poTemp == null)
                    {
                        PurchaseOrderDAO.Add(po);
                        foreach (PurchaseOrderDetail detail in po.PurchaseOrderDetails)
                        {
                            PurchaseOrderDetailDAO.Add(detail);
                        }
                    }
                    else
                    {
                        // Error
                        //failed = true;
                    }
                }
            }

            // insert dept stock out
            if (sync.DepartmentStockOutList != null && sync.DepartmentStockOutList.Count > 0)
            {
                IList productIds = new ArrayList();
                foreach (DepartmentStockOut po in sync.DepartmentStockOutList)
                {
                    if (po.DefectStatus.DefectStatusId == StockDefectStatus.SEND_BACK_TO_MAIN)
                    {
                        foreach (DepartmentStockOutDetail detail in po.DepartmentStockOutDetails)
                        {
                            if (NotInList(productIds, detail.Product.ProductId))
                            {
                                productIds.Add(detail.Product.ProductId);
                            }
                        }
                    }
                }
                IList stockList = new ArrayList();
                ObjectCriteria criteria = new ObjectCriteria();
                criteria.AddSearchInCriteria("Product.ProductId", productIds);
                stockList = StockDAO.FindAll(criteria);

                StockOutMapper mapper = new StockOutMapper();
                StockOutDetailMapper detailMapper = new StockOutDetailMapper();
                DeptRetProdStockInMapper drpsiMapper = new DeptRetProdStockInMapper();
                DeptRetProdStockInDetailMapper drpsiDetMapper = new DeptRetProdStockInDetailMapper();

                // save to main.
                string dateStr = DateTime.Now.ToString("yyMMdd");
                var SICriteria = new ObjectCriteria();
                var maxId = StockInDAO.SelectSpecificType(SICriteria, Projections.Max("StockInId"));
                var stockInId = maxId == null ? dateStr + "00001" : string.Format("{0:00000000000}", (Int64.Parse(maxId.ToString()) + 1));


                foreach (DepartmentStockOut po in sync.DepartmentStockOutList)
                {
                    
                    /*if (po.DefectStatus.DefectStatusId == StockDefectStatus.SEND_BACK_TO_MAIN)
                    {
                        foreach (DepartmentStockOutDetail detail in po.DepartmentStockOutDetails)
                        {
                            productIds.Add(detail.Product.ProductId);
                        }
                    }*/
                    DepartmentStockOut poTemp = DepartmentStockOutDAO.FindById(po.DepartmentStockOutPK);
                    if (poTemp == null)
                    {
                        // ++ Add code for add an empty stock in : 20090906

                        po.StockOutDate = DateTime.Now;
                        DepartmentStockOutDAO.Add(po);
                        
                        
                        StockIn stockIn = drpsiMapper.Convert(po);
                        stockIn.StockInDate = DateTime.Now;
                        if (po.DefectStatus.DefectStatusId == StockDefectStatus.SEND_BACK_TO_MAIN)
                        {
                            stockIn.StockInType = 3; // stock in for stock out to manufacturers    
                        }
                        stockIn.StockInDetails = new ArrayList();
                        
                        foreach (DepartmentStockOutDetail detail in po.DepartmentStockOutDetails)
                        {
                            DepartmentStockOutDetailDAO.Add(detail);

                            // update stock if it's a dept stock out return to main ( defectstatus = 6 )
                            if (po.DefectStatus.DefectStatusId == StockDefectStatus.SEND_BACK_TO_MAIN 
                                && stockList!=null
                                && stockList.Count > 0)
                            {
                                Stock stock = GetStockOfProduct(detail.Product,stockList);
                                if(stock!=null)
                                {
                                    stock.Quantity += detail.GoodQuantity + detail.ErrorQuantity;
                                    stock.GoodQuantity += detail.GoodQuantity;
                                    stock.ErrorQuantity += detail.ErrorQuantity;
                                    StockDAO.Update(stock);
                                }

                                StockInDetail detailStockIn = drpsiDetMapper.Convert(detail);
                                stockIn.StockInDetails.Add(detailStockIn);
                            }
                        }
                        if(stockIn.StockInDetails != null && stockIn.StockInDetails.Count > 0)
                        {
                            
                            // save to main.
                            stockIn.StockInId = stockInId;
                            StockInDAO.Add(stockIn);

                            foreach (StockInDetail stockInDetail in stockIn.StockInDetails)
                            {
                                // add dept stock in
                                var detailPK = new StockInDetailPK { ProductId = stockInDetail.Product.ProductId, StockInId = stockInId };
                                stockInDetail.StockInDetailPK = detailPK;
                                StockInDetailDAO.Add(stockInDetail);
                            }
                            // create new stockInId
                            stockInId = string.Format("{0:00000000000}", (Int64.Parse(stockInId.ToString()) + 1));
                        }
                        // -- Add code for add an empty stock in : 20090906
                    }
                    else
                    {
                        // Error
                        //failed = true;
                    }
                }
            }

            // insert Dept stock in
            if (sync.DepartmentStockInList != null && sync.DepartmentStockInList.Count > 0)
            {
                foreach (DepartmentStockIn po in sync.DepartmentStockInList)
                {
                    DepartmentStockIn poTemp = DepartmentStockInDAO.FindById(po.DepartmentStockInPK);
                    if (poTemp == null)
                    {
                        DepartmentStockInDAO.Add(po);
                        foreach (DepartmentStockInDetail detail in po.DepartmentStockInDetails)
                        {
                            DepartmentStockInDetailDAO.Add(detail);
                        }
                    }
                    else
                    {
                        // Error
                    }
                }
            }

            // insert return po
            if (sync.ReturnPoList != null && sync.ReturnPoList.Count > 0)
            {
                foreach (ReturnPo po in sync.ReturnPoList)
                {
                    ReturnPo poTemp = ReturnPoDAO.FindById(po.ReturnPoPK);
                    if (poTemp == null)
                    {
                        ReturnPoDAO.Add(po);
                    }
                    else
                    {
                        // Error
                    }
                }
            }

            // insert stock history
            if (sync.DepartmentStockHistoryList != null && sync.DepartmentStockHistoryList.Count > 0)
            {
                foreach (DepartmentStockHistory po in sync.DepartmentStockHistoryList)
                {
                    DepartmentStockHistory poTemp = DepartmentStockHistoryDAO.FindById(po.DepartmentStockHistoryPK);
                    if (poTemp == null)
                    {
                        DepartmentStockHistoryDAO.Add(po);
                    }
                    else
                    {
                        // Error
                    }
                }
            }

            // insert or update dept stock
            if (sync.DepartmentStockList != null && sync.DepartmentStockList.Count > 0)
            {
                /*if (!failed)
                {*/
                    foreach (DepartmentStock po in sync.DepartmentStockList)
                    {

                        try
                        {
                            DepartmentStock poTemp = DepartmentStockDAO.FindById(po.DepartmentStockPK);
                            if (poTemp == null)
                            {
                                DepartmentStockDAO.Add(po);
                            }
                            else
                            {
                                poTemp.Quantity = po.Quantity;
                                poTemp.GoodQuantity = po.GoodQuantity;
                                poTemp.LostQuantity = po.LostQuantity;
                                poTemp.ErrorQuantity = po.ErrorQuantity;
                                poTemp.DamageQuantity = po.DamageQuantity;
                                poTemp.OldDamageQuantity = po.OldDamageQuantity;
                                poTemp.OldErrorQuantity = po.OldErrorQuantity;
                                poTemp.OldGoodQuantity = po.OldGoodQuantity;
                                poTemp.OldLostQuantity = po.OldLostQuantity;
                                poTemp.OldUnconfirmQuantity = po.OldUnconfirmQuantity;
                                poTemp.UnconfirmQuantity = po.UnconfirmQuantity;
                                poTemp.UpdateDate = DateTime.Now;
                                DepartmentStockDAO.Update(poTemp);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                /*}*/
            }

            // sync dept cost
            if (sync.DepartmentCostList != null && sync.DepartmentCostList.Count > 0)
            {
                foreach (DepartmentCost departmentCost in sync.DepartmentCostList)
                {
                    DepartmentCost existDeptCost = DepartmentCostDAO.FindById(departmentCost.DepartmentCostPK);
                    if (existDeptCost == null)
                    {
                        DepartmentCostDAO.Add(departmentCost);
                    }
                }
            }

            // sync money
            if(sync.EmployeeMoneyList != null && sync.EmployeeMoneyList.Count > 0)
            {
                foreach (EmployeeMoney employeeMoney in sync.EmployeeMoneyList)
                {
                    EmployeeMoney existEmpMoney = EmployeeMoneyDAO.FindById(employeeMoney.EmployeeMoneyPK);
                    if (existEmpMoney == null)
                    {
                        EmployeeMoneyDAO.Add(employeeMoney);
                    }
                }
            }
        }

        private bool NotInList(IList list, string id)
        {
            foreach (string s in list)
            {
                if(s.Equals(id))
                {
                    return false;
                }
            }
            return true;
        }

        private Stock GetStockOfProduct(Product product, IList stockList)
        {
            foreach (Stock stock in stockList)
            {
                if(stock.Product.ProductId == product.ProductId)
                {
                    return stock;
                }
            }
            return null;
        }

        private long CalculateQuantitiesWhichStockedOut(IList list)
        {
            long result = 0;
            if (list == null)
            {
                return 0;
            }
            foreach (DepartmentStockOutDetail detail in list)
            {
                result += detail.Quantity;
            }
            return result;
        }
        /// <summary>
        /// Update DepartmentStockOut to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentStockOut data)
        {
            DepartmentStockOutDAO.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentStockOut from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentStockOut data)
        {
            DepartmentStockOutDAO.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentStockOut from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentStockOutDAO.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentStockOut from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList FindAll(ObjectCriteria criteria)
        {
            return DepartmentStockOutDAO.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentStockOut from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return DepartmentStockOutDAO.FindPaging(criteria);
        }

        #region IDepartmentStockOutLogic Members


        public long FindMaxId()
        {
            object maxId = DepartmentStockOutDAO.SelectSpecificType(null, Projections.Max("DepartmentStockOutPK.StockOutId"));
            return maxId != null ? (long)maxId : 0;
        }

        #endregion

        #region IDepartmentStockOutLogic Members


        public IList FindStockOut(System.DateTime date, System.DateTime toDate)
        {
            return DepartmentStockOutDAO.FindStockOut(date, toDate);
        }

        #endregion

        #region IDepartmentStockOutLogic Members

        [Transaction(ReadOnly = false)]
        public void ProcessErrorGoods(IList stockList, IList returnGoodsList, IList tempStockOutList, IList destroyGoodsList)
        {

            var maxStockOutDetailIdStr = DepartmentStockOutDAO.SelectSpecificType(null, Projections.Max("DepartmentStockOutPK.StockOutId"));
            long maxId = maxStockOutDetailIdStr != null ? Int64.Parse(maxStockOutDetailIdStr.ToString()) : 0;
            maxId += 1;

            object maxDetailObj = DepartmentStockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
            long maxDetailId = maxDetailObj != null ? (long)maxDetailObj : 0;
            maxDetailId += 1;

            DepartmentStockOut destroytSO = new DepartmentStockOut();
            destroytSO.CreateDate = DateTime.Now;
            destroytSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            destroytSO.UpdateDate = DateTime.Now;
            destroytSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            destroytSO.StockOutDate = DateTime.Now;
            destroytSO.DefectStatus = new StockDefectStatus { DefectStatusId = 8, DefectStatusName = " Huỷ hàng hư và mất" };

            destroytSO.DepartmentStockOutPK = new DepartmentStockOutPK();
            destroytSO.DepartmentStockOutPK.StockOutId = maxId++;
            destroytSO.DepartmentStockOutPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
            destroytSO.ExclusiveKey = 1;
            if (destroyGoodsList.Count > 0)
            {
                foreach (DepartmentStockOutDetail stockOutDetail in destroyGoodsList)
                {

                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.DepartmentStockOut = destroytSO;
                    stockOutDetail.StockOutId = destroytSO.DepartmentStockOutPK.StockOutId;
                    stockOutDetail.DepartmentId = destroytSO.DepartmentStockOutPK.DepartmentId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Huỷ hàng hư và mất";

                    // update defect
                    // update quantity of stock
                    DepartmentStock defect = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (defect == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                    }
                    defect.LostQuantity -= stockOutDetail.LostQuantity;
                    defect.DamageQuantity -= stockOutDetail.DamageQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;

                    DepartmentStockDAO.Update(defect);
                }
                destroytSO.DepartmentStockOutDetails = destroyGoodsList;
                DepartmentStockOutDAO.Add(destroytSO);


                foreach (DepartmentStockOutDetail detail in destroyGoodsList)
                {
                    detail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK();
                    detail.DepartmentStockOutDetailPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
                    detail.DepartmentStockOutDetailPK.StockOutDetailId = maxDetailId++;
                    DepartmentStockOutDetailDAO.Add(detail);
                }
            }

            // -------------- return to main stock ------------
            DepartmentStockOut returnSO = new DepartmentStockOut();
            returnSO.CreateDate = DateTime.Now;
            returnSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            returnSO.UpdateDate = DateTime.Now;
            returnSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            returnSO.StockOutDate = DateTime.Now;
            returnSO.DefectStatus = new StockDefectStatus { DefectStatusId = 6 }; // trả về kho chính
            returnSO.DepartmentStockOutPK = new DepartmentStockOutPK();
            returnSO.DepartmentStockOutPK.StockOutId = maxId++;
            returnSO.DepartmentStockOutPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
            returnSO.ExclusiveKey = 1;

            if (returnGoodsList.Count > 0)
            {
                foreach (DepartmentStockOutDetail stockOutDetail in returnGoodsList)
                {
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.DepartmentStockOut = returnSO;
                    stockOutDetail.StockOutId = returnSO.DepartmentStockOutPK.StockOutId;
                    stockOutDetail.DepartmentId = returnSO.DepartmentStockOutPK.DepartmentId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Trả về cho kho chính";

                    DepartmentStock defect = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (defect == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                    }
                    defect.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;
                    DepartmentStockDAO.Update(defect);
                }
                returnSO.DepartmentStockOutDetails = returnGoodsList;
                DepartmentStockOutDAO.Add(returnSO);

                //maxDetailId += 1;
                foreach (DepartmentStockOutDetail detail in returnGoodsList)
                {
                    detail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK();
                    detail.DepartmentStockOutDetailPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
                    detail.DepartmentStockOutDetailPK.StockOutDetailId = maxDetailId++;
                    DepartmentStockOutDetailDAO.Add(detail);
                }
            }
            // -------------- temporary stock out
            DepartmentStockOut tempSO = new DepartmentStockOut();
            tempSO.CreateDate = DateTime.Now;
            tempSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            tempSO.UpdateDate = DateTime.Now;
            tempSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            tempSO.StockOutDate = DateTime.Now;
            tempSO.DefectStatus = new StockDefectStatus { DefectStatusId = 4 };
            tempSO.DepartmentStockOutPK = new DepartmentStockOutPK();
            tempSO.DepartmentStockOutPK.StockOutId = maxId++;
            tempSO.DepartmentStockOutPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
            tempSO.ExclusiveKey = 1;
            if (tempStockOutList.Count > 0)
            {
                foreach (DepartmentStockOutDetail stockOutDetail in tempStockOutList)
                {
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.DepartmentStockOut = tempSO;
                    stockOutDetail.StockOutId = tempSO.DepartmentStockOutPK.StockOutId;
                    stockOutDetail.DepartmentId = tempSO.DepartmentStockOutPK.DepartmentId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Trả hàng cho nhà sản xuất";

                    DepartmentStock stock = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (stock == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                    }

                    // check whether it's has temp stockout enough ?


                    long totaltempErrorStockOut = 0;
                    long totalReStockCount = 0;

                    ObjectCriteria crit = new ObjectCriteria();
                    crit.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId)
                        .AddEqCriteria("DefectStatus.DefectStatusId", (long)4)
                        .AddEqCriteria("DelFlg", (long)0);
                    IList tempStockedOutList = DepartmentStockOutDetailDAO.FindAll(crit);
                    if (tempStockedOutList != null)
                    {

                        foreach (DepartmentStockOutDetail outDetail in tempStockedOutList)
                        {
                            totaltempErrorStockOut += outDetail.Quantity;
                        }

                    }

                    IList reStockList = DepartmentStockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                    if (reStockList != null)
                    {
                        foreach (DepartmentStockInDetail stockInDetail in reStockList)
                        {
                            totalReStockCount += stockInDetail.Quantity;
                        }
                    }
                    totaltempErrorStockOut = totaltempErrorStockOut - totalReStockCount;
                    if (stockOutDetail.ErrorQuantity > stock.ErrorQuantity - totaltempErrorStockOut)
                    {
                        throw new BusinessException("Lỗi: Mặt hàng " + stockOutDetail.Product.ProductFullName + ", mã vạch "
                                       + stockOutDetail.Product.ProductId + " có tồn " + stock.ErrorQuantity + ", đã xuất tạm " + totaltempErrorStockOut +
                                       ", và đang xuất " + stockOutDetail.ErrorQuantity);
                    }
                    /*defect.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;
                    DepartmentStockDAO.Update(defect);*/
                }
                tempSO.DepartmentStockOutDetails = tempStockOutList;
                DepartmentStockOutDAO.Add(tempSO);

                //maxDetailId += 1;
                foreach (DepartmentStockOutDetail detail in tempStockOutList)
                {
                    detail.DepartmentStockOutDetailPK = new DepartmentStockOutDetailPK();
                    detail.DepartmentStockOutDetailPK.DepartmentId = CurrentDepartment.Get().DepartmentId;
                    detail.DepartmentStockOutDetailPK.StockOutDetailId = maxDetailId++;
                    DepartmentStockOutDetailDAO.Add(detail);
                }
            }
        }

        private DepartmentStock GetDefectFromStockOut(DepartmentStockOutDetail detail, IList list)
        {
            foreach (DepartmentStock stockDefect in list)
            {
                if (stockDefect.Product.ProductId == detail.Product.ProductId)
                {
                    return stockDefect;
                }
            }
            return null;
        }

        #endregion
    }
}
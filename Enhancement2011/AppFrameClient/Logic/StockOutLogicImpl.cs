using System;
using System.Collections;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Exceptions;
using AppFrameClient.Common;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;
using AppFrame.Model;
using AppFrame.DataLayer;

namespace AppFrame.Logic
{
    public class StockOutLogicImpl : IStockOutLogic
    {
        public IStockOutDAO StockOutDAO { get; set; }
        public IStockOutDetailDAO StockOutDetailDAO { get; set; }
        public IStockInDetailDAO StockInDetailDAO { get; set; }        
        public IStockDAO StockDAO { get; set; }

        /// <summary>
        /// Find StockOut object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of StockOut</param>
        /// <returns></returns>
        public StockOut FindById(object id)
        {
            return StockOutDAO.FindById(id);
        }
        
        /// <summary>
        /// Add StockOut to database.
        /// </summary>
        /// <param name="stockOut"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public StockOut Add(StockOut stockOut)
        {
            stockOut.CreateDate = DateTime.Now;
            stockOut.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            stockOut.UpdateDate = DateTime.Now;
            stockOut.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            stockOut.DelFlg = 0;
            stockOut.StockOutDate = DateTime.Now;
            long maxStockOutId = this.FindMaxId();
            maxStockOutId = maxStockOutId + 1;

            stockOut.StockoutId = maxStockOutId;
            StockOutDAO.Add(stockOut);
            var maxStockOutDetailIdStr = StockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
            long maxStockOutDetailId = maxStockOutDetailIdStr != null ? Int64.Parse(maxStockOutDetailIdStr.ToString()) : 0;
            maxStockOutDetailId = maxStockOutDetailId + 1;
            IList productIds = new ArrayList();
            foreach (StockOutDetail stockOutDetail in stockOut.StockOutDetails)
            {
                productIds.Add(stockOutDetail.Product.ProductId);
            }
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddSearchInCriteria("Product.ProductId", productIds);
            
            IList stockList = StockDAO.FindAll(criteria);
            foreach (StockOutDetail stockOutDetail in stockOut.StockOutDetails)
            {
                // check number
                var objectCriteria = new ObjectCriteria();
                objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                objectCriteria.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId);

                Stock stock = GetStock(stockOutDetail.Product.ProductId, stockList);
                if (stock == null)
                {
                    throw new BusinessException("Mặt hàng " + stockOutDetail.Product.ProductId + ", " + stockOutDetail.Product.ProductFullName + " không có trong kho");
                }
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.UnconfirmQuantity = 0;
                // xuất ra cửa hàng khác
                if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 7)
                {
                    stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                    stock.Quantity -= stockOutDetail.Quantity;

                    stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;

                }
                // xuất tạm
                else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 4)
                {
                    // check whether it's has temp stockout enough ?
                    

                    long totaltempErrorStockOut = 0;
                    long totalReStockCount = 0;

                    ObjectCriteria crit = new ObjectCriteria();
                    crit.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId)
                        .AddEqCriteria("DefectStatus.DefectStatusId", (long)4)
                        .AddEqCriteria("DelFlg", (long)0);
                    IList tempStockedOutList = StockOutDetailDAO.FindAll(crit);
                    if(tempStockedOutList!=null)
                    {
                        
                        foreach (StockOutDetail outDetail in tempStockedOutList)
                        {
                            totaltempErrorStockOut += outDetail.Quantity;
                        }
                        
                    }

                    IList reStockList = StockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                    if (reStockList != null)
                    {
                        foreach (StockInDetail stockInDetail in reStockList)
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

                    // update quantity
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    //stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                }
                // xuất trả về cho nhà sản xuất
                else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 5)
                {
                    stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;
                    stock.Quantity -= stockOutDetail.Quantity;
                    stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    stockOutDetail.UnconfirmQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                }
                // xuất hàng mẫu 
                else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 9)
                {
                    stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                    //stock.Quantity -= stockOutDetail.Quantity;
                    //stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                }
                // if need to update main stock
                if (!stockOut.NotUpdateMainStock)
                {
                    stock.GoodQuantity -= stockOutDetail.GoodQuantity;
                    stock.GoodQuantity = Math.Max(0, stock.GoodQuantity);

                    stock.UpdateDate = DateTime.Now;
                    stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    StockDAO.Update(stock);
                }



                stockOutDetail.StockOut = stockOut;
                stockOutDetail.StockOutDetailId = maxStockOutDetailId++;
                stockOutDetail.StockOutId = stockOut.StockoutId;
                stockOutDetail.ProductMaster = stockOutDetail.Product.ProductMaster;
                StockOutDetailDAO.Add(stockOutDetail);
            }
            return stockOut;
        }

        private Stock GetErrorCount(string id, IList list)
        {
            foreach (Stock stockDefect in list)
            {
                if (stockDefect.Product.ProductId == id)
                {
                    return stockDefect;
                }
            }
            return null;
        }

        private Stock GetStock(string id, IList list)
        {
            foreach (Stock stockDefect in list)
            {
                if (stockDefect.Product.ProductId == id)
                {
                    return stockDefect;
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
            foreach (StockOutDetail detail in list)
            {
                result += detail.Quantity;
            }
            return result;
        }

        /// <summary>
        /// Update StockOut to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(StockOut stockOut)
        {
            int listCount = stockOut.StockOutDetails.Count;
            int delCount = 0;            
            if(stockOut.ConfirmFlg == 1)
            {
                stockOut.NotUpdateMainStock = true;
            }
            var maxStockOutDetailIdStr = StockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
            long maxStockOutDetailId = maxStockOutDetailIdStr != null ? Int64.Parse(maxStockOutDetailIdStr.ToString()) : 0;
            maxStockOutDetailId = maxStockOutDetailId + 1;
            IList productIds = new ArrayList();
            foreach (StockOutDetail stockOutDetail in stockOut.StockOutDetails)
            {
                productIds.Add(stockOutDetail.Product.ProductId);
            }
            var criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddSearchInCriteria("Product.ProductId", productIds);

            IList stockList = StockDAO.FindAll(criteria);
            foreach (StockOutDetail stockOutDetail in stockOut.StockOutDetails)
            {
                if (stockOutDetail.StockOutDetailId != 0)
                {
                    // if delete then delete it
                    if(stockOutDetail.DelFlg == 1)
                    {
                        StockOutDetailDAO.Delete(stockOutDetail);
                        delCount++;
                        continue;
                    }
                    // check number
                    var objectCriteria = new ObjectCriteria();
                    objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                    objectCriteria.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId);

                    Stock stock = GetStock(stockOutDetail.Product.ProductId, stockList);
                    if (stock == null)
                    {
                        throw new BusinessException("Mặt hàng " + stockOutDetail.Product.ProductId + ", " +
                                                    stockOutDetail.Product.ProductFullName + " không có trong kho");
                    }

                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;

                    // xuất ra cửa hàng khác
                    if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 7)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;

                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;

                    }// xuất tạm
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 4)
                    {
                        // check whether it's has temp stockout enough ?


                        long totaltempErrorStockOut = 0;
                        long totalReStockCount = 0;

                        ObjectCriteria crit = new ObjectCriteria();
                        crit.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId)
                            .AddEqCriteria("DefectStatus.DefectStatusId", (long) 4)
                            .AddEqCriteria("DelFlg", (long) 0);
                        IList tempStockedOutList = StockOutDetailDAO.FindAll(crit);
                        if (tempStockedOutList != null)
                        {

                            foreach (StockOutDetail outDetail in tempStockedOutList)
                            {
                                totaltempErrorStockOut += outDetail.Quantity;
                            }

                        }

                        IList reStockList = StockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                        if (reStockList != null)
                        {
                            foreach (StockInDetail stockInDetail in reStockList)
                            {
                                totalReStockCount += stockInDetail.Quantity;
                            }
                        }
                        totaltempErrorStockOut = totaltempErrorStockOut - totalReStockCount;
                        if (stockOutDetail.ErrorQuantity > stock.ErrorQuantity - totaltempErrorStockOut)
                        {
                            throw new BusinessException("Lỗi: Mặt hàng " + stockOutDetail.Product.ProductFullName +
                                                        ", mã vạch "
                                                        + stockOutDetail.Product.ProductId + " có tồn " +
                                                        stock.ErrorQuantity + ", đã xuất tạm " + totaltempErrorStockOut +
                                                        ", và đang xuất " + stockOutDetail.ErrorQuantity);
                        }

                        // update quantity
                        stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                        //stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.GoodQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                    }
                        // xuất trả về cho nhà sản xuất
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 5)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                        // xuất hàng mẫu 
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 9)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    else // xuat hang binh thuong
                    {
                        if(stockOutDetail.Quantity > 0 
                            && (    stockOutDetail.GoodQuantity == 0 
                                 && stockOutDetail.ErrorQuantity ==0
                                 && stockOutDetail.DamageQuantity == 0
                                 && stockOutDetail.LostQuantity == 0
                                 && stockOutDetail.UnconfirmQuantity == 0))
                        {
                            stockOutDetail.GoodQuantity = stockOutDetail.Quantity;
                        }
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;

                    }
                    // if need to update main stock
                    if (!stockOut.NotUpdateMainStock)
                    {
                        stock.GoodQuantity -= stockOutDetail.GoodQuantity;
                        stock.UpdateDate = DateTime.Now;
                        stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        StockDAO.Update(stock);

                        // update stock out create date
                        stockOut.CreateDate = DateTime.Now;
                        stockOut.UpdateDate = DateTime.Now;

                        stockOutDetail.CreateDate = DateTime.Now;
                        stockOutDetail.UpdateDate = DateTime.Now;
                    }

                    StockOutDetailDAO.Update(stockOutDetail);
                }
                else
                {
                    // check number
                    var objectCriteria = new ObjectCriteria();
                    objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                    objectCriteria.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId);

                    Stock stock = GetStock(stockOutDetail.Product.ProductId, stockList);
                    if (stock == null)
                    {
                        throw new BusinessException("Mặt hàng " + stockOutDetail.Product.ProductId + ", " +
                                                    stockOutDetail.Product.ProductFullName + " không có trong kho");
                    }
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;
                    // xuất ra cửa hàng khác
                    if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 7)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;

                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;

                    }
                    // xuất tạm
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 4)
                    {
                        // check whether it's has temp stockout enough ?


                        long totaltempErrorStockOut = 0;
                        long totalReStockCount = 0;

                        ObjectCriteria crit = new ObjectCriteria();
                        crit.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId)
                            .AddEqCriteria("DefectStatus.DefectStatusId", (long)4)
                            .AddEqCriteria("DelFlg", (long)0);
                        IList tempStockedOutList = StockOutDetailDAO.FindAll(crit);
                        if (tempStockedOutList != null)
                        {

                            foreach (StockOutDetail outDetail in tempStockedOutList)
                            {
                                totaltempErrorStockOut += outDetail.Quantity;
                            }

                        }

                        IList reStockList = StockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                        if (reStockList != null)
                        {
                            foreach (StockInDetail stockInDetail in reStockList)
                            {
                                totalReStockCount += stockInDetail.Quantity;
                            }
                        }
                        totaltempErrorStockOut = totaltempErrorStockOut - totalReStockCount;
                        if (stockOutDetail.ErrorQuantity > stock.ErrorQuantity - totaltempErrorStockOut)
                        {
                            throw new BusinessException("Lỗi: Mặt hàng " + stockOutDetail.Product.ProductFullName +
                                                        ", mã vạch "
                                                        + stockOutDetail.Product.ProductId + " có tồn " +
                                                        stock.ErrorQuantity + ", đã xuất tạm " + totaltempErrorStockOut +
                                                        ", và đang xuất " + stockOutDetail.ErrorQuantity);
                        }

                        // update quantity
                        stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                        //stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.GoodQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                    }
                    // xuất trả về cho nhà sản xuất
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 5)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    // xuất hàng mẫu 
                    else if (stockOut.DefectStatus != null && stockOut.DefectStatus.DefectStatusId == 9)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        //stock.Quantity -= stockOutDetail.Quantity;
                        //stock.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    // xuất hàng bình thường
                    else
                    {
                        if (stockOutDetail.Quantity > 0
                            && (stockOutDetail.GoodQuantity == 0
                                 && stockOutDetail.ErrorQuantity == 0
                                 && stockOutDetail.DamageQuantity == 0
                                 && stockOutDetail.LostQuantity == 0
                                 && stockOutDetail.UnconfirmQuantity == 0))
                        {
                            stockOutDetail.GoodQuantity = stockOutDetail.Quantity;
                        }
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;
                        stock.Quantity -= stockOutDetail.Quantity;
                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    // if need to update main stock
                    if (!stockOut.NotUpdateMainStock)
                    {
                        stock.GoodQuantity -= stockOutDetail.GoodQuantity;
                        stock.UpdateDate = DateTime.Now;
                        stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        
                        StockDAO.Update(stock);

                        // update create date in order can get it when synchronizing
                        stockOut.CreateDate = DateTime.Now;
                        stockOut.UpdateDate = DateTime.Now;

                        stockOutDetail.CreateDate = DateTime.Now;
                        stockOutDetail.UpdateDate = DateTime.Now;

                    }



                    stockOutDetail.StockOut = stockOut;
                    stockOutDetail.StockOutDetailId = maxStockOutDetailId++;
                    stockOutDetail.StockOutId = stockOut.StockoutId;
                    stockOutDetail.ProductMaster = stockOutDetail.Product.ProductMaster;
                    StockOutDetailDAO.Add(stockOutDetail);
                }
            }

            if(delCount == listCount)
            {
                stockOut.DelFlg = 1;
            }

            if(stockOut.DelFlg!= 1)
            {
                StockOutDAO.Update(stockOut);
            }
            else
            {
                StockOutDAO.Delete(stockOut);
            }
        }
        
        /// <summary>
        /// Delete StockOut from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(StockOut data)
        {
            StockOutDAO.Delete(data);
        }
        
        /// <summary>
        /// Delete StockOut from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            StockOutDAO.DeleteById(id);
        }
        
        /// <summary>
        /// Find all StockOut from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList FindAll(ObjectCriteria criteria)
        {
            return StockOutDAO.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all StockOut from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return StockOutDAO.FindPaging(criteria);
        }

        #region IStockOutLogic Members


        public IList FindByProductMaster(System.DateTime date, System.DateTime toDate)
        {
            return null;//return StockOutDAO.FindByProductMaster(date, toDate);
        }

        #endregion

        #region IStockOutLogic Members


        public IList FindByProductMaster(long id, System.DateTime date, System.DateTime toDate)
        {
            return StockOutDAO.FindByProductMaster(id,date, toDate);
        }

        #endregion

        #region IStockOutLogic Members


        public long FindMaxId()
        {
            object maxId = StockOutDAO.SelectSpecificType(null, Projections.Max("StockoutId"));
            return maxId != null ? (long)maxId : 0;
        }

        #endregion

        #region IStockOutLogic Members


        public IList FindStockOut(System.DateTime date, System.DateTime toDate)
        {
            return StockOutDAO.FindStockOut(date, toDate);
        }

        #endregion

        #region IStockOutLogic Members


        public void ProcessErrorGoods(IList stockList, IList returnGoodsList, IList tempStockOutList, IList destroyGoodsList)
        {
            long maxId = FindMaxId();
            maxId += 1;

            object maxDetailObj = StockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
            long maxDetailId = maxDetailObj != null ? (long)maxDetailObj : 0;
            maxDetailId += 1;

            StockOut destroytSO = new StockOut();
            destroytSO.CreateDate = DateTime.Now;
            destroytSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            destroytSO.UpdateDate = DateTime.Now;
            destroytSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            
            destroytSO.StockOutDate = DateTime.Now;
            destroytSO.DefectStatus = new StockDefectStatus { DefectStatusId = 8, DefectStatusName = " Huỷ hàng hư và mất" };

            destroytSO.StockoutId = maxId++;
            destroytSO.ExclusiveKey = 1;
            if (destroyGoodsList.Count > 0)
            {
                foreach (StockOutDetail stockOutDetail in destroyGoodsList)
                {
                    
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.StockOut = destroytSO;
                    stockOutDetail.StockOutId = destroytSO.StockoutId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Huỷ hàng hư và mất";

                    // update defect
                    // update quantity of stock
                    Stock defect = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (defect == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                    }
                    defect.LostQuantity -= stockOutDetail.LostQuantity;
                    defect.DamageQuantity -= stockOutDetail.DamageQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;
                    
                    StockDAO.Update(defect);
                }
                destroytSO.StockOutDetails = destroyGoodsList;
                StockOutDAO.Add(destroytSO);

                
                foreach (StockOutDetail detail in destroyGoodsList)
                {
                    detail.StockOutDetailId = maxDetailId++;
                    StockOutDetailDAO.Add(detail);
                }
            }
            // -------------- return to manufacturer ------------
            StockOut returnSO = new StockOut();
            returnSO.CreateDate = DateTime.Now;
            returnSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            returnSO.UpdateDate = DateTime.Now;
            returnSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            returnSO.StockOutDate = DateTime.Now;
            returnSO.DefectStatus = new StockDefectStatus { DefectStatusId = 5 };
            returnSO.StockoutId = maxId++;
            returnSO.ExclusiveKey = 1;

            if (returnGoodsList.Count > 0)
            {
                foreach (StockOutDetail stockOutDetail in returnGoodsList)
                {
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.StockOut = returnSO;
                    stockOutDetail.StockOutId = returnSO.StockoutId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Trả hàng cho nhà sản xuất";

                    Stock defect = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (defect == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                    }
                    defect.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;
                    StockDAO.Update(defect);
                }
                returnSO.StockOutDetails = returnGoodsList;
                StockOutDAO.Add(returnSO);

                //maxDetailId += 1;
                foreach (StockOutDetail detail in returnGoodsList)
                {
                    detail.StockOutDetailId = maxDetailId++;
                    StockOutDetailDAO.Add(detail);
                }
            }
            // -------------- temporary stock out
            StockOut tempSO = new StockOut();
            tempSO.CreateDate = DateTime.Now;
            tempSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            tempSO.UpdateDate = DateTime.Now;
            tempSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            tempSO.StockOutDate = DateTime.Now;
            tempSO.DefectStatus = new StockDefectStatus { DefectStatusId = 4 };
            tempSO.StockoutId = maxId++;
            tempSO.ExclusiveKey = 1;
            if (tempStockOutList.Count > 0)
            {
                foreach (StockOutDetail stockOutDetail in tempStockOutList)
                {
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.CreateDate = DateTime.Now;
                    stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.UpdateDate = DateTime.Now;
                    stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockOutDetail.StockOut = tempSO;
                    stockOutDetail.StockOutId = tempSO.StockoutId;
                    stockOutDetail.DelFlg = 0;
                    stockOutDetail.ExclusiveKey = 1;
                    stockOutDetail.Description = " Tạm xuất để sửa hàng";

                    Stock stock = GetDefectFromStockOut(stockOutDetail, stockList);
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
                    IList tempStockedOutList = StockOutDetailDAO.FindAll(crit);
                    if (tempStockedOutList != null)
                    {

                        foreach (StockOutDetail outDetail in tempStockedOutList)
                        {
                            totaltempErrorStockOut += outDetail.Quantity;
                        }

                    }

                    IList reStockList = StockInDetailDAO.FindReStock(stockOutDetail.Product.ProductId);
                    if (reStockList != null)
                    {
                        foreach (StockInDetail stockInDetail in reStockList)
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
                    StockDAO.Update(defect);*/
                }
                tempSO.StockOutDetails = tempStockOutList;
                StockOutDAO.Add(tempSO);

                //maxDetailId += 1;
                foreach (StockOutDetail detail in tempStockOutList)
                {
                    detail.StockOutDetailId = maxDetailId++;
                    StockOutDetailDAO.Add(detail);
                }
            }
        }

        private Stock GetDefectFromStockOut(StockOutDetail detail, IList list)
        {
            foreach (Stock stockDefect in list)
            {
                if(stockDefect.Product.ProductId == detail.Product.ProductId)
                {
                    return stockDefect;
                }
            }
            return null;
        }

        #endregion

        #region IStockOutLogic Members

        [Transaction(ReadOnly = false)]
        public void Add(DepartmentStockIn data)
        {
            string deptStr = string.Format("{0:000}", data.DepartmentId);
            string dateStr = data.StockInDate.ToString("yyMMdd");
            var criteria = new ObjectCriteria();
            criteria.AddGreaterCriteria("StockoutId", (long)0);
            
            var maxId = StockOutDAO.SelectSpecificType(criteria, Projections.Max("StockoutId"));
            var stockOutId = maxId == null ? 1 : Int64.Parse(maxId.ToString()) + 1;
                
            //var stockInPk = new DepartmentStockInPK { DepartmentId = data.DepartmentId, StockInId = stockOutId + "" };

            //data.DepartmentStockInPK = stockInPk;
            data.CreateDate = DateTime.Now;
            data.UpdateDate = DateTime.Now;
            data.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            data.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            
            StockOut stockOut = new StockOut();
            stockOut.StockoutId = stockOutId;
            stockOut.CreateDate = DateTime.Now;
            stockOut.UpdateDate = DateTime.Now;
            stockOut.StockOutDate = DateTime.Now;
            stockOut.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            stockOut.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            stockOut.NotUpdateMainStock = false;
            stockOut.DefectStatus = new StockDefectStatus { DefectStatusId = 0};
            stockOut.DelFlg = 0;
            stockOut.DepartmentId = data.DepartmentId;
            
            

            // we will get the stock to get the data
            IList productMasterIds = new ArrayList();
            foreach (DepartmentStockInDetail stockInDetail in data.DepartmentStockInDetails)
            {
                productMasterIds.Add(stockInDetail.Product.ProductMaster.ProductMasterId);
            }
            IList productIds = new ArrayList();
            foreach (DepartmentStockInDetail stockInDetail in data.DepartmentStockInDetails)
            {
                productIds.Add(stockInDetail.Product.ProductId);
            }

            criteria = new ObjectCriteria();
            criteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
            criteria.AddGreaterCriteria("Quantity", (long)0);
            //criteria.AddSearchInCriteria("ProductMaster.ProductMasterId", productMasterIds);
            criteria.AddSearchInCriteria("Product.ProductId", productIds);
            //criteria.AddOrder("ProductMaster.ProductMasterId", true);
            criteria.AddOrder("Product.ProductId", true);
            IList stockList = StockDAO.FindAll(criteria);

            IList updateStockList = new ArrayList();
            IList stockOutDetailList = new ArrayList();

            var maxSODetailId = StockOutDetailDAO.SelectSpecificType(new ObjectCriteria(), Projections.Max("StockOutDetailId"));
            long stockOutDetailId = (maxSODetailId == null ? 1 : Int64.Parse(maxSODetailId.ToString()) + 1);
            
            foreach (DepartmentStockInDetail stockInDetail in data.DepartmentStockInDetails)
            {
                long quantity = stockInDetail.Quantity;
                foreach (Stock stock in stockList)
                {
                    long stockInQty = 0;
                    //if (stock.ProductMaster.ProductMasterId.Equals(stockInDetail.Product.ProductMaster.ProductMasterId) && stock.Quantity > 0)
                    if (stock.Product.ProductId.Equals(stockInDetail.Product.ProductId) && stock.Quantity > 0)
                    {   
                        if (quantity > stock.GoodQuantity)
                        {
                            throw new BusinessException("Mặt hàng: " + stock.ProductMaster.ProductName
                                + " - mã vạch: " + stock.Product.ProductId
                                + " không đủ hàng! Tồn: " + stockInDetail.StockQuantity + " , cần xuất: " + quantity);

                            stockInQty = stock.GoodQuantity;
                            quantity -= stock.GoodQuantity;
                            stock.GoodQuantity = 0;
                        }
                        else
                        {
                            stockInQty = quantity;
                            stock.GoodQuantity -= quantity;
                            quantity = 0;
                        }
                        stock.Quantity = stock.GoodQuantity + stock.ErrorQuantity + stock.DamageQuantity +
                                         stock.LostQuantity + stock.UnconfirmQuantity;
                        stock.UpdateDate = DateTime.Now;
                        stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        updateStockList.Add(stock);

                        /*var pk = new StockOutDetailPK
                        {
                            //DepartmentId = data.DepartmentId,
                            ProductId = stock.Product.ProductId,
                            StockOutId = stockOutId
                        };*/
                        
                        var detail = new StockOutDetail
                                         {
                                             //StockOutDetailPK = pk,
                            StockOutDetailId = stockOutDetailId++,
                            StockOutId = stockOutId,
                            StockOut = stockOut,
                            Product =  stock.Product,
                            ProductId = stock.Product.ProductId,
                            Price = stockInDetail.Price,
                            DelFlg = 0,
                            ExclusiveKey = 1,
                            Description = "Export goods",
                            DefectStatus = new StockDefectStatus { DefectStatusId = 0},
                            Quantity = stockInQty,
                            GoodQuantity = stockInQty,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            UpdateId = ClientInfo.getInstance().LoggedUser.Name,
                            CreateId = ClientInfo.getInstance().LoggedUser.Name,
                            ProductMaster = stock.ProductMaster,
                            
                        };
                        /*var deptStock = new DepartmentStock
                        {
                            DepartmentStockPK = new DepartmentStockPK
                            {
                                DepartmentId = data.DepartmentId,
                                ProductId = stock.Product.ProductId
                            },
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            UpdateId = ClientInfo.getInstance().LoggedUser.Name,
                            CreateId = ClientInfo.getInstance().LoggedUser.Name,
                            ProductMaster = stock.ProductMaster
                        };*/
                        stockOutDetailList.Add(detail);
                        if (quantity == 0)
                        {
                            break;
                        }
                    }
                }
                if (quantity > 0)
                {
                    if (data.DepartmentStockInPK != null)
                    {
                        data.DepartmentStockInPK.StockInId = null;
                    }
                    throw new BusinessException("Số lượng xuất kho lớn hơn số lượng trong kho : " + stockInDetail.Product.ProductId);
                }
            }
            // insert stock out and stockout detail
            stockOut.StockOutDetails = stockOutDetailList;
            if(ClientSetting.ExportConfirmation)
            {
                stockOut.ConfirmFlg = 1;
            }
            StockOutDAO.Add(stockOut);

            // Remove duplicate rows
            int count = 0;
            while (count < (stockOutDetailList.Count - 1))
            {
                StockOutDetail detail1 = (StockOutDetail)stockOutDetailList[count];
                detail1.CreateDate = DateTime.Now;
                detail1.UpdateDate = DateTime.Now;
                int maxCount = stockOutDetailList.Count - 1;
                while (maxCount > count)
                {
                    StockOutDetail detail2 = (StockOutDetail)stockOutDetailList[maxCount];

                    if (detail1.Product.ProductId.Equals(detail2.Product.ProductId))
                    {
                        detail1.Quantity += detail2.Quantity;
                        stockOutDetailList.RemoveAt(maxCount);
                    }
                    maxCount--;
                }
                count++;
            }

            foreach (StockOutDetail detail in stockOutDetailList)
            {
                StockOutDetailDAO.Add(detail);
            }

            if (!ClientSetting.ExportConfirmation)
            {
                // update stock
                foreach (Stock stock in updateStockList)
                {
                    StockDAO.Update(stock);
                }
            }

        }

        public void Update(DepartmentStockIn data)
        {
            /*data.DepartmentId = CurrentDepartment.Get().DepartmentId;
            data.UpdateDate = DateTime.Now;
            data.UpdateId = ClientInfo.getInstance().LoggedUser.Name;


            int delFlg = 0;
            foreach (DepartmentStockInDetail stockInDetail in data.DepartmentStockInDetails)
            {
                // add product
                Product product = stockInDetail.Product;
                if (string.IsNullOrEmpty(product.ProductId))
                {
                    // TODO product.ProductId = productId++;
                    product.CreateDate = DateTime.Now;
                    product.UpdateDate = DateTime.Now;
                    product.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    product.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    ProductDAO.Add(product);

                    // add dept stock in
                    var detailPK = new DepartmentStockInDetailPK { DepartmentId = data.DepartmentId, ProductId = product.ProductId, StockInId = data.DepartmentStockInPK.StockInId };
                    stockInDetail.DepartmentStockInDetailPK = detailPK;
                    stockInDetail.CreateDate = DateTime.Now;
                    stockInDetail.UpdateDate = DateTime.Now;
                    stockInDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stockInDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    DepartmentStockInDetailDAO.Add(stockInDetail);

                    // dept stock
                    var stockPk = new DepartmentStockPK { DepartmentId = data.DepartmentId, ProductId = product.ProductId };
                    var departmentStock = new DepartmentStock
                    {
                        DepartmentStockPK = stockPk,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Product = product,
                        //Quantity = product.Quantity,
                        GoodQuantity =product.Quantity,
                        Quantity = stockInDetail.Quantity,
                        GoodQuantity = stockInDetail.Quantity,
                        OnStorePrice = stockInDetail.OnStorePrice
                    };
                    departmentStock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    departmentStock.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    DepartmentStockDAO.Add(departmentStock);

                    var pricePk = new DepartmentPricePK { DepartmentId = data.DepartmentId, ProductMasterId = product.ProductMaster.ProductMasterId };

                    var price = DepartmentPriceDAO.FindById(pricePk);
                    if (price == null)
                    {
                        price = new DepartmentPrice { DepartmentPricePK = pricePk, Price = stockInDetail.OnStorePrice, UpdateDate = DateTime.Now, CreateDate = DateTime.Now };
                        price.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        price.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                        DepartmentPriceDAO.Add(price);
                    }
                }
                else
                {
                    var temProduct = ProductDAO.FindById(product.ProductId);
                    if (stockInDetail.DelFlg == 0)
                    {
                        temProduct.Quantity = product.Quantity;
                        temProduct.Price = product.Price;
                    }
                    else
                    {
                        temProduct.DelFlg = 1;
                        delFlg++;
                    }

                    temProduct.UpdateDate = DateTime.Now;
                    product.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

                    ProductDAO.Update(temProduct);

                    // update dept stock in
                    var detailPK = new DepartmentStockInDetailPK { DepartmentId = data.DepartmentId, ProductId = product.ProductId, StockInId = data.DepartmentStockInPK.StockInId };
                    stockInDetail.DepartmentStockInDetailPK = detailPK;
                    stockInDetail.UpdateDate = DateTime.Now;
                    stockInDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    DepartmentStockInDetailDAO.Update(stockInDetail);

                    // update stock
                    var stockPk = new DepartmentStockPK { DepartmentId = data.DepartmentId, ProductId = product.ProductId };
                    var departmentStock = DepartmentStockDAO.FindById(stockPk);
                    departmentStock.UpdateDate = DateTime.Now;
                    if (stockInDetail.DelFlg == 0)
                    {
                        //departmentStock.Quantity = departmentStock.Quantity -
                        //                           (stockInDetail.OldQuantity - stockInDetail.Quantity);
                        departmentStock.GoodQuantity = departmentStock.GoodQuantity -
                                                   (stockInDetail.OldQuantity - stockInDetail.Quantity);
                        departmentStock.Quantity = departmentStock.GoodQuantity + departmentStock.ErrorQuantity +
                                                   departmentStock.LostQuantity + departmentStock.DamageQuantity +
                                                   departmentStock.UnconfirmQuantity;
                    }
                    else
                    {
                        departmentStock.DelFlg = 1;
                    }
                    departmentStock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    departmentStock.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    DepartmentStockDAO.Update(departmentStock);

                    var pricePk = new DepartmentPricePK { DepartmentId = data.DepartmentId, ProductMasterId = product.ProductMaster.ProductMasterId };

                    var price = DepartmentPriceDAO.FindById(pricePk);
                    if (price == null)
                    {
                        price = new DepartmentPrice { DepartmentPricePK = pricePk, Price = stockInDetail.OnStorePrice, UpdateDate = DateTime.Now, CreateDate = DateTime.Now };
                        price.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        price.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                        DepartmentPriceDAO.Add(price);
                    }
                }
            }

            if (delFlg == data.DepartmentStockInDetails.Count)
            {
                data.DelFlg = 1;
            }
            DepartmentStockInDAO.Update(data); */
        }

        #endregion

        #region IStockOutLogic Members


        public void AddFixedStockOut(StockOut stockOut)
        {
            StockOutDAO.Add(stockOut);
            foreach (StockOutDetail outDetail in stockOut.StockOutDetails)
            {
                StockOutDetailDAO.Add(outDetail);
            }
        }

        #endregion
    }
}
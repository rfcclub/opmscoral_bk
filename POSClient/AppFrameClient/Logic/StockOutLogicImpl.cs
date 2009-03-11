using System;
using System.Collections;
using AppFrame.Common;
using AppFrame.Exceptions;
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
        public IStockDefectDAO StockDefectDAO { get; set; }
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
        /// <param name="data"></param>
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
            IList stockDefectList = StockDefectDAO.FindAll(criteria);
            IList stockList = StockDAO.FindAll(criteria);
            foreach (StockOutDetail stockOutDetail in stockOut.StockOutDetails)
            {
                // check number
                var objectCriteria = new ObjectCriteria();
                objectCriteria.AddEqCriteria("DelFlg", CommonConstants.DEL_FLG_NO);
                objectCriteria.AddEqCriteria("Product.ProductId", stockOutDetail.Product.ProductId);
                StockDefect def = GetErrorCount(stockOutDetail.Product.ProductId, stockDefectList);
                Stock stock = GetStock(stockOutDetail.Product.ProductId, stockList);
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.UnconfirmQuantity = 0;
                if (stockOutDetail.DefectStatus != null && stockOutDetail.DefectStatus.DefectStatusId == 7)
                {
                    stockOutDetail.Quantity = stockOutDetail.GoodQuantity;

                    stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.DamageQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;

                }
                else if (stockOutDetail.DefectStatus != null && stockOutDetail.DefectStatus.DefectStatusId == 4)
                {
                    stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                    
                    //stockOutDetail.ErrorQuantity = 0;
                    stockOutDetail.GoodQuantity = 0;
                    stockOutDetail.UnconfirmQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                }
                else if (stockOutDetail.DefectStatus != null && stockOutDetail.DefectStatus.DefectStatusId == 5)
                {
                    stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;

                    stockOutDetail.UnconfirmQuantity = 0;
                    stockOutDetail.LostQuantity = 0;
                }
                
                if (def != null)
                {
                    def.GoodCount -= stockOutDetail.GoodQuantity;
                    def.ErrorCount -= (int)stockOutDetail.ErrorQuantity;
                    def.DamageCount -= (int)stockOutDetail.DamageQuantity;

                    def.Quantity = def.GoodCount + def.ErrorCount + def.DamageCount + def.UnconfirmCount + def.LostCount;

                    def.UpdateDate = DateTime.Now;
                    def.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

                    StockDefectDAO.Update(def);
                    //long errorCount = GetErrorCount(stockOutDetail.Product.ProductId, stockDefectList);
                    //                if (stockOutDetail.Quantity > errorCount - quantity)
                    //                {
                    //                    throw new BusinessException("Số lượng hàng lỗi không đủ để xuất. Số lượng lỗi hiện có là " +
                    //                                    errorCount.ToString() + ", số lượng đã xuất là " + quantity.ToString());
                    //                }

                }

                if (stock != null )
                {
                    stock.Quantity -= stockOutDetail.GoodQuantity;
                    StockDAO.Update(stock);

                }

                stockOutDetail.StockOut = stockOut;
                stockOutDetail.StockOutDetailId = maxStockOutDetailId++;
                stockOutDetail.StockOutId = stockOut.StockoutId;
                StockOutDetailDAO.Add(stockOutDetail);
            }
            return stockOut;
        }

        private StockDefect GetErrorCount(string id, IList list)
        {
            foreach (StockDefect stockDefect in list)
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
        public void Update(StockOut data)
        {
            StockOutDAO.Update(data);
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


        public void ProcessErrorGoods(IList stockDefectList, IList returnGoodsList, IList tempStockOutList, IList destroyGoodsList)
        {
            long maxId = FindMaxId();
            maxId += 1;
            StockOut destroytSO = new StockOut();
            destroytSO.CreateDate = DateTime.Now;
            destroytSO.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            destroytSO.UpdateDate = DateTime.Now;
            destroytSO.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            
            destroytSO.StockOutDate = DateTime.Now;
            destroytSO.DefectStatus = new StockDefectStatus { DefectStatusId = 8, DefectStatusName = " Huỷ hàng hư và mất" };

            destroytSO.StockoutId = maxId++;
            destroytSO.ExclusiveKey = 1;

            foreach (StockOutDetail stockOutDetail in destroyGoodsList)
            {
                stockOutDetail.Quantity = stockOutDetail.LostQuantity + stockOutDetail.DamageQuantity;
                stockOutDetail.GoodQuantity = 0;
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.ErrorQuantity = 0;
                stockOutDetail.DamageQuantity = 0;
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
                StockDefect defect = GetDefectFromStockOut(stockOutDetail, stockDefectList);
                if(defect == null)
                {
                    throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                }
                defect.LostCount -= (int)stockOutDetail.LostQuantity;
                defect.DamageCount -= (int) stockOutDetail.DamageQuantity;
                defect.Quantity -= stockOutDetail.Quantity;
                StockDefectDAO.Update(defect);
            }
            destroytSO.StockOutDetails = destroyGoodsList;
            StockOutDAO.Add(destroytSO);

            object maxDetailObj = StockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
            long maxDetailId = maxDetailObj != null ? (long)maxDetailObj : 0;
            maxDetailId += 1;
            foreach (StockOutDetail detail in destroyGoodsList)
            {
                detail.StockOutDetailId = maxDetailId++;
                StockOutDetailDAO.Add(detail);
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

            foreach (StockOutDetail stockOutDetail in returnGoodsList)
            {
                stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                stockOutDetail.GoodQuantity = 0;
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.ErrorQuantity = 0;
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

                StockDefect defect = GetDefectFromStockOut(stockOutDetail, stockDefectList);
                if (defect == null)
                {
                    throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                }
                defect.ErrorCount -= (int)stockOutDetail.ErrorQuantity;
                defect.Quantity -= stockOutDetail.Quantity;
                StockDefectDAO.Update(defect);
            }
            returnSO.StockOutDetails = returnGoodsList;
            StockOutDAO.Add(returnSO);
            
            //maxDetailId += 1;
            foreach (StockOutDetail detail in returnGoodsList)
            {
                detail.StockOutDetailId = maxDetailId++;
                StockOutDetailDAO.Add(detail);
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

            foreach (StockOutDetail stockOutDetail in tempStockOutList)
            {
                stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;
                stockOutDetail.GoodQuantity = 0;
                stockOutDetail.LostQuantity = 0;
                stockOutDetail.ErrorQuantity = 0;
                stockOutDetail.DamageQuantity = 0;
                stockOutDetail.CreateDate = DateTime.Now;
                stockOutDetail.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                stockOutDetail.UpdateDate = DateTime.Now;
                stockOutDetail.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                stockOutDetail.StockOut = tempSO;
                stockOutDetail.StockOutId = tempSO.StockoutId;
                stockOutDetail.DelFlg = 0;
                stockOutDetail.ExclusiveKey = 1;
                stockOutDetail.Description = " Trả hàng cho nhà sản xuất";

                StockDefect defect = GetDefectFromStockOut(stockOutDetail, stockDefectList);
                if (defect == null)
                {
                    throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
                }
                defect.ErrorCount -= (int)stockOutDetail.ErrorQuantity;
                defect.Quantity -= stockOutDetail.Quantity;
                StockDefectDAO.Update(defect);
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

        private StockDefect GetDefectFromStockOut(StockOutDetail detail, IList list)
        {
            foreach (StockDefect stockDefect in list)
            {
                if(stockDefect.Product.ProductId == detail.Product.ProductId)
                {
                    return stockDefect;
                }
            }
            return null;
        }

        #endregion
    }
}
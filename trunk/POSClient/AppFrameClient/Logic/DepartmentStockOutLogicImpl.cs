using System;
using System.Collections;
using AppFrame.Common;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;
using AppFrame.Model;
using AppFrame.DataLayer;

namespace AppFrame.Logic
{
    public class DepartmentStockOutLogicImpl : IDepartmentStockOutLogic
    {
        public IDepartmentStockOutDAO DepartmentStockOutDAO { get; set; }
        public IDepartmentStockDAO DepartmentStockDAO { get; set; }
        public IDepartmentStockOutDetailDAO DepartmentStockOutDetailDAO { get; set; }
        public IDepartmentStockHistoryDAO DepartmentStockHistoryDAO { get; set; }

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
            var maxStockOutDetailIdStr = DepartmentStockOutDetailDAO.SelectSpecificType(null, Projections.Max("StockOutDetailId"));
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
                    if (stockOutDetail.DefectStatus.DefectStatusId == 7)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity;

                        stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.DamageQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                    }
                    else if (stockOutDetail.DefectStatus.DefectStatusId == 4)
                    {
                        stockOutDetail.Quantity = stockOutDetail.ErrorQuantity;

                        //stockOutDetail.ErrorQuantity = 0;
                        stockOutDetail.GoodQuantity = 0;
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                    }
                    else if (stockOutDetail.DefectStatus.DefectStatusId == 6)
                    {
                        stockOutDetail.Quantity = stockOutDetail.GoodQuantity + stockOutDetail.ErrorQuantity;
                        
                        stockOutDetail.LostQuantity = 0;
                        stockOutDetail.UnconfirmQuantity = 0;
                    }
                }
                
                    stock.GoodQuantity -= stockOutDetail.GoodQuantity;
                    stock.ErrorQuantity -= (int)stockOutDetail.ErrorQuantity;
                    stock.DamageQuantity -= (int)stockOutDetail.DamageQuantity;
                    stock.Quantity -= stockOutDetail.Quantity;
                    stock.UpdateDate = DateTime.Now;
                    stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

                    DepartmentStockDAO.Update(stock);

                stockOutDetail.DepartmentStockOut = stockOut;
                stockOutDetail.StockOutDetailId = maxStockOutDetailId++;
                stockOutDetail.DepartmentId = CurrentDepartment.Get().DepartmentId;
                stockOutDetail.StockOutId = stockOut.DepartmentStockOutPK.StockOutId;
                DepartmentStockOutDetailDAO.Add(stockOutDetail);
            }
            return stockOut;
        }
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
    }
}
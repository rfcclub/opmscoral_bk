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
                }
                    stock.UpdateDate = DateTime.Now;
                    stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

                    DepartmentStockDAO.Update(stock);

                stockOutDetail.DepartmentStockOut = stockOut;
                stockOutDetail.StockOutDetailId = maxStockOutDetailId++;
                stockOutDetail.DepartmentId = CurrentDepartment.Get().DepartmentId;
                stockOutDetail.StockOutId = stockOut.DepartmentStockOutPK.StockOutId;
                stockOutDetail.ProductMaster = stockOutDetail.Product.ProductMaster;
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

        #region IDepartmentStockOutLogic Members


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
                    detail.StockOutDetailId = maxDetailId++;
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
                    detail.StockOutDetailId = maxDetailId++;
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

                    DepartmentStock defect = GetDefectFromStockOut(stockOutDetail, stockList);
                    if (defect == null)
                    {
                        throw new BusinessException("Hàng xuất không tồn tại trong cơ sở dữ liệu, mục hàng lỗi");
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
                    detail.StockOutDetailId = maxDetailId++;
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
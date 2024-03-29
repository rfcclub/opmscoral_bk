using System;
using System.Collections;
using System.Text;
using AppFrame.Common;
using AppFrame.Exceptions;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;
using AppFrame.Model;
using AppFrame.DataLayer;

namespace AppFrame.Logic
{
    public class DepartmentStockLogicImpl : IDepartmentStockLogic
    {
        public IDepartmentStockDAO DepartmentStockDAO { get; set; }

        public IDepartmentStockOutDAO DepartmentStockOutDAO { get; set; }
        public IDepartmentStockOutDetailDAO DepartmentStockOutDetailDAO { get; set; }
        /// <summary>
        /// Find DepartmentStock object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentStock</param>
        /// <returns></returns>
        public DepartmentStock FindById(object id)
        {
            return DepartmentStockDAO.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentStock to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentStock Add(DepartmentStock data)
        {
            DepartmentStockDAO.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update DepartmentStock to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentStock data)
        {
            DepartmentStockDAO.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentStock from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentStock data)
        {
            DepartmentStockDAO.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentStock from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentStockDAO.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentStock from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList FindAll(ObjectCriteria criteria)
        {
            return DepartmentStockDAO.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentStock from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return DepartmentStockDAO.FindPaging(criteria);
        }

        public IList FindByQuery(ObjectCriteria criteria)
        {
            var sql = new StringBuilder("SELECT s, SUM(s.Quantity), SUM(s.Quantity * sdetail.Price), price FROM DepartmentStock s, Product p, ProductMaster pm, DepartmentStockInDetail sdetail, DepartmentPrice price WHERE s.DepartmentStockPK.ProductId = p.ProductId AND p.ProductMaster.ProductMasterId = pm.ProductMasterId AND s.Product.ProductId = sdetail.Product.ProductId AND pm.ProductMasterId = price.DepartmentPricePK.ProductMasterId ");
            foreach (SQLQueryCriteria crit in criteria.GetQueryCriteria())
            {
                sql.Append(" AND ")
                   .Append(crit.PropertyName)
                   .Append(" ")
                   .Append(crit.SQLString)
                   .Append(" :")
                   .Append(crit.PropertyName)
                   .Append(" ");
            }
            sql.Append(" GROUP BY pm.ProductMasterId");

            //var sqltemp = new StringBuilder("SELECT s from DepartmentStockProductMasterView s");
            //DepartmentStockDAO.FindByQuery(sqltemp.ToString(), new ObjectCriteria());
            return DepartmentStockDAO.FindByQuery(sql.ToString(), criteria);
        }

        public IList FindByQueryForDeptStock(ObjectCriteria criteria)
        {
            var sqlString = new StringBuilder("select stock, sum(stock.Quantity) FROM DepartmentStock stock, Product p, ProductMaster pm WHERE stock.Product.ProductId = p.ProductId AND p.ProductMaster.ProductMasterId = pm.ProductMasterId ");
            foreach (SQLQueryCriteria crit in criteria.GetQueryCriteria())
            {
                sqlString.Append(" AND ")
                       .Append(crit.PropertyName)
                       .Append(" ")
                       .Append(crit.SQLString)
                       .Append(" :")
                       .Append(crit.PropertyName)
                       .Append(" ");
            }
            sqlString.Append(" Having sum(stock.Quantity) > 0 ");
            sqlString.Append(" Group BY pm.ProductMasterId");
            //criteria.AddQueryCriteria("productName", "Ao%");
            //var sqlString = "select * FROM Stock stock, Product p WHERE stock.Product_Id = p.Product_Id";
            return DepartmentStockDAO.FindByQueryForDeptStock(sqlString.ToString(), criteria);
        }

        #region IDepartmentStockLogic Members


        public void Process(IList list)
        {
            
        }

        #endregion

        #region IDepartmentStockLogic Members


        public IList FindAllErrors()
        {
            return DepartmentStockDAO.FindAllErrors();
        }

        #endregion

        #region IDepartmentStockLogic Members


        public void ProcessErrorGoods(IList stockList, IList returnGoodsList, IList tempStockOutList, IList destroyGoodsList)
        {
            
           /* var maxStockOutDetailIdStr = DepartmentStockOutDAO.SelectSpecificType(null, Projections.Max("DepartmentStockOutPK.StockOutId"));
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

            destroytSO.DepartmentStockOutPK =new DepartmentStockOutPK();
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
                    stockOutDetail.Description = " Trả hàng cho nhà sản xuất";

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
                    defect.ErrorQuantity -= stockOutDetail.ErrorQuantity;
                    defect.Quantity -= stockOutDetail.Quantity;
                    DepartmentStockDAO.Update(defect);
                }
                tempSO.DepartmentStockOutDetails = tempStockOutList;
                DepartmentStockOutDAO.Add(tempSO);

                //maxDetailId += 1;
                foreach (DepartmentStockOutDetail detail in tempStockOutList)
                {
                    detail.StockOutDetailId = maxDetailId++;
                    DepartmentStockOutDetailDAO.Add(detail);
                }
            } */ 
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

        public long FindMaxId()
        {
            return 0; 
        }

        #endregion

        #region IDepartmentStockLogic Members


        public IList FindAllInProductMasterId(IList ids)
        {
            return DepartmentStockDAO.FindAllInProductMasterId(ids);
            
        }

        #endregion
    }
}
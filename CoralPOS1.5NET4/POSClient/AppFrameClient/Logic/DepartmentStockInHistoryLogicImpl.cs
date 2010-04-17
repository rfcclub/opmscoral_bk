using System;
using System;
using System.Collections;
using System.Diagnostics;
using AppFrame.Common;
using AppFrame.Exceptions;
using AppFrame.Utility.Mapper;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;
using AppFrame.Model;
using AppFrame.DataLayer;

namespace AppFrame.Logic
{
    public class DepartmentStockInHistoryLogicImpl : IDepartmentStockInHistoryLogic
    {
        public IDepartmentStockInHistoryDAO DepartmentStockInHistoryDAO { get; set; }

        /// <summary>
        /// Find DepartmentStockInHistory object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentStockInHistory</param>
        /// <returns></returns>
        public DepartmentStockInHistory FindById(object id)
        {
            return DepartmentStockInHistoryDAO.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentStockInHistory to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentStockInHistory Add(DepartmentStockInHistory data)
        {
            DepartmentStockInHistoryDAO.Add(data);
            return data;
        }

        /// <summary>
        /// Add DepartmentStockInHistory to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        
        /// <summary>
        /// Update DepartmentStockInHistory to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentStockInHistory data)
        {
            DepartmentStockInHistoryDAO.Update(data);
        }

        
        /// <summary>
        /// Delete DepartmentStockInHistory from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentStockInHistory data)
        {
            DepartmentStockInHistoryDAO.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentStockInHistory from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentStockInHistoryDAO.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentStockInHistory from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList FindAll(ObjectCriteria criteria)
        {
            return DepartmentStockInHistoryDAO.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentStockInHistory from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return DepartmentStockInHistoryDAO.FindPaging(criteria);
        }
    }
}
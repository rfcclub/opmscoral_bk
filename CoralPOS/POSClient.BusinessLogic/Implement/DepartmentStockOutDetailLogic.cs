			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using System.Linq.Expressions;
using AppFrame.DataLayer;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.LambdaExtensions;
using NHibernate.Linq.Expressions;
using Spring.Data.NHibernate;
using  CoralPOS.Models;
using  POSClient.DataLayer.Implement;

namespace POSClient.BusinessLogic.Implement
{
    public class DepartmentStockOutDetailLogic : IDepartmentStockOutDetailLogic
    {
        private IDepartmentStockOutDetailDao _innerDao;
        public IDepartmentStockOutDetailDao DepartmentStockOutDetailDao
        {
            get 
            { 
                return _innerDao; 
            }
            set 
            { 
                _innerDao = value; 
            }
        }
        
        /// <summary>
        /// Find DepartmentStockOutDetail object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentStockOutDetail</param>
        /// <returns></returns>
        public DepartmentStockOutDetail FindById(object id)
        {
            return DepartmentStockOutDetailDao.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentStockOutDetail to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentStockOutDetail Add(DepartmentStockOutDetail data)
        {
            DepartmentStockOutDetailDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update DepartmentStockOutDetail to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentStockOutDetail data)
        {
            DepartmentStockOutDetailDao.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentStockOutDetail from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentStockOutDetail data)
        {
            DepartmentStockOutDetailDao.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentStockOutDetail from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentStockOutDetailDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentStockOutDetail from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<DepartmentStockOutDetail> FindAll(ObjectCriteria<DepartmentStockOutDetail> criteria)
        {
            return DepartmentStockOutDetailDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentStockOutDetail from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria<DepartmentStockOutDetail> criteria)
        {
            return DepartmentStockOutDetailDao.FindPaging(criteria);
        }
    }
}
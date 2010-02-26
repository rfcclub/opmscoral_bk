			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class DepartmentPurchaseOrderDetailLogicImpl : IDepartmentPurchaseOrderDetailLogic
    {
        private IDepartmentPurchaseOrderDetailDao _innerDao;

        public IDepartmentPurchaseOrderDetailDao DepartmentPurchaseOrderDetailDao
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
        /// Find DepartmentPurchaseOrderDetail object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentPurchaseOrderDetail</param>
        /// <returns></returns>
        public DepartmentPurchaseOrderDetail FindById(object id)
        {
            return _innerDao.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentPurchaseOrderDetail to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentPurchaseOrderDetail Add(DepartmentPurchaseOrderDetail data)
        {
            _innerDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update DepartmentPurchaseOrderDetail to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentPurchaseOrderDetail data)
        {
            _innerDao.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentPurchaseOrderDetail from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentPurchaseOrderDetail data)
        {
            _innerDao.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentPurchaseOrderDetail from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            _innerDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentPurchaseOrderDetail from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<DepartmentPurchaseOrderDetail> FindAll(ObjectCriteria criteria)
        {
            return _innerDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentPurchaseOrderDetail from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return _innerDao.FindPaging(criteria);
        }
    }
}
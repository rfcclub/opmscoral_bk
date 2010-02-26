			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class PurchaseOrdersTransactionLogicImpl : IPurchaseOrdersTransactionLogic
    {
        private IPurchaseOrdersTransactionDao _innerDao;

        public IPurchaseOrdersTransactionDao PurchaseOrdersTransactionDao
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
        /// Find PurchaseOrdersTransaction object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of PurchaseOrdersTransaction</param>
        /// <returns></returns>
        public PurchaseOrdersTransaction FindById(object id)
        {
            return _innerDao.FindById(id);
        }
        
        /// <summary>
        /// Add PurchaseOrdersTransaction to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public PurchaseOrdersTransaction Add(PurchaseOrdersTransaction data)
        {
            _innerDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update PurchaseOrdersTransaction to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(PurchaseOrdersTransaction data)
        {
            _innerDao.Update(data);
        }
        
        /// <summary>
        /// Delete PurchaseOrdersTransaction from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(PurchaseOrdersTransaction data)
        {
            _innerDao.Delete(data);
        }
        
        /// <summary>
        /// Delete PurchaseOrdersTransaction from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            _innerDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all PurchaseOrdersTransaction from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<PurchaseOrdersTransaction> FindAll(ObjectCriteria criteria)
        {
            return _innerDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all PurchaseOrdersTransaction from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return _innerDao.FindPaging(criteria);
        }
    }
}
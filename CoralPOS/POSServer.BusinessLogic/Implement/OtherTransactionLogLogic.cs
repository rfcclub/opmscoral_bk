			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class OtherTransactionLogLogic : IOtherTransactionLogLogic
    {
        private IOtherTransactionLogDao _innerDao;
        public IOtherTransactionLogDao OtherTransactionLogDao
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
        /// Find OtherTransactionLog object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of OtherTransactionLog</param>
        /// <returns></returns>
        public OtherTransactionLog FindById(object id)
        {
            return OtherTransactionLogDao.FindById(id);
        }
        
        /// <summary>
        /// Add OtherTransactionLog to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public OtherTransactionLog Add(OtherTransactionLog data)
        {
            OtherTransactionLogDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update OtherTransactionLog to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(OtherTransactionLog data)
        {
            OtherTransactionLogDao.Update(data);
        }
        
        /// <summary>
        /// Delete OtherTransactionLog from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(OtherTransactionLog data)
        {
            OtherTransactionLogDao.Delete(data);
        }
        
        /// <summary>
        /// Delete OtherTransactionLog from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            OtherTransactionLogDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all OtherTransactionLog from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<OtherTransactionLog> FindAll(ObjectCriteria criteria)
        {
            return OtherTransactionLogDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all OtherTransactionLog from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return OtherTransactionLogDao.FindPaging(criteria);
        }
    }
}
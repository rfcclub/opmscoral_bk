			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class StockDefinitionStatusLogic : IStockDefinitionStatusLogic
    {
        private IStockDefinitionStatusDao _innerDao;
        public IStockDefinitionStatusDao StockDefinitionStatusDao
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
        /// Find StockDefinitionStatus object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of StockDefinitionStatus</param>
        /// <returns></returns>
        public StockDefinitionStatus FindById(object id)
        {
            return StockDefinitionStatusDao.FindById(id);
        }
        
        /// <summary>
        /// Add StockDefinitionStatus to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public StockDefinitionStatus Add(StockDefinitionStatus data)
        {
            StockDefinitionStatusDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update StockDefinitionStatus to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(StockDefinitionStatus data)
        {
            StockDefinitionStatusDao.Update(data);
        }
        
        /// <summary>
        /// Delete StockDefinitionStatus from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(StockDefinitionStatus data)
        {
            StockDefinitionStatusDao.Delete(data);
        }
        
        /// <summary>
        /// Delete StockDefinitionStatus from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            StockDefinitionStatusDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all StockDefinitionStatus from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<StockDefinitionStatus> FindAll(ObjectCriteria criteria)
        {
            return StockDefinitionStatusDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all StockDefinitionStatus from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return StockDefinitionStatusDao.FindPaging(criteria);
        }
    }
}
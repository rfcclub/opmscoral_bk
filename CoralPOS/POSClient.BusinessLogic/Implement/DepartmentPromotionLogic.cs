			 


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
    public class DepartmentPromotionLogic : IDepartmentPromotionLogic
    {
        private IDepartmentPromotionDao _innerDao;
        public IDepartmentPromotionDao DepartmentPromotionDao
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
        /// Find DepartmentPromotion object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentPromotion</param>
        /// <returns></returns>
        public DepartmentPromotion FindById(object id)
        {
            return DepartmentPromotionDao.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentPromotion to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentPromotion Add(DepartmentPromotion data)
        {
            DepartmentPromotionDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update DepartmentPromotion to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentPromotion data)
        {
            DepartmentPromotionDao.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentPromotion from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentPromotion data)
        {
            DepartmentPromotionDao.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentPromotion from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentPromotionDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentPromotion from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<DepartmentPromotion> FindAll(ObjectCriteria<DepartmentPromotion> criteria)
        {
            return DepartmentPromotionDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentPromotion from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria<DepartmentPromotion> criteria)
        {
            return DepartmentPromotionDao.FindPaging(criteria);
        }
    }
}
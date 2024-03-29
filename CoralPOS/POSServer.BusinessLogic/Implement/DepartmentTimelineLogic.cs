			 


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
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class DepartmentTimelineLogic : IDepartmentTimelineLogic
    {
        private IDepartmentTimelineDao _innerDao;
        public IDepartmentTimelineDao DepartmentTimelineDao
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
        /// Find DepartmentTimeline object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentTimeline</param>
        /// <returns></returns>
        public DepartmentTimeline FindById(object id)
        {
            return DepartmentTimelineDao.FindById(id);
        }
        
        /// <summary>
        /// Add DepartmentTimeline to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public DepartmentTimeline Add(DepartmentTimeline data)
        {
            DepartmentTimelineDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update DepartmentTimeline to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(DepartmentTimeline data)
        {
            DepartmentTimelineDao.Update(data);
        }
        
        /// <summary>
        /// Delete DepartmentTimeline from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(DepartmentTimeline data)
        {
            DepartmentTimelineDao.Delete(data);
        }
        
        /// <summary>
        /// Delete DepartmentTimeline from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            DepartmentTimelineDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all DepartmentTimeline from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<DepartmentTimeline> FindAll(ObjectCriteria<DepartmentTimeline> criteria)
        {
            return DepartmentTimelineDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all DepartmentTimeline from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria<DepartmentTimeline> criteria)
        {
            return DepartmentTimelineDao.FindPaging(criteria);
        }
    }
}
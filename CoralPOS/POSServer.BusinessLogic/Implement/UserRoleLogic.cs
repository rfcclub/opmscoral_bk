			 


using System.Collections;
using System.Collections.Generic;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public class UserRoleLogic : IUserRoleLogic
    {
        private IUserRoleDao _innerDao;
        public IUserRoleDao UserRoleDao
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
        /// Find UserRole object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of UserRole</param>
        /// <returns></returns>
        public UserRole FindById(object id)
        {
            return UserRoleDao.FindById(id);
        }
        
        /// <summary>
        /// Add UserRole to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public UserRole Add(UserRole data)
        {
            UserRoleDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update UserRole to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(UserRole data)
        {
            UserRoleDao.Update(data);
        }
        
        /// <summary>
        /// Delete UserRole from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(UserRole data)
        {
            UserRoleDao.Delete(data);
        }
        
        /// <summary>
        /// Delete UserRole from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            UserRoleDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all UserRole from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<UserRole> FindAll(ObjectCriteria criteria)
        {
            return UserRoleDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all UserRole from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria criteria)
        {
            return UserRoleDao.FindPaging(criteria);
        }
    }
}
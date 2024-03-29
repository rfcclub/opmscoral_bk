using System;
using System.Collections;
using AppFrame.Model;
using NHibernate.Criterion;

namespace AppFrame.DataLayer
{
    public interface IReceiptDAO
    {
        /// <summary>
        /// Find Receipt object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of Receipt</param>
        /// <returns></returns>
        Receipt FindById(object id);
        
        /// <summary>
        /// Add Receipt to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Receipt Add(Receipt data);
        
        /// <summary>
        /// Update Receipt to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update(Receipt data);
        
        /// <summary>
        /// Delete Receipt from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete(Receipt data);
        
        /// <summary>
        /// Delete Receipt from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all Receipt from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList FindAll(ObjectCriteria criteria);
        
        /// <summary>
        /// Find all Receipt from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria criteria);
        
        /// <summary>
        /// Find min, max, count... Receipt from database.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria criteria, IProjection type);
    }
}
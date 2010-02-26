			 
using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate;
using CoralPOS.Models;

namespace POSServer.DataLayer.Implement
{
	
    public interface IDepartmentTimelineDao
    {
        /// <summary>
        /// Find DepartmentTimeline object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of DepartmentTimeline</param>
        /// <returns></returns>
        DepartmentTimeline FindById(object id);
        
        /// <summary>
        /// Add DepartmentTimeline to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DepartmentTimeline Add(DepartmentTimeline data);
        
        /// <summary>
        /// Update DepartmentTimeline to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Update(DepartmentTimeline data);
        
        /// <summary>
        /// Delete DepartmentTimeline from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DepartmentTimeline data);
        
        /// <summary>
        /// Delete DepartmentTimeline from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(object id);
        
        /// <summary>
        /// Find all DepartmentTimeline from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<DepartmentTimeline> FindAll(ObjectCriteria criteria);
        
        /// <summary>
        /// Find all DepartmentTimeline from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria criteria);
        
        /// <summary>
        /// Find min, max, count... DepartmentTimeline from database.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria criteria, IProjection type); 
    }
}

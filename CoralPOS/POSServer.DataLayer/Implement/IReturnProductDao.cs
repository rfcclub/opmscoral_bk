			 
using System;
using System.Collections;
using System.Collections.Generic;
using AppFrame.DataLayer;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate;
using CoralPOS.Models;

namespace POSServer.DataLayer.Implement
{
	
    public interface IReturnProductDao
    {
        /// <summary>
        /// Find Tax object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of Tax</param>
        /// <returns></returns>
        ReturnProduct FindById(object id);
        
        /// <summary>
        /// Add Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ReturnProduct Add(ReturnProduct data);
        
        /// <summary>
        /// Update Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Update(ReturnProduct data);
        
        /// <summary>
        /// Delete Tax from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(ReturnProduct data);
        
        /// <summary>
        /// Delete Tax from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(object id);

        /// <summary>
        /// Find all Tax from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>		
        IList<ReturnProduct> FindAll(LinqCriteria<ReturnProduct> criteria);

        IList<ReturnProduct> FindAll(ObjectCriteria<ReturnProduct> criteria);
		
        object FindFirst(ObjectCriteria<ReturnProduct> criteria);

        /// <summary>
        /// Find all Tax from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<ReturnProduct> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(ObjectCriteria<ReturnProduct> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria<ReturnProduct> criteria, IProjection type);
    }
}

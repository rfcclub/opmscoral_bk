			 
using System;
using System.Collections;
using System.Collections.Generic;
using AppFrame.DataLayer;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate;
using CoralPOS.Models;

namespace POSClient.DataLayer.Implement
{
	
    public interface IDistributorDao
    {
        /// <summary>
        /// Find Tax object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of Tax</param>
        /// <returns></returns>
        Distributor FindById(object id);
        
        /// <summary>
        /// Add Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Distributor Add(Distributor data);
        
        /// <summary>
        /// Update Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Update(Distributor data);
        
        /// <summary>
        /// Delete Tax from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(Distributor data);
        
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
        IList<Distributor> FindAll(LinqCriteria<Distributor> criteria);

        IList<Distributor> FindAll(ObjectCriteria<Distributor> criteria);
		
        object FindFirst(ObjectCriteria<Distributor> criteria);
		
		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="subProp"></param>
        /// <returns></returns>
        IList<TClass> FindAllSubProperty<TClass>(LinqCriteria<Distributor> criteria,Func<Distributor,TClass> subProp);

        /// <summary>
        /// Find all Tax from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<Distributor> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(ObjectCriteria<Distributor> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria<Distributor> criteria, IProjection type);
    }
}


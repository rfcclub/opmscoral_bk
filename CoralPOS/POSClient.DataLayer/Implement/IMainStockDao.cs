			 
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
	
    public interface IMainStockDao
    {
        /// <summary>
        /// Find Tax object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of Tax</param>
        /// <returns></returns>
        MainStock FindById(object id);
        
        /// <summary>
        /// Add Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        MainStock Add(MainStock data);
        
        /// <summary>
        /// Update Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Update(MainStock data);
        
        /// <summary>
        /// Delete Tax from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(MainStock data);
        
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
        IList<MainStock> FindAll(LinqCriteria<MainStock> criteria);

        IList<MainStock> FindAll(ObjectCriteria<MainStock> criteria);
		
        object FindFirst(ObjectCriteria<MainStock> criteria);
		
		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="subProp"></param>
        /// <returns></returns>
        IList<TClass> FindAllSubProperty<TClass>(LinqCriteria<MainStock> criteria,Func<MainStock,TClass> subProp);

        /// <summary>
        /// Find all Tax from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<MainStock> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(ObjectCriteria<MainStock> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria<MainStock> criteria, IProjection type);
		
		/// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="exposeSession"></param>
        /// <returns></returns>
        object Execute(IHibernateCallback callback, bool exposeSession);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegated"></param>
        /// <returns></returns>
        object Execute(HibernateDelegate delegated);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegated"></param>
        /// <returns></returns>
        object ExecuteExposedSession(HibernateDelegate delegated);
    }
}


			 
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
	
    public interface ITransactionDao
    {
        /// <summary>
        /// Find Tax object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of Tax</param>
        /// <returns></returns>
        Transaction FindById(object id);
        
        /// <summary>
        /// Add Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Transaction Add(Transaction data);
        
        /// <summary>
        /// Update Tax to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Update(Transaction data);
        
        /// <summary>
        /// Delete Tax from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(Transaction data);
        
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
        IList<Transaction> FindAll(LinqCriteria<Transaction> criteria);

        IList<Transaction> FindAll(ObjectCriteria<Transaction> criteria);
		
        object FindFirst(ObjectCriteria<Transaction> criteria);
		
		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="subProp"></param>
        /// <returns></returns>
        IList<TClass> FindAllSubProperty<TClass>(LinqCriteria<Transaction> criteria,Func<Transaction,TClass> subProp);

        /// <summary>
        /// Find all Tax from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<Transaction> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(ObjectCriteria<Transaction> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object SelectSpecificType(ObjectCriteria<Transaction> criteria, IProjection type);
    }
}


			 
using System;
using System.Collections;
using System.Collections.Generic;
using AppFrame.DataLayer;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate;
using CoralPOS.Models;
using POSClient.DataLayer.Implement;

namespace POSClient.BusinessLogic.Implement
{
    public interface ICategoryLogic
    {
        /// <summary>
        /// Find  Category object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  Category</param>
        /// <returns></returns>
         Category FindById(object id);
        
        /// <summary>
        /// Add  Category to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         Category Add( Category data);
        
        /// <summary>
        /// Update  Category to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( Category data);
        
        /// <summary>
        /// Delete  Category from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( Category data);
        
        /// <summary>
        /// Delete  Category from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  Category from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<Category> FindAll(ObjectCriteria<Category> criteria);
        
        /// <summary>
        /// Find all  Category from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<Category> criteria);
    }
}
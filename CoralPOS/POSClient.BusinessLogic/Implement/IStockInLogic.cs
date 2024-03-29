			 
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
    public interface IStockInLogic
    {
        /// <summary>
        /// Find  StockIn object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  StockIn</param>
        /// <returns></returns>
         StockIn FindById(object id);
        
        /// <summary>
        /// Add  StockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         StockIn Add( StockIn data);
        
        /// <summary>
        /// Update  StockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( StockIn data);
        
        /// <summary>
        /// Delete  StockIn from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( StockIn data);
        
        /// <summary>
        /// Delete  StockIn from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  StockIn from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<StockIn> FindAll(ObjectCriteria<StockIn> criteria);
        
        /// <summary>
        /// Find all  StockIn from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<StockIn> criteria);
    }
}
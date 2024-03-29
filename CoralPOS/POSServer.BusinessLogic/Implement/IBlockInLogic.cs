			 
using System;
using System.Collections;
using System.Collections.Generic;
using AppFrame.DataLayer;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate;
using CoralPOS.Models;
using POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public interface IBlockInLogic
    {
        /// <summary>
        /// Find  BlockIn object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  BlockIn</param>
        /// <returns></returns>
         BlockIn FindById(object id);
        
        /// <summary>
        /// Add  BlockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         BlockIn Add( BlockIn data);
        
        /// <summary>
        /// Update  BlockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( BlockIn data);
        
        /// <summary>
        /// Delete  BlockIn from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( BlockIn data);
        
        /// <summary>
        /// Delete  BlockIn from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  BlockIn from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<BlockIn> FindAll(ObjectCriteria<BlockIn> criteria);
        
        /// <summary>
        /// Find all  BlockIn from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<BlockIn> criteria);
    }
}
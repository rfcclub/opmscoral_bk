			 
			 

using System;
using System.Collections;
using CoralPOS.Models;

namespace POSServer.DataLayer.Implement
{
    public interface IMainStockLogic
    {
        /// <summary>
        /// Find  MainStock object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  MainStock</param>
        /// <returns></returns>
         MainStock FindById(object id);
        
        /// <summary>
        /// Add  MainStock to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         MainStock Add( MainStock data);
        
        /// <summary>
        /// Update  MainStock to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( MainStock data);
        
        /// <summary>
        /// Delete  MainStock from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( MainStock data);
        
        /// <summary>
        /// Delete  MainStock from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  MainStock from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList FindAll(ObjectCriteria criteria);
        
        /// <summary>
        /// Find all  MainStock from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria criteria);
    }
}
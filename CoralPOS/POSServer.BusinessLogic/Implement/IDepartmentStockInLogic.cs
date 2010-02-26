			 
			 

using System;
using System.Collections;
using System.Collections.Generic;
using CoralPOS.Models;
using POSServer.DataLayer.Implement;

namespace POSServer.BusinessLogic.Implement
{
    public interface IDepartmentStockInLogic
    {
        /// <summary>
        /// Find  DepartmentStockIn object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  DepartmentStockIn</param>
        /// <returns></returns>
         DepartmentStockIn FindById(object id);
        
        /// <summary>
        /// Add  DepartmentStockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         DepartmentStockIn Add( DepartmentStockIn data);
        
        /// <summary>
        /// Update  DepartmentStockIn to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( DepartmentStockIn data);
        
        /// <summary>
        /// Delete  DepartmentStockIn from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( DepartmentStockIn data);
        
        /// <summary>
        /// Delete  DepartmentStockIn from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  DepartmentStockIn from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<DepartmentStockIn> FindAll(ObjectCriteria criteria);
        
        /// <summary>
        /// Find all  DepartmentStockIn from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria criteria);
    }
}
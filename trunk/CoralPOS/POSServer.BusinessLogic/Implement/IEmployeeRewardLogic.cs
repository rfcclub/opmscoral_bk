			 
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
    public interface IEmployeeRewardLogic
    {
        /// <summary>
        /// Find  EmployeeReward object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of  EmployeeReward</param>
        /// <returns></returns>
         EmployeeReward FindById(object id);
        
        /// <summary>
        /// Add  EmployeeReward to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         EmployeeReward Add( EmployeeReward data);
        
        /// <summary>
        /// Update  EmployeeReward to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Update( EmployeeReward data);
        
        /// <summary>
        /// Delete  EmployeeReward from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Delete( EmployeeReward data);
        
        /// <summary>
        /// Delete  EmployeeReward from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(object id);
        
        /// <summary>
        /// Find all  EmployeeReward from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IList<EmployeeReward> FindAll(ObjectCriteria<EmployeeReward> criteria);
        
        /// <summary>
        /// Find all  EmployeeReward from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        QueryResult FindPaging(ObjectCriteria<EmployeeReward> criteria);
    }
}
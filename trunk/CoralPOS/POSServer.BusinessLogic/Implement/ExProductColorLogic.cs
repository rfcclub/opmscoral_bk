			 


using System;
using System.Collections;
using System.Collections.Generic;
using AppFrame.Base;
using NHibernate.Criterion;
using POSServer.BusinessLogic.Common;
using Spring.Transaction.Interceptor;
using  CoralPOS.Models;
using  POSServer.DataLayer.Implement;
using AppFrame.DataLayer;

namespace POSServer.BusinessLogic.Implement
{
    public class ExProductColorLogic : IExProductColorLogic
    {
        private IExProductColorDao _innerDao;
        public IExProductColorDao ExProductColorDao
        {
            get 
            { 
                return _innerDao; 
            }
            set 
            { 
                _innerDao = value; 
            }
        }
        
        /// <summary>
        /// Find ExProductColor object by id. Return null if nothing is found
        /// </summary>
        /// <param name="id">Id of ExProductColor</param>
        /// <returns></returns>
        public ExProductColor FindById(object id)
        {
            return ExProductColorDao.FindById(id);
        }
        
        /// <summary>
        /// Add ExProductColor to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public ExProductColor Add(ExProductColor data)
        {
            ExProductColorDao.Add(data);
            return data;
        }
        
        /// <summary>
        /// Update ExProductColor to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Update(ExProductColor data)
        {
            ExProductColorDao.Update(data);
        }
        
        /// <summary>
        /// Delete ExProductColor from database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void Delete(ExProductColor data)
        {
            ExProductColorDao.Delete(data);
        }
        
        /// <summary>
        /// Delete ExProductColor from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(ReadOnly=false)]
        public void DeleteById(object id)
        {
            ExProductColorDao.DeleteById(id);
        }
        
        /// <summary>
        /// Find all ExProductColor from database. No pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IList<ExProductColor> FindAll(ObjectCriteria<ExProductColor> criteria)
        {
            return ExProductColorDao.FindAll(criteria);
        }
        
        /// <summary>
        /// Find all ExProductColor from database. Has pagination.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public QueryResult FindPaging(ObjectCriteria<ExProductColor> criteria)
        {
            return ExProductColorDao.FindPaging(criteria);
        }

        public void LoadProductColorDefinition(IFlowSession flowSession)
        {
            IList<ExProductColor> productColors = ExProductColorDao.FindAll(new ObjectCriteria<ExProductColor>());
            flowSession.Put(FlowConstants.PRODUCT_COLOR_LIST, productColors);
        }

        public void Process(IFlowSession session)
        {
            
        }

        public void Update(IList ProductColorList)
        {
            var maxIdResult = ExProductColorDao.SelectSpecificType(null, Projections.Max("ColorId"));
            long maxColorId = maxIdResult != null ? Int64.Parse(maxIdResult.ToString()) + 1: 1;
            foreach (ExProductColor productColor in ProductColorList)
            {
                if (productColor.ColorId > 0)
                {
                    ExProductColor current = ExProductColorDao.FindById(productColor.ColorId);
                    current.ColorName = productColor.ColorName;
                    current.UpdateDate = DateTime.Now;
                    ExProductColorDao.Update(current);
                }
                else
                {
                    productColor.ColorId = maxColorId++;
                    productColor.CreateDate = DateTime.Now;
                    productColor.UpdateDate = DateTime.Now;
                    ExProductColorDao.Add(productColor);
                }
            }
            
        }
    }
}
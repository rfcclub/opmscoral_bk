﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using AppFrame.Base;
using AppFrame.DataLayer;
using AppFrame.WPF.Screens;
using Caliburn.Micro;
using CoralPOS.Models;
using POSServer.BusinessLogic.Common;
using POSServer.BusinessLogic.Implement;
using POSServer.Utils;

namespace POSServer.Actions.Stock.StockOut
{
    public class StockOutConfirmPreLoadAction : PosAction
    {
        public IProductMasterLogic ProductMasterLogic { get; set; }
        public IMainStockLogic MainStockLogic
        {
            get; set;
        }

        public IDepartmentLogic DepartmentLogic
        {
            get; set;
        }

        public IStockOutLogic StockOutLogic
        {
            get; set; 
        }

        public override void DoExecute()
        {
            IoC.Get<ICircularLoadViewModel>().StartLoading();
            DoExecuteCompleted += StockOutPreLoadActionDoExecuteCompleted;
            DoExecuteAsync(DoWork, null);
        }

        void StockOutPreLoadActionDoExecuteCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IoC.Get<ICircularLoadViewModel>().StopLoading();
            GoToNextNode();
        }

        private object DoWork()
        {
            
            var stockOutCriteria = new ObjectCriteria<CoralPOS.Models.StockOut>();
            stockOutCriteria.Add(x => x.ConfirmFlg == 1);
            var confirmingStockOuts = (IList) StockOutLogic.FindAll(stockOutCriteria);
            IList<CoralPOS.Models.StockOut> fetchedStockOuts = new List<CoralPOS.Models.StockOut>();
            foreach (CoralPOS.Models.StockOut confirmingStockOut in confirmingStockOuts)
            {
                CoralPOS.Models.StockOut fetchedStockOut = StockOutLogic.Fetch(confirmingStockOut);
                MainStockLogic.UpdateStockQuantity(fetchedStockOut.StockOutDetails);
                fetchedStockOuts.Add(fetchedStockOut);
            }

            Flow.Session.Put(FlowConstants.CONFIRMING_STOCK_OUT_LIST,confirmingStockOuts);
            return null;
        }
    }
}
﻿using System.Collections;
using System.Windows;
using AppFrame.Base;
using AppFrame.CustomAttributes;
using AppFrame.WPF.Screens;
using Caliburn.Micro;
using CoralPOS.Common;
using POSServer.BusinessLogic.Common;
using POSServer.BusinessLogic.Implement;
using POSServer.Utils;
using Spring.Transaction.Interceptor;

namespace POSServer.Actions.Stock.StockOut
{
    [PerRequest]
    public class StockOutConfirmSaveAction : DefaultPosAction
    {
        [Autowired]
        public IProductMasterLogic ProductMasterLogic { get; set; }
        [Autowired]
        public IProductLogic ProductLogic { get; set; }
        [Autowired]
        public IExProductColorLogic ProductColorLogic { get; set; }
        [Autowired]
        public IExProductSizeLogic ProductSizeLogic { get; set; }
        [Autowired]
        public ICategoryLogic CategoryLogic { get; set; }
        [Autowired]
        public IProductTypeLogic ProductTypeLogic { get; set; }
        [Autowired]
        public IStockOutLogic StockOutLogic { get; set; }
        [Autowired]
        public IMainStockLogic MainStockLogic { get; set; }

        public override void DoExecute()
        {
            StartAsyncWork();
        }

        [Transaction]
        public override object Working()
        {
            IList stockOuts = Flow.Session.Get(FlowConstants.SAVE_STOCK_OUT) as IList;
            bool confirmFlag = (bool)Flow.Session.Get(FlowConstants.STOCK_OUT_CONFIRM_FLAG);

            foreach (CoralPOS.Models.StockOut stockOut in stockOuts)
            {
                if (confirmFlag)
                {
                    MainStockLogic.Update(stockOut);
                    stockOut.ConfirmFlg = 2;
                    StockOutLogic.Update(stockOut);
                }
                else
                    StockOutLogic.Delete(stockOut);
            }
            return null;
        }

        public override void AfterWorkCompleted()
        {
            MessageBox.Show("Processed Stock Out Confirmation  !!");
            GoToNextNode();
        }
    }
}
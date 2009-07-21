﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame;
using AppFrame.Common;
using AppFrame.Exceptions;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;
using CoralPOS.ViewModel;

namespace CoralPOSServer.Presenter.GoodsIO.MainStock
{
    public class InventoryCheckingController : IInventoryCheckingController
    {
        #region IInventoryCheckingController Members
        
        CoralPOS.Interfaces.View.GoodsIO.MainStock.IInventoryCheckingView inventoryCheckingView;    
        public CoralPOS.Interfaces.View.GoodsIO.MainStock.IInventoryCheckingView InventoryCheckingView
        {
            get
            {
                return inventoryCheckingView;
            }
            set
            {
                inventoryCheckingView = value;
                inventoryCheckingView.FillProductMasterToComboEvent += new EventHandler<InventoryCheckingEventArgs>(inventoryCheckingView_FillProductMasterToComboEvent);
                inventoryCheckingView.LoadGoodsByProductIdEvent += new EventHandler<InventoryCheckingEventArgs>(inventoryCheckingView_LoadGoodsByProductIdEvent);
                inventoryCheckingView.SaveInventoryCheckingEvent += new EventHandler<InventoryCheckingEventArgs>(inventoryCheckingView_SaveInventoryCheckingEvent);
            }
        }

        void inventoryCheckingView_SaveInventoryCheckingEvent(object sender, InventoryCheckingEventArgs e)
        {
           if(e.SaveStockList!=null && e.SaveStockList.Count > 0)
           {
               
               foreach (Stock stock in e.SaveStockList)
               {
                   stock.CreateDate = DateTime.Now;
                   stock.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                   stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                   stock.UpdateDate = DateTime.Now;
                   stock.DelFlg = 0;
                   
                   // calculate business
                   StockLogic.Update(stock);

                   e.HasErrors = false;
                   
               }
           }
        }

        void inventoryCheckingView_LoadGoodsByProductIdEvent(object sender, InventoryCheckingEventArgs e)
        {
            ObjectCriteria objectCriteria  = new ObjectCriteria();
            objectCriteria.AddEqCriteria("Product.ProductId", e.InputBarcode);
            objectCriteria.AddEqCriteria("DelFlg", (long)0);
            IList stockList = StockLogic.FindAll(objectCriteria);
            if(stockList != null && stockList.Count > 0)
            {
                e.ScannedStock = (Stock)stockList[0];    
            }
            else
            {
                e.ScannedStock = null;
            }
            IList stockDefectList = StockLogic.FindAll(objectCriteria);
            if(stockDefectList!=null && stockDefectList.Count > 0)
            {
                e.ScannedStock = (Stock)stockDefectList[0];
            }
            else
            {
                e.ScannedStock = null;
            }
        }

        void inventoryCheckingView_FillProductMasterToComboEvent(object sender, InventoryCheckingEventArgs e)
        {
            IList stockList = StockLogic.FindByProductMasterName();
            IList stockViewList = new ArrayList();
            if(stockList!=null)
            {
                foreach (IList list in stockList)
                {
                    StockView stockView = new StockView();
                    stockView.ProductMaster = (ProductMaster) list[0];
                    stockView.StockQuantity = (long) list[1];
                    stockViewList.Add(stockView);
                }
                
            }
            e.ReturnStockViewList = stockViewList;
        }

        public IStockLogic StockLogic
        {
            get;set;
        }

        public IProductLogic ProductLogic
        {
            get;set;
        }

        public IProductMasterLogic ProductMasterLogic
        {
            get;set;
            
        }

        public IStockInLogic StockInLogic
        {
            get;set;
            
        }

        public IStockInDetailLogic StockInDetailLogic
        {
            get;set;
            
        }

        #endregion

        #region IInventoryCheckingController Members


        public IStockOutLogic StockOutLogic
        {
            get;set;
            
        }

        public IStockOutDetailLogic StockOutDetailLogic
        {
            get;set;
        }

        #endregion

        #region IInventoryCheckingController Members
        

        #endregion
    }
}
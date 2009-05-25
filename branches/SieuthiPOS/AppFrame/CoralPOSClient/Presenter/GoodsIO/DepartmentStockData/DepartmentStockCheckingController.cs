﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Exceptions;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;
using CoralPOS.Interfaces.Presenter.GoodsIO.MainStock;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;
using NHibernate.Criterion;
using ArrayList = System.Collections.ArrayList;

namespace CoralPOS.Presenter.GoodsIO.DepartmentStockData
{
    public class DepartmentStockCheckingController : IDepartmentStockCheckingController
    {
        #region IInventoryCheckingController Members

        IDepartmentStockCheckingView departmentStockCheckingView;
        public IDepartmentStockCheckingView DepartmentStockCheckingView
        {
            get
            {
                return departmentStockCheckingView;
            }
            set
            {
                departmentStockCheckingView = value;

                departmentStockCheckingView.LoadGoodsByProductIdEvent += new EventHandler<DepartmentStockCheckingEventArgs>(inventoryCheckingView_LoadGoodsByProductIdEvent);
                departmentStockCheckingView.SaveInventoryCheckingEvent += new EventHandler<DepartmentStockCheckingEventArgs>(inventoryCheckingView_SaveInventoryCheckingEvent);
                departmentStockCheckingView.SaveTempInventoryCheckingEvent += new EventHandler<DepartmentStockCheckingEventArgs>(departmentStockCheckingView_SaveTempInventoryCheckingEvent);
                departmentStockCheckingView.LoadTempInventoryCheckingEvent += new EventHandler<DepartmentStockCheckingEventArgs>(departmentStockCheckingView_LoadTempInventoryCheckingEvent);
            }
        }

        void departmentStockCheckingView_LoadTempInventoryCheckingEvent(object sender, DepartmentStockCheckingEventArgs e)
        {
            
        }

        void departmentStockCheckingView_SaveTempInventoryCheckingEvent(object sender, DepartmentStockCheckingEventArgs e)
        {
            
        }

        void inventoryCheckingView_SaveInventoryCheckingEvent(object sender, DepartmentStockCheckingEventArgs e)
        {
            if (e.SaveStockList != null && e.SaveStockList.Count > 0)
            {

                
                foreach (DepartmentStock stock in e.SaveStockList)
                {
                    if(    stock.DamageQuantity == 0 && stock.OldDamageQuantity == 0 
                        && stock.ErrorQuantity == 0 && stock.OldErrorQuantity == 0 
                        && stock.LostQuantity == 0 && stock.OldLostQuantity == 0 
                        && stock.UnconfirmQuantity == 0 && stock.OldUnconfirmQuantity == 0 )
                    {
                        continue;
                    }

                    stock.CreateDate = DateTime.Now;
                    stock.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    stock.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    stock.UpdateDate = DateTime.Now;
                    stock.DelFlg = 0;

                    DepartmentStockLogic.Update(stock); 
                }
                
                

                    
            }
        }

        void inventoryCheckingView_LoadGoodsByProductIdEvent(object sender, DepartmentStockCheckingEventArgs e)
        {
            ObjectCriteria objectCriteria = new ObjectCriteria();
            objectCriteria.AddEqCriteria("DepartmentStockPK.ProductId", e.InputBarcode);
            objectCriteria.AddEqCriteria("DepartmentStockPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
            objectCriteria.AddEqCriteria("DelFlg", (long)0);
            System.Collections.IList stockList = DepartmentStockLogic.FindAll(objectCriteria);
            if (stockList != null && stockList.Count > 0)
            {
                e.ScannedStock = (DepartmentStock)stockList[0];
            }
            else
            {
                e.ScannedStock = null;
            }
        }


        public Logic.IDepartmentStockLogic DepartmentStockLogic
        {
            get;
            set;
        }

        public Logic.IProductLogic ProductLogic
        {
            get;
            set;
        }

        public Logic.IProductMasterLogic ProductMasterLogic
        {
            get;
            set;

        }

        public Logic.IDepartmentStockInLogic DepartmentStockInLogic
        {
            get;
            set;

        }

        public Logic.IDepartmentStockInDetailLogic DepartmentStockInDetailLogic
        {
            get;
            set;

        }

        #endregion

        #region IInventoryCheckingController Members


        public Logic.IDepartmentStockOutLogic DepartmentStockOutLogic
        {
            get;
            set;

        }

        public Logic.IDepartmentStockOutDetailLogic DepartmentStockOutDetailLogic
        {
            get;
            set;
        }

        public IDepartmentStockTempLogic DepartmentStockTempLogic
        {
            get; set;
        }

        #endregion

        
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Model;
using AppFrame.Presenter.GoodsIO.MainStock;
using AppFrame.Utility;
using AppFrame.View.GoodsIO.MainStock;
using AppFrameClient.Common;
using AppFrameClient.View.GoodsIO.DepartmentStockData;
using AppFrameClient.ViewModel;

namespace AppFrameClient.View.GoodsIO.MainStock
{
    public partial class InventoryCheckingForm : AppFrame.Common.BaseForm,IInventoryCheckingView
    {
        private StockViewCollection stockViewList = null;
        private StockDefectCollection stockDefectList = null;
        public InventoryCheckingForm()
        {
            InitializeComponent();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            LoadGoodsByProductId(txtBarcode.Text.Trim());
        }

        #region IInventoryCheckingView Members

        public event EventHandler<AppFrame.Presenter.GoodsIO.MainStock.InventoryCheckingEventArgs> FillProductMasterToComboEvent;

        public event EventHandler<AppFrame.Presenter.GoodsIO.MainStock.InventoryCheckingEventArgs> LoadGoodsByProductIdEvent;

        public event EventHandler<AppFrame.Presenter.GoodsIO.MainStock.InventoryCheckingEventArgs> SaveInventoryCheckingEvent;

        private AppFrame.Presenter.GoodsIO.MainStock.IInventoryCheckingController inventoryCheckingController;
        public AppFrame.Presenter.GoodsIO.MainStock.IInventoryCheckingController InventoryCheckingController
        {
            get
            {
                return inventoryCheckingController;
            }
            set
            {
                inventoryCheckingController = value;
                inventoryCheckingController.InventoryCheckingView = this;
            }
        }

        #endregion

        private void InventoryCheckingForm_Load(object sender, EventArgs e)
        {
            stockViewList = new StockViewCollection(bdsProductMasters);
            bdsProductMasters.DataSource = stockViewList;
            stockDefectList = new StockDefectCollection(bdsStockDefect);
            bdsStockDefect.DataSource = stockDefectList;
            //cboProductMasters.DisplayMember = "ProductMaster.TypeAndName";
            //LoadGoodsToCombo();

        }

        private void LoadGoodsToCombo()
        {
            InventoryCheckingEventArgs checkingEventArgs = new InventoryCheckingEventArgs();
            EventUtility.fireEvent(FillProductMasterToComboEvent,this,checkingEventArgs);
            stockViewList.Clear();
            foreach(var obj in checkingEventArgs.ReturnStockViewList)
            {
                stockViewList.Add((StockView)obj);                
            }
            bdsProductMasters.EndEdit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
             Close();
        }

        private ProductMasterSearchDepartmentForm form;
        private void button2_Click(object sender, EventArgs e)
        {
            form = GlobalUtility.GetOnlyChildFormObject<ProductMasterSearchDepartmentForm>(GlobalCache.Instance().MainForm, FormConstants.PRODUCT_MASTER_SEARCH_DEPARMENT_FORM);
            form.SelectProductEvent += new EventHandler<AppFrame.Presenter.GoodsIO.ProductMasterSearchDepartmentEventArgs>(form_SelectProductEvent);
            if (form != null)
                form.Show();
        }

        void form_SelectProductEvent(object sender, AppFrame.Presenter.GoodsIO.ProductMasterSearchDepartmentEventArgs e)
        {
            LoadGoodsByProductId(e.ReturnProduct.ProductId);
            
        }

        private void LoadGoodsByProductId(string id)
        {
            InventoryCheckingEventArgs checkingEventArgs = new InventoryCheckingEventArgs();
            checkingEventArgs.InputBarcode = id;
            EventUtility.fireEvent(LoadGoodsByProductIdEvent, this, checkingEventArgs);
            Stock stock = checkingEventArgs.ScannedStock;
            if(stock == null)
            {
                MessageBox.Show("Không tìm thấy mã vạch trong kho", "Lỗi");
                return;
            }

            txtProductType.Text = stock.ProductMaster.ProductType.TypeName;
            txtProductName.Text = stock.ProductMaster.ProductName;
            txtStockQuantity.Text = stock.Quantity.ToString("##,##0");
            txtDescription.Text = stock.ProductMaster.Description;


            int stockDefIndex = -1;
            if(HasInStockDefectList(stock,stockDefectList,out stockDefIndex))
            {
                if(stockDefIndex> -1 && stockDefIndex < stockDefectList.Count)
                {
                    stockDefectList[stockDefIndex].GoodCount += 1;                                    
                }
                
            }
            else // create new stock defect row
            {
                StockDefect newStockDefect = stockDefectList.AddNew();
                newStockDefect.Product = stock.Product;
                newStockDefect.ProductMaster = stock.ProductMaster;
                newStockDefect.Stock = stock;
                newStockDefect.Quantity = stock.Quantity;
                newStockDefect.GoodCount += 1;
            }
            bdsStockDefect.EndEdit();
            dgvStock.Refresh();
            dgvStock.Invalidate();
        }

        private bool HasInStockDefectList(Stock stock, StockDefectCollection list, out int stockDefIndex)
        {
            int count = 0;
            foreach (StockDefect stockDefect in list)
            {
                if(stockDefect.Stock.StockId == stock.StockId)
                {
                    stockDefIndex = count;
                    return true;
                }
                count += 1;
            }
            stockDefIndex = count;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InventoryCheckingEventArgs checkingEventArgs = new InventoryCheckingEventArgs();
            checkingEventArgs.SaveStockDefectList = ObjectConverter.ConvertToNonGenericList(stockDefectList);

            EventUtility.fireEvent(SaveInventoryCheckingEvent,this,checkingEventArgs);
        }
    }
}

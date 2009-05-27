﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter.GoodsIO.DepartmentGoodsIO;
using AppFrame.Utility;
using CoralPOS.Interfaces.View.GoodsIO.DepartmentGoodsIO;

namespace CoralPOSClient.View.GoodsIO.DepartmentStockData
{
    public partial class DepartmentPriceUpdateForm : BaseForm, IDepartmentPriceUpdateView
    {
        #region Members
        private const int MAX_COLUMNS = 11;
        public static readonly int PRODUCT_ID_POS = 0;
        public static readonly int PRODUCT_UPDATE_DATE_POS = 1;
        public static readonly int PRODUCT_OLD_PRICE_POS = 2;
        public static readonly int PRODUCT_NEW_PRICE_POS = 3;
        public static readonly int PRODUCT_NAME_POS = 4;
        public static readonly int PRODUCT_TYPE_POS = 5;
        public static readonly int PRODUCT_COLOR_POS = 6;
        public static readonly int PRODUCT_SIZE_POS = 7;
        public static readonly int PRODUCT_COUNTRY_POS = 8;
        public static readonly int PRODUCT_SUPPLIER_POS = 9;
        public static readonly int PRODUCT_MANUFACTURER_POS = 10;
        private readonly DataTable dataTable = new DataTable();
        public IList DepartmentPriceList { get; set; }
        public DepartmentPriceUpdateEventArgs _currentEventArgs { get; set; }
        private IDepartmentPriceUpdateController _departmentPriceUpdateController;
        public IDepartmentPriceUpdateController DepartmentPriceUpdateController
        {
            set
            {
                _departmentPriceUpdateController = value;
                _departmentPriceUpdateController.DepartmentPriceUpdateView = this;
            }
            get
            {
                return _departmentPriceUpdateController;
            }
        }
        public event EventHandler<DepartmentPriceUpdateEventArgs> SaveDepartmentPriceEvent;
        public event EventHandler<DepartmentPriceUpdateEventArgs> SearchDepartmentPriceEvent;
        public event EventHandler<DepartmentPriceUpdateEventArgs> InitDepartmentPriceEvent;
        #endregion

        public DepartmentPriceUpdateForm()
        {
            InitializeComponent();
            dataTable.Columns.Add("Mã sản phẩm");
            dataTable.Columns.Add("Ngày cập nhật");
            dataTable.Columns.Add("Giá cũ");
            dataTable.Columns.Add("Giá mới");
            dataTable.Columns.Add("Tên mặt hàng");
            dataTable.Columns.Add("Chủng loại");
            dataTable.Columns.Add("Màu sắc");
            dataTable.Columns.Add("Kích cỡ");
            dataTable.Columns.Add("Xuất xứ");
            dataTable.Columns.Add("Nhà cung cấp");
            dataTable.Columns.Add("Nhà sản xuất");
        }

        private void DepartmentPriceUpdateForm_Load(object sender, EventArgs e)
        {
            var eventArgs = new DepartmentPriceUpdateEventArgs();
            EventUtility.fireEvent(InitDepartmentPriceEvent, this, eventArgs);

            productMasterSearchControl.cbbProductType.DataSource = eventArgs.ProductTypeList;
            productMasterSearchControl.cbbProductType.DisplayMember = "TypeName";

            productMasterSearchControl.cbbProductSize.DataSource = eventArgs.ProductSizeList;
            productMasterSearchControl.cbbProductSize.DisplayMember = "SizeName";

            productMasterSearchControl.cbbProductColor.DataSource = eventArgs.ProductColorList;
            productMasterSearchControl.cbbProductColor.DisplayMember = "ColorName";

            productMasterSearchControl.cbbCountry.DataSource = eventArgs.CountryList;
            productMasterSearchControl.cbbCountry.DisplayMember = "CountryName";

            productMasterSearchControl.cbbDistributor.DataSource = eventArgs.DistributorList;
            productMasterSearchControl.cbbDistributor.DisplayMember = "DistributorName";

            productMasterSearchControl.cbbManufacturer.DataSource = eventArgs.ManufacturerList;
            productMasterSearchControl.cbbManufacturer.DisplayMember = "ManufacturerName";

            productMasterSearchControl.cbbDistributor.DataSource = eventArgs.DistributorList;
            productMasterSearchControl.cbbDistributor.DisplayMember = "DistributorName";
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var eventArgs = new DepartmentPriceUpdateEventArgs
                                {
                                    ProductMasterId = productMasterSearchControl.txtProductMasterId.Text,
                                    ProductMasterName = productMasterSearchControl.txtProductName.Text,
                                    ProductSize = productMasterSearchControl.cbbProductSize.SelectedIndex > 0 ? ((ProductSize)productMasterSearchControl.cbbProductSize.SelectedItem) : null,
                                    ProductType = productMasterSearchControl.cbbProductType.SelectedIndex > 0 ? ((ProductType)productMasterSearchControl.cbbProductType.SelectedItem) : null,
                                    ProductColor = productMasterSearchControl.cbbProductColor.SelectedIndex > 0 ?
                                                                                                                    ((ProductColor)productMasterSearchControl.cbbProductColor.SelectedItem) : null,
                                    Country = productMasterSearchControl.cbbCountry.SelectedIndex > 0 ? ((Country)productMasterSearchControl.cbbCountry.SelectedItem) : null,
                                    Manufacturer = productMasterSearchControl.cbbManufacturer.SelectedIndex > 0 ? ((Manufacturer)productMasterSearchControl.cbbManufacturer.SelectedItem) : null,
                                    Distributor = productMasterSearchControl.cbbDistributor.SelectedIndex > 0 ? ((Distributor)productMasterSearchControl.cbbDistributor.SelectedItem) : null,
                                    Packager = productMasterSearchControl.cbbPackager.SelectedIndex > 0 ? ((Packager)productMasterSearchControl.cbbPackager.SelectedItem) : null
                                };
            EventUtility.fireEvent(SearchDepartmentPriceEvent, this, eventArgs);

            DepartmentPriceList = eventArgs.DepartmentPriceList;
            _currentEventArgs = eventArgs;
            PopulateDataGrid();

            DateTime dateTime = new DateTime(2008, 10, 10);
            long l = dateTime.Ticks;
        }

        private void PopulateDataGrid()
        {
            dataTable.Rows.Clear();
            for (int i = 0; i < DepartmentPriceList.Count; i++)
            {
                var result = (DepartmentPrice)DepartmentPriceList[i];
                dataTable.Rows.Add(AddProductToDataGrid(result));

            }
            dgvProduct.DataSource = dataTable;
            dgvProduct.Columns[PRODUCT_OLD_PRICE_POS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProduct.Columns[PRODUCT_NEW_PRICE_POS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            for (int i = 0; i < dgvProduct.Columns.Count; i++ )
            {
                if (i != PRODUCT_NEW_PRICE_POS)
                {
                    dgvProduct.Columns[i].ReadOnly = true;
                }
            }
            dgvProduct.Refresh();
        }

        private static object[] AddProductToDataGrid(DepartmentPrice stockInDetail)
        {
            var obj = new object[MAX_COLUMNS];
            obj[PRODUCT_ID_POS] = stockInDetail.ProductMaster.ProductMasterId;
            obj[PRODUCT_UPDATE_DATE_POS] = stockInDetail.UpdateDate.ToString("dd/MM/yyyy HH:mm:ss");
            obj[PRODUCT_OLD_PRICE_POS] = stockInDetail.Price.ToString("#,##", CultureInfo.CreateSpecificCulture("de-DE"));
            obj[PRODUCT_ID_POS] = stockInDetail.ProductMaster.ProductMasterId;
            obj[PRODUCT_NAME_POS] = stockInDetail.ProductMaster.ProductName;
            if (stockInDetail.ProductMaster.ProductType != null)
            {
                obj[PRODUCT_TYPE_POS] = stockInDetail.ProductMaster.ProductType.TypeName;
            }
            if (stockInDetail.ProductMaster.ProductSize != null)
            {
                obj[PRODUCT_SIZE_POS] = stockInDetail.ProductMaster.ProductSize.SizeName;
            }
            if (stockInDetail.ProductMaster.ProductColor != null)
            {
                obj[PRODUCT_COLOR_POS] = stockInDetail.ProductMaster.ProductColor.ColorName;    
            }
            if (stockInDetail.ProductMaster.Country != null)
            {
                obj[PRODUCT_COUNTRY_POS] = stockInDetail.ProductMaster.Country.CountryName;
            }
            if (stockInDetail.ProductMaster.Manufacturer != null)
            {
                obj[PRODUCT_MANUFACTURER_POS] = stockInDetail.ProductMaster.Manufacturer.ManufacturerName;
            }
            if (stockInDetail.ProductMaster.Distributor != null)
            {
                obj[PRODUCT_SUPPLIER_POS] = stockInDetail.ProductMaster.Distributor.DistributorName;
            }

            return obj;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DepartmentPriceList != null && DepartmentPriceList.Count > 0)
            {
                IList updatePriceList = new ArrayList();
                for (int i = 0; i < dgvProduct.RowCount; i++)
                {
                    foreach (DepartmentPrice oldPrice in DepartmentPriceList)
                    {
                        if (oldPrice.ProductMaster.ProductMasterId.Equals(dgvProduct[PRODUCT_ID_POS, i].Value.ToString()))
                        {
                            long newPrice = 0;
                            // validate input
                            if (!NumberUtility.CheckLongNullIsZero(dgvProduct[PRODUCT_NEW_PRICE_POS, i].Value, out newPrice))
                            {
                                MessageBox.Show("Error at line: " + i + "");
                                return;
                            }
                            else if (newPrice != 0 && oldPrice.Price != newPrice)
                            {
                                oldPrice.Price = newPrice;
                                updatePriceList.Add(oldPrice);
                                break;
                            }
                        }
                    }
                }

                if (updatePriceList.Count > 0)
                {
                    var eventArgs = new DepartmentPriceUpdateEventArgs {DepartmentPriceList = updatePriceList};
                    EventUtility.fireEvent(SaveDepartmentPriceEvent, this, eventArgs);
                    if (_currentEventArgs != null)
                    {
                        EventUtility.fireEvent(SearchDepartmentPriceEvent, this, _currentEventArgs);
                        DepartmentPriceList = _currentEventArgs.DepartmentPriceList;
                        PopulateDataGrid();
                    }
                    MessageBox.Show("Thay đổi giá thành công");
                }
                else
                {
                    MessageBox.Show("Nothing to update");
                }
            }
        }

        private void systemHotkey1_Pressed(object sender, EventArgs e)
        {
            if(dgvProduct.CurrentCell==null)
            {
                return;
            }
            Clipboard.SetText(dgvProduct.CurrentCell.Value.ToString());
        }

        private void systemHotkey3_Pressed(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void systemHotkey2_Pressed(object sender, EventArgs e)
        {
            if(dgvProduct.Focused)
            {
                DataGridViewSelectedCellCollection collection = dgvProduct.SelectedCells;
                foreach (DataGridViewCell cell in collection)
                {
                    cell.Value = Clipboard.GetText();
                }
            }
        }
    }
}
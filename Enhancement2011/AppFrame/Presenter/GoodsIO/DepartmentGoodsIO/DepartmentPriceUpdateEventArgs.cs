﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using System.Collections;
using System.Windows.Forms;
using AppFrame.Model;

namespace AppFrame.Presenter.GoodsIO.DepartmentGoodsIO
{
    public class DepartmentPriceUpdateEventArgs : BaseEventArgs
    {
        public bool ZeroWholeSalePrice { get; set;}
        public bool ZeroPrice { get; set;}
        public Form ParentForm { get; set; }
        public Department Department { get; set; }
        public bool AbsoluteFinding { get; set; }
        public IList DepartmentPriceList { get; set; }

        public string ProductMasterId { get; set; }
        public string ProductMasterName { get; set; }
        public ProductType ProductType { get; set; }
        public ProductSize ProductSize { get; set; }
        public ProductColor ProductColor { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Distributor Distributor { get; set; }
        public Packager Packager { get; set; }
        public Country Country { get; set; }

        public IList CountryList { get; set; }
        public IList ProductTypeList { get; set; }
        public IList ProductSizeList { get; set; }
        public IList ProductColorList { get; set; }
        public IList ManufacturerList { get; set; }
        public IList DistributorList { get; set; }
        public IList PackagerList { get; set; }
        public IList StockList { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using System.Collections;
using System.Windows.Forms;
using AppFrame.Model;

namespace AppFrame.Presenter.GoodsIO
{
    public class StockSearchEventArgs : BaseEventArgs
    {
        public string ProductId { get; set;}
        public string Description { get; set;}
        public Form ParentForm { get; set; }
        public IList CountryList { get; set; }
        public IList ProductTypeList { get; set; }
        public IList ProductSizeList { get; set; }
        public IList ProductColorList { get; set; }
        public IList ManufacturerList { get; set; }
        public IList DistributorList { get; set; }
        public IList PackagerList { get; set; }
        public IList StockList { get; set; }

        public string ProductMasterId { get; set; }
        public string ProductMasterName { get; set; }
        public ProductType ProductType { get; set; }
        public ProductSize ProductSize { get; set; }
        public ProductColor ProductColor { get; set; }
        public Country Country { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Distributor Distributor { get; set; }
        public Packager Packager { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool RelevantProductFinding { get; set; }
        public List<UniversalStockReportObject> ReportList { get; set; }
    }
}

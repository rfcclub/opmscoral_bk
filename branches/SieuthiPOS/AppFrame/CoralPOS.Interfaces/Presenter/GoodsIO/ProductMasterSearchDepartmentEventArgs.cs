using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;
using Product=CoralPOS.Interfaces.Model.Product;

namespace CoralPOS.Interfaces.Presenter.GoodsIO
{
    public class ProductMasterSearchDepartmentEventArgs : ProductMasterSearchOrCreateEventArgs
    {
        public bool AvailableInStock { get; set;}
        public ProductMaster SelectedProductMaster { get; set; }
        public Product ReturnProduct { get; set; }
        public IList ProductsInDepartment { get; set; }
    }
}
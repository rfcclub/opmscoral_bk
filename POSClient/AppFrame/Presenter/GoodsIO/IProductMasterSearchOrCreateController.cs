﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.Logic;
using AppFrame.View.GoodsIO;

namespace AppFrame.Presenter.GoodsIO
{
    public interface IProductMasterSearchOrCreateController : IBaseController<ProductMasterEventArgs>
    {
        #region View use in IProductMasterSearchCreateController
        IProductMasterSearchOrCreateView ProductMasterSearchOrCreateView { get; set; }
        #endregion

        #region Logic use in IProductMasterSearchOrCreateController
        IProductMasterLogic ProductMasterLogic { get; set; }
        ICountryLogic CountryLogic { get; set; }
        IProductColorLogic ProductColorLogic { get; set; }
        IProductTypeLogic ProductTypeLogic { get; set; }
        IProductSizeLogic ProductSizeLogic { get; set; }
        IManufacturerLogic ManufacturerLogic { get; set; }
        IDistributorLogic DistributorLogic { get; set; }
        IPackagerLogic PackagerLogic { get; set; }
        #endregion
    }
}

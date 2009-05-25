using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Utility.Mapper;

namespace CoralPOS.Utility.Mapper
{
    public class DeptRetProdStockInDetailMapper : BaseMapper<StockInDetail, DepartmentStockOutDetail>
    {
        #region Implementation of BaseMapper<StockInDetail,DepartmentStockOutDetail>

        public StockInDetail Convert(DepartmentStockOutDetail source)
        {
            StockInDetail dest = new StockInDetail();
            dest.Product = source.Product;
            dest.ProductId = source.Product.ProductId;
            dest.ProductMaster = source.Product.ProductMaster;
            
            dest.CreateDate = DateTime.Now;
            dest.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            dest.UpdateDate = DateTime.Now;
            dest.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
            
            dest.Quantity = source.Quantity;
            
            return dest;
        }

        #endregion
    }
}

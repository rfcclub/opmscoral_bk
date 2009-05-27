using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Utility.Mapper;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;
using AppFrame.Utility.Mapper;
using CoralPOS.ViewModel;

namespace CoralPOS.Utility.Mapper
{
    public class StockOutDetailMapper : BaseMapper<StockOutDetail, DepartmentStockOutDetail>
    {
        #region BaseMapper<StockOutDetail,StockOutDetailView> Members

        public StockOutDetail Convert(DepartmentStockOutDetail source)
        {
            StockOutDetail dest = new StockOutDetail();
            dest.Product = source.Product;
            dest.ProductId = source.Product.ProductId;
            dest.ProductMaster = source.Product.ProductMaster;
            dest.Description = source.Description;
            dest.DefectStatus = source.DefectStatus;

            dest.CreateDate = DateTime.Now;
            dest.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            dest.UpdateDate = DateTime.Now;
            dest.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            dest.Quantity = source.Quantity;
            dest.GoodQuantity = source.GoodQuantity;
            dest.ErrorQuantity = source.ErrorQuantity;
            dest.LostQuantity = source.LostQuantity;
            dest.DamageQuantity = source.DamageQuantity;
            dest.UnconfirmQuantity = source.UnconfirmQuantity;

            return dest;
        }

        #endregion
    }
}

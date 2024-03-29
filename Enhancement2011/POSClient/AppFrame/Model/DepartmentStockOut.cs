using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace AppFrame.Model
{
    #region DepartmentStockOut
    /// <summary>
    /// DepartmentStockOut object for NHibernate mapped table 'department_stock_out'.
    /// </summary>
    [Serializable]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DepartmentStockOut", Namespace = "http://localhost:8001/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Department))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DepartmentStockOutDetail))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProductMaster))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Product))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockOutCost))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.StockDefectStatus))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockOutPK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockOutDetailPK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockOutCostPK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentPrice))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentPricePK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.Category))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.Country))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.Distributor))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.Manufacturer))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.Packager))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.ProductColor))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.ProductSize))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.ProductType))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.BlockInDetail))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.BlockIn))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.BlockInDetailPK))]
    public class DepartmentStockOut : System.IComparable
    {
    	#region Member Variables

        protected Int64 _stockOutId; 		
        protected Int64 _departmentId; 		
        protected DateTime _stockOutDate;
        protected DateTime _createDate;
        protected string _createId;
        protected DateTime _updateDate;
        protected string _updateId;
        protected Int64 _exclusiveKey;
        protected Int64 _delFlg;
        // DepartmentStockOutCost
        protected IList _departmentStockOutCosts = new ArrayList();
        // DepartmentStockOutDetail
        protected IList _departmentStockOutDetails = new ArrayList();

        #endregion

        #region Constructors

        public DepartmentStockOut () 
        {
        }
        
        #endregion

        #region Public Properties

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Xuất kho cửa hàng: ").Append(DepartmentStockOutPK.StockOutId).Append(" ngày: ").Append(StockOutDate).Append("\r\n").Append("Chi tiết:\r\n");
            if (DepartmentStockOutDetails != null && DepartmentStockOutDetails.Count > 0)
            {
                foreach (DepartmentStockOutDetail detail in DepartmentStockOutDetails)
                {
                    sb.Append("Tên sản phẩm: ").Append(detail.Product.ProductMaster.ProductName).Append(" ")
                        .Append(", mã vạch: ").Append(detail.Product.ProductId).Append(", số lượng:").Append(
                        detail.Quantity).Append("\r\n");
                }
            }
            return sb.ToString();
        }
        [DataMember]
        public virtual DepartmentStockOutPK DepartmentStockOutPK { get; set; }
		
        public virtual Int64 StockOutId
        {
            get
            {
                return _stockOutId;
            }
            set
            {
                _stockOutId = value;
            }
        }
        [DataMember]
        public virtual Int64 DepartmentId
        {
            get
            {
                return _departmentId;
            }
            set
            {
                _departmentId = value;
            }
        }
        [DataMember]
        public virtual DateTime StockOutDate
        {
            get
            {
                return _stockOutDate;
            }
            set
            {
                _stockOutDate = value;
            }
        }
        [DataMember]
        public virtual DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }
        [DataMember]
        public virtual string CreateId
        {
            get
            {
                return _createId;
            }
            set
            {
                _createId = value;
            }
        }
        [DataMember]
        public virtual DateTime UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
            }
        }
        [DataMember]
        public virtual string UpdateId
        {
            get
            {
                return _updateId;
            }
            set
            {
                _updateId = value;
            }
        }
        [DataMember]
        public virtual Int64 ExclusiveKey
        {
            get
            {
                return _exclusiveKey;
            }
            set
            {
                _exclusiveKey = value;
            }
        }
        [DataMember]
        public virtual Int64 DelFlg
        {
            get
            {
                return _delFlg;
            }
            set
            {
                _delFlg = value;
            }
        }

        
        // DepartmentStockOutCost
        
        public virtual IList DepartmentStockOutCosts
        {
            get
            {
                return _departmentStockOutCosts;
            }
            set
            {
                _departmentStockOutCosts = value;
            }
        }
        
        
        // DepartmentStockOutDetail
        [DataMember]
        public virtual IList DepartmentStockOutDetails
        {
            get
            {
                return _departmentStockOutDetails;
            }
            set
            {
                _departmentStockOutDetails = value;
            }
        }

        [DataMember]
        public virtual StockDefectStatus DefectStatus { get; set; }
        [DataMember]
        public virtual Int64 ConfirmFlg { get; set; }
        [DataMember]
        public virtual Int64 OtherDepartmentId { get; set; }
        #endregion
        
        #region IComparable Methods
        
        public virtual int CompareTo(object obj)
        {
            return 0;
        }
        
        #endregion
        
        #region Equals and GetHashCode Methods
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return base.Equals(obj);
            
        }

		// override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
    #endregion
}
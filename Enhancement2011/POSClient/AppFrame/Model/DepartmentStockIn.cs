using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace AppFrame.Model
{
    #region DepartmentStockIn
    /// <summary>
    /// DepartmentStockIn object for NHibernate mapped table 'department_stock_in'.
    /// </summary>
    [Serializable]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DepartmentStockIn", Namespace = "http://localhost:8001/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Department))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DepartmentStockInDetail))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProductMaster))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Product))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.StockDefectStatus))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockInPK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockInPK))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AppFrame.Model.DepartmentStockInCostPK))]
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
    public class DepartmentStockIn : System.IComparable//, ISerializable
    {
    	#region Member Variables

        protected Int64 _stockInId; 		
        protected Int64 _departmentId; 		
        protected DateTime _stockInDate;
        protected DateTime _createDate;
        protected string _createId;
        protected DateTime _updateDate;
        protected string _updateId;
        protected Int64 _exclusiveKey;
        protected Int64 _delFlg;
        // DepartmentStockInDetail
        protected IList _departmentStockInDetails = new ArrayList();
        // DepartmentStockInCost
        protected IList _departmentStockInCosts = new ArrayList();

        #endregion

        #region Constructors

        public DepartmentStockIn () 
        {
        }
        
        #endregion

        #region Public Properties
        [DataMember]
		public virtual DepartmentStockInPK DepartmentStockInPK { get; set; }
        [DataMember]
        public virtual Department Department { get; set; }
        [DataMember]
        public virtual Int64 ExportStatus { get; set; }
        [DataMember]
        public virtual Int64 StockInCost { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual Int64 StockInId
        {
            get
            {
                return _stockInId;
            }
            set
            {
                _stockInId = value;
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
        public virtual DateTime StockInDate
        {
            get
            {
                return _stockInDate;
            }
            set
            {
                _stockInDate = value;
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

        
        // DepartmentStockInDetail
        [DataMember]
        public virtual IList DepartmentStockInDetails
        {
            get
            {
                return _departmentStockInDetails;
            }
            set
            {
                _departmentStockInDetails = value;
            }
        }
        
        
        // DepartmentStockInCost
        [DataMember]
        public virtual IList DepartmentStockInCosts
        {
            get
            {
                return _departmentStockInCosts;
            }
            set
            {
                _departmentStockInCosts = value;
            }
        }
        [DataMember]
        public virtual long StockInType { get; set; }
        public override string ToString()
        {
            if(DepartmentStockInPK == null)
            {
                
            }
            StringBuilder sb = new StringBuilder("Nhập kho cửa hàng: ").Append(" ngày: ").Append(StockInDate.ToString("dd/MM/yyyy hh:mm:ss")).Append("\r\n").Append("Chi tiết:\r\n");
            if (DepartmentStockInDetails != null && DepartmentStockInDetails.Count > 0)
            {
                foreach (DepartmentStockInDetail detail in DepartmentStockInDetails)
                {
                    sb.Append("Tên sản phẩm: ").Append(detail.Product.ProductMaster.ProductName).Append(" ")
                        .Append(", mã vạch: ").Append(detail.Product.ProductId).Append(", số lượng:").Append(
                        detail.Quantity).Append("\r\n");
                }
            }
            return sb.ToString();
        }

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
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

		public virtual DepartmentStockInPK DepartmentStockInPK { get; set; }
        public virtual Department Department { get; set; }
        public virtual Int64 ExportStatus { get; set; }
        public virtual Int64 StockInCost { get; set; }

        public virtual string Description { get; set; }

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
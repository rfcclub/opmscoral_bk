using System;
using System.Collections;
using System.Text;

namespace AppFrame.Model
{
    #region DepartmentPrice
    /// <summary>
    /// DepartmentCost object for NHibernate mapped table 'department_cost'.
    /// </summary>
    [Serializable]
    public class DepartmentPrice : System.IComparable
    {
    	#region Member Variables

        protected Int64 _price;
        protected DateTime _createDate;
        protected string _createId;
        protected DateTime _updateDate;
        protected string _updateId;
        protected Int64 _exclusiveKey;
        protected Int64 _delFlg;

        #endregion

        #region Constructors

        public DepartmentPrice() 
        {
        }
        
        #endregion

        #region Public Properties

        public virtual DepartmentPricePK DepartmentPricePK { get; set; }
        public virtual Department Department { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
		
        public virtual Int64 Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Cập nhật giá cho sản phẩm: ").Append(ProductMaster.ProductName).
                Append(" mã vạch: ").Append(ProductMaster.ProductMasterId).Append(" , giá mới: ").Append(Price);
            return sb.ToString();
        }
        #endregion
    }
    #endregion
}
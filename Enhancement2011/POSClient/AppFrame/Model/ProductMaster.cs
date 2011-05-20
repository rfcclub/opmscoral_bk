using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using AppFrame.Common;
using AppFrame.Controls;

namespace AppFrame.Model
{
    #region ProductMaster
    /// <summary>
    /// ProductMaster object for NHibernate mapped table 'product_master'.
    /// </summary>
    [TypeDescriptionProvider(typeof(ComplexCustomDescriptionProvider<ProductMaster>))]
    [Serializable]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ProductMaster", Namespace = "http://localhost:8001/")]
    public class ProductMaster : ClonableObject,System.IComparable
    {
    	#region Member Variables

        protected string _productMasterId; 		
        protected string _productName;
        protected string _description;
        protected Int64 _supplierId;
        protected DateTime _createDate;
        protected string _createId;
        protected DateTime _updateDate;
        protected string _updateId;
        protected Int64 _exclusiveKey;
        protected Int64 _delFlg;
        protected Packager _packager;
        protected ProductColor _productColor;
        protected Country _country;
        protected Category _category;
        protected Manufacturer _manufacturer;
        protected ProductSize _productSize;
        // Product
        protected IList _products = new ArrayList();
        protected Distributor _distributor;
        protected ProductType _productType;
        protected string _productFullName;

        #endregion

        #region Constructors

        public ProductMaster () 
        {
        }
        
        #endregion

        #region Public Properties
        [DataMember]
        public virtual string ImagePath { get; set; }
        public virtual string TypeAndName { 
            get
            {
                if (ProductType != null)
                {
                    return ProductType.TypeName + " - " + ProductName;
                }
                return ProductName;
            }
        }
        
        public virtual string ProductFullName
        {
            get
            {

                _productFullName = ProductMasterId + "  " + ProductName + " Màu:" + ProductColor.ColorName + " Cỡ:" + ProductSize.SizeName;
                return _productFullName;
            }
            set
            {
                _productFullName = value;                
            }
            
        }
        [DataMember]
        public virtual string Barcode { get; set;}
        [DataMember]
        public virtual string ProductMasterId
        {
            get
            {
                return _productMasterId;
            }
            set
            {
                _productMasterId = value;
            }
        }
        [DataMember]
        public virtual string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
            }
        }
        [DataMember]
        public virtual string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        [DataMember]
        public virtual Int64 SupplierId
        {
            get
            {
                return _supplierId;
            }
            set
            {
                _supplierId = value;
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

        [DataMember]
        public virtual Packager Packager
        {
            get
            {
                return _packager;
            }
            set
            {
                _packager = value;
            }
        }

        [DataMember]
        public virtual ProductColor ProductColor
        {
            get
            {
                return _productColor;
            }
            set
            {
                _productColor = value;
            }
        }

        [DataMember]
        public virtual Country Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        [DataMember]
        public virtual Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        [DataMember]
        public virtual Manufacturer Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                _manufacturer = value;
            }
        }

        [DataMember]
        public virtual ProductSize ProductSize
        {
            get
            {
                return _productSize;
            }
            set
            {
                _productSize = value;
            }
        }
        
        
        // Product
        /*public virtual IList Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
            }
        }*/

        [DataMember]
        public virtual Distributor Distributor
        {
            get
            {
                return _distributor;
            }
            set
            {
                _distributor = value;
            }
        }

        [DataMember]
        public virtual ProductType ProductType
        {
            get
            {
                return _productType;
            }
            set
            {
                _productType = value;
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
            StringBuilder sb = new StringBuilder();
            sb.Append("Tên sản phẩm: ").Append(ProductName).Append("\r\n").
                Append("    ").Append("Chủng loại: ").Append(ProductType != null ? ProductType.TypeName : "").Append(" ").Append("\r\n").
                Append("    ").Append("Màu sắc: ").Append(ProductColor != null ? ProductColor.ColorName : "").Append(" ").Append("\r\n").
                Append("    ").Append("Kích cỡ: ").Append(ProductSize != null ? ProductSize.SizeName : "").Append(" ").Append("\r\n").
                Append("    ").Append("Xuất xứ: ").Append(Country != null ? Country.CountryName : "").Append(" ");
            return sb.ToString();
        }
        #endregion
    }
    #endregion
}
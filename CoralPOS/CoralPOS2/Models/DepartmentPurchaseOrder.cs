using System; 
using System.Collections.Generic; 
using System.Text; 


namespace CoralPOS.Models {
    
    
    [Serializable()]
    public class DepartmentPurchaseOrder {
        
        public DepartmentPurchaseOrder() {
        }
        
        public virtual DepartmentPurchaseOrderPK DepartmentPurchaseOrderPK {
            get;
            set;
        }
        
        public virtual System.DateTime CreateDate {
            get;
            set;
        }
        
        public virtual string CreateId {
            get;
            set;
        }
        
        public virtual long CustomerId {
            get;
            set;
        }
        
        public virtual long DelFlg {
            get;
            set;
        }
        
        public virtual long ExFld1 {
            get;
            set;
        }
        
        public virtual long ExFld2 {
            get;
            set;
        }
        
        public virtual long ExFld3 {
            get;
            set;
        }
        
        public virtual string ExFld4 {
            get;
            set;
        }
        
        public virtual string ExFld5 {
            get;
            set;
        }
        
        public virtual long ExclusiveKey {
            get;
            set;
        }
        
        public virtual long OrderStatus {
            get;
            set;
        }
        
        public virtual string PurchaseDescription {
            get;
            set;
        }
        
        public virtual long PurchasePrice {
            get;
            set;
        }
        
        public virtual System.DateTime UpdateDate {
            get;
            set;
        }
        
        public virtual string UpdateId {
            get;
            set;
        }
        
        public virtual IList<DepartmentPurchaseOrderDetail> DepartmentPurchaseOrderDetails {
            get;
            set;
        }
   	 protected bool Equals(DepartmentPurchaseOrder entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as DepartmentPurchaseOrder);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			
			return result;
		}
 }
}

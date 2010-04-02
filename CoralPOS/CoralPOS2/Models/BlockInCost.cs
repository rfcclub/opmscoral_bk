using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Text; 


namespace CoralPOS.Models {
    
    
    [Serializable()]
    public class BlockInCost :IDataErrorInfo {
        
        public BlockInCost() {
        }
        
        public virtual BlockInCostPK BlockInCostPK {
            get;
            set;
        }
        
        public virtual long Cost {
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
        
        public virtual System.DateTime UpdateDate {
            get;
            set;
        }
        
        public virtual string UpdateId {
            get;
            set;
        }
   	 protected bool Equals(BlockInCost entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as BlockInCost);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			
			return result;
		}

        public virtual string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public virtual string Error { get; private set; }
    }
}

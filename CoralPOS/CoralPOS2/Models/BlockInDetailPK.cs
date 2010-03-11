using System; 
using System.Collections.Generic; 
using System.Text; 


namespace CoralPOS.Models {
    
    
    [Serializable()]
    public class BlockInDetailPK {
        
        public virtual string BlockDetailId {
            get;
            set;
        }
        
        public virtual long BlockId {
            get;
            set;
        }
   	 protected bool Equals(BlockInDetailPK entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as BlockInDetailPK);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			
			return result;
		}
 }
}
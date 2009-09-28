using System;
using System.Collections;
using System.ComponentModel;
using ArrayList=System.Collections.ArrayList;

namespace AppFrame.Controls
{
    public class ComplexCustomTypeDescriptor : CustomTypeDescriptor
    {
        public ComplexCustomTypeDescriptor(ICustomTypeDescriptor parent) : base(parent)
        {

        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection parentPDs = base.GetProperties(attributes);
            return ComplexListPropertyHelper.GetList(parentPDs, GetClassName());
        }
        

        public override PropertyDescriptorCollection GetProperties()
        {
            
            PropertyDescriptorCollection parentPDs = base.GetProperties();
            return ComplexListPropertyHelper.GetList(parentPDs,GetClassName());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentPD"></param>
        /// <param name="list"></param>
        private void GetList(PropertyDescriptor parentPD,ref IList list)
        {
            PropertyDescriptorCollection childPDs = parentPD.GetChildProperties();
            // if childPDs count < 0 then return
            if(childPDs.Count<=0 )
            {
                return;
            }
            // if parentPD is immultable type then return
            Type parentType = parentPD.PropertyType;
            if(    parentType.Name.Equals("String") 
                || parentType.Name.Equals("Int32")
                || parentType.Name.Equals("Int64")
                || parentType.Name.Equals("Float")
                || parentType.Name.Equals("Double")
                || parentType.Name.Equals("Boolean")
                || parentType.Name.Equals("DateTime"))
            {
                return;
            }
            foreach (PropertyDescriptor childPD in childPDs)
            {
                ComplexPropertyDescriptor subPropertyDescriptor = new ComplexPropertyDescriptor(parentPD, childPD, parentPD.Name + "." + childPD.Name);
                if (PropertyDescriptorNotInList((PropertyDescriptor)subPropertyDescriptor, list))
                {
                    list.Add(subPropertyDescriptor);
                }
                PropertyDescriptorCollection subChildPDs = childPD.GetChildProperties();
                if(subChildPDs.Count> 0 )
                {
                    GetList(childPD,ref list);
                }
                else
                {
                    return;
                }
            } 
        }
        
        private bool PropertyDescriptorNotInList(PropertyDescriptor descriptor, IList list)
        {
            foreach (PropertyDescriptor propDesc in list)
            {
                if(descriptor.Name.Equals(propDesc.Name))
                {
                    return false;
                }
            }
            return true;
        }

        private IList CopyToList(IList list, PropertyDescriptorCollection cols)
        {
            foreach(PropertyDescriptor col in cols)
            {
                list.Add(col);
            }
            return list;
        }
    }
}

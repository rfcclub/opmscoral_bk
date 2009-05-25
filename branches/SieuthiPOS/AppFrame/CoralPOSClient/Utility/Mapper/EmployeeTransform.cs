﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Utility.Mapper;
using CoralPOS.View.SalePoints;

namespace CoralPOS.Utility.Mapper
{
    public class EmployeeTransform : FormTransform<EmployeeForm, EmployeeInfo>
    {
        
        public EmployeeTransform()
        {
            MapProperties.Add("txtHiddenDepartmentId.Text","DepartmentId");
            MapProperties.Add("txtEmployeeId.Text", "EmployeeId");
            MapProperties.Add("txtEmployeeName.Text", "EmployeeName");
            MapProperties.Add("txtAddress.Text", "Address");
            //mapProps.Add("txtDepartmentName.Text", "DepartmentName");
            //mapProps.Add("txtTimeOfService.Text", "DepartmentId");
            MapProperties.Add("txtSalary.Text", "Salary");

            //mapProps.Add("picEmployeePic.ImageLocation", "DepartmentId");    
        }
        public override void Transform(ref EmployeeForm form, EmployeeInfo sourceObject)
        {
            BaseTransform(ref form,sourceObject);
        }
    }
}

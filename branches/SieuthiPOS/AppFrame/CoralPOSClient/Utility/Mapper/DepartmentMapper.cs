﻿using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Utility.Mapper;
using CoralPOS.Interfaces.Model;
using CoralPOSClient.View.SalePoints;

namespace CoralPOS.Utility.Mapper
{
    public class DepartmentMapper : BaseMapper<Department, SalePointForm>
    {

        public Department Convert(SalePointForm source)
        {
            Department department = new Department();
            return null;
        }
    }
}
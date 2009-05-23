using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View;

namespace CoralPOS.Interfaces.Presenter
{
    public interface ISecurityController
    {
        ISecurityView SecurityView { get; set; }

        ILoginLogic LoginLogic { get; set; }
        IDepartmentLogic DepartmentLogic { get; set; } 
    }
}
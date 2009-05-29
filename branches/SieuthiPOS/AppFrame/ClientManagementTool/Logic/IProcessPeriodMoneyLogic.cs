using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientManagementTool.View.Management;
using CoralPOS.Interfaces.Logic;

namespace ClientManagementTool.Logic
{
    public interface IProcessPeriodMoneyLogic
    {
        // view
        IProcessPeriodMoneyView ProcessPeriodMoneyView { get; set; }

        // logic
        IEmployeeMoneyLogic EmployeeMoneyLogic { get; set; }
    }
}

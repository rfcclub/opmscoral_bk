using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientManagementTool.Logic;

namespace ClientManagementTool.View.Management
{
    public interface IProcessPeriodMoneyView
    {
        IProcessPeriodMoneyLogic ProcessPeriodMoneyLogic { get; set; }

        event EventHandler<ProcessPeriodMoneyEventArgs> LoadProcessPeriodMoneyEvent;
    }
}

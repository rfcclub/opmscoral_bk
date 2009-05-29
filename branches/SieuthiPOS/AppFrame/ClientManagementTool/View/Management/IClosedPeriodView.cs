using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientManagementTool.Logic;

namespace ClientManagementTool.View.Management
{
    public interface IClosedPeriodView
    {
        IClosedPeriodLogic ClosedPeriodLogic { get; set; }

        event EventHandler<ClosedPeriodEventArgs> LoadClosedPeriodEvent;
    }
}

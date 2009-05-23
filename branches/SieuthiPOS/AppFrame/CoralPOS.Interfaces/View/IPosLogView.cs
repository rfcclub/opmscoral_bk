using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View
{
    public interface IPosLogView : IView<PosLogEventArgs>
    {
        #region main controller and event
        IPosLogController PosLogController { set; }
        event EventHandler<PosLogEventArgs> GetPosLogEvent;
        event EventHandler<PosLogEventArgs> SearchPosLogEvent;
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.View;

namespace CoralPOS.Interfaces.Presenter
{
    public interface IPosLogController : IBaseController<PosLogEventArgs>
    {
        #region View use in IStockSearchController
        IPosLogView PosLogView { get; set; }
        #endregion

        #region Logic use in IStockSearchController
        #endregion
    }
}
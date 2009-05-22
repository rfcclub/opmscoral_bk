using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.View.GoodsIO;
using CoralPOS.Interfaces.Logic;

namespace AppFrame.Presenter.GoodsIO
{
    public interface IGoodsIOSearchController : IBaseController<GoodsIOSearchEventArgs>
    {
        #region View use in IGoodsIOSearchController
        IGoodsIOSearchView GoodsIOSearchView { get; set; }
        #endregion

        #region Logic use in IGoodsIOSearchController
        IBlockInDetailLogic BlockInDetailLogic { get; set; }
        #endregion
    }
}

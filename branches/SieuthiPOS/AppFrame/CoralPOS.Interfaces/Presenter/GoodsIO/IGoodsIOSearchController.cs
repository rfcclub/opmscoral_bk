using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.View.GoodsIO;

namespace CoralPOS.Interfaces.Presenter.GoodsIO
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
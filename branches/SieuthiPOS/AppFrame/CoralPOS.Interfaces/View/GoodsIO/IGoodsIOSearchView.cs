using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IGoodsIOSearchView : IView<GoodsIOSearchEventArgs>
    {
        #region main controller and event
        IGoodsIOSearchController GoodsIOSearchController { set; }
        event EventHandler<GoodsIOSearchEventArgs> SearchBlockInDetailEvent;
        event EventHandler<GoodsIOSearchEventArgs> LoadBlockInDetailEvent;
        event EventHandler<GoodsIOSearchEventArgs> DeleteBlockInDetailEvent;
        #endregion
    }
}
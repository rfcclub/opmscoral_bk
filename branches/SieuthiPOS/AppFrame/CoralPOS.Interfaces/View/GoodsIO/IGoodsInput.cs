using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsIO;

namespace CoralPOS.Interfaces.View.GoodsIO
{
    public interface IGoodsInput<T> where T : BaseEventArgs
    {
        IGoodsIOController<T> GoodsIOController { get; set; }
        event EventHandler<T> SaveGoodsInputEvent;
    }
}
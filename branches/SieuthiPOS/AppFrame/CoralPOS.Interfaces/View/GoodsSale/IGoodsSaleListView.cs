using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.GoodsSale;

namespace CoralPOS.Interfaces.View.GoodsSale
{
    public interface IGoodsSaleListView : IView<GoodsSaleListEventArgs>
    {
        IGoodsSaleListController GoodsSaleListController { get; set;}

        event EventHandler<GoodsSaleListEventArgs> GoodsSaleListSearchEvent;
        event EventHandler<GoodsSaleListEventArgs> ExportToExcelEvent;
        event EventHandler<GoodsSaleListEventArgs> PrintGoodsSaleEvent;
        event EventHandler<GoodsSaleListEventArgs> SelectGoodsSaleEvent;
        
    }
}
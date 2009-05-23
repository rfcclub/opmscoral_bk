using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.Masters;

namespace CoralPOS.Interfaces.View.Masters
{
    public interface IUniversalMasterSaveView : IView<UniversalMasterSaveEventArgs>
    {
        IUniversalMasterSaveController UniversalMasterSaveController { get; set; }
        event EventHandler<UniversalMasterSaveEventArgs> SaveUniversalMasterEvent;
    }
}
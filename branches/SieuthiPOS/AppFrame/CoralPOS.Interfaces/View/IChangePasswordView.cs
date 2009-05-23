using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View
{
    public interface IChangePasswordView<T> where T : BaseEventArgs
    {
        ILoginController<T> LoginController { get; set; }
        event EventHandler<T> ChangePasswordEvent;
    }
}
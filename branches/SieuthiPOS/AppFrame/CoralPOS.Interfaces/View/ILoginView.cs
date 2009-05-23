using System;
using System.Runtime.Serialization;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View
{
    public interface ILoginView<T> where T : BaseEventArgs 
    {
        ILoginController<T> LoginController { get;  set; } 
        event EventHandler<T> LoginEvent;

    }
}
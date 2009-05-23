using System;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Interfaces.View
{
    public interface ICustomerView<T> where T:BaseEventArgs 
    {
        ICustomerController<T> CustomerController { set; }
    }
}
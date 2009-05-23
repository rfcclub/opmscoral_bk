using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;

namespace CoralPOS.Interfaces.View
{
    public interface INewCustomerView<T> : ICustomerView<T> where T:BaseEventArgs
    {
        event EventHandler<T> NewCustomerEvent;
    }
}
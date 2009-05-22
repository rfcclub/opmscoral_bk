using AppFrame.Common;
using CoralPOS.Interfaces.Model;

namespace AppFrame.Presenter
{
    public class CustomerEventArgs : BaseEventArgs
    {
        private CustomerModel customer;

        public CustomerModel Customer
        {
            get { return customer; }
            set { customer = value; }
        }
    }
}
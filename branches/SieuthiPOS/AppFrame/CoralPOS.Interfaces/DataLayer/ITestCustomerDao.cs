using System;
using System.Collections;
using System.Text;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.DataLayer
{
    public interface ITestCustomerDao
    {
        bool saveCustomer(CustomerModel customer);
        bool deleteCustomer(CustomerModel customer);
        bool updateCustomer(CustomerModel customer);
        System.Collections.IList searchCustomerByName(string name);
        CustomerModel loadCustomer(int id);
        IList loadAllCustomers();

    }
}
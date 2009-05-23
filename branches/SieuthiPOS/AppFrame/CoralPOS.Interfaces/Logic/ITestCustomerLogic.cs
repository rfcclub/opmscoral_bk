using System.Collections;
using CoralPOS.Interfaces.DataLayer;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Logic
{
    public interface ITestCustomerLogic : IAuthoriseLogic
    {
        ITestCustomerDao CustomerDao { get; set;}
        CustomerModel GetCustomer(string customerName);
        bool SaveCustomer(CustomerModel customer);
        bool UpdateCustomer(CustomerModel customer);
        bool DeleteCustomer(CustomerModel customer);
        IList LoadAllCustomers();
        CustomerModel LoadCustomer(int id);
        IList Find(string name);

        
    }
}
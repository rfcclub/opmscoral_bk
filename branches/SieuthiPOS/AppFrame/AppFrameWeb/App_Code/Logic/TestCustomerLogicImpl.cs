using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppFrame.Common;
using CoralPOS.DataLayer;
using CoralPOS.Interfaces.DataLayer;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Logic;

/// <summary>
/// Summary description for CustomerLogic
/// </summary>
/// 
namespace  CoralPOS.Logic
{  
public class TestCustomerLogic : ITestCustomerLogic
{
    private ITestCustomerDao customerDao;
    private BaseUser currentUser;
    public ITestCustomerDao CustomerDao
    {
        get { return customerDao; }
        set { customerDao = value; }
    }
    

    #region ICustomerLogic Members


    public CustomerModel GetCustomer(string customerName)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public bool SaveCustomer(CustomerModel customer)
    {
        return CustomerDao.saveCustomer(customer);
    }

    public IList LoadAllCustomers()
    {
        return CustomerDao.loadAllCustomers();
    }

    public CustomerModel LoadCustomer(int id)
    {
        return CustomerDao.loadCustomer(id);
    }

    public System.Collections.IList Find(string name)
    {
        return CustomerDao.searchCustomerByName(name);
    }

    #endregion

    #region ICustomerLogic Members


    public bool UpdateCustomer(CustomerModel customer)
    {
        return CustomerDao.updateCustomer(customer);
    }

    #endregion

    #region ICustomerLogic Members


    public bool DeleteCustomer(CustomerModel customer)
    {
        return CustomerDao.deleteCustomer(customer);
    }

    #endregion


    #region IAuthoriseLogic Members

    public BaseUser CurrentUser
    {
        get
        {
            return currentUser;
        }
        set
        {
            currentUser = value;
        }
    }

    #endregion
}
}

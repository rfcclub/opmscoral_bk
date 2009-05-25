using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CoralPOS.Interfaces.DataLayer;
using CoralPOS.Interfaces.Model;
using Spring.Data.NHibernate;
using Spring.Data.NHibernate.Support;

namespace CoralPOS.DataLayer
{
    public class RoleDaoImpl : HibernateDaoSupport, IRoleDao
    {
        IList IRoleDao.getRoles(string Username)
        {
            LoginModel loginModel = HibernateTemplate.Get(typeof(LoginModel), Username) as LoginModel;
            System.Collections.IList listRoleModel = null;
            if (loginModel != null)
            {
                listRoleModel = loginModel.Roles;
            }
            return listRoleModel;
        }

    }
}

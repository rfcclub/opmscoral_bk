using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Presenter;

namespace CoralPOS.Presenter
{
    public class SecurityController : ISecurityController
    {
        #region ISecurityController Members

        private AppFrame.View.ISecurityView securityView;
        public AppFrame.View.ISecurityView SecurityView
        {
            get
            {
                return securityView;
            }
            set
            {
                securityView = value;
                securityView.InitSecuritySettingsEvent += new EventHandler<SecurityEventArgs>(securityView_InitSecuritySettingsEvent);
                securityView.SaveUserEvent += new EventHandler<SecurityEventArgs>(securityView_SaveUserEvent);
            }
        }

        void securityView_SaveUserEvent(object sender, SecurityEventArgs e)
        {
            LoginLogic.ProcessUser(e.SaveModel);
        }

        void securityView_InitSecuritySettingsEvent(object sender, SecurityEventArgs e)
        {
            IList list = DepartmentLogic.FindAll(null);
            e.departmentList = list;
            IList empList = EmployeeInfoLogic.FindAll(null);
            e.employees = empList;
            IList loginList = LoginLogic.FindAll(null);
            e.userInfoList = loginList;

        }

        public Logic.ILoginLogic LoginLogic
        {
            get;set;
        }

        public Logic.IEmployeeDetailLogic EmployeeInfoLogic
        {
            get; set;
        }

        #endregion

        #region ISecurityController Members


        public Logic.IDepartmentLogic DepartmentLogic
        {
            get;set;
        }

        #endregion
    }
}

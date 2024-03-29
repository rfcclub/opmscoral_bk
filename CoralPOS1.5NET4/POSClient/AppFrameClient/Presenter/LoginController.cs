﻿using System;
using System.Collections;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Logic;
using AppFrame.Model;
using AppFrame.Utility;
using AppFrame.View;
using AppFrameClient.Utility;
using Common.Logging;

namespace AppFrame.Presenter
{
    public class LoginController : AsyncController, ILoginController<LoginEventArgs>
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private LoginEventArgs loginEventArgs;
        private ILoginLogic loginLogic;
        private ILoginView<LoginEventArgs> mView;

        #region IAuthController Members

        public ILoginView<LoginEventArgs> LoginView
        {
            get { return mView; }
            set
            {
                mView = value;
                mView.LoginEvent +=new EventHandler<LoginEventArgs>(mView_LoginEvent);
                mView.ConfirmLoginEvent += new EventHandler<LoginEventArgs>(mView_ConfirmLoginEvent);
                mView.ConfirmEmployeeIdEvent += new EventHandler<LoginEventArgs>(mView_ConfirmEmployeeIdEvent);
            }
        }

        void mView_ConfirmEmployeeIdEvent(object sender, LoginEventArgs e)
        {
            try
            {
                SubObjectCriteria subObjectCriteria = new SubObjectCriteria("EmployeeInfo");
                // new EmployeeInfo().EmployeePK.EmployeeId
                subObjectCriteria.AddEqCriteria("Barcode", e.Barcode);
                ObjectCriteria objectCriteria = new ObjectCriteria();
                objectCriteria.AddSubCriteria("EmployeeInfo", subObjectCriteria);
                IList list = LoginLogic.FindAll(objectCriteria);

                if (list != null && list.Count > 0)
                {
                    e.LoginModel = list[0] as LoginModel;
                }
                else
                {
                    e.HasErrors = true;
                }
            }
            catch (Exception)
            {
                e.HasErrors = true;
            }
        }

        void mView_ConfirmLoginEvent(object sender, LoginEventArgs e)
        {
            bool loginResult = loginLogic.validate(e.LoginModel);
            if(loginResult)
            {
                LoginModel model = loginLogic.FindById(e.LoginModel.Username);
                bool matchConfirmType = false;
                foreach (RoleModel role in model.Roles)
                {
                    if(e.ConfirmType.IndexOf(role.Name)>= 0)
                    {
                        matchConfirmType = true;
                        break;
                    }
                }
                if(matchConfirmType)
                    ClientUtility.Log(logger, "Người dùng " + e.LoginModel.Username + " đã " + e.ConfirmAction, "Xác nhận hành động");
                else
                    e.HasErrors = true;
            }
            else
            {
                e.HasErrors = true;
            }
        }

        private IChangePasswordView<LoginEventArgs> changePasswordView;
        public IChangePasswordView<LoginEventArgs> ChangePasswordView
        {
            get { return changePasswordView; }
            set { changePasswordView = value;
            changePasswordView.ChangePasswordEvent += new EventHandler<LoginEventArgs>(changePasswordView_ChangePasswordEvent);
            }
        }

        void changePasswordView_ChangePasswordEvent(object sender, LoginEventArgs e)
        {
            ClientInfo clientInfo = ClientInfo.getInstance();
            try
            {
                string Username = ClientInfo.getInstance().LoggedUser.Name;
                string Password = e.OldPassword;
                string NewPassword = e.NewPassword;
                LoginLogic.ChangePassword(Username, Password, NewPassword);
                ClientUtility.Log(logger, "Người dùng " + clientInfo.LoggedUser.Name + " thay đổi mật khẩu thành công", "Đổi mật khẩu");
            }
            catch (Exception ex)
            {
                e.HasErrors = true;
                ClientUtility.Log(logger, "Người dùng " + clientInfo.LoggedUser.Name + " thay đổi mật khẩu thất bại: " + ex.Message, "Đổi mật khẩu");
                throw;
            }
            
        }

        private void mView_LoginEvent(object sender, LoginEventArgs e)
        {
            //BaseUser baseUser = loginLogic.doAuthentication(e.LoginModel);
            AuthManager authManager = SecurityUtility.LoadAuthenticationModule();
            BaseUser baseUser = authManager.login(e.LoginModel.Username, e.LoginModel.Password);
            ClientInfo clientInfo = ClientInfo.getInstance();
            clientInfo.LoggedUser = baseUser;
            //MessageBox.Show(clientInfo.LoggedUser.IsGuest.ToString() + baseUser.Name);
            if (!baseUser.IsGuest)
            {
                e.HasErrors = false;
                ResultEventArgs.MessageCode = "loginOK";
                ResultEventArgs.IsValid = true;
            }
            else
            {
                e.HasErrors = true;
                ResultEventArgs.ErrorCode = "loginFail";
                ResultEventArgs.IsValid = false;
            }
            EventUtility.fireEvent(CompleteLoginLogicEvent, this, ResultEventArgs);
            ClientUtility.Log(logger, "Người dùng " + clientInfo.LoggedUser.Name + " đăng nhập vào hệ thống", "Đăng nhập");
        }

        public event EventHandler<LoginEventArgs> CompleteLoginLogicEvent;

        public ILoginLogic LoginLogic
        {
            get { return loginLogic; }
            set { loginLogic = value; }
        }


        #endregion
        
        
        public virtual void OnException(Exception ex)
        {
            // override by childern
        }

        #region IBaseController<T> Members

        public LoginEventArgs ResultEventArgs
        {
            get
            {
                return loginEventArgs;
            }
            set
            {
                loginEventArgs = value;
            }
        }

        #endregion

        #region IAuthController<LoginEventArgs> Members


        public string doLogin(LoginModel loginModel)
        {
            bool loginResult = loginLogic.validate(loginModel);


            if (loginResult)
            {
                return "Validate successfully !";
            }
            else
            {
                return "Wrong username or password !";
            }
        }

        #endregion
    }
}
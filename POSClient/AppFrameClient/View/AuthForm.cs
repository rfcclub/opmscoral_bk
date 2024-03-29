using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Model;
using AppFrame.Presenter;
using AppFrame.Utility;
using AppFrame.View;
using AppFrameClient.Common;

namespace AppFrameClient.View
{
    public partial class AuthForm : BaseForm, ILoginView<LoginEventArgs>
    {
        private ILoginController<LoginEventArgs> loginController;
        public event EventHandler<LoginEventArgs> LoginEvent;

        public AuthForm()
        {
            InitializeComponent();
            lblStatus.Text = "";
        }
        #region ILoginView Members

        public ILoginController<LoginEventArgs> LoginController
        {
            set
            {
                loginController = value;
                loginController.LoginView = this;
                loginController.CompleteLoginLogicEvent += loginController_CompleteLoginEvent;
            }
            get
            {
                return loginController;
            }
        }

        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu bị trống");
                return;
            }
            /*loginValidationProvider.Enabled = true;
            if (!ValidateChildren())
            {
                return;
            }

            loginValidationProvider.Enabled = false;*/
            btnLogin.Enabled = false;
            //backgroundWorker.DoWork += new DoWorkEventHandler(DoWorkUsingBackgroundHandler);
            //backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoWorkUsingBackgroundCompleted);
            //backgroundWorker.RunWorkerAsync();
            DoLogin();
        }
        private void DoLogin()
        {
            btnLogin.Enabled = false;
            //MainForm form = GlobalUtility.GetFormObject<MainForm>(FormConstants.MAIN_FORM);
            Form form =GlobalCache.Instance().MainForm;
            if (form is MainForm)
            {
                ((MainForm)form).showProgressBar();
            }
            // create model and raise event
            LoginModel model = new LoginModel();
            model.Username = txtUsername.Text;
            model.Password = txtPassword.Text;
            LoginEventArgs loginEventArgs = new LoginEventArgs();
            loginEventArgs.LoginModel = model;
            if (LoginEvent != null)
            {
                //LoginEvent.BeginInvoke(this, model);
                //EventUtility.fireAsyncEvent(LoginEvent, this, loginEventArgs, new AsyncCallback(EndEvent));
                EventUtility.fireEvent(LoginEvent,this,loginEventArgs);

            }
        }

        private void loginController_CompleteLoginEvent(object sender, LoginEventArgs e)
        {
            // It's on a different thread, so use Invoke.
            //SetEnableButton d = ShowResult;
            //this.Invoke(d, sender, e);
            btnLogin.Enabled = true;
            /*MainForm form = GlobalUtility.GetFormObject<MainForm>(FormConstants.MAIN_FORM);
            form.stopProgressBar();*/
            //MainForm form = (MainForm)GlobalCache.Instance().MainForm;
            Form form =GlobalCache.Instance().MainForm;
            if (form is MainForm)
            {
                ((MainForm)form).stopProgressBar();
            }
            // end invoke in thread safe way
//            if (!CheckUtility.IsNullOrEmpty(e.ErrorCode))
//            {
//                lblStatus.Text = ResourceUtility.GetErrorString(e.ErrorCode);
//            }
//            else
//            {
//                lblStatus.Text = ResourceUtility.GetMessageString(e.MessageCode);
//            }
            if(e.IsValid)
            {

                this.Visible = false;
                var resManager = new ResourceManager("AppFrameClient.Global", Assembly.GetExecutingAssembly());
                if (form is MainForm)
                {
                    if (ClientInfo.getInstance().LoggedUser.IsGuest)
                    {
                        ((MainForm)form).lblStatus.Text = resManager.GetString("guestUserString");
                    }
                    else
                    {
                        ((MainForm)form).lblStatus.Text = resManager.GetString("loggedUserString");
                    }
                }
                Close();
                CleanDatabase();

            }
            
            form.Focus();
            
            MenuUtility.setPermission(GlobalCache.Instance().MainForm, ClientInfo.getInstance());
        }

        private void CleanDatabase()
        {
            
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginController.CompleteLoginLogicEvent -= loginController_CompleteLoginEvent;

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            this.lblStatus.Text = "";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           txtUsername_TextChanged(sender,e);
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();                
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, null); 
            }
        }

        
    }
}
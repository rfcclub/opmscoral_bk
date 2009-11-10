﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Model;
using AppFrame.Presenter;
using AppFrame.Utility;
using AppFrame.View;
using AppFrameClient.Common;
using Spring.Context;
using Spring.Context.Support;

namespace AppFrameClient.View
{
    public partial class LoginForm : BaseForm, ILoginView<LoginEventArgs>
    {

        private ILoginController<LoginEventArgs> loginController;
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        public bool IsConfirmed { get; set; }
        public LoginForm()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu bị trống");
                return;
            }
            /*validationProvider.Enabled = true;
            if (!ValidateChildren())
            {
                return;
            }

            validationProvider.Enabled = false;*/
            button1.Enabled = false;
            
            //backgroundWorker.DoWork += new DoWorkEventHandler(DoWorkUsingBackgroundHandler);
            //backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoWorkUsingBackgroundCompleted);
            //backgroundWorker.RunWorkerAsync();
            DoWorkUsingEvent();

        }

        private void DoWorkUsingBackgroundCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // do nothing
            button1.Enabled = true;
            
            lblStatus.Text = result;
        }

        private void DoWorkUsingEvent()
        {
            button1.Enabled = false;
            // create model and raise event
            LoginModel model = new LoginModel();
            model.Username = txtUsername.Text.Trim();
            model.Password = txtPassword.Text.Trim();
            LoginEventArgs loginEventArgs = new LoginEventArgs();
            loginEventArgs.ConfirmType = "Manager,Supervisor,Administrator";
            loginEventArgs.LoginModel = model;
            if (LoginEvent != null)
            {
                //LoginEvent.BeginInvoke(this, model);
                //EventUtility.fireAsyncEvent<LoginEventArgs>(LoginEvent, this, loginEventArgs, new AsyncCallback(EndEvent));
                EventUtility.fireEvent(ConfirmLoginEvent,this,loginEventArgs);
                if(!loginEventArgs.HasErrors)
                {
                    IsConfirmed = true;
                    ReturnResult();
                }
            }
        }

        private void ReturnResult()
        {
            if (IsConfirmed)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private string result;
        void DoWorkUsingBackgroundHandler(object sender, DoWorkEventArgs e)
        {
            // create model and raise event
            LoginModel model = new LoginModel();
            model.Username = txtUsername.Text;
            model.Password = txtPassword.Text;
            result = LoginController.doLogin(model);

            //lblStatus.Text = result;
        }
        
        
        private void loginController_CompleteLoginEvent(object sender, LoginEventArgs e)
        {
            // It's on a different thread, so use Invoke.
            //SetEnableButton d = ShowResult;
            //this.Invoke(d, sender, e);
            button1.Enabled = true;
            // end invoke in thread safe way
            if (e != null)
            {
                lblStatus.Text = e.EventResult as string;
            }
            this.Visible = false;

            IApplicationContext ctx = ContextRegistry.GetContext();
            MainForm mainForm = ctx.GetObject("MainView") as MainForm;
            mainForm.lblStatus.Text = " You are logged in !";
            MenuUtility.setPermission(mainForm,ClientInfo.getInstance());
        }                  

        

        #region ILoginView Members


        public event EventHandler<LoginEventArgs> LoginEvent;
        public event EventHandler<LoginEventArgs> ConfirmLoginEvent;
        public event EventHandler<LoginEventArgs> ConfirmEmployeeIdEvent;

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,e);
            }
        }
        

    }
}
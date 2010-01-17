using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Logic;
using AppFrame.Model;
using AppFrame.Utility;
using AppFrame.View;
using AppFrameClient;
using AppFrameClient.Common;
using AppFrameClient.Logic;
using AppFrameClient.MasterDBTableAdapters;
using AppFrameClient.Model;
using AppFrameClient.View;
using AppFrameClient.View.Management;

namespace AppFrame.Presenter
{
    public class AuthService : IAuthService,IEmployeeManagementView
    {

        #region IAuthService Members

        private IMainLogic _mainLogic;

        public void login()
        {
            AuthForm loginForm = GlobalUtility.GetFormObject<AuthForm>(FormConstants.LOGIN_FORM);
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.LoginOKEvent += new EventHandler<LoginEventArgs>(loginForm_LoginOKEvent);
            loginForm.ShowDialog();
        }

        void loginForm_LoginOKEvent(object sender, LoginEventArgs e)
        {
            if (!ClientInfo.getInstance().LoggedUser.IsGuest)
            {
                if (ClientSetting.IsClient())
                {
                    CreateThreadEnterPeriod();
                }
                if (PostLogin != null)
                {
                    EventUtility.fireEvent(PostLogin, this, new BaseEventArgs());
                }
            }
        }

        private void CreateThreadEnterPeriod()
        {
            Thread thread = new Thread(new ThreadStart(EnterPeriod));
            thread.Start();
        }

        public void logout()
        {
            
            AuthManager authManager = SecurityUtility.LoadAuthenticationModule();
            authManager.logout();
            GlobalUtility.CloseAllChildForm(GlobalUtility.GetFormObject(FormConstants.MAIN_FORM));
            MenuUtility.setPermission(GlobalCache.Instance().MainForm, ClientInfo.getInstance(), ref ((MainForm)GlobalCache.Instance().MainForm).toolStripClient,
                                          GlobalCache.Instance().ClientToolStripPermission);

            if (ClientInfo.getInstance().LoggedUser.IsGuest && PreLogout != null)
            {
                EventUtility.fireEvent(PreLogout, this, new BaseEventArgs());
            }
        }

        #endregion

        #region IAuthService Members


        public event EventHandler<BaseEventArgs> PostLogin;

        public event EventHandler<BaseEventArgs> PreLogout;

        public void EnterPeriod()
        {
            try
            {
                ObjectCriteria crit = new ObjectCriteria();
                crit.AddEqCriteria("EmployeeMoneyPK.WorkingDay", DateUtility.DateOnly(DateTime.Now));
                crit.AddEqCriteria("EmployeeMoneyPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
                IList list = EmployeeMoneyLogic.FindAll(crit);

                if (list != null && list.Count == 1)
                {

                }
                else
                {
                    DateTime toDay = DateUtility.DateOnly(DateTime.Now);
                    DateTime startToday = toDay.AddHours(3);

                    // get total of yesterday
                    MasterDB masterDB = new MasterDB();
                    DepartmentSumCostReportTableAdapter adapter = new DepartmentSumCostReportTableAdapter();
                    adapter.ClearBeforeFill = true;
                    adapter.Fill(masterDB.DepartmentSumCostReport,
                                 (int)CurrentDepartment.Get().DepartmentId,
                                 startToday.Subtract(new TimeSpan(1, 0, 0, 0)),
                                 startToday
                        );

                    int rowCount = masterDB.DepartmentSumCostReport.Rows.Count;
                    long prevAmount = 0;
                    if (rowCount > 0)
                    {
                        prevAmount = (long)masterDB.DepartmentSumCostReport.Rows[0][masterDB.DepartmentSumCostReport.totalColumn];
                    }

                    EmployeeMoney fixEmplMoney = new EmployeeMoney();
                    fixEmplMoney.WorkingDay = toDay;
                    fixEmplMoney.CreateDate = startToday;
                    fixEmplMoney.UpdateDate = startToday;
                    fixEmplMoney.CreateId = ClientInfo.getInstance().LoggedUser.Name;
                    fixEmplMoney.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                    fixEmplMoney.DateLogin = startToday;
                    fixEmplMoney.DateLogout = startToday;
                    fixEmplMoney.InMoney = prevAmount;
                    //fixEmplMoney.OutMoney = 0;
                    EmployeeMoneyPK fixTimelinePK = new EmployeeMoneyPK
                    {
                        DepartmentId = CurrentDepartment.Get().DepartmentId,
                        EmployeeId = ClientInfo.getInstance().LoggedUser.Name,
                        WorkingDay = toDay
                    };

                    fixEmplMoney.EmployeeMoneyPK = fixTimelinePK;
                    EmployeeMoneyLogic.Add(fixEmplMoney);

                    // update last day
                    ObjectCriteria lastDayCrit = new ObjectCriteria();
                    lastDayCrit.AddEqCriteria("EmployeeMoneyPK.WorkingDay", DateUtility.DateOnly(toDay.Subtract(new TimeSpan(1, 0, 0, 0))));
                    lastDayCrit.AddEqCriteria("EmployeeMoneyPK.DepartmentId", CurrentDepartment.Get().DepartmentId);
                    IList lastList = EmployeeMoneyLogic.FindAll(lastDayCrit);
                    if (lastList != null && lastList.Count > 0)
                    {
                        EmployeeMoney lastDayMoney = (EmployeeMoney)lastList[0];
                        lastDayMoney.OutMoney = prevAmount;
                        lastDayMoney.UpdateDate = startToday;
                        lastDayMoney.UpdateId = ClientInfo.getInstance().LoggedUser.Name;
                        EmployeeMoneyLogic.Update(lastDayMoney);
                    }

                }
            }
            catch (Exception)
            {

            }


            #region unused code
            BaseUser user = ClientInfo.getInstance().LoggedUser;
            // if has entered period then return
            if (user.IsInRole(PosRole.LoggedManager))
            {
                return;
            }

            // if not then try to enter period
            if (user.IsInRole(PosRole.Manager))
            {
                //EmployeeInfo employeeInfo = LoginLogic.GetEmployeeInfo(user.Name);
                DialogResult result = MessageBox.Show("Bạn muốn vào ca trực ?", "Ca trực", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
                BaseUser loggedUser = ClientInfo.getInstance().LoggedUser;
                if (loggedUser.IsInRole(PosRole.Manager))
                {
                    // process period for manager here 
                    EmployeeManagementEventArgs eventArgs = new EmployeeManagementEventArgs();
                    eventArgs.Username = ClientInfo.getInstance().LoggedUser.Name;
                    EventUtility.fireEvent(ProcessPeriodEvent, this, eventArgs);
                    LoginModel userInfo = eventArgs.UserInfo;
                    if (eventArgs.DepartmentManagement == null)
                    {
                        EventUtility.fireEvent(StartPeriodEvent, this, eventArgs);
                        if (eventArgs.HasErrors)
                        {
                            MessageBox.Show("Có lỗi khi thực hiện vào ca. Liên hệ người quản trị.", "Lỗi");
                            return;
                        }
                        else
                        {
                            loggedUser.Roles.Add(PosRole.LoggedManager);
                            EmployeeMoneyForm form =
                            GlobalUtility.GetFormObject<EmployeeMoneyForm>(FormConstants.EMPLOYEE_MONEY_VIEW);
                            form.lblStatus.Text = "VÀO CA";
                            form.ShowDialog();
                            EmployeeMoneyForm.ChoosingResult choosingResult = form.ChoosedResult;

                            if (choosingResult == EmployeeMoneyForm.ChoosingResult.Next)
                            {
                                long inMoney = form.MoneyEntered;
                                eventArgs.InMoney = inMoney;
                                EventUtility.fireEvent(ProcessEmployeeMoneyEvent, this, eventArgs);
                                if (eventArgs.HasErrors)
                                {
                                    MessageBox.Show("Có lỗi khi thực hiện vào ca. Liên hệ người quản trị.", "Lỗi");
                                    return;
                                }
                            }

                            MessageBox.Show("Bạn đã vào ca trực. Nhấn OK để thoát.");
                        }
                    }
                    else
                    {
                        if (!userInfo.EmployeeInfo.EmployeePK.EmployeeId.Equals(eventArgs.DepartmentManagement.DepartmentManagementPK.EmployeeId))
                        {
                            MessageBox.Show("Ca trực đã có người quản lý.", "Lỗi");
                        }
                        else
                        {
                            loggedUser.Roles.Add(PosRole.LoggedManager);
                            MessageBox.Show("Bạn đã trở lại ca trực.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không đủ quyền hạn", "Lỗi");
                }
            } 
            #endregion
        }

        public void LeavePeriod()
        {
            return;
            BaseUser loggedUser = ClientInfo.getInstance().LoggedUser;
            if (loggedUser.IsInRole(PosRole.Manager))
            {
                DialogResult result = MessageBox.Show("Bạn có muốn kết thúc ca trực hay không ? ",
                                                      "Thoát ca trực", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    EmployeeManagementEventArgs eventArgs = new EmployeeManagementEventArgs();
                    eventArgs.Username = ClientInfo.getInstance().LoggedUser.Name;
                    EventUtility.fireEvent(ProcessPeriodEvent, this, eventArgs);
                    LoginModel userInfo = eventArgs.UserInfo;
                    if (eventArgs.DepartmentManagement == null)
                    {
                        MessageBox.Show("Chưa vào ca nên không thể chấm dứt ca.", "Lỗi");
                        return;

                    }
                    if (!userInfo.EmployeeInfo.EmployeePK.EmployeeId.Equals(eventArgs.DepartmentManagement.DepartmentManagementPK.EmployeeId))
                    {
                        MessageBox.Show("Ca trực đã có người quản lý.", "Lỗi");
                        return;
                    }
                    EventUtility.fireEvent(EndPeriodEvent, this, eventArgs);
                    EmployeeMoneyForm form =
                            GlobalUtility.GetFormObject<EmployeeMoneyForm>(FormConstants.EMPLOYEE_MONEY_VIEW);
                    form.lblStatus.Text = "CHỐT CA";
                    form.ShowDialog();
                    EmployeeMoneyForm.ChoosingResult choosingResult = form.ChoosedResult;
                    if (choosingResult == EmployeeMoneyForm.ChoosingResult.Next)
                    {
                        long outMoney = form.MoneyEntered;
                        eventArgs.OutMoney = outMoney;
                    }
                    EventUtility.fireEvent(EndPeriodEvent, this, eventArgs);
                    
                    if (eventArgs.HasErrors)
                    {
                        MessageBox.Show("Có lỗi khi thoát ca. Liên hệ người quản trị");
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã kết thúc ca trực. Nhấn OK để thoát.");
                        logout();
                    }
                }
            }
            else
            {
                MessageBox.Show("Không đủ quyền hạn", "Lỗi");
            }
        }
        
        #endregion

        public IMainLogic MainLogic
        {
            get { return _mainLogic; }
            set
            {
                _mainLogic = value;
                _mainLogic.MainView = this;
            }
        }
        public IEmployeeMoneyLogic EmployeeMoneyLogic { get; set; }
        public event EventHandler<EmployeeManagementEventArgs> ProcessPeriodEvent;
        public event EventHandler<EmployeeManagementEventArgs> StartPeriodEvent;
        public event EventHandler<EmployeeManagementEventArgs> EndPeriodEvent;
        public event EventHandler<EmployeeManagementEventArgs> ProcessEmployeeMoneyEvent;
    }
}

using System.Reflection;
using System.Resources;
using AppFrame.Common;
using AppFrame.Utility;
using AppFrame.Common;
using AppFrame.Utility;
using CoralPOS.Interfaces.View;
using CoralPOS.Common;
using CoralPOSClient.View;
using Spring.Aop;

namespace CoralPOS.Advice
{
    internal class MenuPermissionAdvice : IAfterReturningAdvice
    {
        #region IAfterReturningAdvice Members

        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            var mainForm = GlobalUtility.GetFormObject<MainForm>(FormConstants.MAIN_FORM);
            ClientInfo clientInfo = ClientInfo.getInstance();
            var resManager = new ResourceManager("CoralPOSClient.Global", Assembly.GetExecutingAssembly());
            if (clientInfo.LoggedUser.IsGuest)
            {
                mainForm.lblStatus.Text = resManager.GetString("guestUserString");
            }
            else
            {
                mainForm.lblStatus.Text = resManager.GetString("loggedUserString");
            }
            MenuUtility.setPermission(mainForm, clientInfo);
            //((MainForm) mainForm).setMenuPermissions();

        }

        #endregion
    }
}
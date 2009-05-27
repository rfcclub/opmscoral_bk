using System.Reflection;
using System.Resources;
using AppFrame.Common;
using AppFrame.Utility;
using AppFrame.Common;
using AppFrame.Utility;
using CoralPOS.Interfaces.View;
using CoralPOS.Common;
using Spring.Aop;

namespace CoralPOS.Advice
{
    internal class MenuPermissionAdvice : IAfterReturningAdvice
    {
        #region IAfterReturningAdvice Members

        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            var mainForm = GlobalUtility.GetFormObject(FormConstants.MAIN_FORM);
            ClientInfo clientInfo = ClientInfo.getInstance();
            MenuUtility.setPermission(mainForm, clientInfo);
            //((MainForm) mainForm).setMenuPermissions();

        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Common;
using CoralPOS.Interfaces.Model;

namespace ClientManagementTool.Logic
{
    public class MainLogicEventArgs : BaseEventArgs
    {
        public string Username { get; set; }
        public LoginModel UserInfo { get; set; }
        public DepartmentManagement DepartmentManagement { get; set; }
        public long InMoney { get; set; }
        public long OutMoney { get; set; }
    }
}

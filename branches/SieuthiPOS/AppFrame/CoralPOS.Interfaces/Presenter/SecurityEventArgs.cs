using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter
{
    public class SecurityEventArgs : BaseEventArgs
    {
        public LoginModel SaveModel;
        public IList departmentList { get; set; }
        public IList employees { get; set; }
        public IList userInfoList { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS2.Models;

namespace POSClient.BusinessLogic.Logic.Security
{
    public class LoginLogic
    {
        public bool Login(LoginModel loginInfo)
        {
            string username = loginInfo.Username.ToLower();
            string password = loginInfo.Password.ToLower();
            if(username.Equals("admin") && password.Equals("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
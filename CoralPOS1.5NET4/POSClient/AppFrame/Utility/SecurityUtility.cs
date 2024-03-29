using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using Spring.Context;

namespace AppFrame.Utility
{
    public sealed class SecurityUtility
    {
        public static readonly string AUTHENTICATION_MODULE = "AuthenticationModule";
        public static readonly string AUTH_USER_MANAGER = "AuthUserManager";
        public static readonly string USER = "User";

        public static BaseUser CreateGuestUser()
        {
            BaseUser guestUser = new BaseUser();
            guestUser.IsGuest = true;
            guestUser.Name = "Guest";
            guestUser.FullName = "Khách viếng thăm";
            return guestUser;
        }
        public static AuthManager LoadAuthenticationModule()
        {
            AuthManager authenticationManager = null;
            try
            {

                authenticationManager = GlobalUtility.GetObject(AUTHENTICATION_MODULE) as AuthManager;
            }
            catch(Exception e)
            {
            }
            return authenticationManager;
        }
    }
}

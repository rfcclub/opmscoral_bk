using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.Utility.Mapper;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Utility.Mapper
{
    /// <summary>
    /// Class Convert from LoginModel to BaseUser
    /// </summary>
    public class BaseUserMapper : BaseMapper<BaseUser,LoginModel>
    {

        public BaseUser Convert(LoginModel source)
        {
            if(source == null )
                return null;

            AppFrame.Common.BaseUser user = new AppFrame.Common.BaseUser();
            user.Name = source.Username;
            return user;
        }
    }
}
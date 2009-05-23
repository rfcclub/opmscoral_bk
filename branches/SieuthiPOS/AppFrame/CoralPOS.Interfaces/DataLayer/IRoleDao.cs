using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;

namespace CoralPOS.Interfaces.DataLayer
{
    public interface IRoleDao
    {
        IList getRoles(string Username);
    }
}
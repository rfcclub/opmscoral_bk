using System;
using System.Collections.Generic;
using System.Text;
using AopAlliance.Aop;
using AppFrame.Common;

namespace CoralPOS.Interfaces.Logic
{
    public interface IAuthoriseLogic : IAdvice
    {
        BaseUser CurrentUser { get; set; }
    }
}
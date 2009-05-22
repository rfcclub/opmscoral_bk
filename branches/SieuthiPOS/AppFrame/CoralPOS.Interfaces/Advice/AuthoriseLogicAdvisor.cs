using System;
using System.Collections.Generic;
using System.Text;
using CoralPOS.Interfaces.Logic;
using Spring.Aop.Support;

namespace CoralPOS.Interfaces.Advice
{
    public class AuthoriseLogicAdvisor : DefaultIntroductionAdvisor
    {
        public AuthoriseLogicAdvisor()
            : base(new AuthoriseLogicMixin(),typeof(IAuthoriseLogic))
        {
        }
    }
}
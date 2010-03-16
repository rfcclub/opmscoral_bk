﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using POSServer.BusinessLogic.Implement;

namespace POSServer.Actions.ProductMaster
{
    public class ExProductColorsViewSaveAction : PosAction,IExProductColorsViewSaveAction
    {
        public IExProductColorLogic ProductColorLogic
        {
            get; set;
        }

        public override void DoExecute()
        {
            ProductColorLogic.Process(Flow.Session);
            GoToNextNode();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter
{
    public class TaxChangeEventArgs : BaseEventArgs
    {
        public ProductMaster ProductMasterCondition { get; set; }
        public IList ProductMasterList { get; set; }
        public IList TaxList { get; set; }
    }
}

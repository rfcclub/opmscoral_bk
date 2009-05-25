using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoralPOS.ViewModel
{
    public class StockInResultDetail
    {
        public ProductMasterGlobal ProductMasterGlobal { get; set; }
        public long StockInQuantities { get; set; }
        public long StockInTotalAmounts { get; set; }
    }
}

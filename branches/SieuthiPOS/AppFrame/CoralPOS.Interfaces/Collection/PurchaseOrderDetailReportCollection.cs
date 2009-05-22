using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class PurchaseOrderDetailReportCollection : ReportCollection<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailReportCollection()
        {
        }

        public PurchaseOrderDetailReportCollection(IList<PurchaseOrderDetail> list) : base(list)
        {
        }
    }
}
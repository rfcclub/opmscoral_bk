using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class ProductReportCollection : ReportCollection<Product>
    {
        public ProductReportCollection()
        {
        }

        public ProductReportCollection(IList<Product> list) : base(list)
        {
        }
    }
}
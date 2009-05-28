using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class TaxCollection : BaseCollection<Tax>
    {
        public TaxCollection(BindingSource source) : base(source)
        {
        }

        public TaxCollection()
        {
        }

        public TaxCollection(IList<Tax> list) : base(list)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Collection;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Collection
{
    public class LoginModelCollection : BaseCollection<LoginModel>
    {
        public LoginModelCollection(BindingSource source) : base(source)
        {
        }

        public LoginModelCollection()
        {
        }

        public LoginModelCollection(IList<LoginModel> list) : base(list)
        {
        }
    }
}
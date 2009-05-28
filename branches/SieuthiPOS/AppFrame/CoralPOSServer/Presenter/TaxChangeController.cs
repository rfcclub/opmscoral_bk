using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter;
using CoralPOS.Interfaces.View.GoodsIO;
using NHibernate.Criterion;
using Spring.Transaction.Interceptor;

namespace CoralPOSServer.Presenter
{
    public class TaxChangeController : ITaxChangeController
    {
        private ITaxChangeView taxChangeView;
        

        public ITaxChangeView TaxChangeView
        {
            get { return taxChangeView; }
            set
            {
                taxChangeView = value;
                taxChangeView.SaveTaxEvent += new EventHandler<TaxChangeEventArgs>(taxChangeView_SaveTaxEvent);
                taxChangeView.LoadProductMastersEvent += new EventHandler<TaxChangeEventArgs>(taxChangeView_LoadProductMastersEvent);
            }
        }

        void taxChangeView_LoadProductMastersEvent(object sender, TaxChangeEventArgs e)
        {
            IList taxList = TaxLogic.FindAll(null);
            e.TaxList = taxList;
        }
        [Transaction(ReadOnly = false)]
        void taxChangeView_SaveTaxEvent(object sender, TaxChangeEventArgs e)
        {
            long maxId = TaxLogic.FindMaxId() + 1;

            foreach (Tax tax in e.TaxList)
            {
                if(tax.TaxId == 0)
                {
                    tax.TaxId = maxId++;
                    TaxLogic.Add(tax);                    
                }
                else
                {
                    TaxLogic.Update(tax);
                }
            }    
        }

        public ITaxLogic TaxLogic
        {
            get;
            set;
            
        }

        public IProductMasterLogic ProductMasterLogic
        {
            get;
            set;
        }

        public IProductLogic ProductLogic
        {
            get;
            set;
        }
    }
}

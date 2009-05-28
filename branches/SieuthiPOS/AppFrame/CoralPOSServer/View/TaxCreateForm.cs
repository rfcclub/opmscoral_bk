using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Exceptions;
using AppFrame.Utility;
using CoralPOS.Interfaces.Collection;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter;
using CoralPOS.Interfaces.View.GoodsIO;

namespace CoralPOSServer.View
{
    public partial class TaxCreateForm : BaseForm,ITaxChangeView
    {
        private TaxCollection taxCollectionList;
        

        public TaxCreateForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(!CheckIntegrity())
            {
                return;
            }
                
            string taxName = txtTaxName.Text.Trim();
            long taxValue = Int64.Parse(txtTaxValue.Text.Trim());
            Tax tax = new Tax();
            tax.CreateDate = DateTime.Now;
            tax.CreateId = ClientInfo.getInstance().LoggedUser.Name;
            tax.UpdateDate = DateTime.Now;
            tax.UpdateId = ClientInfo.getInstance().LoggedUser.Name;

            tax.TaxName = taxName;
            tax.TaxValue = taxValue;

            taxCollectionList.Add(tax);
            bdsTax.ResetBindings(false);
            dgvTax.Refresh();
            dgvTax.Invalidate();
            
        }

        private bool CheckIntegrity()
        {
            try
            {
                long test = Int64.Parse(txtTaxValue.Text.Trim());
                if (test < 0)
                    throw new BusinessException("Giá trị thuế không được âm");
            }
            catch (Exception exception)
            {
                if(exception is BusinessException)
                {
                    MessageBox.Show(exception.Message);                                            
                    
                }
                else
                {
                    MessageBox.Show("Giá trị thuế phải là số");    
                }
                return false;
            }
            return true;
        }

        private void TaxCreateForm_Load(object sender, EventArgs e)
        {
            taxCollectionList = new TaxCollection(bdsTax);
            bdsTax.ResetBindings(true);
            
            TaxChangeEventArgs loadAllTaxEventArgs = new TaxChangeEventArgs();
            EventUtility.fireEvent(LoadProductMastersEvent,this,loadAllTaxEventArgs);
            if(loadAllTaxEventArgs.TaxList!=null && loadAllTaxEventArgs.TaxList.Count > 0)
            {
                foreach (Tax tax in loadAllTaxEventArgs.TaxList)
                {
                    taxCollectionList.Add(tax);
                }
                bdsTax.ResetBindings(false);
                dgvTax.Refresh();
                dgvTax.Invalidate();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (Tax tax in taxCollectionList)
            {
                if(tax.Active != 0 && tax.Active != 1)
                {
                    throw new BusinessException("Giá trị trường hiệu lực nên là 0 hoặc 1. Xin xem lại.");
                }
            }
            if(taxCollectionList.Count <= 0)
            {
                MessageBox.Show("Không có gì để lưu !");                
            }

            TaxChangeEventArgs taxChangeEventArgs = new TaxChangeEventArgs();
            taxChangeEventArgs.TaxList = ObjectConverter.ConvertToNonGenericList(taxCollectionList);
            EventUtility.fireEvent(SaveTaxEvent,this,taxChangeEventArgs);
            if(!taxChangeEventArgs.HasErrors)
            {
                MessageBox.Show("Lưu thành công !");
            }            
        }

        private ITaxChangeController taxChangeController;
        public ITaxChangeController TaxChangeController
        {
            get { return taxChangeController; }
            set 
            { 
                taxChangeController = value;
                taxChangeController.TaxChangeView = this;
            }
        }

        public event EventHandler<TaxChangeEventArgs> LoadProductMastersEvent;
        public event EventHandler<TaxChangeEventArgs> SaveTaxEvent;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvTax_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvTax.CurrentCell.ColumnIndex == 3)
            {
                string activeStr = dgvTax.CurrentCell.Value.ToString();
                try
                {
                    long active = Int64.Parse(activeStr);
                    if(active !=0 && active != 1)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Chỉ nên gõ 0: Không hiệu lực và 1 : Cho phép hiệu lực");
                }
                int count = 0;
                Tax currentTax = taxCollectionList[dgvTax.CurrentCell.RowIndex];
                if (currentTax.Active == 1)
                {
                    foreach (Tax tax in taxCollectionList)
                    {
                        if (count != dgvTax.CurrentCell.RowIndex)
                        {
                            tax.Active = 0;
                        }
                        count += 1;
                    }
                    bdsTax.ResetBindings(false);
                    dgvTax.Refresh();
                    dgvTax.Invalidate();
                }
            }
        }
        
    }
}

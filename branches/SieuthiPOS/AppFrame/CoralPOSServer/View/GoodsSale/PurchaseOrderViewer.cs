using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoralPOSServer.View.GoodsSale
{
    public partial class PurchaseOrderViewer : Form
    {
        public PurchaseOrderViewer()
        {
            InitializeComponent();
        }

        private void PurchaseOrderViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
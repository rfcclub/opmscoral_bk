using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoralPOSClient.View.GoodsIO
{
    public partial class BarcodePreviewForm : Form
    {
        public BarcodePreviewForm()
        {
            InitializeComponent();
        }

        public Bitmap imageToPreview { get; set; }

        private void BarcodePreviewForm_Load(object sender, EventArgs e)
        {
            if (imageToPreview != null)
            {
                pictureBox.Image = imageToPreview;
            }
        }
    }
}

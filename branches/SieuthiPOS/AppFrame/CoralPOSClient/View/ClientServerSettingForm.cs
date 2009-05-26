﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Common;
using CoralPOSClient.Properties;

namespace CoralPOSClient.View
{
    public partial class ClientServerSettingForm : Form
    {
        public ClientServerSettingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                Settings.Default.IsClient = FormConstants.SERVER_MODE;
            }
            if(radioButton2.Checked)
            {
                Settings.Default.IsClient = FormConstants.CLIENT_MODE;    
            }

            Settings.Default.Save();
            Close();
        }
    }
}

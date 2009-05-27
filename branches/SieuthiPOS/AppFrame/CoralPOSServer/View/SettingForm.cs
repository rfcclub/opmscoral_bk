using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoralPOS.Common;

namespace CoralPOSServer.View
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {   
            ClientSetting.Instance.Reload();
            cboPrinters.Items.Clear();
            txtSyncImportPath.Text = ClientSetting.Instance.SyncImportPath;
            txtSyncExportPath.Text = ClientSetting.Instance.SyncExportPath;
            txtSyncErrorPath.Text = ClientSetting.Instance.SyncErrorPath;
            txtSyncSuccessPath.Text = ClientSetting.Instance.SyncSuccessPath;
            txtBackupDB.Text = ClientSetting.Instance.DBBackupPath;
            txtMySQLDump.Text = ClientSetting.Instance.MySQLDumpPath;
            PrinterSettings.StringCollection printerNames = PrinterSettings.InstalledPrinters;
            foreach (string printerName in printerNames)
            {
                cboPrinters.Items.Add(printerName);    
            }
            cboPrinters.SelectedItem = ClientSetting.Instance.PrinterName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMySQLDump.Text) 
                || !Directory.Exists(txtMySQLDump.Text)
                || !File.Exists(txtMySQLDump.Text + "\\mysqldump.exe"))
            {
                MessageBox.Show("Không thể tìm ra đường dẫn file backup dữ liệu.");
                return;
            }
            ClientSetting.Instance.DBBackupPath = txtBackupDB.Text;
            ClientSetting.Instance.MySQLDumpPath = txtMySQLDump.Text;
            ClientSetting.Instance.SyncImportPath = txtSyncImportPath.Text;
            ClientSetting.Instance.SyncExportPath = txtSyncExportPath.Text;
            ClientSetting.Instance.SyncErrorPath = txtSyncErrorPath.Text;
            ClientSetting.Instance.SyncSuccessPath = txtSyncSuccessPath.Text;
            ClientSetting.Instance.PrinterName = (string)cboPrinters.SelectedItem;
            ClientSetting.Instance.Save();
            
            MessageBox.Show("Lưu cấu hình thành công!");
            ClientSetting.Instance.Reload();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            ClientSetting.Instance.Reset();
            ClientSetting.Instance.Reload();
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientSetting.Instance.Save();
        }

        private void btnExportPath_Click(object sender, EventArgs e)
        {
            exportPathDialog.ShowDialog();
            string selectedPath = exportPathDialog.SelectedPath;

            txtSyncExportPath.Text = selectedPath.Length > 2 ? selectedPath.Substring(2): selectedPath;
        }

        private void btnImportPath_Click(object sender, EventArgs e)
        {
            importPathDialog.ShowDialog();
            string selectedPath = importPathDialog.SelectedPath;
            txtSyncImportPath.Text = selectedPath.Length > 2 ? selectedPath.Substring(2) : selectedPath;
        }

        private void btnErrorPath_Click(object sender, EventArgs e)
        {
            errorPathDialog.ShowDialog();
            string selectedPath = errorPathDialog.SelectedPath;
            txtSyncErrorPath.Text = selectedPath.Length > 2 ? selectedPath.Substring(2) : selectedPath;
        }

        private void btnSuccessPath_Click(object sender, EventArgs e)
        {
            successPathDialog.ShowDialog();
            string selectedPath = successPathDialog.SelectedPath;
            txtSyncSuccessPath.Text = selectedPath.Length > 2 ? selectedPath.Substring(2) : selectedPath;
        }

        private void btnBackupDB_Click(object sender, EventArgs e)
        {
            backupDBDialog.ShowDialog();
            string selectedPath = backupDBDialog.SelectedPath;
            txtBackupDB.Text = selectedPath.Length > 2 ? selectedPath.Substring(2) : selectedPath;
        }

        private void btnMySQLDump_Click(object sender, EventArgs e)
        {
            mySQLBinDialog.ShowDialog();
            txtMySQLDump.Text = mySQLBinDialog.SelectedPath;
        }
    }
}
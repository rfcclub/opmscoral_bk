﻿namespace AppFrameClient.View.GoodsIO.DepartmentStockData
{
    partial class LoadDatabaseImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.syncResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSyncToMain = new System.Windows.Forms.Button();
            this.dgvSyncResult = new System.Windows.Forms.DataGridView();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpMasterData = new System.Windows.Forms.GroupBox();
            this.chkDepartments = new System.Windows.Forms.CheckBox();
            this.chkPrice = new System.Windows.Forms.CheckBox();
            this.chkPrdMaster = new System.Windows.Forms.CheckBox();
            this.chkMasterData = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.syncResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncResult)).BeginInit();
            this.grpMasterData.SuspendLayout();
            this.SuspendLayout();
            // 
            // syncResultBindingSource
            // 
            this.syncResultBindingSource.DataSource = typeof(AppFrame.Model.SyncResult);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(504, 306);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Kết quả";
            // 
            // btnSyncToMain
            // 
            this.btnSyncToMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncToMain.Location = new System.Drawing.Point(12, 12);
            this.btnSyncToMain.Name = "btnSyncToMain";
            this.btnSyncToMain.Size = new System.Drawing.Size(189, 100);
            this.btnSyncToMain.TabIndex = 53;
            this.btnSyncToMain.Text = "Bắt đầu đồng bộ ";
            this.btnSyncToMain.UseVisualStyleBackColor = true;
            this.btnSyncToMain.Click += new System.EventHandler(this.btnSyncToDept_Click);
            // 
            // dgvSyncResult
            // 
            this.dgvSyncResult.AllowUserToAddRows = false;
            this.dgvSyncResult.AllowUserToDeleteRows = false;
            this.dgvSyncResult.AutoGenerateColumns = false;
            this.dgvSyncResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSyncResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileNameDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.dgvSyncResult.DataSource = this.syncResultBindingSource;
            this.dgvSyncResult.Location = new System.Drawing.Point(12, 171);
            this.dgvSyncResult.Name = "dgvSyncResult";
            this.dgvSyncResult.Size = new System.Drawing.Size(567, 129);
            this.dgvSyncResult.TabIndex = 56;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "Tên file";
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNameDataGridViewTextBoxColumn.Width = 400;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Trạng thái";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // grpMasterData
            // 
            this.grpMasterData.Controls.Add(this.chkDepartments);
            this.grpMasterData.Controls.Add(this.chkPrice);
            this.grpMasterData.Controls.Add(this.chkPrdMaster);
            this.grpMasterData.Enabled = false;
            this.grpMasterData.Location = new System.Drawing.Point(250, 35);
            this.grpMasterData.Name = "grpMasterData";
            this.grpMasterData.Size = new System.Drawing.Size(300, 77);
            this.grpMasterData.TabIndex = 57;
            this.grpMasterData.TabStop = false;
            this.grpMasterData.Text = "Thông tin chung cần đồng bộ";
            // 
            // chkDepartments
            // 
            this.chkDepartments.AutoSize = true;
            this.chkDepartments.Location = new System.Drawing.Point(158, 24);
            this.chkDepartments.Name = "chkDepartments";
            this.chkDepartments.Size = new System.Drawing.Size(137, 17);
            this.chkDepartments.TabIndex = 3;
            this.chkDepartments.Text = "Cửa hàng và nhân viên";
            this.chkDepartments.UseVisualStyleBackColor = true;
            // 
            // chkPrice
            // 
            this.chkPrice.AutoSize = true;
            this.chkPrice.Location = new System.Drawing.Point(6, 48);
            this.chkPrice.Name = "chkPrice";
            this.chkPrice.Size = new System.Drawing.Size(89, 17);
            this.chkPrice.TabIndex = 2;
            this.chkPrice.Text = "Giá mặt hàng";
            this.chkPrice.UseVisualStyleBackColor = true;
            // 
            // chkPrdMaster
            // 
            this.chkPrdMaster.AutoSize = true;
            this.chkPrdMaster.Location = new System.Drawing.Point(7, 24);
            this.chkPrdMaster.Name = "chkPrdMaster";
            this.chkPrdMaster.Size = new System.Drawing.Size(118, 17);
            this.chkPrdMaster.TabIndex = 1;
            this.chkPrdMaster.Text = "Mặt hàng, mã vạch";
            this.chkPrdMaster.UseVisualStyleBackColor = true;
            // 
            // chkMasterData
            // 
            this.chkMasterData.AutoSize = true;
            this.chkMasterData.Location = new System.Drawing.Point(257, 12);
            this.chkMasterData.Name = "chkMasterData";
            this.chkMasterData.Size = new System.Drawing.Size(144, 17);
            this.chkMasterData.TabIndex = 0;
            this.chkMasterData.Text = "Đồng bộ thông tin chung";
            this.chkMasterData.UseVisualStyleBackColor = true;
            this.chkMasterData.CheckedChanged += new System.EventHandler(this.chkMasterData_CheckedChanged);
            // 
            // LoadDatabaseImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 354);
            this.Controls.Add(this.dgvSyncResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSyncToMain);
            this.Controls.Add(this.chkMasterData);
            this.Controls.Add(this.grpMasterData);
            this.Name = "LoadDatabaseImageForm";
            this.Text = "Tao hinh anh du lieu";
            this.Controls.SetChildIndex(this.grpMasterData, 0);
            this.Controls.SetChildIndex(this.chkMasterData, 0);
            this.Controls.SetChildIndex(this.btnSyncToMain, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgvSyncResult, 0);
            ((System.ComponentModel.ISupportInitialize)(this.syncResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncResult)).EndInit();
            this.grpMasterData.ResumeLayout(false);
            this.grpMasterData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource syncResultBindingSource;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSyncToMain;
        private System.Windows.Forms.DataGridView dgvSyncResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox grpMasterData;
        private System.Windows.Forms.CheckBox chkDepartments;
        private System.Windows.Forms.CheckBox chkPrice;
        private System.Windows.Forms.CheckBox chkPrdMaster;
        private System.Windows.Forms.CheckBox chkMasterData;
    }
}
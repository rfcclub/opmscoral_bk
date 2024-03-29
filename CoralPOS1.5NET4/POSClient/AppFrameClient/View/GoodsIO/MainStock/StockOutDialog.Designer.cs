﻿namespace AppFrameClient.View.GoodsIO.MainStock
{
    partial class StockOutDialog
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
            this.lstSize = new System.Windows.Forms.ListBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lstColor = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboProductMasters = new System.Windows.Forms.ComboBox();
            this.productMasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lstProductMaster = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDepartments = new System.Windows.Forms.ComboBox();
            this.cboColors = new System.Windows.Forms.ComboBox();
            this.productColorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboSizes = new System.Windows.Forms.ComboBox();
            this.productSizeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.departmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.productMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productColorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productSizeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lstSize
            // 
            this.lstSize.DisplayMember = "SizeName";
            this.lstSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSize.FormattingEnabled = true;
            this.lstSize.ItemHeight = 14;
            this.lstSize.Location = new System.Drawing.Point(381, 60);
            this.lstSize.Name = "lstSize";
            this.lstSize.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSize.Size = new System.Drawing.Size(117, 102);
            this.lstSize.TabIndex = 121;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(342, 171);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.ReadOnly = true;
            this.txtQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtQuantity.Size = new System.Drawing.Size(156, 22);
            this.txtQuantity.TabIndex = 119;
            // 
            // lstColor
            // 
            this.lstColor.DisplayMember = "ColorName";
            this.lstColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstColor.FormattingEnabled = true;
            this.lstColor.ItemHeight = 14;
            this.lstColor.Location = new System.Drawing.Point(238, 60);
            this.lstColor.Name = "lstColor";
            this.lstColor.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstColor.Size = new System.Drawing.Size(137, 102);
            this.lstColor.TabIndex = 120;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(14, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 14);
            this.label14.TabIndex = 122;
            this.label14.Text = "Mặt hàng";
            // 
            // cboProductMasters
            // 
            this.cboProductMasters.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboProductMasters.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProductMasters.DataSource = this.productMasterBindingSource;
            this.cboProductMasters.DisplayMember = "ProductName";
            this.cboProductMasters.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProductMasters.FormattingEnabled = true;
            this.cboProductMasters.Location = new System.Drawing.Point(3, 32);
            this.cboProductMasters.Name = "cboProductMasters";
            this.cboProductMasters.Size = new System.Drawing.Size(229, 22);
            this.cboProductMasters.TabIndex = 118;
            // 
            // productMasterBindingSource
            // 
            this.productMasterBindingSource.DataSource = typeof(AppFrame.Model.ProductMaster);
            // 
            // lstProductMaster
            // 
            this.lstProductMaster.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstProductMaster.FormattingEnabled = true;
            this.lstProductMaster.ItemHeight = 14;
            this.lstProductMaster.Location = new System.Drawing.Point(3, 60);
            this.lstProductMaster.Name = "lstProductMaster";
            this.lstProductMaster.Size = new System.Drawing.Size(229, 102);
            this.lstProductMaster.TabIndex = 123;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(3, 199);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(768, 285);
            this.dataGridView1.TabIndex = 124;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Mã vạch";
            this.Column8.Name = "Column8";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tên hàng";
            this.Column3.Name = "Column3";
            this.Column3.Width = 170;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Màu sắc";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "K.cỡ";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Số lượng";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Cửa hàng";
            this.Column7.Name = "Column7";
            this.Column7.Width = 150;
            // 
            // dgvDepartment
            // 
            this.dgvDepartment.AllowUserToAddRows = false;
            this.dgvDepartment.AllowUserToDeleteRows = false;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvDepartment.Location = new System.Drawing.Point(504, 60);
            this.dgvDepartment.MultiSelect = false;
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.RowHeadersWidth = 20;
            this.dgvDepartment.Size = new System.Drawing.Size(267, 133);
            this.dgvDepartment.TabIndex = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Cửa hàng";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Số lượng";
            this.Column2.Name = "Column2";
            this.Column2.Width = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 14);
            this.label1.TabIndex = 126;
            this.label1.Text = "Số lượng trong kho:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cboDepartments
            // 
            this.cboDepartments.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDepartments.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDepartments.DataSource = this.departmentBindingSource;
            this.cboDepartments.DisplayMember = "DepartmentName";
            this.cboDepartments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartments.FormattingEnabled = true;
            this.cboDepartments.Location = new System.Drawing.Point(504, 32);
            this.cboDepartments.Name = "cboDepartments";
            this.cboDepartments.Size = new System.Drawing.Size(267, 22);
            this.cboDepartments.TabIndex = 127;
            // 
            // cboColors
            // 
            this.cboColors.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboColors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboColors.DataSource = this.productColorBindingSource;
            this.cboColors.DisplayMember = "ColorName";
            this.cboColors.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboColors.FormattingEnabled = true;
            this.cboColors.Location = new System.Drawing.Point(239, 32);
            this.cboColors.Name = "cboColors";
            this.cboColors.Size = new System.Drawing.Size(136, 22);
            this.cboColors.TabIndex = 128;
            // 
            // productColorBindingSource
            // 
            this.productColorBindingSource.DataSource = typeof(AppFrame.Model.ProductColor);
            // 
            // cboSizes
            // 
            this.cboSizes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSizes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSizes.DataSource = this.productSizeBindingSource;
            this.cboSizes.DisplayMember = "SizeName";
            this.cboSizes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSizes.FormattingEnabled = true;
            this.cboSizes.Location = new System.Drawing.Point(381, 32);
            this.cboSizes.Name = "cboSizes";
            this.cboSizes.Size = new System.Drawing.Size(117, 22);
            this.cboSizes.TabIndex = 129;
            // 
            // productSizeBindingSource
            // 
            this.productSizeBindingSource.DataSource = typeof(AppFrame.Model.ProductSize);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(670, 490);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 42);
            this.btnConfirm.TabIndex = 130;
            this.btnConfirm.Text = "Nhập";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(17, 490);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 42);
            this.btnHelp.TabIndex = 131;
            this.btnHelp.Text = "Giúp đỡ";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(181, 490);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 42);
            this.btnDelete.TabIndex = 132;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // departmentBindingSource
            // 
            this.departmentBindingSource.DataSource = typeof(AppFrame.Model.Department);
            // 
            // StockOutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 544);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cboSizes);
            this.Controls.Add(this.cboColors);
            this.Controls.Add(this.cboDepartments);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDepartment);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lstProductMaster);
            this.Controls.Add(this.lstSize);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lstColor);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cboProductMasters);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StockOutDialog";
            this.Text = "StockOutDialog";
            this.Load += new System.EventHandler(this.StockOutDialog_Load);
            this.Controls.SetChildIndex(this.cboProductMasters, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.lstColor, 0);
            this.Controls.SetChildIndex(this.txtQuantity, 0);
            this.Controls.SetChildIndex(this.lstSize, 0);
            this.Controls.SetChildIndex(this.lstProductMaster, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.dgvDepartment, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cboDepartments, 0);
            this.Controls.SetChildIndex(this.cboColors, 0);
            this.Controls.SetChildIndex(this.cboSizes, 0);
            this.Controls.SetChildIndex(this.btnConfirm, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            ((System.ComponentModel.ISupportInitialize)(this.productMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productColorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productSizeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstSize;
        public System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.ListBox lstColor;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboProductMasters;
        private System.Windows.Forms.ListBox lstProductMaster;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDepartments;
        private System.Windows.Forms.ComboBox cboColors;
        private System.Windows.Forms.ComboBox cboSizes;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.BindingSource productMasterBindingSource;
        private System.Windows.Forms.BindingSource productColorBindingSource;
        private System.Windows.Forms.BindingSource productSizeBindingSource;
        private System.Windows.Forms.BindingSource departmentBindingSource;
    }
}
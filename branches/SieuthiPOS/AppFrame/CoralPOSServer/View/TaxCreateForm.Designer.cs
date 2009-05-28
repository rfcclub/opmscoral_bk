namespace CoralPOSServer.View
{
    partial class TaxCreateForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTax = new System.Windows.Forms.DataGridView();
            this.bdsTax = new System.Windows.Forms.BindingSource(this.components);
            this.txtTaxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaxValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.TaxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsTax)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTax
            // 
            this.dgvTax.AllowUserToAddRows = false;
            this.dgvTax.AllowUserToDeleteRows = false;
            this.dgvTax.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTax.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTax.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTax.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaxId,
            this.TaxName,
            this.TaxValue,
            this.Active});
            this.dgvTax.DataSource = this.bdsTax;
            this.dgvTax.Location = new System.Drawing.Point(13, 115);
            this.dgvTax.Name = "dgvTax";
            this.dgvTax.Size = new System.Drawing.Size(620, 312);
            this.dgvTax.TabIndex = 0;
            this.dgvTax.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTax_CellEndEdit);
            // 
            // txtTaxName
            // 
            this.txtTaxName.Location = new System.Drawing.Point(78, 45);
            this.txtTaxName.Name = "txtTaxName";
            this.txtTaxName.Size = new System.Drawing.Size(209, 20);
            this.txtTaxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Loại thuế:";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(293, 45);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(80, 43);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(558, 433);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Danh sách loại thuế";
            // 
            // txtTaxValue
            // 
            this.txtTaxValue.Location = new System.Drawing.Point(78, 72);
            this.txtTaxValue.Name = "txtTaxValue";
            this.txtTaxValue.Size = new System.Drawing.Size(209, 20);
            this.txtTaxValue.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Giá trị:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 433);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Lưu vào cơ sở dữ liệu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TaxId
            // 
            this.TaxId.DataPropertyName = "TaxId";
            this.TaxId.HeaderText = "Mã thuế";
            this.TaxId.Name = "TaxId";
            this.TaxId.ReadOnly = true;
            // 
            // TaxName
            // 
            this.TaxName.DataPropertyName = "TaxName";
            this.TaxName.HeaderText = "Tên thuế";
            this.TaxName.Name = "TaxName";
            this.TaxName.Width = 200;
            // 
            // TaxValue
            // 
            this.TaxValue.DataPropertyName = "TaxValue";
            this.TaxValue.HeaderText = "Giá trị thuế";
            this.TaxValue.Name = "TaxValue";
            this.TaxValue.Width = 120;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Hiệu lực";
            this.Active.Name = "Active";
            this.Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Active.Width = 155;
            // 
            // TaxCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 472);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTaxValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTaxName);
            this.Controls.Add(this.dgvTax);
            this.Name = "TaxCreateForm";
            this.Text = "Tạo và sửa chữa loại thuế";
            this.Load += new System.EventHandler(this.TaxCreateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsTax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTax;
        private System.Windows.Forms.TextBox txtTaxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.BindingSource bdsTax;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaxValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
    }
}
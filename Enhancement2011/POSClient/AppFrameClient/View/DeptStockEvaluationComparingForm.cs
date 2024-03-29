﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Model;
using AppFrame.Utility;
using AppFrameClient.MasterDBTableAdapters;
using AppFrameClient.Utility;
using InfoBox;

namespace AppFrameClient.View
{
    public partial class DeptStockEvaluationComparingForm : Form
    {
        private IList ReviewTypeList = new ArrayList();
        public DeptStockEvaluationComparingForm()
        {
            InitializeComponent();
        }

        private void DeptStockEvaluationComparingForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'masterDB4.stock_evaluation' table. You can move, or remove it, as needed.
            //this.stock_evaluationTableAdapter.Fill(this.masterDB4.stock_evaluation);
            // TODO: This line of code loads data into the 'masterDB3.mainstkqty' table. You can move, or remove it, as needed.
            this.mainstkqtyTableAdapter.Fill(this.masterDB3.mainstkqty);
            // TODO: This line of code loads data into the 'masterDB2.product_type' table. You can move, or remove it, as needed.
            this.product_typeTableAdapter.Fill(this.masterDB2.product_type);
            // TODO: This line of code loads data into the 'masterDB.Department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.masterDB.Department);

            Department titleStock = new Department
                                       {
                                           DepartmentId = -1,
                                           DepartmentName = "-- CHON KHO ----"
            };
            departmentBindingSource.Add(titleStock);

            Department mainStock = new Department
            {
                DepartmentId = 0,
                DepartmentName = "KHO CHINH"
            };
            departmentBindingSource.Add(mainStock);
            Department chooseDepartment = null;
            foreach (MasterDB.DepartmentRow row in masterDB.Department)
            {
                Department department = new Department
                {
                    DepartmentId = row.DEPARTMENT_ID,
                    DepartmentName = row.DEPARTMENT_NAME
                };
                if(row.DEPARTMENT_ID == CurrentDepartment.Get().DepartmentId)
                {
                    chooseDepartment = department;
                }
                departmentBindingSource.Add(department);
            }
            departmentBindingSource.ResetBindings(false);
            
            InitTypeList();

            if (chooseDepartment != null)
            {
                cboDepartments.SelectedItem = chooseDepartment;
                cboDepartments.Enabled = false;
            }
            if(CurrentDepartment.Get().DepartmentId == 0)
            {
                txtBarcode.Enabled = false;
            }
            this.masterDB1.EnforceConstraints = false;
            this.masterDB3.EnforceConstraints = false;
        }

        private void InitTypeList()
        {
            // TODO: This line of code loads data into the 'masterDB2.product_type' table. You can move, or remove it, as needed.
            this.product_typeTableAdapter.Fill(this.masterDB2.product_type);
            cboTypeBds.Clear();
            ProductType allTypes = new ProductType
            {
                TypeId = 0,
                TypeName = "-- TẤT CẢ MẶT HÀNG --"
            };
            cboTypeBds.Add(allTypes);
            foreach (MasterDB.product_typeRow row in masterDB2.product_type)
            {
                ProductType productType = new ProductType
                {
                    TypeId = row.TYPE_ID,
                    TypeName = row.TYPE_NAME
                };
                cboTypeBds.Add(productType);
            }
            cboTypeBds.ResetBindings(false);
        }

        private void cboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartments.SelectedValue == null) return;
            int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());
                         
            if(deptId > 0)
            {
                dgvDeptStock.Visible = true;
                dgvStock.Visible = false;
                this.stockqtyTableAdapter.Fill(masterDB1.stockqty, deptId);
                stockqtyBindingSource.ResetBindings(false);
                dgvDeptStock.Refresh();
                dgvDeptStock.Invalidate();
            }
            else
            {
                if (deptId == 0)
                {
                    dgvDeptStock.Visible = false;
                    dgvStock.Visible = true;
                    this.mainstkqtyTableAdapter.Fill(masterDB3.mainstkqty);
                    mainstkqtyBindingSource.ResetBindings(false);
                    dgvStock.Refresh();
                    dgvStock.Invalidate();
                }
            }
            CalculateTotal();

        }

        private void CalculateTotal()
        {
            if (null == cboDepartments.SelectedValue) return;
            int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());
            int typeId = 0;
            ClientUtility.TryActionHelper(delegate()
            {
                typeId = Int32.Parse(cboTypes.SelectedValue.ToString());
            }, 1);
            long sl = 0;
            long slthuc = 0;
            if(deptId > 0)
            {
                foreach (DataGridViewRow row in dgvDeptStock.Rows)
                {
                    ClientUtility.TryActionHelper(delegate() {
                                                                sl = sl + Int32.Parse(row.Cells["SLColumn"].Value.ToString())
                                                                ;
                                                            },1);
                    ClientUtility.TryActionHelper(delegate()
                    {
                        slthuc = slthuc + Int32.Parse(row.Cells["SLThucColumn"].Value.ToString())
                        ;
                    }, 1);
                    
                }
            }
            else
            {
                if (deptId == 0)
                {
                    foreach (DataGridViewRow row in dgvStock.Rows)
                    {
                        ClientUtility.TryActionHelper(delegate()
                                                          {
                                                              sl = sl +
                                                                   Int32.Parse(
                                                                       row.Cells["MainSLColumn"].Value.ToString())
                                                                  ;
                                                          }, 1);
                        ClientUtility.TryActionHelper(delegate()
                                                          {
                                                              slthuc = slthuc +
                                                                       Int32.Parse(
                                                                           row.Cells["MainSLThucColumn"].Value.ToString())
                                                                  ;
                                                          }, 1);
                    }
                }
            }

            txtSL.Text = sl.ToString();
            txtSLThuc.Text = slthuc.ToString();
        }

        private void cboTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataset();
        }

        private void FilterDataset()
        {
            int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());
            int typeId = 0;
            ClientUtility.TryActionHelper(delegate()
                                              {
                                                  typeId = Int32.Parse(cboTypes.SelectedValue.ToString());
                                              },1);
            
            if (deptId > 0)
            {
                if (typeId != 0)
                {
                    stockqtyBindingSource.Filter = "type_id = " + typeId;
                }
                else
                {
                    stockqtyBindingSource.Filter = "";
                    for (int i = 0; i < cboTypeBds.Count; i++)
                    {
                        ProductType type = (ProductType)cboTypeBds[i];
                        string strAnd = "";
                        if (!string.IsNullOrEmpty(stockqtyBindingSource.Filter))
                        {
                            strAnd = " OR ";    
                        }
                        stockqtyBindingSource.Filter += strAnd + " type_id = " + type.TypeId.ToString() + " ";
                    }
                }
                if (chkDifferent.Checked)
                {
                    string strAnd = "";
                    if (!string.IsNullOrEmpty(stockqtyBindingSource.Filter))
                    {
                        strAnd = " AND ";
                    }
                    stockqtyBindingSource.Filter += strAnd + " ( quantity <> realquantity ) ";
                }

                stockqtyBindingSource.ResetBindings(false);
                dgvDeptStock.Refresh();
                dgvDeptStock.Invalidate();
            }
            else
            {
                if (deptId == 0)
                {
                    if (typeId != 0)
                    {
                        mainstkqtyBindingSource.Filter = "type_id = " + typeId;
                    }
                    else
                    {
                        mainstkqtyBindingSource.Filter = "";


                        for (int i = 0; i < cboTypeBds.Count;i++ )
                        {
                            ProductType type = (ProductType)cboTypeBds[i];
                            string strAnd = "";
                            if (!string.IsNullOrEmpty(mainstkqtyBindingSource.Filter))
                            {
                                strAnd = " OR ";
                            }
                            mainstkqtyBindingSource.Filter += strAnd + " type_id = " + type.TypeId.ToString() + " ";
                        }
                    }
                    if (chkDifferent.Checked)
                    {
                        string strAnd = "";
                        if (!string.IsNullOrEmpty(mainstkqtyBindingSource.Filter))
                        {
                            strAnd = " AND ";
                        }
                        mainstkqtyBindingSource.Filter += strAnd + " ( quantity <> realquantity ) ";
                    }

                    mainstkqtyBindingSource.ResetBindings(false);
                    dgvStock.Refresh();
                    dgvStock.Invalidate();
                }
            }
            CalculateTotal();
        }

        private ErrorForm _errorForm = null;
        private void btnImportResult_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.Filter = "Text Files|*.txt";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ReviewTypeList.Clear();
                Dictionary<string, int> list = new Dictionary<string, int>();
                string path = fileDialog.FileName;
                StreamReader fileReader = new StreamReader(File.OpenRead(path));

                while (!fileReader.EndOfStream)
                {
                    string line = fileReader.ReadLine();
                    string[] parseLines = line.Split(',');

                    try
                    {
                        if (parseLines.Length == 2)
                        {
                            if (list.ContainsKey(parseLines[0].Trim()))
                            {
                                list[parseLines[0].Trim()] += Int32.Parse(parseLines[1].Trim());
                            }
                            else
                            {
                                list.Add(parseLines[0].Trim(), Int32.Parse(parseLines[1].Trim()));
                            }

                        }
                        else
                        {
                            if (list.ContainsKey(parseLines[0].Trim()))
                            {
                                list[parseLines[0].Trim()] += 1;
                            }
                            else
                            {
                                list.Add(parseLines[0].Trim(), 1);
                            }

                        }
                    }
                    catch (Exception)
                    {
                        if (_errorForm == null)
                        {
                            _errorForm = new ErrorForm();
                            _errorForm.Caption = "Lỗi";
                            _errorForm.ErrorString = "Các mã vạch bị lỗi khi nhập mã vạch từ file text";
                        }
                        _errorForm.ErrorDetails.Add(line);
                        continue;
                    }
                }

                int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());
                
                if (deptId > 0)
                {
                    
                    IDictionary<string, int> lackingIdList = new Dictionary<string, int>();

                    foreach (KeyValuePair<string, int> barCodeLine in list)
                    {
                        bool found = false;
                        foreach (MasterDB.stockqtyRow stockqtyRow in masterDB1.stockqty)
                        {
                            //if (barCodeLine.Key.Equals(stockqtyRow["PRODUCT_ID"].ToString()))
                            if (barCodeLine.Key.Equals(stockqtyRow.PRODUCT_ID))
                            {
                                stockqtyRow.realquantity = barCodeLine.Value;
                                AddToReviewTypeList(stockqtyRow.TYPE_ID.ToString());

                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            lackingIdList.Add(barCodeLine);
                        }

                    }
                    if(lackingIdList.Count > 0)
                    {
                        foreach (KeyValuePair<string, int> barCodeLine in lackingIdList)
                        {
                            foreach (MasterDB.mainstkqtyRow stockqtyRow in masterDB3.mainstkqty)
                            {
                                if (barCodeLine.Key.Equals(stockqtyRow.PRODUCT_ID.ToString()))
                                {
                                    AddToReviewTypeList(stockqtyRow.TYPE_ID.ToString());
                                    masterDB1.stockqty.AddstockqtyRow(
                                        stockqtyRow.TYPE_ID,
                                        stockqtyRow.TYPE_NAME,
                                        stockqtyRow.PRODUCT_MASTER_ID,
                                        barCodeLine.Key,
                                        stockqtyRow.PRODUCT_NAME,
                                        stockqtyRow.COLOR_NAME,
                                        stockqtyRow.SIZE_NAME,
                                        barCodeLine.Key,
                                        0,
                                        0,
                                        barCodeLine.Value
                                        );
                                    break;
                                }
                            }

                        } 
                    }
                    
                    stockqtyBindingSource.ResetBindings(false);
                    dgvDeptStock.Refresh();
                    dgvDeptStock.Invalidate();
                }
                else
                {
                    
                    foreach (KeyValuePair<string, int> barCodeLine in list)
                    {
                        foreach (MasterDB.mainstkqtyRow stockqtyRow in masterDB3.mainstkqty)
                        {
                            if (barCodeLine.Key.Equals(stockqtyRow.PRODUCT_ID.ToString()))
                            {
                                stockqtyRow.realquantity = barCodeLine.Value;
                                AddToReviewTypeList(stockqtyRow.TYPE_ID.ToString());
                                
                                break;
                            }
                        }
                        
                    }

                    stockqtyBindingSource.ResetBindings(false);
                    dgvDeptStock.Refresh();
                    dgvDeptStock.Invalidate();
                }
                
                
                // filter cbbtypes
                CreateTypeList(ReviewTypeList);
                FilterDataset();
                CalculateTotal();
                cboDepartments.Enabled = false;
            }
        }

        private void CreateTypeList(IList list)
        {
            // TODO: This line of code loads data into the 'masterDB2.product_type' table. You can move, or remove it, as needed.
            this.product_typeTableAdapter.Fill(this.masterDB2.product_type);
            cboTypeBds.Clear();
            ProductType allTypes = new ProductType
            {
                TypeId = 0,
                TypeName = "-- TẤT CẢ MẶT HÀNG --"
            };
            cboTypeBds.Add(allTypes);
            ProductType unknownType = new ProductType
            {
                TypeId = 9999,
                TypeName = "++ UNKNOWN TYPE ++"
            };
            cboTypeBds.Add(unknownType);

            foreach (MasterDB.product_typeRow row in masterDB2.product_type)
            {
                ProductType productType = new ProductType
                {
                    TypeId = row.TYPE_ID,
                    TypeName = row.TYPE_NAME
                };
                if (ExistInReviewTypeList(ReviewTypeList, productType))
                {
                    cboTypeBds.Add(productType);
                }
            }
            cboTypeBds.ResetBindings(false);
        }

        private bool ExistInReviewTypeList(IList list, ProductType type)
        {
            bool found = false;
            foreach (string typeId in list)
            {
                if(typeId.Equals(type.TypeId.ToString()))
                {
                    found = true;
                }
            }
            return found;
        }

        private void AddToReviewTypeList(string typeId)
        {
            bool found = false;
            foreach (string type in ReviewTypeList)
            {
                if(type.Equals(typeId))
                {
                    found = true;
                }
            }
            if (!found) ReviewTypeList.Add(typeId);
        }

        private void chkDifferent_CheckedChanged(object sender, EventArgs e)
        {
            FilterDataset();
        }

        private void chkSLEquals_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSLEquals.Checked)
            {
                int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());
                if(deptId > 0)
                {
                    foreach (DataGridViewRow selectedRow in dgvDeptStock.SelectedRows)
                    {
                        selectedRow.Cells["SLThucColumn"].Value = selectedRow.Cells["SLColumn"].Value;
                    }
                }
                else
                {
                    if (deptId == 0)
                    {
                        foreach (DataGridViewRow selectedRow in dgvDeptStock.SelectedRows)
                        {
                            selectedRow.Cells["MainSLThucColumn"].Value = selectedRow.Cells["MainSLColumn"].Value;
                        }
                    }
                }
            }
            chkSLEquals.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                stock_evaluationTableAdapter.Fill(masterDB4.stock_evaluation);

                int typeIndex = cboTypes.SelectedIndex;
                bool chkDiffer = chkDifferent.Checked;
                DateTime saveTime = DateTime.Now;
                cboTypes.SelectedIndex = 0;
                chkDifferent.Checked = false;

                int deptId = Int32.Parse(cboDepartments.SelectedValue.ToString());

                if (deptId > 0)
                {
                    foreach (DataGridViewRow row in dgvDeptStock.Rows)
                    {
                        masterDB4.stock_evaluation.Addstock_evaluationRow(
                            UInt32.Parse(row.Cells[0].Value.ToString()),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            Int32.Parse(row.Cells[7].Value.ToString()),
                            Int32.Parse(row.Cells[7].Value.ToString()),
                            Int32.Parse(row.Cells[8].Value.ToString()),
                            saveTime,
                            (uint)deptId,
                            ((Department)cboDepartments.SelectedItem).DepartmentName
                            );
                    }

                }
                else
                {
                    if (deptId == 0)
                    {
                        foreach (DataGridViewRow row in dgvStock.Rows)
                        {
                            masterDB4.stock_evaluation.Addstock_evaluationRow(
                                UInt32.Parse(row.Cells[0].Value.ToString()),
                                row.Cells[1].Value.ToString(),
                                row.Cells[2].Value.ToString(),
                                row.Cells[6].Value.ToString(),
                                row.Cells[3].Value.ToString(),
                                row.Cells[4].Value.ToString(),
                                row.Cells[5].Value.ToString(),
                                Int32.Parse(row.Cells[7].Value.ToString()),
                                Int32.Parse(row.Cells[8].Value.ToString()),
                                Int32.Parse(row.Cells[9].Value.ToString()),
                                saveTime,
                                (uint)deptId,
                                ((Department)cboDepartments.SelectedItem).DepartmentName
                                );
                        }
                    }
                }
                if (masterDB4.stock_evaluation.Count > 0)
                {
                    stock_evaluationTableAdapter.Update(masterDB4);
                }
                InfoBox.InformationBox.Show("Lưu thành công", new AutoCloseParameters(1));
            }
            catch (Exception)
            {

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitTypeList();
            cboDepartments.Enabled = true;
            cboDepartments_SelectedIndexChanged(null, null);
        }

        private void dgvStock_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvDeptStock_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            IList lackingIdList = new ArrayList();
            if (!string.IsNullOrEmpty(txtBarcode.Text) && txtBarcode.Text.Length == 12)
            {
                    string barCode = txtBarcode.Text;

                    bool found = false;
                    foreach (MasterDB.stockqtyRow stockqtyRow in masterDB1.stockqty)
                    {
                        //if (barCodeLine.Key.Equals(stockqtyRow["PRODUCT_ID"].ToString()))
                        if (barCode.Equals(stockqtyRow.PRODUCT_ID))
                        {
                            stockqtyRow.realquantity += 1;
                            AddToReviewTypeList(stockqtyRow.TYPE_ID.ToString());

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                                                
                            lackingIdList.Add(barCode);
                        
                    }
                
                if (lackingIdList.Count > 0)
                {
                    foreach (string barCodeLine in lackingIdList)
                    {
                        AddToReviewTypeList("9999");
                        masterDB1.stockqty.AddstockqtyRow(
                            9999,
                            "UNKNOWN",
                            "9999999999999",
                            barCodeLine,
                            "UNKNOWN PRODUCT",
                            "UNKNOWN",
                            "UNKNOWN",
                            barCodeLine,
                            0,
                            0,
                            1
                            );

                     

                    }
                }

            stockqtyBindingSource.ResetBindings(false);
            dgvDeptStock.Refresh();
            dgvDeptStock.Invalidate();
            CreateTypeList(ReviewTypeList);
            FilterDataset();
            CalculateTotal();
            txtBarcode.Text = "";
            }
            
        }
        
    }

    class TypeViewObject
    {
        int TypeId { get; set; }
        string TypeName { get; set; }
    }
}

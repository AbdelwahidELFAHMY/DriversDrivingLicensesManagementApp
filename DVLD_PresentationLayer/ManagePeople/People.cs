using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class People : Form
    {
        public People()
        {
            InitializeComponent();
        }
        public void _RefreshPeople()
        {
            DataTable dt = clsPerson.GetPeople();
            DGVPeople.DataSource = dt;

            if (cmbRowsFilter.Items.Count == 0)
            {
                cmbRowsFilter.Items.Add("None");
                foreach (DataColumn col in dt.Columns)
                {
                    cmbRowsFilter.Items.Add(col.ColumnName);
                }
            }
            cmbRowsFilter.SelectedIndex = 0;
        }

        private void People_Load(object sender, EventArgs e)
        {
            _RefreshPeople();
        }

        private void cmbRowsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRowsFilter.SelectedItem.ToString() == "None")
            {
                DGVPeople.DataSource = clsPerson.GetPeople();
                txtSearchValue.Visible = false;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
            }
            else if(cmbRowsFilter.SelectedItem.ToString() == "DateOfBirth")
            {
                txtSearchValue.Visible = false;
                DTPFilter.Visible = true;
                btnDateFilter.Visible = true;
            }
            else
            {
                txtSearchValue.Visible = true;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
            }
        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
                string selectedColumn = cmbRowsFilter.SelectedItem.ToString();
                string filterValue = txtSearchValue.Text;

                if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
                {
                    DataTable dt = clsPerson.GetPeople();
                    DataView dv = dt.DefaultView;

                    if (dt.Columns[selectedColumn].DataType == typeof(int) || dt.Columns[selectedColumn].DataType == typeof(decimal) ||
                        dt.Columns[selectedColumn].DataType == typeof(double) ||  dt.Columns[selectedColumn].DataType == typeof(float))
                    {
                        dv.RowFilter = $"CONVERT({selectedColumn}, 'System.String') LIKE '%{filterValue}%'";
                    }
                    else
                    {
                        dv.RowFilter = $"{selectedColumn} LIKE '%{filterValue}%'";
                    }

                    DGVPeople.DataSource = dv.ToTable();
                }
                else
                {
                    DGVPeople.DataSource = clsPerson.GetPeople();
                }

        }

        private void btnDateFilter_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DTPFilter.Value;

            DataTable dt = clsPerson.GetPeople();
            DataView dv = dt.DefaultView;
          
          string formattedDate = selectedDate.ToString("M/d/yyyy");

          dv.RowFilter = $"CONVERT({"DateOfBirth"}, 'System.String') LIKE '%{formattedDate}%'";

          DGVPeople.DataSource = dv.ToTable();
          
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(-1);
            addEditPerson.ShowDialog();
            _RefreshPeople();   
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson((int)DGVPeople.CurrentRow.Cells[0].Value);
            addEditPerson.ShowDialog();
            _RefreshPeople();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(-1);
            addEditPerson.ShowDialog();
            _RefreshPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + DGVPeople.CurrentRow.Cells[0].Value + "]?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)DGVPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPeople() ;
                }
                else
                {
                    MessageBox.Show("Person is not deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails personDetails = new PersonDetails((int)DGVPeople.CurrentRow.Cells[0].Value);
            personDetails.ShowDialog();
            _RefreshPeople();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVPeople.CurrentRow != null)
            {
                string email = DGVPeople.CurrentRow.Cells["Email"].Value.ToString();
                if (!string.IsNullOrEmpty(email))
                {
                    try
                    {
                        System.Diagnostics.Process.Start($"mailto:{email}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open email client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No email address found for the selected person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVPeople.CurrentRow != null)
            {
                string phoneNumber = DGVPeople.CurrentRow.Cells["Phone"].Value.ToString();
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    try
                    {
                        System.Diagnostics.Process.Start($"tel:{phoneNumber}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to initiate phone call: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No phone number found for the selected person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

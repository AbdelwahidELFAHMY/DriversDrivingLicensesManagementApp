using DVLD_BusinessLayer;
using DVLD_Project.Manage_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Drivers
{
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
            DataTable dt = clsLicense.GetDrivers();
            DGVDrivers.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnDateFilter_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DTPFilter.Value;

            DataTable dt = clsLicense.GetDrivers();
            DataView dv = dt.DefaultView;

            string formattedDate = selectedDate.ToString("M/d/yyyy");

            dv.RowFilter = $"CONVERT({"Date"}, 'System.String') LIKE '%{formattedDate}%'";

            DGVDrivers.DataSource = dv.ToTable();
        }

        private void cmbRowsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGVDrivers.DataSource = clsLicense.GetDrivers();
            if (cmbRowsFilter.SelectedItem.ToString() == "None")
            { 
                txtSearchValue.Visible = false;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
            }
            else if (cmbRowsFilter.SelectedItem.ToString() == "Date")
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
                DataTable dt = clsLicense.GetDrivers();
                DataView dv = dt.DefaultView;

                if (dt.Columns[selectedColumn].DataType == typeof(int) || dt.Columns[selectedColumn].DataType == typeof(decimal) ||
                    dt.Columns[selectedColumn].DataType == typeof(double) || dt.Columns[selectedColumn].DataType == typeof(float))
                {
                    dv.RowFilter = $"CONVERT({selectedColumn}, 'System.String') LIKE '%{filterValue}%'";
                }
                else
                {
                    dv.RowFilter = $"{selectedColumn} LIKE '%{filterValue}%'";
                }

                DGVDrivers.DataSource = dv.ToTable();
            }
            else
            {
                DGVDrivers.DataSource = clsLicense.GetDrivers();
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frm = new PersonDetails((int)DGVDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showLicensesHistoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LicensesHistory frm = new LicensesHistory((int)DGVDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void lblRecords_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DGVDrivers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DTPFilter_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

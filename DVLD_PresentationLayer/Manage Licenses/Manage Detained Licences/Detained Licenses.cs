using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using DVLD_Project.Manage_Licenses.ReleaseDetainedLicense;

namespace DVLD_Project.Manage_Licenses.Manage_Detained_Licences
{
    public partial class DetainedLicenses : Form
    {
        private int _PersonID;
        public DetainedLicenses(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            DataTable dt = clsLicense.GetDetainedLicenses();
            DGVDetainedLicenses.DataSource = dt;
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

        private void cmbRowsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGVDetainedLicenses.DataSource = clsLicense.GetDetainedLicenses();
            if (cmbRowsFilter.SelectedItem.ToString() == "None")
            {
                txtSearchValue.Visible = false;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
                cmboxisReleased.Visible = false;    
            }
            else if (cmbRowsFilter.SelectedItem.ToString() == "DetainDate" || cmbRowsFilter.SelectedItem.ToString() == "ReleaseDate")
            {
                txtSearchValue.Visible = false;
                cmboxisReleased.Visible=false;
                DTPFilter.Visible = true;
                btnDateFilter.Visible = true;
            }
            else if (cmbRowsFilter.SelectedItem.ToString() == "IsReleased")
            {
                txtSearchValue.Visible = false;
                cmboxisReleased.Visible = true;
                cmboxisReleased.SelectedIndex = 0;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
            }
            else {
                txtSearchValue.Visible = true;
                DTPFilter.Visible = false;
                btnDateFilter.Visible = false;
                cmboxisReleased.Visible = false;
            }
        }

        private void btnDateFilter_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DTPFilter.Value;

            DataTable dt = clsLicense.GetDetainedLicenses();
            DataView dv = dt.DefaultView;

            string formattedDate = selectedDate.ToString("M/d/yyyy");



            if (cmbRowsFilter.SelectedItem.ToString() == "DetainDate" || cmbRowsFilter.SelectedItem.ToString() == "ReleaseDate")
                dv.RowFilter = $"CONVERT({"DetainDate"}, 'System.String') LIKE '%{formattedDate}%'";
            else
                dv.RowFilter = $"CONVERT({"ReleaseDate"}, 'System.String') LIKE '%{formattedDate}%'";
              DGVDetainedLicenses.DataSource = dv.ToTable();

        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {

            string selectedColumn = cmbRowsFilter.SelectedItem.ToString();
            string filterValue = txtSearchValue.Text;

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
            {
                DataTable dt = clsLicense.GetDetainedLicenses();
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

                DGVDetainedLicenses.DataSource = dv.ToTable();
            }
            else
            {
                DGVDetainedLicenses.DataSource = clsLicense.GetDetainedLicenses();
            }
        }

        private void cmboxisReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = clsLicense.GetDetainedLicenses();
            DataView dv = dt.DefaultView;

            switch (cmboxisReleased.SelectedItem.ToString())
            {
                case "All":
                    DGVDetainedLicenses.DataSource = dt;
                    break;

                case "Yes":
                    dv.RowFilter = "IsReleased = true"; 
                    DGVDetainedLicenses.DataSource = dv;
                    break;

                case "No":
                    dv.RowFilter = "IsReleased = false"; 
                    DGVDetainedLicenses.DataSource = dv; 
                    break;

                default:
                    break;
            }
        }

        private void picDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicense frm = new DetainLicense(_PersonID);
            frm.ShowDialog();
            DataTable dt = clsLicense.GetDetainedLicenses();
            DGVDetainedLicenses.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }

        private void picRelease_Click(object sender, EventArgs e)
        {

            ReleaseDetainedLicense.ReleaseDetainedLicense frm = new ReleaseDetainedLicense.ReleaseDetainedLicense(_PersonID);
            frm.ShowDialog();

            DataTable dt = clsLicense.GetDetainedLicenses();
            DGVDetainedLicenses.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }

        private void DGVDetainedLicenses_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = DGVDetainedLicenses.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {

                    DGVDetainedLicenses.ClearSelection();
                    DGVDetainedLicenses.Rows[hit.RowIndex].Selected = true;

                    DataGridViewRow selectedRow = DGVDetainedLicenses.Rows[hit.RowIndex];

                    bool IsReleased = (bool)selectedRow.Cells["IsReleased"].Value;

                    if (IsReleased)
                        contextMenuStrip1.Items["releaseDetainedLicenseToolStripMenuItem"].Enabled = false;
                    else
                        contextMenuStrip1.Items["releaseDetainedLicenseToolStripMenuItem"].Enabled = true;

                    contextMenuStrip1.Show(DGVDetainedLicenses, new Point(e.X, e.Y));
                }
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson SelectedPerson = clsPerson.FindPersonByNationalNo(DGVDetainedLicenses.CurrentRow.Cells["NationalNo"].Value.ToString());
            PersonDetails frm = new PersonDetails(SelectedPerson.PersonID);
            frm.ShowDialog();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicense frm = new ShowLicense((int)DGVDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            clsPerson SelectedPerson = clsPerson.FindPersonByNationalNo(DGVDetainedLicenses.CurrentRow.Cells["NationalNo"].Value.ToString());

            LicensesHistory frm = new LicensesHistory(SelectedPerson.PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense.ReleaseDetainedLicense frm = new ReleaseDetainedLicense.ReleaseDetainedLicense(_PersonID);
            frm.ShowDialog();
            DataTable dt = clsLicense.GetDetainedLicenses();
            DGVDetainedLicenses.DataSource = dt;
        }
    }
}

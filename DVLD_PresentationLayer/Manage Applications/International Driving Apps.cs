using DVLD_BusinessLayer;
using DVLD_Project.Manage_Licenses;
using DVLD_Project.ManageTests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Applications
{
    public partial class InternationalDrivingApps : Form
    {
        private int _PersonID;
        public InternationalDrivingApps(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            _RefreshApps();
        }

        public void _RefreshApps()
        {
            DataTable dt = clsApplication.GetInternationalApplications();
            DGVApps.DataSource = dt;

            if (cmboxFilterRows.Items.Count == 0)
            {
                cmboxFilterRows.Items.Add("None");
                foreach (DataColumn col in dt.Columns)
                {
                    cmboxFilterRows.Items.Add(col.ColumnName);
                }
            }
            cmboxFilterRows.SelectedIndex = 0;
        }
        private void cmboxFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmboxFilterRows.SelectedItem.ToString() == "None")
            {
                DGVApps.DataSource = clsApplication.GetInternationalApplications();
                txtSearch.Visible = false;
                DTAppDate.Visible = false;
                btnDateSearch.Visible = false;
            }
            else if (cmboxFilterRows.SelectedItem.ToString() == "ApplicationDate"|| cmboxFilterRows.SelectedItem.ToString() == "LastStatusDate")
            {
                txtSearch.Visible = false;
                DTAppDate.Visible = true;
                btnDateSearch.Visible = true;
            }
            else
            {
                txtSearch.Visible = true;
                DTAppDate.Visible = false;
                btnDateSearch.Visible = false;
            }

        }

        private void btnDateSearch_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DTAppDate.Value;

            DataTable dt = clsApplication.GetInternationalApplications();
            DataView dv = dt.DefaultView;
            if (cmboxFilterRows.SelectedItem.ToString() == "ApplicationDate")
            {
                string formattedDate = selectedDate.ToString("M/d/yyyy");

                dv.RowFilter = $"CONVERT({"ApplicationDate"}, 'System.String') LIKE '%{formattedDate}%'";

                DGVApps.DataSource = dv.ToTable();
            }
            else
            {
                string formattedDate = selectedDate.ToString("M/d/yyyy");

                dv.RowFilter = $"CONVERT({"LastStatusDate"}, 'System.String') LIKE '%{formattedDate}%'";

                DGVApps.DataSource = dv.ToTable();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string selectedColumn = cmboxFilterRows.SelectedItem.ToString();
            string filterValue = txtSearch.Text;

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
            {
                DataTable dt = clsApplication.GetInternationalApplications();
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

                DGVApps.DataSource = dv.ToTable();
            }
            else
            {
                DGVApps.DataSource = clsApplication.GetInternationalApplications();
            }
        }

        private void picAddApp_Click(object sender, EventArgs e)
        {
            IssueInternationalLicense frmIssueInternationalLicense = new IssueInternationalLicense(_PersonID);
            frmIssueInternationalLicense.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseId = clsApplication.GetInternationalLicenseID((int)DGVApps.CurrentRow.Cells[0].Value);
            ShowInternationalLicense frmShowLicense = new ShowInternationalLicense(licenseId);
            frmShowLicense.ShowDialog();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByNationalNo((string)DGVApps.CurrentRow.Cells[2].Value);
            LicensesHistory frmLicensesHistory = new LicensesHistory(person.PersonID);
            frmLicensesHistory.ShowDialog();
        }
    }
}

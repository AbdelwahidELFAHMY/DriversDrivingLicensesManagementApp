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
    public partial class LocalDrivingApps : Form
    {
        private int _PersonID;
        public LocalDrivingApps(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        public void _RefreshApps()
        {
            DataTable dt = clsApplication.GetLocalApplications();
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
                DGVApps.DataSource = clsApplication.GetLocalApplications();
                txtSearch.Visible = false;
                DTAppDate.Visible = false;
                btnDateSearch.Visible = false;
            }
            else if (cmboxFilterRows.SelectedItem.ToString() == "ApplicationDate")
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string selectedColumn = cmboxFilterRows.SelectedItem.ToString();
            string filterValue = txtSearch.Text;

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
            {
                DataTable dt = clsApplication.GetLocalApplications();
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
                DGVApps.DataSource = clsApplication.GetLocalApplications();
            }
        }

        private void btnDateSearch_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DTAppDate.Value;

            DataTable dt = clsApplication.GetLocalApplications();
            DataView dv = dt.DefaultView;

            string formattedDate = selectedDate.ToString("M/d/yyyy");

            dv.RowFilter = $"CONVERT({"ApplicationDate"}, 'System.String') LIKE '%{formattedDate}%'";

            DGVApps.DataSource = dv.ToTable();

        }

        private void LocalDrivingApps_Load(object sender, EventArgs e)
        {
            _RefreshApps();
        }

        private void picAddApp_Click(object sender, EventArgs e)
        {
            NewLicenseApplication newApp = new NewLicenseApplication(_PersonID);
            newApp.ShowDialog();
            _RefreshApps();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationDetails AppDetails = new ShowApplicationDetails((int)DGVApps.CurrentRow.Cells[0].Value);
            AppDetails.ShowDialog();
            _RefreshApps();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLocalLicenseApplicaton editApp = new EditLocalLicenseApplicaton(this._PersonID,(int)DGVApps.CurrentRow.Cells[0].Value);
            editApp.ShowDialog();
            _RefreshApps();

        }

        private void DGVApps_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = DGVApps.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                   
                    DGVApps.ClearSelection();
                    DGVApps.Rows[hit.RowIndex].Selected = true;

                    DataGridViewRow selectedRow = DGVApps.Rows[hit.RowIndex];

                    int PassedTests = (int)selectedRow.Cells["PassedTests"].Value;
                    string Status = (string)selectedRow.Cells["Status"].Value;
                    int LDAppID = (int)selectedRow.Cells["LDLAppID"].Value;

                    if (Status == "Canceled")
                    {
                        contextMenuStrip1.Items["cancelToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["editApplicationToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["schedulerTestsToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["showLicenseToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["deleteApplicationToolStripMenuItem"].Enabled = true;
                    }
                    else if (Status == "Completed")
                    {
                        contextMenuStrip1.Items["cancelToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["editApplicationToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["deleteApplicationToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["schedulerTestsToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["showLicenseToolStripMenuItem"].Enabled = true;
                    }
                    else if(Status == "New")
                    {
                        contextMenuStrip1.Items["cancelToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["editApplicationToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["schedulerTestsToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["deleteApplicationToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["showLicenseToolStripMenuItem"].Enabled = false;
                        ToolStripMenuItem scheduleTestsMenuItem = contextMenuStrip1.Items["schedulerTestsToolStripMenuItem"] as ToolStripMenuItem;

                        if (PassedTests == 0)
                        {
                            scheduleTestsMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = true;
                            scheduleTestsMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled=false;
                            scheduleTestsMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled=false;
                        }else if(PassedTests == 1)
                        {
                            scheduleTestsMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = true;
                            scheduleTestsMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                            scheduleTestsMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                        }else if(PassedTests == 2)
                        {
                            scheduleTestsMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                            scheduleTestsMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                            scheduleTestsMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = true;
                        }
                        else if(PassedTests == 3)
                        {
                            contextMenuStrip1.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = true;
                            contextMenuStrip1.Items["schedulerTestsToolStripMenuItem"].Enabled = false;
                        }
                    }

                    contextMenuStrip1.Show(DGVApps, new Point(e.X, e.Y));
                }
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser currUser = clsUser.GetUserByPersonID(_PersonID);
            IssueLiense frmIssueLicense = new IssueLiense((int)DGVApps.CurrentRow.Cells[0].Value, currUser.UserID);
            frmIssueLicense.ShowDialog();
            _RefreshApps();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to delete This Application", "Confirme Deletion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsApplication.DeleteLocalApplication((int)DGVApps.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Application Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshApps();
                }
                else
                {
                    MessageBox.Show("Application Deletion Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Cancel This Application", "Confirme Cancel", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (clsApplication.CancelLocalApplication((int)DGVApps.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Application Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshApps();
                }
                else
                {
                    MessageBox.Show("Application Cancel Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser currUser = clsUser.GetUserByPersonID(_PersonID);
            TestAppointments frmVisionTestAppointment = new TestAppointments(currUser.UserID,(int)DGVApps.CurrentRow.Cells[0].Value,1);
            frmVisionTestAppointment.ShowDialog();
            _RefreshApps();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser currUser = clsUser.GetUserByPersonID(_PersonID);
            TestAppointments frmWrittenTestAppointment = new TestAppointments(currUser.UserID, (int)DGVApps.CurrentRow.Cells[0].Value,2);
            frmWrittenTestAppointment.ShowDialog();
            _RefreshApps();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser currUser = clsUser.GetUserByPersonID(_PersonID);
            TestAppointments frmStreetTestAppointment = new TestAppointments(currUser.UserID, (int)DGVApps.CurrentRow.Cells[0].Value, 3);
            frmStreetTestAppointment.ShowDialog();
            _RefreshApps();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseId = clsApplication.GetLicenseID((int)DGVApps.CurrentRow.Cells[0].Value);
            ShowLicense frmShowLicense = new ShowLicense(licenseId);
            frmShowLicense.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByNationalNo((string)DGVApps.CurrentRow.Cells[2].Value);
            LicensesHistory frmLicensesHistory = new LicensesHistory(person.PersonID);
            frmLicensesHistory.ShowDialog();
        }
    }
}

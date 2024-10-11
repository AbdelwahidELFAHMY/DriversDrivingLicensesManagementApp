using DVLD_Project.Manage_Application_Types;
using DVLD_Project.Manage_Applications;
using DVLD_Project.Manage_Drivers;
using DVLD_Project.Manage_Licenses;
using DVLD_Project.Manage_Licenses.Manage_Detained_Licences;
using DVLD_Project.Manage_Licenses.ReleaseDetainedLicense;
using DVLD_Project.Manage_Users;
using DVLD_Project.ManageTests.Manage_Test_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD_Project
{
    public partial class HomeForm : Form
    {
        private int _PersonID;
        public HomeForm(int PersonID)
        {
            _PersonID = PersonID;
            InitializeComponent();

            
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            People people = new People();
            people.Activate();
            people.ShowDialog();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Activate();
            users.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(_PersonID);
            changePassword.ShowDialog();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(iconButton5, new Point(0,iconButton5.Height));   
        }

        private void UserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentUserInfo currentUserInfo = new CurrentUserInfo(this._PersonID);
            currentUserInfo.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLicenseApplication NewLicenseApp = new NewLicenseApplication(this._PersonID);
            NewLicenseApp.ShowDialog();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(iconButton1, new Point(0,iconButton1.Height));
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingApps localDrivingApps = new LocalDrivingApps(_PersonID);
            localDrivingApps.ShowDialog();
        }

        private void replacementForLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplacementLicense frmReplacementLicense = new ReplacementLicense(_PersonID);
            frmReplacementLicense.ShowDialog();

        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueInternationalLicense frmIssueInternationalLicense = new IssueInternationalLicense(_PersonID);
            frmIssueInternationalLicense.ShowDialog();
        }

        private void manageApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypes frmAppTypes = new ApplicationTypes();
            frmAppTypes.ShowDialog();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestTypes frm = new TestTypes();
            frm.ShowDialog();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Drivers frm = new Drivers();
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalDrivingApps frm = new InternationalDrivingApps(_PersonID);
            frm.ShowDialog();
        }

        private void manageDetainedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainedLicenses frm = new DetainedLicenses(_PersonID);
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DetainLicense frm = new DetainLicense(_PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainLisenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense(_PersonID);
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLicense frm = new RenewLicense(_PersonID);
            frm.ShowDialog();
        }
    }
}

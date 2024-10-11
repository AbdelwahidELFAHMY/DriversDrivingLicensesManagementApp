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

namespace DVLD_Project.UserControles
{
    public partial class DriverLicensesHistory : UserControl
    {
        public int PersonID = -1;
        public DriverLicensesHistory()
        {
            InitializeComponent();
        }

        private void DriverLicensesHistory_Load(object sender, EventArgs e)
        {
            if (PersonID > -1)
            {
                ctrlPersonDetails1.PersonID = PersonID;
                DGVLocal.DataSource = clsLicense.GetLocalLicenses(PersonID);
                DGVInternational.DataSource = clsLicense.GetInternationalLicenses(PersonID);
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicense frmShowLicense = new ShowLicense((int)DGVLocal.CurrentRow.Cells[0].Value);
            frmShowLicense.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowInternationalLicense frmShow = new ShowInternationalLicense((int)DGVInternational.CurrentRow.Cells[0].Value);
            frmShow.ShowDialog();
        }
    }
}

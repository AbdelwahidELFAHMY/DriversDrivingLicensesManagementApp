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

namespace DVLD_Project.Manage_Licenses
{
    public partial class IssueLiense : Form
    {
        private int _LDLAppID = -1;
        private int _CurrentUser = -1;
        public IssueLiense(int lDLAppID, int currentUser)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;
            ctrlApplicationInfo1._AppID = _LDLAppID;
            _CurrentUser = currentUser;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicenseID = clsLicense.AddLocalLicense(_LDLAppID, _CurrentUser, txtNotes.Text.ToString());
            if(LicenseID != -1)
                MessageBox.Show("Congratulation License Issued Successfuly with LicenseID = " + LicenseID, "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("License Issue Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

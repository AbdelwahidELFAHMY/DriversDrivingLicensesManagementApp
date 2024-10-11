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

namespace DVLD_Project.Manage_Licenses.Manage_Detained_Licences
{
    public partial class DetainLicense : Form
    {
        private int _PersonID = -1;
        private int _LicenseID = -1;
        private int _CreatedByUserID;
        public DetainLicense(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            clsUser currentUser = clsUser.GetUserByPersonID(_PersonID);
            lblCreatedBy.Text = currentUser.UserName;
            _CreatedByUserID = currentUser.UserID;
            lblAppDate.Text = DateTime.Now.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                errorProvider1.SetError(txtSearch, "Please, Enter License ID!");
                timer1.Start();
                return;
            }
            else
            {
                try
                {
                    _LicenseID = Convert.ToInt32(((string)txtSearch.Text).Trim());
                    if (clsLicense.IsLicenseExist(_LicenseID))
                    {
                        lblLicenseID.Text = _LicenseID.ToString();
                        ctrlLicenseInfo1._LicenseID = _LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load(sender, e);
                        if (ctrlLicenseInfo1.isActive)
                            btnDetain.Enabled = true;
                        else
                            btnDetain.Enabled = false;
                   
                    }
                    else
                    {
                        errorProvider1.SetError(txtSearch, "Sorry, This License doesn't exist!");
                        timer1.Start();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                errorProvider1.SetError(txtFees, "Please, Enter Fine Fees");
                timer1.Start();
                return;
            }
            else
            {
                if (clsLicense.IsDetained(_LicenseID))
                {
                    MessageBox.Show("Sorry, This License is Already Detained", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {                    
                    int DetainID = clsLicense.DetainLicense(_LicenseID, (float)Convert.ToDouble(txtFees.Text.ToString()), _CreatedByUserID);
                    if (DetainID != -1)
                    {
                        lblDetainID.Text = DetainID.ToString();
                        MessageBox.Show("The License has Detained Successfuly", "Operation Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Detain License Failed", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ctrlLicenseInfo1.NationalNo))
            {
                clsPerson person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);
                LicensesHistory frmLicenseHistory = new LicensesHistory(person.PersonID);
                frmLicenseHistory.ShowDialog();
            }
        }
    }
}

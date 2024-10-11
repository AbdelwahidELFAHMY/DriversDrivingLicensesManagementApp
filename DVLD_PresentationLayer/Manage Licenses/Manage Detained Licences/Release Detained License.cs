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

namespace DVLD_Project.Manage_Licenses.ReleaseDetainedLicense
{
    public partial class ReleaseDetainedLicense : Form
    {
        private int _PersonID = -1;
        private int _LicenseID = -1;
        private int _CreatedByUserID;
        float Fees;
        private clsPerson person = null;
        public ReleaseDetainedLicense(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            clsUser currentUser = clsUser.GetUserByPersonID(_PersonID);
            lblCreatedBy.Text = currentUser.UserName;
            _CreatedByUserID = currentUser.UserID;
            Fees = clsApplication.GetApplicationTypesFees(9);
            lblAppFees.Text = Fees.ToString();
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
                        person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);

                        if (clsLicense.IsDetained(_LicenseID))
                        {
                            btnRelease.Enabled = true;
                            DataRow dr = clsLicense.GetDetainInfo(_LicenseID);
                            lblDetainID.Text = dr["DetainID"].ToString();
                            lblLicenseID.Text = dr["LicenseID"].ToString();
                            lblDetainDate.Text = dr["DetainDate"].ToString();
                            lblFineFees.Text = dr["FineFees"].ToString();
                            lblTotalFees.Text = (Convert.ToDouble( dr["FineFees"].ToString())+Fees).ToString();
                        }
                        else
                            btnRelease.Enabled = false;

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

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (person!=null)
            {
                LicensesHistory frmLicenseHistory = new LicensesHistory(person.PersonID);
                frmLicenseHistory.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtSearch, "");
            timer1.Stop();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            int ReleaseApplicationID = clsLicense.ReleaseDetainedLicense(_LicenseID, _CreatedByUserID, person.PersonID, (float)Convert.ToDouble(lblTotalFees.Text.ToString()));
            if(ReleaseApplicationID != -1)
            {
                lblApplicationID.Text = ReleaseApplicationID.ToString();
                MessageBox.Show("Release License Done with Succes","Succeeded",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Release License Failed", "Fealure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

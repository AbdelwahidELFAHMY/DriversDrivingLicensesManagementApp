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
    public partial class IssueInternationalLicense : Form
    {
        private int _PersonID = -1;
        private int _LicenseID = -1;
        private int _CreatedByUserID;
        private int[] InsertedApp_License_ID;
        public IssueInternationalLicense(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            clsUser currentUser = clsUser.GetUserByPersonID(_PersonID);
            lblCreatedBy.Text = currentUser.UserName;
            _CreatedByUserID = currentUser.UserID;
            lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString();
            lblAppFees.Text = clsApplication.GetApplicationTypesFees(6).ToString();
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
                        if (clsLicense.getLicenseClass(_LicenseID) == 3)
                        {
                            lblLocalLID.Text = _LicenseID.ToString();
                            ctrlLicenseInfo1._LicenseID = _LicenseID;
                            ctrlLicenseInfo1.ctrlLicenseInfo_Load(sender, e);
                            if (ctrlLicenseInfo1.isActive)
                                btnIssue.Enabled = true;
                            else
                                btnIssue.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Sorry, You Can Take an International License For Class3_Ordinary License Only", "Class Not Permited", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            btnIssue.Enabled = false;
                        }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
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

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure you want to take an International license for this License!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!clsLicense.IsInternationalLicenseExist(_LicenseID))
                {
                    clsPerson person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);
                    InsertedApp_License_ID = clsLicense.IssueInternationalLicense(_LicenseID, 6,
                                                     person.PersonID, clsApplication.GetApplicationTypesFees(6), _CreatedByUserID);

                    if (InsertedApp_License_ID[0] == -1 || InsertedApp_License_ID[1] == -1)
                    {
                        MessageBox.Show("Somthing Went Wrong While Processing Issue international License!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        lblAppID.Text = InsertedApp_License_ID[0].ToString();
                        lblInternationalLID.Text = InsertedApp_License_ID[1].ToString();
                        llShowNewLicense.Enabled = true;
                        MessageBox.Show("Congratulation! Your International License Issued Successfuly With ID " + InsertedApp_License_ID[1], "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnIssue.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Sorry!!, You have Already an Active International License", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInternationalLicense frmShowLicense = new ShowInternationalLicense(InsertedApp_License_ID[2]);
            frmShowLicense.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtSearch, "");
            timer1.Stop();
        }
    }
}

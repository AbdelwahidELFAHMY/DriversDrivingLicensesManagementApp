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
    public partial class ReplacementLicense : Form
    {
        private int _PersonID = -1;
        private int _ReplacementTypeID = 4;
        private int _CreatedByUserID;
        private int[] InsertedApp_License_ID;
        public ReplacementLicense(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            clsUser currentUser = clsUser.GetUserByPersonID(_PersonID);
            lblCreatedBy.Text = currentUser.UserName;
            _CreatedByUserID = currentUser.UserID;
            lblAppDate.Text = DateTime.Now.ToString();
            lblAppFees.Text = clsApplication.GetApplicationTypesFees(4).ToString();
            
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            if(!string.IsNullOrEmpty(ctrlLicenseInfo1.NationalNo))
            {
                clsPerson person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);
                LicensesHistory frmLicenseHistory = new LicensesHistory(person.PersonID);
                frmLicenseHistory.ShowDialog();
            }
        }

        private void rdDamaged_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDamaged.Checked)
            {
                this.Text = "Replacement For Damaged";
                lblAppFees.Text = clsApplication.GetApplicationTypesFees(4).ToString();
                lblTitle.Text = "Replacement For Damaged";
                _ReplacementTypeID = 4;
            }
            else { 
                this.Text = "Replacement For Lost";
                lblAppFees.Text = clsApplication.GetApplicationTypesFees(3).ToString();
                lblTitle.Text = "Replacement For Lost";
                _ReplacementTypeID = 3;
            }
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
                    if (clsLicense.IsLicenseExist(Convert.ToInt32(((string)txtSearch.Text).Trim())))
                    {
                        lblOldLID.Text = txtSearch.Text;
                        ctrlLicenseInfo1._LicenseID = Convert.ToInt32(txtSearch.Text);
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load(sender, e);
                        if(ctrlLicenseInfo1.isActive)
                            btnIssue.Enabled = true;
                        else
                            btnIssue.Enabled = false;

                    }
                    else
                    {
                        errorProvider1.SetError(txtSearch, "Sorry, This License doesn't exist!");
                        timer1.Start();
                        return;
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure you want to replace this license!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsPerson person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);
                InsertedApp_License_ID = clsLicense.ReplaceLicense(ctrlLicenseInfo1._LicenseID, _ReplacementTypeID,
                                                 person.PersonID, clsApplication.GetApplicationTypesFees(_ReplacementTypeID), _CreatedByUserID);

                if (InsertedApp_License_ID[0] == -1 || InsertedApp_License_ID[1] == -1)
                {
                    MessageBox.Show("Somthing Went Wrong While Processing The Replacement Of License!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    lblAppID.Text = InsertedApp_License_ID[0].ToString();
                    lblReplacedLID.Text = InsertedApp_License_ID[1].ToString();
                    llShowNewLicense.Enabled = true;
                    MessageBox.Show("License Replaced Successfuly With ID " + InsertedApp_License_ID[1], "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnIssue.Enabled = false;
                }
            }
            
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense frmShowLicense = new ShowLicense(InsertedApp_License_ID[1]);
            frmShowLicense.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void ctrlLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

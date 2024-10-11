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
    public partial class RenewLicense : Form
    {
        private int _PersonID = -1;
        private int _LicenseID = -1;
        private int _ReplacementTypeID = 4;
        private int _CreatedByUserID;
        private int[] InsertedApp_License_ID;
        private float Fees;
        private clsPerson person = null;
        public RenewLicense(int PersonID)
        {
            InitializeComponent();

            this._PersonID = PersonID;
            clsUser currentUser = clsUser.GetUserByPersonID(_PersonID);
            lblCreatedBy.Text = currentUser.UserName;
            _CreatedByUserID = currentUser.UserID;
            lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblAppFees.Text = clsApplication.GetApplicationTypesFees(4).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtSearch, "");
            timer1.Stop();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense frmShowLicense = new ShowLicense(InsertedApp_License_ID[1]);
            frmShowLicense.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (person!=null)
            {
                LicensesHistory frmLicenseHistory = new LicensesHistory(person.PersonID);
                frmLicenseHistory.ShowDialog();
            }
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
                        lblOldLID.Text = _LicenseID.ToString();
                        ctrlLicenseInfo1._LicenseID = _LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load(sender, e);
                        person = clsPerson.FindPersonByNationalNo(ctrlLicenseInfo1.NationalNo);
                        DateTime ExpirationDate = clsLicense.GetExpirationDate(_LicenseID);
                        float LicenseFees = clsLicense.GetLicenseClassFees(_LicenseID);
                        string notes = clsLicense.GetLicenseNotes(_LicenseID);
                        if (string.IsNullOrEmpty(notes))
                            txtNotes.Text = "No Notes"; 
                        else txtNotes.Text = notes;
                        lblLicenseFees.Text= LicenseFees.ToString();
                        Fees = LicenseFees+(float)Convert.ToDouble(lblAppFees.Text); 
                        lblTotalFees.Text= Fees.ToString();
                        lblExpirationDate.Text = ExpirationDate.ToString();
                        if(ExpirationDate > DateTime.Now)
                        {
                            btnRenew.Enabled = false;
                            MessageBox.Show("This License is Not yet Expired\nIt Will be Expired On: " + ExpirationDate.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                        {
                            btnRenew.Enabled=true;
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

        private void btnRenew_Click(object sender, EventArgs e)
        {
            InsertedApp_License_ID = clsLicense.RenewLicense(_LicenseID,person.PersonID,Fees,_CreatedByUserID);

            if (InsertedApp_License_ID[0] == -1 || InsertedApp_License_ID[1] == -1)
            {
                MessageBox.Show("Somthing Went Wrong While Processing The Renew Of License!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                lblAppID.Text = InsertedApp_License_ID[0].ToString();
                lblRenewedLID.Text = InsertedApp_License_ID[1].ToString();
                llShowNewLicense.Enabled = true;
                MessageBox.Show("License Renewed Successfuly With ID " + InsertedApp_License_ID[1], "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRenew.Enabled = false;
            }
        }
    }
}

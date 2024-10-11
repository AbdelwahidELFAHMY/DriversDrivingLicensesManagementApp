using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.UserControles
{
    public partial class International_License_Info : UserControl
    {
        public int _InterLicenseID = -1;
        public string NationalNo = "";
        public bool isActive;
        public International_License_Info()
        {
            InitializeComponent();
        }

        public void FillWithLicenseData()
        {
            DataTable dt = new DataTable();
            dt = clsLicense.GetInternationalLicenseInfo(_InterLicenseID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblInterLicenseID.Text = row["InternationalLicenseID"].ToString();
                lblLocalLicenseID.Text = row["LocalLicenseID"].ToString();
                lblName.Text = row["Name"].ToString();
                lblNationalNo.Text = row["NationalNo"].ToString();
                NationalNo = row["NationalNo"].ToString();
                lblIssueDate.Text = row["IssueDate"].ToString();
                lblDateOfBirth.Text = row["DateOfBirth"].ToString();
                lblDriverId.Text = row["DriverID"].ToString();
                lblExpirationDate.Text = row["ExpirationDate"].ToString();
                lblAppID.Text = row["ApplicationID"].ToString() ;

                if (row["Gender"].ToString() == "0")
                {
                    lblGender.Text = "Male";
                    picGender.Image = Properties.Resources.male;
                }
                else
                {
                    lblGender.Text = "Female";
                    picGender.Image = Properties.Resources.female;
                }

                if (row["IsActive"].ToString() == "True")
                {
                    lblIsActive.Text = "Yes";
                    isActive = true;
                }
                else
                {
                    lblIsActive.Text = "No";
                    isActive = false;
                }
                if (!string.IsNullOrEmpty(row["ImagePath"].ToString()) && File.Exists(row["ImagePath"].ToString()))
                {
                    try
                    {
                        picImage.Image = Image.FromFile(row["ImagePath"].ToString());
                    }
                    catch (FileNotFoundException)
                    {
                        picImage.Image = Properties.Resources.defaultProfile;
                    }
                }
                else
                {
                    picImage.Image = Properties.Resources.defaultProfile;
                }

            }

        }

        private void International_License_Info_Load(object sender, EventArgs e)
        {
            FillWithLicenseData();
        }
    }
}

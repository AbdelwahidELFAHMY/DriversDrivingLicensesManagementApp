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
    public partial class ctrlLicenseInfo : UserControl
    {
        public int _LicenseID = -1;
        public string NationalNo = "";
        public bool isActive;
        public ctrlLicenseInfo()
        {
            InitializeComponent();
            
        }

        public void FillWithLicenseData()
        {
            DataTable dt = new DataTable();
            dt = clsLicense.GetLicenseInfo(_LicenseID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblLicenseID.Text = _LicenseID.ToString();
                lblClass.Text= row["Class"].ToString();
                lblName.Text = row["Name"].ToString();
                lblNationalNo.Text = row["NationalNo"].ToString();
                NationalNo = row["NationalNo"].ToString();
                lblIssueDate.Text = row["IssueDate"].ToString();
                lblDateOfBirth.Text = row["DateOfBirth"].ToString();
                lblDriverId.Text = row["DriverID"].ToString();
                lblExpirationDate.Text = row["ExpirationDate"].ToString();

                if (row["Gender"].ToString() == "0")
                {
                    lblGender.Text = "Male";
                    picGender.Image = Properties.Resources.male;
                }
                else
                {
                    lblGender.Text = "Female";
                    picGender.Image= Properties.Resources.female;   
                }

                switch (Convert.ToInt16(row["IssueReason"].ToString()))
                {
                    case 1:
                        {
                            lblIssueReason.Text = "First Time";
                            break;
                        }
                    case 2:
                        {
                            lblIssueReason.Text = "Renew";
                            break;
                        }
                    case 3:
                        {
                            lblIssueReason.Text = "Replacement for Damaged";
                            break;
                        }
                    case 4:
                        {
                            lblIssueReason.Text = "Replacement for Lost";
                            break;
                        }
                    default:
                        {
                            lblIssueReason.Text = "?????????";
                            break;
                        }

                }

                if(string.IsNullOrEmpty( row["Notes"].ToString() ))
                    lblNotes.Text = "No Notes";
                else
                    lblNotes.Text = row["Notes"].ToString();

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

        public void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {
            FillWithLicenseData();
        }

    }
}

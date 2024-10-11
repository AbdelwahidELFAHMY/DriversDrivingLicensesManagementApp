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

namespace DVLD_Project.UserControles
{
    public partial class ctrlApplicationInfo : UserControl
    {
        private int _personID =-1;      
        public int _AppID = -1;

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails personDetails = new PersonDetails(_personID);
            personDetails.ShowDialog();
        }

        private void FillWithAppData()
        {
            DataTable dt = new DataTable();
            dt = clsApplication.GetLocalApplicationDetails(_AppID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; 

                lblDLAppID.Text = _AppID.ToString();
                lblApplicant.Text = row["Applicant"].ToString(); 
                lblLicenseClass.Text = row["AppliedForLicense"].ToString();
                lblFees.Text = row["Fees"].ToString();
                lblType.Text = row["Type"].ToString();
                lblID.Text = row["ApplicationID"].ToString();

                _personID = int.Parse(row["PersonID"].ToString());
                if (_personID != 0)
                {
                    llPersonInfo.Enabled = true;
                }

                lblCreatedBy.Text = row["CreatedBy"].ToString();

                if (row["Date"] != DBNull.Value) 
                {
                    lblDate.Text = ((DateTime)row["Date"]).ToString("dd/MM/yyyy"); 
                }
                else
                {
                    lblDate.Text = "No Date";
                }

                lblStatus.Text = row["Status"].ToString();

                if (row["StatusDate"] != DBNull.Value) 
                {
                    lblStatusDate.Text = ((DateTime)row["StatusDate"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblStatusDate.Text = "No Status Date";
                }

                lblPassedTest.Text = row["PassedTests"].ToString() + "/3";
            }

        }

        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {
            FillWithAppData();
        }

    }
}

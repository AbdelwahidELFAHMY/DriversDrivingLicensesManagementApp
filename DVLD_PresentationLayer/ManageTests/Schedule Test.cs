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

namespace DVLD_Project.ManageTests
{
    public partial class ScheduleTest : Form
    {
        private int _LDLAppID;
        private int _TestTypeID;
        private int _CurrentUserID;

        public ScheduleTest(int CurrentUserID, int LDLAppID, int TestTypeID,int TrialNumber)
        {
            InitializeComponent();
            this._LDLAppID = LDLAppID;
            _TestTypeID = TestTypeID;
            _CurrentUserID = CurrentUserID;

            DTPAppointmentDate.MinDate = DateTime.Now;
            lblTrialNumber.Text = TrialNumber.ToString();
            lblDLAppID.Text = _LDLAppID.ToString();
            lblFees.Text = clsTestApointments.GetTestTypeFees(_TestTypeID).ToString();

            DataTable dt = new DataTable();
            dt = clsApplication.GetLocalApplicationDetails(_LDLAppID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblName.Text = row["Applicant"].ToString();
                lblLicenseClass.Text = row["AppliedForLicense"].ToString();
            }

                switch (_TestTypeID)
            {
                case 1:
                    {
                        lblTestTitle.Text = "Vision Test";
                        picTest.Image = Properties.Resources.eye_test;
                        break;
                    }
                case 2:
                    {
                        lblTestTitle.Text = "Written Test";
                        picTest.Image = Properties.Resources.test;
                        break;
                    }
                case 3:
                    {
                        lblTestTitle.Text = "Street Test";
                        picTest.Image = Properties.Resources.StreetTest;
                        break;
                    }
                default:
                    {
                        lblTestTitle.Text = "??????????????????";
                        picTest.Enabled = false;
                        break;
                    }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsTestApointments.AddTestsAppointment(_TestTypeID, _LDLAppID, DTPAppointmentDate.Value, float.Parse(lblFees.Text), _CurrentUserID, 0))
                MessageBox.Show("Appointment Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Appointment Save  Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            btnSave.Enabled = false;

        }
    }
}

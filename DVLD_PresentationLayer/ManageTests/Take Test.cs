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
    public partial class TakeTest : Form
    {
        private int _LDLAppID;
        private int _TestTypeID;
        private int _CurrentUserID;
        private int _TestAppointmentID;
        private byte _TestResult;
        private string _Notes;

        public TakeTest(int CurrentUserID, int LDLAppID, int TestTypeID, int TrialNumber, int TestAppointmentID)
        {
            InitializeComponent();
            this._LDLAppID = LDLAppID;
            _TestTypeID = TestTypeID;
            _CurrentUserID = CurrentUserID;
            _TestAppointmentID = TestAppointmentID;

            lblDate.Text =  clsTestApointments.GetTestAppointmentDate(this._TestAppointmentID);
            lblTrialNumber.Text = (TrialNumber - 1).ToString();
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

        private void GetTestResult()
        {
            if (string.IsNullOrEmpty(txtNotes.Text))
                _Notes = string.Empty;
            else
                _Notes = txtNotes.Text.ToString();

            if (rdBtnPass.Checked)
                _TestResult = 1;
            else
                _TestResult = 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure about the result of this Test", "Confirmaion Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                GetTestResult();
                int InsertedTestID = clsTestApointments.AddTest(_TestAppointmentID, _TestResult, _Notes, _CurrentUserID);
                if (InsertedTestID != -1)
                {
                    lblTestID.Text = InsertedTestID.ToString();
                    MessageBox.Show("Test Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Appointment Save  Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

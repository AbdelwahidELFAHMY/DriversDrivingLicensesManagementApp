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

namespace DVLD_Project.Manage_Applications
{
    public partial class EditLocalLicenseApplicaton : Form
    {
        private DataTable _dt = new DataTable();
        private int _PersonId = 0;
        private int _LDLAppID = 0;
        clsUser _currentUser = new clsUser();
        public EditLocalLicenseApplicaton(int PersonId, int LDLAppID)
        {
            InitializeComponent();
            if (cmboxLicenseClasses.Items.Count == 0)
            {
                FillLicenseClasses();
            }
            this._LDLAppID= LDLAppID;
            this._PersonId = PersonId;  
            _currentUser = clsUser.GetUserByPersonID(_PersonId);
            DataTable CurrentApp = clsApplication.GetLocalApplicationDetails(_LDLAppID);      
            lblAppCreator.Text = _currentUser.UserName.ToString();   
            lblAppDate.Text = DateTime.Now.ToString();
            DataTable dt = new DataTable();
            dt = clsApplication.GetLocalApplicationDetails(_LDLAppID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblAppID.Text = this._LDLAppID.ToString();
                lblApplicant.Text = row["Applicant"].ToString();
                lblAppFees.Text = row["Fees"].ToString();

                cmboxLicenseClasses.SelectedIndex = cmboxLicenseClasses.FindStringExact(row["AppliedForLicense"].ToString());
            }
        }

        private void FillLicenseClasses()
        {
            cmboxLicenseClasses.Items.Clear();
            _dt = clsLicenseClasses.GetLisenceClasses();
            if (!_dt.HasErrors)
            {
                foreach (DataRow dr in _dt.Rows)
                    cmboxLicenseClasses.Items.Add(dr[0].ToString());
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsApplication.UpdateLocalLicenseApp(_LDLAppID, _currentUser.UserID, cmboxLicenseClasses.SelectedIndex + 1))
                MessageBox.Show("Application updated Successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            else
                MessageBox.Show("Information Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }
}

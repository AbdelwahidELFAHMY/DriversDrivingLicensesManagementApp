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
    public partial class NewLicenseApplication : Form
    {
        private DataTable _dt = new DataTable();
        private int _PersonID = -1;
        private int _UserID = -1; 
        public NewLicenseApplication(int PersonID)
        {
            InitializeComponent();
            if (cmboxLicenseClasses.Items.Count == 0)
            {
                FillLicenseClasses();
            }
            lblAppDate.Text = DateTime.Now.ToString();
            _PersonID = PersonID;
            clsUser user = clsUser.GetUserByPersonID(_PersonID);
            if (user != null)
            {
                lblAppCreator.Text = user.UserName;
                _UserID = user.UserID;  
            }
        }

        private void FillLicenseClasses()
        {
            cmboxLicenseClasses.Items.Clear();
            _dt = clsLicenseClasses.GetLisenceClasses();
            if (!_dt.HasErrors){
                foreach (DataRow dr in _dt.Rows)
                   cmboxLicenseClasses.Items.Add(dr[0].ToString());

                cmboxLicenseClasses.SelectedIndex = 2;
            }
            float AppTypeFees = clsApplication.GetApplicationTypesFees(1);
            if (AppTypeFees > -1)
                lblAppFees.Text = AppTypeFees+"";
            else lblAppFees.Text = "???";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Next_Click(object sender, EventArgs e)
        {
            tabNewLicense.SelectedIndex = 1;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tabNewLicense.SelectedIndex = 0;
        }

        private void tabNewLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabNewLicense.SelectedIndex == 0)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
        }

        private void Next_Click_1(object sender, EventArgs e)
        {
            tabNewLicense.SelectedIndex = 1;
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            tabNewLicense.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int _PersonId = ctrlUserInfo1._PersonID;
            if (_PersonId == -1 || _UserID == -1)
            {
                MessageBox.Show("Please, Select Person want to Demmande A License Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabNewLicense.SelectedIndex = 0;
                return;
            }else if (clsApplication.IsPersonHasAlreadyAPP(_PersonId, 1, (cmboxLicenseClasses.SelectedIndex + 1)))
            {
                MessageBox.Show("Sorry, This Person Has Already a License Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                int InsertedAppID = clsApplication.AddNewLocalApp(_PersonId, _UserID, float.Parse(lblAppFees.Text), (cmboxLicenseClasses.SelectedIndex+1));
                if(InsertedAppID == -1)
                {
                    MessageBox.Show("Somthing Went Wrong With Adding Application.\nPlease try Again later Or Call your Admin|", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    lblAppID.Text = InsertedAppID.ToString();
                    MessageBox.Show("Application Saved Successfuly","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);  
                }
            }
        }

    }
}

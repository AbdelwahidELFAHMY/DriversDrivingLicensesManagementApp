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

namespace DVLD_Project.Manage_Users
{
    public partial class ChangePassword : Form
    {
        private int _CurrentPersonID = -1;
        public ChangePassword(int currentPersonID)
        {
            InitializeComponent();
            _CurrentPersonID = currentPersonID; 
            loginInformation1._PersonID = _CurrentPersonID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "Please, Enter Your Password");
                timer1.Start();
                return;
            }
            else if(!clsUser.IsPasswordCorrect(txtCurrentPassword.Text.ToString(), _CurrentPersonID))
            {
                errorProvider1.SetError(txtCurrentPassword, "Password Incorrect!");
                timer1.Start();
                return;
            }
            else if(string.IsNullOrEmpty(txtNewPassword.Text))
            {
                errorProvider1.SetError(txtNewPassword, "Please, Enter a New Password");
                timer1.Start();
                return;
            }else if(string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword, "Please, Confirm your new Password");
                timer1.Start();
                return;
            }else if(txtConfirmPassword.Text.ToString() != txtNewPassword.Text.ToString())
            {
                MessageBox.Show("New Password is Not Confirmed Correctly.\n\t Please confirm it!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (clsUser.changePassword(txtNewPassword.Text.ToString(), _CurrentPersonID))
                    MessageBox.Show("Password Changed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Changing Password Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtConfirmPassword, "");
            errorProvider1.SetError(txtCurrentPassword, "");
            errorProvider1.SetError(txtNewPassword, "");

            timer1.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

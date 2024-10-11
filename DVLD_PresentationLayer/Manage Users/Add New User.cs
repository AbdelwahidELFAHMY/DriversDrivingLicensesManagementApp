using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Users
{
    public partial class AddNewUser : Form
    {

        public AddNewUser()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Next_Click(object sender, EventArgs e)
        {
            tabAddUser.SelectedIndex = 1;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tabAddUser.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int _PersonId = ctrlUserInfo1._PersonID;
            if(_PersonId == -1) { 
                MessageBox.Show("Please, Select a Person to promote to User","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                tabAddUser.SelectedIndex = 0;
                return;
            }
            else
            {
                try
                {
                    clsPerson person = clsPerson.FindPersonByID(_PersonId);

                    if(clsUser.IsUserExist(_PersonId))
                    {
                        MessageBox.Show("This Person is Already a User\nPlease select a Person that is not yet a User.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(txtUsername.Text))
                        {
                            errorProvider1.SetError(txtUsername, "Please, Enter a Username");
                            timer1.Start();
                            return;
                        }
                        else if (string.IsNullOrEmpty(txtPassword.Text))
                        {
                            errorProvider1.SetError(txtPassword, "Please, Enter a Password");
                            timer1.Start();
                            return;
                        }
                        else if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                        {
                            errorProvider1.SetError(txtConfirmPassword, "Please, Confirm your Password");
                            timer1.Start();
                            return;
                        }
                        else if (txtConfirmPassword.Text.ToString() != txtPassword.Text.ToString())
                        {
                            MessageBox.Show("The Password and confirmPassword are not the Same", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        clsUser NewUser = new clsUser();
                        NewUser.UserName = txtUsername.Text;
                        NewUser.PersonID = _PersonId;
                        NewUser.Password = txtPassword.Text;
                        if (chekIsActive.Checked)
                            NewUser.IsActive = 1;
                        else
                            NewUser.IsActive = 0;

                        if (NewUser.SaveUser())
                        {
                            lblUserID.Text = NewUser.UserID.ToString();
                            MessageBox.Show("User Saved Succefully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("User Addition Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Person ID is Not Valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void tabAddUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabAddUser.SelectedIndex == 0)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
        }
    }
}

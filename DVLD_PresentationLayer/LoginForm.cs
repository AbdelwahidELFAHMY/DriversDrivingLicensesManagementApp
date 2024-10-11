using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD_Project
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                txtUsername.Text = Properties.Settings.Default.Username;
                chkRememberMe.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.Username = txtUsername.Text;
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Sorry, Username is Required");
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Sorry, Pessword is Required");
                txtPassword.Focus();
                return;
            }

            String Response = clsAuth.Login(txtUsername.Text.ToString(), txtPassword.Text.ToString());
            if (Response == "Username ou Password Incorrect" || Response == "An error occurred while processing your request.")
            {
                MessageBox.Show(Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Hide();
                HomeForm homeForm = new HomeForm(int.Parse(Response));
                homeForm.ShowDialog();

            }
        }
    }
}

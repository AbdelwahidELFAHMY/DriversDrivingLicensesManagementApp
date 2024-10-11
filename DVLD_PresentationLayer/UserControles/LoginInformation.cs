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
    public partial class LoginInformation : UserControl
    {
        public int _PersonID=-1;
        public LoginInformation()
        {
            InitializeComponent();
        }

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {
            ctrlPersonDetails1.PersonID = _PersonID;

            clsUser user = clsUser.GetUserByPersonID(_PersonID);

            if (user != null)
            {
                lblUserID.Text = user.UserID.ToString();
                lblUserName.Text = user.UserName;
                if (user.IsActive == 1)
                    lblIsActive.Text = "Yes";
                else
                    lblIsActive.Text = "No";
            }
            
        }
    }
}

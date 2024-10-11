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

namespace DVLD_Project.Manage_Application_Types
{
    public partial class EditAppType : Form
    {
        
        public EditAppType(int ApptypeId, string AppTitle, float AppFees)
        {
            InitializeComponent();

            lblID.Text = ApptypeId.ToString();
            txtTitle.Text = AppTitle.ToString();
            txtFees.Text = AppFees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTitle.Text) || string.IsNullOrEmpty(this.txtFees.Text))
            {
                errorProvider1.SetError(txtTitle, "Sorry, This Field is required!");
                errorProvider1.SetError(txtFees, "Sorry, This Field is required!");
                timer1.Start();
                return;
            }
            else
            {
                int AppTypeID = Convert.ToInt16(lblID.Text);
                string AppTypeTitle = Convert.ToString(txtTitle.Text);
                try
                {
                    float AppTypeFees = (float)Convert.ToDouble(txtFees.Text);
                    if (clsApplication.UpdateAppType(AppTypeID, AppTypeTitle, AppTypeFees))
                        MessageBox.Show("Application Type Updated Successsfuly!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Application Type Update Failed!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    return;
                }            
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTitle, "");
            errorProvider1.SetError(txtFees, "");
            timer1.Stop();
        }
    }
}

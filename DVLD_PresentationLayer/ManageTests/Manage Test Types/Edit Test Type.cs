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

namespace DVLD_Project.ManageTests.Manage_Test_Types
{
    public partial class EditTestType : Form
    {
        public EditTestType(int TestTypeID, string Title,string Description, float Fees)
        {
            InitializeComponent();
            lblID.Text = TestTypeID.ToString();
            txtTitle.Text = Title;
            txtDescription.Text = Description;
            txtFees.Text = Fees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtFees, "");
            errorProvider1.SetError(txtDescription, "");
            errorProvider1.SetError(txtTitle, "");

            timer1.Stop();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTitle.Text) || string.IsNullOrEmpty(this.txtFees.Text)|| string.IsNullOrEmpty(this.txtDescription.Text))
            {
                errorProvider1.SetError(txtTitle, "Sorry, This Field is required!");
                errorProvider1.SetError(txtFees, "Sorry, This Field is required!");
                errorProvider1.SetError(txtDescription, "Sorry, This Field is required!");
                timer1.Start();
                return;
            }
            else
            {
                int ID = Convert.ToInt16(lblID.Text);
                string Title = Convert.ToString(txtTitle.Text);
                string Description = Convert.ToString(txtDescription.Text);
                try
                {
                    float Fees = (float)Convert.ToDouble(txtFees.Text);
                    if (clsTestApointments.UpdateTestType(ID,Title,Description,Fees))
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
    }
}

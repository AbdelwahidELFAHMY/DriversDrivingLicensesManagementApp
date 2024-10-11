using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Controls;

namespace DVLD_Project
{
    public partial class AddEditPerson : Form
    {
        public int _PersonID;
        private string _ImagePath = "";

        private clsPerson _Person = new clsPerson();
        public AddEditPerson(int PersonID)
        {
            this._PersonID = PersonID;
            InitializeComponent();
            LoadCountries();

            if(PersonID == -1)
            {
                AddMode();
            }
            else
            {
                UpdateMode();
            }
        }

        private void UpdateMode() {

            _Person = clsPerson.FindPersonByID(_PersonID);


            lblPersonID.Text = this._PersonID.ToString();
            lblTitle.Text = "Update Person";
            cmbCountries.SelectedIndex = _Person.NationalityID-1;

            txtAddress.Text = _Person.Address;
            txtEmail.Text = _Person.Email;
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtThirdName.Text = _Person.ThirdName;
            txtSecondName.Text = _Person.SecondName;
            txtNationalNo.Text = _Person.CIN.ToString();
            txtPhone.Text = _Person.Phone;
            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
            {
                try
                {
                    pboxImage.Image = System.Drawing.Image.FromFile(_Person.ImagePath);

                    llRemoveImage.Visible = true;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("Image introuvable : " + ex.Message);
                    pboxImage.Image = Properties.Resources.defaultProfile; 
                    llRemoveImage.Visible = false;
                }
            }
            else
            {
                llRemoveImage.Visible = false;
                pboxImage.Image = Properties.Resources.defaultProfile; 
            }

            if (_Person.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;
        }

        private void AddMode()
        {
            lblPersonID.Text = "N/A";
            lblTitle.Text = "Add New Person";
            cmbCountries.SelectedIndex = 118;
            llRemoveImage.Visible = false;
        }
        private void LoadCountries()
        {
            DataTable dt = clsAuth.LoadCountries();

            foreach (DataRow dr in dt.Rows)
            {
                cmbCountries.Items.Add(dr[0].ToString());
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   
            if (!CollectPersonData())
                return;

            if(_PersonID == -1)
            {
                _PersonID = _Person.SavePerson();
                if (_PersonID == -1)
                {
                    MessageBox.Show("Error, Adding Person Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else { 
                    MessageBox.Show("Person Saved Successfully","Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    lblPersonID.Text = _PersonID.ToString();
                    this.Close();
                }
            }
            else
            {
                if (_Person.SavePerson() != _PersonID)
                    MessageBox.Show("Error, Updating Person Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Person Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private bool CollectPersonData()
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text)  || string.IsNullOrEmpty(txtFirstName.Text)
                || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtAddress.Text)  || string.IsNullOrEmpty(txtPhone.Text)){
                MessageBox.Show("Sorry, Fill the Fields");
                return false;
            }else
            {
                if(!rdMale.Checked && !rdFemale.Checked)
                {
                    MessageBox.Show("Sorry, Choose Gender");
                    return false;
                }

                _Person.CIN = txtNationalNo.Text;
                _Person.FirstName = txtFirstName.Text;
                _Person.LastName = txtLastName.Text;
                _Person.Email = txtEmail.Text;
                _Person.Address = txtAddress.Text;
                _Person.Phone = txtPhone.Text;
                _Person.SecondName = txtSecondName.Text;
                _Person.ThirdName = txtThirdName.Text;
                _Person.DateOfBirth = DTPDateOfBirth.Value;
                if (rdMale.Checked)
                    _Person.Gendor = 0;
                else
                    _Person.Gendor = 1;
                _Person.ImagePath = _ImagePath;
                _Person.NationalityID = cmbCountries.SelectedIndex+1;

                return true;
            }
        }


        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                       _ImagePath = openFileDialog.FileName;

                       pboxImage.Image = System.Drawing.Image.FromFile(_ImagePath);
                }
            }

        }

        private void llRemoveImage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           _Person.ImagePath = "";
            pboxImage.Image = null;
            llRemoveImage.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNationalNo, "");
            txtNationalNo.Text = string.Empty;
            timer1.Stop();
        }

        private void ValidateNationalNO(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtNationalNo.Text)) && _PersonID == -1)
            {
                clsPerson person = clsPerson.FindPersonByNationalNo(txtNationalNo.Text);

                if(person != null)
                {
                    errorProvider1.SetError(txtNationalNo, "This Person has Already Registred in the System");
                    timer1.Start();            
                    

                }
                else
                {
                    timer1.Stop();
                }

            }
        }

    }
}

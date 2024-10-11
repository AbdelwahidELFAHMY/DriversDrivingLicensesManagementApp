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
using System.IO;

namespace DVLD_Project
{
    public partial class ctrlPersonDetails : UserControl
    {
        private int _personID;

        public int PersonID
        {
            get { return _personID; }
            set
            {
                _personID = value;
                FillWithPersonData(_personID); 
            }
        }

        public ctrlPersonDetails()
        {
            InitializeComponent();
            
        }

        private void FillWithPersonData(int personID)
        {
            clsPerson Person = clsPerson.FindPersonByID(personID);

            if (Person != null)
            {
                lblAddress.Text = Person.Address;
                lblBirthDate.Text = Person.DateOfBirth.ToString("M/dd/yyyy");
                lblEmail.Text = Person.Email;
                lblName.Text = $"{Person.FirstName} {Person.SecondName} {Person.ThirdName} {Person.LastName}";
                lblPersonID.Text = Person.PersonID.ToString();
                lblNationalNo.Text = Person.CIN;
                lblPhone.Text = Person.Phone;

                if (!string.IsNullOrEmpty(Person.ImagePath) && File.Exists(Person.ImagePath))
                {
                    try
                    {
                        picboxImage.Image = Image.FromFile(Person.ImagePath);
                    }
                    catch (FileNotFoundException)
                    {
                        picboxImage.Image = Properties.Resources.defaultProfile;
                    }
                }
                else
                {
                    picboxImage.Image = Properties.Resources.defaultProfile;
                }

                if (Person.Gendor == 0)
                {
                    lblGender.Text = "Male";
                    pictboxGender.Image = Properties.Resources.male;
                }
                else
                {
                    lblGender.Text = "Female";
                    pictboxGender.Image = Properties.Resources.female;
                }

                lblCountry.Text = Person.GetCountryById(Person.NationalityID); 
            }
        }

        private void llEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((this.PersonID > 0))
            {
                AddEditPerson addEditPerson = new AddEditPerson(PersonID);
                addEditPerson.ShowDialog();
                FillWithPersonData(PersonID);
            }
        }

    }
}


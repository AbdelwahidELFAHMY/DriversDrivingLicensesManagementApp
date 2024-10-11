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
    public partial class ctrlUserInfo : UserControl
    {
        private string _SelectedIndex;
        public int _PersonID=-1;    
        public ctrlUserInfo()
        {
            InitializeComponent();
            cmbFilterIndex.SelectedIndex = 0;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ctrlPersonDetails1_Load(sender, e);
        }

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {
            if(_SelectedIndex == "PersonID")
            {
                try
                {
                    clsPerson person = clsPerson.FindPersonByID(int.Parse(txtSearch.Text.ToString()));
                    
                    if(person != null)
                    {
                         ctrlPersonDetails1.PersonID = person.PersonID;
                         _PersonID = person.PersonID;
                    }
                    else
                    {
                        errorProvider1.SetError(txtSearch, "PersonID is Not Valid");
                        timer1.Start();
                    }

                }
                catch
                {
                    errorProvider1.SetError(txtSearch, "PersonID is Not Valid");
                    timer1.Start();
                }
            }else if(_SelectedIndex == "NationalNo")
            {
                clsPerson person = clsPerson.FindPersonByNationalNo(txtSearch.Text.ToString());

                if (person != null)
                {
                        ctrlPersonDetails1.PersonID = person.PersonID;
                        _PersonID = person.PersonID;
                }
                else
                {
                    errorProvider1.SetError(txtSearch, "National No is Not Valid");
                    timer1.Start();
                }
            }
        }

        private void cmbFilterIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedIndex = cmbFilterIndex.SelectedItem as string;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtSearch, "");

            timer1.Stop();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(-1);
            addEditPerson.ShowDialog();
            ctrlPersonDetails1.PersonID = addEditPerson._PersonID;
            _PersonID = addEditPerson._PersonID;
        }
    }
}

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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        public void _RefreshUsers()
        {
            DataTable dt = clsUser.GetUsers();
            DGVUsers.DataSource = dt;

            if (cmbRowsFilter.Items.Count == 0)
            {
                cmbRowsFilter.Items.Add("None");
                foreach (DataColumn col in dt.Columns)
                {
                    cmbRowsFilter.Items.Add(col.ColumnName);
                }
            }
            cmbRowsFilter.SelectedIndex = 0;
        }

        private void cmbRowsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRowsFilter.SelectedItem.ToString() == "None")
            {
                DGVUsers.DataSource = clsPerson.GetPeople();
                txtSearchValue.Visible = false;
                cmbActive.Visible = false;
            }
            else
            {
                txtSearchValue.Visible = true;
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            _RefreshUsers();
        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
            string selectedColumn = cmbRowsFilter.SelectedItem.ToString();
            string filterValue = txtSearchValue.Text;

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
            {
                DataTable dt = clsUser.GetUsers();
                DataView dv = dt.DefaultView;

                if (dt.Columns[selectedColumn].DataType == typeof(int) )
                {
                    dv.RowFilter = $"CONVERT({selectedColumn}, 'System.String') LIKE '%{filterValue}%'";
                }
                else
                {
                    dv.RowFilter = $"{selectedColumn} LIKE '%{filterValue}%'";
                }

                DGVUsers.DataSource = dv.ToTable();
            }
            else
            {
                DGVUsers.DataSource = clsUser.GetUsers();
            }

        }

        private void cmbRowsFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbRowsFilter.SelectedItem.ToString() == "None")
            {
                DGVUsers.DataSource = clsUser.GetUsers();
                txtSearchValue.Visible = false;
                cmbActive.Visible = false;
            }
            else if (cmbRowsFilter.SelectedItem.ToString() == "IsActive")
            {
                txtSearchValue.Visible = false;
                cmbActive.Visible = true;
            }
            else
            {
                txtSearchValue.Visible = true;
                cmbActive.Visible = false;
            }
        }

        private void cmbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedColumn = cmbRowsFilter.SelectedItem?.ToString(); 
            string filterValue = cmbActive.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(filterValue))
            {
                DataTable dt = clsUser.GetUsers();
                DataView dv = dt.DefaultView;

                if (filterValue == "Yes")
                {
                    dv.RowFilter = "[IsActive] = 1"; 
                }
                else if (filterValue == "No")
                {
                    dv.RowFilter = "[IsActive] = 0";  
                }

                DGVUsers.DataSource = dv.ToTable();  
            }
            else
            {
                DGVUsers.DataSource = clsUser.GetUsers();  
            }
        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
            _RefreshUsers();
        }



        //private void iconPictureBox2_Click(object sender, EventArgs e)
        //{
        //    AddEditPerson addEditPerson = new AddEditPerson(-1);
        //    addEditPerson.ShowDialog();
        //    _RefreshPeople();
        //}

        //private void editToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AddEditPerson addEditPerson = new AddEditPerson((int)DGVPeople.CurrentRow.Cells[0].Value);
        //    addEditPerson.ShowDialog();
        //    _RefreshPeople();
        //}

        //private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AddEditPerson addEditPerson = new AddEditPerson(-1);
        //    addEditPerson.ShowDialog();
        //    _RefreshPeople();
        //}

        //private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure you want to delete Person [" + DGVPeople.CurrentRow.Cells[0].Value + "]?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //    {
        //        if (clsPerson.DeletePerson((int)DGVPeople.CurrentRow.Cells[0].Value))
        //        {
        //            MessageBox.Show("Person Deleted Successfully.");
        //            _RefreshPeople();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Person is not deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    PersonDetails personDetails = new PersonDetails((int)DGVPeople.CurrentRow.Cells[0].Value);
        //    personDetails.ShowDialog();
        //    _RefreshPeople();
        //}

        //private void DGVUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void cmbRowsFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        //{

        //}
    }
}

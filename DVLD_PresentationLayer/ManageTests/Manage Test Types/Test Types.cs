using DVLD_BusinessLayer;
using DVLD_Project.Manage_Application_Types;
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
    public partial class TestTypes : Form
    {
        public TestTypes()
        {
            InitializeComponent();
            DataTable dt = clsTestApointments.GetTestTypes();
            DGVTestTypes.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestType frm = new EditTestType((int)DGVTestTypes.CurrentRow.Cells[0].Value,
                                              (string)DGVTestTypes.CurrentRow.Cells[1].Value,
                                              (string)DGVTestTypes.CurrentRow.Cells[2].Value,
                                              (float)Convert.ToDouble(DGVTestTypes.CurrentRow.Cells[3].Value.ToString()));
            frm.ShowDialog();
            DataTable dt = clsTestApointments.GetTestTypes();
            DGVTestTypes.DataSource = dt;
        }
    }
}

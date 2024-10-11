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
    public partial class ApplicationTypes : Form
    {
        public ApplicationTypes()
        {
            InitializeComponent();
            DataTable dt = clsApplication.GetAppTypes();
            DGVAppTypes.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditAppType frm = new EditAppType((int)DGVAppTypes.CurrentRow.Cells[0].Value,
                                              (string)DGVAppTypes.CurrentRow.Cells[1].Value,
                                              (float)Convert.ToDouble(DGVAppTypes.CurrentRow.Cells[2].Value.ToString()));
            frm.ShowDialog();
            DataTable dt = clsApplication.GetAppTypes();
            DGVAppTypes.DataSource = dt;
        }
    }
}

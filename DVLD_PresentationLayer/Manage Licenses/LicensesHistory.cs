using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Licenses
{
    public partial class LicensesHistory : Form
    {
        private int _PersonID;
        public LicensesHistory(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            driverLicensesHistory1.PersonID = _PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

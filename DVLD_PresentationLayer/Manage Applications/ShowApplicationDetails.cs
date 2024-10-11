using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Applications
{
    public partial class ShowApplicationDetails : Form
    {
        private int _LocalAppID;
        public ShowApplicationDetails(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            ctrlApplicationInfo2._AppID = _LocalAppID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlApplicationInfo2_Load(object sender, EventArgs e)
        {

        }
    }
}

﻿using System;
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
    public partial class ShowLicense : Form
    {
        private int _LicenseID;
        public ShowLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlLicenseInfo1._LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

    }
}

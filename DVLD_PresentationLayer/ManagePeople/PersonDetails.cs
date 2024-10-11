using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class PersonDetails : Form
    {
        public PersonDetails(int personID)
        {
            InitializeComponent();

            ctrlPersonDetails2.PersonID = personID; 
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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

namespace DVLD_Project.ManageTests
{
    public partial class TestAppointments : Form
    {
        private int _LDLAppID;
        private int _TestTypeID;
        private int _CurrentUserID;
        public TestAppointments(int CurrentUserID, int LDLAppID,int TestTypeID)
        {
            InitializeComponent();
            this._LDLAppID = LDLAppID;
            this._TestTypeID = TestTypeID;
            this._CurrentUserID = CurrentUserID;
            ctrlApplicationInfo1._AppID = _LDLAppID;
            switch(_TestTypeID)
            {
                case 1:
                    {
                        lblAppointmentTitle.Text = "Vision Test Appointment";
                        picTest.Image = Properties.Resources.eye_test;
                        break;
                    }
                case 2: 
                    {
                        lblAppointmentTitle.Text = "Written Test Appointment";
                        picTest.Image = Properties.Resources.test;
                        break;
                    }
                case 3:
                    {
                        lblAppointmentTitle.Text = "Street Test Appointment";
                        picTest.Image = Properties.Resources.StreetTest;
                        break;
                    }
               default:
                    {
                        lblAppointmentTitle.Text = "??????????????????";
                        picTest.Enabled = false;
                        break;
                    }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshAppoitments()
        {
            DataTable dt = clsTestApointments.GetTestAppointments(_LDLAppID,_TestTypeID);
            DGVAppointment.DataSource = dt;
            int RowsCount = dt.Rows.Count;
            lblRecords.Text = RowsCount.ToString();
            if (RowsCount > 0)
            {
                DataGridViewRow selectedRow = DGVAppointment.Rows[ RowsCount - 1 ];
                string isLocked = selectedRow.Cells["IsLocked"].Value.ToString();
                if(isLocked == "False" )
                    pictureBox2.Enabled = false;
            }
        }

        private void TestAppointments_Load(object sender, EventArgs e)
        {
            _RefreshAppoitments();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ScheduleTest frmScheduleTest = new ScheduleTest(_CurrentUserID, _LDLAppID, _TestTypeID,DGVAppointment.RowCount);
            frmScheduleTest.ShowDialog();
            _RefreshAppoitments();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTest frmTakeTest = new TakeTest(_CurrentUserID, _LDLAppID, _TestTypeID, DGVAppointment.RowCount, (int)DGVAppointment.CurrentRow.Cells[0].Value);
            frmTakeTest.ShowDialog();
            _RefreshAppoitments();
        }

        private void DGVAppointment_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = DGVAppointment.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {

                    DGVAppointment.ClearSelection();
                    DGVAppointment.Rows[hit.RowIndex].Selected = true;

                    DataGridViewRow selectedRow = DGVAppointment.Rows[hit.RowIndex];

                    string isLocked = selectedRow.Cells["IsLocked"].Value.ToString();   

                    if (isLocked == "True")
                    {
                        editToolStripMenuItem.Enabled = false;
                        takeTestToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        editToolStripMenuItem.Enabled = true;
                        takeTestToolStripMenuItem.Enabled = true;
                    }

                    contextMenuStrip1.Show(DGVAppointment, new Point(e.X, e.Y));
                }
            }
        }
    }
}

namespace DVLD_Project.Manage_Licenses.Manage_Detained_Licences
{
    partial class DetainedLicenses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DGVDetainedLicenses = new System.Windows.Forms.DataGridView();
            this.btnDateFilter = new System.Windows.Forms.Button();
            this.DTPFilter = new System.Windows.Forms.DateTimePicker();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.cmbRowsFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboxisReleased = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.picDetainLicense = new System.Windows.Forms.PictureBox();
            this.picRelease = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDetainedLicenses)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetainLicense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRelease)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sans Serif Collection", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(405, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 29);
            this.label3.TabIndex = 65;
            this.label3.Text = "Detained Licenses";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Verdana Pro", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.White;
            this.lblRecords.Location = new System.Drawing.Point(90, 546);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 13);
            this.lblRecords.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana Pro", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 546);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "# Records:";
            // 
            // DGVDetainedLicenses
            // 
            this.DGVDetainedLicenses.AllowUserToAddRows = false;
            this.DGVDetainedLicenses.AllowUserToDeleteRows = false;
            this.DGVDetainedLicenses.AllowUserToOrderColumns = true;
            this.DGVDetainedLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVDetainedLicenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGVDetainedLicenses.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGVDetainedLicenses.ColumnHeadersHeight = 32;
            this.DGVDetainedLicenses.Location = new System.Drawing.Point(12, 261);
            this.DGVDetainedLicenses.Name = "DGVDetainedLicenses";
            this.DGVDetainedLicenses.ReadOnly = true;
            this.DGVDetainedLicenses.Size = new System.Drawing.Size(1005, 282);
            this.DGVDetainedLicenses.TabIndex = 55;
            this.DGVDetainedLicenses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DGVDetainedLicenses_MouseClick);
            // 
            // btnDateFilter
            // 
            this.btnDateFilter.Location = new System.Drawing.Point(484, 232);
            this.btnDateFilter.Name = "btnDateFilter";
            this.btnDateFilter.Size = new System.Drawing.Size(75, 23);
            this.btnDateFilter.TabIndex = 60;
            this.btnDateFilter.Text = "Search";
            this.btnDateFilter.UseVisualStyleBackColor = true;
            this.btnDateFilter.Visible = false;
            this.btnDateFilter.Click += new System.EventHandler(this.btnDateFilter_Click);
            // 
            // DTPFilter
            // 
            this.DTPFilter.Location = new System.Drawing.Point(273, 235);
            this.DTPFilter.Name = "DTPFilter";
            this.DTPFilter.Size = new System.Drawing.Size(200, 20);
            this.DTPFilter.TabIndex = 59;
            this.DTPFilter.Visible = false;
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Location = new System.Drawing.Point(274, 235);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(179, 20);
            this.txtSearchValue.TabIndex = 58;
            this.txtSearchValue.Visible = false;
            this.txtSearchValue.TextChanged += new System.EventHandler(this.txtSearchValue_TextChanged);
            // 
            // cmbRowsFilter
            // 
            this.cmbRowsFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRowsFilter.FormattingEnabled = true;
            this.cmbRowsFilter.Location = new System.Drawing.Point(100, 234);
            this.cmbRowsFilter.Name = "cmbRowsFilter";
            this.cmbRowsFilter.Size = new System.Drawing.Size(167, 21);
            this.cmbRowsFilter.TabIndex = 57;
            this.cmbRowsFilter.SelectedIndexChanged += new System.EventHandler(this.cmbRowsFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Myanmar Text", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(14, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 27);
            this.label2.TabIndex = 56;
            this.label2.Text = "Filter By :";
            // 
            // cmboxisReleased
            // 
            this.cmboxisReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxisReleased.FormattingEnabled = true;
            this.cmboxisReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cmboxisReleased.Location = new System.Drawing.Point(273, 235);
            this.cmboxisReleased.Name = "cmboxisReleased";
            this.cmboxisReleased.Size = new System.Drawing.Size(141, 21);
            this.cmboxisReleased.TabIndex = 66;
            this.cmboxisReleased.SelectedIndexChanged += new System.EventHandler(this.cmboxisReleased_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseToolStripMenuItem,
            this.showLicenseToolStripMenuItem1,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 114);
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.file;
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.license;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.showLicenseToolStripMenuItem.Text = "Show License Details";
            this.showLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // showLicenseToolStripMenuItem1
            // 
            this.showLicenseToolStripMenuItem1.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_32;
            this.showLicenseToolStripMenuItem1.Name = "showLicenseToolStripMenuItem1";
            this.showLicenseToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            this.showLicenseToolStripMenuItem1.Text = "Show Person LicenseHistory ";
            this.showLicenseToolStripMenuItem1.Click += new System.EventHandler(this.showLicenseToolStripMenuItem1_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.turn_on;
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.manage_detained_licenses_;
            this.pictureBox1.Location = new System.Drawing.Point(391, -11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 185);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 64;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.X;
            this.btnClose.IconColor = System.Drawing.Color.IndianRed;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 26;
            this.btnClose.Location = new System.Drawing.Point(913, 546);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 38);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picDetainLicense
            // 
            this.picDetainLicense.BackColor = System.Drawing.Color.Transparent;
            this.picDetainLicense.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDetainLicense.Image = global::DVLD_Project.Properties.Resources.Detainlicense;
            this.picDetainLicense.Location = new System.Drawing.Point(936, 207);
            this.picDetainLicense.Name = "picDetainLicense";
            this.picDetainLicense.Size = new System.Drawing.Size(81, 48);
            this.picDetainLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDetainLicense.TabIndex = 67;
            this.picDetainLicense.TabStop = false;
            this.picDetainLicense.Click += new System.EventHandler(this.picDetainLicense_Click);
            // 
            // picRelease
            // 
            this.picRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.picRelease.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picRelease.ForeColor = System.Drawing.Color.YellowGreen;
            this.picRelease.IconChar = FontAwesome.Sharp.IconChar.HandHoldingHand;
            this.picRelease.IconColor = System.Drawing.Color.YellowGreen;
            this.picRelease.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picRelease.IconSize = 48;
            this.picRelease.Location = new System.Drawing.Point(876, 207);
            this.picRelease.Name = "picRelease";
            this.picRelease.Size = new System.Drawing.Size(54, 48);
            this.picRelease.TabIndex = 68;
            this.picRelease.TabStop = false;
            this.picRelease.Click += new System.EventHandler(this.picRelease_Click);
            // 
            // DetainedLicenses
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1029, 590);
            this.Controls.Add(this.picRelease);
            this.Controls.Add(this.picDetainLicense);
            this.Controls.Add(this.cmboxisReleased);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.DGVDetainedLicenses);
            this.Controls.Add(this.btnDateFilter);
            this.Controls.Add(this.DTPFilter);
            this.Controls.Add(this.txtSearchValue);
            this.Controls.Add(this.cmbRowsFilter);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DetainedLicenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List Detained Licenses";
            ((System.ComponentModel.ISupportInitialize)(this.DGVDetainedLicenses)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetainLicense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRelease)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.DataGridView DGVDetainedLicenses;
        private System.Windows.Forms.Button btnDateFilter;
        private System.Windows.Forms.DateTimePicker DTPFilter;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.ComboBox cmbRowsFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboxisReleased;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
        private System.Windows.Forms.PictureBox picDetainLicense;
        private FontAwesome.Sharp.IconPictureBox picRelease;
    }
}
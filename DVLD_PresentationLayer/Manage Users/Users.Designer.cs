namespace DVLD_Project.Manage_Users
{
    partial class Users
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
            this.DGVUsers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRowsFilter = new System.Windows.Forms.ComboBox();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.cmbActive = new System.Windows.Forms.ComboBox();
            this.AddUser = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVUsers
            // 
            this.DGVUsers.AllowUserToAddRows = false;
            this.DGVUsers.AllowUserToDeleteRows = false;
            this.DGVUsers.AllowUserToOrderColumns = true;
            this.DGVUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVUsers.ColumnHeadersHeight = 32;
            this.DGVUsers.Location = new System.Drawing.Point(12, 228);
            this.DGVUsers.Name = "DGVUsers";
            this.DGVUsers.ReadOnly = true;
            this.DGVUsers.Size = new System.Drawing.Size(769, 210);
            this.DGVUsers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(353, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Manage Users";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(12, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter By: ";
            // 
            // cmbRowsFilter
            // 
            this.cmbRowsFilter.FormattingEnabled = true;
            this.cmbRowsFilter.Location = new System.Drawing.Point(95, 192);
            this.cmbRowsFilter.Name = "cmbRowsFilter";
            this.cmbRowsFilter.Size = new System.Drawing.Size(149, 21);
            this.cmbRowsFilter.TabIndex = 4;
            this.cmbRowsFilter.SelectedIndexChanged += new System.EventHandler(this.cmbRowsFilter_SelectedIndexChanged_1);
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Location = new System.Drawing.Point(262, 194);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(200, 20);
            this.txtSearchValue.TabIndex = 5;
            this.txtSearchValue.TextChanged += new System.EventHandler(this.txtSearchValue_TextChanged);
            // 
            // cmbActive
            // 
            this.cmbActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActive.FormattingEnabled = true;
            this.cmbActive.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbActive.Location = new System.Drawing.Point(262, 193);
            this.cmbActive.Name = "cmbActive";
            this.cmbActive.Size = new System.Drawing.Size(149, 21);
            this.cmbActive.TabIndex = 6;
            this.cmbActive.Visible = false;
            this.cmbActive.SelectedIndexChanged += new System.EventHandler(this.cmbActive_SelectedIndexChanged);
            // 
            // AddUser
            // 
            this.AddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.AddUser.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.AddUser.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            this.AddUser.IconColor = System.Drawing.Color.CornflowerBlue;
            this.AddUser.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AddUser.Location = new System.Drawing.Point(736, 176);
            this.AddUser.Name = "AddUser";
            this.AddUser.Size = new System.Drawing.Size(32, 32);
            this.AddUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AddUser.TabIndex = 7;
            this.AddUser.TabStop = false;
            this.AddUser.Click += new System.EventHandler(this.AddUser_Click);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UsersGear;
            this.iconPictureBox1.IconColor = System.Drawing.Color.CornflowerBlue;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 122;
            this.iconPictureBox1.Location = new System.Drawing.Point(357, 2);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(125, 122);
            this.iconPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconPictureBox1.TabIndex = 1;
            this.iconPictureBox1.TabStop = false;
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(793, 450);
            this.Controls.Add(this.AddUser);
            this.Controls.Add(this.cmbActive);
            this.Controls.Add(this.txtSearchValue);
            this.Controls.Add(this.cmbRowsFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.DGVUsers);
            this.Name = "Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Users";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVUsers;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbRowsFilter;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.ComboBox cmbActive;
        private FontAwesome.Sharp.IconPictureBox AddUser;
    }
}
namespace AAEmu.DBEditor.forms
{
    partial class ProgramSettingsForm
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            LabelGamePakPath = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            BtnEditGamePak = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            LabelServerDataDb = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            BtnEditServerDb = new System.Windows.Forms.Button();
            LabelClientDataDb = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            BtnEditClientDb = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            TextBoxGameSchema = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            TextBoxLoginSchema = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            TextBoxPassword = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            TextBoxUsername = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            TextBoxServerIP = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            BtnSave = new System.Windows.Forms.Button();
            BtnRevert = new System.Windows.Forms.Button();
            BtnCancel = new System.Windows.Forms.Button();
            ofdClientPak = new System.Windows.Forms.OpenFileDialog();
            ofdClientDb = new System.Windows.Forms.OpenFileDialog();
            ofdServerDb = new System.Windows.Forms.OpenFileDialog();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(LabelGamePakPath);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(BtnEditGamePak);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(580, 54);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = " Game Client ";
            // 
            // LabelGamePakPath
            // 
            LabelGamePakPath.AutoSize = true;
            LabelGamePakPath.Location = new System.Drawing.Point(143, 22);
            LabelGamePakPath.Name = "LabelGamePakPath";
            LabelGamePakPath.Size = new System.Drawing.Size(22, 15);
            LabelGamePakPath.TabIndex = 2;
            LabelGamePakPath.Text = "???";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 22);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(61, 15);
            label6.TabIndex = 1;
            label6.Text = "game_pak";
            // 
            // BtnEditGamePak
            // 
            BtnEditGamePak.Location = new System.Drawing.Point(100, 18);
            BtnEditGamePak.Name = "BtnEditGamePak";
            BtnEditGamePak.Size = new System.Drawing.Size(37, 23);
            BtnEditGamePak.TabIndex = 0;
            BtnEditGamePak.Text = "...";
            BtnEditGamePak.UseVisualStyleBackColor = true;
            BtnEditGamePak.Click += BtnEditGamePak_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(LabelServerDataDb);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(BtnEditServerDb);
            groupBox2.Controls.Add(LabelClientDataDb);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(BtnEditClientDb);
            groupBox2.Location = new System.Drawing.Point(12, 72);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(580, 83);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = " Game Server (compact) ";
            // 
            // LabelServerDataDb
            // 
            LabelServerDataDb.AutoSize = true;
            LabelServerDataDb.Location = new System.Drawing.Point(143, 55);
            LabelServerDataDb.Name = "LabelServerDataDb";
            LabelServerDataDb.Size = new System.Drawing.Size(22, 15);
            LabelServerDataDb.TabIndex = 8;
            LabelServerDataDb.Text = "???";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(6, 55);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(84, 15);
            label10.TabIndex = 7;
            label10.Text = "Server Data DB";
            // 
            // BtnEditServerDb
            // 
            BtnEditServerDb.Location = new System.Drawing.Point(100, 51);
            BtnEditServerDb.Name = "BtnEditServerDb";
            BtnEditServerDb.Size = new System.Drawing.Size(37, 23);
            BtnEditServerDb.TabIndex = 6;
            BtnEditServerDb.Text = "...";
            BtnEditServerDb.UseVisualStyleBackColor = true;
            BtnEditServerDb.Click += BtnEditServerDb_Click;
            // 
            // LabelClientDataDb
            // 
            LabelClientDataDb.AutoSize = true;
            LabelClientDataDb.Enabled = false;
            LabelClientDataDb.Location = new System.Drawing.Point(143, 26);
            LabelClientDataDb.Name = "LabelClientDataDb";
            LabelClientDataDb.Size = new System.Drawing.Size(22, 15);
            LabelClientDataDb.TabIndex = 5;
            LabelClientDataDb.Text = "???";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Enabled = false;
            label8.Location = new System.Drawing.Point(6, 26);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(83, 15);
            label8.TabIndex = 4;
            label8.Text = "Client Data DB";
            // 
            // BtnEditClientDb
            // 
            BtnEditClientDb.Enabled = false;
            BtnEditClientDb.Location = new System.Drawing.Point(100, 22);
            BtnEditClientDb.Name = "BtnEditClientDb";
            BtnEditClientDb.Size = new System.Drawing.Size(37, 23);
            BtnEditClientDb.TabIndex = 3;
            BtnEditClientDb.Text = "...";
            BtnEditClientDb.UseVisualStyleBackColor = true;
            BtnEditClientDb.Click += BtnEditClientDb_Click;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox3.Controls.Add(TextBoxGameSchema);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(TextBoxLoginSchema);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(TextBoxPassword);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(TextBoxUsername);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(TextBoxServerIP);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new System.Drawing.Point(12, 161);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(580, 173);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = " MySQL server ";
            // 
            // TextBoxGameSchema
            // 
            TextBoxGameSchema.Location = new System.Drawing.Point(248, 90);
            TextBoxGameSchema.Name = "TextBoxGameSchema";
            TextBoxGameSchema.PlaceholderText = "aaemu_game";
            TextBoxGameSchema.Size = new System.Drawing.Size(174, 23);
            TextBoxGameSchema.TabIndex = 20;
            TextBoxGameSchema.TextChanged += OnSettingsChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(237, 72);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(149, 15);
            label5.TabIndex = 19;
            label5.Text = "Game server schema name";
            // 
            // TextBoxLoginSchema
            // 
            TextBoxLoginSchema.Location = new System.Drawing.Point(248, 37);
            TextBoxLoginSchema.Name = "TextBoxLoginSchema";
            TextBoxLoginSchema.PlaceholderText = "aaemu_login";
            TextBoxLoginSchema.Size = new System.Drawing.Size(174, 23);
            TextBoxLoginSchema.TabIndex = 18;
            TextBoxLoginSchema.TextChanged += OnSettingsChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(237, 19);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(148, 15);
            label4.TabIndex = 17;
            label4.Text = "Login server schema name";
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.Location = new System.Drawing.Point(16, 132);
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.PasswordChar = '*';
            TextBoxPassword.Size = new System.Drawing.Size(174, 23);
            TextBoxPassword.TabIndex = 16;
            TextBoxPassword.TextChanged += OnSettingsChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 114);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(57, 15);
            label3.TabIndex = 15;
            label3.Text = "Password";
            // 
            // TextBoxUsername
            // 
            TextBoxUsername.Location = new System.Drawing.Point(16, 90);
            TextBoxUsername.Name = "TextBoxUsername";
            TextBoxUsername.PlaceholderText = "root";
            TextBoxUsername.Size = new System.Drawing.Size(174, 23);
            TextBoxUsername.TabIndex = 14;
            TextBoxUsername.TextChanged += OnSettingsChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(5, 72);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 15);
            label2.TabIndex = 13;
            label2.Text = "Username";
            // 
            // TextBoxServerIP
            // 
            TextBoxServerIP.Location = new System.Drawing.Point(17, 37);
            TextBoxServerIP.Name = "TextBoxServerIP";
            TextBoxServerIP.PlaceholderText = "localhost:3306";
            TextBoxServerIP.Size = new System.Drawing.Size(174, 23);
            TextBoxServerIP.TabIndex = 12;
            TextBoxServerIP.TextChanged += OnSettingsChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 15);
            label1.TabIndex = 11;
            label1.Text = "ServerIP";
            // 
            // BtnSave
            // 
            BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnSave.Location = new System.Drawing.Point(12, 347);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new System.Drawing.Size(95, 22);
            BtnSave.TabIndex = 21;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnRevert
            // 
            BtnRevert.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnRevert.Location = new System.Drawing.Point(113, 347);
            BtnRevert.Name = "BtnRevert";
            BtnRevert.Size = new System.Drawing.Size(95, 22);
            BtnRevert.TabIndex = 22;
            BtnRevert.Text = "Revert";
            BtnRevert.UseVisualStyleBackColor = true;
            BtnRevert.Click += BtnRevert_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BtnCancel.Location = new System.Drawing.Point(497, 347);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new System.Drawing.Size(95, 22);
            BtnCancel.TabIndex = 23;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // ofdClientPak
            // 
            ofdClientPak.FileName = "game_pak";
            ofdClientPak.Filter = "Pak Files|*pak*.*|All Files|*.*";
            ofdClientPak.Title = "Open client game_pak";
            // 
            // ofdClientDb
            // 
            ofdClientDb.DefaultExt = "sqlite3";
            ofdClientDb.FileName = "compact.sqlite3";
            ofdClientDb.Filter = "SQLite Files|*.sqlite*|All files|*.*";
            ofdClientDb.InitialDirectory = "Data";
            ofdClientDb.ReadOnlyChecked = true;
            ofdClientDb.Title = "Open Client DB File";
            // 
            // ofdServerDb
            // 
            ofdServerDb.DefaultExt = "sqlite3";
            ofdServerDb.FileName = "compact.sqlite3";
            ofdServerDb.Filter = "SQLite Files|*.sqlite*|All files|*.*";
            ofdServerDb.InitialDirectory = "Data";
            ofdServerDb.ReadOnlyChecked = true;
            ofdServerDb.Title = "Open Server DB File";
            // 
            // ProgramSettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(604, 381);
            Controls.Add(BtnCancel);
            Controls.Add(BtnRevert);
            Controls.Add(BtnSave);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MinimumSize = new System.Drawing.Size(620, 420);
            Name = "ProgramSettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Program Settings";
            Load += ProgramSettingsForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnEditGamePak;
        private System.Windows.Forms.TextBox TextBoxGameSchema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxLoginSchema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LabelGamePakPath;
        private System.Windows.Forms.Label LabelServerDataDb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BtnEditServerDb;
        private System.Windows.Forms.Label LabelClientDataDb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnEditClientDb;
        private System.Windows.Forms.Button BtnRevert;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.OpenFileDialog ofdClientPak;
        private System.Windows.Forms.OpenFileDialog ofdClientDb;
        private System.Windows.Forms.OpenFileDialog ofdServerDb;
    }
}
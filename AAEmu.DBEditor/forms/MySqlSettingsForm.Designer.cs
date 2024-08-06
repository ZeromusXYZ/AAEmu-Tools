namespace AAEmu.DBEditor.forms
{
    partial class MySqlSettingsForm
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
            label1 = new System.Windows.Forms.Label();
            tbServerIP = new System.Windows.Forms.TextBox();
            tbUsername = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            tbPassword = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            tbLoginSchema = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            tbGameSchema = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            btnSave = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 16);
            label1.TabIndex = 0;
            label1.Text = "MySQL ServerIP";
            // 
            // tbServerIP
            // 
            tbServerIP.Location = new System.Drawing.Point(23, 28);
            tbServerIP.Name = "tbServerIP";
            tbServerIP.PlaceholderText = "localhost:3306";
            tbServerIP.Size = new System.Drawing.Size(174, 23);
            tbServerIP.TabIndex = 1;
            // 
            // tbUsername
            // 
            tbUsername.Location = new System.Drawing.Point(22, 84);
            tbUsername.Name = "tbUsername";
            tbUsername.PlaceholderText = "root";
            tbUsername.Size = new System.Drawing.Size(174, 23);
            tbUsername.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 65);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 16);
            label2.TabIndex = 2;
            label2.Text = "Username";
            // 
            // tbPassword
            // 
            tbPassword.Location = new System.Drawing.Point(22, 129);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new System.Drawing.Size(174, 23);
            tbPassword.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(11, 110);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(57, 16);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // tbLoginSchema
            // 
            tbLoginSchema.Location = new System.Drawing.Point(254, 28);
            tbLoginSchema.Name = "tbLoginSchema";
            tbLoginSchema.PlaceholderText = "aaemu_login";
            tbLoginSchema.Size = new System.Drawing.Size(174, 23);
            tbLoginSchema.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(243, 9);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(146, 16);
            label4.TabIndex = 6;
            label4.Text = "Login server schema name";
            // 
            // tbGameSchema
            // 
            tbGameSchema.Location = new System.Drawing.Point(254, 84);
            tbGameSchema.Name = "tbGameSchema";
            tbGameSchema.PlaceholderText = "aaemu_game";
            tbGameSchema.Size = new System.Drawing.Size(174, 23);
            tbGameSchema.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(243, 65);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(146, 16);
            label5.TabIndex = 8;
            label5.Text = "Game server schema name";
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(333, 129);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(95, 23);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // MySqlSettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(446, 162);
            Controls.Add(btnSave);
            Controls.Add(tbGameSchema);
            Controls.Add(label5);
            Controls.Add(tbLoginSchema);
            Controls.Add(label4);
            Controls.Add(tbPassword);
            Controls.Add(label3);
            Controls.Add(tbUsername);
            Controls.Add(label2);
            Controls.Add(tbServerIP);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MySqlSettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "MySql Settings";
            Load += MySqlSettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLoginSchema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbGameSchema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
    }
}
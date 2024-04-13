namespace AAEmu.DBEditor.forms.server
{
    partial class AccountChangePasswordForm
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
            tNewPassword = new System.Windows.Forms.TextBox();
            cbShowPassword = new System.Windows.Forms.CheckBox();
            btnSave = new System.Windows.Forms.Button();
            btnGenerate = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 16);
            label1.TabIndex = 0;
            label1.Text = "New Password:";
            // 
            // tNewPassword
            // 
            tNewPassword.Location = new System.Drawing.Point(105, 6);
            tNewPassword.Name = "tNewPassword";
            tNewPassword.PasswordChar = '⬤';
            tNewPassword.PlaceholderText = "new password";
            tNewPassword.Size = new System.Drawing.Size(307, 23);
            tNewPassword.TabIndex = 1;
            tNewPassword.TextChanged += tNewPassword_TextChanged;
            // 
            // cbShowPassword
            // 
            cbShowPassword.AutoSize = true;
            cbShowPassword.Location = new System.Drawing.Point(105, 35);
            cbShowPassword.Name = "cbShowPassword";
            cbShowPassword.Size = new System.Drawing.Size(108, 20);
            cbShowPassword.TabIndex = 2;
            cbShowPassword.Text = "Show password";
            cbShowPassword.UseVisualStyleBackColor = true;
            cbShowPassword.CheckedChanged += cbShowPassword_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Location = new System.Drawing.Point(12, 87);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(571, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save and copy to clipboard";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnGenerate.Location = new System.Drawing.Point(418, 6);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(165, 23);
            btnGenerate.TabIndex = 4;
            btnGenerate.Text = "Generate Random";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // AccountChangePasswordForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(595, 122);
            Controls.Add(btnGenerate);
            Controls.Add(btnSave);
            Controls.Add(cbShowPassword);
            Controls.Add(tNewPassword);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "AccountChangePasswordForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change Password";
            Load += AccountChangePasswordForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbShowPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerate;
        internal System.Windows.Forms.TextBox tNewPassword;
    }
}
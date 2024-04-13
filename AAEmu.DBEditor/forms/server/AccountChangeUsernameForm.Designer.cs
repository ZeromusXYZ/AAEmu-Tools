namespace AAEmu.DBEditor.forms.server
{
    partial class AccountChangeUsernameForm
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
            tNewUsername = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 16);
            label1.TabIndex = 0;
            label1.Text = "New Username:";
            // 
            // tNewUsername
            // 
            tNewUsername.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tNewUsername.Location = new System.Drawing.Point(105, 6);
            tNewUsername.MaxLength = 32;
            tNewUsername.Name = "tNewUsername";
            tNewUsername.PlaceholderText = "new username";
            tNewUsername.Size = new System.Drawing.Size(307, 23);
            tNewUsername.TabIndex = 1;
            tNewUsername.TextChanged += tNewPassword_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Location = new System.Drawing.Point(12, 41);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(400, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save and copy to clipboard";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AccountChangeUsernameForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(428, 76);
            Controls.Add(btnSave);
            Controls.Add(tNewUsername);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "AccountChangeUsernameForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change Username";
            Load += AccountChangePasswordForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.TextBox tNewUsername;
    }
}
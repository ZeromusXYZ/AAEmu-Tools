namespace AAEmu.DBEditor.forms.server
{
    partial class AccountsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsForm));
            dgvUsers = new System.Windows.Forms.DataGridView();
            idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lastLoginDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lastIpDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            createdAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            updatedAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            usersBindingSource = new System.Windows.Forms.BindingSource(components);
            lvCharacters = new System.Windows.Forms.ListView();
            ilRaces = new System.Windows.Forms.ImageList(components);
            charactersBindingSource = new System.Windows.Forms.BindingSource(components);
            pbCharacter = new System.Windows.Forms.PictureBox();
            lCharacterName = new System.Windows.Forms.Label();
            lLevel = new System.Windows.Forms.Label();
            lClass = new System.Windows.Forms.Label();
            lMoney = new System.Windows.Forms.Label();
            Menus = new System.Windows.Forms.MenuStrip();
            MenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            MenuAccountNew = new System.Windows.Forms.ToolStripMenuItem();
            AccountS1 = new System.Windows.Forms.ToolStripSeparator();
            AccountUsername = new System.Windows.Forms.ToolStripMenuItem();
            AccountPassword = new System.Windows.Forms.ToolStripMenuItem();
            AccountS2 = new System.Windows.Forms.ToolStripSeparator();
            AccountDelete = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            tUserFilter = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            cbBanReason = new System.Windows.Forms.ComboBox();
            cbBanned = new System.Windows.Forms.CheckBox();
            cbAccessLevel = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tLabor = new System.Windows.Forms.TextBox();
            tLoyalty = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            tCredits = new System.Windows.Forms.TextBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)usersBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)charactersBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCharacter).BeginInit();
            Menus.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { idDataGridViewTextBoxColumn, usernameDataGridViewTextBoxColumn, emailDataGridViewTextBoxColumn, lastLoginDataGridViewTextBoxColumn, lastIpDataGridViewTextBoxColumn, createdAtDataGridViewTextBoxColumn, updatedAtDataGridViewTextBoxColumn });
            dgvUsers.DataSource = usersBindingSource;
            dgvUsers.Location = new System.Drawing.Point(12, 59);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.Size = new System.Drawing.Size(834, 128);
            dgvUsers.TabIndex = 0;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.Frozen = true;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Width = 50;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            usernameDataGridViewTextBoxColumn.DataPropertyName = "Username";
            usernameDataGridViewTextBoxColumn.Frozen = true;
            usernameDataGridViewTextBoxColumn.HeaderText = "Username";
            usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            emailDataGridViewTextBoxColumn.HeaderText = "Email";
            emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastLoginDataGridViewTextBoxColumn
            // 
            lastLoginDataGridViewTextBoxColumn.DataPropertyName = "LastLogin";
            lastLoginDataGridViewTextBoxColumn.HeaderText = "LastLogin";
            lastLoginDataGridViewTextBoxColumn.Name = "lastLoginDataGridViewTextBoxColumn";
            lastLoginDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastIpDataGridViewTextBoxColumn
            // 
            lastIpDataGridViewTextBoxColumn.DataPropertyName = "LastIp";
            lastIpDataGridViewTextBoxColumn.HeaderText = "LastIp";
            lastIpDataGridViewTextBoxColumn.Name = "lastIpDataGridViewTextBoxColumn";
            lastIpDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdAtDataGridViewTextBoxColumn
            // 
            createdAtDataGridViewTextBoxColumn.DataPropertyName = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.HeaderText = "CreatedAt";
            createdAtDataGridViewTextBoxColumn.Name = "createdAtDataGridViewTextBoxColumn";
            createdAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedAtDataGridViewTextBoxColumn
            // 
            updatedAtDataGridViewTextBoxColumn.DataPropertyName = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.HeaderText = "UpdatedAt";
            updatedAtDataGridViewTextBoxColumn.Name = "updatedAtDataGridViewTextBoxColumn";
            updatedAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usersBindingSource
            // 
            usersBindingSource.DataSource = typeof(data.aaemu.login.Users);
            // 
            // lvCharacters
            // 
            lvCharacters.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lvCharacters.LargeImageList = ilRaces;
            lvCharacters.Location = new System.Drawing.Point(6, 22);
            lvCharacters.Name = "lvCharacters";
            lvCharacters.Size = new System.Drawing.Size(250, 185);
            lvCharacters.TabIndex = 1;
            lvCharacters.UseCompatibleStateImageBehavior = false;
            lvCharacters.SelectedIndexChanged += lvCharacters_SelectedIndexChanged;
            // 
            // ilRaces
            // 
            ilRaces.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ilRaces.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilRaces.ImageStream");
            ilRaces.TransparentColor = System.Drawing.Color.Transparent;
            ilRaces.Images.SetKeyName(0, "none.png");
            ilRaces.Images.SetKeyName(1, "nuian-male.png");
            ilRaces.Images.SetKeyName(2, "none.png");
            ilRaces.Images.SetKeyName(3, "none.png");
            ilRaces.Images.SetKeyName(4, "elf-male.png");
            ilRaces.Images.SetKeyName(5, "harani-male.png");
            ilRaces.Images.SetKeyName(6, "firran-male.png");
            ilRaces.Images.SetKeyName(7, "none.png");
            ilRaces.Images.SetKeyName(8, "none.png");
            ilRaces.Images.SetKeyName(9, "none.png");
            ilRaces.Images.SetKeyName(10, "nuian-female.png");
            ilRaces.Images.SetKeyName(11, "none.png");
            ilRaces.Images.SetKeyName(12, "none.png");
            ilRaces.Images.SetKeyName(13, "elf-female.png");
            ilRaces.Images.SetKeyName(14, "harani-female.png");
            ilRaces.Images.SetKeyName(15, "firran-female.png");
            ilRaces.Images.SetKeyName(16, "none.png");
            ilRaces.Images.SetKeyName(17, "none.png");
            // 
            // charactersBindingSource
            // 
            charactersBindingSource.DataSource = typeof(data.aaemu.game.Characters);
            // 
            // pbCharacter
            // 
            pbCharacter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pbCharacter.Location = new System.Drawing.Point(262, 22);
            pbCharacter.Name = "pbCharacter";
            pbCharacter.Size = new System.Drawing.Size(64, 64);
            pbCharacter.TabIndex = 2;
            pbCharacter.TabStop = false;
            // 
            // lCharacterName
            // 
            lCharacterName.AutoSize = true;
            lCharacterName.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold);
            lCharacterName.Location = new System.Drawing.Point(332, 22);
            lCharacterName.Name = "lCharacterName";
            lCharacterName.Size = new System.Drawing.Size(53, 16);
            lCharacterName.TabIndex = 3;
            lCharacterName.Text = "<name>";
            // 
            // lLevel
            // 
            lLevel.AutoSize = true;
            lLevel.Location = new System.Drawing.Point(332, 38);
            lLevel.Name = "lLevel";
            lLevel.Size = new System.Drawing.Size(116, 16);
            lLevel.TabIndex = 4;
            lLevel.Text = "<level/race/gender>";
            // 
            // lClass
            // 
            lClass.AutoSize = true;
            lClass.Location = new System.Drawing.Point(332, 54);
            lClass.Name = "lClass";
            lClass.Size = new System.Drawing.Size(48, 16);
            lClass.TabIndex = 5;
            lClass.Text = "<class>";
            // 
            // lMoney
            // 
            lMoney.AutoSize = true;
            lMoney.Location = new System.Drawing.Point(332, 70);
            lMoney.Name = "lMoney";
            lMoney.Size = new System.Drawing.Size(59, 16);
            lMoney.TabIndex = 6;
            lMoney.Text = "<money>";
            // 
            // Menus
            // 
            Menus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MenuAccount });
            Menus.Location = new System.Drawing.Point(0, 0);
            Menus.Name = "Menus";
            Menus.Size = new System.Drawing.Size(858, 24);
            Menus.TabIndex = 7;
            Menus.Text = "menuStrip1";
            // 
            // MenuAccount
            // 
            MenuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MenuAccountNew, AccountS1, AccountUsername, AccountPassword, AccountS2, AccountDelete });
            MenuAccount.Name = "MenuAccount";
            MenuAccount.Size = new System.Drawing.Size(64, 20);
            MenuAccount.Text = "&Account";
            MenuAccount.DropDownOpening += MenuAccount_DropDownOpening;
            // 
            // MenuAccountNew
            // 
            MenuAccountNew.Name = "MenuAccountNew";
            MenuAccountNew.Size = new System.Drawing.Size(169, 22);
            MenuAccountNew.Text = "&New";
            MenuAccountNew.Click += MenuAccountNew_Click;
            // 
            // AccountS1
            // 
            AccountS1.Name = "AccountS1";
            AccountS1.Size = new System.Drawing.Size(166, 6);
            // 
            // AccountUsername
            // 
            AccountUsername.Name = "AccountUsername";
            AccountUsername.Size = new System.Drawing.Size(169, 22);
            AccountUsername.Text = "Change Username";
            AccountUsername.Click += AccountUsername_Click;
            // 
            // AccountPassword
            // 
            AccountPassword.Name = "AccountPassword";
            AccountPassword.Size = new System.Drawing.Size(169, 22);
            AccountPassword.Text = "Change &Password";
            AccountPassword.Click += AccountPassword_Click;
            // 
            // AccountS2
            // 
            AccountS2.Name = "AccountS2";
            AccountS2.Size = new System.Drawing.Size(166, 6);
            // 
            // AccountDelete
            // 
            AccountDelete.Name = "AccountDelete";
            AccountDelete.Size = new System.Drawing.Size(169, 22);
            AccountDelete.Text = "Delete";
            AccountDelete.Click += AccountDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 33);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(36, 16);
            label1.TabIndex = 8;
            label1.Text = "Filter:";
            // 
            // tUserFilter
            // 
            tUserFilter.Location = new System.Drawing.Point(54, 30);
            tUserFilter.Name = "tUserFilter";
            tUserFilter.PlaceholderText = "account or character name";
            tUserFilter.Size = new System.Drawing.Size(208, 23);
            tUserFilter.TabIndex = 9;
            tUserFilter.TextChanged += tUserFilter_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(cbBanReason);
            groupBox1.Controls.Add(cbBanned);
            groupBox1.Controls.Add(cbAccessLevel);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tLabor);
            groupBox1.Controls.Add(tLoyalty);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tCredits);
            groupBox1.Location = new System.Drawing.Point(12, 193);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(834, 80);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Account Game Details";
            // 
            // cbBanReason
            // 
            cbBanReason.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cbBanReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbBanReason.FormattingEnabled = true;
            cbBanReason.Location = new System.Drawing.Point(83, 48);
            cbBanReason.Name = "cbBanReason";
            cbBanReason.Size = new System.Drawing.Size(745, 24);
            cbBanReason.TabIndex = 10;
            cbBanReason.SelectedIndexChanged += AccountValueChanged;
            // 
            // cbBanned
            // 
            cbBanned.AutoSize = true;
            cbBanned.Location = new System.Drawing.Point(11, 50);
            cbBanned.Name = "cbBanned";
            cbBanned.Size = new System.Drawing.Size(66, 20);
            cbBanned.TabIndex = 9;
            cbBanned.Text = "Banned";
            cbBanned.UseVisualStyleBackColor = true;
            cbBanned.CheckedChanged += AccountValueChanged;
            // 
            // cbAccessLevel
            // 
            cbAccessLevel.FormattingEnabled = true;
            cbAccessLevel.Items.AddRange(new object[] { "0", "50", "100" });
            cbAccessLevel.Location = new System.Drawing.Point(552, 19);
            cbAccessLevel.Name = "cbAccessLevel";
            cbAccessLevel.Size = new System.Drawing.Size(100, 24);
            cbAccessLevel.TabIndex = 7;
            cbAccessLevel.TextChanged += AccountValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(473, 23);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(73, 16);
            label5.TabIndex = 6;
            label5.Text = "Access Level";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 23);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(37, 16);
            label4.TabIndex = 5;
            label4.Text = "Labor";
            // 
            // tLabor
            // 
            tLabor.Location = new System.Drawing.Point(55, 19);
            tLabor.Name = "tLabor";
            tLabor.Size = new System.Drawing.Size(100, 23);
            tLabor.TabIndex = 4;
            tLabor.TextChanged += AccountValueChanged;
            // 
            // tLoyalty
            // 
            tLoyalty.Location = new System.Drawing.Point(367, 20);
            tLoyalty.Name = "tLoyalty";
            tLoyalty.Size = new System.Drawing.Size(100, 23);
            tLoyalty.TabIndex = 3;
            tLoyalty.TextChanged += AccountValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(316, 23);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(45, 16);
            label3.TabIndex = 2;
            label3.Text = "Loyalty";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(161, 23);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(43, 16);
            label2.TabIndex = 1;
            label2.Text = "Credits";
            // 
            // tCredits
            // 
            tCredits.Location = new System.Drawing.Point(210, 20);
            tCredits.Name = "tCredits";
            tCredits.Size = new System.Drawing.Size(100, 23);
            tCredits.TabIndex = 0;
            tCredits.TextChanged += AccountValueChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(pbCharacter);
            groupBox2.Controls.Add(lCharacterName);
            groupBox2.Controls.Add(lLevel);
            groupBox2.Controls.Add(lClass);
            groupBox2.Controls.Add(lvCharacters);
            groupBox2.Controls.Add(lMoney);
            groupBox2.Location = new System.Drawing.Point(12, 279);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(834, 213);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Character Details";
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Location = new System.Drawing.Point(771, 30);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(75, 23);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AccountsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(858, 504);
            Controls.Add(btnSave);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(tUserFilter);
            Controls.Add(label1);
            Controls.Add(dgvUsers);
            Controls.Add(Menus);
            MainMenuStrip = Menus;
            Name = "AccountsForm";
            Text = "Accounts";
            FormClosed += AccountsForm_FormClosed;
            Load += AccountsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)usersBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)charactersBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCharacter).EndInit();
            Menus.ResumeLayout(false);
            Menus.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private System.Windows.Forms.ListView lvCharacters;
        private System.Windows.Forms.ImageList ilRaces;
        private System.Windows.Forms.DataGridViewImageColumn unitModelParamsDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewImageColumn slotsDataGridViewImageColumn;
        private System.Windows.Forms.BindingSource charactersBindingSource;
        private System.Windows.Forms.PictureBox pbCharacter;
        private System.Windows.Forms.Label lCharacterName;
        private System.Windows.Forms.Label lLevel;
        private System.Windows.Forms.Label lClass;
        private System.Windows.Forms.Label lMoney;
        private System.Windows.Forms.MenuStrip Menus;
        private System.Windows.Forms.ToolStripMenuItem MenuAccount;
        private System.Windows.Forms.ToolStripMenuItem MenuAccountNew;
        private System.Windows.Forms.ToolStripSeparator AccountS1;
        private System.Windows.Forms.ToolStripMenuItem AccountPassword;
        private System.Windows.Forms.ToolStripSeparator AccountS2;
        private System.Windows.Forms.ToolStripMenuItem AccountDelete;
        private System.Windows.Forms.ToolStripMenuItem AccountUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tUserFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tCredits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tLabor;
        private System.Windows.Forms.TextBox tLoyalty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbAccessLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastLoginDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastIpDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdAtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedAtDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox cbBanned;
        private System.Windows.Forms.ComboBox cbBanReason;
    }
}
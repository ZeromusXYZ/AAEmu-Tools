namespace AAEmu.DBViewer
{
    partial class MainForm
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
            this.lbTableNames = new System.Windows.Forms.ListBox();
            this.tcViewer = new System.Windows.Forms.TabControl();
            this.tbTables = new System.Windows.Forms.TabPage();
            this.tpItems = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tItemSearch = new System.Windows.Forms.TextBox();
            this.dgvItemSearch = new System.Windows.Forms.DataGridView();
            this.Item_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Name_EN_US = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnItemSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbItemSearchLanguage = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lItemID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lItemName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lItemCategory = new System.Windows.Forms.Label();
            this.tItemDesc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lItemLevel = new System.Windows.Forms.Label();
            this.openDBDlg = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenDB = new System.Windows.Forms.Button();
            this.tcViewer.SuspendLayout();
            this.tbTables.SuspendLayout();
            this.tpItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTableNames
            // 
            this.lbTableNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTableNames.FormattingEnabled = true;
            this.lbTableNames.Location = new System.Drawing.Point(6, 6);
            this.lbTableNames.Name = "lbTableNames";
            this.lbTableNames.Size = new System.Drawing.Size(270, 303);
            this.lbTableNames.TabIndex = 1;
            // 
            // tcViewer
            // 
            this.tcViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcViewer.Controls.Add(this.tbTables);
            this.tcViewer.Controls.Add(this.tpItems);
            this.tcViewer.Location = new System.Drawing.Point(0, 3);
            this.tcViewer.Name = "tcViewer";
            this.tcViewer.SelectedIndex = 0;
            this.tcViewer.Size = new System.Drawing.Size(786, 340);
            this.tcViewer.TabIndex = 3;
            // 
            // tbTables
            // 
            this.tbTables.Controls.Add(this.btnOpenDB);
            this.tbTables.Controls.Add(this.lbTableNames);
            this.tbTables.Controls.Add(this.label2);
            this.tbTables.Controls.Add(this.cbItemSearchLanguage);
            this.tbTables.Location = new System.Drawing.Point(4, 22);
            this.tbTables.Name = "tbTables";
            this.tbTables.Padding = new System.Windows.Forms.Padding(3);
            this.tbTables.Size = new System.Drawing.Size(778, 314);
            this.tbTables.TabIndex = 0;
            this.tbTables.Text = "Tables and Settings";
            this.tbTables.UseVisualStyleBackColor = true;
            // 
            // tpItems
            // 
            this.tpItems.Controls.Add(this.groupBox1);
            this.tpItems.Controls.Add(this.btnItemSearch);
            this.tpItems.Controls.Add(this.dgvItemSearch);
            this.tpItems.Controls.Add(this.label1);
            this.tpItems.Controls.Add(this.tItemSearch);
            this.tpItems.Location = new System.Drawing.Point(4, 22);
            this.tpItems.Name = "tpItems";
            this.tpItems.Padding = new System.Windows.Forms.Padding(3);
            this.tpItems.Size = new System.Drawing.Size(778, 314);
            this.tpItems.TabIndex = 1;
            this.tpItems.Text = "Items";
            this.tpItems.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search Item Name or ID";
            // 
            // tItemSearch
            // 
            this.tItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tItemSearch.Location = new System.Drawing.Point(135, 6);
            this.tItemSearch.Name = "tItemSearch";
            this.tItemSearch.Size = new System.Drawing.Size(244, 20);
            this.tItemSearch.TabIndex = 0;
            this.tItemSearch.TextChanged += new System.EventHandler(this.TItemSearch_TextChanged);
            this.tItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TItemSearch_KeyDown);
            // 
            // dgvItemSearch
            // 
            this.dgvItemSearch.AllowUserToAddRows = false;
            this.dgvItemSearch.AllowUserToDeleteRows = false;
            this.dgvItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItemSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItemSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_ID,
            this.Item_Name_EN_US});
            this.dgvItemSearch.Location = new System.Drawing.Point(11, 33);
            this.dgvItemSearch.Name = "dgvItemSearch";
            this.dgvItemSearch.ReadOnly = true;
            this.dgvItemSearch.RowHeadersVisible = false;
            this.dgvItemSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemSearch.Size = new System.Drawing.Size(453, 275);
            this.dgvItemSearch.TabIndex = 2;
            this.dgvItemSearch.SelectionChanged += new System.EventHandler(this.DgvItemSearch_SelectionChanged);
            // 
            // Item_ID
            // 
            this.Item_ID.HeaderText = "ID";
            this.Item_ID.Name = "Item_ID";
            this.Item_ID.ReadOnly = true;
            this.Item_ID.Width = 43;
            // 
            // Item_Name_EN_US
            // 
            this.Item_Name_EN_US.FillWeight = 200F;
            this.Item_Name_EN_US.HeaderText = "Name";
            this.Item_Name_EN_US.Name = "Item_Name_EN_US";
            this.Item_Name_EN_US.ReadOnly = true;
            this.Item_Name_EN_US.Width = 60;
            // 
            // btnItemSearch
            // 
            this.btnItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemSearch.Enabled = false;
            this.btnItemSearch.Location = new System.Drawing.Point(385, 4);
            this.btnItemSearch.Name = "btnItemSearch";
            this.btnItemSearch.Size = new System.Drawing.Size(79, 23);
            this.btnItemSearch.TabIndex = 3;
            this.btnItemSearch.Text = "Search";
            this.btnItemSearch.UseVisualStyleBackColor = true;
            this.btnItemSearch.Click += new System.EventHandler(this.BtnItemSearch_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(633, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Language";
            // 
            // cbItemSearchLanguage
            // 
            this.cbItemSearchLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbItemSearchLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbItemSearchLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbItemSearchLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemSearchLanguage.FormattingEnabled = true;
            this.cbItemSearchLanguage.Items.AddRange(new object[] {
            "en_us",
            "ru",
            "ko",
            "zh_cn",
            "zh_tw",
            "de",
            "fr"});
            this.cbItemSearchLanguage.Location = new System.Drawing.Point(694, 6);
            this.cbItemSearchLanguage.Name = "cbItemSearchLanguage";
            this.cbItemSearchLanguage.Size = new System.Drawing.Size(75, 21);
            this.cbItemSearchLanguage.TabIndex = 5;
            this.cbItemSearchLanguage.SelectedIndexChanged += new System.EventHandler(this.CbItemSearchLanguage_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lItemLevel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tItemDesc);
            this.groupBox1.Controls.Add(this.lItemCategory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lItemName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lItemID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(470, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 299);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Index";
            // 
            // lItemID
            // 
            this.lItemID.AutoSize = true;
            this.lItemID.Location = new System.Drawing.Point(58, 16);
            this.lItemID.Name = "lItemID";
            this.lItemID.Size = new System.Drawing.Size(13, 13);
            this.lItemID.TabIndex = 1;
            this.lItemID.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name";
            // 
            // lItemName
            // 
            this.lItemName.AutoSize = true;
            this.lItemName.Location = new System.Drawing.Point(58, 38);
            this.lItemName.Name = "lItemName";
            this.lItemName.Size = new System.Drawing.Size(43, 13);
            this.lItemName.TabIndex = 3;
            this.lItemName.Text = "<none>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Category";
            // 
            // lItemCategory
            // 
            this.lItemCategory.AutoSize = true;
            this.lItemCategory.Location = new System.Drawing.Point(58, 61);
            this.lItemCategory.Name = "lItemCategory";
            this.lItemCategory.Size = new System.Drawing.Size(13, 13);
            this.lItemCategory.TabIndex = 5;
            this.lItemCategory.Text = "0";
            // 
            // tItemDesc
            // 
            this.tItemDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tItemDesc.Location = new System.Drawing.Point(9, 99);
            this.tItemDesc.Multiline = true;
            this.tItemDesc.Name = "tItemDesc";
            this.tItemDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tItemDesc.Size = new System.Drawing.Size(287, 194);
            this.tItemDesc.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Level";
            // 
            // lItemLevel
            // 
            this.lItemLevel.AutoSize = true;
            this.lItemLevel.Location = new System.Drawing.Point(58, 83);
            this.lItemLevel.Name = "lItemLevel";
            this.lItemLevel.Size = new System.Drawing.Size(31, 13);
            this.lItemLevel.TabIndex = 8;
            this.lItemLevel.Text = "none";
            // 
            // openDBDlg
            // 
            this.openDBDlg.DefaultExt = "sqlite3";
            this.openDBDlg.FileName = "compact.sqlite3";
            this.openDBDlg.Filter = "SQLite3 files|*.sqlite3|All files|*.*";
            this.openDBDlg.Title = "Open Server DB File";
            // 
            // btnOpenDB
            // 
            this.btnOpenDB.Location = new System.Drawing.Point(282, 6);
            this.btnOpenDB.Name = "btnOpenDB";
            this.btnOpenDB.Size = new System.Drawing.Size(122, 23);
            this.btnOpenDB.TabIndex = 6;
            this.btnOpenDB.Text = "Open DB";
            this.btnOpenDB.UseVisualStyleBackColor = true;
            this.btnOpenDB.Click += new System.EventHandler(this.BtnOpenDB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 345);
            this.Controls.Add(this.tcViewer);
            this.Name = "MainForm";
            this.Text = "AAEmu.DBViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcViewer.ResumeLayout(false);
            this.tbTables.ResumeLayout(false);
            this.tbTables.PerformLayout();
            this.tpItems.ResumeLayout(false);
            this.tpItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbTableNames;
        private System.Windows.Forms.TabControl tcViewer;
        private System.Windows.Forms.TabPage tbTables;
        private System.Windows.Forms.TabPage tpItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tItemSearch;
        private System.Windows.Forms.DataGridView dgvItemSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_Name_EN_US;
        private System.Windows.Forms.Button btnItemSearch;
        private System.Windows.Forms.ComboBox cbItemSearchLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lItemCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lItemID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tItemDesc;
        private System.Windows.Forms.Label lItemLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openDBDlg;
        private System.Windows.Forms.Button btnOpenDB;
    }
}


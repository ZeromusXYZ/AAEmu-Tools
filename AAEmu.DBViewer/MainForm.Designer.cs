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
            this.label8 = new System.Windows.Forms.Label();
            this.btnFindGameClient = new System.Windows.Forms.Button();
            this.btnOpenServerDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbItemSearchLanguage = new System.Windows.Forms.ComboBox();
            this.tpItems = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.itemIcon = new System.Windows.Forms.Label();
            this.rtItemDesc = new System.Windows.Forms.RichTextBox();
            this.btnFindItemInLoot = new System.Windows.Forms.Button();
            this.lItemLevel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lItemCategory = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lItemName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lItemID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnItemSearch = new System.Windows.Forms.Button();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.Item_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Name_EN_US = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tItemSearch = new System.Windows.Forms.TextBox();
            this.tpLoot = new System.Windows.Forms.TabPage();
            this.btnLootSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tLootSearch = new System.Windows.Forms.TextBox();
            this.dgvLoot = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpSkills = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lSkillGCD = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lSkillCooldown = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lSkillLabor = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lSkillMana = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lSkillCost = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.skillIcon = new System.Windows.Forms.Label();
            this.rtSkillDescription = new System.Windows.Forms.RichTextBox();
            this.lSkillName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lSkillID = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSkillSearch = new System.Windows.Forms.Button();
            this.dgvSkills = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.tSkillSearch = new System.Windows.Forms.TextBox();
            this.openDBDlg = new System.Windows.Forms.OpenFileDialog();
            this.tcViewer.SuspendLayout();
            this.tbTables.SuspendLayout();
            this.tpItems.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.tpLoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoot)).BeginInit();
            this.tpSkills.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).BeginInit();
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
            this.tcViewer.Controls.Add(this.tpLoot);
            this.tcViewer.Controls.Add(this.tpSkills);
            this.tcViewer.Location = new System.Drawing.Point(0, 3);
            this.tcViewer.Name = "tcViewer";
            this.tcViewer.SelectedIndex = 0;
            this.tcViewer.Size = new System.Drawing.Size(786, 340);
            this.tcViewer.TabIndex = 3;
            // 
            // tbTables
            // 
            this.tbTables.Controls.Add(this.label8);
            this.tbTables.Controls.Add(this.btnFindGameClient);
            this.tbTables.Controls.Add(this.btnOpenServerDB);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(474, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Only used for loading icons";
            // 
            // btnFindGameClient
            // 
            this.btnFindGameClient.Location = new System.Drawing.Point(282, 35);
            this.btnFindGameClient.Name = "btnFindGameClient";
            this.btnFindGameClient.Size = new System.Drawing.Size(186, 23);
            this.btnFindGameClient.TabIndex = 7;
            this.btnFindGameClient.Text = "Locate Client game_pak";
            this.btnFindGameClient.UseVisualStyleBackColor = true;
            // 
            // btnOpenServerDB
            // 
            this.btnOpenServerDB.Location = new System.Drawing.Point(282, 6);
            this.btnOpenServerDB.Name = "btnOpenServerDB";
            this.btnOpenServerDB.Size = new System.Drawing.Size(186, 23);
            this.btnOpenServerDB.TabIndex = 6;
            this.btnOpenServerDB.Text = "Open (server) DB";
            this.btnOpenServerDB.UseVisualStyleBackColor = true;
            this.btnOpenServerDB.Click += new System.EventHandler(this.BtnOpenServerDB_Click);
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
            "fr",
            "ja"});
            this.cbItemSearchLanguage.Location = new System.Drawing.Point(694, 6);
            this.cbItemSearchLanguage.Name = "cbItemSearchLanguage";
            this.cbItemSearchLanguage.Size = new System.Drawing.Size(75, 21);
            this.cbItemSearchLanguage.TabIndex = 5;
            this.cbItemSearchLanguage.SelectedIndexChanged += new System.EventHandler(this.CbItemSearchLanguage_SelectedIndexChanged);
            // 
            // tpItems
            // 
            this.tpItems.Controls.Add(this.groupBox1);
            this.tpItems.Controls.Add(this.btnItemSearch);
            this.tpItems.Controls.Add(this.dgvItem);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox1.Controls.Add(this.itemIcon);
            this.groupBox1.Controls.Add(this.rtItemDesc);
            this.groupBox1.Controls.Add(this.btnFindItemInLoot);
            this.groupBox1.Controls.Add(this.lItemLevel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lItemCategory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lItemName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lItemID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox1.Location = new System.Drawing.Point(470, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 299);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Info";
            // 
            // itemIcon
            // 
            this.itemIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.itemIcon.Location = new System.Drawing.Point(232, 14);
            this.itemIcon.Name = "itemIcon";
            this.itemIcon.Size = new System.Drawing.Size(64, 64);
            this.itemIcon.TabIndex = 11;
            this.itemIcon.Text = "???";
            this.itemIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtItemDesc
            // 
            this.rtItemDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtItemDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtItemDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtItemDesc.Location = new System.Drawing.Point(9, 81);
            this.rtItemDesc.Name = "rtItemDesc";
            this.rtItemDesc.Size = new System.Drawing.Size(287, 181);
            this.rtItemDesc.TabIndex = 10;
            this.rtItemDesc.Text = "";
            // 
            // btnFindItemInLoot
            // 
            this.btnFindItemInLoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindItemInLoot.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindItemInLoot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFindItemInLoot.Location = new System.Drawing.Point(9, 268);
            this.btnFindItemInLoot.Name = "btnFindItemInLoot";
            this.btnFindItemInLoot.Size = new System.Drawing.Size(92, 23);
            this.btnFindItemInLoot.TabIndex = 9;
            this.btnFindItemInLoot.Text = "Find in Loot";
            this.btnFindItemInLoot.UseVisualStyleBackColor = false;
            this.btnFindItemInLoot.Click += new System.EventHandler(this.BtnFindItemInLoot_Click);
            // 
            // lItemLevel
            // 
            this.lItemLevel.AutoSize = true;
            this.lItemLevel.Location = new System.Drawing.Point(58, 65);
            this.lItemLevel.Name = "lItemLevel";
            this.lItemLevel.Size = new System.Drawing.Size(31, 13);
            this.lItemLevel.TabIndex = 8;
            this.lItemLevel.Text = "none";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Level";
            // 
            // lItemCategory
            // 
            this.lItemCategory.AutoSize = true;
            this.lItemCategory.Location = new System.Drawing.Point(58, 42);
            this.lItemCategory.Name = "lItemCategory";
            this.lItemCategory.Size = new System.Drawing.Size(13, 13);
            this.lItemCategory.TabIndex = 5;
            this.lItemCategory.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Category";
            // 
            // lItemName
            // 
            this.lItemName.AutoSize = true;
            this.lItemName.Location = new System.Drawing.Point(58, 29);
            this.lItemName.Name = "lItemName";
            this.lItemName.Size = new System.Drawing.Size(43, 13);
            this.lItemName.TabIndex = 3;
            this.lItemName.Text = "<none>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Index";
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
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_ID,
            this.Item_Name_EN_US});
            this.dgvItem.Location = new System.Drawing.Point(11, 33);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(453, 275);
            this.dgvItem.TabIndex = 2;
            this.dgvItem.SelectionChanged += new System.EventHandler(this.DgvItemSearch_SelectionChanged);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search in Item ID, Name or description";
            // 
            // tItemSearch
            // 
            this.tItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tItemSearch.Location = new System.Drawing.Point(197, 6);
            this.tItemSearch.Name = "tItemSearch";
            this.tItemSearch.Size = new System.Drawing.Size(182, 20);
            this.tItemSearch.TabIndex = 0;
            this.tItemSearch.TextChanged += new System.EventHandler(this.TItemSearch_TextChanged);
            this.tItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TItemSearch_KeyDown);
            // 
            // tpLoot
            // 
            this.tpLoot.Controls.Add(this.btnLootSearch);
            this.tpLoot.Controls.Add(this.label7);
            this.tpLoot.Controls.Add(this.tLootSearch);
            this.tpLoot.Controls.Add(this.dgvLoot);
            this.tpLoot.Location = new System.Drawing.Point(4, 22);
            this.tpLoot.Name = "tpLoot";
            this.tpLoot.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoot.Size = new System.Drawing.Size(778, 314);
            this.tpLoot.TabIndex = 2;
            this.tpLoot.Text = "Loot";
            this.tpLoot.UseVisualStyleBackColor = true;
            // 
            // btnLootSearch
            // 
            this.btnLootSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLootSearch.Enabled = false;
            this.btnLootSearch.Location = new System.Drawing.Point(220, 9);
            this.btnLootSearch.Name = "btnLootSearch";
            this.btnLootSearch.Size = new System.Drawing.Size(79, 23);
            this.btnLootSearch.TabIndex = 6;
            this.btnLootSearch.Text = "Search";
            this.btnLootSearch.UseVisualStyleBackColor = true;
            this.btnLootSearch.Click += new System.EventHandler(this.BtnLootSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Search Loot Pack ID";
            // 
            // tLootSearch
            // 
            this.tLootSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tLootSearch.Location = new System.Drawing.Point(121, 11);
            this.tLootSearch.Name = "tLootSearch";
            this.tLootSearch.Size = new System.Drawing.Size(93, 20);
            this.tLootSearch.TabIndex = 4;
            this.tLootSearch.TextChanged += new System.EventHandler(this.TLootSearch_TextChanged);
            this.tLootSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TLootSearch_KeyDown);
            // 
            // dgvLoot
            // 
            this.dgvLoot.AllowUserToAddRows = false;
            this.dgvLoot.AllowUserToDeleteRows = false;
            this.dgvLoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLoot.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLoot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column2,
            this.Column1,
            this.Column9,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dgvLoot.Location = new System.Drawing.Point(8, 42);
            this.dgvLoot.Name = "dgvLoot";
            this.dgvLoot.ReadOnly = true;
            this.dgvLoot.RowHeadersVisible = false;
            this.dgvLoot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoot.Size = new System.Drawing.Size(761, 266);
            this.dgvLoot.TabIndex = 3;
            this.dgvLoot.SelectionChanged += new System.EventHandler(this.DgvLoot_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 43;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Loot Pack ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 95;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Item ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 66;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Name";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Drop Rate";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 81;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Min";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 49;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Max";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 52;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Grade ID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Always Drop";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 91;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Group";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 61;
            // 
            // tpSkills
            // 
            this.tpSkills.Controls.Add(this.groupBox2);
            this.tpSkills.Controls.Add(this.btnSkillSearch);
            this.tpSkills.Controls.Add(this.dgvSkills);
            this.tpSkills.Controls.Add(this.label9);
            this.tpSkills.Controls.Add(this.tSkillSearch);
            this.tpSkills.Location = new System.Drawing.Point(4, 22);
            this.tpSkills.Name = "tpSkills";
            this.tpSkills.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkills.Size = new System.Drawing.Size(778, 314);
            this.tpSkills.TabIndex = 3;
            this.tpSkills.Text = "Skill";
            this.tpSkills.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox2.Controls.Add(this.lSkillGCD);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lSkillCooldown);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.lSkillLabor);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lSkillMana);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lSkillCost);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.skillIcon);
            this.groupBox2.Controls.Add(this.rtSkillDescription);
            this.groupBox2.Controls.Add(this.lSkillName);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lSkillID);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox2.Location = new System.Drawing.Point(511, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 299);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skill Info";
            // 
            // lSkillGCD
            // 
            this.lSkillGCD.AutoSize = true;
            this.lSkillGCD.Location = new System.Drawing.Point(97, 104);
            this.lSkillGCD.Name = "lSkillGCD";
            this.lSkillGCD.Size = new System.Drawing.Size(13, 13);
            this.lSkillGCD.TabIndex = 21;
            this.lSkillGCD.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 104);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "Global Cooldown";
            // 
            // lSkillCooldown
            // 
            this.lSkillCooldown.AutoSize = true;
            this.lSkillCooldown.Location = new System.Drawing.Point(97, 91);
            this.lSkillCooldown.Name = "lSkillCooldown";
            this.lSkillCooldown.Size = new System.Drawing.Size(13, 13);
            this.lSkillCooldown.TabIndex = 19;
            this.lSkillCooldown.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Cooldown";
            // 
            // lSkillLabor
            // 
            this.lSkillLabor.AutoSize = true;
            this.lSkillLabor.Location = new System.Drawing.Point(97, 78);
            this.lSkillLabor.Name = "lSkillLabor";
            this.lSkillLabor.Size = new System.Drawing.Size(13, 13);
            this.lSkillLabor.TabIndex = 17;
            this.lSkillLabor.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Labor";
            // 
            // lSkillMana
            // 
            this.lSkillMana.AutoSize = true;
            this.lSkillMana.Location = new System.Drawing.Point(97, 65);
            this.lSkillMana.Name = "lSkillMana";
            this.lSkillMana.Size = new System.Drawing.Size(13, 13);
            this.lSkillMana.TabIndex = 15;
            this.lSkillMana.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Mana";
            // 
            // lSkillCost
            // 
            this.lSkillCost.AutoSize = true;
            this.lSkillCost.Location = new System.Drawing.Point(97, 51);
            this.lSkillCost.Name = "lSkillCost";
            this.lSkillCost.Size = new System.Drawing.Size(13, 13);
            this.lSkillCost.TabIndex = 13;
            this.lSkillCost.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Cost";
            // 
            // skillIcon
            // 
            this.skillIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skillIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skillIcon.Location = new System.Drawing.Point(188, 51);
            this.skillIcon.Name = "skillIcon";
            this.skillIcon.Size = new System.Drawing.Size(64, 64);
            this.skillIcon.TabIndex = 11;
            this.skillIcon.Text = "???";
            this.skillIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtSkillDescription
            // 
            this.rtSkillDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtSkillDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtSkillDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtSkillDescription.Location = new System.Drawing.Point(9, 125);
            this.rtSkillDescription.Name = "rtSkillDescription";
            this.rtSkillDescription.Size = new System.Drawing.Size(243, 168);
            this.rtSkillDescription.TabIndex = 10;
            this.rtSkillDescription.Text = "";
            // 
            // lSkillName
            // 
            this.lSkillName.AutoSize = true;
            this.lSkillName.Location = new System.Drawing.Point(58, 29);
            this.lSkillName.Name = "lSkillName";
            this.lSkillName.Size = new System.Drawing.Size(43, 13);
            this.lSkillName.TabIndex = 3;
            this.lSkillName.Text = "<none>";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Name";
            // 
            // lSkillID
            // 
            this.lSkillID.AutoSize = true;
            this.lSkillID.Location = new System.Drawing.Point(58, 16);
            this.lSkillID.Name = "lSkillID";
            this.lSkillID.Size = new System.Drawing.Size(13, 13);
            this.lSkillID.TabIndex = 1;
            this.lSkillID.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Index";
            // 
            // btnSkillSearch
            // 
            this.btnSkillSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkillSearch.Enabled = false;
            this.btnSkillSearch.Location = new System.Drawing.Point(426, 4);
            this.btnSkillSearch.Name = "btnSkillSearch";
            this.btnSkillSearch.Size = new System.Drawing.Size(79, 23);
            this.btnSkillSearch.TabIndex = 7;
            this.btnSkillSearch.Text = "Search";
            this.btnSkillSearch.UseVisualStyleBackColor = true;
            this.btnSkillSearch.Click += new System.EventHandler(this.BtnSkillSearch_Click);
            // 
            // dgvSkills
            // 
            this.dgvSkills.AllowUserToAddRows = false;
            this.dgvSkills.AllowUserToDeleteRows = false;
            this.dgvSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSkills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSkills.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSkills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column10,
            this.Column11});
            this.dgvSkills.Location = new System.Drawing.Point(11, 33);
            this.dgvSkills.Name = "dgvSkills";
            this.dgvSkills.ReadOnly = true;
            this.dgvSkills.RowHeadersVisible = false;
            this.dgvSkills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSkills.Size = new System.Drawing.Size(494, 275);
            this.dgvSkills.TabIndex = 6;
            this.dgvSkills.SelectionChanged += new System.EventHandler(this.DgvSkills_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 43;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 200F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Description";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 85;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Web Desc.";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 86;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Search in Skill ID, Name or description";
            // 
            // tSkillSearch
            // 
            this.tSkillSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tSkillSearch.Location = new System.Drawing.Point(197, 6);
            this.tSkillSearch.Name = "tSkillSearch";
            this.tSkillSearch.Size = new System.Drawing.Size(223, 20);
            this.tSkillSearch.TabIndex = 4;
            this.tSkillSearch.TextChanged += new System.EventHandler(this.TSkillSearch_TextChanged);
            this.tSkillSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSkillSearch_KeyDown);
            // 
            // openDBDlg
            // 
            this.openDBDlg.DefaultExt = "sqlite3";
            this.openDBDlg.FileName = "compact.sqlite3";
            this.openDBDlg.Filter = "SQLite3 files|*.sqlite3|All files|*.*";
            this.openDBDlg.Title = "Open Server DB File";
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.tpLoot.ResumeLayout(false);
            this.tpLoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoot)).EndInit();
            this.tpSkills.ResumeLayout(false);
            this.tpSkills.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbTableNames;
        private System.Windows.Forms.TabControl tcViewer;
        private System.Windows.Forms.TabPage tbTables;
        private System.Windows.Forms.TabPage tpItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tItemSearch;
        private System.Windows.Forms.DataGridView dgvItem;
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
        private System.Windows.Forms.Label lItemLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openDBDlg;
        private System.Windows.Forms.Button btnOpenServerDB;
        private System.Windows.Forms.TabPage tpLoot;
        private System.Windows.Forms.Button btnFindItemInLoot;
        private System.Windows.Forms.DataGridView dgvLoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button btnLootSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tLootSearch;
        private System.Windows.Forms.RichTextBox rtItemDesc;
        private System.Windows.Forms.Label itemIcon;
        private System.Windows.Forms.Button btnFindGameClient;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tpSkills;
        private System.Windows.Forms.Button btnSkillSearch;
        private System.Windows.Forms.DataGridView dgvSkills;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tSkillSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label skillIcon;
        private System.Windows.Forms.RichTextBox rtSkillDescription;
        private System.Windows.Forms.Label lSkillName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lSkillID;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lSkillCost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lSkillGCD;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lSkillCooldown;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lSkillLabor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lSkillMana;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}


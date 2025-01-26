namespace AAEmu.DBEditor.forms.server
{
    partial class CharacterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Select a character in the list on the left ...");
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Select a character in the list on the left ...");
            tFilter = new System.Windows.Forms.TextBox();
            lvCharacterList = new System.Windows.Forms.ListView();
            ilRaces = new System.Windows.Forms.ImageList(components);
            label1 = new System.Windows.Forms.Label();
            tcCharacter = new System.Windows.Forms.TabControl();
            tpServerStats = new System.Windows.Forms.TabPage();
            tvStats = new System.Windows.Forms.TreeView();
            tpItems = new System.Windows.Forms.TabPage();
            lContainer = new System.Windows.Forms.Label();
            lvItems = new System.Windows.Forms.ListView();
            chItemTemplateId = new System.Windows.Forms.ColumnHeader();
            chItemCount = new System.Windows.Forms.ColumnHeader();
            chItemName = new System.Windows.Forms.ColumnHeader();
            chItemCategory = new System.Windows.Forms.ColumnHeader();
            chItemDbId = new System.Windows.Forms.ColumnHeader();
            chItemSlot = new System.Windows.Forms.ColumnHeader();
            chItemSlotType = new System.Windows.Forms.ColumnHeader();
            gbContainerSelect = new System.Windows.Forms.GroupBox();
            cbItemContainerTypeSelect = new System.Windows.Forms.ComboBox();
            radioButton1 = new System.Windows.Forms.RadioButton();
            rbPetGear = new System.Windows.Forms.RadioButton();
            rbSystem = new System.Windows.Forms.RadioButton();
            rbMail = new System.Windows.Forms.RadioButton();
            rbWarehouse = new System.Windows.Forms.RadioButton();
            rbInventory = new System.Windows.Forms.RadioButton();
            rbEquipement = new System.Windows.Forms.RadioButton();
            tpOwnedObjects = new System.Windows.Forms.TabPage();
            cbIncludeAccountHouses = new System.Windows.Forms.CheckBox();
            tvOwned = new System.Windows.Forms.TreeView();
            btnSelectionOK = new System.Windows.Forms.Button();
            tcCharacter.SuspendLayout();
            tpServerStats.SuspendLayout();
            tpItems.SuspendLayout();
            gbContainerSelect.SuspendLayout();
            tpOwnedObjects.SuspendLayout();
            SuspendLayout();
            // 
            // tFilter
            // 
            tFilter.Location = new System.Drawing.Point(12, 32);
            tFilter.Name = "tFilter";
            tFilter.PlaceholderText = "<filter by name or ID>";
            tFilter.Size = new System.Drawing.Size(177, 23);
            tFilter.TabIndex = 0;
            tFilter.TextChanged += tFilter_TextChanged;
            // 
            // lvCharacterList
            // 
            lvCharacterList.AllowDrop = true;
            lvCharacterList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lvCharacterList.BackColor = System.Drawing.SystemColors.Window;
            lvCharacterList.FullRowSelect = true;
            lvCharacterList.LargeImageList = ilRaces;
            lvCharacterList.Location = new System.Drawing.Point(12, 61);
            lvCharacterList.MultiSelect = false;
            lvCharacterList.Name = "lvCharacterList";
            lvCharacterList.Size = new System.Drawing.Size(177, 346);
            lvCharacterList.SmallImageList = ilRaces;
            lvCharacterList.TabIndex = 6;
            lvCharacterList.TileSize = new System.Drawing.Size(255, 64);
            lvCharacterList.UseCompatibleStateImageBehavior = false;
            lvCharacterList.View = System.Windows.Forms.View.SmallIcon;
            lvCharacterList.SelectedIndexChanged += lvCharacterList_SelectedIndexChanged;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(33, 16);
            label1.TabIndex = 7;
            label1.Text = "Filter";
            // 
            // tcCharacter
            // 
            tcCharacter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tcCharacter.Controls.Add(tpServerStats);
            tcCharacter.Controls.Add(tpItems);
            tcCharacter.Controls.Add(tpOwnedObjects);
            tcCharacter.Location = new System.Drawing.Point(195, 9);
            tcCharacter.Name = "tcCharacter";
            tcCharacter.SelectedIndex = 0;
            tcCharacter.Size = new System.Drawing.Size(620, 398);
            tcCharacter.TabIndex = 8;
            // 
            // tpServerStats
            // 
            tpServerStats.Controls.Add(tvStats);
            tpServerStats.Location = new System.Drawing.Point(4, 25);
            tpServerStats.Name = "tpServerStats";
            tpServerStats.Padding = new System.Windows.Forms.Padding(3);
            tpServerStats.Size = new System.Drawing.Size(612, 369);
            tpServerStats.TabIndex = 0;
            tpServerStats.Text = "Stats";
            tpServerStats.UseVisualStyleBackColor = true;
            // 
            // tvStats
            // 
            tvStats.Dock = System.Windows.Forms.DockStyle.Fill;
            tvStats.Location = new System.Drawing.Point(3, 3);
            tvStats.Name = "tvStats";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Select a character in the list on the left ...";
            tvStats.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
            tvStats.Size = new System.Drawing.Size(606, 363);
            tvStats.TabIndex = 4;
            // 
            // tpItems
            // 
            tpItems.Controls.Add(lContainer);
            tpItems.Controls.Add(lvItems);
            tpItems.Controls.Add(gbContainerSelect);
            tpItems.Location = new System.Drawing.Point(4, 25);
            tpItems.Name = "tpItems";
            tpItems.Padding = new System.Windows.Forms.Padding(3);
            tpItems.Size = new System.Drawing.Size(612, 369);
            tpItems.TabIndex = 1;
            tpItems.Text = "Items";
            tpItems.UseVisualStyleBackColor = true;
            tpItems.Enter += tpItems_Enter;
            // 
            // lContainer
            // 
            lContainer.AutoSize = true;
            lContainer.Location = new System.Drawing.Point(6, 63);
            lContainer.Name = "lContainer";
            lContainer.Size = new System.Drawing.Size(58, 16);
            lContainer.TabIndex = 2;
            lContainer.Text = "Container";
            // 
            // lvItems
            // 
            lvItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { chItemTemplateId, chItemCount, chItemName, chItemCategory, chItemDbId, chItemSlot, chItemSlotType });
            lvItems.FullRowSelect = true;
            lvItems.GridLines = true;
            lvItems.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            lvItems.Location = new System.Drawing.Point(6, 82);
            lvItems.MultiSelect = false;
            lvItems.Name = "lvItems";
            lvItems.Size = new System.Drawing.Size(600, 281);
            lvItems.TabIndex = 1;
            lvItems.UseCompatibleStateImageBehavior = false;
            lvItems.View = System.Windows.Forms.View.Details;
            // 
            // chItemTemplateId
            // 
            chItemTemplateId.Text = "TemplateId";
            chItemTemplateId.Width = 70;
            // 
            // chItemCount
            // 
            chItemCount.Text = "Amount";
            // 
            // chItemName
            // 
            chItemName.Text = "Name";
            chItemName.Width = 200;
            // 
            // chItemCategory
            // 
            chItemCategory.Text = "Category";
            chItemCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            chItemCategory.Width = 80;
            // 
            // chItemDbId
            // 
            chItemDbId.Text = "DBID";
            chItemDbId.Width = 80;
            // 
            // chItemSlot
            // 
            chItemSlot.Text = "Slot#";
            chItemSlot.Width = 80;
            // 
            // chItemSlotType
            // 
            chItemSlotType.Text = "SlotType";
            chItemSlotType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            chItemSlotType.Width = 30;
            // 
            // gbContainerSelect
            // 
            gbContainerSelect.Controls.Add(cbItemContainerTypeSelect);
            gbContainerSelect.Controls.Add(radioButton1);
            gbContainerSelect.Controls.Add(rbPetGear);
            gbContainerSelect.Controls.Add(rbSystem);
            gbContainerSelect.Controls.Add(rbMail);
            gbContainerSelect.Controls.Add(rbWarehouse);
            gbContainerSelect.Controls.Add(rbInventory);
            gbContainerSelect.Controls.Add(rbEquipement);
            gbContainerSelect.Location = new System.Drawing.Point(6, 6);
            gbContainerSelect.Name = "gbContainerSelect";
            gbContainerSelect.Size = new System.Drawing.Size(600, 54);
            gbContainerSelect.TabIndex = 0;
            gbContainerSelect.TabStop = false;
            gbContainerSelect.Text = "Container";
            // 
            // cbItemContainerTypeSelect
            // 
            cbItemContainerTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbItemContainerTypeSelect.FormattingEnabled = true;
            cbItemContainerTypeSelect.Location = new System.Drawing.Point(528, 21);
            cbItemContainerTypeSelect.Name = "cbItemContainerTypeSelect";
            cbItemContainerTypeSelect.Size = new System.Drawing.Size(66, 24);
            cbItemContainerTypeSelect.TabIndex = 8;
            cbItemContainerTypeSelect.SelectedIndexChanged += cbItemContainerTypeSelect_SelectedIndexChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new System.Drawing.Point(319, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(67, 20);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Tag = "6";
            radioButton1.Text = "Auction";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbPetGear
            // 
            rbPetGear.AutoSize = true;
            rbPetGear.Location = new System.Drawing.Point(392, 22);
            rbPetGear.Name = "rbPetGear";
            rbPetGear.Size = new System.Drawing.Size(69, 20);
            rbPetGear.TabIndex = 6;
            rbPetGear.TabStop = true;
            rbPetGear.Tag = "252";
            rbPetGear.Text = "Pet Gear";
            rbPetGear.UseVisualStyleBackColor = true;
            rbPetGear.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbSystem
            // 
            rbSystem.AutoSize = true;
            rbSystem.Location = new System.Drawing.Point(467, 22);
            rbSystem.Name = "rbSystem";
            rbSystem.Size = new System.Drawing.Size(55, 20);
            rbSystem.TabIndex = 5;
            rbSystem.TabStop = true;
            rbSystem.Tag = "-1";
            rbSystem.Text = "Other";
            rbSystem.UseVisualStyleBackColor = true;
            rbSystem.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbMail
            // 
            rbMail.AutoSize = true;
            rbMail.Location = new System.Drawing.Point(265, 22);
            rbMail.Name = "rbMail";
            rbMail.Size = new System.Drawing.Size(48, 20);
            rbMail.TabIndex = 4;
            rbMail.TabStop = true;
            rbMail.Tag = "5";
            rbMail.Text = "Mail";
            rbMail.UseVisualStyleBackColor = true;
            rbMail.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbWarehouse
            // 
            rbWarehouse.AutoSize = true;
            rbWarehouse.Location = new System.Drawing.Point(175, 22);
            rbWarehouse.Name = "rbWarehouse";
            rbWarehouse.Size = new System.Drawing.Size(84, 20);
            rbWarehouse.TabIndex = 2;
            rbWarehouse.TabStop = true;
            rbWarehouse.Tag = "3";
            rbWarehouse.Text = "Warehouse";
            rbWarehouse.UseVisualStyleBackColor = true;
            rbWarehouse.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbInventory
            // 
            rbInventory.AutoSize = true;
            rbInventory.Location = new System.Drawing.Point(94, 22);
            rbInventory.Name = "rbInventory";
            rbInventory.Size = new System.Drawing.Size(75, 20);
            rbInventory.TabIndex = 1;
            rbInventory.TabStop = true;
            rbInventory.Tag = "2";
            rbInventory.Text = "Inventory";
            rbInventory.UseVisualStyleBackColor = true;
            rbInventory.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // rbEquipement
            // 
            rbEquipement.AutoSize = true;
            rbEquipement.Location = new System.Drawing.Point(6, 22);
            rbEquipement.Name = "rbEquipement";
            rbEquipement.Size = new System.Drawing.Size(82, 20);
            rbEquipement.TabIndex = 0;
            rbEquipement.TabStop = true;
            rbEquipement.Tag = "1";
            rbEquipement.Text = "Equipment";
            rbEquipement.UseVisualStyleBackColor = true;
            rbEquipement.CheckedChanged += rbContainers_CheckedChanged;
            // 
            // tpOwnedObjects
            // 
            tpOwnedObjects.Controls.Add(cbIncludeAccountHouses);
            tpOwnedObjects.Controls.Add(tvOwned);
            tpOwnedObjects.Location = new System.Drawing.Point(4, 25);
            tpOwnedObjects.Name = "tpOwnedObjects";
            tpOwnedObjects.Padding = new System.Windows.Forms.Padding(3);
            tpOwnedObjects.Size = new System.Drawing.Size(612, 369);
            tpOwnedObjects.TabIndex = 2;
            tpOwnedObjects.Text = "Owned";
            tpOwnedObjects.UseVisualStyleBackColor = true;
            // 
            // cbIncludeAccountHouses
            // 
            cbIncludeAccountHouses.AutoSize = true;
            cbIncludeAccountHouses.Checked = true;
            cbIncludeAccountHouses.CheckState = System.Windows.Forms.CheckState.Checked;
            cbIncludeAccountHouses.Location = new System.Drawing.Point(3, 3);
            cbIncludeAccountHouses.Name = "cbIncludeAccountHouses";
            cbIncludeAccountHouses.Size = new System.Drawing.Size(221, 20);
            cbIncludeAccountHouses.TabIndex = 6;
            cbIncludeAccountHouses.Text = "Include other houses on this account";
            cbIncludeAccountHouses.UseVisualStyleBackColor = true;
            cbIncludeAccountHouses.CheckedChanged += cbIncludeAccountHouses_CheckedChanged;
            // 
            // tvOwned
            // 
            tvOwned.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tvOwned.Location = new System.Drawing.Point(3, 27);
            tvOwned.Name = "tvOwned";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Select a character in the list on the left ...";
            tvOwned.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode2 });
            tvOwned.Size = new System.Drawing.Size(606, 339);
            tvOwned.TabIndex = 5;
            // 
            // btnSelectionOK
            // 
            btnSelectionOK.Location = new System.Drawing.Point(124, 9);
            btnSelectionOK.Name = "btnSelectionOK";
            btnSelectionOK.Size = new System.Drawing.Size(65, 23);
            btnSelectionOK.TabIndex = 9;
            btnSelectionOK.Text = "Select";
            btnSelectionOK.UseVisualStyleBackColor = true;
            btnSelectionOK.Visible = false;
            btnSelectionOK.Click += btnSelectionOK_Click;
            // 
            // CharacterForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(827, 419);
            Controls.Add(btnSelectionOK);
            Controls.Add(tcCharacter);
            Controls.Add(label1);
            Controls.Add(lvCharacterList);
            Controls.Add(tFilter);
            Name = "CharacterForm";
            Text = "Character";
            FormClosed += CharacterForm_FormClosed;
            Load += CharacterForm_Load;
            tcCharacter.ResumeLayout(false);
            tpServerStats.ResumeLayout(false);
            tpItems.ResumeLayout(false);
            tpItems.PerformLayout();
            gbContainerSelect.ResumeLayout(false);
            gbContainerSelect.PerformLayout();
            tpOwnedObjects.ResumeLayout(false);
            tpOwnedObjects.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tFilter;
        private System.Windows.Forms.ListView lvCharacterList;
        private System.Windows.Forms.ImageList ilRaces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tcCharacter;
        private System.Windows.Forms.TabPage tpServerStats;
        private System.Windows.Forms.TabPage tpItems;
        private System.Windows.Forms.GroupBox gbContainerSelect;
        private System.Windows.Forms.RadioButton rbPetGear;
        private System.Windows.Forms.RadioButton rbSystem;
        private System.Windows.Forms.RadioButton rbMail;
        private System.Windows.Forms.RadioButton rbWarehouse;
        private System.Windows.Forms.RadioButton rbInventory;
        private System.Windows.Forms.RadioButton rbEquipement;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chItemTemplateId;
        private System.Windows.Forms.ColumnHeader chItemName;
        private System.Windows.Forms.ColumnHeader chItemCategory;
        private System.Windows.Forms.ColumnHeader chItemDbId;
        private System.Windows.Forms.ColumnHeader chItemCount;
        private System.Windows.Forms.Label lContainer;
        private System.Windows.Forms.ColumnHeader chItemSlot;
        private System.Windows.Forms.TreeView tvStats;
        private System.Windows.Forms.TabPage tpOwnedObjects;
        private System.Windows.Forms.TreeView tvOwned;
        private System.Windows.Forms.CheckBox cbIncludeAccountHouses;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ColumnHeader chItemSlotType;
        private System.Windows.Forms.ComboBox cbItemContainerTypeSelect;
        public System.Windows.Forms.Button btnSelectionOK;
    }
}
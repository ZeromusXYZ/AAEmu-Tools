namespace AAEmu.DBEditor.forms.client
{
    partial class ItemsForm
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            label9 = new System.Windows.Forms.Label();
            cbFilterCategory = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            cbFilterImplement = new System.Windows.Forms.ComboBox();
            cbSearchCustom = new System.Windows.Forms.CheckBox();
            cbSearchRegion = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            cbSearchVanilla = new System.Windows.Forms.CheckBox();
            btnSearch = new System.Windows.Forms.Button();
            cbSearchBox = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            lvItems = new System.Windows.Forms.ListView();
            chItemId = new System.Windows.Forms.ColumnHeader();
            chItemName = new System.Windows.Forms.ColumnHeader();
            chItemCategory = new System.Windows.Forms.ColumnHeader();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lItemGMCommand = new System.Windows.Forms.Label();
            btnSelect = new System.Windows.Forms.Button();
            lItemRequires = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            lItemIcon = new System.Windows.Forms.Label();
            lItemImplId = new System.Windows.Forms.Label();
            label140 = new System.Windows.Forms.Label();
            lItemTags = new System.Windows.Forms.Label();
            itemIcon = new System.Windows.Forms.Label();
            rtItemDesc = new System.Windows.Forms.RichTextBox();
            lItemLevel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            lItemCategory = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            lItemName = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lItemID = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            cbDescriptionSearch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(cbDescriptionSearch);
            splitContainer1.Panel1.Controls.Add(label9);
            splitContainer1.Panel1.Controls.Add(cbFilterCategory);
            splitContainer1.Panel1.Controls.Add(label8);
            splitContainer1.Panel1.Controls.Add(cbFilterImplement);
            splitContainer1.Panel1.Controls.Add(cbSearchCustom);
            splitContainer1.Panel1.Controls.Add(cbSearchRegion);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(cbSearchVanilla);
            splitContainer1.Panel1.Controls.Add(btnSearch);
            splitContainer1.Panel1.Controls.Add(cbSearchBox);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new System.Drawing.Size(800, 480);
            splitContainer1.SplitterDistance = 80;
            splitContainer1.TabIndex = 0;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(414, 39);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(99, 16);
            label9.TabIndex = 10;
            label9.Text = "Filter by Category";
            // 
            // cbFilterCategory
            // 
            cbFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbFilterCategory.FormattingEnabled = true;
            cbFilterCategory.Location = new System.Drawing.Point(528, 36);
            cbFilterCategory.Name = "cbFilterCategory";
            cbFilterCategory.Size = new System.Drawing.Size(182, 24);
            cbFilterCategory.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(414, 9);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(108, 16);
            label8.TabIndex = 8;
            label8.Text = "Filter by Implement";
            // 
            // cbFilterImplement
            // 
            cbFilterImplement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbFilterImplement.FormattingEnabled = true;
            cbFilterImplement.Location = new System.Drawing.Point(528, 6);
            cbFilterImplement.Name = "cbFilterImplement";
            cbFilterImplement.Size = new System.Drawing.Size(182, 24);
            cbFilterImplement.TabIndex = 7;
            // 
            // cbSearchCustom
            // 
            cbSearchCustom.AutoSize = true;
            cbSearchCustom.Checked = true;
            cbSearchCustom.CheckState = System.Windows.Forms.CheckState.Checked;
            cbSearchCustom.Location = new System.Drawing.Point(291, 36);
            cbSearchCustom.Name = "cbSearchCustom";
            cbSearchCustom.Size = new System.Drawing.Size(66, 20);
            cbSearchCustom.TabIndex = 6;
            cbSearchCustom.Text = "Custom";
            cbSearchCustom.UseVisualStyleBackColor = true;
            // 
            // cbSearchRegion
            // 
            cbSearchRegion.AutoSize = true;
            cbSearchRegion.Checked = true;
            cbSearchRegion.CheckState = System.Windows.Forms.CheckState.Checked;
            cbSearchRegion.Location = new System.Drawing.Point(178, 36);
            cbSearchRegion.Name = "cbSearchRegion";
            cbSearchRegion.Size = new System.Drawing.Size(107, 20);
            cbSearchRegion.TabIndex = 5;
            cbSearchRegion.Text = "Region Specific";
            cbSearchRegion.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 37);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(78, 16);
            label2.TabIndex = 4;
            label2.Text = "Search range:";
            // 
            // cbSearchVanilla
            // 
            cbSearchVanilla.AutoSize = true;
            cbSearchVanilla.Checked = true;
            cbSearchVanilla.CheckState = System.Windows.Forms.CheckState.Checked;
            cbSearchVanilla.Location = new System.Drawing.Point(112, 36);
            cbSearchVanilla.Name = "cbSearchVanilla";
            cbSearchVanilla.Size = new System.Drawing.Size(60, 20);
            cbSearchVanilla.TabIndex = 3;
            cbSearchVanilla.Text = "Vanilla";
            cbSearchVanilla.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(291, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(104, 25);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cbSearchBox
            // 
            cbSearchBox.FormattingEnabled = true;
            cbSearchBox.Location = new System.Drawing.Point(112, 6);
            cbSearchBox.Name = "cbSearchBox";
            cbSearchBox.Size = new System.Drawing.Size(173, 24);
            cbSearchBox.TabIndex = 1;
            cbSearchBox.KeyDown += cbSearchBox_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 16);
            label1.TabIndex = 0;
            label1.Text = "Search Text or ID";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(lvItems);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(groupBox1);
            splitContainer2.Size = new System.Drawing.Size(800, 396);
            splitContainer2.SplitterDistance = 410;
            splitContainer2.TabIndex = 0;
            // 
            // lvItems
            // 
            lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { chItemId, chItemName, chItemCategory });
            lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            lvItems.FullRowSelect = true;
            lvItems.GridLines = true;
            lvItems.Location = new System.Drawing.Point(0, 0);
            lvItems.Name = "lvItems";
            lvItems.Size = new System.Drawing.Size(410, 396);
            lvItems.TabIndex = 0;
            lvItems.UseCompatibleStateImageBehavior = false;
            lvItems.View = System.Windows.Forms.View.Details;
            lvItems.SelectedIndexChanged += lvItems_SelectedIndexChanged;
            // 
            // chItemId
            // 
            chItemId.Text = "ID";
            chItemId.Width = 100;
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
            // groupBox1
            // 
            groupBox1.BackColor = System.Drawing.Color.FromArgb(70, 60, 40);
            groupBox1.Controls.Add(lItemGMCommand);
            groupBox1.Controls.Add(btnSelect);
            groupBox1.Controls.Add(lItemRequires);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(lItemIcon);
            groupBox1.Controls.Add(lItemImplId);
            groupBox1.Controls.Add(label140);
            groupBox1.Controls.Add(lItemTags);
            groupBox1.Controls.Add(itemIcon);
            groupBox1.Controls.Add(rtItemDesc);
            groupBox1.Controls.Add(lItemLevel);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(lItemCategory);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lItemName);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lItemID);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(208, 192, 171);
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Margin = new System.Windows.Forms.Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4);
            groupBox1.Size = new System.Drawing.Size(386, 396);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Item Info";
            // 
            // lItemGMCommand
            // 
            lItemGMCommand.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lItemGMCommand.AutoSize = true;
            lItemGMCommand.Location = new System.Drawing.Point(8, 369);
            lItemGMCommand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemGMCommand.Name = "lItemGMCommand";
            lItemGMCommand.Size = new System.Drawing.Size(35, 16);
            lItemGMCommand.TabIndex = 32;
            lItemGMCommand.Text = "/item";
            // 
            // btnSelect
            // 
            btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSelect.Location = new System.Drawing.Point(300, 366);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new System.Drawing.Size(75, 23);
            btnSelect.TabIndex = 31;
            btnSelect.Text = "Select";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // lItemRequires
            // 
            lItemRequires.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lItemRequires.AutoSize = true;
            lItemRequires.Location = new System.Drawing.Point(228, 69);
            lItemRequires.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemRequires.Name = "lItemRequires";
            lItemRequires.Size = new System.Drawing.Size(34, 16);
            lItemRequires.TabIndex = 30;
            lItemRequires.Text = "none";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(168, 69);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 16);
            label7.TabIndex = 29;
            label7.Text = "Requires";
            // 
            // lItemIcon
            // 
            lItemIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lItemIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lItemIcon.Location = new System.Drawing.Point(303, 13);
            lItemIcon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemIcon.Name = "lItemIcon";
            lItemIcon.Size = new System.Drawing.Size(72, 72);
            lItemIcon.TabIndex = 28;
            lItemIcon.Text = "???";
            lItemIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lItemImplId
            // 
            lItemImplId.AutoSize = true;
            lItemImplId.Location = new System.Drawing.Point(69, 53);
            lItemImplId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemImplId.Name = "lItemImplId";
            lItemImplId.Size = new System.Drawing.Size(13, 16);
            lItemImplId.TabIndex = 27;
            lItemImplId.Text = "0";
            // 
            // label140
            // 
            label140.AutoSize = true;
            label140.Location = new System.Drawing.Point(8, 53);
            label140.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label140.Name = "label140";
            label140.Size = new System.Drawing.Size(63, 16);
            label140.TabIndex = 26;
            label140.Text = "Implement";
            // 
            // lItemTags
            // 
            lItemTags.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lItemTags.AutoSize = true;
            lItemTags.Location = new System.Drawing.Point(8, 353);
            lItemTags.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemTags.Name = "lItemTags";
            lItemTags.Size = new System.Drawing.Size(22, 16);
            lItemTags.TabIndex = 25;
            lItemTags.Text = "???";
            // 
            // itemIcon
            // 
            itemIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            itemIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            itemIcon.Location = new System.Drawing.Point(456, 18);
            itemIcon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            itemIcon.Name = "itemIcon";
            itemIcon.Size = new System.Drawing.Size(75, 79);
            itemIcon.TabIndex = 11;
            itemIcon.Text = "???";
            itemIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtItemDesc
            // 
            rtItemDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rtItemDesc.BackColor = System.Drawing.Color.FromArgb(70, 60, 40);
            rtItemDesc.ForeColor = System.Drawing.Color.FromArgb(208, 192, 171);
            rtItemDesc.Location = new System.Drawing.Point(8, 89);
            rtItemDesc.Margin = new System.Windows.Forms.Padding(4);
            rtItemDesc.Name = "rtItemDesc";
            rtItemDesc.Size = new System.Drawing.Size(367, 260);
            rtItemDesc.TabIndex = 10;
            rtItemDesc.Text = "";
            // 
            // lItemLevel
            // 
            lItemLevel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lItemLevel.AutoSize = true;
            lItemLevel.Location = new System.Drawing.Point(228, 53);
            lItemLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemLevel.Name = "lItemLevel";
            lItemLevel.Size = new System.Drawing.Size(34, 16);
            lItemLevel.TabIndex = 8;
            lItemLevel.Text = "none";
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(168, 53);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(34, 16);
            label6.TabIndex = 7;
            label6.Text = "Level";
            // 
            // lItemCategory
            // 
            lItemCategory.AutoSize = true;
            lItemCategory.Location = new System.Drawing.Point(69, 69);
            lItemCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemCategory.Name = "lItemCategory";
            lItemCategory.Size = new System.Drawing.Size(13, 16);
            lItemCategory.TabIndex = 5;
            lItemCategory.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 69);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(54, 16);
            label5.TabIndex = 4;
            label5.Text = "Category";
            // 
            // lItemName
            // 
            lItemName.AutoSize = true;
            lItemName.Location = new System.Drawing.Point(69, 37);
            lItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemName.Name = "lItemName";
            lItemName.Size = new System.Drawing.Size(50, 16);
            lItemName.TabIndex = 3;
            lItemName.Text = "<none>";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 37);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(38, 16);
            label4.TabIndex = 2;
            label4.Text = "Name";
            // 
            // lItemID
            // 
            lItemID.AutoSize = true;
            lItemID.Location = new System.Drawing.Point(69, 21);
            lItemID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemID.Name = "lItemID";
            lItemID.Size = new System.Drawing.Size(13, 16);
            lItemID.TabIndex = 1;
            lItemID.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 21);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(36, 16);
            label3.TabIndex = 0;
            label3.Text = "Index";
            // 
            // cbDescriptionSearch
            // 
            cbDescriptionSearch.AutoSize = true;
            cbDescriptionSearch.Location = new System.Drawing.Point(112, 57);
            cbDescriptionSearch.Name = "cbDescriptionSearch";
            cbDescriptionSearch.Size = new System.Drawing.Size(153, 20);
            cbDescriptionSearch.TabIndex = 11;
            cbDescriptionSearch.Text = "Also search descriptions";
            cbDescriptionSearch.UseVisualStyleBackColor = true;
            // 
            // ItemsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 480);
            Controls.Add(splitContainer1);
            Name = "ItemsForm";
            Text = "Items";
            FormClosed += ItemsForm_FormClosed;
            Load += ItemsForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbSearchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chItemId;
        private System.Windows.Forms.ColumnHeader chItemName;
        private System.Windows.Forms.ColumnHeader chItemCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lItemImplId;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label lItemTags;
        private System.Windows.Forms.Label itemIcon;
        private System.Windows.Forms.RichTextBox rtItemDesc;
        private System.Windows.Forms.Label lItemLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lItemCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lItemID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lItemIcon;
        private System.Windows.Forms.Label lItemRequires;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbSearchVanilla;
        private System.Windows.Forms.CheckBox cbSearchCustom;
        private System.Windows.Forms.CheckBox cbSearchRegion;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lItemGMCommand;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbFilterImplement;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFilterCategory;
        private System.Windows.Forms.CheckBox cbDescriptionSearch;
    }
}
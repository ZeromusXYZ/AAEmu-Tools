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
            btnSearch = new System.Windows.Forms.Button();
            cbSearchBox = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            lvItems = new System.Windows.Forms.ListView();
            chItemId = new System.Windows.Forms.ColumnHeader();
            chItemName = new System.Windows.Forms.ColumnHeader();
            chItemType = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(btnSearch);
            splitContainer1.Panel1.Controls.Add(cbSearchBox);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new System.Drawing.Size(800, 450);
            splitContainer1.SplitterDistance = 100;
            splitContainer1.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(279, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(104, 23);
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
            cbSearchBox.Size = new System.Drawing.Size(161, 23);
            cbSearchBox.TabIndex = 1;
            cbSearchBox.KeyDown += cbSearchBox_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 15);
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
            splitContainer2.Size = new System.Drawing.Size(800, 346);
            splitContainer2.SplitterDistance = 410;
            splitContainer2.TabIndex = 0;
            // 
            // lvItems
            // 
            lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { chItemId, chItemName, chItemType });
            lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            lvItems.FullRowSelect = true;
            lvItems.GridLines = true;
            lvItems.Location = new System.Drawing.Point(0, 0);
            lvItems.Name = "lvItems";
            lvItems.Size = new System.Drawing.Size(410, 346);
            lvItems.TabIndex = 0;
            lvItems.UseCompatibleStateImageBehavior = false;
            lvItems.View = System.Windows.Forms.View.Details;
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
            // chItemType
            // 
            chItemType.Text = "Type";
            chItemType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            chItemType.Width = 80;
            // 
            // ItemsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "ItemsForm";
            Text = "Items";
            FormClosed += ItemsForm_FormClosed;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.ColumnHeader chItemType;
    }
}
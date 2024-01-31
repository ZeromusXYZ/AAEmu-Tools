namespace AAEmu.DBEditor.forms.server
{
    partial class ICSForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ICSForm));
            tcICS = new System.Windows.Forms.TabControl();
            tpInfo = new System.Windows.Forms.TabPage();
            textBox1 = new System.Windows.Forms.TextBox();
            tpSKUs = new System.Windows.Forms.TabPage();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            lvSKUs = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            btnSKUGetNewId = new System.Windows.Forms.Button();
            lSKUDiscountCalculation = new System.Windows.Forms.Label();
            btnSKUNew = new System.Windows.Forms.Button();
            btnSKUUpdate = new System.Windows.Forms.Button();
            tSKUBonusItemCount = new System.Windows.Forms.TextBox();
            tSKUBonusItemId = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            tSKUDiscountPrice = new System.Windows.Forms.TextBox();
            tSKUPrice = new System.Windows.Forms.TextBox();
            tSKUEventType = new System.Windows.Forms.TextBox();
            tSKUSelectType = new System.Windows.Forms.TextBox();
            tSKUItemCount = new System.Windows.Forms.TextBox();
            tSKUItemId = new System.Windows.Forms.TextBox();
            tSKUShopEntryPosition = new System.Windows.Forms.TextBox();
            cbSKUShopEntryId = new System.Windows.Forms.ComboBox();
            tSKUSKU = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            rbSKUCurrencyCoins = new System.Windows.Forms.RadioButton();
            rbSKUCurrencyLoyalty = new System.Windows.Forms.RadioButton();
            rbSKUCurrencyAAPoints = new System.Windows.Forms.RadioButton();
            label9 = new System.Windows.Forms.Label();
            rbSKUCurrencyCredits = new System.Windows.Forms.RadioButton();
            cbSKUEventHasEnd = new System.Windows.Forms.CheckBox();
            dtpSKUEventEndDate = new System.Windows.Forms.DateTimePicker();
            label8 = new System.Windows.Forms.Label();
            cbSKUIsDefault = new System.Windows.Forms.CheckBox();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tpShopEntries = new System.Windows.Forms.TabPage();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            tvShopItems = new System.Windows.Forms.TreeView();
            tpShopTabs = new System.Windows.Forms.TabPage();
            tcICS.SuspendLayout();
            tpInfo.SuspendLayout();
            tpSKUs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tpShopEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // tcICS
            // 
            tcICS.Controls.Add(tpInfo);
            tcICS.Controls.Add(tpSKUs);
            tcICS.Controls.Add(tpShopEntries);
            tcICS.Controls.Add(tpShopTabs);
            tcICS.Dock = System.Windows.Forms.DockStyle.Fill;
            tcICS.Location = new System.Drawing.Point(0, 0);
            tcICS.Name = "tcICS";
            tcICS.SelectedIndex = 0;
            tcICS.Size = new System.Drawing.Size(710, 436);
            tcICS.TabIndex = 0;
            // 
            // tpInfo
            // 
            tpInfo.Controls.Add(textBox1);
            tpInfo.Location = new System.Drawing.Point(4, 25);
            tpInfo.Name = "tpInfo";
            tpInfo.Padding = new System.Windows.Forms.Padding(3);
            tpInfo.Size = new System.Drawing.Size(702, 407);
            tpInfo.TabIndex = 3;
            tpInfo.Text = "Information";
            tpInfo.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Location = new System.Drawing.Point(3, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(696, 401);
            textBox1.TabIndex = 0;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tpSKUs
            // 
            tpSKUs.Controls.Add(splitContainer1);
            tpSKUs.Location = new System.Drawing.Point(4, 25);
            tpSKUs.Name = "tpSKUs";
            tpSKUs.Padding = new System.Windows.Forms.Padding(3);
            tpSKUs.Size = new System.Drawing.Size(702, 407);
            tpSKUs.TabIndex = 0;
            tpSKUs.Text = "SKUs";
            tpSKUs.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvSKUs);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnSKUGetNewId);
            splitContainer1.Panel2.Controls.Add(lSKUDiscountCalculation);
            splitContainer1.Panel2.Controls.Add(btnSKUNew);
            splitContainer1.Panel2.Controls.Add(btnSKUUpdate);
            splitContainer1.Panel2.Controls.Add(tSKUBonusItemCount);
            splitContainer1.Panel2.Controls.Add(tSKUBonusItemId);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(label11);
            splitContainer1.Panel2.Controls.Add(tSKUDiscountPrice);
            splitContainer1.Panel2.Controls.Add(tSKUPrice);
            splitContainer1.Panel2.Controls.Add(tSKUEventType);
            splitContainer1.Panel2.Controls.Add(tSKUSelectType);
            splitContainer1.Panel2.Controls.Add(tSKUItemCount);
            splitContainer1.Panel2.Controls.Add(tSKUItemId);
            splitContainer1.Panel2.Controls.Add(tSKUShopEntryPosition);
            splitContainer1.Panel2.Controls.Add(cbSKUShopEntryId);
            splitContainer1.Panel2.Controls.Add(tSKUSKU);
            splitContainer1.Panel2.Controls.Add(label10);
            splitContainer1.Panel2.Controls.Add(rbSKUCurrencyCoins);
            splitContainer1.Panel2.Controls.Add(rbSKUCurrencyLoyalty);
            splitContainer1.Panel2.Controls.Add(rbSKUCurrencyAAPoints);
            splitContainer1.Panel2.Controls.Add(label9);
            splitContainer1.Panel2.Controls.Add(rbSKUCurrencyCredits);
            splitContainer1.Panel2.Controls.Add(cbSKUEventHasEnd);
            splitContainer1.Panel2.Controls.Add(dtpSKUEventEndDate);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(cbSKUIsDefault);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(label6);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new System.Drawing.Size(696, 401);
            splitContainer1.SplitterDistance = 360;
            splitContainer1.TabIndex = 1;
            // 
            // lvSKUs
            // 
            lvSKUs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvSKUs.Dock = System.Windows.Forms.DockStyle.Fill;
            lvSKUs.FullRowSelect = true;
            lvSKUs.Location = new System.Drawing.Point(0, 0);
            lvSKUs.MultiSelect = false;
            lvSKUs.Name = "lvSKUs";
            lvSKUs.Size = new System.Drawing.Size(360, 401);
            lvSKUs.TabIndex = 0;
            lvSKUs.UseCompatibleStateImageBehavior = false;
            lvSKUs.View = System.Windows.Forms.View.Details;
            lvSKUs.SelectedIndexChanged += lvSKUs_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "SKU";
            columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Item";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Count";
            // 
            // btnSKUGetNewId
            // 
            btnSKUGetNewId.Location = new System.Drawing.Point(284, 7);
            btnSKUGetNewId.Name = "btnSKUGetNewId";
            btnSKUGetNewId.Size = new System.Drawing.Size(43, 23);
            btnSKUGetNewId.TabIndex = 36;
            btnSKUGetNewId.Text = "New";
            btnSKUGetNewId.UseVisualStyleBackColor = true;
            btnSKUGetNewId.Click += btnSKUGetNewId_Click;
            // 
            // lSKUDiscountCalculation
            // 
            lSKUDiscountCalculation.AutoSize = true;
            lSKUDiscountCalculation.Location = new System.Drawing.Point(179, 274);
            lSKUDiscountCalculation.Name = "lSKUDiscountCalculation";
            lSKUDiscountCalculation.Size = new System.Drawing.Size(22, 16);
            lSKUDiscountCalculation.TabIndex = 35;
            lSKUDiscountCalculation.Text = "---";
            // 
            // btnSKUNew
            // 
            btnSKUNew.Enabled = false;
            btnSKUNew.Location = new System.Drawing.Point(223, 358);
            btnSKUNew.Name = "btnSKUNew";
            btnSKUNew.Size = new System.Drawing.Size(104, 23);
            btnSKUNew.TabIndex = 34;
            btnSKUNew.Text = "Add as new";
            btnSKUNew.UseVisualStyleBackColor = true;
            btnSKUNew.Click += btnSKUNew_Click;
            // 
            // btnSKUUpdate
            // 
            btnSKUUpdate.Enabled = false;
            btnSKUUpdate.Location = new System.Drawing.Point(3, 358);
            btnSKUUpdate.Name = "btnSKUUpdate";
            btnSKUUpdate.Size = new System.Drawing.Size(83, 23);
            btnSKUUpdate.TabIndex = 33;
            btnSKUUpdate.Text = "Save";
            btnSKUUpdate.UseVisualStyleBackColor = true;
            btnSKUUpdate.Click += btnSKUUpdate_Click;
            // 
            // tSKUBonusItemCount
            // 
            tSKUBonusItemCount.Location = new System.Drawing.Point(268, 303);
            tSKUBonusItemCount.Name = "tSKUBonusItemCount";
            tSKUBonusItemCount.PlaceholderText = "0";
            tSKUBonusItemCount.Size = new System.Drawing.Size(59, 23);
            tSKUBonusItemCount.TabIndex = 32;
            tSKUBonusItemCount.TextChanged += tSKU_Changed;
            // 
            // tSKUBonusItemId
            // 
            tSKUBonusItemId.Location = new System.Drawing.Point(98, 303);
            tSKUBonusItemId.Name = "tSKUBonusItemId";
            tSKUBonusItemId.PlaceholderText = "0";
            tSKUBonusItemId.Size = new System.Drawing.Size(119, 23);
            tSKUBonusItemId.TabIndex = 31;
            tSKUBonusItemId.TextChanged += tSKU_Changed;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(223, 306);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(39, 16);
            label2.TabIndex = 30;
            label2.Text = "Count";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(3, 306);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(80, 16);
            label11.TabIndex = 29;
            label11.Text = "Bonus Item ID";
            // 
            // tSKUDiscountPrice
            // 
            tSKUDiscountPrice.Location = new System.Drawing.Point(98, 271);
            tSKUDiscountPrice.Name = "tSKUDiscountPrice";
            tSKUDiscountPrice.PlaceholderText = "0";
            tSKUDiscountPrice.Size = new System.Drawing.Size(75, 23);
            tSKUDiscountPrice.TabIndex = 28;
            tSKUDiscountPrice.TextChanged += tSKU_Changed;
            // 
            // tSKUPrice
            // 
            tSKUPrice.Location = new System.Drawing.Point(98, 227);
            tSKUPrice.Name = "tSKUPrice";
            tSKUPrice.PlaceholderText = "1000";
            tSKUPrice.Size = new System.Drawing.Size(75, 23);
            tSKUPrice.TabIndex = 27;
            tSKUPrice.TextChanged += tSKU_Changed;
            // 
            // tSKUEventType
            // 
            tSKUEventType.Location = new System.Drawing.Point(98, 160);
            tSKUEventType.Name = "tSKUEventType";
            tSKUEventType.PlaceholderText = "0";
            tSKUEventType.Size = new System.Drawing.Size(63, 23);
            tSKUEventType.TabIndex = 26;
            tSKUEventType.TextChanged += tSKU_Changed;
            // 
            // tSKUSelectType
            // 
            tSKUSelectType.Location = new System.Drawing.Point(98, 131);
            tSKUSelectType.Name = "tSKUSelectType";
            tSKUSelectType.PlaceholderText = "0";
            tSKUSelectType.Size = new System.Drawing.Size(63, 23);
            tSKUSelectType.TabIndex = 25;
            tSKUSelectType.TextChanged += tSKU_Changed;
            // 
            // tSKUItemCount
            // 
            tSKUItemCount.Location = new System.Drawing.Point(268, 93);
            tSKUItemCount.Name = "tSKUItemCount";
            tSKUItemCount.PlaceholderText = "1";
            tSKUItemCount.Size = new System.Drawing.Size(59, 23);
            tSKUItemCount.TabIndex = 24;
            tSKUItemCount.TextChanged += tSKU_Changed;
            // 
            // tSKUItemId
            // 
            tSKUItemId.Location = new System.Drawing.Point(98, 93);
            tSKUItemId.Name = "tSKUItemId";
            tSKUItemId.PlaceholderText = "item template id";
            tSKUItemId.Size = new System.Drawing.Size(119, 23);
            tSKUItemId.TabIndex = 23;
            tSKUItemId.TextChanged += tSKU_Changed;
            // 
            // tSKUShopEntryPosition
            // 
            tSKUShopEntryPosition.Location = new System.Drawing.Point(268, 36);
            tSKUShopEntryPosition.Name = "tSKUShopEntryPosition";
            tSKUShopEntryPosition.PlaceholderText = "0";
            tSKUShopEntryPosition.Size = new System.Drawing.Size(59, 23);
            tSKUShopEntryPosition.TabIndex = 22;
            tSKUShopEntryPosition.TextChanged += tSKU_Changed;
            // 
            // cbSKUShopEntryId
            // 
            cbSKUShopEntryId.FormattingEnabled = true;
            cbSKUShopEntryId.Location = new System.Drawing.Point(98, 36);
            cbSKUShopEntryId.Name = "cbSKUShopEntryId";
            cbSKUShopEntryId.Size = new System.Drawing.Size(132, 24);
            cbSKUShopEntryId.TabIndex = 21;
            cbSKUShopEntryId.TextChanged += tSKU_Changed;
            // 
            // tSKUSKU
            // 
            tSKUSKU.Location = new System.Drawing.Point(98, 7);
            tSKUSKU.Name = "tSKUSKU";
            tSKUSKU.PlaceholderText = "<new sku>";
            tSKUSKU.ReadOnly = true;
            tSKUSKU.Size = new System.Drawing.Size(180, 23);
            tSKUSKU.TabIndex = 20;
            tSKUSKU.TextChanged += tSKU_Changed;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(3, 274);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(83, 16);
            label10.TabIndex = 17;
            label10.Text = "Discount Price";
            // 
            // rbSKUCurrencyCoins
            // 
            rbSKUCurrencyCoins.AutoSize = true;
            rbSKUCurrencyCoins.Location = new System.Drawing.Point(246, 251);
            rbSKUCurrencyCoins.Name = "rbSKUCurrencyCoins";
            rbSKUCurrencyCoins.Size = new System.Drawing.Size(54, 20);
            rbSKUCurrencyCoins.TabIndex = 16;
            rbSKUCurrencyCoins.Tag = "3";
            rbSKUCurrencyCoins.Text = "Coins";
            rbSKUCurrencyCoins.UseVisualStyleBackColor = true;
            rbSKUCurrencyCoins.CheckedChanged += tSKU_Changed;
            // 
            // rbSKUCurrencyLoyalty
            // 
            rbSKUCurrencyLoyalty.AutoSize = true;
            rbSKUCurrencyLoyalty.Location = new System.Drawing.Point(179, 251);
            rbSKUCurrencyLoyalty.Name = "rbSKUCurrencyLoyalty";
            rbSKUCurrencyLoyalty.Size = new System.Drawing.Size(63, 20);
            rbSKUCurrencyLoyalty.TabIndex = 15;
            rbSKUCurrencyLoyalty.Tag = "2";
            rbSKUCurrencyLoyalty.Text = "Loyalty";
            rbSKUCurrencyLoyalty.UseVisualStyleBackColor = true;
            rbSKUCurrencyLoyalty.CheckedChanged += tSKU_Changed;
            // 
            // rbSKUCurrencyAAPoints
            // 
            rbSKUCurrencyAAPoints.AutoSize = true;
            rbSKUCurrencyAAPoints.Location = new System.Drawing.Point(246, 228);
            rbSKUCurrencyAAPoints.Name = "rbSKUCurrencyAAPoints";
            rbSKUCurrencyAAPoints.Size = new System.Drawing.Size(77, 20);
            rbSKUCurrencyAAPoints.TabIndex = 14;
            rbSKUCurrencyAAPoints.Tag = "1";
            rbSKUCurrencyAAPoints.Text = "AA Points";
            rbSKUCurrencyAAPoints.UseVisualStyleBackColor = true;
            rbSKUCurrencyAAPoints.CheckedChanged += tSKU_Changed;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(3, 230);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(33, 16);
            label9.TabIndex = 13;
            label9.Text = "Price";
            // 
            // rbSKUCurrencyCredits
            // 
            rbSKUCurrencyCredits.AutoSize = true;
            rbSKUCurrencyCredits.Checked = true;
            rbSKUCurrencyCredits.Location = new System.Drawing.Point(179, 228);
            rbSKUCurrencyCredits.Name = "rbSKUCurrencyCredits";
            rbSKUCurrencyCredits.Size = new System.Drawing.Size(61, 20);
            rbSKUCurrencyCredits.TabIndex = 12;
            rbSKUCurrencyCredits.TabStop = true;
            rbSKUCurrencyCredits.Tag = "0";
            rbSKUCurrencyCredits.Text = "Credits";
            rbSKUCurrencyCredits.UseVisualStyleBackColor = true;
            rbSKUCurrencyCredits.CheckedChanged += tSKU_Changed;
            // 
            // cbSKUEventHasEnd
            // 
            cbSKUEventHasEnd.AutoSize = true;
            cbSKUEventHasEnd.Location = new System.Drawing.Point(167, 162);
            cbSKUEventHasEnd.Name = "cbSKUEventHasEnd";
            cbSKUEventHasEnd.Size = new System.Drawing.Size(125, 20);
            cbSKUEventHasEnd.TabIndex = 11;
            cbSKUEventHasEnd.Text = "Event has end date";
            cbSKUEventHasEnd.UseVisualStyleBackColor = true;
            cbSKUEventHasEnd.CheckedChanged += tSKU_Changed;
            // 
            // dtpSKUEventEndDate
            // 
            dtpSKUEventEndDate.Location = new System.Drawing.Point(98, 189);
            dtpSKUEventEndDate.Name = "dtpSKUEventEndDate";
            dtpSKUEventEndDate.Size = new System.Drawing.Size(200, 23);
            dtpSKUEventEndDate.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(3, 163);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(63, 16);
            label8.TabIndex = 8;
            label8.Text = "Event Type";
            // 
            // cbSKUIsDefault
            // 
            cbSKUIsDefault.AutoSize = true;
            cbSKUIsDefault.Location = new System.Drawing.Point(98, 66);
            cbSKUIsDefault.Name = "cbSKUIsDefault";
            cbSKUIsDefault.Size = new System.Drawing.Size(75, 20);
            cbSKUIsDefault.TabIndex = 7;
            cbSKUIsDefault.Text = "Is Default";
            cbSKUIsDefault.UseVisualStyleBackColor = true;
            cbSKUIsDefault.CheckedChanged += tSKU_Changed;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(3, 134);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(65, 16);
            label7.TabIndex = 6;
            label7.Text = "Select Type";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(236, 39);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(26, 16);
            label6.TabIndex = 5;
            label6.Text = "Pos";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(3, 39);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(77, 16);
            label5.TabIndex = 4;
            label5.Text = "Shop Entry Id";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(223, 96);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(39, 16);
            label4.TabIndex = 3;
            label4.Text = "Count";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 96);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 16);
            label3.TabIndex = 2;
            label3.Text = "Item ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(28, 16);
            label1.TabIndex = 0;
            label1.Text = "SKU";
            // 
            // tpShopEntries
            // 
            tpShopEntries.Controls.Add(splitContainer2);
            tpShopEntries.Location = new System.Drawing.Point(4, 25);
            tpShopEntries.Name = "tpShopEntries";
            tpShopEntries.Padding = new System.Windows.Forms.Padding(3);
            tpShopEntries.Size = new System.Drawing.Size(702, 407);
            tpShopEntries.TabIndex = 1;
            tpShopEntries.Text = "Shop Items";
            tpShopEntries.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(tvShopItems);
            splitContainer2.Size = new System.Drawing.Size(696, 401);
            splitContainer2.SplitterDistance = 300;
            splitContainer2.TabIndex = 0;
            // 
            // tvShopItems
            // 
            tvShopItems.Dock = System.Windows.Forms.DockStyle.Fill;
            tvShopItems.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            tvShopItems.Location = new System.Drawing.Point(0, 0);
            tvShopItems.Name = "tvShopItems";
            tvShopItems.Size = new System.Drawing.Size(300, 401);
            tvShopItems.TabIndex = 0;
            tvShopItems.DrawNode += tvShopItems_DrawNode;
            // 
            // tpShopTabs
            // 
            tpShopTabs.Location = new System.Drawing.Point(4, 25);
            tpShopTabs.Name = "tpShopTabs";
            tpShopTabs.Padding = new System.Windows.Forms.Padding(3);
            tpShopTabs.Size = new System.Drawing.Size(702, 407);
            tpShopTabs.TabIndex = 2;
            tpShopTabs.Text = "Shop Tab Settings";
            tpShopTabs.UseVisualStyleBackColor = true;
            // 
            // ICSForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(710, 436);
            Controls.Add(tcICS);
            Name = "ICSForm";
            Text = "ICSForm";
            Load += ICSForm_Load;
            tcICS.ResumeLayout(false);
            tpInfo.ResumeLayout(false);
            tpInfo.PerformLayout();
            tpSKUs.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tpShopEntries.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tcICS;
        private System.Windows.Forms.TabPage tpSKUs;
        private System.Windows.Forms.TabPage tpShopEntries;
        private System.Windows.Forms.ListView lvSKUs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpShopTabs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbSKUCurrencyCoins;
        private System.Windows.Forms.RadioButton rbSKUCurrencyLoyalty;
        private System.Windows.Forms.RadioButton rbSKUCurrencyAAPoints;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbSKUCurrencyCredits;
        private System.Windows.Forms.CheckBox cbSKUEventHasEnd;
        private System.Windows.Forms.DateTimePicker dtpSKUEventEndDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbSKUIsDefault;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tSKUSKU;
        private System.Windows.Forms.ComboBox cbSKUShopEntryId;
        private System.Windows.Forms.TextBox tSKUItemCount;
        private System.Windows.Forms.TextBox tSKUItemId;
        private System.Windows.Forms.TextBox tSKUShopEntryPosition;
        private System.Windows.Forms.TextBox tSKUBonusItemCount;
        private System.Windows.Forms.TextBox tSKUBonusItemId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tSKUDiscountPrice;
        private System.Windows.Forms.TextBox tSKUPrice;
        private System.Windows.Forms.TextBox tSKUEventType;
        private System.Windows.Forms.TextBox tSKUSelectType;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvShopItems;
        private System.Windows.Forms.Button btnSKUNew;
        private System.Windows.Forms.Button btnSKUUpdate;
        private System.Windows.Forms.Label lSKUDiscountCalculation;
        private System.Windows.Forms.Button btnSKUGetNewId;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TextBox textBox1;
    }
}
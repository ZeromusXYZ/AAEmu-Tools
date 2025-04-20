namespace AAEmu.DBEditor.tools.ahbot
{
    partial class AhBotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AhBotForm));
            label1 = new System.Windows.Forms.Label();
            lAhBotName = new System.Windows.Forms.Label();
            btnPickAhCharacter = new System.Windows.Forms.Button();
            lAhBotAccount = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            cbServers = new System.Windows.Forms.ComboBox();
            btnConnect = new System.Windows.Forms.Button();
            tcAhBot = new System.Windows.Forms.TabControl();
            tpSettings = new System.Windows.Forms.TabPage();
            AhBotTextBoxInfo = new System.Windows.Forms.TextBox();
            btnCleanMails = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            btnLoadConfig = new System.Windows.Forms.Button();
            tpAhList = new System.Windows.Forms.TabPage();
            gbItemEntrySettings = new System.Windows.Forms.GroupBox();
            lItemIcon = new System.Windows.Forms.Label();
            lListingInfo = new System.Windows.Forms.Label();
            lStackMax = new System.Windows.Forms.Label();
            tComment = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            lStartBidPreview = new System.Windows.Forms.Label();
            lBuyOutPreview = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lItemName = new System.Windows.Forms.Label();
            lItemId = new System.Windows.Forms.Label();
            lGrade = new System.Windows.Forms.Label();
            cbGrade = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            btnRemoveItem = new System.Windows.Forms.Button();
            tSaleQuantity = new System.Windows.Forms.TextBox();
            btnUpdateAhItem = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            tListedCount = new System.Windows.Forms.TextBox();
            tBuyOutPrice = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            tStartBid = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            lbAhLiveList = new System.Windows.Forms.ListBox();
            label9 = new System.Windows.Forms.Label();
            btnQueryServerAH = new System.Windows.Forms.Button();
            lbAhList = new System.Windows.Forms.ListBox();
            tvAhList = new System.Windows.Forms.TreeView();
            tpLogs = new System.Windows.Forms.TabPage();
            tLog = new System.Windows.Forms.TextBox();
            bgwAhCheckLoop = new System.ComponentModel.BackgroundWorker();
            panel1 = new System.Windows.Forms.Panel();
            BtnClearLog = new System.Windows.Forms.Button();
            tcAhBot.SuspendLayout();
            tpSettings.SuspendLayout();
            tpAhList.SuspendLayout();
            gbItemEntrySettings.SuspendLayout();
            tpLogs.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(142, 15);
            label1.TabIndex = 1;
            label1.Text = "Character the Bot runs on";
            // 
            // lAhBotName
            // 
            lAhBotName.AutoSize = true;
            lAhBotName.Location = new System.Drawing.Point(42, 29);
            lAhBotName.Name = "lAhBotName";
            lAhBotName.Size = new System.Drawing.Size(135, 15);
            lAhBotName.TabIndex = 2;
            lAhBotName.Text = "<no character selected>";
            // 
            // btnPickAhCharacter
            // 
            btnPickAhCharacter.Location = new System.Drawing.Point(8, 26);
            btnPickAhCharacter.Name = "btnPickAhCharacter";
            btnPickAhCharacter.Size = new System.Drawing.Size(28, 22);
            btnPickAhCharacter.TabIndex = 3;
            btnPickAhCharacter.Text = "...";
            btnPickAhCharacter.UseVisualStyleBackColor = true;
            btnPickAhCharacter.Click += btnPickAhCharacter_Click;
            // 
            // lAhBotAccount
            // 
            lAhBotAccount.AutoSize = true;
            lAhBotAccount.Location = new System.Drawing.Point(222, 29);
            lAhBotAccount.Name = "lAhBotAccount";
            lAhBotAccount.Size = new System.Drawing.Size(129, 15);
            lAhBotAccount.TabIndex = 4;
            lAhBotAccount.Text = "<no account selected>";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(222, 8);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(152, 15);
            label2.TabIndex = 5;
            label2.Text = "Account the bot will run on";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 61);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 15);
            label3.TabIndex = 6;
            label3.Text = "Server";
            // 
            // cbServers
            // 
            cbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbServers.FormattingEnabled = true;
            cbServers.Location = new System.Drawing.Point(19, 79);
            cbServers.Name = "cbServers";
            cbServers.Size = new System.Drawing.Size(158, 23);
            cbServers.TabIndex = 7;
            cbServers.SelectedIndexChanged += cbServers_SelectedIndexChanged;
            // 
            // btnConnect
            // 
            btnConnect.Location = new System.Drawing.Point(423, 8);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new System.Drawing.Size(275, 93);
            btnConnect.TabIndex = 8;
            btnConnect.Text = "Start";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // tcAhBot
            // 
            tcAhBot.Controls.Add(tpSettings);
            tcAhBot.Controls.Add(tpAhList);
            tcAhBot.Controls.Add(tpLogs);
            tcAhBot.Dock = System.Windows.Forms.DockStyle.Fill;
            tcAhBot.Location = new System.Drawing.Point(0, 0);
            tcAhBot.Name = "tcAhBot";
            tcAhBot.SelectedIndex = 0;
            tcAhBot.Size = new System.Drawing.Size(800, 422);
            tcAhBot.TabIndex = 10;
            // 
            // tpSettings
            // 
            tpSettings.Controls.Add(AhBotTextBoxInfo);
            tpSettings.Controls.Add(btnCleanMails);
            tpSettings.Controls.Add(btnSave);
            tpSettings.Controls.Add(btnLoadConfig);
            tpSettings.Controls.Add(label1);
            tpSettings.Controls.Add(btnConnect);
            tpSettings.Controls.Add(btnPickAhCharacter);
            tpSettings.Controls.Add(lAhBotName);
            tpSettings.Controls.Add(label2);
            tpSettings.Controls.Add(cbServers);
            tpSettings.Controls.Add(lAhBotAccount);
            tpSettings.Controls.Add(label3);
            tpSettings.Location = new System.Drawing.Point(4, 24);
            tpSettings.Name = "tpSettings";
            tpSettings.Size = new System.Drawing.Size(792, 394);
            tpSettings.TabIndex = 2;
            tpSettings.Text = "Settings";
            tpSettings.UseVisualStyleBackColor = true;
            // 
            // AhBotTextBoxInfo
            // 
            AhBotTextBoxInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            AhBotTextBoxInfo.Location = new System.Drawing.Point(8, 168);
            AhBotTextBoxInfo.Multiline = true;
            AhBotTextBoxInfo.Name = "AhBotTextBoxInfo";
            AhBotTextBoxInfo.ReadOnly = true;
            AhBotTextBoxInfo.Size = new System.Drawing.Size(776, 220);
            AhBotTextBoxInfo.TabIndex = 13;
            AhBotTextBoxInfo.Text = resources.GetString("AhBotTextBoxInfo.Text");
            // 
            // btnCleanMails
            // 
            btnCleanMails.Location = new System.Drawing.Point(358, 109);
            btnCleanMails.Name = "btnCleanMails";
            btnCleanMails.Size = new System.Drawing.Size(169, 22);
            btnCleanMails.TabIndex = 12;
            btnCleanMails.Text = "Check Mails";
            btnCleanMails.UseVisualStyleBackColor = true;
            btnCleanMails.Visible = false;
            btnCleanMails.Click += btnCleanMails_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(183, 109);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(169, 22);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save Configuration";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Visible = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new System.Drawing.Point(8, 109);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new System.Drawing.Size(169, 22);
            btnLoadConfig.TabIndex = 10;
            btnLoadConfig.Text = "Load Configuration";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Visible = false;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // tpAhList
            // 
            tpAhList.Controls.Add(gbItemEntrySettings);
            tpAhList.Controls.Add(label10);
            tpAhList.Controls.Add(lbAhLiveList);
            tpAhList.Controls.Add(label9);
            tpAhList.Controls.Add(btnQueryServerAH);
            tpAhList.Controls.Add(lbAhList);
            tpAhList.Controls.Add(tvAhList);
            tpAhList.Location = new System.Drawing.Point(4, 24);
            tpAhList.Name = "tpAhList";
            tpAhList.Padding = new System.Windows.Forms.Padding(3);
            tpAhList.Size = new System.Drawing.Size(792, 394);
            tpAhList.TabIndex = 1;
            tpAhList.Text = "AH";
            tpAhList.UseVisualStyleBackColor = true;
            // 
            // gbItemEntrySettings
            // 
            gbItemEntrySettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            gbItemEntrySettings.Controls.Add(lItemIcon);
            gbItemEntrySettings.Controls.Add(lListingInfo);
            gbItemEntrySettings.Controls.Add(lStackMax);
            gbItemEntrySettings.Controls.Add(tComment);
            gbItemEntrySettings.Controls.Add(label11);
            gbItemEntrySettings.Controls.Add(lStartBidPreview);
            gbItemEntrySettings.Controls.Add(lBuyOutPreview);
            gbItemEntrySettings.Controls.Add(label4);
            gbItemEntrySettings.Controls.Add(lItemName);
            gbItemEntrySettings.Controls.Add(lItemId);
            gbItemEntrySettings.Controls.Add(lGrade);
            gbItemEntrySettings.Controls.Add(cbGrade);
            gbItemEntrySettings.Controls.Add(label5);
            gbItemEntrySettings.Controls.Add(btnRemoveItem);
            gbItemEntrySettings.Controls.Add(tSaleQuantity);
            gbItemEntrySettings.Controls.Add(btnUpdateAhItem);
            gbItemEntrySettings.Controls.Add(label6);
            gbItemEntrySettings.Controls.Add(tListedCount);
            gbItemEntrySettings.Controls.Add(tBuyOutPrice);
            gbItemEntrySettings.Controls.Add(label8);
            gbItemEntrySettings.Controls.Add(label7);
            gbItemEntrySettings.Controls.Add(tStartBid);
            gbItemEntrySettings.Location = new System.Drawing.Point(442, 8);
            gbItemEntrySettings.Name = "gbItemEntrySettings";
            gbItemEntrySettings.Size = new System.Drawing.Size(336, 378);
            gbItemEntrySettings.TabIndex = 21;
            gbItemEntrySettings.TabStop = false;
            gbItemEntrySettings.Text = "Item listing settings";
            // 
            // lItemIcon
            // 
            lItemIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lItemIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lItemIcon.Location = new System.Drawing.Point(251, 18);
            lItemIcon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lItemIcon.Name = "lItemIcon";
            lItemIcon.Size = new System.Drawing.Size(78, 73);
            lItemIcon.TabIndex = 29;
            lItemIcon.Text = "???";
            lItemIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lListingInfo
            // 
            lListingInfo.AutoSize = true;
            lListingInfo.Location = new System.Drawing.Point(6, 242);
            lListingInfo.Name = "lListingInfo";
            lListingInfo.Size = new System.Drawing.Size(12, 15);
            lListingInfo.TabIndex = 21;
            lListingInfo.Text = "?";
            // 
            // lStackMax
            // 
            lStackMax.AutoSize = true;
            lStackMax.Location = new System.Drawing.Point(137, 124);
            lStackMax.Name = "lStackMax";
            lStackMax.Size = new System.Drawing.Size(21, 15);
            lStackMax.TabIndex = 20;
            lStackMax.Text = "/ 1";
            // 
            // tComment
            // 
            tComment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tComment.Location = new System.Drawing.Point(15, 277);
            tComment.Name = "tComment";
            tComment.Size = new System.Drawing.Size(315, 23);
            tComment.TabIndex = 19;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(6, 259);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(61, 15);
            label11.TabIndex = 18;
            label11.Text = "Comment";
            // 
            // lStartBidPreview
            // 
            lStartBidPreview.AutoSize = true;
            lStartBidPreview.Location = new System.Drawing.Point(128, 219);
            lStartBidPreview.Name = "lStartBidPreview";
            lStartBidPreview.Size = new System.Drawing.Size(19, 15);
            lStartBidPreview.TabIndex = 17;
            lStartBidPreview.Text = "0c";
            // 
            // lBuyOutPreview
            // 
            lBuyOutPreview.AutoSize = true;
            lBuyOutPreview.Location = new System.Drawing.Point(128, 176);
            lBuyOutPreview.Name = "lBuyOutPreview";
            lBuyOutPreview.Size = new System.Drawing.Size(19, 15);
            lBuyOutPreview.TabIndex = 16;
            lBuyOutPreview.Text = "0c";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 18);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(31, 15);
            label4.TabIndex = 1;
            label4.Text = "Item";
            // 
            // lItemName
            // 
            lItemName.AutoSize = true;
            lItemName.Location = new System.Drawing.Point(15, 33);
            lItemName.Name = "lItemName";
            lItemName.Size = new System.Drawing.Size(80, 15);
            lItemName.TabIndex = 2;
            lItemName.Text = "<item name>";
            // 
            // lItemId
            // 
            lItemId.AutoSize = true;
            lItemId.Location = new System.Drawing.Point(42, 18);
            lItemId.Name = "lItemId";
            lItemId.Size = new System.Drawing.Size(60, 15);
            lItemId.TabIndex = 3;
            lItemId.Text = "<item id>";
            lItemId.TextChanged += lItemId_TextChanged;
            // 
            // lGrade
            // 
            lGrade.AutoSize = true;
            lGrade.Location = new System.Drawing.Point(6, 51);
            lGrade.Name = "lGrade";
            lGrade.Size = new System.Drawing.Size(38, 15);
            lGrade.TabIndex = 4;
            lGrade.Text = "Grade";
            // 
            // cbGrade
            // 
            cbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbGrade.FormattingEnabled = true;
            cbGrade.Location = new System.Drawing.Point(15, 68);
            cbGrade.Name = "cbGrade";
            cbGrade.Size = new System.Drawing.Size(229, 23);
            cbGrade.TabIndex = 5;
            cbGrade.SelectedIndexChanged += cbGrade_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 103);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(126, 15);
            label5.TabIndex = 6;
            label5.Text = "Item amount per entry";
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnRemoveItem.Location = new System.Drawing.Point(128, 351);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new System.Drawing.Size(116, 22);
            btnRemoveItem.TabIndex = 15;
            btnRemoveItem.Text = "Remove";
            btnRemoveItem.UseVisualStyleBackColor = true;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // tSaleQuantity
            // 
            tSaleQuantity.Location = new System.Drawing.Point(15, 121);
            tSaleQuantity.Name = "tSaleQuantity";
            tSaleQuantity.Size = new System.Drawing.Size(107, 23);
            tSaleQuantity.TabIndex = 7;
            tSaleQuantity.Text = "1";
            // 
            // btnUpdateAhItem
            // 
            btnUpdateAhItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnUpdateAhItem.Location = new System.Drawing.Point(6, 351);
            btnUpdateAhItem.Name = "btnUpdateAhItem";
            btnUpdateAhItem.Size = new System.Drawing.Size(116, 22);
            btnUpdateAhItem.TabIndex = 14;
            btnUpdateAhItem.Text = "Update/Add";
            btnUpdateAhItem.UseVisualStyleBackColor = true;
            btnUpdateAhItem.Click += btnUpdateAhItem_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 156);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(193, 15);
            label6.TabIndex = 8;
            label6.Text = "Sale price (buy-out, zero to disable)";
            // 
            // tListedCount
            // 
            tListedCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            tListedCount.Location = new System.Drawing.Point(15, 323);
            tListedCount.Name = "tListedCount";
            tListedCount.Size = new System.Drawing.Size(107, 23);
            tListedCount.TabIndex = 13;
            tListedCount.Text = "1";
            // 
            // tBuyOutPrice
            // 
            tBuyOutPrice.Location = new System.Drawing.Point(15, 173);
            tBuyOutPrice.Name = "tBuyOutPrice";
            tBuyOutPrice.Size = new System.Drawing.Size(107, 23);
            tBuyOutPrice.TabIndex = 9;
            tBuyOutPrice.Text = "10000";
            tBuyOutPrice.TextChanged += tBuyOutPrice_TextChanged;
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(6, 306);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(259, 15);
            label8.TabIndex = 12;
            label8.Text = "Number of times to keep this entry listed on AH";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 199);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(68, 15);
            label7.TabIndex = 10;
            label7.Text = "Starting Bid";
            // 
            // tStartBid
            // 
            tStartBid.Location = new System.Drawing.Point(15, 217);
            tStartBid.Name = "tStartBid";
            tStartBid.Size = new System.Drawing.Size(107, 23);
            tStartBid.TabIndex = 11;
            tStartBid.Text = "0";
            tStartBid.TextChanged += tStartBid_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(251, 215);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(93, 15);
            label10.TabIndex = 20;
            label10.Text = "Currently on AH";
            // 
            // lbAhLiveList
            // 
            lbAhLiveList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbAhLiveList.FormattingEnabled = true;
            lbAhLiveList.Location = new System.Drawing.Point(251, 232);
            lbAhLiveList.Name = "lbAhLiveList";
            lbAhLiveList.Size = new System.Drawing.Size(185, 154);
            lbAhLiveList.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(255, 13);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(90, 15);
            label9.TabIndex = 18;
            label9.Text = "Bot List settings";
            // 
            // btnQueryServerAH
            // 
            btnQueryServerAH.Location = new System.Drawing.Point(6, 8);
            btnQueryServerAH.Name = "btnQueryServerAH";
            btnQueryServerAH.Size = new System.Drawing.Size(239, 22);
            btnQueryServerAH.TabIndex = 17;
            btnQueryServerAH.Text = "Update live stats";
            btnQueryServerAH.UseVisualStyleBackColor = true;
            btnQueryServerAH.Click += btnQueryServerAH_Click;
            // 
            // lbAhList
            // 
            lbAhList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbAhList.FormattingEnabled = true;
            lbAhList.Location = new System.Drawing.Point(251, 36);
            lbAhList.Name = "lbAhList";
            lbAhList.Size = new System.Drawing.Size(185, 169);
            lbAhList.TabIndex = 16;
            lbAhList.SelectedIndexChanged += lbAhList_SelectedIndexChanged;
            // 
            // tvAhList
            // 
            tvAhList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            tvAhList.Location = new System.Drawing.Point(6, 36);
            tvAhList.Name = "tvAhList";
            tvAhList.Size = new System.Drawing.Size(239, 352);
            tvAhList.TabIndex = 0;
            tvAhList.AfterSelect += tvAhList_AfterSelect;
            // 
            // tpLogs
            // 
            tpLogs.Controls.Add(panel1);
            tpLogs.Controls.Add(tLog);
            tpLogs.Location = new System.Drawing.Point(4, 24);
            tpLogs.Name = "tpLogs";
            tpLogs.Padding = new System.Windows.Forms.Padding(3);
            tpLogs.Size = new System.Drawing.Size(792, 394);
            tpLogs.TabIndex = 0;
            tpLogs.Text = "Logs";
            tpLogs.UseVisualStyleBackColor = true;
            // 
            // tLog
            // 
            tLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tLog.Font = new System.Drawing.Font("Consolas", 9F);
            tLog.Location = new System.Drawing.Point(3, 3);
            tLog.Multiline = true;
            tLog.Name = "tLog";
            tLog.Size = new System.Drawing.Size(781, 340);
            tLog.TabIndex = 0;
            // 
            // bgwAhCheckLoop
            // 
            bgwAhCheckLoop.WorkerReportsProgress = true;
            bgwAhCheckLoop.WorkerSupportsCancellation = true;
            bgwAhCheckLoop.DoWork += bgwAhCheckLoop_DoWork;
            bgwAhCheckLoop.ProgressChanged += bgwAhCheckLoop_ProgressChanged;
            bgwAhCheckLoop.RunWorkerCompleted += bgwAhCheckLoop_RunWorkerCompleted;
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnClearLog);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(3, 349);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(786, 42);
            panel1.TabIndex = 1;
            // 
            // BtnClearLog
            // 
            BtnClearLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnClearLog.Location = new System.Drawing.Point(5, 14);
            BtnClearLog.Name = "BtnClearLog";
            BtnClearLog.Size = new System.Drawing.Size(114, 23);
            BtnClearLog.TabIndex = 0;
            BtnClearLog.Text = "Clear";
            BtnClearLog.UseVisualStyleBackColor = true;
            BtnClearLog.Click += BtnClearLog_Click;
            // 
            // AhBotForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 422);
            Controls.Add(tcAhBot);
            Name = "AhBotForm";
            Text = "Auction House Bot";
            FormClosing += AhBotForm_FormClosing;
            FormClosed += AhBotForm_FormClosed;
            Load += AhBotForm_Load;
            tcAhBot.ResumeLayout(false);
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            tpAhList.ResumeLayout(false);
            tpAhList.PerformLayout();
            gbItemEntrySettings.ResumeLayout(false);
            gbItemEntrySettings.PerformLayout();
            tpLogs.ResumeLayout(false);
            tpLogs.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lAhBotName;
        private System.Windows.Forms.Button btnPickAhCharacter;
        private System.Windows.Forms.Label lAhBotAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tcAhBot;
        private System.Windows.Forms.TabPage tpAhList;
        private System.Windows.Forms.TreeView tvAhList;
        private System.Windows.Forms.TabPage tpLogs;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.ComboBox cbGrade;
        private System.Windows.Forms.Label lGrade;
        private System.Windows.Forms.Label lItemId;
        private System.Windows.Forms.Label lItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tStartBid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tBuyOutPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tSaleQuantity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tListedCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnUpdateAhItem;
        private System.Windows.Forms.ListBox lbAhList;
        private System.Windows.Forms.Button btnQueryServerAH;
        private System.ComponentModel.BackgroundWorker bgwAhCheckLoop;
        private System.Windows.Forms.TextBox tLog;
        private System.Windows.Forms.Button btnCleanMails;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lbAhLiveList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbItemEntrySettings;
        private System.Windows.Forms.Label lBuyOutPreview;
        private System.Windows.Forms.Label lStartBidPreview;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AhBotTextBoxInfo;
        private System.Windows.Forms.Label lStackMax;
        private System.Windows.Forms.Label lListingInfo;
        private System.Windows.Forms.Label lItemIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnClearLog;
    }
}

namespace AAEmu.DBEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MM = new System.Windows.Forms.MenuStrip();
            MMFile = new System.Windows.Forms.ToolStripMenuItem();
            MMFileOpenServer = new System.Windows.Forms.ToolStripMenuItem();
            MMFileOpenClient = new System.Windows.Forms.ToolStripMenuItem();
            MMFileOpenMySQL = new System.Windows.Forms.ToolStripMenuItem();
            MMFileS1 = new System.Windows.Forms.ToolStripSeparator();
            MMFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            MMFileS3 = new System.Windows.Forms.ToolStripSeparator();
            MMFileReload = new System.Windows.Forms.ToolStripMenuItem();
            MMFileS2 = new System.Windows.Forms.ToolStripSeparator();
            MMFileExit = new System.Windows.Forms.ToolStripMenuItem();
            MMClient = new System.Windows.Forms.ToolStripMenuItem();
            MMClientMap = new System.Windows.Forms.ToolStripMenuItem();
            MMClientS1 = new System.Windows.Forms.ToolStripSeparator();
            MMClientItems = new System.Windows.Forms.ToolStripMenuItem();
            MMClientDoodads = new System.Windows.Forms.ToolStripMenuItem();
            MMClientNPCs = new System.Windows.Forms.ToolStripMenuItem();
            MMClientSkills = new System.Windows.Forms.ToolStripMenuItem();
            MMClientZones = new System.Windows.Forms.ToolStripMenuItem();
            MMClientBuffs = new System.Windows.Forms.ToolStripMenuItem();
            MMClientFactions = new System.Windows.Forms.ToolStripMenuItem();
            MMClientQuests = new System.Windows.Forms.ToolStripMenuItem();
            MMClientLoot = new System.Windows.Forms.ToolStripMenuItem();
            MMServer = new System.Windows.Forms.ToolStripMenuItem();
            MMServerAccounts = new System.Windows.Forms.ToolStripMenuItem();
            MMServerCharacters = new System.Windows.Forms.ToolStripMenuItem();
            MMServerGuilds = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            MMServerICS = new System.Windows.Forms.ToolStripMenuItem();
            MMVersion = new System.Windows.Forms.ToolStripMenuItem();
            MMTools = new System.Windows.Forms.ToolStripMenuItem();
            MMToolsAhBot = new System.Windows.Forms.ToolStripMenuItem();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lMySQLServer = new System.Windows.Forms.Label();
            lClientPak = new System.Windows.Forms.Label();
            lServerDB = new System.Windows.Forms.Label();
            ofdServerDB = new System.Windows.Forms.OpenFileDialog();
            ofdClientPak = new System.Windows.Forms.OpenFileDialog();
            TestPanel = new System.Windows.Forms.Panel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            sbL1 = new System.Windows.Forms.ToolStripStatusLabel();
            gbLocale = new System.Windows.Forms.GroupBox();
            rbLocaleFr = new System.Windows.Forms.RadioButton();
            rbLocaleDe = new System.Windows.Forms.RadioButton();
            rbLocaleJa = new System.Windows.Forms.RadioButton();
            rbLocaleZhTw = new System.Windows.Forms.RadioButton();
            rbLocaleZhCn = new System.Windows.Forms.RadioButton();
            rbLocaleRu = new System.Windows.Forms.RadioButton();
            rbLocaleKo = new System.Windows.Forms.RadioButton();
            rbLocaleEnUs = new System.Windows.Forms.RadioButton();
            MM.SuspendLayout();
            statusStrip1.SuspendLayout();
            gbLocale.SuspendLayout();
            SuspendLayout();
            // 
            // MM
            // 
            MM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MMFile, MMClient, MMServer, MMVersion, MMTools });
            MM.Location = new System.Drawing.Point(0, 0);
            MM.Name = "MM";
            MM.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            MM.Size = new System.Drawing.Size(856, 24);
            MM.TabIndex = 0;
            MM.Text = "menuStrip1";
            // 
            // MMFile
            // 
            MMFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MMFileOpenServer, MMFileOpenClient, MMFileOpenMySQL, MMFileS1, MMFileSettings, MMFileS3, MMFileReload, MMFileS2, MMFileExit });
            MMFile.Name = "MMFile";
            MMFile.Size = new System.Drawing.Size(37, 20);
            MMFile.Text = "&File";
            // 
            // MMFileOpenServer
            // 
            MMFileOpenServer.Name = "MMFileOpenServer";
            MMFileOpenServer.Size = new System.Drawing.Size(181, 22);
            MMFileOpenServer.Text = "Open Server DB ...";
            MMFileOpenServer.Click += MMFileOpenServer_Click;
            // 
            // MMFileOpenClient
            // 
            MMFileOpenClient.Name = "MMFileOpenClient";
            MMFileOpenClient.Size = new System.Drawing.Size(181, 22);
            MMFileOpenClient.Text = "Open Game Client ...";
            MMFileOpenClient.Click += MMFileOpenClient_Click;
            // 
            // MMFileOpenMySQL
            // 
            MMFileOpenMySQL.Name = "MMFileOpenMySQL";
            MMFileOpenMySQL.Size = new System.Drawing.Size(181, 22);
            MMFileOpenMySQL.Text = "Open MySQL DB ...";
            MMFileOpenMySQL.Click += MMFileOpenMySQL_Click;
            // 
            // MMFileS1
            // 
            MMFileS1.Name = "MMFileS1";
            MMFileS1.Size = new System.Drawing.Size(178, 6);
            // 
            // MMFileSettings
            // 
            MMFileSettings.Enabled = false;
            MMFileSettings.Name = "MMFileSettings";
            MMFileSettings.Size = new System.Drawing.Size(181, 22);
            MMFileSettings.Text = "Settings ...";
            // 
            // MMFileS3
            // 
            MMFileS3.Name = "MMFileS3";
            MMFileS3.Size = new System.Drawing.Size(178, 6);
            // 
            // MMFileReload
            // 
            MMFileReload.Name = "MMFileReload";
            MMFileReload.Size = new System.Drawing.Size(181, 22);
            MMFileReload.Text = "Reload";
            MMFileReload.Click += MMFileReload_Click;
            // 
            // MMFileS2
            // 
            MMFileS2.Name = "MMFileS2";
            MMFileS2.Size = new System.Drawing.Size(178, 6);
            // 
            // MMFileExit
            // 
            MMFileExit.Name = "MMFileExit";
            MMFileExit.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
            MMFileExit.Size = new System.Drawing.Size(181, 22);
            MMFileExit.Text = "E&xit";
            MMFileExit.Click += MMFileExit_Click;
            // 
            // MMClient
            // 
            MMClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MMClientMap, MMClientS1, MMClientItems, MMClientDoodads, MMClientNPCs, MMClientSkills, MMClientZones, MMClientBuffs, MMClientFactions, MMClientQuests, MMClientLoot });
            MMClient.Name = "MMClient";
            MMClient.Size = new System.Drawing.Size(49, 20);
            MMClient.Text = "Client";
            MMClient.DropDownOpened += MMClient_DropDownOpened;
            // 
            // MMClientMap
            // 
            MMClientMap.Enabled = false;
            MMClientMap.Name = "MMClientMap";
            MMClientMap.Size = new System.Drawing.Size(121, 22);
            MMClientMap.Text = "Map";
            MMClientMap.Click += MMClientMap_Click;
            // 
            // MMClientS1
            // 
            MMClientS1.Name = "MMClientS1";
            MMClientS1.Size = new System.Drawing.Size(118, 6);
            // 
            // MMClientItems
            // 
            MMClientItems.Enabled = false;
            MMClientItems.Name = "MMClientItems";
            MMClientItems.Size = new System.Drawing.Size(121, 22);
            MMClientItems.Text = "Items";
            MMClientItems.Click += MMClientItems_Click;
            // 
            // MMClientDoodads
            // 
            MMClientDoodads.Enabled = false;
            MMClientDoodads.Name = "MMClientDoodads";
            MMClientDoodads.Size = new System.Drawing.Size(121, 22);
            MMClientDoodads.Text = "Doodads";
            // 
            // MMClientNPCs
            // 
            MMClientNPCs.Enabled = false;
            MMClientNPCs.Name = "MMClientNPCs";
            MMClientNPCs.Size = new System.Drawing.Size(121, 22);
            MMClientNPCs.Text = "NPCs";
            // 
            // MMClientSkills
            // 
            MMClientSkills.Enabled = false;
            MMClientSkills.Name = "MMClientSkills";
            MMClientSkills.Size = new System.Drawing.Size(121, 22);
            MMClientSkills.Text = "Skills";
            // 
            // MMClientZones
            // 
            MMClientZones.Enabled = false;
            MMClientZones.Name = "MMClientZones";
            MMClientZones.Size = new System.Drawing.Size(121, 22);
            MMClientZones.Text = "Zones";
            // 
            // MMClientBuffs
            // 
            MMClientBuffs.Enabled = false;
            MMClientBuffs.Name = "MMClientBuffs";
            MMClientBuffs.Size = new System.Drawing.Size(121, 22);
            MMClientBuffs.Text = "Buffs";
            // 
            // MMClientFactions
            // 
            MMClientFactions.Enabled = false;
            MMClientFactions.Name = "MMClientFactions";
            MMClientFactions.Size = new System.Drawing.Size(121, 22);
            MMClientFactions.Text = "Factions";
            // 
            // MMClientQuests
            // 
            MMClientQuests.Enabled = false;
            MMClientQuests.Name = "MMClientQuests";
            MMClientQuests.Size = new System.Drawing.Size(121, 22);
            MMClientQuests.Text = "Quests";
            // 
            // MMClientLoot
            // 
            MMClientLoot.Enabled = false;
            MMClientLoot.Name = "MMClientLoot";
            MMClientLoot.Size = new System.Drawing.Size(121, 22);
            MMClientLoot.Text = "Loot";
            // 
            // MMServer
            // 
            MMServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MMServerAccounts, MMServerCharacters, MMServerGuilds, toolStripMenuItem2, MMServerICS });
            MMServer.Name = "MMServer";
            MMServer.Size = new System.Drawing.Size(51, 20);
            MMServer.Text = "Server";
            MMServer.DropDownOpened += serverToolStripMenuItem_DropDownOpened;
            // 
            // MMServerAccounts
            // 
            MMServerAccounts.Enabled = false;
            MMServerAccounts.Name = "MMServerAccounts";
            MMServerAccounts.Size = new System.Drawing.Size(180, 22);
            MMServerAccounts.Text = "Accounts";
            MMServerAccounts.Click += MMServerAccounts_Click;
            // 
            // MMServerCharacters
            // 
            MMServerCharacters.Enabled = false;
            MMServerCharacters.Name = "MMServerCharacters";
            MMServerCharacters.Size = new System.Drawing.Size(180, 22);
            MMServerCharacters.Text = "Characters";
            MMServerCharacters.Click += MMServerCharacters_Click;
            // 
            // MMServerGuilds
            // 
            MMServerGuilds.Enabled = false;
            MMServerGuilds.Name = "MMServerGuilds";
            MMServerGuilds.Size = new System.Drawing.Size(180, 22);
            MMServerGuilds.Text = "Guilds";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // MMServerICS
            // 
            MMServerICS.Enabled = false;
            MMServerICS.Name = "MMServerICS";
            MMServerICS.Size = new System.Drawing.Size(180, 22);
            MMServerICS.Text = "Cash Shop";
            MMServerICS.Click += MMServerICS_Click;
            // 
            // MMVersion
            // 
            MMVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            MMVersion.Name = "MMVersion";
            MMVersion.Size = new System.Drawing.Size(57, 20);
            MMVersion.Text = "Version";
            // 
            // MMTools
            // 
            MMTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MMToolsAhBot });
            MMTools.Name = "MMTools";
            MMTools.Size = new System.Drawing.Size(46, 20);
            MMTools.Text = "&Tools";
            MMTools.DropDownOpened += MMTools_DropDownOpened;
            // 
            // MMToolsAhBot
            // 
            MMToolsAhBot.Name = "MMToolsAhBot";
            MMToolsAhBot.Size = new System.Drawing.Size(180, 22);
            MMToolsAhBot.Text = "Auction House Bot";
            MMToolsAhBot.Click += MMToolsAhBot_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(86, 35);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 16);
            label3.TabIndex = 3;
            label3.Text = "Server DB:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(86, 64);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(64, 16);
            label2.TabIndex = 4;
            label2.Text = "Client PAK:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(86, 93);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(48, 16);
            label4.TabIndex = 5;
            label4.Text = "MySQL:";
            // 
            // lMySQLServer
            // 
            lMySQLServer.AutoSize = true;
            lMySQLServer.Location = new System.Drawing.Point(189, 93);
            lMySQLServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lMySQLServer.Name = "lMySQLServer";
            lMySQLServer.Size = new System.Drawing.Size(50, 16);
            lMySQLServer.TabIndex = 8;
            lMySQLServer.Text = "127.0.0.1";
            // 
            // lClientPak
            // 
            lClientPak.AutoSize = true;
            lClientPak.Location = new System.Drawing.Point(189, 64);
            lClientPak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lClientPak.Name = "lClientPak";
            lClientPak.Size = new System.Drawing.Size(50, 16);
            lClientPak.TabIndex = 7;
            lClientPak.Text = "<none>";
            // 
            // lServerDB
            // 
            lServerDB.AutoSize = true;
            lServerDB.Location = new System.Drawing.Point(189, 35);
            lServerDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lServerDB.Name = "lServerDB";
            lServerDB.Size = new System.Drawing.Size(50, 16);
            lServerDB.TabIndex = 6;
            lServerDB.Text = "<none>";
            // 
            // ofdServerDB
            // 
            ofdServerDB.DefaultExt = "sqlite3";
            ofdServerDB.FileName = "compact.sqlite3";
            ofdServerDB.Filter = "SQLite Files|*.sqlite*|All files|*.*";
            ofdServerDB.InitialDirectory = "Data";
            ofdServerDB.ReadOnlyChecked = true;
            ofdServerDB.Title = "Open Server DB File";
            // 
            // ofdClientPak
            // 
            ofdClientPak.FileName = "game_pak";
            ofdClientPak.Filter = "Pak Files|*pak*.*|All Files|*.*";
            ofdClientPak.Title = "Open client game_pak";
            // 
            // TestPanel
            // 
            TestPanel.BackColor = System.Drawing.SystemColors.Control;
            TestPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            TestPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            TestPanel.Location = new System.Drawing.Point(14, 33);
            TestPanel.Margin = new System.Windows.Forms.Padding(4);
            TestPanel.Name = "TestPanel";
            TestPanel.Size = new System.Drawing.Size(64, 64);
            TestPanel.TabIndex = 11;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { sbL1 });
            statusStrip1.Location = new System.Drawing.Point(0, 207);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(856, 22);
            statusStrip1.TabIndex = 12;
            statusStrip1.Text = "statusStrip1";
            // 
            // sbL1
            // 
            sbL1.Name = "sbL1";
            sbL1.Size = new System.Drawing.Size(28, 17);
            sbL1.Text = "Info";
            // 
            // gbLocale
            // 
            gbLocale.Controls.Add(rbLocaleFr);
            gbLocale.Controls.Add(rbLocaleDe);
            gbLocale.Controls.Add(rbLocaleJa);
            gbLocale.Controls.Add(rbLocaleZhTw);
            gbLocale.Controls.Add(rbLocaleZhCn);
            gbLocale.Controls.Add(rbLocaleRu);
            gbLocale.Controls.Add(rbLocaleKo);
            gbLocale.Controls.Add(rbLocaleEnUs);
            gbLocale.Location = new System.Drawing.Point(14, 115);
            gbLocale.Name = "gbLocale";
            gbLocale.Size = new System.Drawing.Size(246, 77);
            gbLocale.TabIndex = 13;
            gbLocale.TabStop = false;
            gbLocale.Text = "Client Language";
            // 
            // rbLocaleFr
            // 
            rbLocaleFr.AutoSize = true;
            rbLocaleFr.Enabled = false;
            rbLocaleFr.Location = new System.Drawing.Point(200, 23);
            rbLocaleFr.Name = "rbLocaleFr";
            rbLocaleFr.Size = new System.Drawing.Size(33, 20);
            rbLocaleFr.TabIndex = 7;
            rbLocaleFr.Text = "fr";
            rbLocaleFr.UseVisualStyleBackColor = true;
            rbLocaleFr.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleDe
            // 
            rbLocaleDe.AutoSize = true;
            rbLocaleDe.Enabled = false;
            rbLocaleDe.Location = new System.Drawing.Point(139, 23);
            rbLocaleDe.Name = "rbLocaleDe";
            rbLocaleDe.Size = new System.Drawing.Size(38, 20);
            rbLocaleDe.TabIndex = 6;
            rbLocaleDe.Text = "de";
            rbLocaleDe.UseVisualStyleBackColor = true;
            rbLocaleDe.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleJa
            // 
            rbLocaleJa.AutoSize = true;
            rbLocaleJa.Enabled = false;
            rbLocaleJa.Location = new System.Drawing.Point(200, 51);
            rbLocaleJa.Name = "rbLocaleJa";
            rbLocaleJa.Size = new System.Drawing.Size(34, 20);
            rbLocaleJa.TabIndex = 5;
            rbLocaleJa.Text = "ja";
            rbLocaleJa.UseVisualStyleBackColor = true;
            rbLocaleJa.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleZhTw
            // 
            rbLocaleZhTw.AutoSize = true;
            rbLocaleZhTw.Enabled = false;
            rbLocaleZhTw.Location = new System.Drawing.Point(139, 51);
            rbLocaleZhTw.Name = "rbLocaleZhTw";
            rbLocaleZhTw.Size = new System.Drawing.Size(55, 20);
            rbLocaleZhTw.TabIndex = 4;
            rbLocaleZhTw.Text = "zh_tw";
            rbLocaleZhTw.UseVisualStyleBackColor = true;
            rbLocaleZhTw.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleZhCn
            // 
            rbLocaleZhCn.AutoSize = true;
            rbLocaleZhCn.Enabled = false;
            rbLocaleZhCn.Location = new System.Drawing.Point(78, 51);
            rbLocaleZhCn.Name = "rbLocaleZhCn";
            rbLocaleZhCn.Size = new System.Drawing.Size(55, 20);
            rbLocaleZhCn.TabIndex = 3;
            rbLocaleZhCn.Text = "zh_cn";
            rbLocaleZhCn.UseVisualStyleBackColor = true;
            rbLocaleZhCn.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleRu
            // 
            rbLocaleRu.AutoSize = true;
            rbLocaleRu.Enabled = false;
            rbLocaleRu.Location = new System.Drawing.Point(78, 23);
            rbLocaleRu.Name = "rbLocaleRu";
            rbLocaleRu.Size = new System.Drawing.Size(36, 20);
            rbLocaleRu.TabIndex = 2;
            rbLocaleRu.Text = "ru";
            rbLocaleRu.UseVisualStyleBackColor = true;
            rbLocaleRu.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleKo
            // 
            rbLocaleKo.AutoSize = true;
            rbLocaleKo.Location = new System.Drawing.Point(6, 23);
            rbLocaleKo.Name = "rbLocaleKo";
            rbLocaleKo.Size = new System.Drawing.Size(38, 20);
            rbLocaleKo.TabIndex = 1;
            rbLocaleKo.Text = "ko";
            rbLocaleKo.UseVisualStyleBackColor = true;
            rbLocaleKo.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // rbLocaleEnUs
            // 
            rbLocaleEnUs.AutoSize = true;
            rbLocaleEnUs.Checked = true;
            rbLocaleEnUs.Enabled = false;
            rbLocaleEnUs.Location = new System.Drawing.Point(6, 51);
            rbLocaleEnUs.Name = "rbLocaleEnUs";
            rbLocaleEnUs.Size = new System.Drawing.Size(55, 20);
            rbLocaleEnUs.TabIndex = 0;
            rbLocaleEnUs.TabStop = true;
            rbLocaleEnUs.Text = "en_us";
            rbLocaleEnUs.UseVisualStyleBackColor = true;
            rbLocaleEnUs.CheckedChanged += rbLocale_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(856, 229);
            Controls.Add(gbLocale);
            Controls.Add(statusStrip1);
            Controls.Add(TestPanel);
            Controls.Add(lMySQLServer);
            Controls.Add(lClientPak);
            Controls.Add(lServerDB);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(MM);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MM;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "MainForm";
            Text = "AAEmu.Editor";
            Load += MainForm_Load;
            MM.ResumeLayout(false);
            MM.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            gbLocale.ResumeLayout(false);
            gbLocale.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip MM;
        private System.Windows.Forms.ToolStripMenuItem MMFile;
        private System.Windows.Forms.ToolStripMenuItem MMFileOpenServer;
        private System.Windows.Forms.ToolStripMenuItem MMFileOpenClient;
        private System.Windows.Forms.ToolStripMenuItem MMFileOpenMySQL;
        private System.Windows.Forms.ToolStripSeparator MMFileS1;
        private System.Windows.Forms.ToolStripMenuItem MMFileSettings;
        private System.Windows.Forms.ToolStripSeparator MMFileS2;
        private System.Windows.Forms.ToolStripMenuItem MMFileExit;
        private System.Windows.Forms.ToolStripMenuItem MMClient;
        private System.Windows.Forms.ToolStripMenuItem MMClientItems;
        private System.Windows.Forms.ToolStripMenuItem MMClientMap;
        private System.Windows.Forms.ToolStripSeparator MMClientS1;
        private System.Windows.Forms.ToolStripMenuItem MMClientDoodads;
        private System.Windows.Forms.ToolStripMenuItem MMClientNPCs;
        private System.Windows.Forms.ToolStripMenuItem MMClientSkills;
        private System.Windows.Forms.ToolStripMenuItem MMClientZones;
        private System.Windows.Forms.ToolStripMenuItem MMClientBuffs;
        private System.Windows.Forms.ToolStripMenuItem MMClientFactions;
        private System.Windows.Forms.ToolStripMenuItem MMClientQuests;
        private System.Windows.Forms.ToolStripMenuItem MMClientLoot;
        private System.Windows.Forms.ToolStripMenuItem MMServer;
        private System.Windows.Forms.ToolStripMenuItem MMServerAccounts;
        private System.Windows.Forms.ToolStripMenuItem MMServerGuilds;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MMServerICS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lMySQLServer;
        private System.Windows.Forms.Label lClientPak;
        private System.Windows.Forms.Label lServerDB;
        private System.Windows.Forms.ToolStripSeparator MMFileS3;
        private System.Windows.Forms.ToolStripMenuItem MMFileReload;
        private System.Windows.Forms.OpenFileDialog ofdServerDB;
        private System.Windows.Forms.OpenFileDialog ofdClientPak;
        private System.Windows.Forms.Panel TestPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sbL1;
        private System.Windows.Forms.ToolStripMenuItem MMServerCharacters;
        private System.Windows.Forms.GroupBox gbLocale;
        private System.Windows.Forms.RadioButton rbLocaleEnUs;
        private System.Windows.Forms.RadioButton rbLocaleRu;
        private System.Windows.Forms.RadioButton rbLocaleKo;
        private System.Windows.Forms.RadioButton rbLocaleFr;
        private System.Windows.Forms.RadioButton rbLocaleDe;
        private System.Windows.Forms.RadioButton rbLocaleJa;
        private System.Windows.Forms.RadioButton rbLocaleZhTw;
        private System.Windows.Forms.RadioButton rbLocaleZhCn;
        private System.Windows.Forms.ToolStripMenuItem MMVersion;
        private System.Windows.Forms.ToolStripMenuItem MMTools;
        private System.Windows.Forms.ToolStripMenuItem MMToolsAhBot;
    }
}


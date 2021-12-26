
namespace AAEmu.DbEditor
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
            this.MM = new System.Windows.Forms.MenuStrip();
            this.MMFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileOpenServer = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileOpenClient = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileOpenMySQL = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MMFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileS3 = new System.Windows.Forms.ToolStripSeparator();
            this.MMFileReload = new System.Windows.Forms.ToolStripMenuItem();
            this.MMFileS2 = new System.Windows.Forms.ToolStripSeparator();
            this.MMFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClient = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientMap = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MMClientItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientDoodads = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientNPCs = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientSkills = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientZones = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientBuffs = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientFactions = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientQuests = new System.Windows.Forms.ToolStripMenuItem();
            this.MMClientLoot = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.charactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cashShopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lMySQLServer = new System.Windows.Forms.Label();
            this.lClientPak = new System.Windows.Forms.Label();
            this.lServerDB = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lClientLanguage = new System.Windows.Forms.Label();
            this.ofdServerDB = new System.Windows.Forms.OpenFileDialog();
            this.ofdClientPak = new System.Windows.Forms.OpenFileDialog();
            this.TestPanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sbL1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MM.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MM
            // 
            this.MM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMFile,
            this.MMClient,
            this.serverToolStripMenuItem});
            this.MM.Location = new System.Drawing.Point(0, 0);
            this.MM.Name = "MM";
            this.MM.Size = new System.Drawing.Size(734, 24);
            this.MM.TabIndex = 0;
            this.MM.Text = "menuStrip1";
            // 
            // MMFile
            // 
            this.MMFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMFileOpenServer,
            this.MMFileOpenClient,
            this.MMFileOpenMySQL,
            this.MMFileS1,
            this.MMFileSettings,
            this.MMFileS3,
            this.MMFileReload,
            this.MMFileS2,
            this.MMFileExit});
            this.MMFile.Name = "MMFile";
            this.MMFile.Size = new System.Drawing.Size(37, 20);
            this.MMFile.Text = "&File";
            // 
            // MMFileOpenServer
            // 
            this.MMFileOpenServer.Name = "MMFileOpenServer";
            this.MMFileOpenServer.Size = new System.Drawing.Size(183, 22);
            this.MMFileOpenServer.Text = "Open Server DB ...";
            this.MMFileOpenServer.Click += new System.EventHandler(this.MMFileOpenServer_Click);
            // 
            // MMFileOpenClient
            // 
            this.MMFileOpenClient.Name = "MMFileOpenClient";
            this.MMFileOpenClient.Size = new System.Drawing.Size(183, 22);
            this.MMFileOpenClient.Text = "Open Game Client ...";
            this.MMFileOpenClient.Click += new System.EventHandler(this.MMFileOpenClient_Click);
            // 
            // MMFileOpenMySQL
            // 
            this.MMFileOpenMySQL.Enabled = false;
            this.MMFileOpenMySQL.Name = "MMFileOpenMySQL";
            this.MMFileOpenMySQL.Size = new System.Drawing.Size(183, 22);
            this.MMFileOpenMySQL.Text = "Open MySQL DB ...";
            // 
            // MMFileS1
            // 
            this.MMFileS1.Name = "MMFileS1";
            this.MMFileS1.Size = new System.Drawing.Size(180, 6);
            // 
            // MMFileSettings
            // 
            this.MMFileSettings.Enabled = false;
            this.MMFileSettings.Name = "MMFileSettings";
            this.MMFileSettings.Size = new System.Drawing.Size(183, 22);
            this.MMFileSettings.Text = "Settings ...";
            // 
            // MMFileS3
            // 
            this.MMFileS3.Name = "MMFileS3";
            this.MMFileS3.Size = new System.Drawing.Size(180, 6);
            // 
            // MMFileReload
            // 
            this.MMFileReload.Name = "MMFileReload";
            this.MMFileReload.Size = new System.Drawing.Size(183, 22);
            this.MMFileReload.Text = "Reload";
            this.MMFileReload.Click += new System.EventHandler(this.MMFileReload_Click);
            // 
            // MMFileS2
            // 
            this.MMFileS2.Name = "MMFileS2";
            this.MMFileS2.Size = new System.Drawing.Size(180, 6);
            // 
            // MMFileExit
            // 
            this.MMFileExit.Name = "MMFileExit";
            this.MMFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MMFileExit.Size = new System.Drawing.Size(183, 22);
            this.MMFileExit.Text = "E&xit";
            this.MMFileExit.Click += new System.EventHandler(this.MMFileExit_Click);
            // 
            // MMClient
            // 
            this.MMClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMClientMap,
            this.MMClientS1,
            this.MMClientItems,
            this.MMClientDoodads,
            this.MMClientNPCs,
            this.MMClientSkills,
            this.MMClientZones,
            this.MMClientBuffs,
            this.MMClientFactions,
            this.MMClientQuests,
            this.MMClientLoot});
            this.MMClient.Name = "MMClient";
            this.MMClient.Size = new System.Drawing.Size(50, 20);
            this.MMClient.Text = "Client";
            // 
            // MMClientMap
            // 
            this.MMClientMap.Enabled = false;
            this.MMClientMap.Name = "MMClientMap";
            this.MMClientMap.Size = new System.Drawing.Size(121, 22);
            this.MMClientMap.Text = "Map";
            // 
            // MMClientS1
            // 
            this.MMClientS1.Name = "MMClientS1";
            this.MMClientS1.Size = new System.Drawing.Size(118, 6);
            // 
            // MMClientItems
            // 
            this.MMClientItems.Enabled = false;
            this.MMClientItems.Name = "MMClientItems";
            this.MMClientItems.Size = new System.Drawing.Size(121, 22);
            this.MMClientItems.Text = "Items";
            // 
            // MMClientDoodads
            // 
            this.MMClientDoodads.Enabled = false;
            this.MMClientDoodads.Name = "MMClientDoodads";
            this.MMClientDoodads.Size = new System.Drawing.Size(121, 22);
            this.MMClientDoodads.Text = "Doodads";
            // 
            // MMClientNPCs
            // 
            this.MMClientNPCs.Enabled = false;
            this.MMClientNPCs.Name = "MMClientNPCs";
            this.MMClientNPCs.Size = new System.Drawing.Size(121, 22);
            this.MMClientNPCs.Text = "NPCs";
            // 
            // MMClientSkills
            // 
            this.MMClientSkills.Enabled = false;
            this.MMClientSkills.Name = "MMClientSkills";
            this.MMClientSkills.Size = new System.Drawing.Size(121, 22);
            this.MMClientSkills.Text = "Skills";
            // 
            // MMClientZones
            // 
            this.MMClientZones.Enabled = false;
            this.MMClientZones.Name = "MMClientZones";
            this.MMClientZones.Size = new System.Drawing.Size(121, 22);
            this.MMClientZones.Text = "Zones";
            // 
            // MMClientBuffs
            // 
            this.MMClientBuffs.Enabled = false;
            this.MMClientBuffs.Name = "MMClientBuffs";
            this.MMClientBuffs.Size = new System.Drawing.Size(121, 22);
            this.MMClientBuffs.Text = "Buffs";
            // 
            // MMClientFactions
            // 
            this.MMClientFactions.Enabled = false;
            this.MMClientFactions.Name = "MMClientFactions";
            this.MMClientFactions.Size = new System.Drawing.Size(121, 22);
            this.MMClientFactions.Text = "Factions";
            // 
            // MMClientQuests
            // 
            this.MMClientQuests.Enabled = false;
            this.MMClientQuests.Name = "MMClientQuests";
            this.MMClientQuests.Size = new System.Drawing.Size(121, 22);
            this.MMClientQuests.Text = "Quests";
            // 
            // MMClientLoot
            // 
            this.MMClientLoot.Enabled = false;
            this.MMClientLoot.Name = "MMClientLoot";
            this.MMClientLoot.Size = new System.Drawing.Size(121, 22);
            this.MMClientLoot.Text = "Loot";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsToolStripMenuItem,
            this.charactersToolStripMenuItem,
            this.guildsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cashShopToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Enabled = false;
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // charactersToolStripMenuItem
            // 
            this.charactersToolStripMenuItem.Enabled = false;
            this.charactersToolStripMenuItem.Name = "charactersToolStripMenuItem";
            this.charactersToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.charactersToolStripMenuItem.Text = "Characters";
            // 
            // guildsToolStripMenuItem
            // 
            this.guildsToolStripMenuItem.Enabled = false;
            this.guildsToolStripMenuItem.Name = "guildsToolStripMenuItem";
            this.guildsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.guildsToolStripMenuItem.Text = "Guilds";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(127, 6);
            // 
            // cashShopToolStripMenuItem
            // 
            this.cashShopToolStripMenuItem.Enabled = false;
            this.cashShopToolStripMenuItem.Name = "cashShopToolStripMenuItem";
            this.cashShopToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.cashShopToolStripMenuItem.Text = "Cash Shop";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server DB:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Client PAK:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "MySQL:";
            // 
            // lMySQLServer
            // 
            this.lMySQLServer.AutoSize = true;
            this.lMySQLServer.Location = new System.Drawing.Point(162, 71);
            this.lMySQLServer.Name = "lMySQLServer";
            this.lMySQLServer.Size = new System.Drawing.Size(52, 13);
            this.lMySQLServer.TabIndex = 8;
            this.lMySQLServer.Text = "127.0.0.1";
            // 
            // lClientPak
            // 
            this.lClientPak.AutoSize = true;
            this.lClientPak.Location = new System.Drawing.Point(162, 49);
            this.lClientPak.Name = "lClientPak";
            this.lClientPak.Size = new System.Drawing.Size(43, 13);
            this.lClientPak.TabIndex = 7;
            this.lClientPak.Text = "<none>";
            // 
            // lServerDB
            // 
            this.lServerDB.AutoSize = true;
            this.lServerDB.Location = new System.Drawing.Point(162, 27);
            this.lServerDB.Name = "lServerDB";
            this.lServerDB.Size = new System.Drawing.Size(43, 13);
            this.lServerDB.TabIndex = 6;
            this.lServerDB.Text = "<none>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(69, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Client Language:";
            // 
            // lClientLanguage
            // 
            this.lClientLanguage.AutoSize = true;
            this.lClientLanguage.Location = new System.Drawing.Point(162, 95);
            this.lClientLanguage.Name = "lClientLanguage";
            this.lClientLanguage.Size = new System.Drawing.Size(36, 13);
            this.lClientLanguage.TabIndex = 10;
            this.lClientLanguage.Text = "en_us";
            // 
            // ofdServerDB
            // 
            this.ofdServerDB.DefaultExt = "sqlite3";
            this.ofdServerDB.FileName = "compact.sqlite3";
            this.ofdServerDB.Filter = "SQLite Files|*.sqlite*|All files|*.*";
            this.ofdServerDB.InitialDirectory = "Data";
            this.ofdServerDB.ReadOnlyChecked = true;
            this.ofdServerDB.Title = "Open Server DB File";
            // 
            // ofdClientPak
            // 
            this.ofdClientPak.FileName = "game_pak";
            this.ofdClientPak.Filter = "Pak Files|*pak*.*|All Files|*.*";
            this.ofdClientPak.Title = "Open client game_pak";
            // 
            // TestPanel
            // 
            this.TestPanel.BackColor = System.Drawing.SystemColors.Control;
            this.TestPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TestPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TestPanel.Location = new System.Drawing.Point(12, 27);
            this.TestPanel.Name = "TestPanel";
            this.TestPanel.Size = new System.Drawing.Size(51, 51);
            this.TestPanel.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbL1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 119);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sbL1
            // 
            this.sbL1.Name = "sbL1";
            this.sbL1.Size = new System.Drawing.Size(28, 17);
            this.sbL1.Text = "Info";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 141);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.TestPanel);
            this.Controls.Add(this.lClientLanguage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lMySQLServer);
            this.Controls.Add(this.lClientPak);
            this.Controls.Add(this.lServerDB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MM);
            this.MainMenuStrip = this.MM;
            this.Name = "MainForm";
            this.Text = "AAEmu.Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MM.ResumeLayout(false);
            this.MM.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem charactersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guildsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cashShopToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lMySQLServer;
        private System.Windows.Forms.Label lClientPak;
        private System.Windows.Forms.Label lServerDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lClientLanguage;
        private System.Windows.Forms.ToolStripSeparator MMFileS3;
        private System.Windows.Forms.ToolStripMenuItem MMFileReload;
        private System.Windows.Forms.OpenFileDialog ofdServerDB;
        private System.Windows.Forms.OpenFileDialog ofdClientPak;
        private System.Windows.Forms.Panel TestPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sbL1;
    }
}


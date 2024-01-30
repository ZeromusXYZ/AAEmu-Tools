
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
            MMServerGuilds = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            MMServerICS = new System.Windows.Forms.ToolStripMenuItem();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lMySQLServer = new System.Windows.Forms.Label();
            lClientPak = new System.Windows.Forms.Label();
            lServerDB = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            lClientLanguage = new System.Windows.Forms.Label();
            ofdServerDB = new System.Windows.Forms.OpenFileDialog();
            ofdClientPak = new System.Windows.Forms.OpenFileDialog();
            TestPanel = new System.Windows.Forms.Panel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            sbL1 = new System.Windows.Forms.ToolStripStatusLabel();
            MMServerCharacters = new System.Windows.Forms.ToolStripMenuItem();
            MM.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MM
            // 
            MM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MMFile, MMClient, MMServer });
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
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(80, 33);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 16);
            label3.TabIndex = 3;
            label3.Text = "Server DB:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(80, 60);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(64, 16);
            label2.TabIndex = 4;
            label2.Text = "Client PAK:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(80, 87);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(48, 16);
            label4.TabIndex = 5;
            label4.Text = "MySQL:";
            // 
            // lMySQLServer
            // 
            lMySQLServer.AutoSize = true;
            lMySQLServer.Location = new System.Drawing.Point(189, 87);
            lMySQLServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lMySQLServer.Name = "lMySQLServer";
            lMySQLServer.Size = new System.Drawing.Size(50, 16);
            lMySQLServer.TabIndex = 8;
            lMySQLServer.Text = "127.0.0.1";
            // 
            // lClientPak
            // 
            lClientPak.AutoSize = true;
            lClientPak.Location = new System.Drawing.Point(189, 60);
            lClientPak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lClientPak.Name = "lClientPak";
            lClientPak.Size = new System.Drawing.Size(50, 16);
            lClientPak.TabIndex = 7;
            lClientPak.Text = "<none>";
            // 
            // lServerDB
            // 
            lServerDB.AutoSize = true;
            lServerDB.Location = new System.Drawing.Point(189, 33);
            lServerDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lServerDB.Name = "lServerDB";
            lServerDB.Size = new System.Drawing.Size(50, 16);
            lServerDB.TabIndex = 6;
            lServerDB.Text = "<none>";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(80, 117);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(95, 16);
            label7.TabIndex = 9;
            label7.Text = "Client Language:";
            // 
            // lClientLanguage
            // 
            lClientLanguage.AutoSize = true;
            lClientLanguage.Location = new System.Drawing.Point(189, 117);
            lClientLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lClientLanguage.Name = "lClientLanguage";
            lClientLanguage.Size = new System.Drawing.Size(37, 16);
            lClientLanguage.TabIndex = 10;
            lClientLanguage.Text = "en_us";
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
            TestPanel.Size = new System.Drawing.Size(59, 62);
            TestPanel.TabIndex = 11;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { sbL1 });
            statusStrip1.Location = new System.Drawing.Point(0, 152);
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
            // MMServerCharacters
            // 
            MMServerCharacters.Enabled = false;
            MMServerCharacters.Name = "MMServerCharacters";
            MMServerCharacters.Size = new System.Drawing.Size(180, 22);
            MMServerCharacters.Text = "Characters";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(856, 174);
            Controls.Add(statusStrip1);
            Controls.Add(TestPanel);
            Controls.Add(lClientLanguage);
            Controls.Add(label7);
            Controls.Add(lMySQLServer);
            Controls.Add(lClientPak);
            Controls.Add(lServerDB);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(MM);
            MainMenuStrip = MM;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "MainForm";
            Text = "AAEmu.Editor";
            Load += MainForm_Load;
            MM.ResumeLayout(false);
            MM.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lClientLanguage;
        private System.Windows.Forms.ToolStripSeparator MMFileS3;
        private System.Windows.Forms.ToolStripMenuItem MMFileReload;
        private System.Windows.Forms.OpenFileDialog ofdServerDB;
        private System.Windows.Forms.OpenFileDialog ofdClientPak;
        private System.Windows.Forms.Panel TestPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sbL1;
        private System.Windows.Forms.ToolStripMenuItem MMServerCharacters;
    }
}


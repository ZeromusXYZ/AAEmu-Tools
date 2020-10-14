namespace AAEmu.ClientDataExporter
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
            this.LClientLocation = new System.Windows.Forms.Label();
            this.btnFindClient = new System.Windows.Forms.Button();
            this.clientFolderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.btnQuestSphere = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.exportFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.LQuestSphereData = new System.Windows.Forms.Label();
            this.lMissionXml = new System.Windows.Forms.Label();
            this.btnMissionXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LClientLocation
            // 
            this.LClientLocation.AutoSize = true;
            this.LClientLocation.Location = new System.Drawing.Point(156, 17);
            this.LClientLocation.Name = "LClientLocation";
            this.LClientLocation.Size = new System.Drawing.Size(102, 13);
            this.LClientLocation.TabIndex = 0;
            this.LClientLocation.Text = "<no client selected>";
            // 
            // btnFindClient
            // 
            this.btnFindClient.Location = new System.Drawing.Point(12, 12);
            this.btnFindClient.Name = "btnFindClient";
            this.btnFindClient.Size = new System.Drawing.Size(138, 23);
            this.btnFindClient.TabIndex = 1;
            this.btnFindClient.Text = "Locate Client";
            this.btnFindClient.UseVisualStyleBackColor = true;
            this.btnFindClient.Click += new System.EventHandler(this.BtnFindClient_Click);
            // 
            // clientFolderDlg
            // 
            this.clientFolderDlg.Description = "Please locate the root of the game client where the game_pak file is located";
            this.clientFolderDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.clientFolderDlg.SelectedPath = "C:\\ArcheAge\\Working";
            this.clientFolderDlg.ShowNewFolderButton = false;
            // 
            // btnQuestSphere
            // 
            this.btnQuestSphere.Enabled = false;
            this.btnQuestSphere.Location = new System.Drawing.Point(12, 63);
            this.btnQuestSphere.Name = "btnQuestSphere";
            this.btnQuestSphere.Size = new System.Drawing.Size(138, 23);
            this.btnQuestSphere.TabIndex = 2;
            this.btnQuestSphere.Text = "Quest Sphere Data";
            this.btnQuestSphere.UseVisualStyleBackColor = true;
            this.btnQuestSphere.Click += new System.EventHandler(this.BtnQuestSphere_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Export";
            // 
            // exportFileDlg
            // 
            this.exportFileDlg.Filter = "json files|*.json";
            // 
            // LQuestSphereData
            // 
            this.LQuestSphereData.AutoSize = true;
            this.LQuestSphereData.Location = new System.Drawing.Point(156, 68);
            this.LQuestSphereData.Name = "LQuestSphereData";
            this.LQuestSphereData.Size = new System.Drawing.Size(43, 13);
            this.LQuestSphereData.TabIndex = 4;
            this.LQuestSphereData.Text = "<none>";
            // 
            // lMissionXml
            // 
            this.lMissionXml.AutoSize = true;
            this.lMissionXml.Location = new System.Drawing.Point(156, 109);
            this.lMissionXml.Name = "lMissionXml";
            this.lMissionXml.Size = new System.Drawing.Size(43, 13);
            this.lMissionXml.TabIndex = 6;
            this.lMissionXml.Text = "<none>";
            // 
            // btnMissionXml
            // 
            this.btnMissionXml.Enabled = false;
            this.btnMissionXml.Location = new System.Drawing.Point(12, 104);
            this.btnMissionXml.Name = "btnMissionXml";
            this.btnMissionXml.Size = new System.Drawing.Size(138, 23);
            this.btnMissionXml.TabIndex = 5;
            this.btnMissionXml.Text = "Mission XML Data";
            this.btnMissionXml.UseVisualStyleBackColor = true;
            this.btnMissionXml.Click += new System.EventHandler(this.btnMissionXml_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lMissionXml);
            this.Controls.Add(this.btnMissionXml);
            this.Controls.Add(this.LQuestSphereData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuestSphere);
            this.Controls.Add(this.btnFindClient);
            this.Controls.Add(this.LClientLocation);
            this.Name = "MainForm";
            this.Text = "AAEmu Client Data Exporter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LClientLocation;
        private System.Windows.Forms.Button btnFindClient;
        private System.Windows.Forms.FolderBrowserDialog clientFolderDlg;
        private System.Windows.Forms.Button btnQuestSphere;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog exportFileDlg;
        private System.Windows.Forms.Label LQuestSphereData;
        private System.Windows.Forms.Label lMissionXml;
        private System.Windows.Forms.Button btnMissionXml;
    }
}


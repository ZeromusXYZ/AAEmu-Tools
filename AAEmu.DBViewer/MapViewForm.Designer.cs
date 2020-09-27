namespace AAEmu.DBViewer
{
    partial class MapViewForm
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
            this.pView = new System.Windows.Forms.Panel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsslPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.cbPoINames = new System.Windows.Forms.CheckBox();
            this.statusBar.SuspendLayout();
            this.gbTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // pView
            // 
            this.pView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.pView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pView.Location = new System.Drawing.Point(0, 0);
            this.pView.Name = "pView";
            this.pView.Size = new System.Drawing.Size(628, 460);
            this.pView.TabIndex = 0;
            this.pView.Paint += new System.Windows.Forms.PaintEventHandler(this.pView_Paint);
            this.pView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pView_MouseDown);
            this.pView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pView_MouseMove);
            this.pView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pView_MouseUp);
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.SystemColors.Control;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPos,
            this.tsslCoords});
            this.statusBar.Location = new System.Drawing.Point(0, 438);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(628, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // tsslPos
            // 
            this.tsslPos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslPos.Name = "tsslPos";
            this.tsslPos.Size = new System.Drawing.Size(50, 17);
            this.tsslPos.Text = "Position";
            this.tsslPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCoords
            // 
            this.tsslCoords.Name = "tsslCoords";
            this.tsslCoords.Size = new System.Drawing.Size(74, 17);
            this.tsslCoords.Text = "0,0 | 0°N, 0°E";
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.cbPoINames);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTools.Location = new System.Drawing.Point(0, 0);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(628, 47);
            this.gbTools.TabIndex = 2;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Show";
            // 
            // cbPoINames
            // 
            this.cbPoINames.AutoSize = true;
            this.cbPoINames.Checked = true;
            this.cbPoINames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPoINames.Location = new System.Drawing.Point(12, 19);
            this.cbPoINames.Name = "cbPoINames";
            this.cbPoINames.Size = new System.Drawing.Size(78, 17);
            this.cbPoINames.TabIndex = 0;
            this.cbPoINames.Text = "PoI Names";
            this.cbPoINames.UseVisualStyleBackColor = true;
            this.cbPoINames.ClientSizeChanged += new System.EventHandler(this.cbPoINames_ClientSizeChanged);
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 460);
            this.Controls.Add(this.gbTools);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.pView);
            this.Name = "MapViewForm";
            this.Text = "Map View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapViewForm_FormClosed);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.gbTools.ResumeLayout(false);
            this.gbTools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pView;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslPos;
        private System.Windows.Forms.ToolStripStatusLabel tsslCoords;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.CheckBox cbPoINames;
    }
}
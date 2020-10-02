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
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsslCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslViewOffset = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRuler = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.cbFocus = new System.Windows.Forms.CheckBox();
            this.cbZoneBorders = new System.Windows.Forms.CheckBox();
            this.cbShowWorldMap = new System.Windows.Forms.CheckBox();
            this.cbPoINames = new System.Windows.Forms.CheckBox();
            this.pView = new System.Windows.Forms.PictureBox();
            this.statusBar.SuspendLayout();
            this.gbTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.SystemColors.Control;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCoords,
            this.tsslViewOffset,
            this.tsslRuler});
            this.statusBar.Location = new System.Drawing.Point(0, 438);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(628, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // tsslCoords
            // 
            this.tsslCoords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCoords.Name = "tsslCoords";
            this.tsslCoords.Size = new System.Drawing.Size(74, 17);
            this.tsslCoords.Text = "0,0 | 0°N, 0°E";
            this.tsslCoords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslViewOffset
            // 
            this.tsslViewOffset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslViewOffset.Name = "tsslViewOffset";
            this.tsslViewOffset.Size = new System.Drawing.Size(50, 17);
            this.tsslViewOffset.Text = "Position";
            this.tsslViewOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslRuler
            // 
            this.tsslRuler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslRuler.Name = "tsslRuler";
            this.tsslRuler.Size = new System.Drawing.Size(36, 17);
            this.tsslRuler.Text = "-- , --";
            this.tsslRuler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.cbFocus);
            this.gbTools.Controls.Add(this.cbZoneBorders);
            this.gbTools.Controls.Add(this.cbShowWorldMap);
            this.gbTools.Controls.Add(this.cbPoINames);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTools.Location = new System.Drawing.Point(0, 0);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(628, 47);
            this.gbTools.TabIndex = 2;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Show";
            // 
            // cbFocus
            // 
            this.cbFocus.AutoSize = true;
            this.cbFocus.Checked = true;
            this.cbFocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFocus.Location = new System.Drawing.Point(270, 19);
            this.cbFocus.Name = "cbFocus";
            this.cbFocus.Size = new System.Drawing.Size(119, 17);
            this.cbFocus.TabIndex = 3;
            this.cbFocus.Text = "Show Focus Border";
            this.cbFocus.UseVisualStyleBackColor = true;
            this.cbFocus.CheckedChanged += new System.EventHandler(this.cbFocus_CheckedChanged);
            // 
            // cbZoneBorders
            // 
            this.cbZoneBorders.AutoSize = true;
            this.cbZoneBorders.Location = new System.Drawing.Point(90, 19);
            this.cbZoneBorders.Name = "cbZoneBorders";
            this.cbZoneBorders.Size = new System.Drawing.Size(90, 17);
            this.cbZoneBorders.TabIndex = 2;
            this.cbZoneBorders.Text = "Zone Borders";
            this.cbZoneBorders.UseVisualStyleBackColor = true;
            this.cbZoneBorders.CheckedChanged += new System.EventHandler(this.cbZoneBorders_CheckedChanged);
            // 
            // cbShowWorldMap
            // 
            this.cbShowWorldMap.AutoSize = true;
            this.cbShowWorldMap.Checked = true;
            this.cbShowWorldMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowWorldMap.Location = new System.Drawing.Point(6, 19);
            this.cbShowWorldMap.Name = "cbShowWorldMap";
            this.cbShowWorldMap.Size = new System.Drawing.Size(78, 17);
            this.cbShowWorldMap.TabIndex = 1;
            this.cbShowWorldMap.Text = "World Map";
            this.cbShowWorldMap.UseVisualStyleBackColor = true;
            this.cbShowWorldMap.CheckedChanged += new System.EventHandler(this.cbShowWorldMap_CheckedChanged);
            // 
            // cbPoINames
            // 
            this.cbPoINames.AutoSize = true;
            this.cbPoINames.Location = new System.Drawing.Point(186, 19);
            this.cbPoINames.Name = "cbPoINames";
            this.cbPoINames.Size = new System.Drawing.Size(78, 17);
            this.cbPoINames.TabIndex = 0;
            this.cbPoINames.Text = "PoI Names";
            this.cbPoINames.UseVisualStyleBackColor = true;
            this.cbPoINames.CheckedChanged += new System.EventHandler(this.cbPoINames_CheckedChanged);
            // 
            // pView
            // 
            this.pView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pView.Location = new System.Drawing.Point(0, 47);
            this.pView.Name = "pView";
            this.pView.Size = new System.Drawing.Size(628, 391);
            this.pView.TabIndex = 0;
            this.pView.TabStop = false;
            this.pView.Click += new System.EventHandler(this.pView_Click);
            this.pView.Paint += new System.Windows.Forms.PaintEventHandler(this.OnViewPaint);
            this.pView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnViewMouseDown);
            this.pView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnViewMouseMove);
            this.pView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnViewMouseUp);
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 460);
            this.Controls.Add(this.pView);
            this.Controls.Add(this.gbTools);
            this.Controls.Add(this.statusBar);
            this.DoubleBuffered = true;
            this.Name = "MapViewForm";
            this.Text = "Map View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapViewForm_FormClosed);
            this.Load += new System.EventHandler(this.MapViewForm_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.gbTools.ResumeLayout(false);
            this.gbTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslViewOffset;
        private System.Windows.Forms.ToolStripStatusLabel tsslCoords;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.CheckBox cbShowWorldMap;
        private System.Windows.Forms.CheckBox cbZoneBorders;
        public System.Windows.Forms.CheckBox cbPoINames;
        public System.Windows.Forms.CheckBox cbFocus;
        private System.Windows.Forms.PictureBox pView;
        private System.Windows.Forms.ToolStripStatusLabel tsslRuler;
    }
}
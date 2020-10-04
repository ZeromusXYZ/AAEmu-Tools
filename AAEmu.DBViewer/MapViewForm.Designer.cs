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
            this.tsslViewOffset = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRuler = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSelectionInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.cbShowGrid = new System.Windows.Forms.CheckBox();
            this.cbPathNames = new System.Windows.Forms.CheckBox();
            this.cbShowCity = new System.Windows.Forms.CheckBox();
            this.cbShowZone = new System.Windows.Forms.CheckBox();
            this.cbShowContinent = new System.Windows.Forms.CheckBox();
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
            this.tsslViewOffset,
            this.tsslZoom,
            this.tsslCoords,
            this.tsslRuler,
            this.tsslSelectionInfo});
            this.statusBar.Location = new System.Drawing.Point(0, 436);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(628, 24);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // tsslViewOffset
            // 
            this.tsslViewOffset.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslViewOffset.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslViewOffset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslViewOffset.Name = "tsslViewOffset";
            this.tsslViewOffset.Size = new System.Drawing.Size(43, 19);
            this.tsslViewOffset.Text = "Offset";
            this.tsslViewOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslViewOffset.Visible = false;
            // 
            // tsslZoom
            // 
            this.tsslZoom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslZoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslZoom.Name = "tsslZoom";
            this.tsslZoom.Size = new System.Drawing.Size(53, 19);
            this.tsslZoom.Text = "Zoom%";
            // 
            // tsslCoords
            // 
            this.tsslCoords.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslCoords.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslCoords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCoords.Name = "tsslCoords";
            this.tsslCoords.Size = new System.Drawing.Size(92, 19);
            this.tsslCoords.Text = "-- , -- | 0°N, 0°E";
            this.tsslCoords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslRuler
            // 
            this.tsslRuler.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslRuler.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslRuler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslRuler.Name = "tsslRuler";
            this.tsslRuler.Size = new System.Drawing.Size(40, 19);
            this.tsslRuler.Text = "-- , --";
            this.tsslRuler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSelectionInfo
            // 
            this.tsslSelectionInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslSelectionInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslSelectionInfo.Name = "tsslSelectionInfo";
            this.tsslSelectionInfo.Size = new System.Drawing.Size(32, 19);
            this.tsslSelectionInfo.Text = "Info";
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.cbShowGrid);
            this.gbTools.Controls.Add(this.cbPathNames);
            this.gbTools.Controls.Add(this.cbShowCity);
            this.gbTools.Controls.Add(this.cbShowZone);
            this.gbTools.Controls.Add(this.cbShowContinent);
            this.gbTools.Controls.Add(this.cbFocus);
            this.gbTools.Controls.Add(this.cbZoneBorders);
            this.gbTools.Controls.Add(this.cbShowWorldMap);
            this.gbTools.Controls.Add(this.cbPoINames);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbTools.Location = new System.Drawing.Point(529, 0);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(99, 436);
            this.gbTools.TabIndex = 2;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Show";
            // 
            // cbShowGrid
            // 
            this.cbShowGrid.AutoSize = true;
            this.cbShowGrid.Checked = true;
            this.cbShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowGrid.Location = new System.Drawing.Point(6, 221);
            this.cbShowGrid.Name = "cbShowGrid";
            this.cbShowGrid.Size = new System.Drawing.Size(45, 17);
            this.cbShowGrid.TabIndex = 8;
            this.cbShowGrid.Text = "Grid";
            this.cbShowGrid.UseVisualStyleBackColor = true;
            this.cbShowGrid.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbPathNames
            // 
            this.cbPathNames.AutoSize = true;
            this.cbPathNames.Location = new System.Drawing.Point(6, 175);
            this.cbPathNames.Name = "cbPathNames";
            this.cbPathNames.Size = new System.Drawing.Size(84, 17);
            this.cbPathNames.TabIndex = 7;
            this.cbPathNames.Text = "Path Names";
            this.cbPathNames.UseVisualStyleBackColor = true;
            this.cbPathNames.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbShowCity
            // 
            this.cbShowCity.AutoSize = true;
            this.cbShowCity.Checked = true;
            this.cbShowCity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowCity.Enabled = false;
            this.cbShowCity.Location = new System.Drawing.Point(6, 88);
            this.cbShowCity.Name = "cbShowCity";
            this.cbShowCity.Size = new System.Drawing.Size(43, 17);
            this.cbShowCity.TabIndex = 6;
            this.cbShowCity.Text = "City";
            this.cbShowCity.UseVisualStyleBackColor = true;
            this.cbShowCity.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbShowZone
            // 
            this.cbShowZone.AutoSize = true;
            this.cbShowZone.Checked = true;
            this.cbShowZone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowZone.Location = new System.Drawing.Point(6, 65);
            this.cbShowZone.Name = "cbShowZone";
            this.cbShowZone.Size = new System.Drawing.Size(51, 17);
            this.cbShowZone.TabIndex = 5;
            this.cbShowZone.Text = "Zone";
            this.cbShowZone.UseVisualStyleBackColor = true;
            this.cbShowZone.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbShowContinent
            // 
            this.cbShowContinent.AutoSize = true;
            this.cbShowContinent.Checked = true;
            this.cbShowContinent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowContinent.Location = new System.Drawing.Point(6, 42);
            this.cbShowContinent.Name = "cbShowContinent";
            this.cbShowContinent.Size = new System.Drawing.Size(71, 17);
            this.cbShowContinent.TabIndex = 4;
            this.cbShowContinent.Text = "Continent";
            this.cbShowContinent.UseVisualStyleBackColor = true;
            this.cbShowContinent.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbFocus
            // 
            this.cbFocus.AutoSize = true;
            this.cbFocus.Checked = true;
            this.cbFocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFocus.Location = new System.Drawing.Point(6, 198);
            this.cbFocus.Name = "cbFocus";
            this.cbFocus.Size = new System.Drawing.Size(89, 17);
            this.cbFocus.TabIndex = 3;
            this.cbFocus.Text = "Focus Border";
            this.cbFocus.UseVisualStyleBackColor = true;
            this.cbFocus.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbZoneBorders
            // 
            this.cbZoneBorders.AutoSize = true;
            this.cbZoneBorders.Location = new System.Drawing.Point(6, 132);
            this.cbZoneBorders.Name = "cbZoneBorders";
            this.cbZoneBorders.Size = new System.Drawing.Size(86, 17);
            this.cbZoneBorders.TabIndex = 2;
            this.cbZoneBorders.Text = "Map Borders";
            this.cbZoneBorders.UseVisualStyleBackColor = true;
            this.cbZoneBorders.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbShowWorldMap
            // 
            this.cbShowWorldMap.AutoSize = true;
            this.cbShowWorldMap.Checked = true;
            this.cbShowWorldMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowWorldMap.Location = new System.Drawing.Point(6, 19);
            this.cbShowWorldMap.Name = "cbShowWorldMap";
            this.cbShowWorldMap.Size = new System.Drawing.Size(54, 17);
            this.cbShowWorldMap.TabIndex = 1;
            this.cbShowWorldMap.Text = "World";
            this.cbShowWorldMap.UseVisualStyleBackColor = true;
            this.cbShowWorldMap.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbPoINames
            // 
            this.cbPoINames.AutoSize = true;
            this.cbPoINames.Location = new System.Drawing.Point(6, 152);
            this.cbPoINames.Name = "cbPoINames";
            this.cbPoINames.Size = new System.Drawing.Size(78, 17);
            this.cbPoINames.TabIndex = 0;
            this.cbPoINames.Text = "PoI Names";
            this.cbPoINames.UseVisualStyleBackColor = true;
            this.cbPoINames.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // pView
            // 
            this.pView.BackColor = System.Drawing.Color.DimGray;
            this.pView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pView.Location = new System.Drawing.Point(0, 0);
            this.pView.Name = "pView";
            this.pView.Size = new System.Drawing.Size(529, 436);
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
        private System.Windows.Forms.ToolStripStatusLabel tsslZoom;
        private System.Windows.Forms.ToolStripStatusLabel tsslSelectionInfo;
        private System.Windows.Forms.CheckBox cbShowCity;
        private System.Windows.Forms.CheckBox cbShowZone;
        private System.Windows.Forms.CheckBox cbShowContinent;
        public System.Windows.Forms.CheckBox cbPathNames;
        public System.Windows.Forms.CheckBox cbShowGrid;
    }
}
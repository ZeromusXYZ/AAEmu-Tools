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
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.rbGridOff = new System.Windows.Forms.RadioButton();
            this.rbGridGeo = new System.Windows.Forms.RadioButton();
            this.rbGridCells = new System.Windows.Forms.RadioButton();
            this.rbGridUnits = new System.Windows.Forms.RadioButton();
            this.cbDrawMiniMap = new System.Windows.Forms.CheckBox();
            this.cbDrawMainMap = new System.Windows.Forms.CheckBox();
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
            this.gbGrid.SuspendLayout();
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
            this.gbTools.Controls.Add(this.gbGrid);
            this.gbTools.Controls.Add(this.cbDrawMiniMap);
            this.gbTools.Controls.Add(this.cbDrawMainMap);
            this.gbTools.Controls.Add(this.cbPathNames);
            this.gbTools.Controls.Add(this.cbShowCity);
            this.gbTools.Controls.Add(this.cbShowZone);
            this.gbTools.Controls.Add(this.cbShowContinent);
            this.gbTools.Controls.Add(this.cbFocus);
            this.gbTools.Controls.Add(this.cbZoneBorders);
            this.gbTools.Controls.Add(this.cbShowWorldMap);
            this.gbTools.Controls.Add(this.cbPoINames);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbTools.Location = new System.Drawing.Point(518, 0);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(110, 436);
            this.gbTools.TabIndex = 2;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Show";
            // 
            // gbGrid
            // 
            this.gbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrid.Controls.Add(this.rbGridOff);
            this.gbGrid.Controls.Add(this.rbGridGeo);
            this.gbGrid.Controls.Add(this.rbGridCells);
            this.gbGrid.Controls.Add(this.rbGridUnits);
            this.gbGrid.Location = new System.Drawing.Point(6, 316);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(97, 114);
            this.gbGrid.TabIndex = 11;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Grid";
            // 
            // rbGridOff
            // 
            this.rbGridOff.AutoSize = true;
            this.rbGridOff.Checked = true;
            this.rbGridOff.Location = new System.Drawing.Point(8, 19);
            this.rbGridOff.Name = "rbGridOff";
            this.rbGridOff.Size = new System.Drawing.Size(39, 17);
            this.rbGridOff.TabIndex = 3;
            this.rbGridOff.TabStop = true;
            this.rbGridOff.Text = "Off";
            this.rbGridOff.UseVisualStyleBackColor = true;
            this.rbGridOff.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // rbGridGeo
            // 
            this.rbGridGeo.AutoSize = true;
            this.rbGridGeo.Location = new System.Drawing.Point(8, 88);
            this.rbGridGeo.Name = "rbGridGeo";
            this.rbGridGeo.Size = new System.Drawing.Size(45, 17);
            this.rbGridGeo.TabIndex = 2;
            this.rbGridGeo.TabStop = true;
            this.rbGridGeo.Text = "Geo";
            this.rbGridGeo.UseVisualStyleBackColor = true;
            this.rbGridGeo.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // rbGridCells
            // 
            this.rbGridCells.AutoSize = true;
            this.rbGridCells.Location = new System.Drawing.Point(8, 65);
            this.rbGridCells.Name = "rbGridCells";
            this.rbGridCells.Size = new System.Drawing.Size(47, 17);
            this.rbGridCells.TabIndex = 1;
            this.rbGridCells.Text = "Cells";
            this.rbGridCells.UseVisualStyleBackColor = true;
            this.rbGridCells.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // rbGridUnits
            // 
            this.rbGridUnits.AutoSize = true;
            this.rbGridUnits.Location = new System.Drawing.Point(8, 42);
            this.rbGridUnits.Name = "rbGridUnits";
            this.rbGridUnits.Size = new System.Drawing.Size(49, 17);
            this.rbGridUnits.TabIndex = 0;
            this.rbGridUnits.Text = "Units";
            this.rbGridUnits.UseVisualStyleBackColor = true;
            this.rbGridUnits.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbDrawMiniMap
            // 
            this.cbDrawMiniMap.AutoSize = true;
            this.cbDrawMiniMap.Checked = true;
            this.cbDrawMiniMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDrawMiniMap.Location = new System.Drawing.Point(6, 143);
            this.cbDrawMiniMap.Name = "cbDrawMiniMap";
            this.cbDrawMiniMap.Size = new System.Drawing.Size(69, 17);
            this.cbDrawMiniMap.TabIndex = 10;
            this.cbDrawMiniMap.Text = "Mini Map";
            this.cbDrawMiniMap.UseVisualStyleBackColor = true;
            this.cbDrawMiniMap.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbDrawMainMap
            // 
            this.cbDrawMainMap.AutoSize = true;
            this.cbDrawMainMap.Location = new System.Drawing.Point(6, 120);
            this.cbDrawMainMap.Name = "cbDrawMainMap";
            this.cbDrawMainMap.Size = new System.Drawing.Size(73, 17);
            this.cbDrawMainMap.TabIndex = 9;
            this.cbDrawMainMap.Text = "Main Map";
            this.cbDrawMainMap.UseVisualStyleBackColor = true;
            this.cbDrawMainMap.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbPathNames
            // 
            this.cbPathNames.AutoSize = true;
            this.cbPathNames.Location = new System.Drawing.Point(6, 237);
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
            this.cbFocus.Location = new System.Drawing.Point(6, 260);
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
            this.cbZoneBorders.Location = new System.Drawing.Point(6, 166);
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
            this.cbPoINames.Location = new System.Drawing.Point(6, 214);
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
            this.pView.Size = new System.Drawing.Size(518, 436);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapViewForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapViewForm_FormClosed);
            this.Load += new System.EventHandler(this.MapViewForm_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.gbTools.ResumeLayout(false);
            this.gbTools.PerformLayout();
            this.gbGrid.ResumeLayout(false);
            this.gbGrid.PerformLayout();
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
        private System.Windows.Forms.CheckBox cbDrawMiniMap;
        private System.Windows.Forms.CheckBox cbDrawMainMap;
        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.RadioButton rbGridGeo;
        private System.Windows.Forms.RadioButton rbGridCells;
        private System.Windows.Forms.RadioButton rbGridUnits;
        private System.Windows.Forms.RadioButton rbGridOff;
    }
}
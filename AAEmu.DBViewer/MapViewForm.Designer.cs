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
            this.components = new System.ComponentModel.Container();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsslViewOffset = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRuler = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSelectionInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPoIInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.cbInstanceSelect = new System.Windows.Forms.ComboBox();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.cbUnitSize = new System.Windows.Forms.ComboBox();
            this.rbGridOff = new System.Windows.Forms.RadioButton();
            this.rbGridGeo = new System.Windows.Forms.RadioButton();
            this.rbGridCells = new System.Windows.Forms.RadioButton();
            this.rbGridUnits = new System.Windows.Forms.RadioButton();
            this.cbDrawMiniMap = new System.Windows.Forms.CheckBox();
            this.cbDrawMainMap = new System.Windows.Forms.CheckBox();
            this.cbFocus = new System.Windows.Forms.CheckBox();
            this.cbZoneBorders = new System.Windows.Forms.CheckBox();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbDrawWorld = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawContinent = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawZone = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawCity = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbShowPoI = new System.Windows.Forms.ToolStripButton();
            this.tsbShowPath = new System.Windows.Forms.ToolStripButton();
            this.tsbShowHousing = new System.Windows.Forms.ToolStripButton();
            this.tsbShowSubzone = new System.Windows.Forms.ToolStripButton();
            this.tsbShowQuestSphere = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbNamesPoI = new System.Windows.Forms.ToolStripButton();
            this.tsbNamesPath = new System.Windows.Forms.ToolStripButton();
            this.tsbNamesHousing = new System.Windows.Forms.ToolStripButton();
            this.tsbNamesSubzone = new System.Windows.Forms.ToolStripButton();
            this.tsbNamesQuestSphere = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pView = new System.Windows.Forms.PictureBox();
            this.cmsMapInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmPoI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            this.gbTools.SuspendLayout();
            this.gbGrid.SuspendLayout();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).BeginInit();
            this.cmsMapInfo.SuspendLayout();
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
            this.tsslSelectionInfo,
            this.tsslPoIInfo});
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
            // tsslPoIInfo
            // 
            this.tsslPoIInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslPoIInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslPoIInfo.Name = "tsslPoIInfo";
            this.tsslPoIInfo.Size = new System.Drawing.Size(28, 19);
            this.tsslPoIInfo.Text = "PoI";
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.cbInstanceSelect);
            this.gbTools.Controls.Add(this.gbGrid);
            this.gbTools.Controls.Add(this.cbDrawMiniMap);
            this.gbTools.Controls.Add(this.cbDrawMainMap);
            this.gbTools.Controls.Add(this.cbFocus);
            this.gbTools.Controls.Add(this.cbZoneBorders);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbTools.Location = new System.Drawing.Point(518, 0);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(110, 436);
            this.gbTools.TabIndex = 2;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Options";
            // 
            // cbInstanceSelect
            // 
            this.cbInstanceSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInstanceSelect.DropDownHeight = 240;
            this.cbInstanceSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInstanceSelect.DropDownWidth = 200;
            this.cbInstanceSelect.Enabled = false;
            this.cbInstanceSelect.FormattingEnabled = true;
            this.cbInstanceSelect.IntegralHeight = false;
            this.cbInstanceSelect.Location = new System.Drawing.Point(6, 409);
            this.cbInstanceSelect.Name = "cbInstanceSelect";
            this.cbInstanceSelect.Size = new System.Drawing.Size(98, 21);
            this.cbInstanceSelect.TabIndex = 12;
            this.cbInstanceSelect.SelectedIndexChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // gbGrid
            // 
            this.gbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrid.Controls.Add(this.cbUnitSize);
            this.gbGrid.Controls.Add(this.rbGridOff);
            this.gbGrid.Controls.Add(this.rbGridGeo);
            this.gbGrid.Controls.Add(this.rbGridCells);
            this.gbGrid.Controls.Add(this.rbGridUnits);
            this.gbGrid.Location = new System.Drawing.Point(7, 264);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(97, 139);
            this.gbGrid.TabIndex = 11;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Grid";
            // 
            // cbUnitSize
            // 
            this.cbUnitSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnitSize.FormattingEnabled = true;
            this.cbUnitSize.Items.AddRange(new object[] {
            "1024",
            "512",
            "256",
            "100",
            "64",
            "32"});
            this.cbUnitSize.Location = new System.Drawing.Point(26, 65);
            this.cbUnitSize.Name = "cbUnitSize";
            this.cbUnitSize.Size = new System.Drawing.Size(65, 21);
            this.cbUnitSize.TabIndex = 4;
            this.cbUnitSize.SelectedIndexChanged += new System.EventHandler(this.cbOptionsChanged);
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
            this.rbGridGeo.Location = new System.Drawing.Point(6, 115);
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
            this.rbGridCells.Location = new System.Drawing.Point(6, 92);
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
            this.cbDrawMiniMap.Location = new System.Drawing.Point(6, 42);
            this.cbDrawMiniMap.Name = "cbDrawMiniMap";
            this.cbDrawMiniMap.Size = new System.Drawing.Size(75, 17);
            this.cbDrawMiniMap.TabIndex = 10;
            this.cbDrawMiniMap.Text = "Road map";
            this.cbDrawMiniMap.UseVisualStyleBackColor = true;
            this.cbDrawMiniMap.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbDrawMainMap
            // 
            this.cbDrawMainMap.AutoSize = true;
            this.cbDrawMainMap.Location = new System.Drawing.Point(6, 19);
            this.cbDrawMainMap.Name = "cbDrawMainMap";
            this.cbDrawMainMap.Size = new System.Drawing.Size(65, 17);
            this.cbDrawMainMap.TabIndex = 9;
            this.cbDrawMainMap.Text = "Full map";
            this.cbDrawMainMap.UseVisualStyleBackColor = true;
            this.cbDrawMainMap.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbFocus
            // 
            this.cbFocus.AutoSize = true;
            this.cbFocus.Checked = true;
            this.cbFocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFocus.Location = new System.Drawing.Point(6, 134);
            this.cbFocus.Name = "cbFocus";
            this.cbFocus.Size = new System.Drawing.Size(88, 17);
            this.cbFocus.TabIndex = 3;
            this.cbFocus.Text = "Focus border";
            this.cbFocus.UseVisualStyleBackColor = true;
            this.cbFocus.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // cbZoneBorders
            // 
            this.cbZoneBorders.AutoSize = true;
            this.cbZoneBorders.Location = new System.Drawing.Point(6, 88);
            this.cbZoneBorders.Name = "cbZoneBorders";
            this.cbZoneBorders.Size = new System.Drawing.Size(89, 17);
            this.cbZoneBorders.TabIndex = 2;
            this.cbZoneBorders.Text = "Zone borders";
            this.cbZoneBorders.UseVisualStyleBackColor = true;
            this.cbZoneBorders.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsbDrawWorld,
            this.tsbDrawContinent,
            this.tsbDrawZone,
            this.tsbDrawCity,
            this.toolStripLabel2,
            this.tsbShowPoI,
            this.tsbShowPath,
            this.tsbShowHousing,
            this.tsbShowSubzone,
            this.tsbShowQuestSphere,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tsbNamesPoI,
            this.tsbNamesPath,
            this.tsbNamesHousing,
            this.tsbNamesSubzone,
            this.tsbNamesQuestSphere,
            this.toolStripSeparator2});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(518, 25);
            this.toolBar.TabIndex = 3;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel3.Text = "Draw";
            // 
            // tsbDrawWorld
            // 
            this.tsbDrawWorld.CheckOnClick = true;
            this.tsbDrawWorld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawWorld.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_globe;
            this.tsbDrawWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawWorld.Name = "tsbDrawWorld";
            this.tsbDrawWorld.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawWorld.Text = "World Map";
            this.tsbDrawWorld.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbDrawContinent
            // 
            this.tsbDrawContinent.CheckOnClick = true;
            this.tsbDrawContinent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawContinent.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_nuia;
            this.tsbDrawContinent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawContinent.Name = "tsbDrawContinent";
            this.tsbDrawContinent.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawContinent.Text = "Continent";
            this.tsbDrawContinent.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbDrawZone
            // 
            this.tsbDrawZone.CheckOnClick = true;
            this.tsbDrawZone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawZone.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_zone;
            this.tsbDrawZone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawZone.Name = "tsbDrawZone";
            this.tsbDrawZone.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawZone.Text = "Zone";
            this.tsbDrawZone.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbDrawCity
            // 
            this.tsbDrawCity.CheckOnClick = true;
            this.tsbDrawCity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawCity.Enabled = false;
            this.tsbDrawCity.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_city;
            this.tsbDrawCity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawCity.Name = "tsbDrawCity";
            this.tsbDrawCity.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawCity.Text = "City/Cave";
            this.tsbDrawCity.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel2.Text = "Show";
            // 
            // tsbShowPoI
            // 
            this.tsbShowPoI.Checked = true;
            this.tsbShowPoI.CheckOnClick = true;
            this.tsbShowPoI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowPoI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowPoI.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_poi;
            this.tsbShowPoI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowPoI.Name = "tsbShowPoI";
            this.tsbShowPoI.Size = new System.Drawing.Size(23, 22);
            this.tsbShowPoI.Text = "PoI";
            this.tsbShowPoI.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbShowPath
            // 
            this.tsbShowPath.Checked = true;
            this.tsbShowPath.CheckOnClick = true;
            this.tsbShowPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowPath.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_carriage;
            this.tsbShowPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowPath.Name = "tsbShowPath";
            this.tsbShowPath.Size = new System.Drawing.Size(23, 22);
            this.tsbShowPath.Text = "Paths";
            this.tsbShowPath.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbShowHousing
            // 
            this.tsbShowHousing.Checked = true;
            this.tsbShowHousing.CheckOnClick = true;
            this.tsbShowHousing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowHousing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowHousing.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_house;
            this.tsbShowHousing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowHousing.Name = "tsbShowHousing";
            this.tsbShowHousing.Size = new System.Drawing.Size(23, 22);
            this.tsbShowHousing.Text = "Housing";
            this.tsbShowHousing.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbShowSubzone
            // 
            this.tsbShowSubzone.Checked = true;
            this.tsbShowSubzone.CheckOnClick = true;
            this.tsbShowSubzone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowSubzone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowSubzone.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_house;
            this.tsbShowSubzone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowSubzone.Name = "tsbShowSubzone";
            this.tsbShowSubzone.Size = new System.Drawing.Size(23, 22);
            this.tsbShowSubzone.Text = "Subzone";
            this.tsbShowSubzone.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbShowQuestSphere
            // 
            this.tsbShowQuestSphere.Checked = true;
            this.tsbShowQuestSphere.CheckOnClick = true;
            this.tsbShowQuestSphere.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowQuestSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowQuestSphere.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_quest;
            this.tsbShowQuestSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowQuestSphere.Name = "tsbShowQuestSphere";
            this.tsbShowQuestSphere.Size = new System.Drawing.Size(23, 22);
            this.tsbShowQuestSphere.Text = "Quest Spheres";
            this.tsbShowQuestSphere.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Names";
            // 
            // tsbNamesPoI
            // 
            this.tsbNamesPoI.CheckOnClick = true;
            this.tsbNamesPoI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNamesPoI.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_poi;
            this.tsbNamesPoI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNamesPoI.Name = "tsbNamesPoI";
            this.tsbNamesPoI.Size = new System.Drawing.Size(23, 22);
            this.tsbNamesPoI.Text = "PoI";
            this.tsbNamesPoI.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbNamesPath
            // 
            this.tsbNamesPath.CheckOnClick = true;
            this.tsbNamesPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNamesPath.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_carriage;
            this.tsbNamesPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNamesPath.Name = "tsbNamesPath";
            this.tsbNamesPath.Size = new System.Drawing.Size(23, 22);
            this.tsbNamesPath.Text = "Paths";
            this.tsbNamesPath.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbNamesHousing
            // 
            this.tsbNamesHousing.CheckOnClick = true;
            this.tsbNamesHousing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNamesHousing.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_house;
            this.tsbNamesHousing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNamesHousing.Name = "tsbNamesHousing";
            this.tsbNamesHousing.Size = new System.Drawing.Size(23, 22);
            this.tsbNamesHousing.Text = "Housing";
            this.tsbNamesHousing.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbNamesSubzone
            // 
            this.tsbNamesSubzone.CheckOnClick = true;
            this.tsbNamesSubzone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNamesSubzone.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_house;
            this.tsbNamesSubzone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNamesSubzone.Name = "tsbNamesSubzone";
            this.tsbNamesSubzone.Size = new System.Drawing.Size(23, 22);
            this.tsbNamesSubzone.Text = "Subzone";
            this.tsbNamesSubzone.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // tsbNamesQuestSphere
            // 
            this.tsbNamesQuestSphere.CheckOnClick = true;
            this.tsbNamesQuestSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNamesQuestSphere.Image = global::AAEmu.DBViewer.Properties.Resources.icon_map_quest;
            this.tsbNamesQuestSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNamesQuestSphere.Name = "tsbNamesQuestSphere";
            this.tsbNamesQuestSphere.Size = new System.Drawing.Size(23, 22);
            this.tsbNamesQuestSphere.Text = "Quest Spheres";
            this.tsbNamesQuestSphere.CheckedChanged += new System.EventHandler(this.cbOptionsChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pView
            // 
            this.pView.BackColor = System.Drawing.Color.DimGray;
            this.pView.ContextMenuStrip = this.cmsMapInfo;
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
            // cmsMapInfo
            // 
            this.cmsMapInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPoI,
            this.tsmPath,
            this.tsmMap,
            this.tsmCopyToClipboard});
            this.cmsMapInfo.Name = "cmsMapInfo";
            this.cmsMapInfo.Size = new System.Drawing.Size(167, 92);
            // 
            // tsmPoI
            // 
            this.tsmPoI.Name = "tsmPoI";
            this.tsmPoI.Size = new System.Drawing.Size(166, 22);
            this.tsmPoI.Text = "PoI";
            // 
            // tsmPath
            // 
            this.tsmPath.Name = "tsmPath";
            this.tsmPath.Size = new System.Drawing.Size(166, 22);
            this.tsmPath.Text = "Path";
            // 
            // tsmMap
            // 
            this.tsmMap.Name = "tsmMap";
            this.tsmMap.Size = new System.Drawing.Size(166, 22);
            this.tsmMap.Text = "Map";
            // 
            // tsmCopyToClipboard
            // 
            this.tsmCopyToClipboard.Name = "tsmCopyToClipboard";
            this.tsmCopyToClipboard.Size = new System.Drawing.Size(166, 22);
            this.tsmCopyToClipboard.Text = "CopyToClipboard";
            this.tsmCopyToClipboard.Visible = false;
            this.tsmCopyToClipboard.Click += new System.EventHandler(this.tsmCopyToClipboard_Click);
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 460);
            this.Controls.Add(this.toolBar);
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
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).EndInit();
            this.cmsMapInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslViewOffset;
        private System.Windows.Forms.ToolStripStatusLabel tsslCoords;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.CheckBox cbZoneBorders;
        public System.Windows.Forms.CheckBox cbFocus;
        private System.Windows.Forms.PictureBox pView;
        private System.Windows.Forms.ToolStripStatusLabel tsslRuler;
        private System.Windows.Forms.ToolStripStatusLabel tsslZoom;
        private System.Windows.Forms.ToolStripStatusLabel tsslSelectionInfo;
        private System.Windows.Forms.CheckBox cbDrawMiniMap;
        private System.Windows.Forms.CheckBox cbDrawMainMap;
        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.RadioButton rbGridGeo;
        private System.Windows.Forms.RadioButton rbGridCells;
        private System.Windows.Forms.RadioButton rbGridUnits;
        private System.Windows.Forms.RadioButton rbGridOff;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripButton tsbNamesPoI;
        public System.Windows.Forms.ToolStripButton tsbNamesPath;
        public System.Windows.Forms.ToolStripButton tsbNamesHousing;
        public System.Windows.Forms.ToolStripButton tsbNamesSubzone;
        public System.Windows.Forms.ToolStripButton tsbNamesQuestSphere;
        public System.Windows.Forms.ToolStrip toolBar;
        public System.Windows.Forms.ToolStripButton tsbShowPoI;
        public System.Windows.Forms.ToolStripButton tsbShowPath;
        public System.Windows.Forms.ToolStripButton tsbShowHousing;
        public System.Windows.Forms.ToolStripButton tsbShowSubzone;
        public System.Windows.Forms.ToolStripButton tsbShowQuestSphere;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsbDrawWorld;
        private System.Windows.Forms.ToolStripButton tsbDrawContinent;
        private System.Windows.Forms.ToolStripButton tsbDrawZone;
        private System.Windows.Forms.ToolStripButton tsbDrawCity;
        private System.Windows.Forms.ComboBox cbInstanceSelect;
        private System.Windows.Forms.ComboBox cbUnitSize;
        private System.Windows.Forms.ToolStripStatusLabel tsslPoIInfo;
        private System.Windows.Forms.ContextMenuStrip cmsMapInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmPoI;
        private System.Windows.Forms.ToolStripMenuItem tsmPath;
        private System.Windows.Forms.ToolStripMenuItem tsmMap;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyToClipboard;
    }
}
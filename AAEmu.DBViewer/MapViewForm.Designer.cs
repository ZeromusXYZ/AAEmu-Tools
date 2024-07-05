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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewForm));
            statusBar = new System.Windows.Forms.StatusStrip();
            tsslViewOffset = new System.Windows.Forms.ToolStripStatusLabel();
            tsslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            tsslCoords = new System.Windows.Forms.ToolStripStatusLabel();
            tsslRuler = new System.Windows.Forms.ToolStripStatusLabel();
            tsslSelectionInfo = new System.Windows.Forms.ToolStripStatusLabel();
            tsslPoIInfo = new System.Windows.Forms.ToolStripStatusLabel();
            gbTools = new System.Windows.Forms.GroupBox();
            cbInstanceSelect = new System.Windows.Forms.ComboBox();
            gbGrid = new System.Windows.Forms.GroupBox();
            rbGridPaths = new System.Windows.Forms.RadioButton();
            cbUnitSize = new System.Windows.Forms.ComboBox();
            rbGridOff = new System.Windows.Forms.RadioButton();
            rbGridGeo = new System.Windows.Forms.RadioButton();
            rbGridCells = new System.Windows.Forms.RadioButton();
            rbGridUnits = new System.Windows.Forms.RadioButton();
            cbDrawMiniMap = new System.Windows.Forms.CheckBox();
            cbDrawMainMap = new System.Windows.Forms.CheckBox();
            cbFocus = new System.Windows.Forms.CheckBox();
            cbZoneBorders = new System.Windows.Forms.CheckBox();
            toolBar = new System.Windows.Forms.ToolStrip();
            TsbZoomIn = new System.Windows.Forms.ToolStripButton();
            TsbZoomOut = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            tsbDrawWorld = new System.Windows.Forms.ToolStripButton();
            tsbDrawContinent = new System.Windows.Forms.ToolStripButton();
            tsbDrawZone = new System.Windows.Forms.ToolStripButton();
            tsbDrawCity = new System.Windows.Forms.ToolStripButton();
            toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            tsbShowPoI = new System.Windows.Forms.ToolStripButton();
            tsbShowPath = new System.Windows.Forms.ToolStripButton();
            tsbShowHousing = new System.Windows.Forms.ToolStripButton();
            tsbShowSubzone = new System.Windows.Forms.ToolStripButton();
            tsbShowQuestSphere = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            tsbNamesPoI = new System.Windows.Forms.ToolStripButton();
            tsbNamesPath = new System.Windows.Forms.ToolStripButton();
            tsbNamesHousing = new System.Windows.Forms.ToolStripButton();
            tsbNamesSubzone = new System.Windows.Forms.ToolStripButton();
            tsbNamesQuestSphere = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            pView = new System.Windows.Forms.PictureBox();
            cmsMapInfo = new System.Windows.Forms.ContextMenuStrip(components);
            tsmPoI = new System.Windows.Forms.ToolStripMenuItem();
            tsmPath = new System.Windows.Forms.ToolStripMenuItem();
            tsmMap = new System.Windows.Forms.ToolStripMenuItem();
            tsmCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            statusBar.SuspendLayout();
            gbTools.SuspendLayout();
            gbGrid.SuspendLayout();
            toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pView).BeginInit();
            cmsMapInfo.SuspendLayout();
            SuspendLayout();
            // 
            // statusBar
            // 
            statusBar.BackColor = System.Drawing.SystemColors.Control;
            statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsslViewOffset, tsslZoom, tsslCoords, tsslRuler, tsslSelectionInfo, tsslPoIInfo });
            statusBar.Location = new System.Drawing.Point(0, 507);
            statusBar.Name = "statusBar";
            statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusBar.Size = new System.Drawing.Size(733, 24);
            statusBar.TabIndex = 1;
            statusBar.Text = "statusStrip1";
            // 
            // tsslViewOffset
            // 
            tsslViewOffset.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslViewOffset.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslViewOffset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsslViewOffset.Name = "tsslViewOffset";
            tsslViewOffset.Size = new System.Drawing.Size(43, 19);
            tsslViewOffset.Text = "Offset";
            tsslViewOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            tsslViewOffset.Visible = false;
            // 
            // tsslZoom
            // 
            tsslZoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslZoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslZoom.Name = "tsslZoom";
            tsslZoom.Size = new System.Drawing.Size(53, 19);
            tsslZoom.Text = "Zoom%";
            // 
            // tsslCoords
            // 
            tsslCoords.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslCoords.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslCoords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsslCoords.Name = "tsslCoords";
            tsslCoords.Size = new System.Drawing.Size(92, 19);
            tsslCoords.Text = "-- , -- | 0°N, 0°E";
            tsslCoords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslRuler
            // 
            tsslRuler.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslRuler.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslRuler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsslRuler.Name = "tsslRuler";
            tsslRuler.Size = new System.Drawing.Size(40, 19);
            tsslRuler.Text = "-- , --";
            tsslRuler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSelectionInfo
            // 
            tsslSelectionInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslSelectionInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslSelectionInfo.Name = "tsslSelectionInfo";
            tsslSelectionInfo.Size = new System.Drawing.Size(32, 19);
            tsslSelectionInfo.Text = "Info";
            // 
            // tsslPoIInfo
            // 
            tsslPoIInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            tsslPoIInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            tsslPoIInfo.Name = "tsslPoIInfo";
            tsslPoIInfo.Size = new System.Drawing.Size(28, 19);
            tsslPoIInfo.Text = "PoI";
            // 
            // gbTools
            // 
            gbTools.Controls.Add(cbInstanceSelect);
            gbTools.Controls.Add(gbGrid);
            gbTools.Controls.Add(cbDrawMiniMap);
            gbTools.Controls.Add(cbDrawMainMap);
            gbTools.Controls.Add(cbFocus);
            gbTools.Controls.Add(cbZoneBorders);
            gbTools.Dock = System.Windows.Forms.DockStyle.Right;
            gbTools.Location = new System.Drawing.Point(605, 0);
            gbTools.Margin = new System.Windows.Forms.Padding(4);
            gbTools.Name = "gbTools";
            gbTools.Padding = new System.Windows.Forms.Padding(4);
            gbTools.Size = new System.Drawing.Size(128, 507);
            gbTools.TabIndex = 2;
            gbTools.TabStop = false;
            gbTools.Text = "Options";
            // 
            // cbInstanceSelect
            // 
            cbInstanceSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cbInstanceSelect.DropDownHeight = 240;
            cbInstanceSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbInstanceSelect.DropDownWidth = 200;
            cbInstanceSelect.Enabled = false;
            cbInstanceSelect.FormattingEnabled = true;
            cbInstanceSelect.IntegralHeight = false;
            cbInstanceSelect.Location = new System.Drawing.Point(7, 475);
            cbInstanceSelect.Margin = new System.Windows.Forms.Padding(4);
            cbInstanceSelect.Name = "cbInstanceSelect";
            cbInstanceSelect.Size = new System.Drawing.Size(114, 23);
            cbInstanceSelect.TabIndex = 12;
            cbInstanceSelect.SelectedIndexChanged += cbOptionsChanged;
            // 
            // gbGrid
            // 
            gbGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbGrid.Controls.Add(rbGridPaths);
            gbGrid.Controls.Add(cbUnitSize);
            gbGrid.Controls.Add(rbGridOff);
            gbGrid.Controls.Add(rbGridGeo);
            gbGrid.Controls.Add(rbGridCells);
            gbGrid.Controls.Add(rbGridUnits);
            gbGrid.Location = new System.Drawing.Point(8, 278);
            gbGrid.Margin = new System.Windows.Forms.Padding(4);
            gbGrid.Name = "gbGrid";
            gbGrid.Padding = new System.Windows.Forms.Padding(4);
            gbGrid.Size = new System.Drawing.Size(113, 191);
            gbGrid.TabIndex = 11;
            gbGrid.TabStop = false;
            gbGrid.Text = "Grid";
            // 
            // rbGridPaths
            // 
            rbGridPaths.AutoSize = true;
            rbGridPaths.Location = new System.Drawing.Point(7, 131);
            rbGridPaths.Name = "rbGridPaths";
            rbGridPaths.Size = new System.Drawing.Size(54, 19);
            rbGridPaths.TabIndex = 5;
            rbGridPaths.TabStop = true;
            rbGridPaths.Text = "Paths";
            rbGridPaths.UseVisualStyleBackColor = true;
            rbGridPaths.CheckedChanged += cbOptionsChanged;
            // 
            // cbUnitSize
            // 
            cbUnitSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbUnitSize.FormattingEnabled = true;
            cbUnitSize.Items.AddRange(new object[] { "1024", "512", "256", "100", "64", "32" });
            cbUnitSize.Location = new System.Drawing.Point(30, 75);
            cbUnitSize.Margin = new System.Windows.Forms.Padding(4);
            cbUnitSize.Name = "cbUnitSize";
            cbUnitSize.Size = new System.Drawing.Size(75, 23);
            cbUnitSize.TabIndex = 4;
            cbUnitSize.SelectedIndexChanged += cbOptionsChanged;
            // 
            // rbGridOff
            // 
            rbGridOff.AutoSize = true;
            rbGridOff.Checked = true;
            rbGridOff.Location = new System.Drawing.Point(9, 22);
            rbGridOff.Margin = new System.Windows.Forms.Padding(4);
            rbGridOff.Name = "rbGridOff";
            rbGridOff.Size = new System.Drawing.Size(42, 19);
            rbGridOff.TabIndex = 3;
            rbGridOff.TabStop = true;
            rbGridOff.Text = "Off";
            rbGridOff.UseVisualStyleBackColor = true;
            rbGridOff.CheckedChanged += cbOptionsChanged;
            // 
            // rbGridGeo
            // 
            rbGridGeo.AutoSize = true;
            rbGridGeo.Location = new System.Drawing.Point(7, 157);
            rbGridGeo.Margin = new System.Windows.Forms.Padding(4);
            rbGridGeo.Name = "rbGridGeo";
            rbGridGeo.Size = new System.Drawing.Size(46, 19);
            rbGridGeo.TabIndex = 2;
            rbGridGeo.TabStop = true;
            rbGridGeo.Text = "Geo";
            rbGridGeo.UseVisualStyleBackColor = true;
            rbGridGeo.CheckedChanged += cbOptionsChanged;
            // 
            // rbGridCells
            // 
            rbGridCells.AutoSize = true;
            rbGridCells.Location = new System.Drawing.Point(7, 106);
            rbGridCells.Margin = new System.Windows.Forms.Padding(4);
            rbGridCells.Name = "rbGridCells";
            rbGridCells.Size = new System.Drawing.Size(50, 19);
            rbGridCells.TabIndex = 1;
            rbGridCells.Text = "Cells";
            rbGridCells.UseVisualStyleBackColor = true;
            rbGridCells.CheckedChanged += cbOptionsChanged;
            // 
            // rbGridUnits
            // 
            rbGridUnits.AutoSize = true;
            rbGridUnits.Location = new System.Drawing.Point(9, 49);
            rbGridUnits.Margin = new System.Windows.Forms.Padding(4);
            rbGridUnits.Name = "rbGridUnits";
            rbGridUnits.Size = new System.Drawing.Size(52, 19);
            rbGridUnits.TabIndex = 0;
            rbGridUnits.Text = "Units";
            rbGridUnits.UseVisualStyleBackColor = true;
            rbGridUnits.CheckedChanged += cbOptionsChanged;
            // 
            // cbDrawMiniMap
            // 
            cbDrawMiniMap.AutoSize = true;
            cbDrawMiniMap.Checked = true;
            cbDrawMiniMap.CheckState = System.Windows.Forms.CheckState.Checked;
            cbDrawMiniMap.Location = new System.Drawing.Point(7, 49);
            cbDrawMiniMap.Margin = new System.Windows.Forms.Padding(4);
            cbDrawMiniMap.Name = "cbDrawMiniMap";
            cbDrawMiniMap.Size = new System.Drawing.Size(80, 19);
            cbDrawMiniMap.TabIndex = 10;
            cbDrawMiniMap.Text = "Road map";
            cbDrawMiniMap.UseVisualStyleBackColor = true;
            cbDrawMiniMap.CheckedChanged += cbOptionsChanged;
            // 
            // cbDrawMainMap
            // 
            cbDrawMainMap.AutoSize = true;
            cbDrawMainMap.Location = new System.Drawing.Point(7, 22);
            cbDrawMainMap.Margin = new System.Windows.Forms.Padding(4);
            cbDrawMainMap.Name = "cbDrawMainMap";
            cbDrawMainMap.Size = new System.Drawing.Size(72, 19);
            cbDrawMainMap.TabIndex = 9;
            cbDrawMainMap.Text = "Full map";
            cbDrawMainMap.UseVisualStyleBackColor = true;
            cbDrawMainMap.CheckedChanged += cbOptionsChanged;
            // 
            // cbFocus
            // 
            cbFocus.AutoSize = true;
            cbFocus.Checked = true;
            cbFocus.CheckState = System.Windows.Forms.CheckState.Checked;
            cbFocus.Location = new System.Drawing.Point(7, 155);
            cbFocus.Margin = new System.Windows.Forms.Padding(4);
            cbFocus.Name = "cbFocus";
            cbFocus.Size = new System.Drawing.Size(95, 19);
            cbFocus.TabIndex = 3;
            cbFocus.Text = "Focus border";
            cbFocus.UseVisualStyleBackColor = true;
            cbFocus.CheckedChanged += cbOptionsChanged;
            // 
            // cbZoneBorders
            // 
            cbZoneBorders.AutoSize = true;
            cbZoneBorders.Location = new System.Drawing.Point(7, 101);
            cbZoneBorders.Margin = new System.Windows.Forms.Padding(4);
            cbZoneBorders.Name = "cbZoneBorders";
            cbZoneBorders.Size = new System.Drawing.Size(96, 19);
            cbZoneBorders.TabIndex = 2;
            cbZoneBorders.Text = "Zone borders";
            cbZoneBorders.UseVisualStyleBackColor = true;
            cbZoneBorders.CheckedChanged += cbOptionsChanged;
            // 
            // toolBar
            // 
            toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { TsbZoomIn, TsbZoomOut, toolStripSeparator3, toolStripLabel3, tsbDrawWorld, tsbDrawContinent, tsbDrawZone, tsbDrawCity, toolStripLabel2, tsbShowPoI, tsbShowPath, tsbShowHousing, tsbShowSubzone, tsbShowQuestSphere, toolStripSeparator1, toolStripLabel1, tsbNamesPoI, tsbNamesPath, tsbNamesHousing, tsbNamesSubzone, tsbNamesQuestSphere, toolStripSeparator2 });
            toolBar.Location = new System.Drawing.Point(0, 0);
            toolBar.Name = "toolBar";
            toolBar.Size = new System.Drawing.Size(605, 25);
            toolBar.TabIndex = 3;
            toolBar.Text = "toolStrip1";
            // 
            // TsbZoomIn
            // 
            TsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            TsbZoomIn.Image = (System.Drawing.Image)resources.GetObject("TsbZoomIn.Image");
            TsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            TsbZoomIn.Name = "TsbZoomIn";
            TsbZoomIn.Size = new System.Drawing.Size(23, 22);
            TsbZoomIn.Text = "+";
            TsbZoomIn.Click += TsbZoomIn_Click;
            // 
            // TsbZoomOut
            // 
            TsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            TsbZoomOut.Image = (System.Drawing.Image)resources.GetObject("TsbZoomOut.Image");
            TsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            TsbZoomOut.Name = "TsbZoomOut";
            TsbZoomOut.Size = new System.Drawing.Size(23, 22);
            TsbZoomOut.Text = "-";
            TsbZoomOut.Click += TsbZoomOut_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new System.Drawing.Size(34, 22);
            toolStripLabel3.Text = "Draw";
            // 
            // tsbDrawWorld
            // 
            tsbDrawWorld.CheckOnClick = true;
            tsbDrawWorld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbDrawWorld.Image = Properties.Resources.icon_map_globe;
            tsbDrawWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbDrawWorld.Name = "tsbDrawWorld";
            tsbDrawWorld.Size = new System.Drawing.Size(23, 22);
            tsbDrawWorld.Text = "World Map";
            tsbDrawWorld.CheckedChanged += cbOptionsChanged;
            // 
            // tsbDrawContinent
            // 
            tsbDrawContinent.CheckOnClick = true;
            tsbDrawContinent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbDrawContinent.Image = Properties.Resources.icon_map_nuia;
            tsbDrawContinent.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbDrawContinent.Name = "tsbDrawContinent";
            tsbDrawContinent.Size = new System.Drawing.Size(23, 22);
            tsbDrawContinent.Text = "Continent";
            tsbDrawContinent.CheckedChanged += cbOptionsChanged;
            // 
            // tsbDrawZone
            // 
            tsbDrawZone.CheckOnClick = true;
            tsbDrawZone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbDrawZone.Image = Properties.Resources.icon_map_zone;
            tsbDrawZone.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbDrawZone.Name = "tsbDrawZone";
            tsbDrawZone.Size = new System.Drawing.Size(23, 22);
            tsbDrawZone.Text = "Zone";
            tsbDrawZone.CheckedChanged += cbOptionsChanged;
            // 
            // tsbDrawCity
            // 
            tsbDrawCity.CheckOnClick = true;
            tsbDrawCity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbDrawCity.Enabled = false;
            tsbDrawCity.Image = Properties.Resources.icon_map_city;
            tsbDrawCity.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbDrawCity.Name = "tsbDrawCity";
            tsbDrawCity.Size = new System.Drawing.Size(23, 22);
            tsbDrawCity.Text = "City/Cave";
            tsbDrawCity.CheckedChanged += cbOptionsChanged;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new System.Drawing.Size(36, 22);
            toolStripLabel2.Text = "Show";
            // 
            // tsbShowPoI
            // 
            tsbShowPoI.Checked = true;
            tsbShowPoI.CheckOnClick = true;
            tsbShowPoI.CheckState = System.Windows.Forms.CheckState.Checked;
            tsbShowPoI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowPoI.Image = Properties.Resources.icon_map_poi;
            tsbShowPoI.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowPoI.Name = "tsbShowPoI";
            tsbShowPoI.Size = new System.Drawing.Size(23, 22);
            tsbShowPoI.Text = "PoI";
            tsbShowPoI.CheckedChanged += cbOptionsChanged;
            // 
            // tsbShowPath
            // 
            tsbShowPath.Checked = true;
            tsbShowPath.CheckOnClick = true;
            tsbShowPath.CheckState = System.Windows.Forms.CheckState.Checked;
            tsbShowPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowPath.Image = Properties.Resources.icon_map_carriage;
            tsbShowPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowPath.Name = "tsbShowPath";
            tsbShowPath.Size = new System.Drawing.Size(23, 22);
            tsbShowPath.Text = "Paths";
            tsbShowPath.CheckedChanged += cbOptionsChanged;
            // 
            // tsbShowHousing
            // 
            tsbShowHousing.Checked = true;
            tsbShowHousing.CheckOnClick = true;
            tsbShowHousing.CheckState = System.Windows.Forms.CheckState.Checked;
            tsbShowHousing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowHousing.Image = Properties.Resources.icon_map_house;
            tsbShowHousing.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowHousing.Name = "tsbShowHousing";
            tsbShowHousing.Size = new System.Drawing.Size(23, 22);
            tsbShowHousing.Text = "Housing";
            tsbShowHousing.CheckedChanged += cbOptionsChanged;
            // 
            // tsbShowSubzone
            // 
            tsbShowSubzone.Checked = true;
            tsbShowSubzone.CheckOnClick = true;
            tsbShowSubzone.CheckState = System.Windows.Forms.CheckState.Checked;
            tsbShowSubzone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowSubzone.Image = Properties.Resources.icon_map_house;
            tsbShowSubzone.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowSubzone.Name = "tsbShowSubzone";
            tsbShowSubzone.Size = new System.Drawing.Size(23, 22);
            tsbShowSubzone.Text = "Subzone";
            tsbShowSubzone.CheckedChanged += cbOptionsChanged;
            // 
            // tsbShowQuestSphere
            // 
            tsbShowQuestSphere.Checked = true;
            tsbShowQuestSphere.CheckOnClick = true;
            tsbShowQuestSphere.CheckState = System.Windows.Forms.CheckState.Checked;
            tsbShowQuestSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowQuestSphere.Image = Properties.Resources.icon_map_quest;
            tsbShowQuestSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowQuestSphere.Name = "tsbShowQuestSphere";
            tsbShowQuestSphere.Size = new System.Drawing.Size(23, 22);
            tsbShowQuestSphere.Text = "Quest Spheres";
            tsbShowQuestSphere.CheckedChanged += cbOptionsChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            toolStripLabel1.Text = "Names";
            // 
            // tsbNamesPoI
            // 
            tsbNamesPoI.CheckOnClick = true;
            tsbNamesPoI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbNamesPoI.Image = Properties.Resources.icon_map_poi;
            tsbNamesPoI.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbNamesPoI.Name = "tsbNamesPoI";
            tsbNamesPoI.Size = new System.Drawing.Size(23, 22);
            tsbNamesPoI.Text = "PoI";
            tsbNamesPoI.CheckedChanged += cbOptionsChanged;
            // 
            // tsbNamesPath
            // 
            tsbNamesPath.CheckOnClick = true;
            tsbNamesPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbNamesPath.Image = Properties.Resources.icon_map_carriage;
            tsbNamesPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbNamesPath.Name = "tsbNamesPath";
            tsbNamesPath.Size = new System.Drawing.Size(23, 22);
            tsbNamesPath.Text = "Paths";
            tsbNamesPath.CheckedChanged += cbOptionsChanged;
            // 
            // tsbNamesHousing
            // 
            tsbNamesHousing.CheckOnClick = true;
            tsbNamesHousing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbNamesHousing.Image = Properties.Resources.icon_map_house;
            tsbNamesHousing.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbNamesHousing.Name = "tsbNamesHousing";
            tsbNamesHousing.Size = new System.Drawing.Size(23, 22);
            tsbNamesHousing.Text = "Housing";
            tsbNamesHousing.CheckedChanged += cbOptionsChanged;
            // 
            // tsbNamesSubzone
            // 
            tsbNamesSubzone.CheckOnClick = true;
            tsbNamesSubzone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbNamesSubzone.Image = Properties.Resources.icon_map_house;
            tsbNamesSubzone.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbNamesSubzone.Name = "tsbNamesSubzone";
            tsbNamesSubzone.Size = new System.Drawing.Size(23, 22);
            tsbNamesSubzone.Text = "Subzone";
            tsbNamesSubzone.CheckedChanged += cbOptionsChanged;
            // 
            // tsbNamesQuestSphere
            // 
            tsbNamesQuestSphere.CheckOnClick = true;
            tsbNamesQuestSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbNamesQuestSphere.Image = Properties.Resources.icon_map_quest;
            tsbNamesQuestSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbNamesQuestSphere.Name = "tsbNamesQuestSphere";
            tsbNamesQuestSphere.Size = new System.Drawing.Size(23, 22);
            tsbNamesQuestSphere.Text = "Quest Spheres";
            tsbNamesQuestSphere.CheckedChanged += cbOptionsChanged;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pView
            // 
            pView.BackColor = System.Drawing.Color.DimGray;
            pView.ContextMenuStrip = cmsMapInfo;
            pView.Dock = System.Windows.Forms.DockStyle.Fill;
            pView.Location = new System.Drawing.Point(0, 0);
            pView.Margin = new System.Windows.Forms.Padding(4);
            pView.Name = "pView";
            pView.Size = new System.Drawing.Size(605, 507);
            pView.TabIndex = 0;
            pView.TabStop = false;
            pView.Click += pView_Click;
            pView.Paint += pBox_Paint;
            pView.MouseDown += OnViewMouseDown;
            pView.MouseMove += OnViewMouseMove;
            pView.MouseUp += OnViewMouseUp;
            // 
            // cmsMapInfo
            // 
            cmsMapInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmPoI, tsmPath, tsmMap, tsmCopyToClipboard });
            cmsMapInfo.Name = "cmsMapInfo";
            cmsMapInfo.Size = new System.Drawing.Size(167, 92);
            // 
            // tsmPoI
            // 
            tsmPoI.Name = "tsmPoI";
            tsmPoI.Size = new System.Drawing.Size(166, 22);
            tsmPoI.Text = "PoI";
            // 
            // tsmPath
            // 
            tsmPath.Name = "tsmPath";
            tsmPath.Size = new System.Drawing.Size(166, 22);
            tsmPath.Text = "Path";
            // 
            // tsmMap
            // 
            tsmMap.Name = "tsmMap";
            tsmMap.Size = new System.Drawing.Size(166, 22);
            tsmMap.Text = "Map";
            // 
            // tsmCopyToClipboard
            // 
            tsmCopyToClipboard.Name = "tsmCopyToClipboard";
            tsmCopyToClipboard.Size = new System.Drawing.Size(166, 22);
            tsmCopyToClipboard.Text = "CopyToClipboard";
            tsmCopyToClipboard.Visible = false;
            tsmCopyToClipboard.Click += tsmCopyToClipboard_Click;
            // 
            // MapViewForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(733, 531);
            Controls.Add(toolBar);
            Controls.Add(pView);
            Controls.Add(gbTools);
            Controls.Add(statusBar);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "MapViewForm";
            Text = "Map View";
            FormClosing += MapViewForm_FormClosing;
            FormClosed += MapViewForm_FormClosed;
            Load += MapViewForm_Load;
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            gbTools.ResumeLayout(false);
            gbTools.PerformLayout();
            gbGrid.ResumeLayout(false);
            gbGrid.PerformLayout();
            toolBar.ResumeLayout(false);
            toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pView).EndInit();
            cmsMapInfo.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ComboBox cbUnitSize;
        private System.Windows.Forms.ToolStripStatusLabel tsslPoIInfo;
        private System.Windows.Forms.ContextMenuStrip cmsMapInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmPoI;
        private System.Windows.Forms.ToolStripMenuItem tsmPath;
        private System.Windows.Forms.ToolStripMenuItem tsmMap;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyToClipboard;
        public System.Windows.Forms.ComboBox cbInstanceSelect;
        private System.Windows.Forms.RadioButton rbGridPaths;
        private System.Windows.Forms.ToolStripButton TsbZoomIn;
        private System.Windows.Forms.ToolStripButton TsbZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
using AAEmu.DBDefs;
using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AAEmu.DBViewer
{
    public partial class MapViewForm : Form
    {
        public static MapViewForm ThisForm;
        private PictureBox pb = new PictureBox();
        private Point viewOffset = new Point(0, 0);
        private Point cursorCoords = new Point(0, 0);
        private Point rulerCoords = new Point(0, 0);
        private string cursorZones = string.Empty;
        private Point startDragPos = new Point();
        private Point startDragOffset = new Point();
        private bool isDragging = false;
        private bool hasDragged = false;
        private float viewScale = 1f;
        private List<MapViewMap> allmaps = new List<MapViewMap>();
        private MapViewMap topMostMap = null;
        private List<MapViewPoI> poi = new List<MapViewPoI>();
        private List<MapViewPath> paths = new List<MapViewPath>();
        private List<MapViewPath> housing = new List<MapViewPath>();
        private List<MapViewPoI> quest_sign_sphere = new List<MapViewPoI>();
        private RectangleF FocusBorder = new RectangleF();

        public Point ViewOffset { get => viewOffset; set { viewOffset = value; updateStatusBar(); } }

        public MapViewForm()
        {
            MapViewForm.ThisForm = this;
            InitializeComponent();
            using (var loadingform = new LoadingForm())
            {
                loadingform.Show();
                loadingform.ShowInfo("Preparing map data ...");

                pb.SetBounds(0, 0, 35536, 35536);

                pView.MouseWheel += new MouseEventHandler(MapViewOnMouseWheel);
                viewOffset.Y = 20000;
                viewScale = 0.05f;

                ClearPoI();
                ClearMaps();
                MapViewHelper.PopulateList();
                MapViewHelper.PopulateMiniMapList();
                AddMap(new RectangleF(-14731, 46, 64651, 38865), "Erenor", "main_world", MapLevel.WorldMap, 0).InstanceName = "main_world";
                AddMap(new RectangleF(-6758, 2673, 32130, 19308), "Nuia", "land_west", MapLevel.Continent, 0).InstanceName = "main_world";
                AddMap(new RectangleF(10627, 1632, 26024, 15636), "Haranya", "land_east", MapLevel.Continent, 0).InstanceName = "main_world";
                AddMap(new RectangleF(7035, 21504, 24743, 14883), "Auroria", "land_origin", MapLevel.Continent, 0).InstanceName = "main_world";
                /*
                foreach (var wgv in AADB.DB_World_Groups)
                {
                    var wg = wgv.Value;
                    var level = MapLevel.None;
                    if ((wg.PosAndSize.Width <= 0) || (wg.PosAndSize.Height <= 0))
                        continue;
                        //level = MapLevel.None;
                    else
                    if (wg.target_id <= 1)
                        level = MapLevel.WorldMap;
                    else
                        level = MapLevel.Continent;
                    var imageName = wg.name;

                    var mapref = MapViewImageHelper.GetRefByImageMapId(wg.image_map);

                    if (mapref != null)
                    {
                        imageName = mapref.FileName;
                        level = mapref.Level;
                    }

                    AddMap(wg.PosAndSize.X, wg.PosAndSize.Y, wg.PosAndSize.Width, wg.PosAndSize.Height,wg.name,
                        mapFolder, imageName, level);
                }
                */

                var locale = Properties.Settings.Default.DefaultGameLanguage;

                foreach (var zgv in AADB.DB_Zone_Groups)
                {
                    var zg = zgv.Value;

                    // Skip non-main world zones
                    //if (zg.target_id <= 1)
                    //    continue;

                    if ((zg.PosAndSize.Width <= 0) || (zg.PosAndSize.Height <= 0))
                        continue;

                    var iref = MapViewHelper.GetRefByImageMapId(zg.image_map);
                    var mapFileName = zg.name;
                    var level = MapLevel.Zone;
                    if (iref != null)
                    {
                        level = iref.Level;
                        if (MainForm.ThisForm.pak.isOpen)
                        {
                            var fList = iref.GetPossibleFileNames(locale);
                            // Here we only check if the name has a valid filename in the pak
                            // When actually loading, this will be repeated again to get the correct file
                            foreach (var fName in fList)
                            {
                                if (MainForm.ThisForm.pak.FileExists(fName))
                                {
                                    mapFileName = iref.BaseFileName;
                                    break;
                                }
                            }
                        }
                    }

                    if (level < MapLevel.Zone)
                        continue;

                    var m = AddMap(zg.PosAndSize, zg.display_textLocalized + " (" + zg.id.ToString() + ")", mapFileName, level, zg.id);
                    //var m = AddMap(zg.PosAndSize, zg.display_textLocalized, "game/ui/map/road/", zg.name + "_road_100", MapLevel.Zone);

                    if (zg.faction_chat_region_id == 2)
                        m.MapBorderColor = Color.DarkCyan;
                    if (zg.faction_chat_region_id == 3)
                        m.MapBorderColor = Color.DarkGreen;
                    if (zg.faction_chat_region_id == 4)
                        m.MapBorderColor = Color.DarkRed;
                    // If it has the Rough Sea (Turbulance) buff, it's the ocean, use a blue border
                    if (zg.buff_id == 7743)
                    {
                        m.MapBorderColor = Color.Navy;
                        // m.MapLevel = MapLevel.Continent;
                    }
                }

                // End loading
            }

        }

        public static MapViewForm GetMap()
        {
            if (ThisForm != null)
                return ThisForm;
            else
                return new MapViewForm();
        }

        private string GetCursorZones()
        {
            MapViewMap newTopMap = null;
            var res = string.Empty;
            var cursorZones = new List<MapViewMap>();
            foreach (var map in allmaps)
            {
                if (map.MapLevel <= MapLevel.Continent)
                    continue;
                if (map.InstanceName != cbInstanceSelect.Text)
                    continue;
                if (map.ZoneCoords.Contains(cursorCoords))
                    cursorZones.Add(map);
            }

            // Check if cursor is still inside the current zone
            if (cursorZones.Contains(topMostMap) && (topMostMap != null))
            {
                // Leave everthing as is
            }
            else
            {
                foreach (var z in cursorZones)
                {
                    if (newTopMap == null)
                    {
                        newTopMap = z;
                    }
                    else
                    {
                        if ((newTopMap.ZoneCoords.Width * newTopMap.ZoneCoords.Height) >= (z.ZoneCoords.Width * z.ZoneCoords.Height))
                        {
                            newTopMap = z;
                        }
                    }

                }
                topMostMap = newTopMap;
            }

            foreach (var z in cursorZones)
            {
                if (z == topMostMap)
                    res += "[" + z.Name + "], ";
                else
                    res += z.Name + ", ";
            }
            return res;
        }

        private void updateStatusBar()
        {
            tsslZoom.Text = "Zoom: " + (viewScale * 100).ToString() + "%";
            /*
            if (isDragging)
            {
                tsslViewOffset.Text = "drag from X:" + startDragPos.X.ToString() + " Y:" + startDragPos.Y.ToString();
            }
            */
            tsslViewOffset.Text = "View X:" + ViewOffset.X.ToString() + " Y:" + ViewOffset.Y.ToString();

            if ((rulerCoords.X != 0) && (rulerCoords.Y != 0))
            {
                var rulerF = new Vector2(rulerCoords.X, rulerCoords.Y);
                var cursorF = new Vector2(cursorCoords.X, cursorCoords.Y);
                var offF = Vector2.Subtract(cursorF, rulerF);
                float dist = Vector2.Distance(cursorF, rulerF);
                if ((dist < -16f) || (dist > 16))
                    tsslRuler.Text = "X:" + rulerCoords.X.ToString() + " Y:" + rulerCoords.Y.ToString() + " => Off X:" + offF.X.ToString() + " Y:" + offF.Y.ToString() + " (D: " + dist.ToString("0.0") + ")";
                else
                    tsslRuler.Text = "X:" + rulerCoords.X.ToString() + " Y:" + rulerCoords.Y.ToString();
            }
            else
            {
                tsslRuler.Text = "X:-- Y:--";
            }

            var lastTopMap = topMostMap;
            tsslCoords.Text = "X:" + cursorCoords.X.ToString() + " Y:" + cursorCoords.Y.ToString() + " | " + AADB.CoordToSextant(cursorCoords.X, cursorCoords.Y);
            tsslSelectionInfo.Text = "inside: " + GetCursorZones();

            if (topMostMap != lastTopMap)
                pView.Refresh();
        }

        private void MapViewOnMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (viewScale >= 2f)
                    viewScale += 0.25f;
                else
                    viewScale += 0.025f;
            }

            if (e.Delta < 0)
            {
                if (viewScale >= 2f)
                    viewScale -= 0.25f;
                else
                    viewScale -= 0.025f;
            }

            if (viewScale < 0.025f)
                viewScale = 0.025f;
            pView.Invalidate();
            updateStatusBar();
        }

        private Point CoordToPixel(Point coord) => CoordToPixel(coord.X, coord.Y);

        private Point CoordToPixel(float x, float y)
        {
            return new Point((int)x, (int)y * -1);
        }

        private void DrawCross(Graphics g, float x, float y, Color color, string name)
        {
            int crossSize = (int)(6f / viewScale) + 1;
            var pen = new Pen(color);
            var pos = CoordToPixel(x, y);
            g.DrawLine(pen, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y - crossSize, ViewOffset.X + x, ViewOffset.Y + pos.Y + crossSize);
            g.DrawLine(pen, ViewOffset.X + pos.X - crossSize, ViewOffset.Y + pos.Y, ViewOffset.X + pos.X + crossSize, ViewOffset.Y + pos.Y);
            if (name != string.Empty)
            {
                var f = new Font(Font.FontFamily, 12f / viewScale);
                var br = new SolidBrush(color);
                g.DrawString(name, f, br, ViewOffset.X + pos.X + crossSize, ViewOffset.Y + pos.Y - crossSize);
            }
        }

        private void DrawRadius(Graphics g, float x, float y, Color color, string name, float radius)
        {
            var pen = new Pen(color);
            var pos = CoordToPixel(x, y);
            var tl = CoordToPixel(x - radius, y + radius);
            g.DrawEllipse(pen, new RectangleF(ViewOffset.X + tl.X, ViewOffset.Y + tl.Y, radius * 2f, radius * 2f));

            // g.DrawLine(pen, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y, ViewOffset.X + x, ViewOffset.Y + pos.Y);
            // g.DrawLine(pen, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);
            if (name != string.Empty)
            {
                var f = new Font(Font.FontFamily, 12f / viewScale);
                var br = new SolidBrush(color);
                g.DrawString(name, f, br, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);
            }
        }

        private void DrawMap(Graphics g, MapViewMap map)
        {
            var showMap = true;
            switch (map.MapLevel)
            {
                case MapLevel.WorldMap:
                    showMap = tsbDrawWorld.Checked;
                    break;
                case MapLevel.Continent:
                    showMap = tsbDrawContinent.Checked;
                    break;
                case MapLevel.Zone:
                    showMap = tsbDrawZone.Checked;
                    break;
                case MapLevel.City:
                    showMap = tsbDrawCity.Checked;
                    break;
            }
            if (!showMap)
                return;

            var pen = new Pen(map.MapBorderColor);
            var roadpen = Pens.Black;

            var mappos = CoordToPixel(map.ZoneCoords.X, map.ZoneCoords.Y);

            var zoneBorderRect = new RectangleF(ViewOffset.X + mappos.X, ViewOffset.Y + mappos.Y - map.ZoneCoords.Height, map.ZoneCoords.Width, map.ZoneCoords.Height);

            // Main Map
            if ((map.MapBitmapImage != null) && ((map.MapLevel <= MapLevel.WorldMap) || cbDrawMainMap.Checked))
            {
                g.DrawImage(map.MapBitmapImage, zoneBorderRect);
            }

            var roadBorderRect = new RectangleF();
            // Road Map Overlay (we need to read image dimensions of the mainmap, so it also requires that one loaded)
            if ((map.MapBitmapImage != null) && (map.RoadBitmapImage != null))
            {
                var roadsize = new SizeF(
                    map.ZoneCoords.Width * map.RoadMapCoords.Width / map.ImgCoords.Width,
                    map.ZoneCoords.Height * map.RoadMapCoords.Height / map.ImgCoords.Height
                    );
                var roadCoordsOffset = new PointF(
                    map.RoadMapOffset.X / map.ImgCoords.Width * map.ZoneCoords.Width,
                    map.RoadMapOffset.Y / map.ImgCoords.Height * map.ZoneCoords.Height
                    );
                /*
                var roadsize = new SizeF(
                    map.ZoneCoords.Width * map.RoadBitmapImage.Width / (float)map.MapBitmapImage.Width,
                    map.ZoneCoords.Height * map.RoadBitmapImage.Height / (float)map.MapBitmapImage.Height
                    );
                var roadCoordsOffset = new PointF(
                    map.RoadMapOffset.X / (float)map.MapBitmapImage.Width * map.ZoneCoords.Width,
                    map.RoadMapOffset.Y / (float)map.MapBitmapImage.Height * map.ZoneCoords.Height
                    );
                */

                roadBorderRect = new RectangleF(
                    ViewOffset.X + mappos.X + roadCoordsOffset.X,
                    ViewOffset.Y + mappos.Y + roadCoordsOffset.Y - map.ZoneCoords.Height,
                    roadsize.Width, roadsize.Height);

                if (cbDrawMiniMap.Checked)
                    g.DrawImage(map.RoadBitmapImage, roadBorderRect);
            }

            if (cbZoneBorders.Checked && (map.Name != string.Empty))
            {
                g.DrawRectangle(pen, zoneBorderRect.X, zoneBorderRect.Y, zoneBorderRect.Width, zoneBorderRect.Height);
                g.DrawRectangle(roadpen, roadBorderRect.X, roadBorderRect.Y, roadBorderRect.Width, roadBorderRect.Height);

                var f = new Font(Font.FontFamily, 100f);
                var br = new SolidBrush(map.MapBorderColor);
                g.DrawString(map.Name, f, br, ViewOffset.X + mappos.X, ViewOffset.Y + mappos.Y);
            }
        }

        private void DrawGrid(Graphics g)
        {
            var MaxPointX = 32768;
            var MaxPointY = 32768 + 4096;

            var inst = MapViewWorldXML.GetInstanceByName(cbInstanceSelect.Text);
            if (inst != null)
            {
                MaxPointX = inst.CellCount.X * 1024;
                MaxPointY = inst.CellCount.Y * 1024;
            }


            var fnt = new Font(Font.FontFamily, 10f / viewScale);
            var br = new System.Drawing.SolidBrush(Color.White);
            var smallGridSize = 1024; // Cell size (resolution in heightmap is actualy 2m instead of 1m, so here we use 1024 instead of 512)
            if ((viewScale > 0.5f) && !rbGridCells.Checked && !rbGridGeo.Checked)
            {
                smallGridSize = 32; // Sector
                fnt = new Font(Font.FontFamily, 7f / viewScale);
            }

            for (var x = 0; x <= MaxPointX; x += smallGridSize)
            {
                var gridString = x.ToString();
                var off = 0;
                var p = x % 4096 == 0 ? System.Drawing.Pens.LightGray : System.Drawing.Pens.DarkGray;
                if (rbGridCells.Checked || rbGridGeo.Checked)
                {
                    if (((int)x % 1024) == 0)
                    {
                        if (rbGridCells.Checked)
                        {
                            gridString = (x / 1024).ToString();
                            off = 512;
                        }
                        else
                        {
                            var xx = ((x / 1024) - 21);
                            if (xx >= 0)
                                gridString = "E " + xx.ToString() + "°";
                            else
                                gridString = "W " + (xx * -1).ToString() + "°";
                            if (xx == 0)
                                p = System.Drawing.Pens.DeepPink;
                        }
                    }
                    else
                        gridString = " ";
                }
                var stringSize = g.MeasureString(gridString, fnt);
                var hTop = CoordToPixel(x, MaxPointY);
                var hBottom = CoordToPixel(x, 0);
                g.DrawLine(p, ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y, ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);

                var ht = new Point(ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y);
                var hb = new Point(ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);
                if (ht.Y < 0)
                    ht.Y = (int)stringSize.Height;
                if (hb.Y > (int)(pView.Height / viewScale) - (int)stringSize.Height)
                    hb.Y = (int)(pView.Height / viewScale) - (int)stringSize.Height;
                g.DrawString(gridString, fnt, br, ht.X - (stringSize.Width / 2) + off, ht.Y - stringSize.Height);
                g.DrawString(gridString, fnt, br, hb.X - (stringSize.Width / 2) + off, hb.Y);
            }
            for (var y = 0; y <= MaxPointY; y += smallGridSize)
            {
                var gridString = y.ToString();
                var off = 0;
                var p = y % 4096 == 0 ? System.Drawing.Pens.LightGray : System.Drawing.Pens.DarkGray;
                if (rbGridCells.Checked || rbGridGeo.Checked)
                {
                    if (((int)y % 1024) == 0)
                    {
                        if (rbGridCells.Checked)
                        {
                            gridString = (y / 1024).ToString();
                            off = 512;
                        }
                        else
                        {
                            var yy = ((y / 1024) - 28);
                            if (yy == 0)
                                p = System.Drawing.Pens.DeepPink;
                            if (yy >= 0)
                                gridString = "N " + yy.ToString() + "°";
                            else
                                gridString = "S " + (yy * -1).ToString() + "°";
                        }
                    }
                    else
                        gridString = " ";
                }
                var stringSize = g.MeasureString(gridString, fnt);
                var hTop = CoordToPixel(0, y);
                var hBottom = CoordToPixel(MaxPointX, y);
                g.DrawLine(p, ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y, ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);

                var ht = new Point(ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y);
                var hb = new Point(ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);
                if (ht.X < 0)
                    ht.X = (int)stringSize.Width;
                if (hb.X > (int)(pView.Width / viewScale) - (int)stringSize.Width)
                    hb.X = (int)(pView.Width / viewScale) - (int)stringSize.Width;
                g.DrawString(gridString, fnt, br, ht.X - stringSize.Width, ht.Y - (stringSize.Height / 2) - off);
                g.DrawString(gridString, fnt, br, hb.X, hb.Y - (stringSize.Height / 2) - off);
            }
        }

        private void DrawPath(Graphics g, MapViewPath path, bool showName)
        {
            if (path.allpoints.Count <= 0)
                return;

            bool first = true;
            Point lastPos = new Point(0, 0);
            foreach (var p in path.allpoints)
            {
                if (first)
                {
                    // Draw Startpoint
                    first = false;
                    DrawCross(g, p.X, p.Y, path.Color, showName ? path.PathName : "");
                }
                else
                {
                    // Draw Line
                    var pen = new Pen(path.Color);
                    pen.Width = (int)(3f / viewScale) + 1;
                    if (path.DrawStyle == 1)
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    if (path.DrawStyle == 2)
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    var pos = CoordToPixel(p.X, p.Y);
                    var lpos = CoordToPixel(lastPos.X, lastPos.Y);
                    g.DrawLine(pen, ViewOffset.X + lpos.X, ViewOffset.Y + lpos.Y, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);

                }
                lastPos = new Point((int)p.X, (int)p.Y);
            }
        }

        private void OnViewPaint(object sender, PaintEventArgs e)
        {
            // Draw the map, or something like that

            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            g.ScaleTransform(viewScale, viewScale);

            cursorZones = string.Empty;
            foreach (var level in Enum.GetValues(typeof(MapLevel)))
            {
                foreach (var map in allmaps)
                    if ((map.MapLevel == (MapLevel)level) && (map.InstanceName == cbInstanceSelect.Text))
                        DrawMap(g, map);
            }

            if (topMostMap != null)
                DrawMap(g, topMostMap);

            // Draw Grid
            if (rbGridUnits.Checked || rbGridCells.Checked || rbGridGeo.Checked)
                DrawGrid(g);

            // Draw Points of Interest
            if (tsbShowPoI.Checked)
                foreach (var p in poi)
                {
                    if (p.Radius <= 3f)
                        DrawCross(g, p.CoordX, p.CoordY, p.PoIColor, tsbNamesPoI.Checked ? p.Name : "");
                    else
                        DrawRadius(g, p.CoordX, p.CoordY, p.PoIColor, tsbNamesPoI.Checked ? p.Name : "", p.Radius);
                }

            // Draw Quest Sign spheres
            if (tsbShowQuestSphere.Checked)
                foreach (var p in quest_sign_sphere)
                {
                    if (p.Radius <= 3f)
                        DrawCross(g, p.CoordX, p.CoordY, p.PoIColor, tsbNamesQuestSphere.Checked ? p.Name : "");
                    else
                        DrawRadius(g, p.CoordX, p.CoordY, p.PoIColor, tsbNamesQuestSphere.Checked ? p.Name : "", p.Radius);
                }

            // Draw Paths
            if (tsbShowPath.Checked)
                foreach (var path in paths)
                    DrawPath(g, path, tsbNamesPath.Checked);

            // Draw Housing
            if (tsbShowHousing.Checked)
                foreach (var houseArea in housing)
                    DrawPath(g, houseArea, tsbNamesHousing.Checked);

            if (cbFocus.Checked)
                g.DrawRectangle(Pens.OrangeRed, ViewOffset.X + FocusBorder.X, ViewOffset.Y - FocusBorder.Y - FocusBorder.Height, FocusBorder.Width, FocusBorder.Height);

            // Ruler
            if ((rulerCoords.X != 0) || (rulerCoords.Y != 0))
            {
                DrawCross(g, rulerCoords.X, rulerCoords.Y, Color.Red, "");
                // Draw Line
                var pen = new Pen(Color.Red);
                pen.Width = (int)(3f / viewScale) + 1;
                var pos = CoordToPixel(rulerCoords.X, rulerCoords.Y);
                var lpos = CoordToPixel(cursorCoords.X, cursorCoords.Y);
                g.DrawLine(pen, ViewOffset.X + lpos.X, ViewOffset.Y + lpos.Y, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);
            }

            updateStatusBar();
        }

        private Bitmap PackedImageToBitmap(string packedFileFolder, string packedFileName)
        {
            if (MainForm.ThisForm.pak.isOpen)
            {
                var fn = packedFileFolder + packedFileName;

                if (MainForm.ThisForm.pak.FileExists(packedFileFolder + Properties.Settings.Default.DefaultGameLanguage + "/" + packedFileName))
                {
                    fn = packedFileFolder + Properties.Settings.Default.DefaultGameLanguage + "/" + packedFileName;
                }
                return PackedImageToBitmap(fn);
            }
            return null;
        }

        private Bitmap PackedImageToBitmap(string fn)
        {
            if (MainForm.ThisForm.pak.isOpen)
            {

                if (MainForm.ThisForm.pak.FileExists(fn))
                {
                    try
                    {
                        var fStream = MainForm.ThisForm.pak.ExportFileAsStream(fn);
                        var fif = FREE_IMAGE_FORMAT.FIF_DDS;
                        FIBITMAP fiBitmap = FreeImage.LoadFromStream(fStream, ref fif);
                        var bmp = FreeImage.GetBitmap(fiBitmap);
                        return bmp;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        private void OnViewMouseMove(object sender, MouseEventArgs e)
        {
            cursorCoords = new Point((int)(e.X / viewScale) - viewOffset.X, ((int)(e.Y / viewScale) - viewOffset.Y) * -1);
            if (isDragging)
            {
                hasDragged = true;
                var dx = (int)Math.Floor((startDragPos.X - e.X) / viewScale);
                var dy = (int)Math.Floor((startDragPos.Y - e.Y) / viewScale);
                ViewOffset = new Point(startDragOffset.X - dx, startDragOffset.Y - dy);
                pView.Refresh();
            }
            else
            {
                if ((rulerCoords.X != 0) || (rulerCoords.Y != 0))
                    pView.Refresh();
            }
            updateStatusBar();
        }

        private void OnViewMouseDown(object sender, MouseEventArgs e)
        {
            startDragPos = new Point(e.X, e.Y);
            startDragOffset = new Point(ViewOffset.X, ViewOffset.Y);
            isDragging = true;
            pView.Invalidate();
            updateStatusBar();
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            pView.Invalidate();
            updateStatusBar();
        }

        public MapViewPoI AddPoI(float x, float y, string name, Color col, float radius = 0f)
        {
            var newPoi = new MapViewPoI();
            newPoi.CoordX = x;
            newPoi.CoordY = y;
            newPoi.Name = name;
            newPoi.PoIColor = col;
            newPoi.Radius = radius;
            poi.Add(newPoi);
            return newPoi;
        }

        public void ClearPoI()
        {
            poi.Clear();
        }

        public int GetPoICount()
        {
            return poi.Count;
        }


        public MapViewPoI AddQuestSphere(float x, float y, string name, Color col, float radius = 0f)
        {
            var newPoi = new MapViewPoI();
            newPoi.CoordX = x;
            newPoi.CoordY = y;
            newPoi.Name = name;
            newPoi.PoIColor = col;
            newPoi.Radius = radius;
            quest_sign_sphere.Add(newPoi);
            return newPoi;
        }

        public void ClearQuestSpheres()
        {
            quest_sign_sphere.Clear();
        }

        public int GetQuestSphereCount()
        {
            return quest_sign_sphere.Count;
        }

        public MapViewMap AddMap(RectangleF mapLoc, string displayName, string fileName, MapLevel level, long zone_group_id)
        {
            var newMap = new MapViewMap();
            newMap.ZoneCoords = mapLoc;
            newMap.Name = displayName;
            newMap.MapLevel = level;
            newMap.MapImageFile = fileName;

            if (zone_group_id > 0)
            {
                var inst = MapViewWorldXML.FindInstanceByZoneGroup(zone_group_id);
                if (inst != null)
                    newMap.InstanceName = inst.WorldName;
            }

            // MainMap
            var fn = string.Empty;
            if (MainForm.ThisForm.pak.isOpen)
            {
                var fList = MapViewImageRef.ListPossibleFileNames(fileName, Properties.Settings.Default.DefaultGameLanguage);
                foreach (var fName in fList)
                {
                    if (MainForm.ThisForm.pak.FileExists(fName))
                    {
                        fn = fName;
                        newMap.MapImageFile = fn;
                        break;
                    }
                }
            }
            if (fn != string.Empty)
                newMap.MapBitmapImage = PackedImageToBitmap(fn);
            else
                newMap.MapBitmapImage = PackedImageToBitmap("game/ui/map/world/", fileName + ".dds");

            if (newMap.MapBitmapImage != null)
                newMap.ImgCoords = new RectangleF(0, 0, newMap.MapBitmapImage.Width, newMap.MapBitmapImage.Height);
            else
                newMap.ImgCoords = new RectangleF(0, 0, 928, 556); // This is the common size for all/most maps

            if (newMap.ImgCoords.Width > 1024)
                newMap.ImgCoords = new RectangleF(0, 0, newMap.ImgCoords.Width / 2f, newMap.ImgCoords.Height / 2f);

            // RoadMap
            fn = string.Empty;
            var roadRef = MapViewHelper.GetMiniMapRefByZoneGroup(zone_group_id, 100, newMap.MapLevel);
            if (roadRef != null)
            {
                newMap.RoadMapOffset = roadRef.Offset;
                newMap.RoadMapCoords = roadRef.Rect;
                if (MainForm.ThisForm.pak.isOpen)
                {
                    var fList = roadRef.GetPossibleFileNames(Properties.Settings.Default.DefaultGameLanguage);
                    foreach (var fName in fList)
                    {
                        if (MainForm.ThisForm.pak.FileExists(fName))
                        {
                            fn = fName;
                            newMap.RoadImageFile = fn;
                            break;
                        }
                    }
                }
            }
            if (fn != string.Empty)
                newMap.RoadBitmapImage = PackedImageToBitmap(fn);
            else
                newMap.RoadBitmapImage = PackedImageToBitmap("game/ui/map/world/", fileName + "_road_100.dds");


            allmaps.Add(newMap);
            return newMap;
        }

        public void ClearMaps()
        {
            allmaps.Clear();
        }

        public void AddPath(MapViewPath path)
        {
            paths.Add(path);
        }

        public void ClearPaths()
        {
            paths.Clear();
        }

        public int GetPathCount()
        {
            return paths.Count;
        }

        public void AddHousing(MapViewPath path)
        {
            housing.Add(path);
        }

        public void ClearHousing()
        {
            housing.Clear();
        }

        public int GetHousingCount()
        {
            return housing.Count;
        }

        private void MapViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            if (e.CloseReason != CloseReason.MdiFormClosing)
            {
                // Clear some memory
                poi.Clear();
                paths.Clear();
                foreach (var m in allmaps)
                {
                    if (m.MapBitmapImage != null)
                        m.MapBitmapImage.Dispose();
                    if (m.RoadBitmapImage != null)
                        m.RoadBitmapImage.Dispose();
                }
                allmaps.Clear();
            }
            */
            // ThisForm = null;
        }

        public void FocusAll(bool focusPoIs, bool focusPaths, bool focusSpheres)
        {
            var first = true;
            FocusBorder = new RectangleF();
            if (focusPoIs)
            {
                foreach (var p in poi)
                {
                    if (first)
                    {
                        first = false;
                        FocusBorder.X = p.CoordX;
                        FocusBorder.Y = p.CoordY;
                        FocusBorder.Width = 1;
                        FocusBorder.Height = 1;
                    }
                    else
                    {
                        if (p.CoordX < FocusBorder.X)
                        {
                            FocusBorder.Width = FocusBorder.Width + FocusBorder.X - p.CoordX;
                            FocusBorder.X = p.CoordX;
                        }
                        if (p.CoordX > FocusBorder.Right)
                            FocusBorder.Width = p.CoordX - FocusBorder.Left + 1;

                        if (p.CoordY < FocusBorder.Y)
                        {
                            FocusBorder.Height = FocusBorder.Height + FocusBorder.Y - p.CoordY;
                            FocusBorder.Y = p.CoordY;
                        }
                        if (p.CoordY > FocusBorder.Bottom)
                            FocusBorder.Height = p.CoordY - FocusBorder.Top + 1;
                    }
                }

            }

            if (focusPaths)
            {
                foreach (var pt in paths)
                {
                    foreach (var p in pt.allpoints)
                    {
                        if (first)
                        {
                            first = false;
                            FocusBorder.X = p.X;
                            FocusBorder.Y = p.Y;
                            FocusBorder.Width = 1;
                            FocusBorder.Height = 1;
                        }
                        else
                        {
                            if (p.X < FocusBorder.X)
                            {
                                FocusBorder.Width = FocusBorder.Width + FocusBorder.X - p.X;
                                FocusBorder.X = p.X;
                            }
                            if (p.X > FocusBorder.Right)
                                FocusBorder.Width = p.X - FocusBorder.Left + 1;

                            if (p.Y < FocusBorder.Y)
                            {
                                FocusBorder.Height = FocusBorder.Height + FocusBorder.Y - p.Y;
                                FocusBorder.Y = p.Y;
                            }
                            if (p.Y > FocusBorder.Bottom)
                                FocusBorder.Height = p.Y - FocusBorder.Top + 1;
                        }
                    }
                }

            }

            if (focusSpheres)
            {
                foreach (var p in quest_sign_sphere)
                {
                    if (first)
                    {
                        first = false;
                        FocusBorder.X = p.CoordX;
                        FocusBorder.Y = p.CoordY;
                        FocusBorder.Width = 1;
                        FocusBorder.Height = 1;
                    }
                    else
                    {
                        if (p.CoordX < FocusBorder.X)
                        {
                            FocusBorder.Width = FocusBorder.Width + FocusBorder.X - p.CoordX;
                            FocusBorder.X = p.CoordX;
                        }
                        if (p.CoordX > FocusBorder.Right)
                            FocusBorder.Width = p.CoordX - FocusBorder.Left + 1;

                        if (p.CoordY < FocusBorder.Y)
                        {
                            FocusBorder.Height = FocusBorder.Height + FocusBorder.Y - p.CoordY;
                            FocusBorder.Y = p.CoordY;
                        }
                        if (p.CoordY > FocusBorder.Bottom)
                            FocusBorder.Height = p.CoordY - FocusBorder.Top + 1;
                    }
                }

            }


            if ((FocusBorder.Width > 0) && (FocusBorder.Height > 0))
            {
                var center = CoordToPixel(new Point((int)(FocusBorder.X + (FocusBorder.Width / 2f)), (int)(FocusBorder.Y + (FocusBorder.Height / 2))));
                viewOffset.X = (center.X * -1);
                viewOffset.Y = (center.Y * -1) + ((int)FocusBorder.Height / 2);
                viewOffset.X += (int)Math.Round(((float)pView.Width / viewScale) / 2f);
                viewOffset.Y += (int)Math.Round(((float)pView.Height / viewScale) / 2f);
            }
            pView.Refresh();
        }

        private void pBox_Paint(object sender, PaintEventArgs e)
        {
            OnViewPaint(sender, e);
        }

        private void MapViewForm_Load(object sender, EventArgs e)
        {
            tsbDrawWorld.Checked = Properties.Settings.Default.MapShowWorld;
            tsbDrawContinent.Checked = Properties.Settings.Default.MapShowContinent;
            tsbDrawZone.Checked = Properties.Settings.Default.MapShowZone;
            tsbDrawCity.Checked = Properties.Settings.Default.MapShowCity;

            cbDrawMainMap.Checked = Properties.Settings.Default.MapShowMainMap;
            cbDrawMiniMap.Checked = Properties.Settings.Default.MapShowRoadMap;

            rbGridOff.Checked = Properties.Settings.Default.GridMode == 0;
            rbGridUnits.Checked = Properties.Settings.Default.GridMode == 1;
            rbGridCells.Checked = Properties.Settings.Default.GridMode == 2;
            rbGridGeo.Checked = Properties.Settings.Default.GridMode == 3;

            MapViewZonePathOffsets.LoadOffsetsFromFile();

            cbInstanceSelect.Items.Clear();
            if (MapViewWorldXML.instances != null)
            {
                foreach (var inst in MapViewWorldXML.instances)
                {
                    cbInstanceSelect.Items.Add(inst.WorldName);
                    if (inst.WorldName == MapViewWorldXML.main_world.WorldName)
                        cbInstanceSelect.SelectedIndex = cbInstanceSelect.Items.Count - 1;
                }
            }
            cbInstanceSelect.Enabled = true;
        }

        private void pView_Click(object sender, EventArgs e)
        {
            if (!hasDragged)
            {
                var rulerF = new Vector2(rulerCoords.X, rulerCoords.Y);
                var cursorF = new Vector2(cursorCoords.X, cursorCoords.Y);
                float dist = Vector2.Distance(cursorF, rulerF);
                if ((dist < -16f) || (dist > 16))
                    rulerCoords = cursorCoords;
                else
                    rulerCoords = new Point();
            }
            hasDragged = false;
            updateStatusBar();
        }


        private void cbOptionsChanged(object sender, EventArgs e)
        {
            pView.Refresh();
            updateStatusBar();
        }

        private void MapViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.MapShowWorld = tsbDrawWorld.Checked;
            Properties.Settings.Default.MapShowContinent = tsbDrawContinent.Checked;
            Properties.Settings.Default.MapShowZone = tsbDrawZone.Checked;
            Properties.Settings.Default.MapShowCity = tsbDrawCity.Checked;

            Properties.Settings.Default.MapShowMainMap = cbDrawMainMap.Checked;
            Properties.Settings.Default.MapShowRoadMap = cbDrawMiniMap.Checked;

            if (rbGridOff.Checked)
                Properties.Settings.Default.GridMode = 0;
            if (rbGridUnits.Checked)
                Properties.Settings.Default.GridMode = 1;
            if (rbGridCells.Checked)
                Properties.Settings.Default.GridMode = 2;
            if (rbGridGeo.Checked)
                Properties.Settings.Default.GridMode = 3;
            Properties.Settings.Default.Save();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void gbTools_Enter(object sender, EventArgs e)
        {

        }
    }


    public class MapViewPoI
    {
        public float CoordX = 0f;
        public float CoordY = 0f;
        public Color PoIColor = Color.Yellow;
        public string Name = string.Empty;
        public float Radius = 0f;
    }

    public enum MapLevel : byte
    {
        None = 0,
        WorldMap = 1,
        Continent = 2,
        Zone = 3,
        City = 4
    }

    public class MapViewMap
    {
        public MapLevel MapLevel = MapLevel.None;
        public RectangleF ZoneCoords = new RectangleF();
        public RectangleF ImgCoords = new RectangleF();
        public Color MapBorderColor = Color.Yellow;
        public string Name = string.Empty;
        public string InstanceName = string.Empty;
        public string MapImageFile = string.Empty;
        public Bitmap MapBitmapImage = null;

        public string RoadImageFile = string.Empty;
        public PointF RoadMapOffset = new PointF();
        public RectangleF RoadMapCoords = new RectangleF();
        public Bitmap RoadBitmapImage = null;
    }

    public class MapViewPath
    {
        public string PathName = string.Empty;
        public Color Color = Color.White;
        public long TypeId = 0;
        public List<Vector3> allpoints = new List<Vector3>();
        // Helper data for drawing, not actually related to the data
        public byte DrawStyle = 0;
    }

    public class MapViewWorldXMLZoneInfo
    {
        public string name = string.Empty;
        public long zone_key = 0;
        public int originCellX = 0;
        public int originCellY = 0;
    }

    public class MapViewWorldXML
    {
        static public MapViewWorldXML main_world;
        static public List<MapViewWorldXML> instances;
        public XmlDocument _xml;
        public Dictionary<long, MapViewWorldXMLZoneInfo> zones = new Dictionary<long, MapViewWorldXMLZoneInfo>();
        public string WorldName = string.Empty;
        public bool IsInstance = false;
        public Point CellCount = new Point();

        public bool LoadFromStream(Stream s)
        {
            try
            {
                zones.Clear();
                _xml = new XmlDocument();
                _xml.Load(s);
                var worldNode = _xml.SelectNodes("/World");
                if (worldNode.Count < 1)
                    return false;
                var worldAttribs = MainForm.ReadNodeAttributes(worldNode[0]);
                if (worldAttribs.TryGetValue("name", out var wName))
                    WorldName = wName;
                if (worldAttribs.TryGetValue("cellxcount", out var sCellXCount))
                    if (worldAttribs.TryGetValue("cellycount", out var sCellYCount))
                    {
                        CellCount = new Point(int.Parse(sCellXCount), int.Parse(sCellYCount));
                    }
                if (worldAttribs.TryGetValue("isInstance", out var sIsInstance))
                    if (int.TryParse(sIsInstance, out var vIsInstance))
                        IsInstance = vIsInstance != 0;


                var zoneNodes = _xml.SelectNodes("/World/ZoneList/Zone");
                for (var i = 0; i < zoneNodes.Count; i++)
                {
                    var n = zoneNodes[i];
                    var attribs = MainForm.ReadNodeAttributes(n);
                    var newZI = new MapViewWorldXMLZoneInfo();
                    foreach (var attrib in attribs)
                    {
                        switch (attrib.Key)
                        {
                            case "name":
                                newZI.name = attrib.Value;
                                break;
                            case "id":
                                newZI.zone_key = long.Parse(attrib.Value);
                                break;
                            case "originx":
                                newZI.originCellX = int.Parse(attrib.Value);
                                break;
                            case "originy":
                                newZI.originCellY = int.Parse(attrib.Value);
                                break;

                        }
                    }
                    zones.Add(newZI.zone_key, newZI);
                }
            }
            catch
            {
                zones.Clear();
                _xml = null;
                return false;
            }
            return zones.Count > 0;
        }

        public MapViewWorldXMLZoneInfo GetZoneByKey(long key)
        {
            if ((_xml != null) && zones.TryGetValue(key, out var z))
                return z;
            return null;
        }

        static public MapViewWorldXML FindInstanceByZoneKey(long zone_key)
        {
            foreach (var i in instances)
            {
                foreach (var z in i.zones)
                    if (z.Value.zone_key == zone_key)
                        return i;
            }
            return null;
        }

        static public MapViewWorldXML GetInstanceByName(string iName)
        {
            foreach (var i in instances)
            {
                if (i.WorldName == iName)
                    return i;
            }
            return null;
        }

        static public MapViewWorldXML FindInstanceByZoneGroup(long zone_group_id)
        {
            foreach(var zone in AADB.DB_Zones)
            {
                if (zone.Value.group_id == zone_group_id)
                {
                    var inst = FindInstanceByZoneKey(zone.Value.zone_key);
                    if (inst != null)
                        return inst;
                }
            }
            return null;
        }
    }

}

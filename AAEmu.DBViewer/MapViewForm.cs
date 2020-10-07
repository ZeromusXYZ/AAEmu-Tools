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
        public static MapViewForm ThisForm ;
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
        private List<MapViewPoI> poi = new List<MapViewPoI>();
        private List<MapViewMap> subMaps = new List<MapViewMap>();
        private MapViewMap topMostMap = null;
        private MapViewMap WorldMap = null;
        private List<MapViewPath> paths = new List<MapViewPath>();
        private RectangleF FocusBorder = new RectangleF();

        public Point ViewOffset { get => viewOffset; set { viewOffset = value; updateStatusBar(); } }

        public MapViewForm()
        {
            MapViewForm.ThisForm = this;
            InitializeComponent();

            pb.SetBounds(0, 0, 35536, 35536);

            pView.MouseWheel += new MouseEventHandler(MapViewOnMouseWheel);
            viewOffset.Y = 20000;
            viewScale = 0.05f;

            ClearPoI();
            ClearMaps();
            MapViewImageHelper.PopulateList();
            MapViewImageHelper.PopulateMiniMapList();
            WorldMap = AddMap(new RectangleF(-14731, 46, 64651, 38865), "Erenor", "main_world", MapLevel.WorldMap);
            AddMap(new RectangleF(-6758, 2673, 32130, 19308), "Nuia", "land_west", MapLevel.Continent);
            AddMap(new RectangleF(10627, 1632, 26024, 15636), "Haranya", "land_east", MapLevel.Continent);
            AddMap(new RectangleF(7035, 21504, 24743, 14883), "Auroria", "land_origin", MapLevel.Continent);
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

                if ((zg.PosAndSize.Width <= 0) || (zg.PosAndSize.Height <= 0))
                    continue;

                var iref = MapViewImageHelper.GetRefByImageMapId(zg.image_map);
                var mapFileName = zg.name ;
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

                var m = AddMap(zg.PosAndSize, zg.display_textLocalized + " ("+zg.id.ToString()+")", mapFileName, level);
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
            foreach (var map in subMaps)
            {
                if (map.MapLevel <= MapLevel.Continent)
                    continue;
                var pos = CoordToPixel(map.ZoneCoords.X, map.ZoneCoords.Y);
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
            tsslViewOffset.Text = "View X:"+ViewOffset.X.ToString() + " Y:" + ViewOffset.Y.ToString();

            if ((rulerCoords.X != 0) && (rulerCoords.Y != 0))
            {
                var rulerF = new Vector2(rulerCoords.X, rulerCoords.Y);
                var cursorF = new Vector2(cursorCoords.X, cursorCoords.Y);
                var offF = Vector2.Subtract(cursorF, rulerF);
                float dist = Vector2.Distance(cursorF, rulerF);
                if ((dist < -16f) || (dist > 16))
                    tsslRuler.Text = "X:" + rulerCoords.X.ToString() + " Y:" + rulerCoords.Y.ToString() + " => Off X:" + offF.X.ToString() + " Y:" + offF.Y.ToString() + " (D: " + dist.ToString("0.0")+")";
                else
                    tsslRuler.Text = "X:" + rulerCoords.X.ToString() + " Y:" + rulerCoords.Y.ToString();
            }
            else
            {
                tsslRuler.Text = "X:-- Y:--";
            }

            var lastTopMap = topMostMap;
            tsslCoords.Text = "X:"+cursorCoords.X.ToString() + " Y:" + cursorCoords.Y.ToString() + " | " + AADB.CoordToSextant(cursorCoords.X, cursorCoords.Y);
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

        private Point CoordToPixel(Point coord) => CoordToPixel(coord.X,coord.Y);

        private Point CoordToPixel(float x, float y)
        {
            return new Point((int)x, (int)y * -1);
        }

        private void DrawCross(Graphics g,float x, float y, Color color, string name)
        {
            int crossSize = (int)(6f / viewScale) + 1 ;
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

        private void DrawSubMap(Graphics g, MapViewMap map)
        {
            var showMap = true;
            switch (map.MapLevel)
            {
                case MapLevel.WorldMap:
                    showMap = cbShowWorldMap.Checked;
                    break;
                case MapLevel.Continent:
                    showMap = cbShowContinent.Checked;
                    break;
                case MapLevel.Zone:
                    showMap = cbShowZone.Checked;
                    break;
                case MapLevel.City:
                    showMap = cbShowCity.Checked;
                    break;
            }
            if (!showMap)
                return;

            var pen = new Pen(map.MapBorderColor);
            var pos = CoordToPixel(map.ZoneCoords.X, map.ZoneCoords.Y);
            var imgPos = CoordToPixel(map.ImgCoords.X, map.ImgCoords.Y);

            var targetRect = new RectangleF(ViewOffset.X + pos.X, ViewOffset.Y + pos.Y - map.ZoneCoords.Height, map.ZoneCoords.Width, map.ZoneCoords.Height);
            if (cbZoneBorders.Checked)
                g.DrawRectangle(pen, targetRect.X, targetRect.Y, targetRect.Width, targetRect.Height);

            if (map.BitmapImage != null)
            {
                float targetAspect = targetRect.Width / targetRect.Height;
                float imageAspect = (float)map.BitmapImage.Width / (float)map.BitmapImage.Height;
                var drawRect = targetRect ;

                if (targetAspect >= imageAspect)
                {
                    drawRect = new RectangleF();
                    drawRect.Width = targetRect.Width / targetAspect * imageAspect;
                    drawRect.Height = targetRect.Height;
                    drawRect.X = targetRect.X + ((targetRect.Width - drawRect.Width) / 2);
                    drawRect.Y = targetRect.Y;
                }
                else
                {
                    drawRect = new RectangleF();
                    drawRect.Width = targetRect.Width;
                    drawRect.Height = targetRect.Height * targetAspect / imageAspect;
                    drawRect.X = targetRect.X;
                    drawRect.Y = targetRect.Y + ((targetRect.Height - drawRect.Height) / 2);
                }

                g.DrawImage(map.BitmapImage, drawRect);
                /*
                if (cbZoneBorders.Checked)
                    g.DrawRectangle(Pens.Silver, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height);
                */
            }
            //g.DrawImage(map.BitmapImage, ViewOffset.X + imgPos.X, ViewOffset.Y + imgPos.Y - map.ImgHeight, map.ImgWidth, map.ImgHeight);

            if (cbZoneBorders.Checked && (map.Name != string.Empty))
            {
                var f = new Font(Font.FontFamily, 100f);
                var br = new SolidBrush(map.MapBorderColor);
                g.DrawString(map.Name, f, br, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);
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
                foreach (var map in subMaps)
                    if (map.MapLevel == (MapLevel)level)
                        DrawSubMap(g, map);
            }

            if (topMostMap != null)
                DrawSubMap(g, topMostMap);

            // Draw Grid
            if (cbShowGrid.Checked)
            {
                var fnt = new Font(Font.FontFamily, 10f / viewScale);
                var br = new System.Drawing.SolidBrush(Color.White);
                var smallGridSize = 512; // Cell size
                if (viewScale > 0.5f)
                {
                    smallGridSize = 32; // Sector
                    fnt = new Font(Font.FontFamily, 7f / viewScale);
                }
                for (var x = 0; x <= 32768; x += smallGridSize)
                {
                    var s = g.MeasureString(x.ToString(), fnt);
                    var hTop = CoordToPixel(x, 32768 + 4096);
                    var hBottom = CoordToPixel(x, 0);
                    g.DrawLine(x % 4096 == 0 ? System.Drawing.Pens.LightGray : System.Drawing.Pens.DarkGray, ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y, ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);

                    var ht = new Point(ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y);
                    var hb = new Point(ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);
                    if (ht.Y < 0)
                        ht.Y = (int)s.Height ;
                    if (hb.Y > (int)(pView.Height / viewScale) - (int)s.Height)
                        hb.Y = (int)(pView.Height / viewScale) - (int)s.Height;
                    g.DrawString(x.ToString(), fnt, br, ht.X - (s.Width / 2), ht.Y - s.Height);
                    g.DrawString(x.ToString(), fnt, br, hb.X - (s.Width / 2), hb.Y);
                }
                for (var y = 0; y <= 32768 + 4096; y += smallGridSize)
                {
                    var s = g.MeasureString(y.ToString(), fnt);
                    var hTop = CoordToPixel(0, y);
                    var hBottom = CoordToPixel(32768, y);
                    g.DrawLine(y % 4096 == 0 ? System.Drawing.Pens.LightGray : System.Drawing.Pens.DarkGray, ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y, ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);

                    var ht = new Point(ViewOffset.X + hTop.X, ViewOffset.Y + hTop.Y);
                    var hb = new Point(ViewOffset.X + hBottom.X, ViewOffset.Y + hBottom.Y);
                    if (ht.X < 0)
                        ht.X = (int)s.Width;
                    if (hb.X > (int)(pView.Width / viewScale) - (int)s.Width)
                        hb.X = (int)(pView.Width / viewScale) - (int)s.Width;
                    g.DrawString(y.ToString(), fnt, br, ht.X - s.Width, ht.Y - (s.Height / 2));
                    g.DrawString(y.ToString(), fnt, br, hb.X, hb.Y - (s.Height / 2));
                }
            }

            // Draw Points of Interest
            foreach (var p in poi)
                DrawCross(g, p.CoordX, p.CoordY, p.PoIColor, cbPoINames.Checked ? p.Name : "");

            foreach(var path in paths)
            {
                if (path.allpoints.Count <= 0)
                    continue;

                bool first = true;
                Point lastPos = new Point(0, 0);
                foreach(var p in path.allpoints)
                {
                    if (first)
                    {
                        // Draw Startpoint
                        first = false;
                        DrawCross(g, p.X, p.Y, path.Color, cbPathNames.Checked ? path.PathName : "");
                    }
                    else
                    {
                        // Draw Line
                        var pen = new Pen(path.Color);
                        pen.Width = (int)(3f / viewScale)+1;
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

        public MapViewPoI AddPoI(Point coords, string name) => AddPoI(coords.X,coords.Y, name, Color.Yellow);

        public MapViewPoI AddPoI(float x, float y, string name, Color col)
        {
            var newPoi = new MapViewPoI();
            newPoi.CoordX = x;
            newPoi.CoordY = y;
            newPoi.Name = name;
            newPoi.PoIColor = col;
            poi.Add(newPoi);
            return newPoi;
        }

        public void ClearPoI()
        {
            poi.Clear();
        }

        public MapViewMap AddMap(RectangleF mapLoc, string displayName, string fileName, MapLevel level)
        {
            var newMap = new MapViewMap();
            newMap.ZoneCoords = mapLoc;
            newMap.Name = displayName;
            newMap.MapLevel = level;
            newMap.ImageFile = fileName;
            var fn = string.Empty;
            if (MainForm.ThisForm.pak.isOpen)
            {
                var fList = MapViewImageRef.ListPossibleFileNames(fileName, Properties.Settings.Default.DefaultGameLanguage);
                foreach (var fName in fList)
                {
                    if (MainForm.ThisForm.pak.FileExists(fName))
                    {
                        fn = fName;
                        break;
                    }
                }
            }
            if (fn != string.Empty)
                newMap.BitmapImage = PackedImageToBitmap(fn);
            else
                newMap.BitmapImage = PackedImageToBitmap("game/ui/map/world/", fn + ".dds");

            newMap.ImgCoords = mapLoc;

            subMaps.Add(newMap);
            return newMap;
        }

        public void ClearMaps()
        {
            subMaps.Clear();
        }

        public void AddPath(MapViewPath path)
        {
            paths.Add(path);
        }

        public void ClearPaths()
        {
            paths.Clear();
        }

        private void MapViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThisForm = null;
        }

        public void FocusAll(bool focusPoIs, bool focusPaths)
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
            MapViewZonePathOffsets.LoadOffsetsFromFile();
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

        public int GetPathCount()
        {
            return paths.Count;
        }

        public int GetPoICount()
        {
            return poi.Count;
        }

        private void cbOptionsChanged(object sender, EventArgs e)
        {
            pView.Refresh();
            updateStatusBar();
        }
    }


    public class MapViewPoI
    {
        public float CoordX = 0f;
        public float CoordY = 0f;
        public Color PoIColor = Color.Yellow;
        public string Name = string.Empty;
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
        public RectangleF ZoneCoords = new RectangleF();
        public RectangleF ImgCoords = new RectangleF();
        public Color MapBorderColor = Color.Yellow;
        public string Name = string.Empty;
        public string ImageFile = string.Empty;
        public Bitmap BitmapImage = null;
        public MapLevel MapLevel = MapLevel.None;
    }

    public class MapViewPath
    {
        public string PathName = string.Empty;
        public Color Color = Color.White;
        public long PathType = 0 ;
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

    static public class MapViewWorldXML
    {
        static public XmlDocument _xml;
        static public Dictionary<long, MapViewWorldXMLZoneInfo> zones = new Dictionary<long, MapViewWorldXMLZoneInfo>();

        static public bool LoadFromStream(Stream s)
        {
            try
            {
                zones.Clear();
                _xml = new XmlDocument();
                _xml.Load(s);
                var zoneNodes = _xml.SelectNodes("/World/ZoneList/Zone");
                for(var i = 0; i < zoneNodes.Count; i++)
                {
                    var n = zoneNodes[i];
                    var attribs = MainForm.ReadNodeAttributes(n);
                    var newZI = new MapViewWorldXMLZoneInfo();
                    foreach(var attrib in attribs)
                    {
                        switch(attrib.Key)
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
            return zones.Count > 0 ;
        }

        static public MapViewWorldXMLZoneInfo GetZoneByKey(long key)
        {
            if ((_xml != null) && zones.TryGetValue(key, out var z))
                return z;
            return null;
        }
    }


}

using AAEmu.DBDefs;
using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBViewer
{
    public partial class MapViewForm : Form
    {
        public static MapViewForm ThisForm ;
        private PictureBox pb = new PictureBox();
        private Bitmap mainWorldImage;
        private Point viewOffset = new Point(0, 0);
        private Point startDragPos = new Point();
        private Point startDragOffset = new Point();
        private bool isDragging = false;
        private float viewScale = 1f;
        private List<MapViewPoint> poi = new List<MapViewPoint>();


        public Point ViewOffset { get => viewOffset; set { viewOffset = value; updateStatusBar(); } }

        public MapViewForm()
        {
            MapViewForm.ThisForm = this;
            InitializeComponent();

            pb.SetBounds(0, 0, 35536, 35536);
            mainWorldImage = PackedImageToBitmap("game/ui/map/world/", "main_world");

            pView.MouseWheel += new MouseEventHandler(MapViewOnMouseWheel);
            viewOffset.Y = 20000;
            viewScale = 0.05f;
        }

        public static MapViewForm GetMap()
        {
            if (ThisForm != null)
                return ThisForm;
            else
                return new MapViewForm();
        }

        private void updateStatusBar()
        {
            var s = string.Empty;
            s += ViewOffset.X.ToString() + " , " + ViewOffset.Y.ToString() + " | " + (viewScale * 100).ToString()+"%" ;
            if (isDragging)
                s += " | drag: " + startDragPos.X.ToString() + " , " + startDragPos.Y.ToString();
            tsslPos.Text = s;
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
            const int crossSize = 3;
            var pen = new Pen(color);
            var pos = CoordToPixel(x, y);
            g.DrawLine(pen, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y - crossSize, ViewOffset.X + x, ViewOffset.Y + pos.Y + crossSize);
            g.DrawLine(pen, ViewOffset.X + pos.X - crossSize, ViewOffset.Y + pos.Y, ViewOffset.X + pos.X + crossSize, ViewOffset.Y + pos.Y);
            if ((cbPoINames.Checked) && (name != string.Empty) )
            {
                var br = new SolidBrush(color);
                g.DrawString(name, Font, br, ViewOffset.X + pos.X + crossSize, ViewOffset.Y + pos.Y - crossSize);
            }
        }

        private void pView_Paint(object sender, PaintEventArgs e)
        {
            // Draw the map, or something like that

            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            g.ScaleTransform(viewScale, viewScale);

            // Main map
            g.DrawImage(mainWorldImage, ViewOffset);

            // Draw a string on the PictureBox.
            if (isDragging)
                g.DrawString("Dragging", Font, System.Drawing.Brushes.Blue, new Point(30, 30));
            //else
            //    g.DrawString("Map view test", Font, System.Drawing.Brushes.Blue, new Point(30, 30));

            // Draw a line in the PictureBox.
            // g.DrawLine(System.Drawing.Pens.Red, pb.Left, pb.Top, pb.Right, pb.Bottom);

            for (var x = 0; x < 36000; x += 4000)
                g.DrawLine(x % 4000 == 0 ? System.Drawing.Pens.Gray : System.Drawing.Pens.LightGray, ViewOffset.X + x, 0, ViewOffset.X + x, 36000) ;
            for (var y = 0; y > -36000; y -= 4000)
                g.DrawLine(y % 4000 == 0 ? System.Drawing.Pens.Gray : System.Drawing.Pens.LightGray, 0, ViewOffset.Y + y, 36000, ViewOffset.Y + y);

            var f = new Font(Font.FontFamily, 100f);
            foreach (var zg in AADB.DB_Zone_Groups.Values)
            {
                if (zg.name.StartsWith("instance"))
                    continue;

                Color col = Color.Cyan;
                switch(zg.id % 4)
                {
                    case 0: col = Color.Cyan; break;
                    case 1: col = Color.LimeGreen; break;
                    case 2: col = Color.Green; break;
                    case 3: col = Color.LightBlue; break;
                }

                var br = new System.Drawing.SolidBrush(col);
                var pn = new System.Drawing.Pen(br);

                var pos = CoordToPixel(zg.PosAndSize.X, zg.PosAndSize.Y);
                g.DrawRectangle(pn, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y - zg.PosAndSize.Height, zg.PosAndSize.Width, zg.PosAndSize.Height);
                g.DrawString(zg.display_textLocalized, f, br, ViewOffset.X + pos.X, ViewOffset.Y + pos.Y);
            }

            // Draw Points of Interest
            foreach (var p in poi)
            {
                DrawCross(g, p.CoordX, p.CoordY, p.PoIColor, p.Name);
            }
        }

        private Bitmap PackedImageToBitmap(string packedFileFolder, string packedFileName)
        {
            if (MainForm.ThisForm.pak.isOpen)
            {
                var fn = packedFileFolder + packedFileName + ".dds" ;

                if (MainForm.ThisForm.pak.FileExists(packedFileFolder + Properties.Settings.Default.DefaultGameLanguage + "/" + packedFileName + ".dds"))
                {
                    fn = packedFileFolder + Properties.Settings.Default.DefaultGameLanguage + "/" + packedFileName + ".dds";
                }

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

        private void pView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var dx = (int)Math.Floor((startDragPos.X - e.X) / viewScale);
                var dy = (int)Math.Floor((startDragPos.Y - e.Y) / viewScale);
                ViewOffset = new Point(startDragOffset.X - dx, startDragOffset.Y - dy);
                pView.Refresh();
            }
            updateStatusBar();
        }

        private void pView_MouseDown(object sender, MouseEventArgs e)
        {
            startDragPos = new Point(e.X, e.Y);
            startDragOffset = new Point(ViewOffset.X, ViewOffset.Y);
            isDragging = true;
            pView.Invalidate();
            updateStatusBar();
        }

        private void pView_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            pView.Invalidate();
            updateStatusBar();
        }

        public void AddPoI(Point coords, string name) => AddPoI(coords.X,coords.Y, name, Color.Yellow);

        public void AddPoI(float x, float y, string name, Color col)
        {
            var newPoi = new MapViewPoint();
            newPoi.CoordX = x;
            newPoi.CoordY = y;
            newPoi.Name = name;
            newPoi.PoIColor = col;
            poi.Add(newPoi);
        }

        public void ClearPoI()
        {
            poi.Clear();
        }

        private void MapViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThisForm = null;
        }

        private void cbPoINames_ClientSizeChanged(object sender, EventArgs e)
        {
            pView.Refresh();
            updateStatusBar();
        }
    }

    public class MapViewPoint
    {
        public float CoordX = 0f;
        public float CoordY = 0f;
        public Color PoIColor = Color.Yellow;
        public string Name = string.Empty;
    }

}

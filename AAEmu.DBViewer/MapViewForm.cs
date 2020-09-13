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
        private PictureBox pb = new PictureBox();
        private Bitmap mainWorldImage;
        private Point viewOffset = new Point(0, 0);
        private Point startDragPos = new Point();
        private Point startDragOffset = new Point();
        private bool isDragging = false;
        private float viewScale = 1f;

        public Point ViewOffset { get => viewOffset; set { viewOffset = value; updateStatusBar(); } }

        public MapViewForm()
        {
            InitializeComponent();

            pb.SetBounds(0, 0, 65536, 65536);
            mainWorldImage = PackedImageToBitmap("game/ui/map/world/", "main_world");

            pView.MouseWheel += new MouseEventHandler(MapViewOnMouseWheel);
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
                viewScale += 0.25f;

            if (e.Delta < 0)
                viewScale -= 0.25f;

            if (viewScale < 0.25f)
                viewScale = 0.25f;
            pView.Invalidate();
            updateStatusBar();
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
            else
                g.DrawString("Map view test", Font, System.Drawing.Brushes.Blue, new Point(30, 30));

            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, pb.Left, pb.Top, pb.Right, pb.Bottom);
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
    }
}

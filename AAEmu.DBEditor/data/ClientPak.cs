using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAPacker;
using FreeImageAPI;

namespace AAEmu.DbEditor.data
{
    class ClientPak
    {
        public string FileName { get; private set; }
        public AAPak Pak { get; private set; }

        public ImageList Icons { get; private set; }
        public static string DefaultPakIcon = "charge_package.dds";

        public bool Initialize()
        {
            Icons = new ImageList();
            Icons.ImageSize = new Size(48, 48);
            return true;
        }

        public bool Open(string fileName)
        {
            if ((Pak != null) && (Pak.IsOpen))
            {
                MainForm.Self.UpdateProgress("Closing Client Pak ...");
                Pak.ClosePak();
            }

            MainForm.Self.UpdateProgress("Loading Client Pak "+fileName+"...");
            if (Pak == null)
                Pak = new AAPak(fileName, true, false);
            else
                Pak.OpenPak(fileName, true);

            MainForm.Self.UpdateProgress("Loading default icon ...");
            Icons.Images.Clear();
            GetIconIndexByName(DefaultPakIcon);

            if (Pak.IsOpen)
                FileName = Pak._gpFilePath;

            return (Pak != null) && (Pak.IsOpen);
        }

        private Bitmap PackedImageToBitmap(string fn)
        {
            if (Pak.IsOpen)
            {

                if (Pak.FileExists(fn))
                {
                    try
                    {
                        var fStream = Pak.ExportFileAsStream(fn);
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

        public int GetIconIndexByName(string IconName)
        {
            var idx = Icons.Images.IndexOfKey(IconName);
            if (idx >= 0)
                return idx;

            var fn = "game/ui/icon/" + IconName;

            if (Pak.FileExists(fn))
            {
                var bmp = PackedImageToBitmap(fn);
                if (bmp != null)
                    Icons.Images.Add(IconName, bmp);
            }

            return -1;
        }

        public Image GetIconByName(string IconName)
        {
            var idx = GetIconIndexByName(IconName);
            if (idx < 0)
                return null;
            return Icons.Images[idx];
        }
    }
}

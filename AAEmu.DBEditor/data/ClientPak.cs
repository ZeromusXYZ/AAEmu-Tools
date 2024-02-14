using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBEditor.Properties;
using AAPacker;

namespace AAEmu.DBEditor.data
{
    internal class ClientPak
    {
        public string FileName { get; private set; }
        public AAPak Pak { get; private set; }

        /// <summary>
        /// Default Icons Set 64x64
        /// </summary>
        public ImageList Icons { get; private set; }
        public ImageList Icons16 { get; private set; }
        public ImageList Icons32 { get; private set; }
        public ImageList Icons128 { get; private set; }
        public static string DefaultPakIcon = "charge_package.dds";
        private Dictionary<long, int> ItemIconIndexCache = new();

        public bool Initialize()
        {
            Icons = new ImageList
            {
                Site = null,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(64, 64),
                ImageStream = null,
                Tag = null,
                TransparentColor = default
            };
            Icons.Images.Add("noicon", (Image)Resources.ResourceManager.GetObject("noicon"));

            Icons16 = new ImageList
            {
                Site = null,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(16, 16),
                ImageStream = null,
                Tag = null,
                TransparentColor = default
            };
            Icons16.Images.Add("noicon", (Image)Resources.ResourceManager.GetObject("noicon"));

            Icons32 = new ImageList
            {
                Site = null,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(32, 32),
                ImageStream = null,
                Tag = null,
                TransparentColor = default
            };
            Icons32.Images.Add("noicon", (Image)Resources.ResourceManager.GetObject("noicon"));

            Icons128 = new ImageList
            {
                Site = null,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(128, 128),
                ImageStream = null,
                Tag = null,
                TransparentColor = default
            };
            Icons128.Images.Add("noicon", (Image)Resources.ResourceManager.GetObject("noicon"));
            ItemIconIndexCache.Clear();

            return true;
        }

        public bool Open(string fileName)
        {
            if ((Pak != null) && (Pak.IsOpen))
            {
                MainForm.Self.UpdateProgress("Closing Client Pak ...");
                Pak.ClosePak();
            }

            MainForm.Self.UpdateProgress("Loading Client Pak " + fileName + "...");
            if (Pak == null)
                Pak = new AAPak();

            if (!Pak.OpenPak(fileName, true))
            {
                MainForm.Self.UpdateProgress("Loading Client Pak failed to open " + fileName);
                Pak.ClosePak();
                Pak = null;
                FileName = string.Empty;
                return false;
            }

            MainForm.Self.UpdateProgress("Loading default icon ...");
            Icons.Images.Clear();
            Icons16.Images.Clear();
            Icons32.Images.Clear();
            Icons128.Images.Clear();
            GetIconIndexByName(DefaultPakIcon);

            FileName = Pak.GpFilePath;

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
                        return AAEmu.Tools.BitmapUtil.ReadDDSFromStream(fStream);
                    }
                    catch
                    {
                    }
                }
            }

            return null;
        }

        public int GetIconIndexByName(string iconName)
        {
            var idx = Icons.Images.IndexOfKey(iconName);
            if (idx >= 0)
                return idx;

            var fn = "game/ui/icon/" + iconName;

            if (!Pak.FileExists(fn))
                return -1;

            var bmp = PackedImageToBitmap(fn);
            if (bmp == null)
                return -1;

            Icons.Images.Add(iconName, bmp);
            Icons16.Images.Add(iconName, bmp);
            Icons32.Images.Add(iconName, bmp);
            Icons128.Images.Add(iconName, bmp);
            return Icons.Images.Count - 1;
        }

        public Image GetIconByName(string iconName)
        {
            var idx = GetIconIndexByName(iconName);
            return idx < 0 ? null : Icons.Images[idx];
        }

        public int GetIconIndexByItemTemplateId(long itemTemplateId)
        {
            if (ItemIconIndexCache.TryGetValue(itemTemplateId, out var idx))
                return idx;

            var itemEntry = Data.Server.GetItem(itemTemplateId);// .CompactSqlite.Items.FirstOrDefault(x => x.Id == itemTemplateId);
            if (itemEntry != null)
            {
                var iconEntry = Data.Server.CompactSqlite.Icons.FirstOrDefault(x => x.Id == itemEntry.IconId);
                if (iconEntry != null)
                {
                    var imageListIconIndex = Data.Client.GetIconIndexByName(iconEntry.Filename);
                    ItemIconIndexCache.Add(itemTemplateId, imageListIconIndex);
                    return imageListIconIndex;
                }
            }

            return -1;
        }
    }
}

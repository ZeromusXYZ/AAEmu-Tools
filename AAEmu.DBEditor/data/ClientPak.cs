﻿using System.Drawing;
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

        public ImageList Icons { get; private set; }
        public static string DefaultPakIcon = "charge_package.dds";

        public bool Initialize()
        {
            Icons = new ImageList
            {
                Site = null,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(48, 48),
                ImageStream = null,
                Tag = null,
                TransparentColor = default
            };
            Icons.Images.Add("noicon", (Image)Resources.ResourceManager.GetObject("noicon"));
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
            return Icons.Images.Count - 1;
        }

        public Image GetIconByName(string iconName)
        {
            var idx = GetIconIndexByName(iconName);
            return idx < 0 ? null : Icons.Images[idx];
        }

        public int GetIconIndexByItemTemplateId(long itemTemplateId)
        {
            var itemEntry = Data.Server.CompactSqlite.Items.FirstOrDefault(x => x.Id == itemTemplateId);
            if (itemEntry != null)
            {
                var iconEntry = Data.Server.CompactSqlite.Icons.FirstOrDefault(x => x.Id == itemEntry.IconId);
                if (iconEntry != null)
                {
                    return Data.Client.GetIconIndexByName(iconEntry.Filename);
                }
            }

            return -1;
        }
    }
}

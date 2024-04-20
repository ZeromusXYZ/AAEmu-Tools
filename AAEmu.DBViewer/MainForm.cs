﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AAPacker;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;
using AAEmu.ClipboardHelper;
using System.Globalization;
using System.Xml;
using System.Numerics;
using Newtonsoft.Json;
using AAEmu.DBViewer.JsonData;
using AAEmu.DBViewer.utils;
using System.Runtime;
using AAEmu.Game.Models.Game.World;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        public static MainForm ThisForm;
        private string defaultTitle;
        public AAPak pak = new AAPak("");
        private List<string> possibleLanguageIDs = new List<string>();
        private List<string> allTableNames = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            ThisForm = this;
        }

        private void TryLoadPakKeys(string fileName)
        {
            try
            {
                var keyFile = fileName + ".key";
                if (File.Exists(keyFile))
                {
                    var customKey = new byte[16];
                    using (var fs = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
                    {
                        if (fs.Length != 16)
                        {
                            fs.Dispose();
                            return;
                        }

                        fs.Read(customKey, 0, 16);
                    }

                    pak.SetCustomKey(customKey);
                }
                else
                    pak.SetDefaultKey();
            }
            catch
            {
                // Reset key
                pak.SetDefaultKey();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Update settings if needed
            if (!Properties.Settings.Default.IsUpdated)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.IsUpdated = true;
                Properties.Settings.Default.Save();
            }

            defaultTitle = Text;
            possibleLanguageIDs.Clear();
            possibleLanguageIDs.Add("ko");
            possibleLanguageIDs.Add("ru");
            possibleLanguageIDs.Add("en_us");
            possibleLanguageIDs.Add("zh_cn");
            possibleLanguageIDs.Add("zh_tw");
            possibleLanguageIDs.Add("de");
            possibleLanguageIDs.Add("fr");
            possibleLanguageIDs.Add("ja");
            cbItemSearchRange.SelectedIndex = 0;
            tcDoodads.SelectedTab = tpDoodadWorkflow;

            cbItemSearchType.Items.Clear();
            cbItemSearchType.Items.Add("Any");
            foreach (var implId in Enum.GetValues(typeof(GameItemImplId)))
                cbItemSearchType.Items.Add(((int)implId).ToString() + " - " + implId.ToString());
            cbItemSearchType.SelectedIndex = 0;
            cbNewGM.Checked = Properties.Settings.Default.NewGMCommands;

            if (!LoadServerDB(false))
            {
                Close();
                return;
            }


            string game_pakFileName = Properties.Settings.Default.GamePakFileName;
            if (File.Exists(game_pakFileName))
            {
                using (var loading = new LoadingForm())
                {
                    loading.Show();
                    loading.ShowInfo("Opening: " + Path.GetFileName(game_pakFileName));

                    TryLoadPakKeys(game_pakFileName);

                    if (pak.OpenPak(game_pakFileName, true))
                    {
                        Properties.Settings.Default.GamePakFileName = game_pakFileName;
                        lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                        loading.ShowInfo("Loading: World Data");
                        PrepareWorldXML(true);
                        loading.ShowInfo("Loading: Quest Sign Sphere Data");
                        LoadQuestSpheres();
                    }
                }
            }

            LoadHistory(Properties.Settings.Default.HistorySearchItem, cbItemSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchNpc, cbSearchNPC);
            LoadHistory(Properties.Settings.Default.HistorySearchQuest, cbQuestSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchSkill, cbSkillSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchDoodad, cbSearchDoodads);
            LoadHistory(Properties.Settings.Default.HistorySearchBuff, cbSearchBuffs);
            LoadHistory(Properties.Settings.Default.HistorySearchSQL, cbSimpleSQL);

            tcViewer.SelectedTab = tpItems;
            AttachEventsToLabels();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.HistorySearchItem = CreateHistory(cbItemSearch);
            Properties.Settings.Default.HistorySearchNpc = CreateHistory(cbSearchNPC);
            Properties.Settings.Default.HistorySearchQuest = CreateHistory(cbQuestSearch);
            Properties.Settings.Default.HistorySearchSkill = CreateHistory(cbSkillSearch);
            Properties.Settings.Default.HistorySearchDoodad = CreateHistory(cbSearchDoodads);
            Properties.Settings.Default.HistorySearchBuff = CreateHistory(cbSearchBuffs);
            Properties.Settings.Default.HistorySearchSQL = CreateHistory(cbSimpleSQL);
            Properties.Settings.Default.Save();

            if (MapViewForm.ThisForm != null)
                MapViewForm.ThisForm.Close();

            if (pak != null)
                pak.ClosePak();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                .Concat(controls)
                .Where(c => c.GetType() == type);
        }

        public static void CopyToClipBoard(string cliptext)
        {
            try
            {
                // Because nothing is ever as simple as the next line >.>
                // Clipboard.SetText(s);
                // Helper will (try to) prevent errors when copying to clipboard because of threading issues
                var cliphelp = new SetClipboardHelper(DataFormats.Text, cliptext);
                cliphelp.DontRetryWorkOnFailed = false;
                cliphelp.Go();
            }
            catch
            {
            }
        }

        private void LabelToCopy_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                string s = (sender as Label).Text;
                if ((s == "<none>") || (s == "???") || (s.Trim(' ') == string.Empty))
                    s = string.Empty;
                if (s != string.Empty)
                    CopyToClipBoard(s);
            }
        }

        private void AttachEventsToLabels()
        {
            var controls = GetAll(this, typeof(Label));
            foreach (Control c in controls)
            {
                if (c is Label)
                {
                    Label l = (c as Label);
                    if (!l.Name.ToLower().StartsWith("label"))
                    {
                        // l.ForeColor = Color.Red;
                        l.Cursor = Cursors.Hand;
                        l.Click += LabelToCopy_Click;
                    }
                    else
                    {
                        l.ForeColor = SystemColors.ControlDark;
                    }
                }
            }
        }

        private long GetInt64(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return 0;
            else
                return reader.GetInt64(fieldname);
        }

        private string GetString(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return string.Empty;
            else
                return reader.GetString(fieldname);
        }

        private bool GetBool(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return false;
            else
            {
                var val = reader.GetString(fieldname);
                return ((val != null) && ((val == "t") || (val == "T") || (val == "1")));
            }
        }


        private float GetFloat(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return 0;
            else
                return reader.GetFloat(fieldname);
        }

        private void LoadTableNames()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        allTableNames.Clear();
                        lbTableNames.Items.Clear();
                        while (reader.Read())
                        {
                            var tName = GetString(reader, "name");
                            allTableNames.Add(tName);
                            lbTableNames.Items.Add(tName);
                        }
                    }
                }
            }
        }

        private void LoadIcons()
        {
            string sql = "SELECT * FROM icons ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Icons.Clear();
                        while (reader.Read())
                        {
                            AADB.DB_Icons.Add(GetInt64(reader, "id"), GetString(reader, "filename"));
                        }
                    }
                }
            }
        }

        private void AddCustomTranslation(string table, string field, long idx, string val)
        {
            // Try overriding if empty
            foreach (var tl in AADB.DB_Translations)
            {
                if ((tl.Value.idx == idx) && (tl.Value.table == table) && (tl.Value.field == field))
                {
                    if (tl.Value.value == string.Empty)
                    {
                        tl.Value.value = val;
                    }

                    return;
                }
            }

            // Not in table yet, add it
            GameTranslation t = new GameTranslation();
            t.idx = idx;
            t.table = table;
            t.field = field;
            t.value = val;
            string k = t.table + ":" + t.field + ":" + t.idx.ToString();
            AADB.DB_Translations.Add(k, t);
        }

        private void AddCustomTranslations()
        {
            // Custom Translations
            // For whatever reason, these are not defined in en_us, so we add them ourselves as they are kinda important for the visuals
            AddCustomTranslation("system_factions", "name", 1, "[Friendly]");
            AddCustomTranslation("system_factions", "name", 2, "[Neutral]");
            AddCustomTranslation("system_factions", "name", 3, "[Hostile]");

            // Manually translated the doodad group from korean, just to make it easier to read
            AddCustomTranslation("doodad_groups", "name", 2, "[Deforestation - Trees]");
            AddCustomTranslation("doodad_groups", "name", 3, "[Picking - Herbs]");
            AddCustomTranslation("doodad_groups", "name", 4, "[Mining - Minerals]");
            AddCustomTranslation("doodad_groups", "name", 5, "[Livestock - Livestock]");
            AddCustomTranslation("doodad_groups", "name", 6, "[Other - Mailbox]");
            AddCustomTranslation("doodad_groups", "name", 7, "[Workbench - Pension item]");
            AddCustomTranslation("doodad_groups", "name", 8, "[Workbench - Architecture]");
            AddCustomTranslation("doodad_groups", "name", 9, "[Workbench - Cooking]");
            AddCustomTranslation("doodad_groups", "name", 10, "[Workbench - Handicraft]");
            AddCustomTranslation("doodad_groups", "name", 11, "[Others - Quest]");
            AddCustomTranslation("doodad_groups", "name", 12, "[Agriculture - Crops]");
            AddCustomTranslation("doodad_groups", "name", 13, "[Others - Skill]");
            AddCustomTranslation("doodad_groups", "name", 14, "[Collect - Water]");
            AddCustomTranslation("doodad_groups", "name", 15, "[Collect - Soil]");
            AddCustomTranslation("doodad_groups", "name", 16, "[Workbench - Machine]");
            AddCustomTranslation("doodad_groups", "name", 17, "[Workbench - Metal]");
            AddCustomTranslation("doodad_groups", "name", 18, "[Workbench - Crest]");
            AddCustomTranslation("doodad_groups", "name", 19, "[Workbench - Masonry]");
            AddCustomTranslation("doodad_groups", "name", 20, "[Workbench - Fabric]");
            AddCustomTranslation("doodad_groups", "name", 21, "[Workbench - Leather]");
            AddCustomTranslation("doodad_groups", "name", 22, "[Workbench - Weapons]");
            AddCustomTranslation("doodad_groups", "name", 23, "[Workbench - Wood]");
            AddCustomTranslation("doodad_groups", "name", 24, "[Others - Interaction]");
            AddCustomTranslation("doodad_groups", "name", 25, "[Others - Level]");
            AddCustomTranslation("doodad_groups", "name", 26, "[Interaction - Sales]");
            AddCustomTranslation("doodad_groups", "name", 27, "[Interaction - Adventure T1]");
            AddCustomTranslation("doodad_groups", "name", 28, "[Interaction - Adventure T2]");
            AddCustomTranslation("doodad_groups", "name", 29, "[Interaction - Adventure T3]");
            AddCustomTranslation("doodad_groups", "name", 30, "[Interaction - Adventure T4]");
            AddCustomTranslation("doodad_groups", "name", 31, "[Interaction - Adventure T5]");
            AddCustomTranslation("doodad_groups", "name", 32, "[Interaction - Adventure T6]");
            AddCustomTranslation("doodad_groups", "name", 33, "[Interaction - Adventure T7]");
            //AddCustomTranslation("doodad_groups", "name", 34, "[Test Doodad]");
            //AddCustomTranslation("doodad_groups", "name", 35, "[Trash Doodad]");
            //AddCustomTranslation("doodad_groups", "name", 36, "[Door]");
            //AddCustomTranslation("doodad_groups", "name", 37, "[Can Place On Water]");
            AddCustomTranslation("doodad_groups", "name", 38, "[Interaction - Backpack]");
            AddCustomTranslation("doodad_groups", "name", 39, "[Interaction - Excavation]");
            AddCustomTranslation("doodad_groups", "name", 40, "[Agriculture - Marine Crops]");
            //AddCustomTranslation("doodad_groups", "name", 41, "[]");
            AddCustomTranslation("doodad_groups", "name", 42, "[Marine related]");
            AddCustomTranslation("doodad_groups", "name", 43, "[Portal - Space gap]");
            AddCustomTranslation("doodad_groups", "name", 44, "[Portal - Mirage Island]");
            AddCustomTranslation("doodad_groups", "name", 45, "[Portal - Instance Dungeon]");
            AddCustomTranslation("doodad_groups", "name", 46, "[Workbench - Specialties]");
            AddCustomTranslation("doodad_groups", "name", 47, "[Unused]");
            AddCustomTranslation("doodad_groups", "name", 48, "[Workbench - Archeum]");
            AddCustomTranslation("doodad_groups", "name", 49, "[Workbench - Cloth Armor]");
            AddCustomTranslation("doodad_groups", "name", 50, "[Workbench - Leather Armor]");
            AddCustomTranslation("doodad_groups", "name", 51, "[Workbench - Woodworking]");
            AddCustomTranslation("doodad_groups", "name", 52, "[Workbench - Metal Armor]");
            AddCustomTranslation("doodad_groups", "name", 53, "[Others - Spatial Records]");
            AddCustomTranslation("doodad_groups", "name", 54, "[Others - Quest (hunting grounds)]");
            AddCustomTranslation("doodad_groups", "name", 55, "[Normal Treasure]");
            AddCustomTranslation("doodad_groups", "name", 56, "[Grand Treasure]");
            AddCustomTranslation("doodad_groups", "name", 57, "[Rare Treasure]");
            AddCustomTranslation("doodad_groups", "name", 58, "[Arcane Treasure]");
            AddCustomTranslation("doodad_groups", "name", 59, "[Heroic Treasure]");
            AddCustomTranslation("doodad_groups", "name", 60, "[Housing Area Marker - General Residential Area]");
            AddCustomTranslation("doodad_groups", "name", 61, "[Housing Area Marker - Marine Residential Area]");
            AddCustomTranslation("doodad_groups", "name", 62, "[Housing Area Marker - Luxury Residential Area]");
            AddCustomTranslation("doodad_groups", "name", 63,
                "[Housing Area Marker - Private Pumpkin Scarecrow Garden]");
            AddCustomTranslation("doodad_groups", "name", 64, "[Housing Area Marker - Thatched Farmhouse Area]");
            AddCustomTranslation("doodad_groups", "name", 65, "[Fish]");
            AddCustomTranslation("doodad_groups", "name", 66, "[Workbench - Paper]");
            AddCustomTranslation("doodad_groups", "name", 67, "[Workbench - Printing]");
            AddCustomTranslation("doodad_groups", "name", 68, "[Housing - Doors/Windows]");
            AddCustomTranslation("doodad_groups", "name", 69, "[Housing - Furniture]");
            AddCustomTranslation("doodad_groups", "name", 70, "[Fish Furniture]");
            AddCustomTranslation("doodad_groups", "name", 71, "[Fish Stand]");
            //AddCustomTranslation("doodad_groups", "name", 72, "[Collide check]");
            //AddCustomTranslation("doodad_groups", "name", 73, "[o_abyss_gate_mine_A]");
            //AddCustomTranslation("doodad_groups", "name", 74, "[o_abyss_gate_mine_B]");
            //AddCustomTranslation("doodad_groups", "name", 75, "[o_abyss_gate_warning_A]");
            //AddCustomTranslation("doodad_groups", "name", 76, "[o_abyss_gate_warning_B]");
            AddCustomTranslation("doodad_groups", "name", 77, "[Others - Quest_Do not apply frost protection]");
            AddCustomTranslation("doodad_groups", "name", 78, "[Housing Area Marker - Straw Hat Scarecrow Garden]");
            AddCustomTranslation("doodad_groups", "name", 79, "[Housing Area Marker - Water Residential Area]");
            AddCustomTranslation("doodad_groups", "name", 80, "[Holy Statue?]");
            //AddCustomTranslation("doodad_groups", "name", 81, "[arche_mall_race]");
            AddCustomTranslation("doodad_groups", "name", 82, "[Backpack Storage]");
            AddCustomTranslation("doodad_groups", "name", 83, "[Workbench - Art]");

            // Missing Tags Information
            AddCustomTranslation("tags", "name", 1348, "[No fishing allowed]");
            AddCustomTranslation("tags", "name", 1338, "[Summon Guard/Pet]");
            AddCustomTranslation("tags", "name", 1456, "[Can't use the training arena]");
            AddCustomTranslation("tags", "name", 1457, "[Instance Worldview]");
            AddCustomTranslation("tags", "name", 1545, "[Snowman Event]");
            AddCustomTranslation("tags", "name", 1574, "[Library Items and Skill restricted]");

            // End of Custom Translations
        }

        private void GetLanguageSelectFromFiles(string mainDb)
        {
            cbItemSearchLanguage.Enabled = false;
            cbItemSearchLanguage.Items.Clear();

            foreach (var l in possibleLanguageIDs)
            {
                var localizedDb = Path.Combine(Path.GetDirectoryName(mainDb), l + Path.GetExtension(mainDb));
                if (File.Exists(localizedDb))
                {
                    cbItemSearchLanguage.Items.Add(l);
                }
            }
        }

        private void LoadTranslations(string lng)
        {
            string sql = "SELECT * FROM localized_texts ORDER BY tbl_name, tbl_column_name, idx";

            List<string> columnNames = null;

            var overrideDb = "";

            // TODO: If main DB doesn't have a localized_texts table, check if a external DB exists with the language name
            if (!allTableNames.Contains("localized_texts"))
            {
                GetLanguageSelectFromFiles(SQLite.SQLiteFileName);
                var localizedDb = Path.Combine(Path.GetDirectoryName(SQLite.SQLiteFileName),
                    lng + Path.GetExtension(SQLite.SQLiteFileName));
                if (File.Exists(localizedDb))
                    overrideDb = localizedDb;
            }

            using (var connection = SQLite.CreateConnection(overrideDb))
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Translations.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    //command.Parameters.AddWithValue("@lng", lng);
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        while (reader.Read())
                        {
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                if (columnNames.IndexOf(lng) < 0)
                                {
                                    // selected language not found, revert to en_us or ko as default

                                    if (columnNames.IndexOf("en_us") >= 0)
                                    {
                                        MessageBox.Show("The selected language \"" + lng +
                                                        "\" was not found in localized_texts !\r\n" +
                                                        "Reverted to English",
                                            "Language not found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lng = "en_us";
                                    }
                                    else if (columnNames.IndexOf("ko") >= 0)
                                    {
                                        MessageBox.Show("The selected language \"" + lng +
                                                        "\" was not found in localized_texts !\r\n" +
                                                        "Reverted to Korean",
                                            "Language not found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lng = "ko";
                                    }
                                    else
                                    {
                                        MessageBox.Show("The selected language \"" + lng +
                                                        "\" was not found in localized_texts !\r\n" +
                                                        "Also was not able to revert to English or Korean, functionality of this program is not guaranteed",
                                            "Language not found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }

                            GameTranslation t = new GameTranslation();
                            t.idx = GetInt64(reader, "idx");
                            t.table = GetString(reader, "tbl_name");
                            t.field = GetString(reader, "tbl_column_name");

                            t.value = GetString(reader, lng);
                            string k = t.table + ":" + t.field + ":" + t.idx.ToString();
                            AADB.DB_Translations.Add(k, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if ((columnNames != null) && (overrideDb == ""))
            {
                try
                {
                    cbItemSearchLanguage.Enabled = false;

                    List<string> availableLng = new List<string>();
                    foreach (var l in possibleLanguageIDs)
                    {
                        if (columnNames.IndexOf(l) >= 0)
                            availableLng.Add(l);
                    }

                    cbItemSearchLanguage.Items.Clear();
                    for (int i = 0; i < availableLng.Count; i++)
                    {
                        var l = availableLng[i];
                        cbItemSearchLanguage.Items.Add(l);
                        if (l == lng)
                            cbItemSearchLanguage.SelectedIndex = i;
                    }
                }
                catch
                {
                    // Do nothing
                }
            }

            if (cbItemSearchLanguage.Items.Contains(lng))
                cbItemSearchLanguage.Text = lng;
            cbItemSearchLanguage.Enabled = (cbItemSearchLanguage.Items.Count > 1);
            Properties.Settings.Default.DefaultGameLanguage = lng;
        }

        private void LoadZones()
        {

            // Zones
            string sql = "SELECT * FROM zones ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Zones.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var columnNames = reader.GetColumnNames();
                        bool hasAbox_show = (columnNames.IndexOf("abox_show") > 0);

                        while (reader.Read())
                        {
                            GameZone t = new GameZone();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.zone_key = GetInt64(reader, "zone_key");
                            t.group_id = GetInt64(reader, "group_id");
                            t.closed = GetBool(reader, "closed");
                            t.display_text = GetString(reader, "display_text");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.zone_climate_id = GetInt64(reader, "zone_climate_id");

                            if (hasAbox_show)
                                t.abox_show = GetBool(reader, "abox_show");
                            else
                                t.abox_show = false;

                            if (t.display_text != string.Empty)
                                t.display_textLocalized =
                                    AADB.GetTranslationByID(t.id, "zones", "display_text", t.display_text);
                            else
                                t.display_textLocalized = "";
                            t.SearchString = t.name + " " + t.display_text + " " + t.display_textLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Zones.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            // Zone_Groups
            sql = "SELECT * FROM zone_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Zone_Groups.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameZone_Groups t = new GameZone_Groups();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            var x = GetFloat(reader, "x");
                            var y = GetFloat(reader, "y");
                            var w = GetFloat(reader, "w");
                            var h = GetFloat(reader, "h");
                            t.PosAndSize = new RectangleF(x, y, w, h);
                            t.image_map = GetInt64(reader, "image_map");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.target_id = GetInt64(reader, "target_id");
                            t.display_text = GetString(reader, "display_text");
                            t.faction_chat_region_id = GetInt64(reader, "faction_chat_region_id");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.pirate_desperado = GetBool(reader, "pirate_desperado");
                            t.fishing_sea_loot_pack_id = GetInt64(reader, "fishing_sea_loot_pack_id");
                            t.fishing_land_loot_pack_id = GetInt64(reader, "fishing_land_loot_pack_id");
                            t.buff_id = GetInt64(reader, "buff_id");

                            if (t.display_text != string.Empty)
                                t.display_textLocalized = AADB.GetTranslationByID(t.id, "zone_groups", "display_text",
                                    t.display_text);
                            else
                                t.display_textLocalized = "";
                            t.SearchString = t.name + " " + t.display_text + " " + t.display_textLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Zone_Groups.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            // World_Groups
            sql = "SELECT * FROM world_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_World_Groups.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameWorld_Groups t = new GameWorld_Groups();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            int x = (int)GetInt64(reader, "x");
                            int y = (int)GetInt64(reader, "y");
                            int w = (int)GetInt64(reader, "w");
                            int h = (int)GetInt64(reader, "h");
                            int ix = (int)GetInt64(reader, "image_x");
                            int iy = (int)GetInt64(reader, "image_y");
                            int iw = (int)GetInt64(reader, "image_w");
                            int ih = (int)GetInt64(reader, "image_h");
                            t.PosAndSize = new Rectangle(x, y, w, h);
                            t.Image_PosAndSize = new Rectangle(ix, iy, iw, ih);
                            t.image_map = GetInt64(reader, "image_map");
                            t.target_id = GetInt64(reader, "target_id");

                            t.SearchString = t.name.ToLower();

                            AADB.DB_World_Groups.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }


        }

        private void LoadItemCategories()
        {
            string sql = "SELECT * FROM item_categories ORDER BY id ASC";
            /*
            CREATE TABLE item_categories(
              id INT,
              name TEXT,
              processed_state_id INT,
              usage_id INT,
              impl1_id INT,
              impl2_id INT,
              pickup_sound_id INT,
              category_order INT,
              item_group_id INT,
              use_or_equipment_sound_id INT,
              secure NUM
            )
            */
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_ItemsCategories.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameItemCategories t = new GameItemCategories();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "item_categories", "name", t.name);

                            AADB.DB_ItemsCategories.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            cbItemSearchItemCategoryTypeList.DataSource = null;
            cbItemSearchItemCategoryTypeList.Items.Clear();
            List<GameItemCategories> cats = new List<GameItemCategories>();
            cats.Add(new GameItemCategories());
            foreach (var t in AADB.DB_ItemsCategories)
                cats.Add(t.Value);
            cbItemSearchItemCategoryTypeList.DataSource = cats;
            cbItemSearchItemCategoryTypeList.DisplayMember = "DisplayListName";
            cbItemSearchItemCategoryTypeList.ValueMember = "DisplayListValue";

            cbItemSearchItemCategoryTypeList.SelectedIndex = 0;
            cbItemSearchItemArmorSlotTypeList.SelectedIndex = 0;
        }


        private void LoadItems()
        {
            string sql = "SELECT * FROM items ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Items.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameItem t = new GameItem();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.catgegory_id = GetInt64(reader, "category_id");
                            t.description = GetString(reader, "description");
                            t.price = GetInt64(reader, "price");
                            t.refund = GetInt64(reader, "refund");
                            t.max_stack_size = GetInt64(reader, "max_stack_size");
                            t.icon_id = GetInt64(reader, "icon_id");
                            t.sellable = GetBool(reader, "sellable");
                            t.fixed_grade = GetInt64(reader, "fixed_grade");
                            t.use_skill_id = GetInt64(reader, "use_skill_id");
                            t.impl_id = GetInt64(reader, "impl_id");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "items", "name", t.name);
                            t.descriptionLocalized =
                                AADB.GetTranslationByID(t.id, "items", "description", t.description);

                            t.SearchString = t.name + " " + t.description + " " + t.nameLocalized + " " +
                                             t.descriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Items.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

        }

        private void LoadItemArmors()
        {
            string sql = "SELECT * FROM item_armors ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Item_Armors.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameItemArmors t = new GameItemArmors();
                            t.id = GetInt64(reader, "id");
                            t.item_id = GetInt64(reader, "item_id");
                            t.slot_type_id = GetInt64(reader, "slot_type_id");

                            AADB.DB_Item_Armors.Add(t.id, t);
                            if (AADB.DB_Items.TryGetValue(t.item_id, out var item))
                                item.item_armors = t;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

        }

        private void LoadSkills()
        {
            List<string> columnNames = null;
            bool readWebDesc = false;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            using (var connection = SQLite.CreateConnection())
            {
                // Skills base
                string sql = "SELECT * FROM skills ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Skills.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {

                        if (columnNames == null)
                        {
                            columnNames = reader.GetColumnNames();
                            if (columnNames.IndexOf("web_desc") >= 0)
                                readWebDesc = true;
                        }

                        while (reader.Read())
                        {
                            var t = new GameSkills();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.desc = GetString(reader, "desc");
                            // Added a check for this
                            if (readWebDesc)
                                t.web_desc = GetString(reader, "web_desc");
                            else
                                t.web_desc = string.Empty;
                            t.cost = GetInt64(reader, "cost");
                            t.icon_id = GetInt64(reader, "icon_id");
                            t.show = GetBool(reader, "show");
                            t.cooldown_time = GetInt64(reader, "cooldown_time");
                            t.casting_time = GetInt64(reader, "casting_time");
                            t.ignore_global_cooldown = GetBool(reader, "ignore_global_cooldown");
                            t.effect_delay = GetInt64(reader, "effect_delay");
                            t.ability_id = GetInt64(reader, "ability_id");
                            t.mana_cost = GetInt64(reader, "mana_cost");
                            t.timing_id = GetInt64(reader, "timing_id");
                            t.consume_lp = GetInt64(reader, "consume_lp");
                            t.default_gcd = GetBool(reader, "default_gcd");
                            ;
                            t.custom_gcd = GetInt64(reader, "custom_gcd");
                            t.first_reagent_only = GetBool(reader, "first_reagent_only");
                            t.plot_id = GetInt64(reader, "plot_id");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "skills", "name", t.name);
                            t.descriptionLocalized = AADB.GetTranslationByID(t.id, "skills", "desc", t.desc);
                            if (readWebDesc)
                                t.webDescriptionLocalized =
                                    AADB.GetTranslationByID(t.id, "skills", "web_desc", t.web_desc);
                            else
                                t.webDescriptionLocalized = string.Empty;

                            t.SearchString = t.name + " " + t.desc + " " + t.nameLocalized + " " +
                                             t.descriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Skills.Add(t.id, t);
                        }

                    }
                }

                // Skill Effects
                sql = "SELECT * FROM skill_effects ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Skill_Effects.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameSkillEffects();
                            t.id = GetInt64(reader, "id");
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.effect_id = GetInt64(reader, "effect_id");
                            t.weight = GetInt64(reader, "weight");
                            t.start_level = GetInt64(reader, "start_level");
                            t.end_level = GetInt64(reader, "end_level");
                            t.friendly = GetBool(reader, "friendly");
                            t.non_friendly = GetBool(reader, "non_friendly");
                            t.target_buff_tag_id = GetInt64(reader, "target_buff_tag_id");
                            t.target_nobuff_tag_id = GetInt64(reader, "target_nobuff_tag_id");
                            t.source_buff_tag_id = GetInt64(reader, "source_buff_tag_id");
                            t.source_nobuff_tag_id = GetInt64(reader, "source_nobuff_tag_id");
                            t.chance = GetInt64(reader, "chance");
                            t.front = GetBool(reader, "front");
                            t.back = GetBool(reader, "back");
                            t.target_npc_tag_id = GetInt64(reader, "target_npc_tag_id");
                            t.application_method_id = GetInt64(reader, "application_method_id");
                            t.synergy_text = GetBool(reader, "synergy_text");
                            t.consume_source_item = GetBool(reader, "consume_source_item");
                            t.consume_item_id = GetInt64(reader, "consume_item_id");
                            t.consume_item_count = GetInt64(reader, "consume_item_count");
                            t.always_hit = GetBool(reader, "always_hit");
                            t.item_set_id = GetInt64(reader, "item_set_id");
                            t.interaction_success_hit = GetBool(reader, "interaction_success_hit");
                            AADB.DB_Skill_Effects.Add(t.id, t);
                        }

                    }
                }

                // Effects
                sql = "SELECT * FROM effects ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Effects.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameEffects();
                            t.id = GetInt64(reader, "id");
                            t.actual_id = GetInt64(reader, "actual_id");
                            t.actual_type = GetString(reader, "actual_type");
                            AADB.DB_Effects.Add(t.id, t);
                        }

                    }
                }

                // Mount Skills
                sql = "SELECT * FROM mount_skills ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Mount_Skills.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        columnNames = null;
                        var readNames = false;
                        while (reader.Read())
                        {
                            // name field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readNames = (columnNames.IndexOf("name") >= 0);
                            }

                            var t = new GameMountSkill();
                            t.id = GetInt64(reader, "id");
                            if (readNames)
                                t.name = GetString(reader, "name");
                            t.skill_id = GetInt64(reader, "skill_id");
                            AADB.DB_Mount_Skills.Add(t.id, t);
                        }
                    }
                }

                // Slave Mount Skills
                sql = "SELECT * FROM slave_mount_skills ORDER BY slave_id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Mount_Skills.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        columnNames = null;
                        var indx = 1L;
                        var readId = false;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlaveMountSkill();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.slave_id = GetInt64(reader, "slave_id");
                            t.mount_skill_id = GetInt64(reader, "mount_skill_id");
                            AADB.DB_Slave_Mount_Skills.Add(t.id, t);
                            indx++;
                        }
                    }
                }

                // NpSkills
                if (allTableNames.Contains("np_skills"))
                {
                    sql = "SELECT * FROM np_skills ORDER BY id ASC";
                    using (var command = connection.CreateCommand())
                    {
                        AADB.DB_NpSkills.Clear();

                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            while (reader.Read())
                            {
                                var t = new GameNpSkills();
                                t.id = GetInt64(reader, "id");
                                t.owner_id = GetInt64(reader, "owner_id");
                                t.owner_type = GetString(reader, "owner_type"); // They are actually all "Npc"
                                t.skill_id = GetInt64(reader, "skill_id");
                                t.skill_use_condition_id = (SkillUseConditionKind)GetInt64(reader, "skill_use_condition_id");
                                t.skill_use_param1 = GetFloat(reader, "skill_use_param1");
                                t.skill_use_param2 = GetFloat(reader, "skill_use_param2");
                                AADB.DB_NpSkills.Add(t.id, t);
                            }

                        }
                    }
                }
                else
                {
                    AADB.DB_NpSkills.Clear();
                }
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void LoadSkillReagents()
        {
            string sql = "SELECT * FROM skill_reagents ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Skill_Reagents.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameSkillItems t = new GameSkillItems();
                            t.id = GetInt64(reader, "id");
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.item_id = GetInt64(reader, "item_id");
                            t.amount = GetInt64(reader, "amount");

                            AADB.DB_Skill_Reagents.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LoadSkillProducts()
        {
            string sql = "SELECT * FROM skill_products ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Skill_Products.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameSkillItems t = new GameSkillItems();
                            t.id = GetInt64(reader, "id");
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.item_id = GetInt64(reader, "item_id");
                            t.amount = GetInt64(reader, "amount");

                            AADB.DB_Skill_Products.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LoadNPCs()
        {
            string sql = "SELECT * FROM npcs ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_NPCs.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasEquip_bodies_id = (columnNames.IndexOf("equip_bodies_id") > 0);
                        bool hasMileStoneID = (columnNames.IndexOf("milestone_id") > 0);
                        bool hasComments = ((columnNames.IndexOf("comment1") > 0) &&
                                            (columnNames.IndexOf("comment2") > 0) &&
                                            (columnNames.IndexOf("comment3") > 0) &&
                                            (columnNames.IndexOf("comment_wear") > 0));
                        bool hasNpc_tendency_id = (columnNames.IndexOf("npc_tendency_id") > 0);
                        bool hasRecruiting_battle_field_id = (columnNames.IndexOf("recruiting_battle_field_id") > 0);
                        bool hasFX_scale = (columnNames.IndexOf("fx_scale") > 0);
                        bool hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameNPC();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.char_race_id = GetInt64(reader, "char_race_id");
                            t.npc_grade_id = GetInt64(reader, "npc_grade_id");
                            t.npc_kind_id = GetInt64(reader, "npc_kind_id");
                            t.level = GetInt64(reader, "level");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.model_id = GetInt64(reader, "model_id");
                            t.npc_template_id = GetInt64(reader, "npc_template_id");
                            if (hasEquip_bodies_id)
                                t.equip_bodies_id = GetInt64(reader, "equip_bodies_id");
                            else
                                t.equip_bodies_id = -1;
                            t.equip_cloths_id = GetInt64(reader, "equip_cloths_id");
                            t.equip_weapons_id = GetInt64(reader, "equip_weapons_id");
                            t.skill_trainer = GetBool(reader, "skill_trainer");
                            t.ai_file_id = GetInt64(reader, "ai_file_id");
                            t.merchant = GetBool(reader, "merchant");
                            t.npc_nickname_id = GetInt64(reader, "npc_nickname_id");
                            t.auctioneer = GetBool(reader, "auctioneer");
                            t.show_name_tag = GetBool(reader, "show_name_tag");
                            t.visible_to_creator_only = GetBool(reader, "visible_to_creator_only");
                            t.no_exp = GetBool(reader, "no_exp");
                            t.pet_item_id = GetInt64(reader, "pet_item_id");
                            t.base_skill_id = GetInt64(reader, "base_skill_id");
                            t.track_friendship = GetBool(reader, "track_friendship");
                            t.priest = GetBool(reader, "priest");
                            if (hasNpc_tendency_id)
                                t.npc_tendency_id = GetInt64(reader, "npc_tendency_id");
                            else
                                t.npc_tendency_id = -1;
                            t.blacksmith = GetBool(reader, "blacksmith");
                            t.teleporter = GetBool(reader, "teleporter");
                            t.opacity = GetFloat(reader, "opacity");
                            t.ability_changer = GetBool(reader, "ability_changer");
                            t.scale = GetFloat(reader, "scale");
                            if (hasComments)
                            {
                                t.comment1 = GetString(reader, "comment1");
                                t.comment2 = GetString(reader, "comment2");
                                t.comment3 = GetString(reader, "comment3");
                                t.comment_wear = GetString(reader, "comment_wear");
                            }
                            else
                            {
                                t.comment1 = string.Empty;
                                t.comment2 = string.Empty;
                                t.comment3 = string.Empty;
                                t.comment_wear = string.Empty;
                            }

                            t.sight_range_scale = GetFloat(reader, "sight_range_scale");
                            t.sight_fov_scale = GetFloat(reader, "sight_fov_scale");
                            if (hasMileStoneID)
                                t.milestone_id = GetInt64(reader, "milestone_id");
                            else
                                t.milestone_id = -1;
                            t.attack_start_range_scale = GetFloat(reader, "attack_start_range_scale");
                            t.aggression = GetBool(reader, "aggression");
                            t.exp_multiplier = GetFloat(reader, "exp_multiplier");
                            t.exp_adder = GetInt64(reader, "exp_adder");
                            t.stabler = GetBool(reader, "stabler");
                            t.accept_aggro_link = GetBool(reader, "accept_aggro_link");
                            if (hasRecruiting_battle_field_id)
                                t.recruiting_battle_field_id = GetInt64(reader, "recruiting_battle_field_id");
                            else
                                t.recruiting_battle_field_id = -1;
                            t.return_distance = GetInt64(reader, "return_distance");
                            t.npc_ai_param_id = GetInt64(reader, "npc_ai_param_id");
                            t.non_pushable_by_actor = GetBool(reader, "non_pushable_by_actor");
                            t.banker = GetBool(reader, "banker");
                            t.aggro_link_special_rule_id = GetInt64(reader, "aggro_link_special_rule_id");
                            t.aggro_link_help_dist = GetFloat(reader, "aggro_link_help_dist");
                            t.aggro_link_sight_check = GetBool(reader, "aggro_link_sight_check");
                            t.expedition = GetBool(reader, "expedition");
                            t.honor_point = GetInt64(reader, "honor_point");
                            t.trader = GetBool(reader, "trader");
                            t.aggro_link_special_guard = GetBool(reader, "aggro_link_special_guard");
                            t.aggro_link_special_ignore_npc_attacker =
                                GetBool(reader, "aggro_link_special_ignore_npc_attacker");
                            t.absolute_return_distance = GetFloat(reader, "absolute_return_distance");
                            t.repairman = GetBool(reader, "repairman");
                            t.activate_ai_always = GetBool(reader, "activate_ai_always");
                            t.so_state = GetString(reader, "so_state");
                            t.specialty = GetBool(reader, "specialty");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.specialty_coin_id = GetInt64(reader, "specialty_coin_id");
                            t.use_range_mod = GetBool(reader, "use_range_mod");
                            t.npc_posture_set_id = GetInt64(reader, "npc_posture_set_id");
                            t.mate_equip_slot_pack_id = GetInt64(reader, "mate_equip_slot_pack_id");
                            t.mate_kind_id = GetInt64(reader, "mate_kind_id");
                            t.engage_combat_give_quest_id = GetInt64(reader, "engage_combat_give_quest_id");
                            t.total_custom_id = GetInt64(reader, "total_custom_id");
                            t.no_apply_total_custom = GetBool(reader, "no_apply_total_custom");
                            t.base_skill_strafe = GetBool(reader, "base_skill_strafe");
                            t.base_skill_delay = GetFloat(reader, "base_skill_delay");
                            t.npc_interaction_set_id = GetInt64(reader, "npc_interaction_set_id");
                            t.use_abuser_list = GetBool(reader, "use_abuser_list");
                            t.return_when_enter_housing_area = GetBool(reader, "return_when_enter_housing_area");
                            t.look_converter = GetBool(reader, "look_converter");
                            t.use_ddcms_mount_skill = GetBool(reader, "use_ddcms_mount_skill");
                            t.crowd_effect = GetBool(reader, "crowd_effect");
                            if (hasFX_scale)
                                t.fx_scale = GetFloat(reader, "fx_scale");
                            else
                                t.fx_scale = 1.0f;
                            if (hasTranslate)
                                t.translate = GetBool(reader, "translate");
                            else
                                t.translate = false;
                            t.no_penalty = GetBool(reader, "no_penalty");
                            t.show_faction_tag = GetBool(reader, "show_faction_tag");


                            t.nameLocalized = AADB.GetTranslationByID(t.id, "npcs", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_NPCs.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_spawner_npcs ORDER BY id ASC";

            AADB.DB_Npc_Spawner_Npcs.Clear();
            if (allTableNames.Contains("npc_spawner_npcs"))
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            // AADB.DB_Npc_Spawner_Npcs.Clear();

                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var t = new GameNpcSpawnerNpc();
                                // Actual DB entries
                                t.id = GetInt64(reader, "id");
                                t.npc_spawner_id = GetInt64(reader, "npc_spawner_id");
                                t.member_id = GetInt64(reader, "member_id");
                                t.member_type = GetString(reader, "member_type");
                                t.weight = GetFloat(reader, "weight");
                                AADB.DB_Npc_Spawner_Npcs.Add(t.id, t);
                            }
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_spawners ORDER BY id ASC";

            AADB.DB_Npc_Spawners.Clear();
            if (allTableNames.Contains("npc_spawners"))
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            // AADB.DB_Npc_Spawners.Clear();

                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var t = new GameNpcSpawner();
                                // Actual DB entries
                                t.id = GetInt64(reader, "id");
                                t.npc_spawner_category_id = GetInt64(reader, "npc_spawner_category_id");
                                t.name = GetString(reader, "name");
                                t.comment = GetString(reader, "comment");
                                t.maxPopulation = GetInt64(reader, "maxPopulation");
                                t.startTime = GetFloat(reader, "startTime");
                                t.endTime = GetFloat(reader, "endTime");
                                t.destroyTime = GetFloat(reader, "destroyTime");
                                t.spawn_delay_min = GetFloat(reader, "spawn_delay_min");
                                t.activation_state = GetBool(reader, "activation_state");
                                t.save_indun = GetBool(reader, "save_indun");
                                t.min_population = GetInt64(reader, "min_population");
                                t.test_radius_npc = GetFloat(reader, "test_radius_npc");
                                t.test_radius_pc = GetFloat(reader, "test_radius_pc");
                                t.suspend_spawn_count = GetInt64(reader, "suspend_spawn_count");
                                t.spawn_delay_max = GetFloat(reader, "spawn_delay_max");

                                AADB.DB_Npc_Spawners.Add(t.id, t);
                            }
                        }
                    }
                }
            }

            sql = "SELECT * FROM quest_monster_groups ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Quest_Monster_Groups.Clear();

                        var readCatId = false;
                        List<string> columnNames = null;
                        while (reader.Read())
                        {
                            // category_id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readCatId = (columnNames.IndexOf("category_id") >= 0);
                            }

                            var t = new GameQuestMonsterGroups();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.category_id = readCatId ? GetInt64(reader, "category_id") : 0;
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_monster_groups", "name", t.name);

                            AADB.DB_Quest_Monster_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_interactions ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_NpcInteractions.Clear();

                        while (reader.Read())
                        {
                            var t = new GameNpcInteractions();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.npc_interaction_set_id = GetInt64(reader, "npc_interaction_set_id");
                            t.skill_id = GetInt64(reader, "skill_id");

                            AADB.DB_NpcInteractions.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM ai_files ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_AiFiles.Clear();

                        while (reader.Read())
                        {
                            var t = new GameAiFiles();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.param_template = GetString(reader, "param_template");

                            AADB.DB_AiFiles.Add(t.id, t);
                        }
                    }
                }
            }

        }

        private void LoadFactions()
        {
            string sql = "SELECT * FROM system_factions ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_GameSystem_Factions.Clear();
                        while (reader.Read())
                        {
                            var t = new GameSystemFaction();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.owner_name = GetString(reader, "owner_name");
                            t.owner_type_id = GetInt64(reader, "owner_type_id");
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.political_system_id = GetInt64(reader, "political_system_id");
                            t.mother_id = GetInt64(reader, "mother_id");
                            t.aggro_link = GetBool(reader, "aggro_link");
                            t.guard_help = GetBool(reader, "guard_help");
                            t.is_diplomacy_tgt = GetBool(reader, "is_diplomacy_tgt");
                            t.diplomacy_link_id = GetInt64(reader, "diplomacy_link_id");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "system_factions", "name", t.name);
                            // Actuall not even sure if owner_name can be localized, also not sure yet what owner_id points to
                            if (t.owner_name != string.Empty)
                                t.owner_nameLocalized =
                                    AADB.GetTranslationByID(t.id, "system_factions", "name", t.owner_name);
                            else
                                t.owner_nameLocalized = "";
                            t.SearchString = t.name + " " + t.nameLocalized + " " + t.owner_nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_GameSystem_Factions.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM system_faction_relations ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_GameSystem_Faction_Relations.Clear();
                        while (reader.Read())
                        {
                            var t = new GameSystemFactionRelation();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.faction1_id = GetInt64(reader, "faction1_id");
                            t.faction2_id = GetInt64(reader, "faction2_id");
                            t.state_id = GetInt64(reader, "state_id");
                            AADB.DB_GameSystem_Faction_Relations.Add(t.id, t);
                        }
                    }
                }
            }

        }

        private void LoadDoodads()
        {
            // doodad_almighties
            string sql = "SELECT * FROM doodad_almighties ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Almighties.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasMileStoneID = (columnNames.IndexOf("milestone_id") > 0);
                        bool hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodad();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.model = GetString(reader, "model");
                            t.once_one_man = GetBool(reader, "once_one_man");
                            t.once_one_interaction = GetBool(reader, "once_one_interaction");
                            t.show_name = GetBool(reader, "show_name");
                            t.mgmt_spawn = GetBool(reader, "mgmt_spawn");
                            t.percent = GetInt64(reader, "percent");
                            t.min_time = GetInt64(reader, "min_time");
                            t.max_time = GetInt64(reader, "max_time");
                            t.model_kind_id = GetInt64(reader, "model_kind_id");
                            t.use_creator_faction = GetBool(reader, "use_creator_faction");
                            t.force_tod_top_priority = GetBool(reader, "force_tod_top_priority");
                            if (hasMileStoneID)
                                t.milestone_id = GetInt64(reader, "milestone_id");
                            else
                                t.milestone_id = -1;
                            t.group_id = GetInt64(reader, "group_id");
                            t.show_minimap = GetBool(reader, "show_minimap");
                            t.use_target_decal = GetBool(reader, "use_target_decal");
                            t.use_target_silhouette = GetBool(reader, "use_target_silhouette");
                            t.use_target_highlight = GetBool(reader, "use_target_highlight");
                            t.target_decal_size = GetFloat(reader, "target_decal_size");
                            t.sim_radius = GetInt64(reader, "sim_radius");
                            t.collide_ship = GetBool(reader, "collide_ship");
                            t.collide_vehicle = GetBool(reader, "collide_vehicle");
                            t.climate_id = GetInt64(reader, "climate_id");
                            t.save_indun = GetBool(reader, "save_indun");
                            t.mark_model = GetString(reader, "mark_model");
                            t.force_up_action = GetBool(reader, "force_up_action");
                            t.load_model_from_world = GetBool(reader, "load_model_from_world");
                            t.parentable = GetBool(reader, "parentable");
                            t.childable = GetBool(reader, "childable");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.growth_time = GetInt64(reader, "growth_time");
                            t.despawn_on_collision = GetBool(reader, "despawn_on_collision");
                            t.no_collision = GetBool(reader, "no_collision");
                            t.restrict_zone_id = GetInt64(reader, "restrict_zone_id");
                            if (hasTranslate)
                                t.translate = GetBool(reader, "translate");
                            else
                                t.translate = false;

                            // Helpers
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_almighties", "name", t.name);
                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_Doodad_Almighties.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_groups
            sql = "SELECT * FROM doodad_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();

                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Groups.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasName = (columnNames.IndexOf("name") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadGroup();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            if (hasName)
                                t.name = GetString(reader, "name");
                            else
                                t.name = string.Empty;
                            t.is_export = GetBool(reader, "is_export");
                            t.guard_on_field_time = GetInt64(reader, "guard_on_field_time");
                            t.removed_by_house = GetBool(reader, "removed_by_house");

                            // Helpers
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_groups", "name", t.name);
                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_Doodad_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_funcs
            sql = "SELECT * FROM doodad_funcs ORDER BY doodad_func_group_id ASC, actual_func_id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Funcs.Clear();
                        while (reader.Read())
                        {
                            var t = new GameDoodadFunc();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.doodad_func_group_id = GetInt64(reader, "doodad_func_group_id");
                            t.actual_func_id = GetInt64(reader, "actual_func_id");
                            t.actual_func_type = GetString(reader, "actual_func_type");
                            t.next_phase = GetInt64(reader, "next_phase");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.func_skill_id = GetInt64(reader, "func_skill_id");
                            t.perm_id = GetInt64(reader, "perm_id");
                            t.act_count = GetInt64(reader, "act_count");
                            t.popup_warn = GetBool(reader, "popup_warn");
                            t.forbid_on_climb = GetBool(reader, "forbid_on_climb");

                            AADB.DB_Doodad_Funcs.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_func_groups
            sql = "SELECT * FROM doodad_func_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Func_Groups.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasComment = (columnNames.IndexOf("comment") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadFuncGroup();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.model = GetString(reader, "model");
                            t.doodad_almighty_id = GetInt64(reader, "doodad_almighty_id");
                            t.doodad_func_group_kind_id = GetInt64(reader, "doodad_func_group_kind_id");
                            t.phase_msg = GetString(reader, "phase_msg");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.name = GetString(reader, "name");
                            t.sound_time = GetInt64(reader, "sound_time");
                            if (hasComment)
                                t.comment = GetString(reader, "comment");
                            else
                                t.comment = string.Empty;
                            t.is_msg_to_zone = GetBool(reader, "is_msg_to_zone");

                            // Helpers
                            if (t.name != string.Empty)
                                t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_func_groups", "name");
                            else
                                t.nameLocalized = "";
                            if (t.phase_msgLocalized != string.Empty)
                                t.phase_msgLocalized = AADB.GetTranslationByID(t.id, "doodad_func_groups", "phase_msg");
                            else
                                t.phase_msgLocalized = "";
                            t.SearchString = t.name + " " + t.phase_msg + " " + t.nameLocalized + " " +
                                             t.phase_msgLocalized + " " + t.comment;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Doodad_Func_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_phase_func
            sql = "SELECT * FROM doodad_phase_funcs ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Phase_Funcs.Clear();

                        while (reader.Read())
                        {
                            var t = new GameDoodadPhaseFunc();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.doodad_func_group_id = GetInt64(reader, "doodad_func_group_id");
                            t.actual_func_id = GetInt64(reader, "actual_func_id");
                            t.actual_func_type = GetString(reader, "actual_func_type");

                            AADB.DB_Doodad_Phase_Funcs.Add(t.id, t);
                        }
                    }
                }
            }
        }

        private void LoadQuests()
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Categories.Clear();

                    command.CommandText = "SELECT * FROM quest_categories ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestCategory t = new GameQuestCategory();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_categories", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Quest_Categories.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Contexts.Clear();

                    command.CommandText = "SELECT * FROM quest_contexts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContexts t = new GameQuestContexts();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.category_id = GetInt64(reader, "category_id");
                            t.repeatable = GetBool(reader, "repeatable");
                            t.level = GetInt64(reader, "level");
                            t.selective = GetBool(reader, "selective");
                            t.successive = GetBool(reader, "successive");
                            t.restart_on_fail = GetBool(reader, "restart_on_fail");
                            t.chapter_idx = GetInt64(reader, "chapter_idx");
                            t.quest_idx = GetInt64(reader, "quest_idx");
                            // t.milestone_id = GetInt64(reader, "milestone_id");
                            t.let_it_done = GetBool(reader, "let_it_done");
                            t.detail_id = GetInt64(reader, "detail_id");
                            t.zone_id = GetInt64(reader, "zone_id");
                            t.degree = GetInt64(reader, "degree");
                            t.use_quest_camera = GetBool(reader, "use_quest_camera");
                            t.score = GetInt64(reader, "score");
                            t.use_accept_message = GetBool(reader, "use_accept_message");
                            t.use_complete_message = GetBool(reader, "use_complete_message");
                            t.grade_id = GetInt64(reader, "grade_id");


                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_contexts", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Quest_Contexts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Acts.Clear();

                    command.CommandText = "SELECT * FROM quest_acts ORDER BY quest_component_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestAct t = new GameQuestAct();
                            t.id = GetInt64(reader, "id");
                            t.quest_component_id = GetInt64(reader, "quest_component_id");
                            t.act_detail_id = GetInt64(reader, "act_detail_id");
                            t.act_detail_type = GetString(reader, "act_detail_type");

                            AADB.DB_Quest_Acts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Components.Clear();

                    command.CommandText =
                        "SELECT * FROM quest_components ORDER BY quest_context_id ASC, component_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponent t = new GameQuestComponent();
                            t.id = GetInt64(reader, "id");
                            t.quest_context_id = GetInt64(reader, "quest_context_id");
                            t.component_kind_id = GetInt64(reader, "component_kind_id");
                            t.next_component = GetInt64(reader, "next_component");
                            t.npc_ai_id = GetInt64(reader, "npc_ai_id");
                            t.npc_id = GetInt64(reader, "npc_id");
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.skill_self = GetBool(reader, "skill_self");
                            t.ai_path_name = GetString(reader, "ai_path_name");
                            t.ai_path_type_id = GetInt64(reader, "ai_path_type_id");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.npc_spawner_id = GetInt64(reader, "npc_spawner_id");
                            t.play_cinema_before_bubble = GetBool(reader, "play_cinema_before_bubble");
                            t.ai_command_set_id = GetInt64(reader, "ai_command_set_id");
                            t.or_unit_reqs = GetBool(reader, "or_unit_reqs");
                            t.cinema_id = GetInt64(reader, "cinema_id");
                            t.summary_voice_id = GetInt64(reader, "summary_voice_id");
                            t.hide_quest_marker = GetBool(reader, "hide_quest_marker");
                            t.buff_id = GetInt64(reader, "buff_id");
                            AADB.DB_Quest_Components.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Component_Texts.Clear();

                    command.CommandText =
                        "SELECT * FROM quest_component_texts ORDER BY quest_component_id ASC, quest_component_text_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponentText t = new GameQuestComponentText();
                            t.id = GetInt64(reader, "id");
                            t.quest_component_text_kind_id = GetInt64(reader, "quest_component_text_kind_id");
                            t.quest_component_id = GetInt64(reader, "quest_component_id");
                            t.text = GetString(reader, "text");

                            t.textLocalized = AADB.GetTranslationByID(t.id, "quest_component_texts", "text", t.text);

                            AADB.DB_Quest_Component_Texts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Context_Texts.Clear();

                    command.CommandText = "SELECT * FROM quest_context_texts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContextText t = new GameQuestContextText();
                            t.id = GetInt64(reader, "id");
                            t.quest_context_text_kind_id = GetInt64(reader, "quest_context_text_kind_id");
                            t.quest_context_id = GetInt64(reader, "quest_context_id");
                            t.text = GetString(reader, "text");

                            t.textLocalized = AADB.GetTranslationByID(t.id, "quest_context_texts", "text", t.text);

                            AADB.DB_Quest_Context_Texts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }



            }

        }

        private void LoadTags()
        {
            using (var connection = SQLite.CreateConnection())
            {
                // Tag Names
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Tags.Clear();

                    command.CommandText = "SELECT * FROM tags ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var hasDesc = reader.GetColumnNames().Contains("desc");

                        while (reader.Read())
                        {
                            GameTags t = new GameTags();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            if (hasDesc)
                                t.desc = GetString(reader, "desc");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "tags", "name", t.name);
                            t.descLocalized = AADB.GetTranslationByID(t.id, "tags", "desc", t.desc);

                            t.SearchString = t.name + " " + t.nameLocalized + " " + t.desc + " " + t.descLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Tags.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Buff Tags
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Tagged_Buffs.Clear();
                    command.CommandText = "SELECT * FROM tagged_buffs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("id"))
                            {
                                GameTaggedValues t = new GameTaggedValues();
                                t.id = GetInt64(reader, "id");
                                t.tag_id = GetInt64(reader, "tag_id");
                                t.target_id = GetInt64(reader, "buff_id");
                                AADB.DB_Tagged_Buffs.Add(t.id, t);
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Items Tags
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Tagged_Items.Clear();
                    command.CommandText = "SELECT * FROM tagged_items ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("id"))
                            {
                                GameTaggedValues t = new GameTaggedValues();
                                t.id = GetInt64(reader, "id");
                                t.tag_id = GetInt64(reader, "tag_id");
                                t.target_id = GetInt64(reader, "item_id");
                                AADB.DB_Tagged_Items.Add(t.id, t);
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // NPC Tags
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Tagged_NPCs.Clear();
                    command.CommandText = "SELECT * FROM tagged_npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("id"))
                            {
                                GameTaggedValues t = new GameTaggedValues();
                                t.id = GetInt64(reader, "id");
                                t.tag_id = GetInt64(reader, "tag_id");
                                t.target_id = GetInt64(reader, "npc_id");
                                AADB.DB_Tagged_NPCs.Add(t.id, t);
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Skill Tags
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Tagged_Skills.Clear();
                    command.CommandText = "SELECT * FROM tagged_skills ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("id"))
                            {
                                GameTaggedValues t = new GameTaggedValues();
                                t.id = GetInt64(reader, "id");
                                t.tag_id = GetInt64(reader, "tag_id");
                                t.target_id = GetInt64(reader, "skill_id");
                                AADB.DB_Tagged_Skills.Add(t.id, t);
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }

        }

        private void LoadZoneGroupBannedTags()
        {
            string sql = "SELECT * FROM zone_group_banned_tags ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Zone_Group_Banned_Tags.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        var colNames = reader.GetColumnNames();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("id"))
                            {
                                GameZoneGroupBannedTags t = new GameZoneGroupBannedTags();
                                t.id = GetInt64(reader, "id");
                                t.zone_group_id = GetInt64(reader, "zone_group_id");
                                t.tag_id = GetInt64(reader, "tag_id");
                                if (colNames.Contains("banned_periods_id"))
                                    t.banned_periods_id = GetInt64(reader, "banned_periods_id");
                                else if (colNames.Contains("banned_periods"))
                                    t.banned_periods_id = GetInt64(reader, "banned_periods");
                                else
                                    t.banned_periods_id = 0;
                                if (colNames.Contains("usage"))
                                    t.usage = GetString(reader, "usage");
                                else
                                    t.usage = String.Empty;
                                AADB.DB_Zone_Group_Banned_Tags.Add(t.id, t);
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LoadBuffs()
        {
            string sql = "SELECT * FROM buffs ORDER BY id ASC";
            string triggerSql = "SELECT * FROM buff_triggers ORDER BY id ASC";
            string npcInitialSql = "SELECT * FROM npc_initial_buffs ORDER BY npc_id ASC";
            string passiveSql = "SELECT * FROM passive_buffs ORDER BY id ASC";
            string slavePassiveSql = "SELECT * FROM slave_passive_buffs ORDER BY owner_id ASC";
            string slaveInitialSql = "SELECT * FROM slave_initial_buffs ORDER BY slave_id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                Application.UseWaitCursor = true;
                Cursor = Cursors.WaitCursor;

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Buffs.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        // Get a list of all fields
                        var cols = reader.GetColumnNames();
                        // Remove those that we'll specifically load
                        cols.Remove("id");
                        cols.Remove("name");
                        cols.Remove("desc");
                        cols.Remove("icon_id");
                        cols.Remove("duration");
                        cols.Remove("name_tr");
                        cols.Remove("desc_tr");

                        while (reader.Read())
                        {

                            GameBuff t = new GameBuff();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.desc = GetString(reader, "desc");
                            t.icon_id = GetInt64(reader, "icon_id");
                            t.duration = GetInt64(reader, "duration");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "buffs", "name", t.name);
                            t.descLocalized = AADB.GetTranslationByID(t.id, "buffs", "desc", t.desc);

                            t.SearchString = t.name + " " + t.nameLocalized + " " + t.desc + " " + t.descLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            // Read remaining data
                            foreach (var c in cols)
                            {
                                if (!reader.IsDBNull(c))
                                {
                                    var v = reader.GetString(c, string.Empty);
                                    var isnumber = double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture,
                                        out var dVal);
                                    if (isnumber)
                                    {
                                        if (dVal != 0f)
                                            t._others.Add(c, v);
                                    }
                                    else if ((v != string.Empty) && (v != "0") && (v != "f") && (v != "NULL") &&
                                             (v != "--- :null"))
                                    {
                                        t._others.Add(c, v);
                                    }
                                }
                            }

                            AADB.DB_Buffs.Add(t.id, t);
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_BuffTriggers.Clear();
                    command.CommandText = triggerSql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {

                        while (reader.Read())
                        {
                            GameBuffTrigger t = new GameBuffTrigger();
                            t.id = GetInt64(reader, "id");
                            t.buff_id = GetInt64(reader, "buff_id");
                            t.event_id = GetInt64(reader, "event_id");
                            t.effect_id = GetInt64(reader, "effect_id");

                            AADB.DB_BuffTriggers.Add(t.id, t);
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_NpcInitialBuffs.Clear();
                    command.CommandText = npcInitialSql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        List<string> columnNames = null;
                        var indx = 1L;
                        var readId = false;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameNpcInitialBuffs();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.npc_id = GetInt64(reader, "npc_id");
                            t.buff_id = GetInt64(reader, "buff_id");

                            AADB.DB_NpcInitialBuffs.Add(t.id, t);
                            indx++;
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Passive_Buffs.Clear();
                    command.CommandText = passiveSql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {

                        while (reader.Read())
                        {
                            var t = new GamePassiveBuff();
                            t.id = GetInt64(reader, "id");
                            t.ability_id = GetInt64(reader, "ability_id");
                            t.level = GetInt64(reader, "level");
                            t.buff_id = GetInt64(reader, "buff_id");
                            t.req_points = GetInt64(reader, "req_points");
                            t.active = GetBool(reader, "active");

                            AADB.DB_Passive_Buffs.Add(t.id, t);
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Passive_Buffs.Clear();
                    command.CommandText = slavePassiveSql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        List<string> columnNames = null;
                        var readId = false;
                        var indx = 1L;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlavePassiveBuff();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.passive_buff_id = GetInt64(reader, "passive_buff_id");

                            AADB.DB_Slave_Passive_Buffs.Add(t.id, t);
                            indx++;
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Initial_Buffs.Clear();
                    command.CommandText = slaveInitialSql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var indx = 1L;
                        while (reader.Read())
                        {
                            var t = new GameSlaveInitialBuff();
                            //t.id = GetInt64(reader, "id"); no in 3030
                            t.id = indx;
                            t.slave_id = GetInt64(reader, "slave_id");
                            t.buff_id = GetInt64(reader, "buff_id");

                            AADB.DB_Slave_Initial_Buffs.Add(t.id, t);
                            indx++;
                        }
                    }
                }

                Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
            }

        }


        private void LoadTransfers()
        {
            string sql = "SELECT * FROM transfers ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Transfers.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            GameTransfers t = new GameTransfers();
                            t.id = GetInt64(reader, "id");
                            t.model_id = GetInt64(reader, "model_id");
                            t.path_smoothing = GetFloat(reader, "path_smoothing");

                            AADB.DB_Transfers.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadTransferPaths()
        {
            string sql = "SELECT * FROM transfer_paths ORDER BY owner_id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_TransferPaths.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            GameTransferPaths t = new GameTransferPaths();
                            //t.id = GetInt64(reader, "id");
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.path_name = GetString(reader, "path_name");
                            t.wait_time_start = GetFloat(reader, "wait_time_start");
                            t.wait_time_end = GetFloat(reader, "wait_time_end");

                            AADB.DB_TransferPaths.Add(t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadPlots()
        {
            // base plots
            string sql = "SELECT * FROM plots";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plots.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlot();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.target_type_id = GetInt64(reader, "target_type_id");

                            AADB.DB_Plots.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // events
            sql = "SELECT * FROM plot_events";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plot_Events.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlotEvent();
                            t.id = GetInt64(reader, "id");
                            t.plot_id = GetInt64(reader, "plot_id");
                            t.postion = GetInt64(reader, "position");
                            t.name = GetString(reader, "name");
                            t.source_update_method_id = GetInt64(reader, "source_update_method_id");
                            t.target_update_method_id = GetInt64(reader, "target_update_method_id");
                            t.target_update_method_param1 = GetInt64(reader, "target_update_method_param1");
                            t.target_update_method_param2 = GetInt64(reader, "target_update_method_param2");
                            t.target_update_method_param3 = GetInt64(reader, "target_update_method_param3");
                            t.target_update_method_param4 = GetInt64(reader, "target_update_method_param4");
                            t.target_update_method_param5 = GetInt64(reader, "target_update_method_param5");
                            t.target_update_method_param6 = GetInt64(reader, "target_update_method_param6");
                            t.target_update_method_param7 = GetInt64(reader, "target_update_method_param7");
                            t.target_update_method_param8 = GetInt64(reader, "target_update_method_param8");
                            t.target_update_method_param9 = GetInt64(reader, "target_update_method_param9");
                            t.tickets = GetInt64(reader, "tickets");
                            t.aeo_diminishing = GetBool(reader, "aoe_diminishing");

                            AADB.DB_Plot_Events.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // next events
            sql = "SELECT * FROM plot_next_events";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plot_Next_Events.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlotNextEvent();
                            t.id = GetInt64(reader, "id");
                            t.event_id = GetInt64(reader, "event_id");
                            t.postion = GetInt64(reader, "position");
                            t.next_event_id = GetInt64(reader, "next_event_id");
                            t.delay = GetInt64(reader, "delay");
                            t.speed = GetInt64(reader, "speed");

                            AADB.DB_Plot_Next_Events.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // plot events condition
            sql = "SELECT * FROM plot_event_conditions";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plot_Event_Conditions.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlotEventCondition();
                            t.id = GetInt64(reader, "id");
                            t.event_id = GetInt64(reader, "event_id");
                            t.postion = GetInt64(reader, "position");
                            t.condition_id = GetInt64(reader, "condition_id");
                            t.source_id = GetInt64(reader, "source_id");
                            t.target_id = GetInt64(reader, "target_id");
                            t.notify_failure = GetBool(reader, "notify_failure");

                            AADB.DB_Plot_Event_Conditions.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // plot condition
            sql = "SELECT * FROM plot_conditions";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plot_Conditions.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GamePlotCondition();
                            t.id = GetInt64(reader, "id");
                            t.not_condition = GetBool(reader, "not_condition");
                            t.kind_id = GetInt64(reader, "kind_id");
                            t.param1 = GetInt64(reader, "param1");
                            t.param2 = GetInt64(reader, "param2");
                            t.param3 = GetInt64(reader, "param3");

                            AADB.DB_Plot_Conditions.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // plot effects
            sql = "SELECT * FROM plot_effects";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Plot_Effects.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GamePlotEffect();
                            t.id = GetInt64(reader, "id");
                            t.event_id = GetInt64(reader, "event_id");
                            t.position = GetInt64(reader, "position");
                            t.source_id = GetInt64(reader, "source_id");
                            t.target_id = GetInt64(reader, "target_id");
                            t.actual_id = GetInt64(reader, "actual_id");
                            t.actual_type = GetString(reader, "actual_type");

                            AADB.DB_Plot_Effects.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            dgvItem.Rows.Clear();
            string searchText = cbItemSearch.Text;
            if (searchText == string.Empty)
                return;
            string lng = cbItemSearchLanguage.Text;
            string sql = string.Empty;
            string sqlWhere = string.Empty;
            long searchID;
            bool SearchByID = false;
            if (long.TryParse(searchText, out searchID))
                SearchByID = true;
            bool showFirst = true;
            long firstResult = -1;
            string searchTextLower = searchText.ToLower();

            // More Complex syntax with category names
            // SELECT t1.idx, t1.ru, t1.ru_ver, t2.ID, t2.category_id, t3.name, t4.en_us FROM localized_texts as t1 LEFT JOIN items as t2 ON (t1.idx = t2.ID) LEFT JOIN item_categories as t3 ON (t2.category_id = t3.ID) LEFT JOIN localized_texts as t4 ON ((t4.idx = t3.ID) AND (t4.tbl_name = 'item_categories') AND (t4.tbl_column_name = 'name') ) WHERE (t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1.ru LIKE '%Камень%') ORDER BY t1.ru ASC

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvItem.Visible = false;

            // Optimization
            var oldResizeMode = dgvItem.RowHeadersWidthSizeMode;
            var oldHeaderVisible = dgvItem.RowHeadersVisible;
            dgvItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgvItem.RowHeadersVisible = false;
            bool filterByType = cbItemSearchType.SelectedIndex > 0;
            var filterType = cbItemSearchType.SelectedIndex - 1;

            using (var loadform = new LoadingForm())
            {
                loadform.Show();
                loadform.ShowInfo($"Scanning {AADB.DB_Items.Count} items");
                var i = 0;

                dgvItem.Hide();

                foreach (var item in AADB.DB_Items)
                {
                    i++;
                    if ((cbItemSearchRange.SelectedIndex == 1) && (item.Key < 8000000))
                        continue;
                    else if ((cbItemSearchRange.SelectedIndex == 2) && (item.Key < 9000000))
                        continue;

                    bool addThis = false;
                    if (SearchByID)
                    {
                        if (item.Key == searchID)
                        {
                            addThis = true;
                        }
                    }
                    else if (item.Value.SearchString.IndexOf(searchTextLower) >= 0)
                        addThis = true;

                    // Hardcode * as add all if armor slot is provided
                    if ((searchTextLower == "*") && ((cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0) ||
                                                     (cbItemSearchItemCategoryTypeList.SelectedIndex > 0) ||
                                                     (cbItemSearchRange.SelectedIndex > 0) || (filterByType)))
                        addThis = true;

                    if (addThis && filterByType)
                    {
                        if (filterType != item.Value.impl_id)
                            addThis = false;
                    }

                    if (addThis && (cbItemSearchItemCategoryTypeList.SelectedIndex > 0))
                    {
                        if (long.TryParse(cbItemSearchItemCategoryTypeList.SelectedValue.ToString(), out var cId))
                            if (cId != item.Value.catgegory_id)
                                addThis = false;
                    }

                    if (addThis && (cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0))
                    {
                        if (item.Value.item_armors == null)
                            addThis = false;
                        else if (cbItemSearchItemArmorSlotTypeList.SelectedIndex != item.Value.item_armors.slot_type_id)
                        {
                            addThis = false;
                        }
                    }

                    if (addThis)
                    {
                        int line = dgvItem.Rows.Add();
                        var row = dgvItem.Rows[line];
                        long itemIdx = item.Value.id;
                        if (firstResult < 0)
                            firstResult = itemIdx;
                        row.Cells[0].Value = itemIdx.ToString();
                        row.Cells[1].Value = item.Value.nameLocalized;

                        if (showFirst)
                        {
                            showFirst = false;
                            ShowDBItem(itemIdx);
                        }

                        if ((dgvItem.Rows.Count % 25) == 0)
                            loadform.ShowInfo($"Scanning {i}/{AADB.DB_Items.Count} items");

                        if (dgvItem.Rows.Count > 500)
                        {
                            MessageBox.Show(
                                "Too many result items, list is cut off at 500. Try narrowing your search!");
                            break;
                        }
                    }
                }

                dgvItem.Show();


            }

            // Optimization
            dgvItem.RowHeadersWidthSizeMode = oldResizeMode;
            dgvItem.RowHeadersVisible = oldHeaderVisible;

            dgvItem.Visible = true;
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

            if (firstResult >= 0)
            {
                ShowDBItem(firstResult);
                AddToSearchHistory(cbItemSearch, searchTextLower);
            }
        }

        /// <summary>
        /// Convert a hex string to a .NET Color object.
        /// </summary>
        /// <param name="hexColor">a hex string: "FFFFFFFF", "00000000"</param>
        public static Color HexStringToARGBColor(string hexARGB)
        {
            string a;
            string r;
            string g;
            string b;
            if (hexARGB.Length == 8)
            {
                a = hexARGB.Substring(0, 2);
                r = hexARGB.Substring(2, 2);
                g = hexARGB.Substring(4, 2);
                b = hexARGB.Substring(6, 2);
            }
            else if (hexARGB.Length == 6)
            {
                a = hexARGB.Substring(0, 2);
                r = hexARGB.Substring(2, 2);
                g = hexARGB.Substring(4, 2);
                b = hexARGB.Substring(6, 2);
            }
            else
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("hexColor is not exactly 6 digits.");
                return Color.Empty;
            }

            Color color = Color.Empty;
            try
            {
                int ai = Int32.Parse(a, System.Globalization.NumberStyles.HexNumber);
                int ri = Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
                color = Color.FromArgb(ai, ri, gi, bi);
            }
            catch
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("Conversion failed.");
                return Color.Empty;
            }

            return color;
        }

        private void FormattedTextToRichtEditOld(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
            restText = restText.Replace("|nc;", "|cFFFFFFFF;");
            var colStartPos = -1;
            var colResetPos = -1;
            bool invalidPipe = false;
            while (restText.Length > 0)
            {
                var pipeStart = restText.IndexOf("|");
                var atStart = restText.IndexOf("@");

                if ((pipeStart >= 0) && (atStart >= 0) && (atStart < pipeStart))
                {
                    pipeStart = -1;
                }

                if ((atStart >= 0) && (restText.Length >= pipeStart + 12))
                {
                    var atStartBracket = restText.IndexOf("(", atStart);
                    var atEndBracket = restText.IndexOf(")", atStart);
                    if ((atStartBracket >= 0) && (atEndBracket >= 0) && (atEndBracket > atStartBracket))
                    {
                        var fieldNameStr = restText.Substring(atStart + 1, atStartBracket - atStart - 1);
                        var fieldValStr = restText.Substring(atStartBracket + 1, atEndBracket - atStartBracket - 1);
                        if (long.TryParse(fieldValStr, out long itemVal))
                        {
                            rt.AppendText(restText.Substring(0, atStart));
                            rt.SelectionColor = Color.Yellow;
                            if ((fieldNameStr == "ITEM_NAME") &&
                                (AADB.DB_Items.TryGetValue(itemVal, out GameItem item)))
                            {
                                rt.AppendText(item.nameLocalized);
                            }
                            else if ((fieldNameStr == "NPC_NAME") &&
                                     (AADB.DB_NPCs.TryGetValue(itemVal, out GameNPC npc)))
                            {
                                rt.AppendText(npc.nameLocalized);
                            }
                            else if ((fieldNameStr == "QUEST_NAME") &&
                                     (AADB.DB_Quest_Contexts.TryGetValue(itemVal, out GameQuestContexts quest)))
                            {
                                rt.AppendText(quest.nameLocalized);
                            }
                            else
                            {
                                rt.AppendText("@" + fieldNameStr + "(" + itemVal.ToString() + ")");
                            }

                            rt.SelectionColor = rt.ForeColor;

                            restText = restText.Substring(atEndBracket + 1);
                            pipeStart = -1;
                        }
                    }
                }


                if ((pipeStart >= 0) && (restText.Length >= pipeStart + 1))
                {
                    var pipeOption = restText.Substring(pipeStart + 1, 1);
                    colStartPos = -1;
                    colResetPos = -1;
                    invalidPipe = false;
                    switch (pipeOption)
                    {
                        case "c":
                            if (restText.Length >= pipeStart + 10)
                                colStartPos = pipeStart;
                            break;
                        case "r":
                            if (restText.Length >= pipeStart + 1)
                                colResetPos = pipeStart;
                            break;
                        default:
                            invalidPipe = true;
                            break;
                    }
                }
                else
                {
                    colStartPos = -1;
                    colResetPos = -1;
                }

                if ((invalidPipe) && (pipeStart >= 0))
                {
                    // We need to handle this in order to fix bugs related to text formatting
                    rt.AppendText(restText.Substring(0, pipeStart));
                    rt.SelectionColor = rt.ForeColor;
                    rt.AppendText("|");
                    restText = restText.Substring(pipeStart + 1);
                }
                else if ((colStartPos >= 0) && (restText.Length >= colStartPos + 10))
                {
                    var colText = restText.Substring(colStartPos + 2, 8);
                    rt.AppendText(restText.Substring(0, colStartPos));
                    var newCol = HexStringToARGBColor(colText);
                    rt.SelectionColor = newCol;
                    restText = restText.Substring(colStartPos + 10);
                }
                else if ((colResetPos >= 0) && (restText.Length >= colResetPos + 2))
                {
                    rt.AppendText(restText.Substring(0, colResetPos));
                    rt.SelectionColor = rt.ForeColor;
                    restText = restText.Substring(colResetPos + 2);
                }
                else if (atStart >= 0)
                {
                    // already handled
                }
                else
                {
                    rt.AppendText(restText);
                    restText = string.Empty;
                }
            }

        }

        private void FormattedTextToRichtEdit(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            //restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
            var resetColor = rt.ForeColor;

            while (restText.Length > 0)
            {
                var nextEndBracket = restText.IndexOf(")");
                var nextEndAccolade = restText.IndexOf("}");
                if (restText.StartsWith("\r")) // linefeed
                {
                    var currentCol = rt.SelectionColor;
                    rt.AppendText("\r");
                    rt.SelectionColor = currentCol;
                    restText = restText.Substring(1);
                }
                else if (restText.StartsWith("|r")) // reset color
                {
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(2);
                }
                else if (restText.StartsWith("[START_DESCRIPTION]")) // reset color
                {
                    resetColor = Color.LimeGreen;
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(19);
                }
                else if (restText.StartsWith("|nc;")) // named color ?
                {
                    rt.SelectionColor = Color.White;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|ni;")) // named color ?
                {
                    rt.SelectionColor = Color.GreenYellow;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nd;")) // named color ?
                {
                    rt.SelectionColor = Color.OrangeRed;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nr;")) // named color ?
                {
                    rt.SelectionColor = Color.Red;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|c")) // hexcode color
                {
                    rt.SelectionColor = HexStringToARGBColor(restText.Substring(2, 8));
                    restText = restText.Substring(10);
                }
                else if (restText.StartsWith("@ITEM_NAME(") && (nextEndBracket > 11))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(11, nextEndBracket - 11);
                    if (long.TryParse(valueText, out var value) && (AADB.DB_Items.TryGetValue(value, out var item)))
                        rt.AppendText(item.nameLocalized);
                    else
                        rt.AppendText("@ITEM_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@DOODAD_NAME(") && (nextEndBracket > 13))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(13, nextEndBracket - 13);
                    if (long.TryParse(valueText, out var value) &&
                        (AADB.DB_Doodad_Almighties.TryGetValue(value, out var doodad)))
                        rt.AppendText(doodad.nameLocalized);
                    else
                        rt.AppendText("@DOODAD_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_NAME(") && (nextEndBracket > 10))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(10, nextEndBracket - 10);
                    if (long.TryParse(valueText, out var value) && (AADB.DB_NPCs.TryGetValue(value, out var npc)))
                        rt.AppendText(npc.nameLocalized);
                    else
                        rt.AppendText("@NPC_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_GROUP_NAME(") && (nextEndBracket > 16))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(16, nextEndBracket - 16);
                    if (long.TryParse(valueText, out var value) && (AADB.DB_Quest_Monster_Groups.TryGetValue(value, out var npcGroup)))
                        rt.AppendText(npcGroup.nameLocalized);
                    else
                        rt.AppendText("@NPC_GROUP_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@QUEST_NAME(") && (nextEndBracket > 12))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(12, nextEndBracket - 12);
                    if (long.TryParse(valueText, out var value) &&
                        (AADB.DB_Quest_Contexts.TryGetValue(value, out var quest)))
                        rt.AppendText(quest.nameLocalized);
                    else
                        rt.AppendText("@QUEST_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("#{") && (nextEndAccolade > 2))
                {
                    rt.SelectionColor = Color.Pink;
                    var valueText = restText.Substring(2, nextEndAccolade - 2);
                    rt.AppendText("#{" + valueText + "}");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndAccolade + 1);
                }
                else
                {
                    rt.AppendText(restText.Substring(0, 1));
                    restText = restText.Substring(1);
                }
            }
        }

        private int IconIDToLabel(long icon_id, Label iconImgLabel)
        {
            var cachedImageIndex = -1;
            if (pak.IsOpen)
            {
                if (AADB.DB_Icons.TryGetValue(icon_id, out var iconname))
                {
                    var fn = "game/ui/icon/" + iconname;

                    if (pak.FileExists(fn))
                    {
                        try
                        {
                            // Is it cached already ?
                            cachedImageIndex = ilIcons.Images.IndexOfKey(icon_id.ToString());
                            if (cachedImageIndex >= 0)
                            {
                                if (iconImgLabel != null)
                                {
                                    iconImgLabel.Image = ilIcons.Images[cachedImageIndex];
                                    iconImgLabel.Text = "";
                                }
                            }
                            else
                            {
                                // Load from pak if not cached
                                var fStream = pak.ExportFileAsStream(fn);
                                var bmp = AAEmu.Tools.BitmapUtil.ReadDDSFromStream(fStream);

                                if (iconImgLabel != null)
                                {
                                    iconImgLabel.Image = bmp;
                                    iconImgLabel.Text = "";
                                }

                                // If not loaded into image list yet, do it now
                                ilIcons.Images.Add(icon_id.ToString(), bmp);
                                ilMiniIcons.Images.Add(icon_id.ToString(), bmp);
                                cachedImageIndex = ilIcons.Images.IndexOfKey(icon_id.ToString());
                            }
                            // itemIcon.Text = "[" + iconname + "]";
                        }
                        catch
                        {
                            if (iconImgLabel != null)
                            {
                                iconImgLabel.Image = null;
                                iconImgLabel.Text = "ERROR - " + iconname;
                            }
                        }
                    }
                    else if (iconImgLabel != null)
                    {
                        iconImgLabel.Image = null;
                        iconImgLabel.Text = "NOT FOUND - " + iconname + " ?";
                    }
                }
                else if (iconImgLabel != null)
                {
                    iconImgLabel.Image = null;
                    iconImgLabel.Text = icon_id.ToString() + "?";
                }
            }
            else if (iconImgLabel != null)
            {
                iconImgLabel.Image = null;
                iconImgLabel.Text = icon_id.ToString();
            }

            return cachedImageIndex;
        }

        private string MSToString(long msTime, bool dontShowMS = false)
        {
            string res = string.Empty;
            long ms = (msTime % 1000);
            msTime = msTime / 1000;
            long ss = (msTime % 60);
            msTime = msTime / 60;
            long mm = (msTime % 60);
            msTime = msTime / 60;
            long hh = (msTime % 24);
            msTime = msTime / 24;
            long dd = msTime;

            if (dd > 0)
                res += dd.ToString() + "d ";
            if (hh > 0)
                res += hh.ToString() + "h ";
            if (mm > 0)
                res += mm.ToString() + "m ";
            if (dontShowMS == false)
            {
                if (ss > 0)
                    res += ss.ToString() + "s ";
                if (ms > 0)
                    res += ms.ToString() + "ms";
            }
            else if ((ss > 0) || (ms > 0))
            {
                float s = ss + (1f / 1000f * ms);
                res += s.ToString() + "s";
            }

            if (res == string.Empty)
                res = "none";

            return res;
        }

        private string SecondsToString(long secTime)
        {
            string res = string.Empty;
            long ss = (secTime % 60);
            secTime = secTime / 60;
            long mm = (secTime % 60);
            secTime = secTime / 60;
            long hh = (secTime % 24);
            secTime = secTime / 24;
            long dd = secTime;

            if (dd > 0)
                res += dd.ToString() + "d ";
            if (hh > 0)
                res += hh.ToString() + "h ";
            if (mm > 0)
                res += mm.ToString() + "m ";
            if (ss > 0)
                res += ss.ToString() + "s ";

            if (res == string.Empty)
                res = "none";

            return res;
        }

        private string RangeToString(float range)
        {
            if (Math.Abs(range) < 150.0f)
            {
                return range.ToString("0") + " mm";
            }
            else if (Math.Abs(range) < 1000.0f)
            {
                return (range / 10).ToString("0") + " cm";
            }
            else if (Math.Abs(range) < 15000.0f)
            {
                return (range / 1000).ToString("0.0") + " m";
            }
            else
            {
                return (range / 1000).ToString("0") + " m";
            }
        }

        private void ShowDBItem(long idx)
        {
            if (AADB.DB_Items.TryGetValue(idx, out var item))
            {
                lItemID.Text = idx.ToString();
                lItemImplId.Text = ((GameItemImplId)item.impl_id).ToString() + " (" + item.impl_id.ToString() + ")";
                lItemName.Text = item.nameLocalized;
                lItemCategory.Text = AADB.GetTranslationByID(item.catgegory_id, "item_categories", "name") + " (" +
                                     item.catgegory_id.ToString() + ")";
                var fullDescription = item.descriptionLocalized;
                if (AADB.DB_Skills.TryGetValue(item.use_skill_id, out var useSkill))
                {
                    if (!string.IsNullOrWhiteSpace(fullDescription))
                        fullDescription += "\r\r";
                    fullDescription += "[START_DESCRIPTION]" + useSkill.descriptionLocalized;
                }

                FormattedTextToRichtEdit(fullDescription, rtItemDesc);
                lItemLevel.Text = item.level.ToString();
                IconIDToLabel(item.icon_id, itemIcon);
                btnFindItemSkill.Enabled = true; // (item.use_skill_id > 0);
                var gmadditem = cbNewGM.Checked ? "/item add self " : "/additem ";
                gmadditem += item.id.ToString();
                gmadditem += " " + item.max_stack_size.ToString();
                if (item.fixed_grade >= 0)
                    gmadditem += " " + item.fixed_grade.ToString();
                lItemTags.Text = TagsAsString(idx, AADB.DB_Tagged_Items);
                lItemAddGMCommand.Text = gmadditem;

                ShowSelectedData("items", "(id = " + idx.ToString() + ")", "id ASC");
            }
            else
            {
                lItemID.Text = idx.ToString();
                lItemImplId.Text = "";
                lItemName.Text = "<not found>";
                lItemCategory.Text = "";
                rtItemDesc.Clear();
                lItemLevel.Text = "";
                itemIcon.Text = "???";
                btnFindItemSkill.Enabled = false;
                lItemTags.Text = "???";
                lItemAddGMCommand.Text = cbNewGM.Checked ? "/item add self ???" : "/additem ???";
            }
        }

        private string ConditionTypeName(long id)
        {
            switch (id)
            {
                case 1: return "Level (1)";
                case 2: return "Relation (2)";
                case 3: return "Direction (3)";
                case 4: return "Unk4 (4)";
                case 5: return "BuffTag (5)";
                case 6: return "WeaponEquipStatus (6)";
                case 7: return "Chance (7)";
                case 8: return "Dead (8)";
                case 9: return "CombatDiceResult (9)";
                case 10: return "InstrumentType (10)";
                case 11: return "Range (11)";
                case 12: return "Variable (12)";
                case 13: return "UnitAttrib (13)";
                case 14: return "Actability (14)";
                case 15: return "Stealth (15)";
                case 16: return "Visible (16)";
                case 17: return "ABLevel (17)";
                default: return id.ToString();
            }
        }

        private void AddPlotEventNode(TreeNode parent, GamePlotEvent child, int depth = 0, string nodeNamePrefix = "")
        {
            depth++;
            if (depth > 16)
            {
                var overflowNode = new TreeNode("Too many recursions !");
                overflowNode.ImageIndex = 4;
                overflowNode.SelectedImageIndex = 4;
                overflowNode.Tag = 0;
                parent.Nodes.Add(overflowNode);
                return;
            }

            var nodeName = nodeNamePrefix + child.id.ToString() + " - " + child.name;
            if (child.tickets > 1)
                nodeName = child.tickets.ToString() + " x " + nodeName;
            var eventNode = new TreeNode(nodeName);
            eventNode.ImageIndex = 1;
            eventNode.SelectedImageIndex = 1;
            eventNode.Tag = child.id;

            parent.Nodes.Add(eventNode);

            // Does it have conditions ?
            var plotEventConditions = AADB.DB_Plot_Event_Conditions.Where(pec => pec.Value.event_id == child.id);
            foreach (var plotEventCondition in plotEventConditions)
            {
                var eventConditionName = $"Event Condition ({plotEventCondition.Value.condition_id})";
                var eventConditionNode = new TreeNode(eventConditionName);
                eventConditionNode.ImageIndex = 3;
                eventConditionNode.SelectedImageIndex = 3;
                eventConditionNode.Tag = 0;
                eventNode.Nodes.Add(eventConditionNode);
                eventConditionNode.Nodes.Add("Source: " + PlotUpdateMethode(plotEventCondition.Value.source_id));
                eventConditionNode.Nodes.Add("Target: " + PlotUpdateMethode(plotEventCondition.Value.target_id));
                eventConditionNode.Nodes.Add("Notify Failure: " + plotEventCondition.Value.notify_failure);
                if (AADB.DB_Plot_Conditions.TryGetValue(plotEventCondition.Value.condition_id, out var condition))
                {
                    eventConditionNode.Text += (condition.not_condition ? " NOT " : " ") + ConditionTypeName(condition.kind_id);
                    // eventConditionNode.Nodes.Add("Condition: " + (condition.not_condition ? "NOT " : "") + ConditionTypeName(condition.kind_id));
                    if (condition.param1 != 0)
                        eventConditionNode.Nodes.Add("Param1: " + condition.param1);
                    if (condition.param2 != 0)
                        eventConditionNode.Nodes.Add("Param2: " + condition.param2);
                    if (condition.param3 != 0)
                        eventConditionNode.Nodes.Add("Param3: " + condition.param3);
                }
            }

            // Plot Effects
            var plotEventEffects = AADB.DB_Plot_Effects.Where(pec => pec.Value.event_id == child.id).OrderBy(pec => pec.Value.position);
            foreach (var plotEffect in plotEventEffects)
            {
                var eventConditionName = $"Plot Effect ({plotEffect.Value.id})";
                var effectNode = new TreeNode(eventConditionName);
                effectNode.ImageIndex = 2;
                effectNode.SelectedImageIndex = 2;
                effectNode.Tag = 0;
                eventNode.Nodes.Add(effectNode);
                effectNode.Nodes.Add("Pos: " + plotEffect.Value.position);
                effectNode.Nodes.Add("Source: " + PlotUpdateMethode(plotEffect.Value.source_id));
                effectNode.Nodes.Add("Target: " + PlotUpdateMethode(plotEffect.Value.target_id));
                // var actualEffectNode = effectNode.Nodes.Add("Actual Effect: " + plotEffect.Value.actual_type + " (" +plotEffect.Value.actual_id + ")");

                CreatePlotEffectNode(plotEffect.Value.actual_type, plotEffect.Value.actual_id, effectNode, true);
            }

            var nextEvents = AADB.DB_Plot_Next_Events.Where(nextEvent => nextEvent.Value.event_id == child.id).OrderBy(p => p.Value.postion);
            foreach (var n in nextEvents)
            {
                if (n.Value.next_event_id == n.Value.event_id)
                {
                    var rNode = eventNode.Nodes.Add("Repeats itself with Delay: " + n.Value.delay + ", Speed: " + n.Value.speed);
                    rNode.ImageIndex = 3;
                    rNode.SelectedImageIndex = rNode.ImageIndex;
                }
                else
                if (AADB.DB_Plot_Events.TryGetValue(n.Value.next_event_id, out var next))
                {
                    //AddPlotEventNode(eventNode, next, depth, "Next Plot Event: ");

                    var nextNode = new TreeNode("Next Event Node: " + next.id.ToString() + " - " + next.name);
                    nextNode.Tag = next.id;
                    nextNode.ImageIndex = 2;
                    nextNode.SelectedImageIndex = nextNode.ImageIndex;
                    eventNode.Nodes.Add(nextNode);
                }
                else
                {
                    var errorNode = new TreeNode("Unknown Next Event: " + n.Value.next_event_id.ToString());
                    errorNode.ImageIndex = 4;
                    errorNode.SelectedImageIndex = 4;
                    errorNode.Tag = 0;
                    eventNode.Nodes.Add(errorNode);
                }

            }
        }

        private void ShowSkillPlotTree(GameSkills skill)
        {
            tvSkill.Nodes.Clear();
            var imgId = ilIcons.Images.IndexOfKey(skill.icon_id.ToString());
            var rootNode = new TreeNode(skill.nameLocalized, imgId, imgId);
            rootNode.Tag = 0;
            tvSkill.Nodes.Add(rootNode);

            var skillEffectsList = from se in AADB.DB_Skill_Effects
                                   where se.Value.skill_id == skill.id
                                   select se.Value;

            TreeNode effectsRoot = null;

            if (skillEffectsList != null)
            {
                foreach (var skillEffect in skillEffectsList)
                {
                    if (effectsRoot == null)
                    {
                        effectsRoot = tvSkill.Nodes.Add("Effects");
                        effectsRoot.ImageIndex = 2;
                        effectsRoot.SelectedImageIndex = 2;
                        effectsRoot.Tag = 0;
                    }

                    CreateSkillEffectNode(skillEffect.effect_id, effectsRoot, true);
                }
            }


            if (AADB.DB_Plots.TryGetValue(skill.plot_id, out var plot))
            {
                var firstPlotNode = new TreeNode(plot.id.ToString() + " - " + plot.name);
                firstPlotNode.ImageIndex = 2;
                firstPlotNode.SelectedImageIndex = 2;
                firstPlotNode.Tag = plot.id;
                tvSkill.Nodes.Add(firstPlotNode);

                var events = AADB.DB_Plot_Events.Where(plotEvent => plotEvent.Value.plot_id == plot.id)
                    .OrderBy(plotEvent => plotEvent.Value.postion);
                foreach (var e in events)
                {
                    AddPlotEventNode(firstPlotNode, e.Value);
                }
            }

            tvSkill.ExpandAll();
            tvSkill.SelectedNode = rootNode;
        }

        private void CreateSkillEffectNode(long effect_id, TreeNode effectsRoot, bool hideEmptyProperties)
        {
            var effectTypeText = "???";
            if (AADB.DB_Effects.TryGetValue(effect_id, out var effect))
            {
                effectTypeText = effect.actual_type + " ( " + effect.actual_id.ToString() + " )";
            }

            var skillEffectNode = effectsRoot.Nodes.Add(effectTypeText);
            skillEffectNode.ImageIndex = 2;
            skillEffectNode.SelectedImageIndex = 2;
            skillEffectNode.Tag = 0;

            if (effect != null)
            {
                var effectsTableName = FunctionTypeToTableName(effect.actual_type);
                var effectValuesList = GetCustomTableValues(effectsTableName, "id", effect.actual_id.ToString());
                foreach (var effectValues in effectValuesList)
                    foreach (var effectValue in effectValues)
                    {
                        var thisNode = AddCustomPropertyNode(effectValue.Key, effectValue.Value, hideEmptyProperties,
                            skillEffectNode);
                        if (thisNode == null)
                            continue;

                        if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
                        {
                            thisNode.ImageIndex = 4;
                            thisNode.SelectedImageIndex = 4;
                        }
                    }
            }
        }

        private void CreatePlotEffectNode(string actualType, long id, TreeNode effectsRoot, bool hideEmptyProperties)
        {
            var effectTypeText = actualType + " ( " + id.ToString() + " )";

            var skillEffectNode = effectsRoot.Nodes.Add(effectTypeText);
            skillEffectNode.ImageIndex = 3;
            skillEffectNode.SelectedImageIndex = 3;
            skillEffectNode.Tag = 0;

            var effectsTableName = FunctionTypeToTableName(actualType);
            var effectValuesList = GetCustomTableValues(effectsTableName, "id", id.ToString());
            foreach (var effectValues in effectValuesList)
                foreach (var effectValue in effectValues)
                {
                    var thisNode = AddCustomPropertyNode(effectValue.Key, effectValue.Value, hideEmptyProperties,
                        skillEffectNode);
                    if (thisNode == null)
                        continue;

                    if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
                    {
                        thisNode.ImageIndex = 4;
                        thisNode.SelectedImageIndex = 4;
                    }
                }
        }

        private void ShowDBSkill(long idx)
        {
            if (AADB.DB_Skills.TryGetValue(idx, out var skill))
            {
                lSkillID.Text = idx.ToString();
                lSkillName.Text = skill.nameLocalized;
                lSkillCost.Text = skill.cost.ToString();
                lSkillMana.Text = skill.mana_cost.ToString();
                lSkillLabor.Text = skill.consume_lp.ToString();
                lSkillCooldown.Text = MSToString(skill.cooldown_time);
                if ((skill.default_gcd) && (!skill.ignore_global_cooldown))
                {
                    lSkillGCD.Text = "Default";
                }
                else if ((!skill.default_gcd) && (!skill.ignore_global_cooldown))
                {
                    lSkillGCD.Text = MSToString(skill.custom_gcd);
                }
                else if ((!skill.default_gcd) && (skill.ignore_global_cooldown))
                {
                    lSkillGCD.Text = "Ignored";
                }
                else
                {
                    lSkillGCD.Text = "Default";
                }

                // lSkillGCD.Text = skill.ignore_global_cooldown ? "Ignore" : "Normal";
                FormattedTextToRichtEdit(skill.descriptionLocalized, rtSkillDescription);
                IconIDToLabel(skill.icon_id, skillIcon);
                lSkillTags.Text = TagsAsString(idx, AADB.DB_Tagged_Skills);

                ShowSelectedData("skills", "(id = " + idx.ToString() + ")", "id ASC");

                if (skill.first_reagent_only)
                {
                    labelSkillReagents.Text = "Requires either of these items to use";
                }
                else
                {
                    labelSkillReagents.Text = "Required items to use this skill";
                }

                // Produces
                dgvSkillProducts.Rows.Clear();
                foreach (var p in AADB.DB_Skill_Products)
                {
                    if (p.Value.skill_id == idx)
                    {
                        var line = dgvSkillProducts.Rows.Add();
                        var row = dgvSkillProducts.Rows[line];
                        row.Cells[0].Value = p.Value.item_id.ToString();
                        if (AADB.DB_Items.TryGetValue(p.Value.item_id, out var item))
                        {
                            row.Cells[1].Value = item.nameLocalized;
                        }
                        else
                        {
                            row.Cells[1].Value = "???";
                        }

                        row.Cells[2].Value = p.Value.amount.ToString();
                    }
                }

                // Reagents
                dgvSkillReagents.Rows.Clear();
                foreach (var p in AADB.DB_Skill_Reagents)
                {
                    if (p.Value.skill_id == idx)
                    {
                        var line = dgvSkillReagents.Rows.Add();
                        var row = dgvSkillReagents.Rows[line];
                        row.Cells[0].Value = p.Value.item_id.ToString();
                        if (AADB.DB_Items.TryGetValue(p.Value.item_id, out var item))
                        {
                            row.Cells[1].Value = item.nameLocalized;
                        }
                        else
                        {
                            row.Cells[1].Value = "???";
                        }

                        row.Cells[2].Value = p.Value.amount.ToString();
                    }
                }

                ShowSkillPlotTree(skill);
            }
            else
            {
                lSkillID.Text = idx.ToString();
                lSkillName.Text = "<not found>";
                lSkillCost.Text = "";
                lSkillMana.Text = "";
                lSkillLabor.Text = "";
                lSkillCooldown.Text = "";
                lSkillGCD.Text = "";
                rtSkillDescription.Clear();
                skillIcon.Image = null;
                skillIcon.Text = "???";
                lSkillTags.Text = "???";
                tvSkill.Nodes.Clear();
            }
        }

        private GameZone_Groups GetZoneGroupByID(long zoneGroupId)
        {
            if (AADB.DB_Zone_Groups.TryGetValue(zoneGroupId, out var zg))
                return zg;
            else
                return null;
        }

        private void ShowDBZone(long idx)
        {
            bool blank_zone_groups = false;
            bool blank_world_groups = false;
            if (AADB.DB_Zones.TryGetValue(idx, out var zone))
            {
                // From Zones
                lZoneID.Text = zone.id.ToString();
                if (zone.closed)
                    lZoneDisplayName.Text = zone.display_textLocalized + " (closed)";
                else
                    lZoneDisplayName.Text = zone.display_textLocalized;
                lZoneName.Text = zone.name;
                lZoneKey.Text = zone.zone_key.ToString();
                lZoneGroupID.Text = zone.group_id.ToString();
                lZoneFactionID.Text = zone.faction_id.ToString();
                btnFindQuestsInZone.Tag = zone.id;
                btnFindQuestsInZone.Enabled = (zone.id > 0);
                btnFindTransferPathsInZone.Tag = zone.zone_key;

                var zg = GetZoneGroupByID(zone.group_id);
                // From Zone_Groups
                if (zg != null)
                {
                    lZoneGroupsDisplayName.Text = zg.display_textLocalized;
                    lZoneGroupsName.Text = zg.name;
                    string zoneNPCFile = zg.GamePakZoneNPCsDat();
                    if ((pak.IsOpen) && (pak.FileExists(zoneNPCFile)))
                    {
                        btnFindNPCsInZone.Tag = zg.id;
                        btnFindNPCsInZone.Enabled = true;
                    }
                    else
                    {
                        btnFindNPCsInZone.Tag = null;
                        btnFindNPCsInZone.Enabled = false;
                    }

                    string zoneDoodadFile = zg.GamePakZoneDoodadsDat();
                    if ((pak.IsOpen) && (pak.FileExists(zoneDoodadFile)))
                    {
                        btnFindDoodadsInZone.Tag = zg.id;
                        btnFindDoodadsInZone.Enabled = true;
                    }
                    else
                    {
                        btnFindDoodadsInZone.Tag = null;
                        btnFindDoodadsInZone.Enabled = false;
                    }

                    lZoneGroupsSizePos.Text = "X:" + zg.PosAndSize.X.ToString("0.0") + " Y:" +
                                              zg.PosAndSize.Y.ToString("0.0") + "  W:" +
                                              zg.PosAndSize.Width.ToString("0.0") + " H:" +
                                              zg.PosAndSize.Height.ToString("0.0");
                    lZoneGroupsImageMap.Text = zg.image_map.ToString();
                    lZoneGroupsSoundID.Text = zg.sound_id.ToString();
                    lZoneGroupsSoundPackID.Text = zg.sound_pack_id.ToString();
                    lZoneGroupsTargetID.Text = zg.target_id.ToString();
                    lZoneGroupsFactionChatID.Text = zg.faction_chat_region_id.ToString();
                    lZoneGroupsPirateDesperado.Text = zg.pirate_desperado.ToString();
                    lZoneGroupsBuffID.Text = zg.buff_id.ToString();
                    if (zg.fishing_sea_loot_pack_id > 0)
                    {
                        btnZoneGroupsSaltWaterFish.Tag = zg.fishing_sea_loot_pack_id;
                        btnZoneGroupsSaltWaterFish.Enabled = true;
                    }
                    else
                    {
                        btnZoneGroupsSaltWaterFish.Tag = 0;
                        btnZoneGroupsSaltWaterFish.Enabled = false;
                    }

                    if (zg.fishing_land_loot_pack_id > 0)
                    {
                        btnZoneGroupsFreshWaterFish.Tag = zg.fishing_land_loot_pack_id;
                        btnZoneGroupsFreshWaterFish.Enabled = true;
                    }
                    else
                    {
                        btnZoneGroupsFreshWaterFish.Tag = 0;
                        btnZoneGroupsFreshWaterFish.Enabled = false;
                    }

                    var bannedTagsCount = 0;
                    var bannedInfo = string.Empty;
                    foreach (var b in AADB.DB_Zone_Group_Banned_Tags)
                    {
                        if (b.Value.zone_group_id == zg.id)
                        {
                            bannedTagsCount++;
                            if (AADB.DB_Tags.TryGetValue(b.Value.tag_id, out var tag))
                            {
                                bannedInfo += tag.id + " - " + tag.nameLocalized + "\r\n";
                            }
                            else
                            {
                                bannedInfo += tag.id + " - [Unknown Tag]\r\n";
                            }
                        }
                    }

                    if (bannedTagsCount <= 0)
                    {
                        labelZoneGroupRestrictions.Text = "(no restrictions)";
                        labelZoneGroupRestrictions.ForeColor = System.Drawing.Color.DarkGray;
                        labelZoneGroupRestrictions.Tag = 0;
                        mainFormToolTip.ToolTipTitle = "";
                        mainFormToolTip.SetToolTip(labelZoneGroupRestrictions, "");
                    }
                    else
                    {
                        labelZoneGroupRestrictions.Text = "(" + bannedTagsCount.ToString() + " restrictions)";
                        labelZoneGroupRestrictions.ForeColor = System.Drawing.Color.Red;
                        labelZoneGroupRestrictions.Tag = zg.id;
                        mainFormToolTip.ToolTipTitle = "Banned ZoneGroup Tags";
                        mainFormToolTip.SetToolTip(labelZoneGroupRestrictions, bannedInfo);
                    }

                    // From World_Group
                    if (AADB.DB_World_Groups.TryGetValue(zg.target_id, out var wg))
                    {
                        lWorldGroupName.Text = wg.name;
                        lWorldGroupSizeAndPos.Text = "X:" + wg.PosAndSize.X.ToString() + " Y:" +
                                                     wg.PosAndSize.Y.ToString() + "  W:" +
                                                     wg.PosAndSize.Width.ToString() + " H:" +
                                                     wg.PosAndSize.Height.ToString();
                        lWorldGroupImageSizeAndPos.Text = "X:" + wg.Image_PosAndSize.X.ToString() + " Y:" +
                                                          wg.Image_PosAndSize.Y.ToString() + "  W:" +
                                                          wg.Image_PosAndSize.Width.ToString() + " H:" +
                                                          wg.Image_PosAndSize.Height.ToString();
                        lWorldGroupImageMap.Text = wg.image_map.ToString();
                        lWorldGroupTargetID.Text = wg.target_id.ToString();
                    }
                    else
                    {
                        blank_world_groups = true;
                    }

                }
                else
                {
                    blank_zone_groups = true;
                    blank_world_groups = true;
                }

                // Other Info
                var inst = MapViewWorldXML.FindInstanceByZoneKey(zone.zone_key);
                if (inst != null)
                    lZoneInstance.Text = inst.WorldName;
                else
                    lZoneInstance.Text = "<unknown>";

                /*
                // It doesn't seem like there is any zone that exists in multiple instances
                // But I'm keeping the code here, just in case
                var instances = string.Empty;
                foreach (var i in MapViewWorldXML.instances)
                {
                    foreach (var z in i.zones)
                        if (z.Value.zone_key == zone.zone_key)
                        {
                            if (instances != string.Empty)
                                instances += ", ";
                            instances += i.WorldName;
                        }
                }
                if (instances == string.Empty)
                    instances = "<unknown>";
                lZoneInstance.Text = instances;
                */

                ShowSelectedData("zones", "(id = " + idx.ToString() + ")", "id ASC");
            }
            else
            {
                lZoneID.Text = "";
                lZoneDisplayName.Text = "<none>";
                lZoneName.Text = "<none>";
                lZoneKey.Text = "";
                lZoneGroupID.Text = "";
                lZoneFactionID.Text = "";
                btnFindTransferPathsInZone.Tag = 0;
                lZoneInstance.Text = "<none>";

                blank_world_groups = true;
                blank_zone_groups = true;
            }

            if (blank_zone_groups)
            {
                // blank stuff
                lZoneGroupsDisplayName.Text = "<none>";
                lZoneGroupsName.Text = "<none>";
                btnFindNPCsInZone.Tag = null;
                btnFindNPCsInZone.Enabled = false;
                lZoneGroupsSizePos.Text = "";
                lZoneGroupsImageMap.Text = "";
                lZoneGroupsSoundID.Text = "";
                lZoneGroupsSoundPackID.Text = "";
                lZoneGroupsTargetID.Text = "";
                lZoneGroupsFactionChatID.Text = "";
                lZoneGroupsPirateDesperado.Text = "";
                lZoneGroupsBuffID.Text = "";

                btnZoneGroupsSaltWaterFish.Tag = 0;
                btnZoneGroupsSaltWaterFish.Enabled = false;
                btnZoneGroupsFreshWaterFish.Tag = 0;
                btnZoneGroupsFreshWaterFish.Enabled = false;
            }

            if (blank_world_groups)
            {
                lWorldGroupName.Text = "<none>";
                lWorldGroupSizeAndPos.Text = "";
                lWorldGroupImageSizeAndPos.Text = "";
                lWorldGroupImageMap.Text = "";
                lWorldGroupTargetID.Text = "";
            }

        }


        private string VisualizeDropRate(long dropRate, long dropRateMax, bool alwaysDrop, double diceRate = 1.0, double packRate = 1.0)
        {
            if (alwaysDrop)
                return "Always";

            var res = string.Empty;
            var rate = ((double)dropRate / (double)dropRateMax * 100.0);
            var totalRate = rate * diceRate * packRate;

            if (dropRateMax == 0)
                res += totalRate.ToString("F3", CultureInfo.InvariantCulture) + "% (raw:" + dropRate;
            else
                res += totalRate.ToString("F3", CultureInfo.InvariantCulture) + "% (raw:" + dropRate + "/" + dropRateMax;

            if (packRate < 1.0)
                res += " ,pack:" + (packRate * 100.0).ToString("F3", CultureInfo.InvariantCulture) + "%";

            if (diceRate < 1.0)
                res += " ,dice:" + (diceRate * 100.0).ToString("F3", CultureInfo.InvariantCulture) + "%";

            res += ")";
            return res;
        }

        private string CopperToValuta(long copper)
        {
            var res = "";
            if (copper > 10000)
            {
                var gold = copper / 10000;
                res += gold.ToString() + "g ";
                copper -= gold * 10000;
            }
            if (copper > 100)
            {
                var silver = copper / 100;
                res += silver.ToString() + "s ";
                copper -= silver * 100;
            }
            if (copper > 0)
            {
                res += copper.ToString() + "c";
            }
            else
            {
                if (copper < 0)
                {
                    res = copper.ToString();
                }
            }
            return res.Trim();
        }

        private string VisualizeAmounts(long amountMin, long amountMax, long itemId)
        {
            var res = string.Empty;
            if (itemId != 500)
            {
                res = amountMin != amountMax ? amountMin.ToString() + "~" + amountMax : amountMin.ToString();
                return res;
            }
            res = amountMin != amountMax ? CopperToValuta(amountMin) + " ~ " + CopperToValuta(amountMax) : CopperToValuta(amountMin);
            return res;
        }


        private void ShowDBLootByItem(long itemId)
        {
            if (AADB.DB_Loots.Count <= 0)
            {
                MessageBox.Show($"Unable to search for Item {itemId} because no loot data has been loaded");
                return;
            }
            var groupWeights = new Dictionary<long, long>();

            var itemLoots = AADB.DB_Loots.Where(l => l.Value.item_id == itemId);

            dgvLoot.Rows.Clear();
            foreach (var itemLoot in itemLoots)
            {

                var loots = AADB.DB_Loots.Where(l => (l.Value.loot_pack_id == itemLoot.Value.loot_pack_id) && (l.Value.group == itemLoot.Value.group));
                // Get Loot weights

                long dropRateTotal = 0;
                foreach (var loot in loots)
                    dropRateTotal += loot.Value.drop_rate;

                int line = dgvLoot.Rows.Add();
                var row = dgvLoot.Rows[line];

                row.Cells[0].Value = itemLoot.Value.id.ToString();
                row.Cells[1].Value = itemLoot.Value.loot_pack_id.ToString();
                row.Cells[2].Value = itemLoot.Value.item_id.ToString();
                row.Cells[3].Value = AADB.GetTranslationByID(itemLoot.Value.item_id, "items", "name");
                row.Cells[4].Value = VisualizeDropRate(itemLoot.Value.drop_rate, dropRateTotal, itemLoot.Value.always_drop);
                row.Cells[5].Value = VisualizeAmounts(itemLoot.Value.min_amount, itemLoot.Value.max_amount, itemLoot.Value.item_id);
                row.Cells[6].Value = itemLoot.Value.grade_id.ToString();
                row.Cells[7].Value = itemLoot.Value.always_drop.ToString();
                row.Cells[8].Value = itemLoot.Value.group.ToString();
                /*
                row.Cells[5].Value = itemLoot.Value.min_amount.ToString();
                row.Cells[6].Value = itemLoot.Value.max_amount.ToString();
                row.Cells[7].Value = itemLoot.Value.grade_id.ToString();
                row.Cells[8].Value = itemLoot.Value.always_drop.ToString();
                row.Cells[9].Value = itemLoot.Value.group.ToString();
                */
            }


        }

        private void ShowLootGroupData(long loots_pack_id, long loots_group)
        {
            LLootGroupPackID.Text = loots_pack_id.ToString();
            LLootPackGroupNumber.Text = loots_group.ToString();
            if (AADB.DB_Loot_Pack_Dropping_Npc.Any(lp => lp.Value.loot_pack_id == loots_pack_id))
                btnFindLootNpc.Tag = (long)loots_pack_id;
            else
                btnFindLootNpc.Tag = (long)0;
        }

        private void ShowDBLootByID(long loot_id, bool isFirstResult, bool isAltLine, bool isDefaultPack = true, int numberOfNonDefaultPacks = 1)
        {
            if (AADB.DB_Loots.Count <= 0)
            {
                MessageBox.Show($"Unable to search for loot pack {loot_id} because no loot data has been loaded");
                return;
            }

            if (numberOfNonDefaultPacks <= 1)
                isDefaultPack = true;

            // var packRate = isDefaultPack ? 1.0 : 1.0 / (double)numberOfNonDefaultPacks;

            var inGroupWeights = new Dictionary<long, long>();

            var loots = AADB.DB_Loots.Where(l => l.Value.loot_pack_id == loot_id).OrderBy(l => l.Value.group);
            var lootgroups = AADB.DB_Loot_Groups.Where(l => l.Value.pack_id == loot_id).OrderBy(l => l.Value.group_no);

            var withVal = lootgroups.Where(g => g.Value.drop_rate > 1);
            var groupsDropRateTotal = withVal.Sum(g => g.Value.drop_rate);
            //var groupsDropRateTotal = lootgroups.Sum(g => g.Value.drop_rate);
            if (groupsDropRateTotal <= 0)
                groupsDropRateTotal = 0;

            // Get Loot weights
            foreach (var loot in loots)
            {
                long newVal = 0;
                if (inGroupWeights.TryGetValue(loot.Value.group, out var weight))
                {
                    newVal = weight;
                    inGroupWeights.Remove(loot.Value.group);
                }

                newVal += loot.Value.drop_rate;
                inGroupWeights.Add(loot.Value.group, newVal);
            }

            // List results
            if (isFirstResult)
                dgvLoot.Rows.Clear();

            long lastGroup = -1;
            var groupCount = 0;
            foreach (var lootKV in loots)
            {
                var loot = lootKV.Value;
                int line = dgvLoot.Rows.Add();
                var row = dgvLoot.Rows[line];
                if (!inGroupWeights.TryGetValue(loot.group, out var dropRateTotal))
                    dropRateTotal = 1;

                if (dropRateTotal == 0)
                    dropRateTotal = 1;

                if (lastGroup != loot.group)
                {
                    lastGroup = loot.group;
                    groupCount++;
                }

                if (isAltLine == false)
                {
                    if ((groupCount % 2) == 0)
                        row.DefaultCellStyle.BackColor = SystemColors.ControlLight;
                    else
                        row.DefaultCellStyle.BackColor = SystemColors.Window;
                }
                else
                {
                    if ((groupCount % 2) == 0)
                        row.DefaultCellStyle.BackColor = SystemColors.ControlLightLight;
                    else
                        row.DefaultCellStyle.BackColor = SystemColors.ControlDark;
                }

                var diceRate = 1.0;
                var dices = AADB.DB_Loot_ActAbility_Groups.Where(l =>
                    (l.Value.loot_pack_id == loot.loot_pack_id) && (l.Value.loot_group_id == loot.group));
                foreach (var dice in dices)
                {
                    var diceAverage = (dice.Value.min_dice + dice.Value.max_dice) / 2;
                    diceRate = diceAverage / 10000.0;
                }

                var groupsDropRate = lootgroups.FirstOrDefault(g => g.Value.group_no == loot.group).Value?.drop_rate ?? 0;
                var packRate = isDefaultPack || (groupsDropRate <= 1) ? 1.0 : groupsDropRate / groupsDropRateTotal;
                // TODO: Fix me

                row.Cells[0].Value = loot.id.ToString();
                row.Cells[1].Value = loot.loot_pack_id.ToString();
                row.Cells[2].Value = loot.item_id.ToString();
                row.Cells[3].Value = AADB.GetTranslationByID(loot.item_id, "items", "name");
                row.Cells[4].Value = VisualizeDropRate(loot.drop_rate, dropRateTotal, loot.always_drop, diceRate, packRate);
                row.Cells[5].Value = VisualizeAmounts(loot.min_amount, loot.max_amount, loot.item_id);
                // row.Cells[5].Value = loot.max_amount == loot.min_amount ? loot.min_amount.ToString() : loot.min_amount.ToString() + "~" + loot.max_amount.ToString();
                row.Cells[6].Value = loot.grade_id.ToString();
                row.Cells[7].Value = loot.always_drop.ToString();
                row.Cells[8].Value = loot.group.ToString();
                row.Cells[9].Value = groupsDropRate.ToString() + " / " + groupsDropRateTotal.ToString();
            }
        }

        private void ShowDBQuest(long quest_id)
        {
            tvQuestWorkflow.Nodes.Clear();
            if (!AADB.DB_Quest_Contexts.TryGetValue(quest_id, out var q))
            {
                rtQuestText.Text = "";
                btnQuestFindRelatedOnMap.Tag = 0;
                return;
            }

            btnQuestFindRelatedOnMap.Tag = quest_id;
            var rootNode = tvQuestWorkflow.Nodes.Add(q.nameLocalized + " ( " + q.id + " )");
            rootNode.ForeColor = Color.White;

            var contextNode = rootNode.Nodes.Add("Context");
            contextNode.ForeColor = Color.White;
            var contextFieldsList = GetCustomTableValues("quest_contexts", "id", q.id.ToString());
            foreach (var contextFields in contextFieldsList)
            {
                foreach (var contextField in contextFields)
                {
                    if (contextField.Key == "translate") // we can ignore this one
                        continue;
                    else if
                        (contextField.Key ==
                         "category_id") // special hanlding for this one, as we can't use it's general name to autogenrate the name
                    {
                        if (AADB.DB_Quest_Categories.TryGetValue(q.category_id, out var questCat))
                        {
                            contextNode.Nodes.Add("Category: " + questCat.nameLocalized + " ( " + questCat.id + " )");
                        }
                        else
                            AddCustomPropertyNode(contextField.Key, contextField.Value, false, contextNode);
                        //contextNode.Nodes.Add(AddCustomPropertyInfo(contextField.Key, contextField.Value));
                    }
                    else
                        AddCustomPropertyNode(contextField.Key, contextField.Value, cbQuestWorkflowHideEmpty.Checked,
                            contextNode);
                    //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(contextField.Value))
                    //    contextNode.Nodes.Add(AddCustomPropertyInfo(contextField.Key, contextField.Value));
                }
            }

            contextNode.Expand();

            //dgvQuestComponents.Rows.Clear();
            var comps = from c in AADB.DB_Quest_Components
                        where c.Value.quest_context_id == q.id
                        orderby c.Value.component_kind_id
                        select c.Value;

            var questText = "";

            var qTextQuery = from qt in AADB.DB_Quest_Context_Texts
                             where qt.Value.quest_context_id == q.id
                             orderby qt.Value.quest_context_text_kind_id
                             select qt.Value.textLocalized;

            foreach (var t in qTextQuery)
                questText += t + "\r\r";

            foreach (var c in comps)
            {
                // Component Info
                var kindName = "";
                switch (c.component_kind_id)
                {
                    case 1:
                        kindName = "None";
                        break;
                    case 2:
                        kindName = "Start";
                        break;
                    case 3:
                        kindName = "Supply";
                        break;
                    case 4:
                        kindName = "Progress";
                        break;
                    case 5:
                        kindName = "Fail";
                        break;
                    case 6:
                        kindName = "Ready";
                        break;
                    case 7:
                        kindName = "Drop";
                        break;
                    case 8:
                        kindName = "Reward";
                        break;
                }

                kindName += " ( " + c.component_kind_id + " )";

                // Quest Component header
                var componentNode = rootNode.Nodes.Add("Component " + c.id.ToString() + " - " + kindName);
                questText += "|nc;" + kindName + "|r\r\r";

                // Quest description text
                var compTextRaw = from ct in AADB.DB_Quest_Component_Texts
                                  where ct.Value.quest_component_id == c.id
                                  // && ct.Value.quest_component_text_kind_id == c.component_kind_id
                                  select ct.Value;

                if (compTextRaw != null)
                {
                    foreach (var ct in compTextRaw)
                    {
                        var compText = AADB.GetTranslationByID(ct.id, "quest_component_texts", "text", ct.text);
                        questText += compText + "\r\r";
                    }
                }

                componentNode.ForeColor = Color.Yellow;
                var componentInfoNode = componentNode.Nodes.Add("Properties");
                componentInfoNode.ForeColor = Color.Yellow;
                var fieldsList = GetCustomTableValues("quest_components", "id", c.id.ToString());
                foreach (var fields in fieldsList)
                {
                    foreach (var field in fields)
                    {
                        if (field.Key == "quest_context_id") // skip redundant info
                            continue;
                        if (field.Key == "component_kind_id") // skip redundant info
                            continue;
                        //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(field.Value))
                        //    componentInfoNode.Nodes.Add(AddCustomPropertyInfo(field.Key, field.Value));
                        AddCustomPropertyNode(field.Key, field.Value, cbQuestWorkflowHideEmpty.Checked,
                            componentInfoNode);
                    }
                }

                componentInfoNode.Expand();

                // Acts Info
                var acts = from a in AADB.DB_Quest_Acts
                           where a.Value.quest_component_id == c.id
                           select a.Value;

                foreach (var a in acts)
                {
                    var actsNode = componentNode.Nodes.Add("Act " + a.id + " - " + a.act_detail_type + " ( " +
                                                           a.act_detail_id + " )");
                    actsNode.ForeColor = Color.LimeGreen;
                    var actDetailTableName = FunctionTypeToTableName(a.act_detail_type);
                    var actsFieldsList = GetCustomTableValues(actDetailTableName, "id", a.act_detail_id.ToString());
                    foreach (var fields in actsFieldsList)
                    {
                        foreach (var field in fields)
                        {
                            if (field.Key == "quest_act_obj_alias_id")
                            {
                                if (long.TryParse(field.Value, out var aliasId) && (aliasId > 0))
                                {
                                    // no idea why this is the name field instead of text
                                    var objAlias = AADB.GetTranslationByID(aliasId, "quest_act_obj_aliases", "name",
                                        field.Value);
                                    questText += "|ni;QuestActObjAlias(" + field.Value + ")|r \r" + objAlias + "\r\r\r";
                                }
                            }

                            //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(field.Value))
                            //    actsNode.Nodes.Add(AddCustomPropertyInfo(field.Key, field.Value));
                            AddCustomPropertyNode(field.Key, field.Value, cbQuestWorkflowHideEmpty.Checked, actsNode);
                        }
                    }

                    actsNode.Expand();
                }
            }

            rootNode.Expand();
            tvQuestWorkflow.SelectedNode = rootNode;
            FormattedTextToRichtEdit(questText, rtQuestText);
            ShowSelectedData("quest_contexts", "id = " + q.id.ToString(), "id ASC");
        }


        private void AddBuffTag(string name)
        {
            if (name.Trim(' ') == string.Empty)
                return;
            var lastTagID = flpBuff.Controls.Count + 10;
            var L = new Label();
            L.Tag = lastTagID;
            // L.BorderStyle = BorderStyle.FixedSingle;
            L.Text = name;
            L.AutoSize = true;
            // L.BackColor = Color.White;
            if ((lastTagID % 2) == 0)
                L.ForeColor = Color.Gray;
            else
                L.ForeColor = Color.LightGray;
            flpBuff.Controls.Add(L);
            L.Cursor = Cursors.Hand;
            L.Click += new EventHandler(LabelToCopy_Click);
        }

        private void ClearBuffTags()
        {
            for (int i = flpBuff.Controls.Count - 1; i >= 0; i--)
            {
                Label c = (flpBuff.Controls[i] is Label) ? (flpBuff.Controls[i] as Label) : null;
                if ((c is Label) && (c.Tag != null) && ((int)c.Tag > 0))
                {
                    flpBuff.Controls.RemoveAt(i);
                }
            }
        }

        private string TagsAsString(long target_id, Dictionary<long, GameTaggedValues> lookupTable)
        {
            var tags = from t in lookupTable
                       where t.Value.target_id == target_id
                       select t.Value;
            var s = string.Empty;
            foreach (var t in tags)
            {
                if (s.Length > 0)
                    s += " , ";
                if (AADB.DB_Tags.TryGetValue(t.tag_id, out var taglookup))
                    s += taglookup.nameLocalized + " ";
                s += "(" + t.tag_id.ToString() + ")";
            }

            return s;
        }

        private void ShowDBBuff(long buff_id)
        {
            if (!AADB.DB_Buffs.TryGetValue(buff_id, out var b))
            {
                lBuffId.Text = "???";
                lBuffName.Text = "???";
                lBuffDuration.Text = "???";
                buffIcon.Text = "???";
                lBuffTags.Text = "???";
                ClearBuffTags();
                rtBuffDesc.Clear();
                tvBuffTriggers.Nodes.Clear();
                return;
            }

            lBuffId.Text = b.id.ToString();
            lBuffName.Text = b.nameLocalized;
            lBuffDuration.Text = MSToString(b.duration);
            lBuffTags.Text = TagsAsString(buff_id, AADB.DB_Tagged_Buffs);
            IconIDToLabel(b.icon_id, buffIcon);
            FormattedTextToRichtEdit(b.descLocalized, rtBuffDesc);
            ClearBuffTags();
            foreach (var c in b._others)
            {
                AddBuffTag(c.Key + " = " + c.Value);
            }

            lBuffAddGMCommand.Text = "/addbuff " + lBuffId.Text;
            ShowSelectedData("buffs", "id = " + b.id.ToString(), "id ASC");

            ShowDBBuffTriggers(buff_id);
        }

        private void ShowDBBuffTriggers(long buff_id)
        {
            string EventTypeName(long id)
            {
                switch (id)
                {
                    case 1: return "Attack (1)";
                    case 2: return "Attacked (2)";
                    case 3: return "Damage (3)";
                    case 4: return "Damaged (4)";
                    case 5: return "Dispelled (5)";
                    case 6: return "Timeout (6)";
                    case 7: return "DamagedMelee (7)";
                    case 8: return "DamagedRanged (8)";
                    case 9: return "DamagedSpell (9)";
                    case 10: return "DamagedSiege (10)";
                    case 11: return "Landing (11)";
                    case 12: return "Started (12)";
                    case 13: return "RemoveOnMove (13)";
                    case 14: return "ChannelingCancel (14)";
                    case 15: return "RemoveOnDamage (15)";
                    case 16: return "Death (16)";
                    case 17: return "Unmount (17)";
                    case 18: return "Kill (18)";
                    case 19: return "DamagedCollision (19)";
                    case 20: return "Immotality (20)";
                    case 21: return "Time (21)";
                    case 22: return "KillAny (22)";
                    default: return id.ToString();
                }
            }

            tvBuffTriggers.Nodes.Clear();
            var triggers = AADB.DB_BuffTriggers.Values.Where(bt => bt.buff_id == buff_id)
                .GroupBy(bt => bt.event_id, bt => bt).ToDictionary(bt => bt.Key, bt => bt.ToList());

            foreach (var triggerGrouping in triggers)
            {
                var groupingNode = tvBuffTriggers.Nodes.Add(EventTypeName(triggerGrouping.Key));
                groupingNode.ImageIndex = 1;
                groupingNode.SelectedImageIndex = 1;

                foreach (var trigger in triggerGrouping.Value)
                {
                    if (AADB.DB_Effects.TryGetValue(trigger.effect_id, out var effect))
                    {
                        // var triggerNode = new TreeNode($"{trigger.id} - Effect {trigger.effect_id} ({effect.actual_type} {effect.actual_id})");
                        // groupingNode.Nodes.Add(triggerNode);
                        CreateSkillEffectNode(effect.id, groupingNode, cbBuffsHideEmpty.Checked);
                    }
                }
            }

            tvBuffTriggers.ExpandAll();
            if (tvBuffTriggers.Nodes.Count > 0)
                tvBuffTriggers.SelectedNode = tvBuffTriggers.Nodes[0];
        }


        private void TItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnItemSearch_Click(null, null);
            }
        }

        private void DgvItemSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count <= 0)
                return;
            var row = dgvItem.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;
            ShowDBItem(long.Parse(val.ToString()));
        }

        private void CbItemSearchLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItemSearchLanguage.Enabled == false)
                return;
            cbItemSearchLanguage.Enabled = false;
            if (cbItemSearchLanguage.Text != Properties.Settings.Default.DefaultGameLanguage)
            {
                Properties.Settings.Default.DefaultGameLanguage = cbItemSearchLanguage.Text;
                LoadServerDB(false);
            }

            cbItemSearchLanguage.Enabled = true;
        }

        private void BtnOpenServerDB_Click(object sender, EventArgs e)
        {
            LoadServerDB(true);
        }

        private bool LoadServerDB(bool forceDlg)
        {
            string sqlfile = Properties.Settings.Default.DBFileName;

            while (forceDlg || (!File.Exists(sqlfile)))
            {
                forceDlg = false;
                if (openDBDlg.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    sqlfile = openDBDlg.FileName;
                }
            }

            SQLite.SQLiteFileName = sqlfile;

            var i = cbItemSearchLanguage.Items.IndexOf(Properties.Settings.Default.DefaultGameLanguage);
            cbItemSearchLanguage.SelectedIndex = i;
            using (var loading = new LoadingForm())
            {
                loading.Show();
                loading.ShowInfo("Loading: Table names");
                // The table name loading is basically just to check if we can read the DB file
                LoadTableNames();
                Properties.Settings.Default.DBFileName = sqlfile;
                Text = defaultTitle + " - " + sqlfile + " (" + Properties.Settings.Default.DefaultGameLanguage + ")";
                // make sure translations are loaded first, other tables depend on it

                loading.ShowInfo("Loading: Translation");
                LoadTranslations(Properties.Settings.Default.DefaultGameLanguage);
                loading.ShowInfo("Loading: Custom Translations");
                AddCustomTranslations();
                loading.ShowInfo("Loading: Icon info");
                LoadIcons();
                loading.ShowInfo("Loading: Factions");
                LoadFactions();
                loading.ShowInfo("Loading: Plots");
                LoadPlots();
                loading.ShowInfo("Loading: Buffs");
                LoadBuffs();
                loading.ShowInfo("Loading: Transfers");
                LoadTransfers();
                LoadTransferPaths();
                loading.ShowInfo("Loading: Tags");
                LoadTags();
                LoadZoneGroupBannedTags();
                loading.ShowInfo("Loading: Zones");
                LoadZones();
                loading.ShowInfo("Loading: Doodads");
                LoadDoodads();
                loading.ShowInfo("Loading: Items");
                LoadItemCategories();
                LoadItems();
                LoadItemArmors();
                loading.ShowInfo("Loading: Skills");
                LoadSkills();
                LoadSkillReagents();
                LoadSkillProducts();
                loading.ShowInfo("Loading: Models");
                LoadModels();
                loading.ShowInfo("Loading: NPCs");
                LoadNPCs();
                loading.ShowInfo("Loading: Vehicles");
                LoadSlaves();
                loading.ShowInfo("Loading: Quests");
                LoadQuests();
                loading.ShowInfo("Loading: Trades");
                LoadTrades();
                loading.ShowInfo("Loading: Loot");
                LoadLoots();
            }

            return true;
        }

        private void LoadLoots()
        {
            AADB.DB_Loot_Groups.Clear();
            AADB.DB_Loots.Clear();
            AADB.DB_Loot_Pack_Dropping_Npc.Clear();
            AADB.DB_Loot_ActAbility_Groups.Clear();

            if (!allTableNames.Contains("loots"))
                return;

            using (var connection = SQLite.CreateConnection())
            {
                string sql = "SELECT * FROM loot_groups ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootGroup();
                            t.id = GetInt64(reader, "id");
                            t.pack_id = GetInt64(reader, "pack_id");
                            t.group_no = GetInt64(reader, "group_no");
                            t.drop_rate = GetInt64(reader, "drop_rate");
                            t.item_grade_distribution_id = GetInt64(reader, "item_grade_distribution_id");

                            AADB.DB_Loot_Groups.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                sql = "SELECT * FROM loots ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLoot();
                            t.id = GetInt64(reader, "id");
                            t.group = GetInt64(reader, "group");
                            t.drop_rate = GetInt64(reader, "drop_rate");
                            t.grade_id = GetInt64(reader, "grade_id");
                            t.item_id = GetInt64(reader, "item_id");
                            t.loot_pack_id = GetInt64(reader, "loot_pack_id");
                            t.min_amount = GetInt64(reader, "min_amount");
                            t.max_amount = GetInt64(reader, "max_amount");
                            t.always_drop = GetBool(reader, "always_drop");

                            AADB.DB_Loots.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                // NPC Loot References
                sql = "SELECT * FROM loot_pack_dropping_npcs ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootPackDroppingNpc();
                            t.id = GetInt64(reader, "id");
                            t.npc_id = GetInt64(reader, "npc_id");
                            t.loot_pack_id = GetInt64(reader, "loot_pack_id");
                            t.default_pack = GetBool(reader, "default_pack");

                            AADB.DB_Loot_Pack_Dropping_Npc.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                sql = "SELECT * FROM loot_actability_groups ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootActAbilityGroup();
                            t.id = GetInt64(reader, "id");
                            t.loot_pack_id = GetInt64(reader, "loot_pack_id");
                            t.loot_group_id = GetInt64(reader, "loot_group_id");
                            t.max_dice = GetInt64(reader, "max_dice");
                            t.min_dice = GetInt64(reader, "min_dice");

                            AADB.DB_Loot_ActAbility_Groups.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadTrades()
        {
            var sourceZones = new List<long>();

            using (var connection = SQLite.CreateConnection())
            {
                // Tag Names
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Specialities.Clear();

                    command.CommandText = "SELECT * FROM specialties ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameSpecialties t = new GameSpecialties();
                            t.id = GetInt64(reader, "id");
                            t.row_zone_group_id = GetInt64(reader, "row_zone_group_id");
                            t.col_zone_group_id = GetInt64(reader, "col_zone_group_id");
                            t.ratio = GetInt64(reader, "ratio");
                            t.profit = GetInt64(reader, "profit");
                            t.vendor_exist = GetBool(reader, "vendor_exist");

                            AADB.DB_Specialities.Add(t.id, t);

                            if (!sourceZones.Contains(t.row_zone_group_id))
                                sourceZones.Add(t.row_zone_group_id);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }

            lbTradeSource.Sorted = true;
            lbTradeDestination.Sorted = true;
            lbTradeSource.Items.Clear();
            foreach (var z in sourceZones)
            {
                if (AADB.DB_Zone_Groups.TryGetValue(z, out var zone))
                    lbTradeSource.Items.Add(zone);
            }

        }

        private void LoadSlaves()
        {
            using (var connection = SQLite.CreateConnection())
            {
                // Slaves
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slaves.Clear();

                    command.CommandText = "SELECT * FROM slaves ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameSlaves();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.model_id = GetInt64(reader, "model_id");
                            t.mountable = GetBool(reader, "mountable");
                            t.offset_x = GetFloat(reader, "offset_x");
                            t.offset_y = GetFloat(reader, "offset_y");
                            t.offset_z = GetFloat(reader, "offset_z");
                            t.obb_pos_x = GetFloat(reader, "obb_pos_x");
                            t.obb_pos_y = GetFloat(reader, "obb_pos_y");
                            t.obb_pos_z = GetFloat(reader, "obb_pos_z");
                            t.obb_size_x = GetFloat(reader, "obb_size_x");
                            t.obb_size_y = GetFloat(reader, "obb_size_y");
                            t.obb_size_z = GetFloat(reader, "obb_size_z");
                            t.portal_spawn_fx_id = GetInt64(reader, "portal_spawn_fx_id");
                            t.portal_scale = GetFloat(reader, "portal_scale");
                            t.portal_time = GetFloat(reader, "portal_time");
                            t.portal_despawn_fx_id = GetInt64(reader, "portal_despawn_fx_id");
                            t.hp25_doodad_count = GetInt64(reader, "hp25_doodad_count");
                            t.hp50_doodad_count = GetInt64(reader, "hp50_doodad_count");
                            t.hp75_doodad_count = GetInt64(reader, "hp75_doodad_count");
                            t.spawn_x_offset = GetFloat(reader, "spawn_x_offset");
                            t.spawn_y_offset = GetFloat(reader, "spawn_y_offset");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.level = GetInt64(reader, "level");
                            t.cost = GetInt64(reader, "cost");
                            t.slave_kind_id = GetInt64(reader, "slave_kind_id");
                            t.spawn_valid_area_range = GetInt64(reader, "spawn_valid_area_range");
                            t.slave_initial_item_pack_id = GetInt64(reader, "slave_initial_item_pack_id");
                            t.slave_customizing_id = GetInt64(reader, "slave_customizing_id");
                            t.customizable = GetBool(reader, "customizable");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "slaves", "name", t.name);
                            t.searchText = t.name.ToLower() + " " + t.nameLocalized.ToLower();

                            AADB.DB_Slaves.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Slave Bindings
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Bindings.Clear();

                    command.CommandText = "SELECT * FROM slave_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readId = false;
                        var indx = 1L;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlaveBinding();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.slave_id = GetInt64(reader, "slave_id");
                            t.attach_point_id = GetInt64(reader, "attach_point_id");

                            AADB.DB_Slave_Bindings.Add(t.id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Slave Doodad Bindings
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Doodad_Bindings.Clear();

                    command.CommandText = "SELECT * FROM slave_doodad_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readId = false;
                        var indx = 1L;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlaveDoodadBinding();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.attach_point_id = GetInt64(reader, "attach_point_id");
                            t.doodad_id = GetInt64(reader, "doodad_id");
                            t.persist = GetBool(reader, "persist");
                            t.scale = GetFloat(reader, "scale");

                            AADB.DB_Slave_Doodad_Bindings.Add(t.id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LoadModels()
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Models.Clear();

                    command.CommandText = "SELECT * FROM models ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readComment = false;
                        while (reader.Read())
                        {
                            // comment field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readComment = (columnNames.IndexOf("comment") >= 0);
                            }

                            var t = new GameModel();

                            t.id = GetInt64(reader, "id");
                            if (readComment)
                                t.comment = GetString(reader, "comment");
                            t.sub_id = GetInt64(reader, "sub_id");
                            t.sub_type = GetString(reader, "sub_type");
                            t.dying_time = GetFloat(reader, "dying_time");
                            t.sound_material_id = GetInt64(reader, "sound_material_id");
                            t.big = GetBool(reader, "big");
                            t.target_decal_size = GetFloat(reader, "target_decal_size");
                            t.use_target_decal = GetBool(reader, "use_target_decal");
                            t.use_target_silhouette = GetBool(reader, "use_target_silhouette");
                            t.use_target_highlight = GetBool(reader, "use_target_highlight");
                            t.name = GetString(reader, "name");
                            t.camera_distance = GetFloat(reader, "camera_distance");
                            t.show_name_tag = GetBool(reader, "show_name_tag");
                            t.name_tag_offset = GetFloat(reader, "name_tag_offset");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.despawn_doodad_on_collision = GetBool(reader, "despawn_doodad_on_collision");
                            t.play_mount_animation = GetBool(reader, "play_mount_animation");
                            t.selectable = GetBool(reader, "selectable");
                            t.mount_pose_id = GetInt64(reader, "mount_pose_id");
                            t.camera_distance_for_wide_angle = GetFloat(reader, "camera_distance_for_wide_angle");
                            AADB.DB_Models.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }

        }

        private void TItemSearch_TextChanged(object sender, EventArgs e)
        {
            btnItemSearch.Enabled = (cbItemSearch.Text != string.Empty);
        }

        private void BtnFindItemInLoot_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count <= 0)
                return;
            var row = dgvItem.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            ShowDBLootByItem(long.Parse(val.ToString()));
            tcViewer.SelectedTab = tpLoot;
        }

        private void TLootSearch_TextChanged(object sender, EventArgs e)
        {
            btnLootSearch.Enabled = (tLootSearch.Text != string.Empty);
        }

        private void DgvLoot_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoot.SelectedRows.Count <= 0)
                return;
            var row = dgvLoot.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[1].Value;
            var valgroup = row.Cells[8].Value;
            var id = row.Cells[0].Value;
            if (val == null)
                return;
            tLootSearch.Text = val.ToString();

            if ((val != null) && (valgroup != null))
                ShowLootGroupData(long.Parse(val.ToString()), long.Parse(valgroup.ToString()));
            else
                btnFindLootNpc.Tag = (long)0;
            btnFindLootNpc.Enabled = (long)(btnFindLootNpc.Tag) != 0;

            //"SELECT * FROM loots WHERE (loot_pack_id = @tpackid) ORDER BY id ASC";
            if (id != null)
                ShowSelectedData("loots", "(id = " + id.ToString() + ")", "id ASC");
        }

        private void BtnLootSearch_Click(object sender, EventArgs e)
        {
            string searchText = tLootSearch.Text;
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                return;

            ShowDBLootByID(searchID, true, false);
        }

        private void TLootSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLootSearch_Click(null, null);
            }
        }

        private void TSkillSearch_TextChanged(object sender, EventArgs e)
        {
            btnSkillSearch.Enabled = (cbSkillSearch.Text != string.Empty);
        }

        private void BtnSkillSearch_Click(object sender, EventArgs e)
        {
            dgvSkills.Rows.Clear();
            dgvSkillReagents.Rows.Clear();
            dgvSkillProducts.Rows.Clear();
            string searchText = cbSkillSearch.Text;
            if (searchText == string.Empty)
                return;
            string lng = cbItemSearchLanguage.Text;
            string sql = string.Empty;
            string sqlWhere = string.Empty;
            long searchID;
            bool SearchByID = false;
            if (long.TryParse(searchText, out searchID))
                SearchByID = true;
            bool showFirst = true;
            long firstResult = -1;
            string searchTextLower = searchText.ToLower();

            // More Complex syntax with category names
            // SELECT t1.idx, t1.ru, t1.ru_ver, t2.ID, t2.category_id, t3.name, t4.en_us FROM localized_texts as t1 LEFT JOIN items as t2 ON (t1.idx = t2.ID) LEFT JOIN item_categories as t3 ON (t2.category_id = t3.ID) LEFT JOIN localized_texts as t4 ON ((t4.idx = t3.ID) AND (t4.tbl_name = 'item_categories') AND (t4.tbl_column_name = 'name') ) WHERE (t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1.ru LIKE '%Камень%') ORDER BY t1.ru ASC

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvSkills.Visible = false;
            foreach (var skill in AADB.DB_Skills)
            {
                bool addThis = false;
                if (SearchByID)
                {
                    if (skill.Key == searchID)
                    {
                        addThis = true;
                    }
                }
                else if (skill.Value.SearchString.IndexOf(searchTextLower) >= 0)
                    addThis = true;

                if (addThis)
                {
                    int line = dgvSkills.Rows.Add();
                    var row = dgvSkills.Rows[line];
                    long itemIdx = skill.Value.id;
                    if (firstResult < 0)
                        firstResult = itemIdx;
                    row.Cells[0].Value = itemIdx.ToString();
                    row.Cells[1].Value = skill.Value.nameLocalized;
                    row.Cells[2].Value = skill.Value.descriptionLocalized;
                    //row.Cells[3].Value = skill.Value.webDescriptionLocalized;

                    if (showFirst)
                    {
                        showFirst = false;
                        ShowDBSkill(itemIdx);
                    }
                }
            }

            dgvSkills.Visible = true;

            if (dgvSkills.Rows.Count > 0)
                AddToSearchHistory(cbSkillSearch, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        private void TSkillSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSkillSearch_Click(null, null);
            }
        }

        private void DgvSkills_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSkills.SelectedRows.Count <= 0)
                return;
            var row = dgvSkills.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;
            ShowDBSkill(long.Parse(val.ToString()));
        }

        private void AddSkillLine(long skillindex)
        {
            if (AADB.DB_Skills.TryGetValue(skillindex, out var skill))
            {
                int line = dgvSkills.Rows.Add();
                var row = dgvSkills.Rows[line];
                row.Cells[0].Value = skill.id.ToString();
                row.Cells[1].Value = skill.nameLocalized;
                row.Cells[2].Value = skill.descriptionLocalized;

                if (line == 0)
                    ShowDBSkill(skill.id);
            }
        }

        private void ShowDBSkillByItem(long id)
        {
            if (AADB.DB_Items.TryGetValue(id, out var item))
            {
                dgvSkills.Rows.Clear();
                dgvSkillReagents.Rows.Clear();
                dgvSkillProducts.Rows.Clear();
                if (item.use_skill_id > 0)
                    AddSkillLine(item.use_skill_id);

                foreach (var p in AADB.DB_Skill_Reagents)
                {
                    if (p.Value.item_id == id)
                        AddSkillLine(p.Value.skill_id);
                }

                foreach (var p in AADB.DB_Skill_Products)
                {
                    if (p.Value.item_id == id)
                        AddSkillLine(p.Value.skill_id);
                }

                cbSkillSearch.Text = string.Empty;
                tcViewer.SelectedTab = tpSkills;
                //cbSkillSearch.Text = item.use_skill_id.ToString();
                //BtnSkillSearch_Click(null, null);
            }
        }

        private void BtnFindItemSkill_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count <= 0)
                return;
            var row = dgvItem.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            ShowDBSkillByItem(long.Parse(val.ToString()));
            tcViewer.SelectedTab = tpSkills;
        }

        private void BtnFindGameClient_Click(object sender, EventArgs e)
        {
            if (openGamePakFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var loading = new LoadingForm())
                {
                    loading.Show();
                    if (pak.IsOpen)
                    {
                        loading.ShowInfo("Closing: " + pak.GpFilePath);
                        pak.ClosePak();

                        // TODO: HACK to try and free up as many memomry as possible - https://stackoverflow.com/questions/30622145/free-memory-of-byte
                        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                        GC.Collect();
                    }

                    loading.ShowInfo("Opening: " + Path.GetFileName(openGamePakFileDialog.FileName));

                    TryLoadPakKeys(openGamePakFileDialog.FileName);

                    if (pak.OpenPak(openGamePakFileDialog.FileName, true))
                    {
                        Properties.Settings.Default.GamePakFileName = openGamePakFileDialog.FileName;
                        lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                        PrepareWorldXML(true);
                        if (MapViewForm.ThisForm != null)
                        {
                            MapViewForm.ThisForm.Close();
                            MapViewForm.ThisForm = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to load: " + openGamePakFileDialog.FileName);
                    }
                }
            }
        }

        private void ShowSelectedData(string table, string whereStatement, string orderStatement)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + table + " WHERE " + whereStatement + " ORDER BY " +
                                          orderStatement + " LIMIT 1";
                    labelCurrentDataInfo.Text = command.CommandText;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        dgvCurrentData.Rows.Clear();
                        var columnNames = reader.GetColumnNames();

                        while (reader.Read())
                        {
                            long thisID = -1;
                            if (columnNames.IndexOf("id") >= 0)
                                thisID = GetInt64(reader, "id");

                            foreach (var col in columnNames)
                            {
                                int line = dgvCurrentData.Rows.Add();
                                var row = dgvCurrentData.Rows[line];
                                row.Cells[0].Value = col;
                                row.Cells[1].Value = reader.GetValue(col).ToString();
                                ;
                                row.Cells[2].Value = AADB.GetTranslationByID(thisID, table, col, " ");
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void BtnZonesSearch_Click(object sender, EventArgs e)
        {
            string searchText = tZonesSearch.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            dgvZones.Rows.Clear();
            foreach (var t in AADB.DB_Zones)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.zone_key == searchID) || (z.group_id == searchID) ||
                    (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvZones.Rows.Add();
                    var row = dgvZones.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.name;
                    row.Cells[2].Value = z.group_id.ToString();
                    row.Cells[3].Value = z.zone_key.ToString();
                    row.Cells[4].Value = z.display_textLocalized;
                    row.Cells[5].Value = z.closed.ToString();

                    if (first)
                    {
                        first = false;
                        ShowDBZone(z.id);
                    }

                }
            }
        }

        private void TZonesSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearchZones.Enabled = (tZonesSearch.Text != string.Empty);
        }

        private void BtnZonesShowAll_Click(object sender, EventArgs e)
        {
            bool first = true;
            dgvZones.Rows.Clear();
            foreach (var t in AADB.DB_Zones)
            {
                var z = t.Value;
                var line = dgvZones.Rows.Add();
                var row = dgvZones.Rows[line];

                row.Cells[0].Value = z.id.ToString();
                row.Cells[1].Value = z.name;
                row.Cells[2].Value = z.group_id.ToString();
                row.Cells[3].Value = z.zone_key.ToString();
                row.Cells[4].Value = z.display_textLocalized;
                row.Cells[5].Value = z.closed.ToString();

                if (first)
                {
                    first = false;
                    ShowDBZone(z.id);
                }
            }

        }

        private void TZonesSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnZonesSearch_Click(null, null);
            }

        }

        private void DgvZones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvZones.SelectedRows.Count <= 0)
                return;
            var row = dgvZones.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            ShowDBZone(long.Parse(val.ToString()));
        }

        private void BtnZoneGroupsFishLoot_Click(object sender, EventArgs e)
        {
            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    long LootID = (long)b.Tag;

                    if (LootID > 0)
                    {
                        ShowDBLootByID(LootID, true, false);
                        tcViewer.SelectedTab = tpLoot;
                    }
                }
            }
        }

        private void LbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTableNames.SelectedItem == null)
                return;
            var tablename = lbTableNames.SelectedItem.ToString();
            cbSimpleSQL.Text = "SELECT * FROM " + tablename + " LIMIT 0, 50";

            // BtnSimpleSQL_Click(null, null);
        }

        private void BtnSimpleSQL_Click(object sender, EventArgs e)
        {
            if (cbSimpleSQL.Text == string.Empty)
                return;

            try
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = cbSimpleSQL.Text;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;
                            dgvSimple.Rows.Clear();
                            dgvSimple.Columns.Clear();
                            var columnNames = reader.GetColumnNames();

                            foreach (var col in columnNames)
                            {
                                dgvSimple.Columns.Add(col, col);
                            }

                            while (reader.Read())
                            {
                                int line = dgvSimple.Rows.Add();
                                var row = dgvSimple.Rows[line];
                                int c = 0;
                                foreach (var col in columnNames)
                                {
                                    row.Cells[c].Value = reader.GetValue(col).ToString();
                                    c++;
                                }

                                if (((line % 100) == 0) && (line > 0))
                                {
                                    if (MessageBox.Show($"Already added {line} records, continue?", "SQL Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                        break;
                                }
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }
                if (dgvSimple.Rows.Count > 0)
                    AddToSearchHistory(cbSimpleSQL, cbSimpleSQL.Text);
            }
            catch (Exception x)
            {
                Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
                MessageBox.Show(x.Message, "Run MySQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TSimpleSQL_TextChanged(object sender, EventArgs e)
        {
            btnSimpleSQL.Enabled = (cbSimpleSQL.Text != string.Empty);
        }

        private void TSimpleSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSimpleSQL.Enabled))
                BtnSimpleSQL_Click(null, null);
        }

        private void BtnSearchNPC_Click(object sender, EventArgs e)
        {
            string searchText = cbSearchNPC.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvNPCs.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_NPCs)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.model_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvNPCs.Rows.Add();
                    var row = dgvNPCs.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.level.ToString();
                    row.Cells[3].Value = z.npc_kind_id.ToString();
                    row.Cells[4].Value = z.npc_grade_id.ToString();
                    row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                    row.Cells[6].Value = "???";

                    if (c == 0)
                    {
                        ShowDBNPCInfo(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " items, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            if (c > 0)
                AddToSearchHistory(cbSearchNPC, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void TSearchNPC_TextChanged(object sender, EventArgs e)
        {
            btnSearchNPC.Enabled = (cbSearchNPC.Text != string.Empty);
        }

        private void TSearchNPC_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchNPC.Enabled))
                BtnSearchNPC_Click(null, null);
        }

        private void ShowDBNPCInfo(long id)
        {
            if (AADB.DB_NPCs.TryGetValue(id, out var npc))
            {
                lNPCTemplate.Text = npc.id.ToString();
                lNPCTags.Text = TagsAsString(id, AADB.DB_Tagged_NPCs);
                tvNPCInfo.Nodes.Clear();

                if (npc.npc_nickname_id > 0)
                {
                    var nickNode = tvNPCInfo.Nodes.Add("[" + AADB.GetTranslationByID(npc.npc_nickname_id, "npc_nicknames", "name") + "]");
                    nickNode.ForeColor = Color.Yellow;
                }

                if (npc.ai_file_id > 0)
                {
                    if (AADB.DB_AiFiles.TryGetValue(npc.ai_file_id, out var aiFile))
                    {
                        var aiNode = tvNPCInfo.Nodes.Add("AI: " + aiFile.name);
                        aiNode.ForeColor = Color.White;
                        // aiNode.ImageIndex = 3;
                        // aiNode.SelectedImageIndex = aiNode.ImageIndex;
                    }
                    else
                    {
                        tvNPCInfo.Nodes.Add("AI Unknown FileId: " + npc.ai_file_id);
                    }
                }

                #region spawners
                // Spawners
                var spawnersNode = tvNPCInfo.Nodes.Add("Spawners");
                spawnersNode.ImageIndex = 1;
                spawnersNode.SelectedImageIndex = 1;
                var spawners = AADB.DB_Npc_Spawner_Npcs.Values.Where(x => x.member_type == "Npc" && x.member_id == npc.id).ToList();
                foreach (var npcSpawner in spawners)
                {
                    if (AADB.DB_Npc_Spawners.TryGetValue(npcSpawner.npc_spawner_id, out var spawner))
                    {
                        var spawnerNode = spawnersNode.Nodes.Add("ID:" + npcSpawner.npc_spawner_id + (spawner.activation_state ? " (active)" : ""));
                        spawnerNode.Nodes.Add($"Category: {spawner.npc_spawner_category_id}");
                        if (!string.IsNullOrWhiteSpace(spawner.name))
                            spawnerNode.Nodes.Add($"Name: {spawner.name}");
                        if (!string.IsNullOrWhiteSpace(spawner.comment))
                            spawnerNode.Nodes.Add($"Comment: {spawner.comment}");

                        spawnerNode.Nodes.Add($"Activation State: {spawner.activation_state}");
                        spawnerNode.Nodes.Add($"Save Indun: {spawner.save_indun}");

                        if (spawner.spawn_delay_min == spawner.spawn_delay_max)
                        {
                            spawnerNode.Nodes.Add($"Spawn Delay: {MSToString((long)spawner.spawn_delay_min * 1000, true)}");
                        }
                        else
                        {
                            spawnerNode.Nodes.Add($"Spawn Delay Min: {MSToString((long)spawner.spawn_delay_min * 1000, true)}");
                            spawnerNode.Nodes.Add($"Spawn Delay Max: {MSToString((long)spawner.spawn_delay_max * 1000, true)}");
                        }

                        if (spawner.min_population != 0)
                            spawnerNode.Nodes.Add($"Min Population: {spawner.min_population}");
                        if (spawner.maxPopulation != 1)
                            spawnerNode.Nodes.Add($"Max Population: {spawner.maxPopulation}");
                        if (spawner.startTime > 0)
                            spawnerNode.Nodes.Add($"Start Time: {spawner.startTime}");
                        if (spawner.endTime > 0)
                            spawnerNode.Nodes.Add($"End Time: {spawner.endTime}");
                        if (spawner.test_radius_npc > 0)
                            spawnerNode.Nodes.Add($"Test Radius NPC: {spawner.test_radius_npc}");
                        if (spawner.test_radius_pc > 0)
                            spawnerNode.Nodes.Add($"Test Radius PC: {spawner.test_radius_pc}");
                        if (spawner.suspend_spawn_count > 0)
                            spawnerNode.Nodes.Add($"Suspend Spawn Count: {spawner.suspend_spawn_count}");
                    }
                    else
                    {
                        spawnersNode.Nodes.Add("NOT found!:" + npcSpawner.npc_spawner_id);
                    }
                    spawnersNode.Expand();
                }
                #endregion

                #region interactions
                var interactionNode = tvNPCInfo.Nodes.Add("Interaction");
                interactionNode.ImageIndex = 2;
                interactionNode.SelectedImageIndex = 2;
                if (npc.npc_interaction_set_id > 0)
                {
                    var interactions = AADB.DB_NpcInteractions.Values.Where(x => x.npc_interaction_set_id == npc.npc_interaction_set_id).ToList();
                    foreach (var interaction in interactions)
                    {
                        AddCustomPropertyNode("skill_id", interaction.skill_id.ToString(), false, interactionNode);
                    }
                }
                if (npc.auctioneer)
                    interactionNode.Nodes.Add("Auction");
                if (npc.banker)
                    interactionNode.Nodes.Add("Warehouse");
                if (npc.blacksmith)
                    interactionNode.Nodes.Add("Blacksmith");
                if (npc.expedition)
                    interactionNode.Nodes.Add("Guild Manager");
                if (npc.merchant)
                    interactionNode.Nodes.Add("Merchant");
                if (npc.priest)
                    interactionNode.Nodes.Add("Priest");
                if (npc.repairman)
                    interactionNode.Nodes.Add("Repairs");
                if (npc.skill_trainer)
                    interactionNode.Nodes.Add("Skillmanager");
                if (npc.specialty)
                    interactionNode.Nodes.Add("Speciality");
                if (npc.stabler)
                    interactionNode.Nodes.Add("Stablemaster");
                if (npc.teleporter)
                    interactionNode.Nodes.Add("Teleporter");
                if (npc.trader)
                    interactionNode.Nodes.Add("Trader");

                if (interactionNode.Nodes.Count > 0)
                    interactionNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(interactionNode);

                #endregion

                #region skills
                // Base Skill
                if (npc.base_skill_id > 0)
                {
                    var baseSkillNode = tvNPCInfo.Nodes.Add("Base Skill");
                    baseSkillNode.ImageIndex = 2;
                    baseSkillNode.SelectedImageIndex = 2;
                    AddCustomPropertyNode("skill_id", npc.base_skill_id.ToString(), false, baseSkillNode);
                    AddCustomPropertyNode("base_skill_delay", npc.base_skill_delay.ToString(), true, baseSkillNode);
                    AddCustomPropertyNode("base_skill_strafe", npc.base_skill_strafe.ToString(), false, baseSkillNode);
                    baseSkillNode.Expand();
                }

                // NP Skills
                var npSkillsNode = tvNPCInfo.Nodes.Add("NP Skills");
                npSkillsNode.ImageIndex = 2;
                npSkillsNode.SelectedImageIndex = 2;
                var npSkills = AADB.DB_NpSkills.Values.Where(x => x.owner_id == npc.id && x.owner_type == "Npc").ToList();
                foreach (var npSkill in npSkills)
                {
                    var npSkillNode = AddCustomPropertyNode("skill_id", npSkill.skill_id.ToString(), false, npSkillsNode);
                    npSkillNode.Text += $", {npSkill.skill_use_condition_id}( {npSkill.skill_use_param1:F1} | {npSkill.skill_use_param2:F1} )";
                }
                if (npSkillsNode.Nodes.Count > 0)
                    npSkillsNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(npSkillsNode);
                #endregion

                #region initial_buffs
                var initialBuffsNode = tvNPCInfo.Nodes.Add("Initial Buffs");
                initialBuffsNode.ImageIndex = 2;
                initialBuffsNode.SelectedImageIndex = 2;
                var initialBuffs = AADB.DB_NpcInitialBuffs.Values.Where(x => x.npc_id == npc.id).ToList();
                foreach (var initialBuff in initialBuffs)
                {
                    AddCustomPropertyNode("buff_id", initialBuff.buff_id.ToString(), false, initialBuffsNode);
                }
                if (initialBuffsNode.Nodes.Count > 0)
                    initialBuffsNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(initialBuffsNode);


                #endregion

                ShowSelectedData("npcs", "(id = " + id.ToString() + ")", "id ASC");
                btnShowNPCsOnMap.Tag = npc.id;
                btnShowNpcLoot.Tag = npc.id;
                btnShowNpcLoot.Enabled = AADB.DB_Loot_Pack_Dropping_Npc.Any(pl => pl.Value.npc_id == npc.id);
            }
            else
            {
                lNPCTemplate.Text = "???";
                lNPCTags.Text = "???";
                tvNPCInfo.Nodes.Clear();
                btnShowNPCsOnMap.Tag = 0;
                btnShowNpcLoot.Tag = 0;
                btnShowNpcLoot.Enabled = false;
            }

            lGMNPCSpawn.Text = "/spawn npc " + lNPCTemplate.Text;
        }

        private void DgvNPCs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNPCs.SelectedRows.Count <= 0)
                return;
            var row = dgvNPCs.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;


            long id = -1;
            if (row.Cells[0].Value != null)
                if (long.TryParse(row.Cells[0].Value.ToString(), out var i))
                    id = i;
            if (id > 0)
                ShowDBNPCInfo(id);
        }

        private void TSearchFaction_TextChanged(object sender, EventArgs e)
        {
            btnSearchFaction.Enabled = (tSearchFaction.Text != string.Empty);
        }

        private void BtnSearchFaction_Click(object sender, EventArgs e)
        {
            string searchText = tSearchFaction.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvFactions.Rows.Clear();
            foreach (var t in AADB.DB_GameSystem_Factions)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.owner_id == searchID) || (z.mother_id == searchID) ||
                    (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvFactions.Rows.Add();
                    var row = dgvFactions.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    if (z.mother_id != 0)
                    {
                        row.Cells[2].Value = AADB.GetFactionName(z.mother_id, true);
                    }
                    else
                    {
                        row.Cells[2].Value = string.Empty;
                    }

                    if (z.owner_nameLocalized != string.Empty)
                        row.Cells[3].Value = z.owner_nameLocalized + " (" + z.owner_id.ToString() + ")";
                    else
                        row.Cells[3].Value = z.owner_id.ToString();
                    string d = string.Empty;
                    if (z.is_diplomacy_tgt)
                        d += "Is Target ";
                    if (z.diplomacy_link_id != 0)
                    {
                        d += AADB.GetFactionName(z.diplomacy_link_id, true);
                    }

                    row.Cells[4].Value = d;

                    if (first)
                    {
                        first = false;
                        ShowDBFaction(z.id);
                    }
                }

            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void TSearchFaction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchFaction_Click(null, null);
        }

        private void BtnFactionsAll_Click(object sender, EventArgs e)
        {
            bool first = true;
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvFactions.Rows.Clear();
            foreach (var t in AADB.DB_GameSystem_Factions)
            {
                var z = t.Value;
                var line = dgvFactions.Rows.Add();
                var row = dgvFactions.Rows[line];

                row.Cells[0].Value = z.id.ToString();
                row.Cells[1].Value = z.nameLocalized;
                if (z.mother_id != 0)
                {
                    row.Cells[2].Value = AADB.GetFactionName(z.mother_id, true);
                }
                else
                {
                    row.Cells[2].Value = string.Empty;
                }

                if (z.owner_nameLocalized != string.Empty)
                    row.Cells[3].Value = z.owner_nameLocalized + " (" + z.owner_id.ToString() + ")";
                else
                    row.Cells[3].Value = z.owner_id.ToString();
                string d = string.Empty;
                if (z.is_diplomacy_tgt)
                    d += "Is Target ";
                if (z.diplomacy_link_id != 0)
                {
                    d += AADB.GetFactionName(z.diplomacy_link_id, true);
                }

                row.Cells[4].Value = d;

                if (first)
                {
                    first = false;
                    ShowDBFaction(z.id);
                }
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void DgvFactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFactions.SelectedRows.Count <= 0)
                return;
            var row = dgvFactions.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDBFaction(long.Parse(id.ToString()));
                ShowSelectedData("system_factions", "(id = " + id.ToString() + ")", "id ASC");
            }
        }

        private void ShowDBFaction(long id)
        {
            if (AADB.DB_GameSystem_Factions.TryGetValue(id, out var f))
            {
                lFactionID.Text = f.id.ToString();
                lFactionName.Text = f.nameLocalized;
                lFactionOwnerName.Text = f.owner_nameLocalized;
                lFactionOwnerTypeID.Text = f.owner_type_id.ToString();
                lFactionOwnerID.Text = f.owner_id.ToString();
                LFactionPoliticalSystemID.Text = f.political_system_id.ToString();
                lFactionMotherID.Text = f.mother_id.ToString();
                lFactionAggroLink.Text = f.aggro_link.ToString();
                lFactionGuardLink.Text = f.guard_help.ToString();
                lFactionIsDiplomacyTarget.Text = f.is_diplomacy_tgt.ToString();
                lFactionDiplomacyLinkID.Text = f.diplomacy_link_id.ToString();

                AADB.SetFactionRelationLabel(f, 148, ref lFactionHostileNuia);
                AADB.SetFactionRelationLabel(f, 149, ref lFactionHostileHaranya);
                AADB.SetFactionRelationLabel(f, 114, ref lFactionHostilePirate);
            }
            else
            {
                lFactionID.Text = "0";
                lFactionName.Text = "<none>";
                lFactionOwnerName.Text = "";
                lFactionOwnerTypeID.Text = "";
                lFactionOwnerID.Text = "";
                LFactionPoliticalSystemID.Text = "";
                lFactionMotherID.Text = "";
                lFactionAggroLink.Text = "";
                lFactionGuardLink.Text = "";
                lFactionIsDiplomacyTarget.Text = "";
                lFactionDiplomacyLinkID.Text = "";
                lFactionHostileNuia.Text = "";
                lFactionHostileHaranya.Text = "";
            }

        }

        private void TSearchDoodads_TextChanged(object sender, EventArgs e)
        {
            btnSearchDoodads.Enabled = (cbSearchDoodads.Text != string.Empty);
        }

        private void TSearchDoodads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchDoodads_Click(null, null);
        }

        private void BtnSearchDoodads_Click(object sender, EventArgs e)
        {
            string searchText = cbSearchDoodads.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvDoodads.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Doodad_Almighties)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.group_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvDoodads.Rows.Add();
                    var row = dgvDoodads.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.mgmt_spawn.ToString();
                    if (AADB.DB_Doodad_Groups.TryGetValue(z.group_id, out var dGroup))
                    {
                        row.Cells[3].Value = dGroup.nameLocalized + " (" + z.group_id.ToString() + ")";
                    }
                    else
                    {
                        row.Cells[3].Value = z.group_id.ToString();
                    }

                    row.Cells[4].Value = z.percent.ToString();
                    if (z.faction_id != 0)
                        row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                    else
                        row.Cells[5].Value = string.Empty;
                    row.Cells[6].Value = z.model_kind_id.ToString();

                    if (first)
                    {
                        first = false;
                        ShowDBDoodad(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " doodads, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            if (c > 0)
                AddToSearchHistory(cbSearchDoodads, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void DgvDoodads_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoodads.SelectedRows.Count <= 0)
                return;
            var row = dgvDoodads.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDBDoodad(long.Parse(id.ToString()));
                ShowSelectedData("doodad_almighties", "(id = " + id.ToString() + ")", "id ASC");
            }

        }

        private string FunctionTypeToTableName(string funcName)
        {
            if (funcName == "DoodadFuncNaviOpenMailbox")
                return "doodad_func_navi_open_mailboxes";
            var tableName = string.Empty;
            var rest = funcName;
            while (rest.Length > 0)
            {
                var c = rest.Substring(0, 1);
                if (c != c.ToLower()) // decaptialize the name, and put a underscore in front
                {
                    tableName += "_" + c.ToLower();
                }
                else
                {
                    tableName += c;
                }

                rest = rest.Substring(1);
            }

            tableName = tableName.Trim('_') + "s"; // remove excess underscores and add a "s"
            return tableName;
        }

        private List<Dictionary<string, string>> GetCustomTableValues(string tableName, string indexName,
            string searchId)
        {
            var res = new List<Dictionary<string, string>>();

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = "SELECT * FROM " + tableName + " WHERE " + indexName + "=" + searchId;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var resRow = new Dictionary<string, string>();
                                int c = 0;
                                foreach (var col in columnNames)
                                {
                                    if (col != indexName) // skip the index field
                                    {
                                        if (reader.IsDBNull(col))
                                            resRow.Add(col, "<null>");
                                        else
                                            resRow.Add(col, reader.GetValue(col).ToString());
                                    }

                                    c++;
                                }

                                res.Add(resRow);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var eRow = new Dictionary<string, string>();
                        eRow.Add("Exception", ex.Message);
                        res.Add(eRow);
                    }
                }
            }

            return res;
        }


        private bool IsCustomPropertyEmpty(string value)
        {
            return (string.IsNullOrWhiteSpace(value) || (value == "0") || (value == "<null>") || (value == "f"));
        }

        private TreeNode AddCustomPropertyNode(string key, string value, bool hideNull, TreeNode rootNode)
        {
            if (hideNull && IsCustomPropertyEmpty(value))
                return null;

            // First try to detect localizable entries
            var rootSplit = rootNode.Text.Split(' ');
            var rootTypeName = string.Empty;
            long rootTypeKey = 0;
            if ((rootSplit.Length == 4) && (rootSplit[1] == "(") && (rootSplit[3] == ")")) // name ( id )
            {
                rootTypeName = FunctionTypeToTableName(rootSplit[0]);
                if (long.TryParse(rootSplit[2], out var v))
                    rootTypeKey = v;
            }

            var localizedValue = value;
            if (!string.IsNullOrWhiteSpace(rootTypeName) && (rootTypeKey > 0))
            {
                localizedValue = AADB.GetTranslationByID(rootTypeKey, rootTypeName, key, "");
            }

            if (!string.IsNullOrWhiteSpace(localizedValue) && (localizedValue != value))
            {
                var localizedNode = new TreeNodeWithInfo();
                localizedNode.targetTabPage = tbLocalizer;
                localizedNode.targetTextBox = tSearchLocalized;
                localizedNode.targetSearchText = rootTypeName + " " + key + " =" + rootTypeKey + "=";
                localizedNode.targetSearchButton = null;
                localizedNode.Text = key + ": " + localizedValue;
                localizedNode.ForeColor = Color.LightYellow;
                rootNode.Nodes.Add(localizedNode);
                return localizedNode;
            }

            var nodeText = key + ": " + value;
            if (!long.TryParse(value, out var val))
            {
                var normalNode = rootNode.Nodes.Add(nodeText);
                return normalNode;
            }

            var res = new TreeNodeWithInfo();
            var setCustomIcon = -1;

            if (key.EndsWith("delay") || key.EndsWith("_time") || key.EndsWith("duration"))
            {
                if (long.TryParse(value, out var delayVal))
                {
                    nodeText += " - " + MSToString(delayVal);
                    res.ForeColor = Color.LightSeaGreen;
                }
            }
            else if (key.EndsWith("next_phase") && (AADB.DB_Doodad_Func_Groups.TryGetValue(val, out var nextPhase)))
            {
                var s = string.IsNullOrWhiteSpace(nextPhase.nameLocalized) ? nextPhase.name : nextPhase.nameLocalized;
                if (!string.IsNullOrEmpty(s))
                    nodeText += " - " + s;
                res.ForeColor = Color.WhiteSmoke;
            }
            else if (key.EndsWith("skill_id") && (AADB.DB_Skills.TryGetValue(val, out var skill)))
            {
                res.targetTabPage = tpSkills;
                res.targetSearchBox = cbSkillSearch;
                // res.targetSearchText = skill.nameLocalized;
                res.targetSearchText = skill.id.ToString();
                res.targetSearchButton = btnSkillSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + skill.nameLocalized;
                setCustomIcon = IconIDToLabel(skill.icon_id, null);
            }
            else if (key.EndsWith("item_id") && (AADB.DB_Items.TryGetValue(val, out var item)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                // res.targetSearchText = item.nameLocalized;
                res.targetSearchText = item.id.ToString();
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + item.nameLocalized;
                setCustomIcon = IconIDToLabel(item.icon_id, null);
            }
            else if (key.EndsWith("doodad_id") && (AADB.DB_Doodad_Almighties.TryGetValue(val, out var doodad)))
            {
                res.targetTabPage = tpDoodads;
                res.targetSearchBox = cbSearchDoodads;
                // res.targetSearchText = doodad.nameLocalized;
                res.targetSearchText = doodad.id.ToString();
                res.targetSearchButton = btnSearchDoodads;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + doodad.nameLocalized;
            }
            else if (key.EndsWith("npc_id") && (AADB.DB_NPCs.TryGetValue(val, out var npc)))
            {
                res.targetTabPage = tpNPCs;
                res.targetSearchBox = cbSearchNPC;
                // res.targetSearchText = npc.nameLocalized;
                res.targetSearchText = npc.id.ToString();
                res.targetSearchButton = btnSearchNPC;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + npc.nameLocalized;
            }
            else if (key.EndsWith("slave_id") && (AADB.DB_Slaves.TryGetValue(val, out var slave)))
            {
                res.targetTabPage = tpNPCs;
                // res.targetSearchBox = slave.id.ToString();
                res.targetSearchText = slave.id.ToString();
                res.targetSearchButton = btnSearchSlave;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + slave.nameLocalized;
            }
            else if (key.EndsWith("buff_id") && (AADB.DB_Buffs.TryGetValue(val, out var buff)))
            {
                res.targetTabPage = tpBuffs;
                res.targetSearchBox = cbSearchBuffs;
                // res.targetSearchText = buff.nameLocalized;
                res.targetSearchText = buff.id.ToString();
                res.targetSearchButton = btnSearchBuffs;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + buff.nameLocalized;
                setCustomIcon = IconIDToLabel(buff.icon_id, null);
            }
            else if (key.EndsWith("zone_id"))
            {
                if (AADB.DB_Zones.TryGetValue(val, out var zone))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zone.display_textLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    nodeText += " - " + zone.display_textLocalized + " (key)";
                }
                // Some quest related entries use "zone_id" when they instead mean "zone_group_id"
                // So we add the 2nd part here.
                if (AADB.DB_Zone_Groups.TryGetValue(val, out var zoneGroup))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zoneGroup.display_textLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    if (zone != null)
                        nodeText += " or ";
                    else
                        nodeText += " - ";
                    nodeText += zoneGroup.display_textLocalized + " (group)";
                }
            }
            else if (key.EndsWith("zone_group_id") && (AADB.DB_Zone_Groups.TryGetValue(val, out var zoneGroup)))
            {
                res.targetTabPage = tpZones;
                res.targetTextBox = tZonesSearch;
                res.targetSearchText = zoneGroup.display_textLocalized;
                res.targetSearchButton = btnSearchZones;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + zoneGroup.display_textLocalized;
            }
            else if (key.EndsWith("item_category_id") &&
                     (AADB.DB_ItemsCategories.TryGetValue(val, out var itemCategory)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                res.targetSearchText = itemCategory.nameLocalized;
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + itemCategory.nameLocalized;
            }
            else if (key.EndsWith("tag_id") && (AADB.DB_Tags.TryGetValue(val, out var tagId)))
            {
                res.targetTabPage = tpTags;
                res.targetTextBox = tSearchTags;
                // res.targetSearchText = tagId.nameLocalized;
                res.targetSearchText = tagId.id.ToString();
                res.targetSearchButton = btnSearchTags;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + tagId.nameLocalized;
            }
            else if (key.EndsWith("wi_id"))
            {
                try
                {
                    res.ForeColor = Color.LawnGreen;
                    nodeText += " - " + ((WorldInteractionType)Enum.Parse(typeof(WorldInteractionType), value)).ToString();
                }
                catch
                {
                    nodeText += " - ???";
                    res.ForeColor = Color.Red;
                }
            }
            else if (key.EndsWith("special_effect_type_id"))
            {
                try
                {
                    nodeText += " - " + ((SpecialType)Enum.Parse(typeof(SpecialType), value)).ToString();
                    res.ForeColor = Color.LawnGreen;
                }
                catch
                {
                    nodeText += " - ???";
                    res.ForeColor = Color.Red;
                }
            }
            else if (key.EndsWith("loot_pack_id"))
            {
                var lootNode = new TreeNodeWithInfo();
                lootNode.Text = key + ": " + val;
                lootNode.ForeColor = Color.Yellow;

                var loots = AADB.DB_Loots.Where(l => l.Value.loot_pack_id == val).OrderBy(l => l.Value.group);
                foreach (var loot in loots)
                {
                    var itemNode = new TreeNodeWithInfo();
                    itemNode.Text = loot.Value.item_id.ToString();
                    if (AADB.DB_Items.TryGetValue(loot.Value.item_id, out var lItem))
                    {
                        itemNode.targetTabPage = tpItems;
                        itemNode.targetSearchBox = cbItemSearch;
                        itemNode.targetSearchText = lItem.nameLocalized;
                        itemNode.targetSearchButton = btnItemSearch;
                        itemNode.ForeColor = Color.WhiteSmoke;
                        itemNode.Text += " - " + lItem.nameLocalized;
                        itemNode.SelectedImageIndex = itemNode.ImageIndex = IconIDToLabel(lItem.icon_id, null);
                    }
                    else
                    {
                        itemNode.Text = " - ???";
                        itemNode.ForeColor = Color.Red;
                    }

                    lootNode.Nodes.Add(itemNode);
                }

                rootNode.Nodes.Add(lootNode);
                return lootNode;
            }

            res.Text = nodeText;

            rootNode.Nodes.Add(res);
            if ((rootNode?.TreeView.ImageList != null) && (setCustomIcon >= 0))
            {
                res.ImageIndex = setCustomIcon;
                res.SelectedImageIndex = setCustomIcon;
            }

            return res;
        }

        private void ShowDBDoodad(long id)
        {
            if (AADB.DB_Doodad_Almighties.TryGetValue(id, out var doodad))
            {
                lDoodadID.Text = doodad.id.ToString();
                lDoodadName.Text = doodad.nameLocalized;
                lDoodadModel.Text = doodad.model;
                lDoodadOnceOneMan.Text = doodad.once_one_man.ToString();
                lDoodadOnceOneInteraction.Text = doodad.once_one_interaction.ToString();
                lDoodadShowName.Text = doodad.show_name.ToString();
                lDoodadMgmtSpawn.Text = doodad.mgmt_spawn.ToString();
                lDoodadPercent.Text = doodad.percent.ToString();
                lDoodadMinTime.Text = MSToString(doodad.min_time);
                lDoodadMaxTime.Text = MSToString(doodad.max_time);
                lDoodadModelKindID.Text = doodad.model_kind_id.ToString();
                lDoodadUseCreatorFaction.Text = doodad.use_creator_faction.ToString();
                lDoodadForceToDTopPriority.Text = doodad.force_tod_top_priority.ToString();
                lDoodadMilestoneID.Text = doodad.milestone_id.ToString();
                lDoodadGroupID.Text = doodad.group_id.ToString();
                lDoodadShowName.Text = doodad.show_minimap.ToString();
                lDoodadUseTargetDecal.Text = doodad.use_target_decal.ToString();
                lDoodadUseTargetSilhouette.Text = doodad.use_target_silhouette.ToString();
                lDoodadUseTargetHighlight.Text = doodad.use_target_highlight.ToString();
                lDoodadTargetDecalSize.Text = doodad.target_decal_size.ToString();
                lDoodadSimRadius.Text = RangeToString(doodad.sim_radius);
                lDoodadCollideShip.Text = doodad.collide_ship.ToString();
                lDoodadCollideVehicle.Text = doodad.collide_vehicle.ToString();
                lDoodadClimateID.Text = doodad.climate_id.ToString();
                lDoodadSaveIndun.Text = doodad.save_indun.ToString();
                lDoodadMarkModel.Text = doodad.mark_model.ToString();
                lDoodadForceUpAction.Text = doodad.force_up_action.ToString();
                lDoodadLoadModelFromWorld.Text = doodad.load_model_from_world.ToString();
                lDoodadParentable.Text = doodad.parentable.ToString();
                lDoodadChildable.Text = doodad.childable.ToString();
                lDoodadFactionID.Text = AADB.GetFactionName(doodad.faction_id, true);
                lDoodadGrowthTime.Text = MSToString(doodad.growth_time);
                lDoodadDespawnOnCollision.Text = doodad.despawn_on_collision.ToString();
                lDoodadNoCollision.Text = doodad.no_collision.ToString();
                lDoodadRestrictZoneID.Text = doodad.restrict_zone_id.ToString();
                btnShowDoodadOnMap.Tag = doodad.id;

                if (AADB.DB_Doodad_Groups.TryGetValue(doodad.group_id, out var dGroup))
                {
                    lDoodadGroupName.Text = dGroup.nameLocalized + " (" + doodad.group_id.ToString() + ")";
                    lDoodadGroupIsExport.Text = dGroup.is_export.ToString();
                    lDoodadGroupGuardOnFieldTime.Text = SecondsToString(dGroup.guard_on_field_time);
                    lDoodadGroupRemovedByHouse.Text = dGroup.removed_by_house.ToString();
                }
                else
                {
                    lDoodadGroupName.Text = "<none>";
                    lDoodadGroupIsExport.Text = "";
                    lDoodadGroupGuardOnFieldTime.Text = "";
                    lDoodadGroupRemovedByHouse.Text = "";
                }

                bool firstFuncGroup = true;
                dgvDoodadFuncGroups.Rows.Clear();
                foreach (var f in AADB.DB_Doodad_Func_Groups)
                {
                    var dFuncGroup = f.Value;
                    if (dFuncGroup.doodad_almighty_id == doodad.id)
                    {
                        GameDoodadPhaseFunc dPhaseFunc = null;
                        foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                            if (dpf.Value.doodad_func_group_id == dFuncGroup.id)
                            {
                                dPhaseFunc = dpf.Value;
                                break;
                            }


                        var line = dgvDoodadFuncGroups.Rows.Add();
                        var row = dgvDoodadFuncGroups.Rows[line];

                        row.Cells[0].Value = dFuncGroup.id.ToString();
                        row.Cells[1].Value = dFuncGroup.doodad_func_group_kind_id.ToString();
                        row.Cells[2].Value = dPhaseFunc?.actual_func_id.ToString() ?? "-";
                        row.Cells[3].Value = dPhaseFunc?.actual_func_type ?? "none";

                        if (firstFuncGroup)
                        {
                            firstFuncGroup = false;
                            ShowDBDoodadFuncGroup(dFuncGroup.id);
                        }
                    }
                }

                // Details Tab
                tvDoodadDetails.Nodes.Clear();
                var rootNode = tvDoodadDetails.Nodes.Add(doodad.nameLocalized + " ( " + doodad.id + " )");
                rootNode.ForeColor = Color.White;
                foreach (var f in AADB.DB_Doodad_Func_Groups)
                {
                    var dFuncGroup = f.Value;
                    if (dFuncGroup.doodad_almighty_id == doodad.id)
                    {
                        var doodadGroupName = "Group: " + dFuncGroup.id.ToString() + " - Kind: " + dFuncGroup.doodad_func_group_kind_id.ToString() + " - " + dFuncGroup.nameLocalized;
                        var groupNode = rootNode.Nodes.Add(doodadGroupName);
                        groupNode.ForeColor = Color.LightCyan;

                        // Phase Funcs
                        foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                            if (dpf.Value.doodad_func_group_id == dFuncGroup.id)
                            {
                                var phaseNode = groupNode.Nodes.Add("PhaseFuncs: " + dpf.Value.actual_func_type +
                                                                    " ( " +
                                                                    dpf.Value.actual_func_id + " )");
                                phaseNode.ForeColor = Color.Yellow;
                                var tableName = FunctionTypeToTableName(dpf.Value.actual_func_type);
                                var fieldsList = GetCustomTableValues(tableName, "id",
                                    dpf.Value.actual_func_id.ToString());
                                foreach (var fields in fieldsList)
                                {
                                    foreach (var fl in fields)
                                    {
                                        AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked,
                                            phaseNode);
                                        /*
                                        if (cbDoodadWorkflowHideEmpty.Checked && (string.IsNullOrWhiteSpace(fl.Value) || (fl.Value == "0") || (fl.Value == "<null>") || (fl.Value == "f")))
                                        {
                                            // ignore empty values
                                        }
                                        else
                                        {
                                            phaseNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                        }
                                        */
                                    }
                                }

                                phaseNode.Collapse();
                            }

                        // doodad_funcs
                        var funcFieldsList =
                            GetCustomTableValues("doodad_funcs", "doodad_func_group_id", dFuncGroup.id.ToString());
                        foreach (var funcFields in funcFieldsList)
                        {
                            var funcsNode = groupNode.Nodes.Add(funcFields.Count > 0
                                ? "Funcs: " + funcFields["actual_func_type"] + " ( " + funcFields["actual_func_id"] +
                                  " )"
                                : "Funcs");
                            funcsNode.ForeColor = Color.LimeGreen;
                            var tableName = FunctionTypeToTableName(funcFields["actual_func_type"]);
                            var fieldsList = GetCustomTableValues(tableName, "id", funcFields["actual_func_id"]);
                            foreach (var fl in funcFields)
                            {
                                if ((fl.Key == "actual_func_type") || (fl.Key == "actual_func_id"))
                                    continue;

                                AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked, funcsNode);
                                /*
                                if (cbDoodadWorkflowHideEmpty.Checked && IsCustomPropertyEmpty(fl.Value))
                                {
                                    // ignore empty values
                                }
                                else
                                {
                                    funcsNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                }
                                */
                            }

                            funcsNode.Nodes.Add("<=== details ===>").ForeColor = Color.Gray;
                            foreach (var fields in fieldsList)
                            {
                                foreach (var fl in fields)
                                {
                                    AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked,
                                        funcsNode);
                                    /*
                                    if (cbDoodadWorkflowHideEmpty.Checked && IsCustomPropertyEmpty(fl.Value))
                                    {
                                        // ignore empty values
                                    }
                                    else
                                    {
                                        funcsNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                    }
                                    */
                                }
                            }

                            funcsNode.Collapse();
                            if (funcsNode.Nodes.Count <= 0)
                                groupNode.Nodes.Remove(funcsNode);
                        }

                        groupNode.Expand();
                    }
                }

                rootNode.Expand();

            }
            else
            {
                // blank
                lDoodadID.Text = "0";
                lDoodadName.Text = "<none>";
                lDoodadModel.Text = "<none>";
                lDoodadOnceOneMan.Text = "";
                lDoodadOnceOneInteraction.Text = "";
                lDoodadShowName.Text = "";
                lDoodadMgmtSpawn.Text = "";
                lDoodadPercent.Text = "";
                lDoodadMinTime.Text = "";
                lDoodadMaxTime.Text = "";
                lDoodadModelKindID.Text = "";
                lDoodadUseCreatorFaction.Text = "";
                lDoodadForceToDTopPriority.Text = "";
                lDoodadMilestoneID.Text = "";
                lDoodadGroupID.Text = "";
                lDoodadShowName.Text = "";
                lDoodadUseTargetDecal.Text = "";
                lDoodadUseTargetSilhouette.Text = "";
                lDoodadUseTargetHighlight.Text = "";
                lDoodadTargetDecalSize.Text = "";
                lDoodadSimRadius.Text = "";
                lDoodadCollideShip.Text = "";
                lDoodadCollideVehicle.Text = "";
                lDoodadClimateID.Text = "";
                lDoodadSaveIndun.Text = "";
                lDoodadMarkModel.Text = "";
                lDoodadForceUpAction.Text = "";
                lDoodadLoadModelFromWorld.Text = "";
                lDoodadParentable.Text = "";
                lDoodadChildable.Text = "";
                lDoodadFactionID.Text = "";
                lDoodadGrowthTime.Text = "";
                lDoodadDespawnOnCollision.Text = "";
                lDoodadNoCollision.Text = "";
                lDoodadRestrictZoneID.Text = "";

                lDoodadGroupName.Text = "<none>";
                lDoodadGroupIsExport.Text = "";
                lDoodadGroupGuardOnFieldTime.Text = "";
                lDoodadGroupRemovedByHouse.Text = "";

                btnShowDoodadOnMap.Tag = 0;

                dgvDoodadFuncGroups.Rows.Clear();

                tvDoodadDetails.Nodes.Clear();
            }
        }


        private void ShowDBDoodadFuncGroup(long id)
        {
            if (AADB.DB_Doodad_Func_Groups.TryGetValue(id, out var dfg))
            {
                // DoodadFuncGroup
                lDoodadFuncGroupID.Text = dfg.id.ToString();
                lDoodadFuncGroupModel.Text = dfg.model;
                lDoodadFuncGroupKindID.Text = dfg.doodad_func_group_kind_id.ToString();
                lDoodadFuncGroupPhaseMsg.Text = dfg.phase_msgLocalized;
                lDoodadFuncGroupSoundID.Text = dfg.sound_id.ToString();
                lDoodadFuncGroupName.Text = dfg.nameLocalized;
                lDoodadFuncGroupSoundTime.Text = MSToString(dfg.sound_time);
                lDoodadFuncGroupComment.Text = dfg.comment;
                lDoodadFuncGroupIsMsgToZone.Text = dfg.is_msg_to_zone.ToString();

                //lDoodadPhaseFuncsId.Text = "";
                lDoodadPhaseFuncsActualId.Text = "";
                lDoodadPhaseFuncsActualType.Text = "";
                foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                    if (dpf.Value.doodad_func_group_id == dfg.id)
                    {
                        //lDoodadPhaseFuncsId.Text = dpf.Value.id.ToString();
                        lDoodadPhaseFuncsActualId.Text = dpf.Value.actual_func_id.ToString();
                        lDoodadPhaseFuncsActualType.Text = dpf.Value.actual_func_type;
                        break;
                    }
            }
            else
            {
                // blank
                lDoodadFuncGroupID.Text = "0";
                lDoodadFuncGroupModel.Text = "<none>";
                lDoodadFuncGroupKindID.Text = "";
                lDoodadFuncGroupPhaseMsg.Text = "";
                lDoodadFuncGroupSoundID.Text = "";
                lDoodadFuncGroupName.Text = "";
                lDoodadFuncGroupSoundTime.Text = "";
                lDoodadFuncGroupComment.Text = "";
                lDoodadFuncGroupIsMsgToZone.Text = "";

                //lDoodadPhaseFuncsId.Text = "";
                lDoodadPhaseFuncsActualId.Text = "";
                lDoodadPhaseFuncsActualType.Text = "";
            }
        }

        private void DgvDoodadFuncGroups_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoodadFuncGroups.SelectedRows.Count <= 0)
                return;
            var row = dgvDoodadFuncGroups.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDBDoodadFuncGroup(long.Parse(id.ToString()));
            }

        }

        public class MapSpawnLocation
        {
            public long id;
            public float x;
            public float y;
            public float z;

            // Helpers
            public long count = 1;
            public long zoneGroupId; // Used internally, not actually in the files directly

            string FloatToCoord(double f)
            {
                var f1 = Math.Floor(f);
                f -= f1;
                f *= 60;
                var f2 = Math.Floor(f);
                f -= f2;
                f *= 60;
                var f3 = Math.Floor(f);

                return f1.ToString("0") + "° " + f2.ToString("00") + "' " + f3.ToString("00") + "\"";
            }

            public string AsSextant()
            {
                // https://www.reddit.com/r/archeage/comments/3dak17/datamining_every_location_of_everything_in/
                // (0.00097657363894522145695357130138029 * (X - Coordinate)) - 21 = (Longitude in degrees)
                // (0.00097657363894522145695357130138029 * (Y - Coordinate)) - 28 = (Latitude in degrees)

                var fx = (0.00097657363894522145695357130138029f * x) - 21f;
                var fy = (0.00097657363894522145695357130138029f * y) - 28f;
                string res = string.Empty;
                // X - Longitude
                if (fx >= 0f)
                {
                    res += "E ";
                }
                else
                {
                    res += "W ";
                    fx *= -1f;
                }

                res += FloatToCoord(fx);
                res += " , ";
                // Y - Latitude
                if (fy >= 0f)
                {
                    res += "N ";
                }
                else
                {
                    res += "S ";
                    fy *= -1f;
                }

                res += FloatToCoord(fy);

                return res;
            }
        }

        private List<MapSpawnLocation> GetNPCSpawnsInZoneGroup(long zoneGroupId, bool uniqueOnly = false,
            List<long> filterByIDs = null, string instanceName = "main_world")
        {
            List<MapSpawnLocation> res = new List<MapSpawnLocation>();
            var zg = GetZoneGroupByID(zoneGroupId);
            if ((zg != null) && (pak.IsOpen) && (pak.FileExists(zg.GamePakZoneNPCsDat(instanceName))))
            {
                // Open .dat file and read it's contents
                using (var fs = pak.ExportFileAsStream(zg.GamePakZoneNPCsDat(instanceName)))
                {
                    int indexCount = ((int)fs.Length / 16);
                    using (var reader = new BinaryReader(fs))
                    {
                        for (int i = 0; i < indexCount; i++)
                        {
                            MapSpawnLocation msl = new MapSpawnLocation();
                            msl.id = reader.ReadInt32();
                            // Locations not used here, but we need to read it
                            msl.x = reader.ReadSingle();
                            msl.y = reader.ReadSingle();
                            msl.z = reader.ReadSingle();

                            msl.zoneGroupId = zoneGroupId;
                            if ((filterByIDs == null) || filterByIDs.Contains(msl.id))
                            {
                                bool canAdd = true;
                                if (uniqueOnly)
                                    foreach (var m in res)
                                    {
                                        if (m.id == msl.id)
                                        {
                                            canAdd = false;
                                            m.count++;
                                            break;
                                        }
                                    }

                                if (canAdd)
                                    res.Add(msl);
                            }
                        }
                    }
                }
            }

            return res;
        }


        private void BtnFindNPCsInZone_Click(object sender, EventArgs e)
        {
            List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    long ZoneGroupId = (long)b.Tag;
                    var zg = GetZoneGroupByID(ZoneGroupId);
                    string ZoneGroupFile = string.Empty;
                    if (zg != null)
                        ZoneGroupFile = zg.GamePakZoneNPCsDat();

                    if (zg != null)
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var map = MapViewForm.GetMap();
                        map.Show();

                        if (map.GetPoICount() > 0)
                            if (MessageBox.Show("Keep current PoI's on map ?", "Add NPC", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.No)
                                map.ClearPoI();

                        npcList.AddRange(GetNPCSpawnsInZoneGroup(zg.id, false));

                        if (npcList.Count > 0)
                        {
                            using (var loading = new LoadingForm())
                            {
                                loading.ShowInfo("Loading " + npcList.Count.ToString() + " NPCs");
                                loading.Show();

                                // Add to NPC list
                                // tcViewer.SelectedTab = tpNPCs;
                                dgvNPCs.Hide();
                                dgvNPCs.Rows.Clear();
                                //Refresh();
                                int c = 0;
                                foreach (var npc in npcList)
                                {
                                    if (AADB.DB_NPCs.TryGetValue(npc.id, out var z))
                                    {
                                        var line = dgvNPCs.Rows.Add();
                                        var row = dgvNPCs.Rows[line];

                                        row.Cells[0].Value = z.id.ToString();
                                        row.Cells[1].Value = z.nameLocalized;
                                        row.Cells[2].Value = z.level.ToString();
                                        row.Cells[3].Value = z.npc_kind_id.ToString();
                                        row.Cells[4].Value = z.npc_grade_id.ToString();
                                        row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);

                                        var npc_spawner_npcs = AADB.GetNpcSpawnerNpcsByNpcId(z.id);

                                        if (npc_spawner_npcs.Count > 0)
                                        {
                                            if (npc_spawner_npcs.Count == 1)
                                                row.Cells[6].Value = string.Format("Used by 1 spawner, {0}",
                                                    npc_spawner_npcs[0].npc_spawner_id);
                                            else
                                            {
                                                var ids = npc_spawner_npcs.Select(a => a.npc_spawner_id).ToList();
                                                row.Cells[6].Value = string.Format("Used by {0} spawners, {1}",
                                                    ids.Count,
                                                    string.Join(", ", ids));
                                            }
                                        }
                                        else if (npc.count == 1)
                                            row.Cells[6].Value = string.Format("{0} , {1} = ({2})", npc.x, npc.y,
                                                npc.AsSextant());
                                        else
                                            row.Cells[6].Value = npc.count.ToString() + " instances in zone";

                                        c++;
                                        if ((c % 25) == 0)
                                        {
                                            loading.ShowInfo("Loading " + c.ToString() + "/" +
                                                             npcList.Count.ToString() +
                                                             " NPCs");
                                        }

                                        map.AddPoI(npc.x, npc.y, npc.z,
                                            z.nameLocalized + " (" + npc.id.ToString() + ")",
                                            Color.Yellow, 0f, "npc", npc.id, z);
                                    }
                                    else
                                    {
                                        map.AddPoI(npc.x, npc.y, npc.z, "(" + npc.id.ToString() + ")", Color.Red, 0f,
                                            "npc",
                                            npc.id, z);
                                    }
                                }

                                tcViewer.SelectedTab = tpNPCs;
                                dgvNPCs.Show();

                            }
                        }

                        map.tsbShowPoI.Checked = true;
                        map.tsbNamesPoI.Checked = false; // Disable this when loading from zone
                        map.FocusAll(true, false, false);
                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void btnFindQuestsInZone_Click(object sender, EventArgs e)
        {
            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b.Tag != null)
                {
                    long zoneid = (long)b.Tag;
                    bool first = true;

                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;
                    dgvQuests.Hide();

                    foreach (var quest in AADB.DB_Quest_Contexts)
                    {
                        var q = quest.Value;
                        if (q.zone_id == zoneid)
                        {
                            if (first)
                            {
                                first = false;
                                dgvQuests.Rows.Clear();
                            }

                            var line = dgvQuests.Rows.Add();
                            var row = dgvQuests.Rows[line];

                            row.Cells[0].Value = q.id.ToString();
                            row.Cells[1].Value = q.nameLocalized;
                            row.Cells[2].Value = q.level.ToString();
                            if (AADB.DB_Zones.TryGetValue(q.zone_id, out var z))
                            {
                                if (AADB.DB_Zone_Groups.TryGetValue(z.group_id, out var zg))
                                    row.Cells[3].Value = zg.display_textLocalized;
                                else
                                    row.Cells[3].Value = z.display_textLocalized;
                            }
                            else
                                row.Cells[3].Value = q.zone_id.ToString();

                            if (AADB.DB_Quest_Categories.TryGetValue(q.category_id, out var qc))
                                row.Cells[4].Value = qc.nameLocalized;
                            else
                                row.Cells[4].Value = q.category_id.ToString();
                        }
                    }

                    dgvQuests.Show();

                    Application.UseWaitCursor = false;
                    Cursor = Cursors.Default;

                    tcViewer.SelectedTab = tpQuests;
                }
            }

        }

        private void btnQuestsSearch_Click(object sender, EventArgs e)
        {
            string searchText = cbQuestSearch.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            dgvQuests.Rows.Clear();
            foreach (var t in AADB.DB_Quest_Contexts)
            {
                var q = t.Value;
                if ((q.id == searchID) || (q.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvQuests.Rows.Add();
                    var row = dgvQuests.Rows[line];

                    row.Cells[0].Value = q.id.ToString();
                    row.Cells[1].Value = q.nameLocalized;
                    row.Cells[2].Value = q.level.ToString();
                    if (AADB.DB_Zones.TryGetValue(q.zone_id, out var z))
                    {
                        if (AADB.DB_Zone_Groups.TryGetValue(z.group_id, out var zg))
                            row.Cells[3].Value = zg.display_textLocalized;
                        else
                            row.Cells[3].Value = z.display_textLocalized;
                    }
                    else
                        row.Cells[3].Value = q.zone_id.ToString();

                    if (AADB.DB_Quest_Categories.TryGetValue(q.category_id, out var qc))
                        row.Cells[4].Value = qc.nameLocalized;
                    else
                        row.Cells[4].Value = q.category_id.ToString();


                    if (first)
                    {
                        first = false;
                        ShowDBQuest(q.id);
                    }

                }
            }

            if (dgvQuests.Rows.Count > 0)
                AddToSearchHistory(cbQuestSearch, searchText);
        }

        private void tQuestSearch_TextChanged(object sender, EventArgs e)
        {
            btnQuestsSearch.Enabled = (cbQuestSearch.Text != string.Empty);
        }

        private void tQuestSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuestsSearch_Click(null, null);
            }
        }

        private void labelZoneGroupRestrictions_Click(object sender, EventArgs e)
        {
            if (!(sender is Label l) || (l == null))
                return;

            var zoneGroupId = (long)l.Tag;
            var bannedInfo = string.Empty;
            ;
            foreach (var b in AADB.DB_Zone_Group_Banned_Tags)
            {
                if (b.Value.zone_group_id == zoneGroupId)
                {
                    if (AADB.DB_Tags.TryGetValue(b.Value.tag_id, out var tag))
                    {
                        bannedInfo += tag.id + " - " + tag.nameLocalized + "\r\n";
                    }
                }
            }

            MessageBox.Show(bannedInfo, "Restrictions for ZoneGroup " + zoneGroupId.ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void dgvQuests_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQuests.SelectedRows.Count <= 0)
                return;
            var row = dgvQuests.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            var qid = long.Parse(val.ToString());
            ShowDBQuest(qid);
            ShowSelectedData("quest_contexts", "id = " + qid.ToString(), "id ASC");
        }

        private void dgvQuestComponents_SelectionChanged(object sender, EventArgs e)
        {
            /*
            if (dgvQuestComponents.SelectedRows.Count <= 0)
                return;
            var row = dgvQuestComponents.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            var cid = long.Parse(val.ToString());
            ShowDBQuestComponent(cid);
            ShowSelectedData("quest_components", "id = " + cid.ToString(), "id ASC");
            */
        }

        private void tSearchLocalized_TextChanged(object sender, EventArgs e)
        {
            dgvLocalized.Rows.Clear();
            var sTexts = tSearchLocalized.Text.ToLower().Split(' ');
            if (sTexts.Length <= 0)
                return;

            var count = 0;
            foreach (var i in AADB.DB_Translations)
            {
                var translation = i.Value;
                var tSearchString = "=" + translation.table.ToLower() + "=" + translation.field.ToLower() + "=" +
                                    translation.idx.ToString() + "=" + translation.value.ToLower() + "=";
                var searchOK = true;
                foreach (var s in sTexts)
                    if (!tSearchString.Contains(s))
                    {
                        searchOK = false;
                        break;
                    }

                if (searchOK)
                {
                    int line = dgvLocalized.Rows.Add();
                    var row = dgvLocalized.Rows[line];
                    row.Cells[0].Value = translation.table;
                    row.Cells[1].Value = translation.field;
                    row.Cells[2].Value = translation.idx.ToString();
                    row.Cells[3].Value = translation.value;
                    count++;
                }

                if (count > 50)
                    break;
            }
        }

        private void tbLocalizer_Enter(object sender, EventArgs e)
        {
            if (tSearchLocalized.Text == string.Empty)
                tSearchLocalized.Text = "doodad name =320=";
        }

        private void btnSearchBuffs_Click(object sender, EventArgs e)
        {
            string searchText = cbSearchBuffs.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvBuffs.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Buffs)
            {
                var b = t.Value;
                if ((b.id == searchID) || (b.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvBuffs.Rows.Add();
                    var row = dgvBuffs.Rows[line];

                    row.Cells[0].Value = b.id.ToString();
                    row.Cells[1].Value = b.nameLocalized;
                    row.Cells[2].Value = b.duration > 0 ? MSToString(b.duration, true) : "";

                    if (first)
                    {
                        first = false;
                        ShowDBBuff(b.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " buffs, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            if (c > 0)
                AddToSearchHistory(cbSearchBuffs, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void tSearchBuffs_TextChanged(object sender, EventArgs e)
        {
            btnSearchBuffs.Enabled = (cbSearchBuffs.Text.Length > 0);
        }

        private void tSearchBuffs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchBuffs_Click(null, null);
        }

        private void dgvBuffs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBuffs.SelectedRows.Count <= 0)
                return;
            var row = dgvBuffs.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDBBuff(long.Parse(id.ToString()));
                ShowSelectedData("buffs", "id = " + id.ToString(), "id ASC");
            }

        }

        private void btnExportDataForVieweD_Click(object sender, EventArgs e)
        {
            // Create lookup files for use in VieweD

            var LookupExportPath = Path.Combine(Application.StartupPath, "export", "data", "lookup");
            try
            {
                Directory.CreateDirectory(LookupExportPath);
            }
            catch (Exception x)
            {
                MessageBox.Show("Failed to create export directory: " + LookupExportPath + "\r\n" + x.Message);
                return;
            }

            try
            {
                // NPC
                var NPCList = new List<string>();
                foreach (var npc in AADB.DB_NPCs)
                    NPCList.Add(npc.Key.ToString() + ";" + npc.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "npcs.txt"), NPCList);

                // Doodad
                var DoodadList = new List<string>();
                foreach (var doodad in AADB.DB_Doodad_Almighties)
                    DoodadList.Add(doodad.Key.ToString() + ";" + doodad.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "doodads.txt"), DoodadList);

                // Skill
                var SkillList = new List<string>();
                foreach (var skill in AADB.DB_Skills)
                    SkillList.Add(skill.Key.ToString() + ";" + skill.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "skills.txt"), SkillList);

                // Items
                var ItemList = new List<string>();
                foreach (var item in AADB.DB_Items)
                    ItemList.Add(item.Key.ToString() + ";" + item.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "items.txt"), ItemList);

                // Zone Groups
                var ZoneGroupList = new List<string>();
                foreach (var zonegroup in AADB.DB_Zone_Groups)
                    ZoneGroupList.Add(zonegroup.Key.ToString() + ";" + zonegroup.Value.display_textLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "zonegroups.txt"), ZoneGroupList);

                // Zone Keys
                var ZoneKeysList = new List<string>();
                foreach (var zonekey in AADB.DB_Zones)
                    ZoneKeysList.Add(zonekey.Value.zone_key.ToString() + ";" + zonekey.Value.display_textLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "zonekeys.txt"), ZoneKeysList);

                // Factions
                var FactionList = new List<string>();
                foreach (var faction in AADB.DB_GameSystem_Factions)
                    FactionList.Add(faction.Key.ToString() + ";" + faction.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "factions.txt"), FactionList);

                // Quest Names
                var QuestList = new List<string>();
                foreach (var quest in AADB.DB_Quest_Contexts)
                    QuestList.Add(quest.Key.ToString() + ";" + quest.Value.nameLocalized);
                File.WriteAllLines(Path.Combine(LookupExportPath, "quests.txt"), QuestList);


                MessageBox.Show("Done exporting", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception x)
            {
                MessageBox.Show("Export Failed !\r\n" + x.Message);
                return;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            var map = MapViewForm.GetMap();
            map.Show();
        }

        private void btnShowNPCsOnMap_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);
            var map = MapViewForm.GetMap();
            map.Show();

            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            using (var loading = new LoadingForm())
            {
                loading.ShowInfo("Searching in zones: " + AADB.DB_Zone_Groups.Count.ToString());
                loading.Show();

                var zoneCount = 0;
                foreach (var zgv in AADB.DB_Zone_Groups)
                {
                    var zg = zgv.Value;
                    if (zg != null)
                    {
                        zoneCount++;
                        loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                         AADB.DB_Zone_Groups.Count.ToString());
                        npcList.AddRange(GetNPCSpawnsInZoneGroup(zg.id, false));
                    }
                }

                if ((map.GetPoICount() > 0) && (npcList.Count > 0))
                    if (MessageBox.Show("Keep current PoI's ?", "Add NPC", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPoI();

                if (npcList.Count > 0)
                {
                    // Add to NPC list
                    foreach (var npc in npcList)
                    {
                        if (npc.id != searchId)
                            continue;
                        if (AADB.DB_NPCs.TryGetValue(npc.id, out var z))
                        {
                            map.AddPoI(npc.x, npc.y, npc.z, z.nameLocalized + " (" + npc.id.ToString() + ")",
                                Color.Yellow,
                                0f, "npc", npc.id, z);
                        }
                    }
                }

            }

            map.tsbNamesPoI.Checked = true;
            map.cbFocus.Checked = true;
            map.FocusAll(true, false, false);
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        static public Dictionary<string, string> ReadNodeAttributes(XmlNode node)
        {
            var res = new Dictionary<string, string>();
            res.Clear();
            if (node.Attributes != null)
            {
                for (var i = 0; i < node.Attributes.Count; i++)
                    res.Add(node.Attributes.Item(i).Name.ToLower(), node.Attributes.Item(i).Value);
            }

            return res;
        }

        private void btnFindTransferPathsInZone_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);

            List<MapViewPath> allpaths = new List<MapViewPath>();

            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    if ((sender as Button).Tag == null)
                        return;
                    var searchId = (long)(sender as Button).Tag;
                    if (searchId <= 0)
                        return;

                    foreach (var zv in AADB.DB_Zones)
                        if (zv.Value.zone_key == searchId)
                            AddTransferPath(ref allpaths, zv.Value);

                    if (allpaths.Count <= 0)
                        MessageBox.Show("No paths found inside this zone");
                    else
                    {
                        // Show on map
                        //MessageBox.Show("Found " + allpoints.Count.ToString() + " points inside " + pathsFound.ToString() + " paths");
                        var map = MapViewForm.GetMap();
                        map.Show();
                        if (map.GetPathCount() > 0)
                            if (MessageBox.Show("Append paths ?", "Add path to map", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) != DialogResult.Yes)
                                map.ClearPaths();
                        foreach (var p in allpaths)
                            map.AddPath(p);
                        map.tsbNamesPoI.Checked = true;
                        map.FocusAll(false, true, false);
                    }

                }
            }
        }

        private Vector3 ReadXmlPos(string posStringBase)
        {
            var baseVec = Vector3.Zero;
            var posVals = posStringBase.Split(',');
            if (posVals.Length != 3)
            {
                MessageBox.Show("Invalid number of values inside Pos: " + posStringBase);
            }
            else
                try
                {
                    baseVec = new Vector3(
                        float.Parse(posVals[0], CultureInfo.InvariantCulture),
                        float.Parse(posVals[1], CultureInfo.InvariantCulture),
                        float.Parse(posVals[2], CultureInfo.InvariantCulture)
                    );
                }
                catch
                {
                    MessageBox.Show("Invalid float inside Pos: " + posStringBase);
                }

            return baseVec;
        }

        private void AddCustomPath(ref List<MapViewPath> allpaths, Stream fileStream,
            string rootNodeName = "/Objects/Entity", string PointsNodeName = "Points/Point")
        {
            try
            {
                var _doc = new XmlDocument();
                _doc.Load(fileStream);
                var _allTransferBlocks = _doc.SelectNodes(rootNodeName);
                var pathsFound = 0;
                for (var i = 0; i < _allTransferBlocks.Count; i++)
                {
                    var block = _allTransferBlocks[i];
                    var attribs = ReadNodeAttributes(block);

                    if (attribs.TryGetValue("name", out var blockName))
                    {

                        var newPath = new MapViewPath();
                        newPath.PathName = blockName;

                        var baseVec = new Vector3();

                        if (attribs.TryGetValue("pos", out var posStringBase))
                        {
                            var posVals = posStringBase.Split(',');
                            if (posVals.Length != 3)
                            {
                                MessageBox.Show("Invalid number of values inside Pos: " + posStringBase);
                                continue;
                            }

                            try
                            {
                                baseVec = new Vector3(
                                    float.Parse(posVals[0], CultureInfo.InvariantCulture),
                                    float.Parse(posVals[1], CultureInfo.InvariantCulture),
                                    float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                );
                            }
                            catch
                            {
                                MessageBox.Show("Invalid float inside Pos: " + posStringBase);
                            }
                        }


                        pathsFound++;
                        var pointsxml = block.SelectNodes(PointsNodeName);
                        for (var n = 0; n < pointsxml.Count; n++)
                        {
                            var pointxml = pointsxml[n];
                            var pointattribs = ReadNodeAttributes(pointxml);
                            if (pointattribs.TryGetValue("pos", out var posString))
                            {
                                var posVals = posString.Split(',');
                                if (posVals.Length != 3)
                                {
                                    MessageBox.Show("Invalid number of values inside Pos: " + posString);
                                    continue;
                                }

                                try
                                {
                                    var vec = new Vector3(
                                        float.Parse(posVals[0], CultureInfo.InvariantCulture),
                                        float.Parse(posVals[1], CultureInfo.InvariantCulture),
                                        float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                    ) + baseVec;
                                    newPath.AddPoint(vec);
                                }
                                catch
                                {
                                    MessageBox.Show("Invalid float inside Pos: " + posString);
                                }

                            }
                        }

                        allpaths.Add(newPath);

                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void AddTransferPath(ref List<MapViewPath> allpaths, GameZone zone)
        {
            if (zone != null)
            {
                var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.zone_key);

                // If it's not in the world.xml, it's probably not a real zone anyway
                if (worldOff == null)
                    return;

                if (!pak.IsOpen || !pak.FileExists(zone.GamePakZoneTransferPathXML))
                {
                    // MessageBox.Show("No path file found for this zone");
                    return;
                }
                // MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

                int pathsFound = 0;
                try
                {
                    var fs = pak.ExportFileAsStream(zone.GamePakZoneTransferPathXML);

                    var _doc = new XmlDocument();
                    _doc.Load(fs);
                    var _allTransferBlocks = _doc.SelectNodes("/Objects/Transfer");
                    for (var i = 0; i < _allTransferBlocks.Count; i++)
                    {
                        var block = _allTransferBlocks[i];
                        var attribs = ReadNodeAttributes(block);

                        if (attribs.TryGetValue("name", out var blockName))
                        {

                            var newPath = new MapViewPath();
                            newPath.PathName = blockName;

                            foreach (var tp in AADB.DB_TransferPaths)
                                if (tp.path_name == blockName)
                                    newPath.TypeId = tp.owner_id;

                            long model = 0;
                            if (AADB.DB_Transfers.TryGetValue(newPath.TypeId, out var transfer))
                            {
                                model = transfer.model_id;
                            }

                            bool knownType = true;
                            switch (model)
                            {
                                case 116: // Ferry
                                    newPath.Color = Color.Navy;
                                    break;
                                case 314: // Tram
                                case 606: // Tram
                                    newPath.Color = Color.Yellow;
                                    break;
                                case 548: // Andelph Train
                                    newPath.Color = Color.Yellow;
                                    break;
                                case 654: // Carriage
                                    newPath.Color = Color.DarkOrange;
                                    break;
                                case 657: // Airship
                                    newPath.Color = Color.Blue;
                                    newPath.DrawStyle = 1;
                                    break;
                                case 2217: // Cargo Ship (2C-Solis)
                                case 2236: // Cargo Ship (Solz-Yny)
                                    newPath.Color = Color.DarkCyan;
                                    newPath.DrawStyle = 2;
                                    break;
                                default:
                                    knownType = false;
                                    break;
                            }

                            if (!knownType)
                                newPath.PathName = blockName + "(t:" + newPath.TypeId.ToString() + " m:" +
                                                   model.ToString() + " z:" + zone.zone_key.ToString() + ")";
                            else
                                newPath.PathName = blockName + "(t:" + newPath.TypeId.ToString() + " z:" +
                                                   zone.zone_key.ToString() + ")";

                            // We don't need the cellX/Y values here
                            /*
                            int cellXOffset = 0;
                            int cellYOffset = 0;

                            if (attribs.TryGetValue("cellx",out var cellXOffsetString))
                                try { cellXOffset = int.Parse(cellXOffsetString); }
                                catch { cellXOffset = 0; }
                            if (attribs.TryGetValue("celly", out var cellYOffsetString))
                                try { cellYOffset = int.Parse(cellYOffsetString); }
                                catch { cellYOffset = 0; }
                            */

                            PointF cellOffset = new PointF((worldOff.originCellX * 1024f),
                                (worldOff.originCellY * 1024f));
                            pathsFound++;

                            var pointsxml = block.SelectNodes("Points/Point");
                            for (var n = 0; n < pointsxml.Count; n++)
                            {
                                var pointxml = pointsxml[n];
                                var pointattribs = ReadNodeAttributes(pointxml);
                                if (pointattribs.TryGetValue("pos", out var posString))
                                {
                                    var posVals = posString.Split(',');
                                    if (posVals.Length != 3)
                                    {
                                        MessageBox.Show("Invalid number of values inside Pos: " + posString);
                                        continue;
                                    }

                                    try
                                    {
                                        var vec = new Vector3(
                                            float.Parse(posVals[0], CultureInfo.InvariantCulture) + cellOffset.X,
                                            float.Parse(posVals[1], CultureInfo.InvariantCulture) + cellOffset.Y,
                                            float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                        );
                                        newPath.AddPoint(vec);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Invalid float inside Pos: " + posString);
                                    }

                                }
                            }

                            allpaths.Add(newPath);

                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

            }
            else
            {
                MessageBox.Show("Invalid zone selected ?");
                return;
            }
        }

        private void AddSubZones(ref List<MapViewPath> allareas, GameZone zone)
        {
            if (zone != null)
            {
                var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.zone_key);

                // If it's not in the world.xml, it's probably not a real zone anyway
                if (worldOff == null)
                    return;

                if (!pak.IsOpen || !pak.FileExists(zone.GamePakSubZoneXML))
                {
                    // MessageBox.Show("No path file found for this zone");
                    return;
                }
                //MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

                int areasFound = 0;
                try
                {
                    var fs = pak.ExportFileAsStream(zone.GamePakSubZoneXML);

                    var _doc = new XmlDocument();
                    _doc.Load(fs);
                    var _allHousingBlocks = _doc.SelectNodes("/Objects/Entity");
                    for (var i = 0; i < _allHousingBlocks.Count; i++)
                    {
                        var block = _allHousingBlocks[i];
                        var entityAttribs = ReadNodeAttributes(block);

                        if (entityAttribs.TryGetValue("name", out var blockName))
                        {

                            int cellXOffset = 0;
                            int cellYOffset = 0;

                            if (entityAttribs.TryGetValue("cellx", out var cellXOffsetString))
                                try
                                {
                                    cellXOffset = int.Parse(cellXOffsetString);
                                }
                                catch
                                {
                                    cellXOffset = 0;
                                }

                            if (entityAttribs.TryGetValue("celly", out var cellYOffsetString))
                                try
                                {
                                    cellYOffset = int.Parse(cellYOffsetString);
                                }
                                catch
                                {
                                    cellYOffset = 0;
                                }


                            var areaNodes = block.SelectNodes("Area");
                            for (var j = 0; j < areaNodes.Count; j++)
                            {
                                var areaNode = areaNodes[j];
                                var areaAttribs = ReadNodeAttributes(areaNode);
                                var startVector = new Vector3();

                                var newArea = new MapViewPath();
                                newArea.PathName = blockName;


                                if (areaAttribs.TryGetValue("value1", out var val1))
                                {
                                    newArea.TypeId = long.Parse(val1);
                                }

                                if (newArea.TypeId <= 0)
                                    newArea.Color = Color.Orange;

                                newArea.PathName = blockName + "(v:" + newArea.TypeId.ToString() + " z:" +
                                                   zone.zone_key.ToString() + ")";

                                if (entityAttribs.TryGetValue("pos", out var valPos))
                                {
                                    var posVals = valPos.Split(',');
                                    if (posVals.Length != 3)
                                    {
                                        MessageBox.Show("Invalid number of values inside Pos: " + valPos);
                                        continue;
                                    }

                                    try
                                    {
                                        startVector = new Vector3(
                                            float.Parse(posVals[0], CultureInfo.InvariantCulture),
                                            float.Parse(posVals[1], CultureInfo.InvariantCulture),
                                            float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                        );
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Invalid float inside Pos: " + valPos);
                                    }
                                }


                                PointF cellOffset = new PointF(((worldOff.originCellX + cellXOffset) * 1024f),
                                    ((worldOff.originCellY + cellYOffset) * 1024f));
                                areasFound++;

                                Vector3 firstVector = Vector3.Zero;

                                var pointsxml = areaNode.SelectNodes("Points/Point");
                                for (var n = 0; n < pointsxml.Count; n++)
                                {
                                    var pointxml = pointsxml[n];
                                    var pointattribs = ReadNodeAttributes(pointxml);
                                    if (pointattribs.TryGetValue("pos", out var posString))
                                    {
                                        var posVals = posString.Split(',');
                                        if (posVals.Length != 3)
                                        {
                                            MessageBox.Show("Invalid number of values inside Pos: " + posString);
                                            continue;
                                        }

                                        try
                                        {
                                            var vec = new Vector3(
                                                float.Parse(posVals[0], CultureInfo.InvariantCulture) + cellOffset.X,
                                                float.Parse(posVals[1], CultureInfo.InvariantCulture) + cellOffset.Y,
                                                float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                            ) + startVector;
                                            if (firstVector == Vector3.Zero)
                                                firstVector = vec;
                                            newArea.AddPoint(vec);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Invalid float inside Pos: " + posString);
                                        }

                                    }
                                }

                                // Close the loop
                                if (firstVector != Vector3.Zero)
                                    newArea.AddPoint(firstVector);

                                allareas.Add(newArea);

                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

            }
            else
            {
                MessageBox.Show("Invalid zone selected ?");
                return;
            }
        }

        private void AddHousingZones(ref List<MapViewPath> allareas, GameZone zone)
        {
            if (zone != null)
            {
                var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.zone_key);

                // If it's not in the world.xml, it's probably not a real zone anyway
                if (worldOff == null)
                    return;

                if (!pak.IsOpen || !pak.FileExists(zone.GamePakZoneHousingXML))
                {
                    // MessageBox.Show("No path file found for this zone");
                    return;
                }
                // MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

                int areasFound = 0;
                try
                {
                    var fs = pak.ExportFileAsStream(zone.GamePakZoneHousingXML);

                    var _doc = new XmlDocument();
                    _doc.Load(fs);
                    var _allHousingBlocks = _doc.SelectNodes("/Objects/Entity");
                    for (var i = 0; i < _allHousingBlocks.Count; i++)
                    {
                        var block = _allHousingBlocks[i];
                        var entityAttribs = ReadNodeAttributes(block);

                        if (entityAttribs.TryGetValue("name", out var blockName))
                        {

                            int cellXOffset = 0;
                            int cellYOffset = 0;

                            if (entityAttribs.TryGetValue("cellx", out var cellXOffsetString))
                                try
                                {
                                    cellXOffset = int.Parse(cellXOffsetString);
                                }
                                catch
                                {
                                    cellXOffset = 0;
                                }

                            if (entityAttribs.TryGetValue("celly", out var cellYOffsetString))
                                try
                                {
                                    cellYOffset = int.Parse(cellYOffsetString);
                                }
                                catch
                                {
                                    cellYOffset = 0;
                                }


                            var areaNodes = block.SelectNodes("Area");
                            for (var j = 0; j < areaNodes.Count; j++)
                            {
                                var areaNode = areaNodes[j];
                                var areaAttribs = ReadNodeAttributes(areaNode);
                                var startVector = new Vector3();

                                var newArea = new MapViewPath();
                                newArea.PathName = blockName;


                                if (areaAttribs.TryGetValue("value1", out var val1))
                                {
                                    newArea.TypeId = long.Parse(val1);
                                }

                                if (newArea.TypeId <= 0)
                                    newArea.Color = Color.DarkGray;

                                newArea.PathName = blockName + "(v:" + newArea.TypeId.ToString() + " z:" +
                                                   zone.zone_key.ToString() + ")";

                                if (entityAttribs.TryGetValue("pos", out var valPos))
                                {
                                    var posVals = valPos.Split(',');
                                    if (posVals.Length != 3)
                                    {
                                        MessageBox.Show("Invalid number of values inside Pos: " + valPos);
                                        continue;
                                    }

                                    try
                                    {
                                        startVector = new Vector3(
                                            float.Parse(posVals[0], CultureInfo.InvariantCulture),
                                            float.Parse(posVals[1], CultureInfo.InvariantCulture),
                                            float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                        );
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Invalid float inside Pos: " + valPos);
                                    }
                                }

                                PointF cellOffset = new PointF(((worldOff.originCellX + cellXOffset) * 1024f),
                                    ((worldOff.originCellY + cellYOffset) * 1024f));
                                areasFound++;

                                Vector3 firstVector = Vector3.Zero;

                                var pointsxml = areaNode.SelectNodes("Points/Point");
                                for (var n = 0; n < pointsxml.Count; n++)
                                {
                                    var pointxml = pointsxml[n];
                                    var pointattribs = ReadNodeAttributes(pointxml);
                                    if (pointattribs.TryGetValue("pos", out var posString))
                                    {
                                        var posVals = posString.Split(',');
                                        if (posVals.Length != 3)
                                        {
                                            MessageBox.Show("Invalid number of values inside Pos: " + posString);
                                            continue;
                                        }

                                        try
                                        {
                                            var vec = new Vector3(
                                                float.Parse(posVals[0], CultureInfo.InvariantCulture) + cellOffset.X,
                                                float.Parse(posVals[1], CultureInfo.InvariantCulture) + cellOffset.Y,
                                                float.Parse(posVals[2], CultureInfo.InvariantCulture)
                                            ) + startVector;
                                            if (firstVector == Vector3.Zero)
                                                firstVector = vec;
                                            newArea.AddPoint(vec);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Invalid float inside Pos: " + posString);
                                        }

                                    }
                                }

                                // Close the loop
                                if (firstVector != Vector3.Zero)
                                    newArea.AddPoint(firstVector);

                                allareas.Add(newArea);

                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

            }
            else
            {
                MessageBox.Show("Invalid zone selected ?");
                return;
            }
        }

        public class NPCExportDataPosition
        {
            public float X = 0f;
            public float Y = 0f;
            public float Z = 0f;
            public sbyte RotationX = 0;
            public sbyte RotationY = 0;
            public sbyte RotationZ = 0;
        }

        public class NPCExportData
        {
            public long Id = 0;
            public long Count = 1;
            public long UnitId = 0;
            public string Title = string.Empty;
            public NPCExportDataPosition Position = new NPCExportDataPosition();
            public float Scale = 0f;
            public long Zone = 0;
        }

        public class DoodadExportData
        {
            public long Id = 0;
            public long UnitId = 0;
            public string Title = string.Empty;
            public NPCExportDataPosition Position = new NPCExportDataPosition();
            public float Scale = 0f;
            public long FuncGroupId = 0;
            public long Zone = 0;
        }

        private void btnExportNPCSpawnData_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);

            foreach (var inst in MapViewWorldXML.instances)
            {
                var NPCList = new List<NPCExportData>();
                var i = 0;

                var zoneGroups = new List<long>();
                foreach (var z in inst.zones)
                {
                    var zone = AADB.GetZoneByKey(z.Value.zone_key);
                    if (zone != null)
                        if (!zoneGroups.Contains(zone.group_id))
                            zoneGroups.Add(zone.group_id);
                }

                foreach (var zoneGroupId in zoneGroups)
                {
                    if (!AADB.DB_Zone_Groups.TryGetValue(zoneGroupId, out var zoneGroup))
                        continue;

                    var npcs = GetNPCSpawnsInZoneGroup(zoneGroup.id, false, null, inst.WorldName);
                    foreach (var npc in npcs)
                    {
                        i++;
                        var newNPC = new NPCExportData();
                        newNPC.Id = i;
                        newNPC.Count = 1;
                        newNPC.UnitId = npc.id;
                        if (AADB.DB_NPCs.TryGetValue(npc.id, out var vNPC))
                            newNPC.Title = vNPC.nameLocalized;
                        newNPC.Position.X = npc.x;
                        newNPC.Position.Y = npc.y;
                        newNPC.Position.Z = npc.z;
                        newNPC.Zone = npc.zoneGroupId;

                        NPCList.Add(newNPC);
                    }
                }

                string json = JsonConvert.SerializeObject(NPCList.ToArray(), Newtonsoft.Json.Formatting.Indented);
                Directory.CreateDirectory(Path.Combine("export", "Data", "Worlds", inst.WorldName));
                File.WriteAllText(Path.Combine("export", "Data", "Worlds", inst.WorldName, "npc_spawns.json"), json);

            }

            MessageBox.Show("Done");
        }

        private void btnFindAllTransferPaths_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);
            List<MapViewPath> allpaths = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            foreach (var zv in AADB.DB_Zones)
                AddTransferPath(ref allpaths, zv.Value);

            if (allpaths.Count <= 0)
                MessageBox.Show("No paths found ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();

                if (map.GetPathCount() > 0)
                    if (MessageBox.Show("Keep current paths ?", "Add Transfers", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPaths();

                foreach (var p in allpaths)
                    map.AddPath(p);

                map.tsbShowPath.Checked = true;
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        public void PrepareWorldXML(bool overwrite)
        {
            if (overwrite || (MapViewWorldXML.instances == null) || (MapViewWorldXML.instances.Count < 0))
            {
                if (!pak.IsOpen)
                {
                    MessageBox.Show("Game pak file was not loaded !");
                    return;
                }

                MapViewWorldXML.instances = new List<MapViewWorldXML>();

                foreach (var pfi in pak.Files)
                {
                    if (pfi.Name.EndsWith("/world.xml") && pfi.Name.StartsWith("game/worlds/"))
                    {
                        var splitName = pfi.Name.ToLower().Split('/');
                        if (splitName.Count() != 4)
                            continue;
                        var thisInstanceName = splitName[2];

                        var inst = new MapViewWorldXML();
                        if (inst.LoadFromStream(
                                pak.ExportFileAsStream("game/worlds/" + thisInstanceName + "/world.xml")))
                        {
                            MapViewWorldXML.instances.Add(inst);
                            if (thisInstanceName == "main_world")
                                MapViewWorldXML.main_world = inst;
                        }
                        else
                        {
                            MessageBox.Show("Failed to load " + thisInstanceName);
                        }
                    }
                }

            }
        }

        private List<MapSpawnLocation> GetDoodadSpawnsInZoneGroup(long zoneGroupId, bool uniqueOnly = false,
            long filterByID = -1, string instanceName = "main_world")
        {
            List<MapSpawnLocation> res = new List<MapSpawnLocation>();
            var zg = GetZoneGroupByID(zoneGroupId);
            if ((zg != null) && (pak.IsOpen) && (pak.FileExists(zg.GamePakZoneDoodadsDat(instanceName))))
            {
                // Open .dat file and read it's contents
                using (var fs = pak.ExportFileAsStream(zg.GamePakZoneDoodadsDat(instanceName)))
                {
                    int indexCount = ((int)fs.Length / 16);
                    using (var reader = new BinaryReader(fs))
                    {
                        for (int i = 0; i < indexCount; i++)
                        {
                            MapSpawnLocation msl = new MapSpawnLocation();
                            msl.id = reader.ReadInt32();
                            // Locations not used here, but we need to read it
                            msl.x = reader.ReadSingle();
                            msl.y = reader.ReadSingle();
                            msl.z = reader.ReadSingle();

                            msl.zoneGroupId = zoneGroupId;
                            if ((filterByID == -1) || (msl.id == filterByID))
                            {
                                bool canAdd = true;
                                if (uniqueOnly)
                                    foreach (var m in res)
                                    {
                                        if (m.id == msl.id)
                                        {
                                            canAdd = false;
                                            m.count++;
                                            break;
                                        }
                                    }

                                if (canAdd)
                                    res.Add(msl);
                            }
                        }
                    }
                }
            }

            return res;
        }

        private void btnShowDoodadOnMap_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);
            var map = MapViewForm.GetMap();
            map.Show();
            map.ClearPoI();

            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            List<MapSpawnLocation> doodadList = new List<MapSpawnLocation>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            using (var loading = new LoadingForm())
            {
                loading.ShowInfo("Searching in zones: " + AADB.DB_Zone_Groups.Count.ToString());
                loading.Show();

                var zoneCount = 0;
                foreach (var zgv in AADB.DB_Zone_Groups)
                {
                    var zg = zgv.Value;
                    if (zg != null)
                    {
                        zoneCount++;
                        loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                         AADB.DB_Zone_Groups.Count.ToString());
                        doodadList.AddRange(GetDoodadSpawnsInZoneGroup(zg.id, false));
                    }
                }

                if (doodadList.Count > 0)
                {
                    // Add to NPC list
                    foreach (var doodad in doodadList)
                    {
                        if (doodad.id != searchId)
                            continue;
                        if (AADB.DB_Doodad_Almighties.TryGetValue(doodad.id, out var z))
                        {
                            map.AddPoI(doodad.x, doodad.y, doodad.z,
                                z.nameLocalized + " (" + doodad.id.ToString() + ")",
                                Color.Yellow, 0f, "doodad", doodad.id, z);
                        }
                    }
                }

            }

            map.tsbNamesPoI.Checked = true;
            map.cbFocus.Checked = true;
            map.FocusAll(true, false, false);
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        public void LoadQuestSpheres()
        {
            if ((pak == null) || (!pak.IsOpen))
                return;


            AADB.PAK_QuestSignSpheres = new List<QuestSphereEntry>();
            var sl = new List<string>();

            // Find all related files and concat them into a giant stringlist
            foreach (var pfi in pak.Files)
            {
                var lowername = pfi.Name.ToLower();
                if (lowername.EndsWith("quest_sign_sphere.g"))
                {
                    var namesplited = lowername.Split('/');
                    var thisStream = pak.ExportFileAsStream(pfi);
                    using (var rs = new StreamReader(thisStream))
                    {
                        sl.Clear();
                        while (!rs.EndOfStream)
                        {
                            sl.Add(rs.ReadLine().Trim(' ').Trim('\t').ToLower());
                        }
                    }

                    var worldname = "";
                    var zone = 0;

                    if (namesplited.Length > 6)
                    {
                        if ((namesplited[0] == "game") &&
                            (namesplited[1] == "worlds") &&
                            (namesplited[3] == "level_design") &&
                            (namesplited[4] == "zone") &&
                            (namesplited[6] == "client")
                           )
                        {
                            worldname = namesplited[2];
                            zone = int.Parse(namesplited[5]);
                        }
                    }

                    var zoneOffX = 0f;
                    var zoneOffY = 0f;

                    var zonexml = MapViewWorldXML.main_world.GetZoneByKey(zone);
                    if (zonexml != null)
                    {
                        zoneOffX = zonexml.originCellX * 1024f;
                        zoneOffY = zonexml.originCellY * 1024f;
                    }

                    /*
                    GameZone gameZone = AADB.GetZoneByKey(zone);
                    if (gameZone != null)
                    {
                        if (AADB.DB_Zone_Groups.TryGetValue(gameZone.group_id, out var zg))
                        {
                            zoneOffX = zg.PosAndSize.X;
                            zoneOffY = zg.PosAndSize.Y;
                        }
                        else
                        {
                            // no zone group ?
                        }
                    }
                    else
                    {
                        // zone not found in DB
                    }
                    */

                    for (int i = 0; i < sl.Count - 4; i++)
                    {
                        var l0 = sl[i + 0]; // area
                        var l1 = sl[i + 1]; // qtype
                        var l2 = sl[i + 2]; // ctype
                        var l3 = sl[i + 3]; // pos
                        var l4 = sl[i + 4]; // radius


                        if (l0.StartsWith("area") && l1.StartsWith("qtype") && l2.StartsWith("ctype") &&
                            l3.StartsWith("pos") && l4.StartsWith("radius"))
                        {
                            try
                            {
                                var qse = new QuestSphereEntry();
                                qse.worldID = worldname;
                                qse.zoneID = zone;

                                qse.questID = int.Parse(l1.Substring(6));

                                qse.componentID = int.Parse(l2.Substring(6));

                                var subline = l3.Substring(4).Replace("(", "").Replace(")", "").Replace("x", "")
                                    .Replace("y", "").Replace("z", "").Replace(" ", "");
                                var posstring = subline.Split(',');
                                if (posstring.Length == 3)
                                {
                                    // Parse the floats with NumberStyles.Float and CultureInfo.InvariantCulture or we get all sorts of
                                    // weird stuff with the decimal points depending on the user's language settings
                                    qse.X = zoneOffX + float.Parse(posstring[0], NumberStyles.Float,
                                        CultureInfo.InvariantCulture);
                                    qse.Y = zoneOffY + float.Parse(posstring[1], NumberStyles.Float,
                                        CultureInfo.InvariantCulture);
                                    qse.Z = float.Parse(posstring[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                                }

                                qse.radius = float.Parse(l4.Substring(7), NumberStyles.Float,
                                    CultureInfo.InvariantCulture);

                                AADB.PAK_QuestSignSpheres.Add(qse);
                                i += 5;
                            }
                            catch (Exception x)
                            {
                                MessageBox.Show("Exception: " + x.Message, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    // System.Threading.Thread.Sleep(5);
                }
            }


        }

        private void btnFindAllQuestSpheres_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            PrepareWorldXML(false);
            var map = MapViewForm.GetMap();
            map.Show();

            if (map.GetQuestSphereCount() > 0)
                if (MessageBox.Show("Keep current Quest Spheres ?", "Add Spheres", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    map.ClearQuestSpheres();

            foreach (var p in AADB.PAK_QuestSignSpheres)
            {
                var name = string.Empty;
                if (AADB.DB_Quest_Contexts.TryGetValue(p.questID, out var qc))
                    name += qc.nameLocalized + " ";
                name += "q:" + p.questID.ToString() + " c:" + p.componentID.ToString();
                Color col = Color.LightCyan;

                var isFilteredVal = false;
                if ((eQuestSignSphereSearch.Text != string.Empty) &&
                    name.ToLower().Contains(eQuestSignSphereSearch.Text.ToLower()))
                    isFilteredVal = true;
                if (isFilteredVal)
                    col = Color.Red;

                if (cbQuestSignSphereSearchShowAll.Checked || (eQuestSignSphereSearch.Text == string.Empty))
                {
                    map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.radius, p.componentID);
                }
                else if (isFilteredVal)
                    map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.radius, p.componentID);
            }

            map.tsbShowQuestSphere.Checked = true;
            map.tsbNamesQuestSphere.Checked =
                (!cbQuestSignSphereSearchShowAll.Checked && (eQuestSignSphereSearch.Text != string.Empty));
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void btnFindAllHousing_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);
            List<MapViewPath> allareas = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            foreach (var zv in AADB.DB_Zones)
                AddHousingZones(ref allareas, zv.Value);

            if (allareas.Count <= 0)
                MessageBox.Show("No housing found ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();

                // We no longer have the housing by zone button, so we no longer need to ask, just clear it
                //if (map.GetHousingCount() > 0)
                //    if (MessageBox.Show("Keep current housing areas ?", "Add Housing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                map.ClearHousing();

                foreach (var p in allareas)
                    map.AddHousing(p);

                map.tsbShowHousing.Checked = true;
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        private void btnFindAllSubZone_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);
            List<MapViewPath> allareas = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            foreach (var zv in AADB.DB_Zones)
                AddSubZones(ref allareas, zv.Value);

            if (allareas.Count <= 0)
                MessageBox.Show("No subzone found ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();

                // We no longer have the housing by zone button, so we no longer need to ask, just clear it
                //if (map.GetHousingCount() > 0)
                //    if (MessageBox.Show("Keep current housing areas ?", "Add Housing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                map.ClearSubZone();

                foreach (var p in allareas)
                {
                    map.AddSubZone(p);
                }


                map.tsbShowSubzone.Checked = true;
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;


        }

        private void btnQuestFindRelatedOnMap_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            if ((sender as Button).Tag == null)
                return;
            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            PrepareWorldXML(false);
            var map = MapViewForm.GetMap();
            map.Show();

            var foundCount = 0;

            if ((map.GetQuestSphereCount() > 0) || (map.GetPoICount() > 0))
                if (MessageBox.Show("Keep current NPC and Quest Spheres ?", "Add Quest Info", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                {
                    map.ClearPoI();
                    map.ClearQuestSpheres();
                }

            var sphereCount = 0;
            // Quest Spheres
            foreach (var p in AADB.PAK_QuestSignSpheres)
            {
                var name = string.Empty;
                if (AADB.DB_Quest_Contexts.TryGetValue(p.questID, out var qc))
                    name += qc.nameLocalized + " ";
                else
                    continue;
                if (qc.id != searchId)
                    continue;
                name += "q:" + p.questID.ToString() + " c:" + p.componentID.ToString();
                map.AddQuestSphere(p.X, p.Y, p.Z, name, Color.Cyan, p.radius, p.componentID);
                sphereCount++;
            }

            var NPCsToShow = new List<long>();
            // TODO: NPCs (start/end/progress)
            // TODO: Monsters (single, group, zone)
            var comps = from c in AADB.DB_Quest_Components
                        where c.Value.quest_context_id == searchId
                        select c.Value;
            foreach (var c in comps)
            {
                if ((c.npc_id > 0) && (!NPCsToShow.Contains(c.npc_id)))
                    NPCsToShow.Add(c.npc_id);

                var acts = from a in AADB.DB_Quest_Acts
                           where a.Value.quest_component_id == c.id
                           select a.Value;

                foreach (var a in acts)
                {
                    if (a.act_detail_type == "QuestActObjMonsterHunt")
                    {
                        string sql = "SELECT * FROM quest_act_obj_monster_hunts WHERE id = " +
                                     a.act_detail_id.ToString();
                        using (var connection = SQLite.CreateConnection())
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = sql;
                                command.Prepare();
                                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                                {
                                    while (reader.Read())
                                    {
                                        var huntNPC = GetInt64(reader, "npc_id");
                                        if (!NPCsToShow.Contains(huntNPC))
                                        {
                                            NPCsToShow.Add(huntNPC);
                                            foundCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (NPCsToShow.Count > 0)
            {
                List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

                using (var loading = new LoadingForm())
                {
                    loading.ShowInfo("Searching NPCs in zones: " + AADB.DB_Zone_Groups.Count.ToString());
                    loading.Show();

                    var zoneCount = 0;
                    foreach (var zgv in AADB.DB_Zone_Groups)
                    {
                        var zg = zgv.Value;
                        if (zg != null)
                        {
                            zoneCount++;
                            loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                             AADB.DB_Zone_Groups.Count.ToString());
                            npcList.AddRange(GetNPCSpawnsInZoneGroup(zg.id, false, NPCsToShow));
                        }
                    }

                    if (npcList.Count > 0)
                    {
                        // Add to NPC list
                        foreach (var npc in npcList)
                        {
                            //if (!NPCsToShow.Contains(npc.id))
                            //    continue;
                            if (AADB.DB_NPCs.TryGetValue(npc.id, out var z))
                            {
                                map.AddPoI(npc.x, npc.y, npc.z, z.nameLocalized + " (" + npc.id.ToString() + ")",
                                    Color.Yellow, 0f, "npc", npc.id, z);
                                foundCount++;
                            }
                        }
                    }

                }

            }

            if (foundCount <= 0)
            {
                if (NPCsToShow.Count > 0)
                    MessageBox.Show("The Quest listed NPCs, but no valid match was found in the dat files.");
            }

            if ((foundCount <= 0) && (sphereCount <= 0))
                MessageBox.Show("Nothing to display.");

            map.tsbShowQuestSphere.Checked = true;
            map.tsbNamesQuestSphere.Checked = true;
            map.FocusAll(true, false, true);
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void tFilterTables_TextChanged(object sender, EventArgs e)
        {
            var lastSelected = lbTableNames.SelectedIndex >= 0 ? lbTableNames.Text : "";
            if (tFilterTables.Text == string.Empty)
            {
                lbTableNames.Items.Clear();
                foreach (var s in allTableNames)
                    lbTableNames.Items.Add(s);
            }
            else
            {
                lbTableNames.Items.Clear();
                foreach (var s in allTableNames)
                {
                    if (s.ToLower().Contains(tFilterTables.Text.ToLower()))
                    {
                        lbTableNames.Items.Add(s);
                        if (s == lastSelected)
                            lbTableNames.SelectedIndex = lbTableNames.Items.Count - 1;
                    }
                }
            }
        }

        private void btnLoadCustomPaths_Click(object sender, EventArgs e)
        {
            List<MapViewPath> allareas = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            if (ofdCustomPaths.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(ofdCustomPaths.FileName, FileMode.Open, FileAccess.Read))
                {
                    AddCustomPath(ref allareas, fs, "/Objects/Entity", "Area/Points/Point");
                }
            }
            else
            {
                return;
            }


            if (allareas.Count <= 0)
                MessageBox.Show("Nothing to show ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();
                if (map.GetPathCount() > 0)
                    if (MessageBox.Show("Keep current paths ?", "Add Custom Path", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPaths();

                foreach (var p in allareas)
                    map.AddPath(p);

                map.FocusAll(false, true, false);
                map.tsbShowPath.Checked = true;
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        private void btnFindDoodadsInZone_Click(object sender, EventArgs e)
        {
            List<MapSpawnLocation> doodadList = new List<MapSpawnLocation>();

            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    long ZoneGroupId = (long)b.Tag;
                    var zg = GetZoneGroupByID(ZoneGroupId);
                    string ZoneGroupFile = string.Empty;
                    if (zg != null)
                        ZoneGroupFile = zg.GamePakZoneDoodadsDat();

                    if (zg != null)
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var map = MapViewForm.GetMap();
                        map.Show();

                        if (map.GetPoICount() > 0)
                            if (MessageBox.Show("Keep current PoI's on map ?", "Add Doodad", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.No)
                                map.ClearPoI();

                        doodadList.AddRange(GetDoodadSpawnsInZoneGroup(zg.id));

                        if (doodadList.Count > 0)
                        {
                            using (var loading = new LoadingForm())
                            {
                                loading.ShowInfo("Loading " + doodadList.Count.ToString() + " Doodads");
                                loading.Show();

                                // Add to NPC list
                                // tcViewer.SelectedTab = tpNPCs;
                                dgvDoodads.Hide();
                                dgvDoodads.Rows.Clear();
                                //Refresh();
                                int c = 0;
                                foreach (var doodad in doodadList)
                                {
                                    if (AADB.DB_Doodad_Almighties.TryGetValue(doodad.id, out var z))
                                    {
                                        var line = dgvDoodads.Rows.Add();
                                        var row = dgvDoodads.Rows[line];

                                        row.Cells[0].Value = z.id.ToString();
                                        row.Cells[1].Value = z.nameLocalized;
                                        row.Cells[2].Value = z.mgmt_spawn.ToString();
                                        row.Cells[3].Value = z.group_id.ToString();
                                        row.Cells[4].Value = z.percent.ToString();
                                        row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                                        row.Cells[6].Value = z.model_kind_id.ToString();
                                        row.Cells[7].Value = z.model.ToString();
                                        if (doodad.count == 1)
                                            row.Cells[8].Value = string.Format("{0} , {1} = ({2})", doodad.x, doodad.y,
                                                doodad.AsSextant());
                                        else
                                            row.Cells[8].Value = doodad.count.ToString() + " instances in zone";

                                        c++;
                                        if ((c % 25) == 0)
                                        {
                                            loading.ShowInfo("Loading " + c.ToString() + "/" +
                                                             doodadList.Count.ToString() +
                                                             " Doodads");
                                        }

                                        map.AddPoI(doodad.x, doodad.y, doodad.z,
                                            z.nameLocalized + " (" + doodad.id.ToString() + ")", Color.Yellow, 0f,
                                            "doodad",
                                            doodad.id, z);
                                    }
                                    else
                                    {
                                        map.AddPoI(doodad.x, doodad.y, doodad.z, "(" + doodad.id.ToString() + ")",
                                            Color.Red, 0f, "doodad", doodad.id, z);
                                    }
                                }

                                tcViewer.SelectedTab = tpDoodads;
                                dgvDoodads.Show();

                            }
                        }

                        map.tsbShowPoI.Checked = true;
                        map.tsbNamesPoI.Checked = false; // Disable this when loading from zone
                        map.FocusAll(true, false, false);
                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void btnLoadCustomAAEmuJson_Click(object sender, EventArgs e)
        {
            List<MapViewPoI> allPoIs = new List<MapViewPoI>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;


            if (ofdJsonData.ShowDialog() == DialogResult.OK)
            {
                var jsonFileName = ofdJsonData.FileName;
                var isDoodads = jsonFileName.ToLower().Contains("doodad");
                var isNPCs = jsonFileName.ToLower().Contains("npc");
                ;
                var isTransfers = jsonFileName.ToLower().Contains("transfer");
                ;
                var contents = File.ReadAllText(jsonFileName);
                if (string.IsNullOrWhiteSpace(contents))
                    MessageBox.Show("File " + jsonFileName + " is empty.");
                else
                {
                    var data = JsonConvert.DeserializeObject<List<JsonNpcSpawns>>(contents);
                    if (data.Count > 0)
                    {
                        try
                        {
                            foreach (var spawner in data)
                            {
                                var ni = new MapViewPoI();
                                ni.Coord = new Vector3(spawner.Position.X, spawner.Position.Y, spawner.Position.Z);
                                ni.PoIColor = Color.LightGray;
                                if (isNPCs && AADB.DB_NPCs.TryGetValue(spawner.UnitId, out var npc))
                                {
                                    ni.Name = npc.nameLocalized;
                                    ni.PoIColor = Color.Yellow;
                                    ni.TypeId = npc.id;
                                    ni.SourceObject = npc;
                                }
                                else if (isDoodads &&
                                         AADB.DB_Doodad_Almighties.TryGetValue(spawner.UnitId, out var doodad))
                                {
                                    ni.Name = doodad.nameLocalized;
                                    ni.PoIColor = Color.DarkGreen;
                                    ni.TypeId = doodad.id;
                                    ni.SourceObject = doodad;
                                }
                                else if (isTransfers && AADB.DB_Transfers.TryGetValue(spawner.UnitId, out var transfer))
                                {
                                    ni.Name = "Model: " + transfer.model_id.ToString();
                                    ni.PoIColor = Color.Navy;
                                    ni.TypeId = transfer.id;
                                    ni.SourceObject = transfer;
                                }
                                else if (isDoodads || isNPCs || isTransfers)
                                {
                                    ni.PoIColor = Color.Red;
                                    ni.Name = spawner.name;
                                    ni.TypeId = spawner.UnitId; // when unknown, use Id
                                }

                                if (isTransfers)
                                    ni.TypeName = "transfer";
                                if (isDoodads)
                                    ni.TypeName = "doodad";
                                if (isNPCs)
                                    ni.TypeName = "npc";

                                ni.Name += " (Id:" + spawner.Id + " - tId:" + spawner.UnitId + ")";
                                if (spawner.UnitId <= 0)
                                    ni.PoIColor = Color.Red;
                                allPoIs.Add(ni);
                            }
                        }
                        catch
                        {

                        }
                    }

                }
            }
            else
            {
                return;
            }


            if (allPoIs.Count <= 0)
                MessageBox.Show("Nothing to show ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();
                if (map.GetPoICount() > 0)
                    if (MessageBox.Show("Keep current PoI ?", "Add Custom Json data", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPoI();

                foreach (var p in allPoIs)
                    map.AddPoI(p.Coord.X, p.Coord.Y, p.Coord.Z, p.Name, p.PoIColor, 0f, p.TypeName, p.TypeId, p);

                map.FocusAll(true, false, false);
                map.tsbShowPoI.Checked = true;

                map.Refresh();

                if (allPoIs.Count > 10000)
                    MessageBox.Show($"Done loading {allPoIs.Count} items");
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        private void AddAreaShapes(string fileName, int cellX, int cellY, ref List<MapViewPath> allAreaShapes)
        {
            var fs = pak.ExportFileAsStream(fileName);

            var _doc = new XmlDocument();
            _doc.Load(fs);
            var _allEntityBlocks = _doc.SelectNodes("/Mission/Objects/Entity");
            var cellPos = new Vector3(cellX * 1024, cellY * 1024, 0);

            for (var i = 0; i < _allEntityBlocks.Count; i++)
            {
                var block = _allEntityBlocks[i];
                var attribs = ReadNodeAttributes(block);
                if (attribs.TryGetValue("entityclass", out var entityClass))
                {
                    if (entityClass == "AreaShape")
                    {

                        var areaName = attribs["name"];
                        var entityId = attribs["entityid"];
                        var posString = attribs["pos"];
                        var areaPos = ReadXmlPos(posString);

                        var mapPath = new MapViewPath();
                        mapPath.PathName = areaName + " (" + entityId + ")";

                        var areaBlock = block.SelectSingleNode("Area");
                        if (areaBlock == null)
                            continue; // this shape has no area defined

                        var areaAttribs = ReadNodeAttributes(areaBlock);
                        var areaHeight = areaAttribs["height"]; // not used in the this viewer

                        var pointBlocks = areaBlock.SelectNodes("Points/Point");

                        var firstPos = Vector3.Zero;
                        for (var p = 0; p < pointBlocks.Count; p++)
                        {
                            var pointAttribs = ReadNodeAttributes(pointBlocks[p]);
                            var pointPos = ReadXmlPos(pointAttribs["pos"]);
                            var pos = cellPos + areaPos + pointPos;
                            mapPath.AddPoint(pos);
                            if (p == 0)
                                firstPos = pos;
                        }

                        if (pointBlocks.Count > 2)
                            mapPath.AddPoint(firstPos);
                        if (areaName.ToLower().Contains("_lake"))
                            mapPath.Color = Color.Aqua;
                        if (areaName.ToLower().Contains("_pond"))
                            mapPath.Color = Color.CornflowerBlue;
                        if (areaName.ToLower().Contains("_water"))
                            mapPath.Color = Color.DarkBlue;
                        if (areaName.ToLower().Contains("_river"))
                            mapPath.Color = Color.Blue;

                        allAreaShapes.Add(mapPath);
                    }
                }
            }
        }

        private void btnShowEntityAreaShape_Click(object sender, EventArgs e)
        {
            if (!pak.IsOpen)
                return;

            var entityFiles = new List<string>();
            var worldName = "main_world";

            var allAreaShapes = new List<MapViewPath>();


            using (var loading = new LoadingForm())
            {
                loading.Show();
                loading.ShowInfo("Searching for cell entities");
                var worldXml = MapViewWorldXML.GetInstanceByName(worldName);

                for (var y = 0; y <= worldXml.CellCount.Y; y++)
                    for (var x = 0; x <= worldXml.CellCount.X; x++)
                    {
                        var cellName = x.ToString().PadLeft(3, '0') + "_" + y.ToString().PadLeft(3, '0');
                        var fn = "game/worlds/" + worldName + "/cells/" + cellName + "/client/entities.xml";
                        if (pak.FileExists(fn))
                        {
                            AddAreaShapes(fn, x, y, ref allAreaShapes);
                        }
                    }


                var map = MapViewForm.GetMap();
                map.Show();
                if (map.GetPathCount() > 0)
                    if (MessageBox.Show("Keep current Paths ?", "Add AreaShapes", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPaths();

                loading.ShowInfo("Adding " + allAreaShapes.Count.ToString() + " areas");

                foreach (var p in allAreaShapes)
                    map.AddPath(p);

                map.FocusAll(false, true, false);
                map.tsbShowPath.Checked = true;
            }

        }

        private void tpSkills_Click(object sender, EventArgs e)
        {

        }
        private string PlotUpdateMethode(long id)
        {
            switch (id)
            {
                case 1: return "OriginalSource (1)";
                case 2: return "OriginalTarget (2)";
                case 3: return "PreviousSource (3)";
                case 4: return "PreviousTarget (4)";
                case 5: return "Area (5)";
                case 6: return "RandomUnit (6)";
                case 7: return "RandomArea (7)";
                default: return id.ToString();
            }
        }


        private void tvSkill_AfterSelect(object sender, TreeViewEventArgs e)
        {

            lPlotEventSourceUpdate.Text = "Source Update: ?";
            lPlotEventTargetUpdate.Text = "Target Update: ?";
            lPlotEventP1.Text = "1: ?";
            lPlotEventP2.Text = "2: ?";
            lPlotEventP3.Text = "3: ?";
            lPlotEventP4.Text = "4: ?";
            lPlotEventP5.Text = "5: ?";
            lPlotEventP6.Text = "6: ?";
            lPlotEventP7.Text = "7: ?";
            lPlotEventP8.Text = "8: ?";
            lPlotEventP9.Text = "9: ?";
            lPlotEventTickets.Text = "Tickets: ?";
            lPlotEventAoE.Text = "AoE: ?";


            if ((e.Node == null) || (e.Node.Tag == null))
                return;
            if (!AADB.DB_Plot_Events.TryGetValue(long.Parse(e.Node.Tag.ToString()), out var plotEvent))
                return;

            lPlotEventSourceUpdate.Text =
                "Source Update Method: " + PlotUpdateMethode(plotEvent.source_update_method_id);
            lPlotEventTargetUpdate.Text =
                "Target Update Method: " + PlotUpdateMethode(plotEvent.target_update_method_id);
            lPlotEventP1.Text = "1: " + plotEvent.target_update_method_param1.ToString();
            lPlotEventP2.Text = "2: " + plotEvent.target_update_method_param2.ToString();
            lPlotEventP3.Text = "3: " + plotEvent.target_update_method_param3.ToString();
            lPlotEventP4.Text = "4: " + plotEvent.target_update_method_param4.ToString();
            lPlotEventP5.Text = "5: " + plotEvent.target_update_method_param5.ToString();
            lPlotEventP6.Text = "6: " + plotEvent.target_update_method_param6.ToString();
            lPlotEventP7.Text = "7: " + plotEvent.target_update_method_param7.ToString();
            lPlotEventP8.Text = "8: " + plotEvent.target_update_method_param8.ToString();
            lPlotEventP9.Text = "9: " + plotEvent.target_update_method_param9.ToString();
            lPlotEventTickets.Text = "Tickets: " + plotEvent.tickets.ToString();
            lPlotEventAoE.Text = "AoE: " + (plotEvent.aeo_diminishing ? "Diminishing" : "Normal");

            ShowSelectedData("plot_events", "id == " + plotEvent.id.ToString(), "id");
        }

        private void label137_Click(object sender, EventArgs e)
        {

        }

        private void lbTradeSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTradeDestination.Items.Clear();
            if (!(lbTradeSource.SelectedItem is GameZone_Groups sourceZone))
                return;

            foreach (var z in AADB.DB_Specialities)
            {
                if ((z.Value.vendor_exist) && (z.Value.row_zone_group_id == sourceZone.id))
                {
                    if (AADB.DB_Zone_Groups.TryGetValue(z.Value.col_zone_group_id, out var destZone))
                        lbTradeDestination.Items.Add(destZone);
                }
            }

        }

        private void lbTradeDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lbTradeSource.SelectedItem is GameZone_Groups sourceZone))
                return;
            if (!(lbTradeDestination.SelectedItem is GameZone_Groups destZone))
                return;

            lTradeRoute.Text = "-";
            foreach (var z in AADB.DB_Specialities)
            {
                if ((z.Value.vendor_exist) && (z.Value.row_zone_group_id == sourceZone.id) &&
                    (z.Value.col_zone_group_id == destZone.id))
                {
                    lTradeRoute.Text = sourceZone.ToString() + " => " + destZone.ToString();
                    lTradeProfit.Text = z.Value.profit.ToString();
                    lTradeRatio.Text = z.Value.ratio.ToString();
                }
            }

        }

        private void btnExportDoodadSpawnData_Click(object sender, EventArgs e)
        {
            PrepareWorldXML(false);

            foreach (var inst in MapViewWorldXML.instances)
            {
                var DoodadList = new List<DoodadExportData>();
                var i = 0;

                var zoneGroups = new List<long>();
                foreach (var z in inst.zones)
                {
                    var zone = AADB.GetZoneByKey(z.Value.zone_key);
                    if (zone != null)
                        if (!zoneGroups.Contains(zone.group_id))
                            zoneGroups.Add(zone.group_id);
                }

                foreach (var zoneGroupId in zoneGroups)
                {
                    if (!AADB.DB_Zone_Groups.TryGetValue(zoneGroupId, out var zoneGroup))
                        continue;

                    var npcs = GetDoodadSpawnsInZoneGroup(zoneGroup.id, false, -1, inst.WorldName);
                    foreach (var npc in npcs)
                    {
                        i++;
                        var newDoodad = new DoodadExportData();
                        newDoodad.Id = i;
                        newDoodad.UnitId = npc.id;
                        newDoodad.Position.X = npc.x;
                        newDoodad.Position.Y = npc.y;
                        newDoodad.Position.Z = npc.z;
                        if (AADB.DB_Doodad_Almighties.TryGetValue(npc.id, out var vDoodad))
                            newDoodad.Title = vDoodad.nameLocalized;
                        newDoodad.FuncGroupId = 0;
                        newDoodad.Zone = npc.zoneGroupId;

                        DoodadList.Add(newDoodad);
                    }
                }

                string json = JsonConvert.SerializeObject(DoodadList.ToArray(), Newtonsoft.Json.Formatting.Indented);
                Directory.CreateDirectory(Path.Combine("export", "Data", "Worlds", inst.WorldName));
                File.WriteAllText(Path.Combine("export", "Data", "Worlds", inst.WorldName, "doodad_spawns.json"), json);

            }

            MessageBox.Show("Done");
        }

        private void cbItemSearchFilterChanged(object sender, EventArgs e)
        {
            if (cbItemSearch.Text.Replace("*", "") == string.Empty)
            {

                if ((cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0) ||
                    (cbItemSearchItemCategoryTypeList.SelectedIndex > 0) ||
                    (cbItemSearchRange.SelectedIndex > 0) ||
                    (cbItemSearchType.SelectedIndex > 0))
                    cbItemSearch.Text = "*";
                else
                    cbItemSearch.Text = string.Empty;
            }
        }

        private void ProcessNodeInfoDoubleClick(TreeNode node)
        {
            CopyToClipBoard(node.Text);
            if (node is TreeNodeWithInfo info)
            {
                if (info.targetTabPage != null)
                    tcViewer.SelectedTab = info.targetTabPage;
                if (info.targetTextBox != null)
                    info.targetTextBox.Text = info.targetSearchText;
                if (info.targetSearchBox != null)
                    info.targetSearchBox.Text = info.targetSearchText;
                if (info.targetSearchButton != null)
                {
                    info.targetSearchButton.Enabled = true;
                    info.targetSearchButton.PerformClick();
                }
            }
        }

        private void tvQuestWorkflow_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void tvDoodadDetails_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void tvSkill_DoubleClick(object sender, EventArgs e)
        {
            // In properties the node tag is used internally, so only allow this node double-click if it's not set
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void tvBuffTriggers_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void ShowDBTag(long tag)
        {
            tvTagInfo.Nodes.Clear();
            if (tag <= 0)
                return;

            ShowSelectedData("tags", "id = " + tag.ToString(), "id");

            TreeNode groupNode = null;

            groupNode = null;
            var buffs = from i in AADB.DB_Tagged_Buffs.Values
                        where i.tag_id == tag
                        select i;
            foreach (var i in buffs)
            {
                if (groupNode == null)
                    groupNode = tvTagInfo.Nodes.Add("Buffs");
                AddCustomPropertyNode("buff_id", i.target_id.ToString(), false, groupNode);
            }

            groupNode = null;
            var items = from i in AADB.DB_Tagged_Items.Values
                        where i.tag_id == tag
                        select i;
            foreach (var i in items)
            {
                if (groupNode == null)
                    groupNode = tvTagInfo.Nodes.Add("Items");
                AddCustomPropertyNode("item_id", i.target_id.ToString(), false, groupNode);
            }

            groupNode = null;
            var npcs = from i in AADB.DB_Tagged_NPCs.Values
                       where i.tag_id == tag
                       select i;
            foreach (var i in npcs)
            {
                if (groupNode == null)
                    groupNode = tvTagInfo.Nodes.Add("NPCs");
                AddCustomPropertyNode("npc_id", i.target_id.ToString(), false, groupNode);
            }

            groupNode = null;
            var skills = from i in AADB.DB_Tagged_Skills.Values
                         where i.tag_id == tag
                         select i;
            foreach (var i in skills)
            {
                if (groupNode == null)
                    groupNode = tvTagInfo.Nodes.Add("Skills");
                AddCustomPropertyNode("skill_id", i.target_id.ToString(), false, groupNode);
            }

            groupNode = null;
            var zones = from i in AADB.DB_Zone_Group_Banned_Tags.Values
                        where i.tag_id == tag
                        select i;
            foreach (var i in zones)
            {
                if (groupNode == null)
                    groupNode = tvTagInfo.Nodes.Add("Zone Groups (banned tags)");
                AddCustomPropertyNode("zone_group_id", i.zone_group_id.ToString(), false, groupNode);
            }

            // expand if only one group is showing
            if (tvTagInfo.Nodes.Count == 1)
                tvTagInfo.ExpandAll();
        }

        private void btnSearchTags_Click(object sender, EventArgs e)
        {
            string searchText = tSearchTags.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvTags.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Tags)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvTags.Rows.Add();
                    var row = dgvTags.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;

                    if (c == 0)
                    {
                        ShowDBTag(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " items, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void tSearchTags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchTags_Click(null, null);
            }
        }

        private void dgvTags_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTags.SelectedRows.Count <= 0)
                return;
            var row = dgvTags.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;
            ShowDBTag(long.Parse(val.ToString()));
        }

        private void tSearchTags_TextChanged(object sender, EventArgs e)
        {
            btnSearchTags.Enabled = (tSearchTags.Text != "");
        }

        private void tvTagInfo_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void cbBuffsHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (long.TryParse(lBuffId.Text, out var id))
                ShowDBBuff(id);
        }

        private void cbDoodadWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (long.TryParse(lDoodadID.Text, out var id))
                ShowDBDoodad(id);
        }

        private void cbQuestWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if ((dgvQuests.CurrentRow != null) && (dgvQuests.CurrentRow.Cells.Count > 0))
            {
                if (long.TryParse(dgvQuests.CurrentRow.Cells[0].Value.ToString(), out var id))
                    ShowDBQuest(id);
            }
        }

        private void cbNewGM_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NewGMCommands = cbNewGM.Checked;
        }

        private string TreeViewToString(TreeNodeCollection treeView, int depth = 0)
        {
            var res = string.Empty;
            foreach (TreeNode node in treeView)
            {
                var spacer = string.Empty.PadRight(depth * 2, ' ');
                if (depth > 0)
                    spacer += (depth % 2 == 0) ? "- " : "* ";
                res += spacer + $"{node.Text}\r\n";
                res += TreeViewToString(node.Nodes, depth + 1);
            }
            return res;
        }

        private void btnCopySkillExecutionTree_Click(object sender, EventArgs e)
        {
            CopyToClipBoard($"Skill: {lSkillID.Text} - " + TreeViewToString(tvSkill.Nodes, 0));
        }

        private void btnSkillTreeCollapse_Click(object sender, EventArgs e)
        {
            tvSkill.CollapseAll();
            foreach (TreeNode node in tvSkill.Nodes)
            {
                node.Expand();
            }
        }

        private void btnShowNpcLoot_Click(object sender, EventArgs e)
        {
            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            var packs = AADB.DB_Loot_Pack_Dropping_Npc.Where(lp => lp.Value.npc_id == searchId).ToList();

            var nonDefaultPackCount = packs.Count(p => p.Value.default_pack == false);

            for (var c = 0; c < packs.Count; c++)
                ShowDBLootByID(
                    packs[c].Value.loot_pack_id,
                    (c == 0),
                    (c % 2) != 0,
                    packs[c].Value.default_pack,
                    nonDefaultPackCount);
            if (packs.Count > 0)
                tcViewer.SelectedTab = tpLoot;
        }

        private void btnFindLootNpc_Click(object sender, EventArgs e)
        {
            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            var packs = AADB.DB_Loot_Pack_Dropping_Npc.Where(lp => lp.Value.loot_pack_id == searchId).ToList();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvNPCs.Rows.Clear();
            int c = 0;
            foreach (var t in packs)
            {
                if (!AADB.DB_NPCs.TryGetValue(t.Value.npc_id, out var z))
                    continue;

                var line = dgvNPCs.Rows.Add();
                var row = dgvNPCs.Rows[line];

                row.Cells[0].Value = z.id.ToString();
                row.Cells[1].Value = z.nameLocalized;
                row.Cells[2].Value = z.level.ToString();
                row.Cells[3].Value = z.npc_kind_id.ToString();
                row.Cells[4].Value = z.npc_grade_id.ToString();
                row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                row.Cells[6].Value = "???";

                if (c == 0)
                {
                    ShowDBNPCInfo(z.id);
                }

                c++;
                if (c >= 250)
                {
                    MessageBox.Show(
                        "The results were cut off at " + c.ToString() + " items, please refine your search !",
                        "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

            if (packs.Count > 0)
                tcViewer.SelectedTab = tpNPCs;
            else
                MessageBox.Show("No NPCs found");
        }

        private void btnLoadAAEmuWater_Click(object sender, EventArgs e)
        {
            List<MapViewPath> allPaths = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;


            if (ofdJsonData.ShowDialog() == DialogResult.OK)
            {
                var jsonFileName = ofdJsonData.FileName;
                WaterBodies.Load(jsonFileName, out var water);
                if (water.Areas.Count > 0)
                {
                    try
                    {
                        foreach (var w in water.Areas)
                        {
                            var mvp = new MapViewPath();
                            mvp.PathName = $"{w.Name} ({w.Id})";
                            if (w.AreaType == WaterBodyAreaType.Polygon)
                            {
                                mvp.Color = Color.Blue;
                            }
                            if (w.AreaType == WaterBodyAreaType.LineArray)
                            {
                                if (allPaths.Count % 2 == 0)
                                    mvp.Color = Color.Cyan;
                                else
                                    mvp.Color = Color.LightCyan;
                            }

                            mvp.allpoints.AddRange(w.Points);

                            if ((w.AreaType == WaterBodyAreaType.LineArray) && (mvp.allpoints.Count > 2) && (mvp.allpoints[^1].Equals(mvp.allpoints[0])))
                                mvp.allpoints.RemoveAt(mvp.allpoints.Count - 1);
                            allPaths.Add(mvp);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("No water data found !");
                    return;
                }
            }
            else
            {
                return;
            }

            if (allPaths.Count <= 0)
                MessageBox.Show("Nothing to show ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();
                if (map.GetPathCount() > 0)
                    if (MessageBox.Show("Keep current Paths ?", "Add Json water data", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        map.ClearPaths();

                foreach (var p in allPaths)
                    map.AddPath(p);

                map.FocusAll(false, true, false);
                map.tsbShowPath.Checked = true;

                map.Refresh();
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void btnSearchSlave_Click(object sender, EventArgs e)
        {
            string searchText = tSearchSlave.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvSlaves.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Slaves)
            {
                var z = t.Value;
                var modelDetails = string.Empty;
                if (AADB.DB_Models.TryGetValue(z.model_id, out var model))
                    modelDetails = model.sub_type + " " + model.sub_id.ToString() + " - " + model.name;

                if (
                    (z.id == searchID) ||
                    (z.model_id == searchID) ||
                    (z.searchText.IndexOf(searchText) >= 0) ||
                    (modelDetails.IndexOf(searchText) >= 0)
                    )
                {
                    var line = dgvSlaves.Rows.Add();
                    var row = dgvSlaves.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.model_id.ToString();
                    row.Cells[3].Value = z.level.ToString();
                    row.Cells[4].Value = AADB.GetFactionName(z.faction_id, true);
                    row.Cells[5].Value = modelDetails;

                    if (c == 0)
                    {
                        ShowDBSlaveInfo(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " items, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void tSearchSlave_TextChanged(object sender, EventArgs e)
        {
            btnSearchSlave.Enabled = !string.IsNullOrEmpty(tSearchSlave.Text);
        }

        private void tSearchSlave_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchSlave.Enabled))
                btnSearchSlave_Click(null, null);
        }

        private void ShowDBSlaveInfo(long id)
        {
            if (AADB.DB_Slaves.TryGetValue(id, out var slave))
            {
                lSlaveTemplate.Text = slave.id.ToString();
                lSlaveName.Text = slave.nameLocalized;
                ShowSelectedData("slaves", "(id = " + id.ToString() + ")", "id ASC");

                tvSlaveInfo.Nodes.Clear();

                // Passive Buffs
                var slavePassiveBuff = AADB.DB_Slave_Passive_Buffs.Values.FirstOrDefault(x => x.owner_type == "Slave" && x.owner_id == slave.id);
                if ((slavePassiveBuff != null) && AADB.DB_Passive_Buffs.TryGetValue(slavePassiveBuff.passive_buff_id, out var passiveBuff))
                {
                    var passiveNode = tvSlaveInfo.Nodes.Add("Passive Buffs");
                    AddCustomPropertyNode("buff_id", passiveBuff.buff_id.ToString(), false, passiveNode);
                }

                // Initial Buffs
                var slaveInitialBuffs = AADB.DB_Slave_Initial_Buffs.Values.Where(x => x.slave_id == slave.id).ToList();
                if (slaveInitialBuffs.Count > 0)
                {
                    var initialNode = tvSlaveInfo.Nodes.Add("Initial Buffs");
                    foreach (var initialBuff in slaveInitialBuffs)
                    {
                        AddCustomPropertyNode("buff_id", initialBuff.buff_id.ToString(), false, initialNode);
                    }
                }

                // Skills
                var slaveMountSkills = AADB.DB_Slave_Mount_Skills.Values.Where(x => x.slave_id == slave.id).ToList();
                if (slaveMountSkills.Count > 0)
                {
                    var skillNode = tvSlaveInfo.Nodes.Add("Skills");
                    foreach (var slaveMountSkill in slaveMountSkills)
                    {
                        if (!AADB.DB_Mount_Skills.TryGetValue(slaveMountSkill.mount_skill_id, out var mountSkill))
                            continue;
                        AddCustomPropertyNode("skill_id", mountSkill.skill_id.ToString(), false, skillNode);
                    }       
                }

                // Bindings
                var slaveBindings = AADB.DB_Slave_Bindings.Values.Where(x => x.owner_type == "Slave" && x.owner_id == slave.id).ToList();
                var slaveDoodadBindings = AADB.DB_Slave_Doodad_Bindings.Values.Where(x => x.owner_type == "Slave" && x.owner_id == slave.id).ToList();
                if ((slaveBindings.Count > 0) || (slaveDoodadBindings.Count > 0))
                {
                    var bindingNode = tvSlaveInfo.Nodes.Add("Bindings");
                    foreach (var slaveBinding in slaveBindings)
                    {
                        var n = AddCustomPropertyNode("slave_id", slaveBinding.slave_id.ToString(), false, bindingNode);
                        n.Text = $" @{slaveBinding.attach_point_id}: {n.Text}";
                    }
                    foreach (var slaveDoodadBinding in slaveDoodadBindings)
                    {
                        var n = AddCustomPropertyNode("doodad_id", slaveDoodadBinding.doodad_id.ToString(), false, bindingNode);
                        n.Text = $" @{slaveDoodadBinding.attach_point_id}: {n.Text}";
                    }
                }

                tvSlaveInfo.ExpandAll();

            }
            else
            {
                lSlaveTemplate.Text = "???";
                lSlaveName.Text = "???";
                tvSlaveInfo.Nodes.Clear();
            }
        }

        private void tvNPCInfo_DoubleClick(object sender, EventArgs e)
        {
            // In properties the node tag is used internally, so only allow this node double-click if it's not set
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void AddToSearchHistory(ComboBox searchBox, string searchString)
        {
            var oldText = searchBox.Text;
            var s = searchString.ToLower();
            for (int i = searchBox.Items.Count - 1; i >= 0; i--)
            {
                string item = (string)searchBox.Items[i];
                if (item.ToLower() == s)
                    searchBox.Items.RemoveAt(i);
            }
            searchBox.Items.Insert(0, oldText);

            // Put a artificial limit
            while (searchBox.Items.Count > 50)
                searchBox.Items.RemoveAt(searchBox.Items.Count - 1);

            searchBox.Text = oldText;
        }

        private void LoadHistory(string savedHistoryString, ComboBox comboBox)
        {
            var lines = savedHistoryString.Split(Environment.NewLine);
            comboBox.Items.Clear();
            foreach (var line in lines)
                if (!string.IsNullOrWhiteSpace(line))
                    comboBox.Items.Add(line);
        }

        private string CreateHistory(ComboBox comboBox)
        {
            var res = new List<string>();
            foreach (string line in comboBox.Items)
                if (!string.IsNullOrWhiteSpace(line))
                    res.Add(line);
            return string.Join(Environment.NewLine, res);
        }

        private void dgvSlaves_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSlaves.SelectedRows.Count <= 0)
                return;
            var row = dgvSlaves.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            var qid = long.Parse(val.ToString());
            ShowDBSlaveInfo(qid);
        }

        private void tvSlaveInfo_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }
    }
}

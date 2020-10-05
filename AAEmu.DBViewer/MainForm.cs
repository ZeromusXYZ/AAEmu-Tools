using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AAPakEditor;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;
using AAEmu.ClipboardHelper;
using FreeImageAPI;
using System.Threading;
using System.Globalization;
using System.Xml;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        public static MainForm ThisForm;
        private string defaultTitle;
        public AAPak pak = new AAPak("");
        private List<string> possibleLanguageIDs = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            ThisForm = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
                    if (pak.OpenPak(game_pakFileName, true))
                    {
                        Properties.Settings.Default.GamePakFileName = game_pakFileName;
                        lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                        loading.ShowInfo("Loading: World Data");
                        PrepareWorldXML(true);
                    }
                }
            }
            tcViewer.SelectedTab = tpItems;
            AttachEventsToLabels();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        private void CopyToClipBoard(string cliptext)
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
                        lbTableNames.Items.Clear();
                        while (reader.Read())
                        {
                            lbTableNames.Items.Add(GetString(reader, "name"));
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
            AddCustomTranslation("doodad_groups", "name", 63, "[Housing Area Marker - Private Pumpkin Scarecrow Garden]");
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

        private void LoadTranslations(string lng)
        {
            string sql = "SELECT * FROM localized_texts ORDER BY tbl_name, tbl_column_name, idx";

            List<string> columnNames = null;

            using (var connection = SQLite.CreateConnection())
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
                                        MessageBox.Show("The selected language \"" + lng + "\" was not found in localized_texts !\r\n" +
                                            "Reverted to English",
                                            "Language not found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lng = "en_us";
                                    }
                                    else
                                    if (columnNames.IndexOf("ko") >= 0)
                                    {
                                        MessageBox.Show("The selected language \"" + lng + "\" was not found in localized_texts !\r\n" +
                                            "Reverted to Korean",
                                            "Language not found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lng = "ko";
                                    }
                                    else
                                    {
                                        MessageBox.Show("The selected language \"" + lng + "\" was not found in localized_texts !\r\n" +
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
            if (columnNames != null)
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
                finally
                {
                    cbItemSearchLanguage.Enabled = true;
                }
            }
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
                                t.display_textLocalized = GetTranslationByID(t.id, "zones", "display_text", t.display_text);
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
                                t.display_textLocalized = GetTranslationByID(t.id, "zone_groups", "display_text", t.display_text);
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

                            t.nameLocalized = GetTranslationByID(t.id, "item_categories", "name", t.name);

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

                            t.nameLocalized = GetTranslationByID(t.id, "items", "name", t.name);
                            t.descriptionLocalized = GetTranslationByID(t.id, "items", "description", t.description);

                            t.SearchString = t.name + " " + t.description + " " + t.nameLocalized + " " + t.descriptionLocalized;
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
            string sql = "SELECT * FROM skills ORDER BY id ASC";

            List<string> columnNames = null;
            bool readWebDesc = false;

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Skills.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        if (columnNames == null)
                        {
                            columnNames = reader.GetColumnNames();
                            if (columnNames.IndexOf("web_desc") >= 0)
                                readWebDesc = true;
                        }

                        while (reader.Read())
                        {
                            GameSkills t = new GameSkills();
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
                            t.default_gcd = GetBool(reader, "default_gcd"); ;
                            t.custom_gcd = GetInt64(reader, "custom_gcd");
                            t.first_reagent_only = GetBool(reader, "first_reagent_only");

                            t.nameLocalized = GetTranslationByID(t.id, "skills", "name", t.name);
                            t.descriptionLocalized = GetTranslationByID(t.id, "skills", "desc", t.desc);
                            if (readWebDesc)
                                t.webDescriptionLocalized = GetTranslationByID(t.id, "skills", "web_desc", t.web_desc);
                            else
                                t.webDescriptionLocalized = string.Empty;

                            t.SearchString = t.name + " " + t.desc + " " + t.nameLocalized + " " + t.descriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Skills.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

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
                        bool hasComments = ((columnNames.IndexOf("comment1") > 0) && (columnNames.IndexOf("comment2") > 0) && (columnNames.IndexOf("comment3") > 0) && (columnNames.IndexOf("comment_wear") > 0));
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
                            t.aggro_link_special_ignore_npc_attacker = GetBool(reader, "aggro_link_special_ignore_npc_attacker");
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


                            t.nameLocalized = GetTranslationByID(t.id, "npcs", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_NPCs.Add(t.id, t);
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

                            t.nameLocalized = GetTranslationByID(t.id, "system_factions", "name", t.name);
                            // Actuall not even sure if owner_name can be localized, also not sure yet what owner_id points to
                            if (t.owner_name != string.Empty)
                                t.owner_nameLocalized = GetTranslationByID(t.id, "system_factions", "name", t.owner_name);
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
                            t.nameLocalized = GetTranslationByID(t.id, "doodad_almighties", "name", t.name);
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
                            t.nameLocalized = GetTranslationByID(t.id, "doodad_groups", "name", t.name);
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
                                t.nameLocalized = GetTranslationByID(t.id, "doodad_func_groups", "name");
                            else
                                t.nameLocalized = "";
                            if (t.phase_msgLocalized != string.Empty)
                                t.phase_msgLocalized = GetTranslationByID(t.id, "doodad_func_groups", "phase_msg");
                            else
                                t.phase_msgLocalized = "";
                            t.SearchString = t.name + " " + t.phase_msg + " " + t.nameLocalized + " " + t.phase_msgLocalized + " " + t.comment;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Doodad_Func_Groups.Add(t.id, t);
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

                            t.nameLocalized = GetTranslationByID(t.id, "quest_categories", "name", t.name);

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


                            t.nameLocalized = GetTranslationByID(t.id, "quest_contexts", "name", t.name);

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

                    command.CommandText = "SELECT * FROM quest_components ORDER BY quest_context_id ASC, component_kind_id ASC, id ASC";
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

                            t.nameLocalized = GetTranslationByID(t.id, "tags", "name", t.name);
                            t.descLocalized = GetTranslationByID(t.id, "tags", "desc", t.desc);

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
                            GameTaggedValues t = new GameTaggedValues();
                            t.id = GetInt64(reader, "id");
                            t.tag_id = GetInt64(reader, "tag_id");
                            t.target_id = GetInt64(reader, "buff_id");
                            AADB.DB_Tagged_Buffs.Add(t.id, t);
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
                            GameTaggedValues t = new GameTaggedValues();
                            t.id = GetInt64(reader, "id");
                            t.tag_id = GetInt64(reader, "tag_id");
                            t.target_id = GetInt64(reader, "item_id");
                            AADB.DB_Tagged_Items.Add(t.id, t);
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
                            GameTaggedValues t = new GameTaggedValues();
                            t.id = GetInt64(reader, "id");
                            t.tag_id = GetInt64(reader, "tag_id");
                            t.target_id = GetInt64(reader, "npc_id");
                            AADB.DB_Tagged_NPCs.Add(t.id, t);
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
                            GameTaggedValues t = new GameTaggedValues();
                            t.id = GetInt64(reader, "id");
                            t.tag_id = GetInt64(reader, "tag_id");
                            t.target_id = GetInt64(reader, "skill_id");
                            AADB.DB_Tagged_Skills.Add(t.id, t);
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

                        while (reader.Read())
                        {
                            GameZoneGroupBannedTags t = new GameZoneGroupBannedTags();
                            t.id = GetInt64(reader, "id");
                            t.zone_group_id = GetInt64(reader, "zone_group_id");
                            t.tag_id = GetInt64(reader, "tag_id");
                            t.banned_periods_id = GetInt64(reader, "banned_periods_id");
                            t.usage = GetString(reader, "usage");
                            AADB.DB_Zone_Group_Banned_Tags.Add(t.id, t);
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

            using (var connection = SQLite.CreateConnection())
            {
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

                            t.nameLocalized = GetTranslationByID(t.id, "buffs", "name", t.name);
                            t.descLocalized = GetTranslationByID(t.id, "buffs", "desc", t.desc);

                            t.SearchString = t.name + " " + t.nameLocalized + " " + t.desc + " " + t.descLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            // Read remaining data
                            foreach (var c in cols)
                            {
                                if (!reader.IsDBNull(c))
                                {
                                    var v = reader.GetString(c, string.Empty);
                                    var isnumber = double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out var dVal);
                                    if (isnumber)
                                    {
                                        if (dVal != 0f)
                                            t._others.Add(c, v);
                                    }
                                    else
                                    if ((v != string.Empty) && (v != "0") && (v != "f") && (v != "NULL") && (v != "--- :null"))
                                    {
                                        t._others.Add(c, v);
                                    }
                                }
                            }

                            AADB.DB_Buffs.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
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

        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            dgvItem.Rows.Clear();
            string searchText = tItemSearch.Text;
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
            foreach (var item in AADB.DB_Items)
            {
                bool addThis = false;
                if (SearchByID)
                {
                    if (item.Key == searchID)
                    {
                        addThis = true;
                    }
                }
                else
                if (item.Value.SearchString.IndexOf(searchTextLower) >= 0)
                    addThis = true;

                // Hardcode * as add all if armor slot is provided
                if ((searchTextLower == "*") && ((cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0) || (cbItemSearchItemCategoryTypeList.SelectedIndex > 0)))
                    addThis = true;

                if (addThis && (cbItemSearchItemCategoryTypeList.SelectedIndex > 0))
                {
                    if (long.TryParse(cbItemSearchItemCategoryTypeList.SelectedValue.ToString(), out var cId))
                        if (cId != item.Value.catgegory_id)
                            addThis = false;
                }

                if (addThis && (cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0))
                {
                    if ((item.Value.item_armors == null) || (cbItemSearchItemArmorSlotTypeList.SelectedIndex != item.Value.item_armors.slot_type_id))
                        addThis = false;
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
                }
            }
            dgvItem.Visible = true;
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

            if (firstResult >= 0)
                ShowDBItem(firstResult);

        }

        private string GetTranslationByID(long idx, string table, string field, string defaultValue = "$NODEFAULT")
        {
            string res = string.Empty;
            string k = table + ":" + field + ":" + idx.ToString();
            if (AADB.DB_Translations.TryGetValue(k, out GameTranslation val))
                res = val.value;
            // If no translation found ...
            if (res == string.Empty)
            {
                if (defaultValue == "$NODEFAULT")
                    return "<NT:" + table + ":" + field + ":" + idx.ToString() + ">";
                else
                    return defaultValue;
            }
            else
            {
                return res;
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
            else
            if (hexARGB.Length == 6)
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

        private void FormattedTextToRichtEdit(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
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
                            if ((fieldNameStr == "ITEM_NAME") && (AADB.DB_Items.TryGetValue(itemVal, out GameItem item)))
                            {
                                rt.AppendText(item.nameLocalized);
                            }
                            else
                            if ((fieldNameStr == "NPC_NAME") && (AADB.DB_NPCs.TryGetValue(itemVal, out GameNPC npc)))
                            {
                                rt.AppendText(npc.nameLocalized);
                            }
                            else
                            if ((fieldNameStr == "QUEST_NAME") && (AADB.DB_Quest_Contexts.TryGetValue(itemVal, out GameQuestContexts quest)))
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
                else
                if ((colStartPos >= 0) && (restText.Length >= colStartPos + 10))
                {
                    var colText = restText.Substring(colStartPos + 2, 8);
                    rt.AppendText(restText.Substring(0, colStartPos));
                    var newCol = HexStringToARGBColor(colText);
                    rt.SelectionColor = newCol;
                    restText = restText.Substring(colStartPos + 10);
                }
                else
                if ((colResetPos >= 0) && (restText.Length >= colResetPos + 2))
                {
                    rt.AppendText(restText.Substring(0, colResetPos));
                    rt.SelectionColor = rt.ForeColor;
                    restText = restText.Substring(colResetPos + 2);
                }
                else
                if (atStart >= 0)
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

        private void IconIDToLabel(long icon_id, Label iconImgLabel)
        {

            if (pak.isOpen)
            {
                if (AADB.DB_Icons.TryGetValue(icon_id, out var iconname))
                {
                    var fn = "game/ui/icon/" + iconname;

                    if (pak.FileExists(fn))
                    {
                        try
                        {
                            var fStream = pak.ExportFileAsStream(fn);
                            var fif = FREE_IMAGE_FORMAT.FIF_DDS;
                            FIBITMAP fiBitmap = FreeImage.LoadFromStream(fStream, ref fif);
                            var bmp = FreeImage.GetBitmap(fiBitmap);
                            iconImgLabel.Image = bmp;

                            iconImgLabel.Text = "";
                            // itemIcon.Text = "[" + iconname + "]";
                        }
                        catch
                        {
                            iconImgLabel.Image = null;
                            iconImgLabel.Text = "ERROR - " + iconname;
                        }
                    }
                    else
                    {
                        iconImgLabel.Image = null;
                        iconImgLabel.Text = "NOT FOUND - " + iconname + " ?";
                    }
                }
                else
                {
                    iconImgLabel.Image = null;
                    iconImgLabel.Text = icon_id.ToString() + "?";
                }
            }
            else
            {
                iconImgLabel.Image = null;
                iconImgLabel.Text = icon_id.ToString();
            }

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
            else
            if ((ss > 0) || (ms > 0))
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
            else
            if (Math.Abs(range) < 1000.0f)
            {
                return (range / 10).ToString("0") + " cm";
            }
            else
            if (Math.Abs(range) < 15000.0f)
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
                lItemName.Text = item.nameLocalized;
                lItemCategory.Text = GetTranslationByID(item.catgegory_id, "item_categories", "name") + " (" + item.catgegory_id.ToString() + ")";
                FormattedTextToRichtEdit(item.descriptionLocalized, rtItemDesc);
                lItemLevel.Text = item.level.ToString();
                IconIDToLabel(item.icon_id, itemIcon);
                btnFindItemSkill.Enabled = true;// (item.use_skill_id > 0);
                var gmadditem = "/additem " + item.id.ToString();
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
                lItemName.Text = "<not found>";
                lItemCategory.Text = "";
                rtItemDesc.Clear();
                lItemLevel.Text = "";
                itemIcon.Text = "???";
                btnFindItemSkill.Enabled = false;
                lItemTags.Text = "???";
                lItemAddGMCommand.Text = "/additem ???";
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
                else
                if ((!skill.default_gcd) && (!skill.ignore_global_cooldown))
                {
                    lSkillGCD.Text = MSToString(skill.custom_gcd);
                }
                else
                if ((!skill.default_gcd) && (skill.ignore_global_cooldown))
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
                lZoneClimateID.Text = zone.zone_climate_id.ToString();
                lZoneABoxShow.Text = zone.abox_show.ToString();
                btnFindQuestsInZone.Tag = zone.id;
                btnFindQuestsInZone.Enabled = (zone.id > 0);
                btnFindTransferPathsInZone.Tag = zone.zone_key;

                var zg = GetZoneGroupByID(zone.group_id);
                // From Zone_Groups
                if (zg != null)
                {
                    lZoneGroupsDisplayName.Text = zg.display_textLocalized;
                    lZoneGroupsName.Text = zg.name;
                    string zonefile = zg.GamePakZoneNPCsDat;
                    if ((pak.isOpen) && (pak.FileExists(zonefile)))
                    {
                        btnFindNPCsInZone.Tag = zg.id;
                        btnFindNPCsInZone.Enabled = true;
                    }
                    else
                    {
                        btnFindNPCsInZone.Tag = null;
                        btnFindNPCsInZone.Enabled = false;
                    }

                    lZoneGroupsSizePos.Text = "X:" + zg.PosAndSize.X.ToString("0.0") + " Y:" + zg.PosAndSize.Y.ToString("0.0") + "  W:" + zg.PosAndSize.Width.ToString("0.0") + " H:" + zg.PosAndSize.Height.ToString("0.0");
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
                        lWorldGroupSizeAndPos.Text = "X:" + wg.PosAndSize.X.ToString() + " Y:" + wg.PosAndSize.Y.ToString() + "  W:" + wg.PosAndSize.Width.ToString() + " H:" + wg.PosAndSize.Height.ToString();
                        lWorldGroupImageSizeAndPos.Text = "X:" + wg.Image_PosAndSize.X.ToString() + " Y:" + wg.Image_PosAndSize.Y.ToString() + "  W:" + wg.Image_PosAndSize.Width.ToString() + " H:" + wg.Image_PosAndSize.Height.ToString();
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
                lZoneClimateID.Text = "";
                lZoneABoxShow.Text = "";
                btnFindTransferPathsInZone.Tag = 0;

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


        private string VisualizeDropRate(long droprate)
        {
            if (droprate == 1)
                return "Special";
            return droprate.ToString();
            /*
            double d = droprate / 100000;
            return d.ToString("0.00") + " %";
            */
        }

        private void ShowDBLootByItem(long idx)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loots WHERE (item_id = @tidx) ORDER BY id ASC";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tidx", idx.ToString());
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        dgvLoot.Rows.Clear();
                        while (reader.Read())
                        {
                            if (GetInt64(reader, "item_id") == idx)
                            {
                                int line = dgvLoot.Rows.Add();
                                var row = dgvLoot.Rows[line];

                                row.Cells[0].Value = GetInt64(reader, "id").ToString();
                                row.Cells[1].Value = GetInt64(reader, "loot_pack_id").ToString();
                                row.Cells[2].Value = idx.ToString();
                                row.Cells[3].Value = GetTranslationByID(idx, "items", "name");
                                row.Cells[4].Value = VisualizeDropRate(GetInt64(reader, "drop_rate"));
                                row.Cells[5].Value = GetInt64(reader, "min_amount").ToString();
                                row.Cells[6].Value = GetInt64(reader, "max_amount").ToString();
                                row.Cells[7].Value = GetInt64(reader, "grade_id").ToString();
                                row.Cells[8].Value = GetString(reader, "always_drop").ToString();
                                row.Cells[9].Value = GetInt64(reader, "group").ToString();
                            }
                        }
                    }
                }
            }
        }

        private void ShowLootGroupData(long loots_pack_id, long loots_group)
        {
            LLootGroupPackID.Text = loots_pack_id.ToString();
            LLootPackGroupNumber.Text = loots_group.ToString();
        }

        private void ShowDBLootByID(long loot_id)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    // command.CommandText = "SELECT * FROM loots as t1 LEFT JOIN loot_groups as t2 ON t1.loot_pack_id = t2.pack_id AND [t1].[group] = t2.group_no WHERE (t1.loot_pack_id = @tpackid) ORDER BY id ASC";
                    command.CommandText = "SELECT * FROM loots WHERE (loot_pack_id = @tpackid) ORDER BY id ASC";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tpackid", loot_id.ToString());
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        dgvLoot.Rows.Clear();
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        dgvLoot.Visible = false;
                        while (reader.Read())
                        {
                            if (GetInt64(reader, "loot_pack_id") == loot_id)
                            {
                                int line = dgvLoot.Rows.Add();
                                var row = dgvLoot.Rows[line];

                                var itemid = GetInt64(reader, "item_id");
                                row.Cells[0].Value = GetInt64(reader, "id").ToString();
                                row.Cells[1].Value = GetInt64(reader, "loot_pack_id").ToString();
                                row.Cells[2].Value = itemid.ToString();
                                row.Cells[3].Value = GetTranslationByID(itemid, "items", "name");
                                row.Cells[4].Value = VisualizeDropRate(GetInt64(reader, "drop_rate"));
                                row.Cells[5].Value = GetInt64(reader, "min_amount").ToString();
                                row.Cells[6].Value = GetInt64(reader, "max_amount").ToString();
                                row.Cells[7].Value = GetInt64(reader, "grade_id").ToString();
                                row.Cells[8].Value = GetString(reader, "always_drop").ToString();
                                row.Cells[9].Value = GetInt64(reader, "group").ToString();
                            }
                        }
                        dgvLoot.Visible = true;
                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void ShowDBQuestComponent(long quest_component_id)
        {
            if (!AADB.DB_Quest_Components.TryGetValue(quest_component_id, out var c))
            {
                dgvQuestActs.Rows.Clear();
                return;
            }
            var acts = from a in AADB.DB_Quest_Acts
                       where a.Value.quest_component_id == c.id
                       select a.Value;

            dgvQuestActs.Rows.Clear();
            foreach (var a in acts)
            {
                int line = dgvQuestActs.Rows.Add();
                var row = dgvQuestActs.Rows[line];

                row.Cells[0].Value = a.id.ToString();
                row.Cells[1].Value = a.act_detail_id.ToString();
                row.Cells[2].Value = a.act_detail_type.ToString();
            }
        }


        private void ShowDBQuest(long quest_id)
        {

            if (!AADB.DB_Quest_Contexts.TryGetValue(quest_id, out var q))
            {
                lQuestId.Text = "0";
                dgvQuestComponents.Rows.Clear();
                return;
            }
            lQuestId.Text = q.id.ToString();
            dgvQuestComponents.Rows.Clear();
            var comps = from c in AADB.DB_Quest_Components
                        where c.Value.quest_context_id == quest_id
                        select c.Value;
            var first = true;
            foreach (var c in comps)
            {
                int line = dgvQuestComponents.Rows.Add();
                var row = dgvQuestComponents.Rows[line];

                row.Cells[0].Value = c.id.ToString();
                row.Cells[1].Value = c.component_kind_id.ToString();
                row.Cells[2].Value = c.next_component.ToString();
                if (AADB.DB_NPCs.TryGetValue(c.npc_id, out var npc))
                    row.Cells[3].Value = npc.nameLocalized + " (" + c.npc_id + ")";
                else
                    row.Cells[3].Value = c.npc_id.ToString();
                if (AADB.DB_Skills.TryGetValue(c.skill_id, out var skill))
                    row.Cells[4].Value = skill.nameLocalized + " (" + c.skill_id.ToString() + ")";
                else
                    row.Cells[4].Value = c.skill_id.ToString();
                row.Cells[5].Value = c.skill_self.ToString();
                row.Cells[6].Value = c.npc_spawner_id.ToString();
                row.Cells[7].Value = c.buff_id.ToString();
                if (first)
                {
                    ShowDBQuestComponent(c.id);
                    first = false;
                }
            }
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
            ShowSelectedData("buffs", "id = " + b.id.ToString(), "id ASC");
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
                loading.ShowInfo("Loading: NPCs");
                LoadNPCs();
                loading.ShowInfo("Loading: Quests");
                LoadQuests();
            }

            return true;
        }

        private void TItemSearch_TextChanged(object sender, EventArgs e)
        {
            btnItemSearch.Enabled = (tItemSearch.Text != string.Empty);
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
            var valgroup = row.Cells[9].Value;
            var id = row.Cells[0].Value;
            if (val == null)
                return;
            tLootSearch.Text = val.ToString();

            if ((val != null) && (valgroup != null))
                ShowLootGroupData(long.Parse(val.ToString()), long.Parse(valgroup.ToString()));

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

            ShowDBLootByID(searchID);
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
            btnSkillSearch.Enabled = (tSkillSearch.Text != string.Empty);
        }

        private void BtnSkillSearch_Click(object sender, EventArgs e)
        {
            dgvSkills.Rows.Clear();
            dgvSkillReagents.Rows.Clear();
            dgvSkillProducts.Rows.Clear();
            string searchText = tSkillSearch.Text;
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
                else
                if (skill.Value.SearchString.IndexOf(searchTextLower) >= 0)
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

                tSkillSearch.Text = string.Empty;
                tcViewer.SelectedTab = tpSkills;
                //tSkillSearch.Text = item.use_skill_id.ToString();
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
                pak.ClosePak();
                if (pak.OpenPak(openGamePakFileDialog.FileName, true))
                {
                    Properties.Settings.Default.GamePakFileName = openGamePakFileDialog.FileName;
                    lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                    PrepareWorldXML(true);
                }
            }
        }

        private void ShowSelectedData(string table, string whereStatement, string orderStatement)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + table + " WHERE " + whereStatement + " ORDER BY " + orderStatement + " LIMIT 1";
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
                                row.Cells[1].Value = reader.GetValue(col).ToString(); ;
                                row.Cells[2].Value = GetTranslationByID(thisID, table, col, " ");
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
                if ((z.id == searchID) || (z.zone_key == searchID) || (z.group_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
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
                        ShowDBLootByID(LootID);
                        tcViewer.SelectedTab = tpLoot;
                    }
                }
            }
        }

        private void LbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tablename = lbTableNames.SelectedItem.ToString();
            tSimpleSQL.Text = "SELECT * FROM " + tablename + " LIMIT 0, 50";

            // BtnSimpleSQL_Click(null, null);
        }

        private void BtnSimpleSQL_Click(object sender, EventArgs e)
        {
            if (tSimpleSQL.Text == string.Empty)
                return;

            try
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = tSimpleSQL.Text;
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
                            }
                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }
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
            btnSimpleSQL.Enabled = (tSimpleSQL.Text != string.Empty);
        }

        private void TSimpleSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSimpleSQL.Enabled))
                BtnSimpleSQL_Click(null, null);
        }

        private void BtnSearchNPC_Click(object sender, EventArgs e)
        {
            string searchText = tSearchNPC.Text.ToLower();
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
                        MessageBox.Show("The results were cut off at " + c.ToString() + " items, please refine your search !", "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void TSearchNPC_TextChanged(object sender, EventArgs e)
        {
            btnSearchNPC.Enabled = (tSearchNPC.Text != string.Empty);
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
                ShowSelectedData("npcs", "(id = " + id.ToString() + ")", "id ASC");
                btnShowNPCsOnMap.Tag = npc.id;
            }
            else
            {
                lNPCTemplate.Text = "???";
                lNPCTags.Text = "???";
                btnShowNPCsOnMap.Tag = 0;
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
                if ((z.id == searchID) || (z.owner_id == searchID) || (z.mother_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
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
            btnSearchDoodads.Enabled = (tSearchDoodads.Text != string.Empty);
        }

        private void TSearchDoodads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchDoodads_Click(null, null);
        }

        private void BtnSearchDoodads_Click(object sender, EventArgs e)
        {
            string searchText = tSearchDoodads.Text.ToLower();
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
                        MessageBox.Show("The results were cut off at " + c.ToString() + " doodads, please refine your search !", "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }
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
                        var line = dgvDoodadFuncGroups.Rows.Add();
                        var row = dgvDoodadFuncGroups.Rows[line];

                        row.Cells[0].Value = dFuncGroup.id.ToString();
                        row.Cells[1].Value = dFuncGroup.doodad_func_group_kind_id.ToString();
                        row.Cells[2].Value = dFuncGroup.nameLocalized;

                        if (firstFuncGroup)
                        {
                            firstFuncGroup = false;
                            ShowDBDoodadFuncGroup(dFuncGroup.id);
                        }
                    }
                }

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

                dgvDoodadFuncGroups.Rows.Clear();
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

        private List<MapSpawnLocation> GetNPCSpawnsInZoneGroup(long zoneGroupId, bool uniqueOnly = false, long filterByID = -1)
        {
            List<MapSpawnLocation> res = new List<MapSpawnLocation>();
            var zg = GetZoneGroupByID(zoneGroupId);
            if ((zg != null) && (pak.isOpen) && (pak.FileExists(zg.GamePakZoneNPCsDat)))
            {
                // Open .dat file and read it's contents
                using (var fs = pak.ExportFileAsStream(zg.GamePakZoneNPCsDat))
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
                        ZoneGroupFile = zg.GamePakZoneNPCsDat;

                    if (zg != null)
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var map = MapViewForm.GetMap();
                        map.Show();
                        map.ClearPoI();

                        npcList.AddRange(GetNPCSpawnsInZoneGroup(zg.id, true));

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
                                Refresh();
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
                                        if (npc.count == 1)
                                            row.Cells[6].Value = string.Format("{0} , {1} = ({2})", npc.x, npc.y, npc.AsSextant());
                                        else
                                            row.Cells[6].Value = npc.count.ToString() + " instances in zone";

                                        c++;
                                        if ((c % 25) == 0)
                                        {
                                            loading.ShowInfo("Loading " + c.ToString() + "/" + npcList.Count.ToString() + " NPCs");
                                        }

                                        map.AddPoI((int)npc.x, (int)npc.y, z.nameLocalized + " (" + npc.id.ToString() + ")", Color.Yellow);
                                    }
                                }
                                tcViewer.SelectedTab = tpNPCs;
                                dgvNPCs.Show();

                            }
                        }
                        map.cbPoINames.Checked = false; // Disable this when loading from zone
                        map.FocusAll(true,false);
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

                    tcViewer.SelectedTab = tpQuests;
                }
            }

        }

        private void btnQuestsSearch_Click(object sender, EventArgs e)
        {
            string searchText = tQuestSearch.Text.ToLower();
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

        }

        private void tQuestSearch_TextChanged(object sender, EventArgs e)
        {
            btnQuestsSearch.Enabled = (tQuestSearch.Text != string.Empty);
        }

        private void tQuestSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuestsSearch_Click(null, null);
            }
        }

        private void cbItemSearchItemArmorSlotTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tItemSearch.Text.Replace("*", "") == string.Empty)
            {
                if ((cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0) || (cbItemSearchItemCategoryTypeList.SelectedIndex > 0))
                    tItemSearch.Text = "*";
                else
                    tItemSearch.Text = string.Empty;
            }
        }

        private void labelZoneGroupRestrictions_Click(object sender, EventArgs e)
        {
            if (!(sender is Label))
                return;
            var zoneGroupId = (long)(sender as Label).Tag;
            var bannedInfo = string.Empty; ;
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
            MessageBox.Show(bannedInfo, "Restrictions for ZoneGroup " + zoneGroupId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var tSearchString = "=" + translation.table.ToLower() + "=" + translation.field.ToLower() + "=" + translation.idx.ToString() + "=" + translation.value.ToLower() + "=";
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
            string searchText = tSearchBuffs.Text.ToLower();
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
                        MessageBox.Show("The results were cut off at " + c.ToString() + " buffs, please refine your search !", "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void tSearchBuffs_TextChanged(object sender, EventArgs e)
        {
            btnSearchBuffs.Enabled = (tSearchBuffs.Text.Length > 0);
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
            if (MapViewForm.ThisForm != null)
                MapViewForm.ThisForm.Show();

            var map = new MapViewForm();
            map.Show();
        }

        private void btnShowNPCsOnMap_Click(object sender, EventArgs e)
        {
            var map = MapViewForm.GetMap();
            map.Show();
            map.ClearPoI();

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
                        loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" + AADB.DB_Zone_Groups.Count.ToString());
                        npcList.AddRange(GetNPCSpawnsInZoneGroup(zg.id, true));
                    }
                }

                if (npcList.Count > 0)
                {
                    // Add to NPC list
                    foreach (var npc in npcList)
                    {
                        if (npc.id != searchId)
                            continue;
                        if (AADB.DB_NPCs.TryGetValue(npc.id, out var z))
                        {
                            map.AddPoI((int)npc.x, (int)npc.y, z.nameLocalized + " (" + npc.id.ToString() + ")", Color.Yellow);
                        }
                    }
                }

            }
            map.cbPoINames.Checked = true;
            map.cbFocus.Checked = true;
            map.FocusAll(true,false);
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
                            if (MessageBox.Show("Append paths ?", "Add path to map", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                map.ClearPaths();
                        foreach (var p in allpaths)
                            map.AddPath(p);
                        map.cbPoINames.Checked = true;
                        map.FocusAll(false,true);
                    }

                }
            } 
        }

        private void AddTransferPath(ref List<MapViewPath> allpaths, GameZone zone)
        {
            if (zone != null)
            {
                var worldOff = MapViewWorldXML.GetZoneByKey(zone.zone_key);

                // If it's not in the world.xml, it's probably not a real zone anyway
                if (worldOff == null)
                    return;

                if (!pak.isOpen || !pak.FileExists(zone.GamePakZoneTransferPathXML))
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
                                    newPath.PathType = tp.owner_id;

                            long model = 0;
                            if (AADB.DB_Transfers.TryGetValue(newPath.PathType, out var transfer))
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
                                newPath.PathName = blockName + "(t:" + newPath.PathType.ToString() + " m:" + model.ToString() + " z:"+zone.zone_key.ToString()+")";
                            else
                                newPath.PathName = blockName + "(t:" + newPath.PathType.ToString() + " z:" + zone.zone_key.ToString() + ")";

                            // We don't need the cellX/Y values here
                            /*
                            int cellXOffset = 0;
                            int cellYOffset = 0;

                            if (attribs.TryGetValue("cellx",out var cellXOffsetString))
                            {
                                try
                                {
                                    cellXOffset = int.Parse(cellXOffsetString); 
                                }
                                catch
                                {
                                    cellXOffset = 0;
                                }
                            }
                            if (attribs.TryGetValue("celly", out var cellYOffsetString))
                            {
                                try
                                {
                                    cellYOffset = int.Parse(cellYOffsetString);
                                }
                                catch
                                {
                                    cellYOffset = 0;
                                }
                            }
                            */

                            PointF cellOffset = new PointF((worldOff.originCellX * 1024f), (worldOff.originCellY * 1024f));
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
                                        newPath.allpoints.Add(vec);
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

        private void btnDebug_Click(object sender, EventArgs e)
        {
            var s = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" +
                    "<!-- Helper file for displaying path data on the map, needs manual editing -->\r\n" +
                    "<pathoffsets>\r\n";
            foreach(var zv in AADB.DB_Zones)
            {
                s += "\t<zone id=\""+zv.Value.id.ToString()+"\" key=\""+zv.Value.zone_key.ToString()+"\" x=\"0\" y=\"0\" /><!-- "+zv.Value.name+" -->\r\n";
            }
            s += "</pathoffsets>\r\n";
            File.WriteAllText("debug.xml", s);
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
                map.ClearPaths();
                foreach (var p in allpaths)
                    map.AddPath(p);
            }
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

        }

        public void PrepareWorldXML(bool overwrite)
        {
            if (overwrite || (MapViewWorldXML.zones.Count <= 0))
            {
                // try loading if no zones loaded yet
                var worldxmlfile = "game/worlds/main_world/world.xml";
                if ((!pak.isOpen) || (!pak.FileExists(worldxmlfile)))
                {
                    MessageBox.Show("Could not find world.xml");
                    return;
                }

                if (!MapViewWorldXML.LoadFromStream(pak.ExportFileAsStream(worldxmlfile)))
                {
                    MessageBox.Show("world.xml did not contain valid data");
                    return;
                }
            }
        }
    }
}

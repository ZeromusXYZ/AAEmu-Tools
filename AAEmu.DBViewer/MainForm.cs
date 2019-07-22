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

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        private string defaultTitle;
        private AAPak pak = new AAPak("");
        private List<string> possibleLanguageIDs = new List<string>();
        private string map_data_npc_map_dir = "/map_data/npc_map/";
        private string main_world_dir = "main_world";
        private string game_worlds_dir = "game/worlds/";

        public MainForm()
        {
            InitializeComponent();
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
                        Properties.Settings.Default.GamePakFileName = game_pakFileName;
                }
            }
            tcViewer.SelectedTab = tpItems;
            AttachEventsToLabels();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
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
                            lbTableNames.Items.Add(GetString(reader,"name"));
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
            foreach(var tl in AADB.DB_Translations)
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

            // End of Custom Translations
        }

        private void LoadTranslations(string lng)
        {
            string sql = "SELECT * FROM localized_texts ORDER BY tbl_name, tbl_column_name, idx";

            List<string> columnNames = null ;

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
                            t.idx = GetInt64(reader,"idx");
                            t.table = GetString(reader,"tbl_name");
                            t.field = GetString(reader,"tbl_column_name");
                            
                            t.value = GetString(reader,lng);
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
                    foreach(var l in possibleLanguageIDs)
                    {
                        if (columnNames.IndexOf(l) >= 0)
                            availableLng.Add(l);
                    }
                    cbItemSearchLanguage.Items.Clear();
                    for(int i = 0; i < availableLng.Count;i++)
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
            Properties.Settings.Default.DefaultGameLanguage = lng ;
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
                            t.abox_show = GetBool(reader, "abox_show");

                            if (t.display_text != string.Empty)
                                t.display_textLocalized = GetTranslationByID(t.id, "zones", "display_text", t.display_text);
                            else
                                t.display_textLocalized = "" ;
                            t.SearchString = t.name + " " + t.display_text + " " + t.display_textLocalized ;
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
                            t.id = GetInt64(reader,"id");
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
                            t.descriptionLocalized = GetTranslationByID(t.id, "items", "description", t.descriptionLocalized);

                            t.SearchString = t.name + " " + t.description + " " + t.nameLocalized + " " + t.descriptionLocalized ;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Items.Add(t.id, t);
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
                            t.custom_gcd = GetInt64(reader,"custom_gcd");
                            t.first_reagent_only = GetBool(reader, "first_reagent_only");

                            t.nameLocalized = GetTranslationByID(t.id, "skills", "name", t.name);
                            t.descriptionLocalized = GetTranslationByID(t.id, "skills", "desc", t.descriptionLocalized);
                            if (readWebDesc)
                                t.webDescriptionLocalized = GetTranslationByID(t.id, "skills", "web_desc", t.webDescriptionLocalized);
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
                            t.equip_bodies_id = GetInt64(reader, "equip_bodies_id");
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
                            t.comment1 = GetString(reader, "comment1");
                            t.npc_tendency_id = GetInt64(reader, "npc_tendency_id");
                            t.blacksmith = GetBool(reader, "blacksmith");
                            t.teleporter = GetBool(reader, "teleporter");
                            t.opacity = GetFloat(reader, "opacity");
                            t.ability_changer = GetBool(reader, "ability_changer");
                            t.scale = GetFloat(reader, "scale");
                            t.comment2 = GetString(reader, "comment2");
                            t.comment3 = GetString(reader, "comment3");
                            t.sight_range_scale = GetFloat(reader, "sight_range_scale");
                            t.sight_fov_scale = GetFloat(reader, "sight_fov_scale");
                            t.milestone_id = GetInt64(reader, "milestone_id");
                            t.attack_start_range_scale = GetFloat(reader, "attack_start_range_scale");
                            t.aggression = GetBool(reader, "aggression");
                            t.exp_multiplier = GetFloat(reader, "exp_multiplier");
                            t.exp_adder = GetInt64(reader, "exp_adder");
                            t.stabler = GetBool(reader, "stabler");
                            t.accept_aggro_link = GetBool(reader, "accept_aggro_link");
                            t.recruiting_battle_field_id = GetInt64(reader, "recruiting_battle_field_id");
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
                            t.comment_wear = GetString(reader, "comment_wear");
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
                            t.fx_scale = GetFloat(reader, "fx_scale");
                            t.translate = GetBool(reader, "translate");
                            t.no_penalty = GetBool(reader, "no_penalty");
                            t.show_faction_tag = GetBool(reader, "show_faction_tag");


                            t.nameLocalized = GetTranslationByID(t.id, "npcs", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized ;
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
                            t.SearchString = t.name + " " + t.nameLocalized + " " + t.owner_nameLocalized ;
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
                            t.milestone_id = GetInt64(reader, "milestone_id");
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
                            t.force_up_action = GetBool(reader , "force_up_action");
                            t.load_model_from_world = GetBool(reader, "load_model_from_world");
                            t.parentable = GetBool(reader, "parentable");
                            t.childable = GetBool(reader, "childable");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.growth_time = GetInt64(reader, "growth_time");
                            t.despawn_on_collision = GetBool(reader, "despawn_on_collision");
                            t.no_collision = GetBool(reader, "no_collision");
                            t.restrict_zone_id = GetInt64(reader, "restrict_zone_id");
                            t.translate = GetBool(reader, "translate");

                            // Helpers
                            t.nameLocalized = GetTranslationByID(t.id, "doodad_almighties", "name", t.name);
                            t.SearchString = t.name + " " + t.nameLocalized ;
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
                        while (reader.Read())
                        {
                            var t = new GameDoodadGroup();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
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
                            t.comment = GetString(reader, "comment");
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




        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            dgvItem.Rows.Clear();
            string searchText = tItemSearch.Text ;
            if (searchText == string.Empty)
                return;
            string lng = cbItemSearchLanguage.Text;
            string sql = string.Empty;
            string sqlWhere = string.Empty;
            long searchID ;
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

        private string GetTranslationByID(long idx, string table, string field, string defaultValue = "")
        {
            string res = string.Empty;
            string k = table + ":" + field + ":" + idx.ToString();
            if (AADB.DB_Translations.TryGetValue(k, out GameTranslation val))
                res = val.value;
            // If no translation found ...
            if (res == string.Empty)
            {
                if (defaultValue == string.Empty)
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
            string a ;
            string r ;
            string g ;
            string b ;
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

                if ( (atStart >= 0) && (restText.Length >= pipeStart + 12) )
                {
                    var atStartBracket = restText.IndexOf("(", atStart);
                    var atEndBracket = restText.IndexOf(")", atStart);
                    if ((atStartBracket >= 0) && (atEndBracket >= 0) && (atEndBracket > atStartBracket))
                    {
                        var fieldNameStr = restText.Substring(atStart+1, atStartBracket - atStart - 1);
                        var fieldValStr = restText.Substring(atStartBracket+1, atEndBracket - atStartBracket - 1);
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
                            {
                                rt.AppendText("@"+fieldNameStr+"(" + itemVal.ToString() + ")");
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
                        case "c": if (restText.Length >= pipeStart + 10)
                                colStartPos = pipeStart;
                            break;
                        case "r": if  (restText.Length >= pipeStart + 1)
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

        private string MSToString(long msTime)
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
            if (ss > 0)
                res += ss.ToString() + "s ";
            if (ms > 0)
                res += ms.ToString() + "ms";

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
                return range.ToString("0")+" mm";
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
            if (AADB.DB_Items.TryGetValue(idx,out var item))
            {
                lItemID.Text = idx.ToString();
                lItemName.Text = item.nameLocalized ;
                lItemCategory.Text = GetTranslationByID(item.catgegory_id, "item_categories", "name") + " (" + item.catgegory_id.ToString() + ")";
                FormattedTextToRichtEdit(item.descriptionLocalized,rtItemDesc);
                lItemLevel.Text = item.level.ToString();
                IconIDToLabel(item.icon_id, itemIcon);
                btnFindItemSkill.Enabled = true;// (item.use_skill_id > 0);

                ShowSelectedData("items", "(id = " + idx.ToString() + ")", "id ASC");
            }
            else
            {
                lItemID.Text = idx.ToString();
                lItemName.Text = "<not found>";
                lItemCategory.Text = "" ;
                rtItemDesc.Clear();
                lItemLevel.Text = "";
                itemIcon.Text = "???";
                btnFindItemSkill.Enabled = false;
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

                ShowSelectedData("skills", "(id = " + idx.ToString() + ")", "id ASC");

                if (skill.first_reagent_only)
                {
                    lSkillReagents.Text = "Requires either of these items to use";
                }
                else
                {
                    lSkillReagents.Text = "Required items to use this skill";
                }
                // Produces
                dgvSkillProducts.Rows.Clear();
                foreach(var p in AADB.DB_Skill_Products)
                {
                    if (p.Value.skill_id == idx)
                    {
                        var line = dgvSkillProducts.Rows.Add();
                        var row = dgvSkillProducts.Rows[line];
                        row.Cells[0].Value = p.Value.item_id.ToString();
                        if (AADB.DB_Items.TryGetValue(p.Value.item_id, out var item))
                        {
                            row.Cells[1].Value = item.nameLocalized ;
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
            }
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

                // From Zone_Groups
                if (AADB.DB_Zone_Groups.TryGetValue(zone.group_id, out var zg))
                {
                    lZoneGroupsDisplayName.Text = zg.display_textLocalized;
                    lZoneGroupsName.Text = zg.name;
                    string zonefile;
                    if (zg.target_id != 1)
                    {
                        zonefile = game_worlds_dir + main_world_dir + map_data_npc_map_dir + zg.name + ".dat";
                    }
                    else
                    {
                        zonefile = game_worlds_dir + zg.name + map_data_npc_map_dir + zg.name + ".dat";
                    }
                    if ((pak.isOpen) && (pak.FileExists(zonefile)))
                    {
                        btnFindNPCsInZone.Tag = zonefile;
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
                return "1 (Always?)";
            double d = droprate / 100000;
            return d.ToString("0.00") + " %";
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

        private void ShowDBLootByID(long loot_id)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
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




        private void TItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnItemSearch_Click(null,null);
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

            while ( forceDlg || (!File.Exists(sqlfile)) )
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
                loading.ShowInfo("Loading: Zones");
                LoadZones();
                loading.ShowInfo("Loading: Doodads");
                LoadDoodads();
                loading.ShowInfo("Loading: Items");
                LoadItems();
                loading.ShowInfo("Loading: Skills");
                LoadSkills();
                LoadSkillReagents();
                LoadSkillProducts();
                loading.ShowInfo("Loading: NPCs");
                LoadNPCs();
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
            var id = row.Cells[0].Value;
            if (val == null)
                return;
            tLootSearch.Text = val.ToString();

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

                foreach(var p in AADB.DB_Skill_Reagents)
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
                if (pak.OpenPak(openGamePakFileDialog.FileName,true))
                    Properties.Settings.Default.GamePakFileName = openGamePakFileDialog.FileName;
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
                            long thisID = -1 ;
                            if (columnNames.IndexOf("id") >= 0)
                                thisID = GetInt64(reader, "id");

                            foreach(var col in columnNames)
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
                searchID = -1 ;

            bool first = true;
            dgvZones.Rows.Clear();
            foreach(var t in AADB.DB_Zones)
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
            if ( (sender != null) && (sender is Button) )
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

            BtnSimpleSQL_Click(null, null);
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
                    row.Cells[3].Value = z.npc_template_id.ToString();
                    row.Cells[4].Value = z.npc_kind_id.ToString();
                    row.Cells[5].Value = z.npc_grade_id.ToString();
                    row.Cells[6].Value = AADB.GetFactionName(z.faction_id,true);
                    row.Cells[7].Value = z.model_id.ToString();
                    row.Cells[8].Value = "(??,??)";

                    if (c == 0)
                    {
                        // ShowDBNPC(z.id);
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

        private void DgvNPCs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNPCs.SelectedRows.Count <= 0)
                return;
            var row = dgvNPCs.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
                ShowSelectedData("npcs", "(id = " + id.ToString() + ")", "id ASC");
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
                        row.Cells[2].Value = AADB.GetFactionName(z.mother_id,true);
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
                    row.Cells[2].Value = AADB.GetFactionName(z.mother_id,true);
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
            foreach(var t in AADB.DB_Doodad_Almighties)
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
                        row.Cells[3].Value = dGroup.nameLocalized + " (" +z.group_id.ToString() + ")"; 
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
                    row.Cells[7].Value = z.model ;

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
            if (AADB.DB_Doodad_Almighties.TryGetValue(id,out var doodad))
            {
                lDoodadID.Text = doodad.id.ToString();
                lDoodadName.Text = doodad.nameLocalized;
                lDoodadModel.Text = doodad.model;
                lDoodadOnceOneMan.Text = doodad.once_one_man.ToString();
                lDoodadOnceOneInteraction.Text = doodad.once_one_interaction.ToString() ;
                lDoodadShowName.Text = doodad.show_name.ToString() ;
                lDoodadMgmtSpawn.Text = doodad.mgmt_spawn.ToString() ;
                lDoodadPercent.Text = doodad.percent.ToString();
                lDoodadMinTime.Text = MSToString(doodad.min_time);
                lDoodadMaxTime.Text = MSToString(doodad.max_time);
                lDoodadModelKindID.Text = doodad.model_kind_id.ToString() ;
                lDoodadUseCreatorFaction.Text = doodad.use_creator_faction.ToString() ;
                lDoodadForceToDTopPriority.Text = doodad.force_tod_top_priority.ToString() ;
                lDoodadMilestoneID.Text = doodad.milestone_id.ToString() ;
                lDoodadGroupID.Text = doodad.group_id.ToString() ;
                lDoodadShowName.Text = doodad.show_minimap.ToString() ;
                lDoodadUseTargetDecal.Text = doodad.use_target_decal.ToString() ;
                lDoodadUseTargetSilhouette.Text = doodad.use_target_silhouette.ToString() ;
                lDoodadUseTargetHighlight.Text = doodad.use_target_highlight.ToString() ;
                lDoodadTargetDecalSize.Text = doodad.target_decal_size.ToString();
                lDoodadSimRadius.Text = RangeToString(doodad.sim_radius);
                lDoodadCollideShip.Text = doodad.collide_ship.ToString() ;
                lDoodadCollideVehicle.Text = doodad.collide_vehicle.ToString() ;
                lDoodadClimateID.Text = doodad.climate_id.ToString() ;
                lDoodadSaveIndun.Text = doodad.save_indun.ToString() ;
                lDoodadMarkModel.Text = doodad.mark_model.ToString() ;
                lDoodadForceUpAction.Text = doodad.force_up_action.ToString() ;
                lDoodadLoadModelFromWorld.Text = doodad.load_model_from_world.ToString() ;
                lDoodadParentable.Text = doodad.parentable.ToString() ;
                lDoodadChildable.Text = doodad.childable.ToString() ;
                lDoodadFactionID.Text = AADB.GetFactionName(doodad.faction_id,true);
                lDoodadGrowthTime.Text = MSToString(doodad.growth_time);
                lDoodadDespawnOnCollision.Text = doodad.despawn_on_collision.ToString() ;
                lDoodadNoCollision.Text = doodad.no_collision.ToString() ;
                lDoodadRestrictZoneID.Text = doodad.restrict_zone_id.ToString() ;

                if (AADB.DB_Doodad_Groups.TryGetValue(doodad.group_id,out var dGroup))
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
                foreach(var f in AADB.DB_Doodad_Func_Groups)
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
                lDoodadFuncGroupModel.Text = dfg.model ;
                lDoodadFuncGroupKindID.Text = dfg.doodad_func_group_kind_id.ToString();
                lDoodadFuncGroupPhaseMsg.Text = dfg.phase_msgLocalized ;
                lDoodadFuncGroupSoundID.Text = dfg.sound_id.ToString();
                lDoodadFuncGroupName.Text = dfg.nameLocalized;
                lDoodadFuncGroupSoundTime.Text = MSToString(dfg.sound_time);
                lDoodadFuncGroupComment.Text = dfg.comment ;
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
            public long id ;
            public float x ;
            public float y ;
            public float z ;

            string FloatToCoord(double f)
            {
                var f1 = Math.Floor(f);
                f -= f1;
                f *= 60;
                var f2 = Math.Floor(f);
                f -= f2;
                f *= 60;
                var f3 = Math.Floor(f);

                return f1.ToString("0")+"° "+f2.ToString("00")+"' " + f3.ToString("00")+"\"" ;
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

                return res ;
            }
        }

        private void BtnFindNPCsInZone_Click(object sender, EventArgs e)
        {
            Dictionary<long, MapSpawnLocation> npcList = new Dictionary<long, MapSpawnLocation>();

            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    string ZoneGroupFile = (string)b.Tag;

                    if ((ZoneGroupFile != string.Empty) && (pak.isOpen) && (pak.FileExists(ZoneGroupFile)))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        // Open .dat file and read it's contents
                        using (var fs = pak.ExportFileAsStream(ZoneGroupFile))
                        {
                            int indexCount = ((int)fs.Length / 16);
                            using (var reader = new BinaryReader(fs))
                            {
                                for (int i = 0; i < indexCount; i++)
                                {
                                    MapSpawnLocation msl = new MapSpawnLocation();
                                    msl.id = reader.ReadInt32();
                                    msl.x = reader.ReadSingle();
                                    msl.y = reader.ReadSingle();
                                    msl.z = reader.ReadSingle();

                                    // Only add uniques for now
                                    // if (npcList.TryGetValue .IndexOf(id) < 0)
                                    npcList.Add(i,msl);
                                }
                            }
                        }

                        if (npcList.Count > 0)
                        {
                            using (var loading = new LoadingForm())
                            {
                                loading.ShowInfo("Loading " + npcList.Count.ToString()+" NPCs");
                                loading.Show();

                                // Add to NPC list
                                // tcViewer.SelectedTab = tpNPCs;
                                dgvNPCs.Rows.Clear();
                                Refresh();
                                int c = 0;
                                foreach (var npc in npcList)
                                {
                                    if (AADB.DB_NPCs.TryGetValue(npc.Value.id, out var z))
                                    {
                                        var line = dgvNPCs.Rows.Add();
                                        var row = dgvNPCs.Rows[line];

                                        row.Cells[0].Value = z.id.ToString();
                                        row.Cells[1].Value = z.nameLocalized;
                                        row.Cells[2].Value = z.level.ToString();
                                        row.Cells[3].Value = z.npc_template_id.ToString();
                                        row.Cells[4].Value = z.npc_kind_id.ToString();
                                        row.Cells[5].Value = z.npc_grade_id.ToString();
                                        row.Cells[6].Value = AADB.GetFactionName(z.faction_id, true);
                                        row.Cells[7].Value = z.model_id.ToString();
                                        row.Cells[8].Value = npc.Value.AsSextant();

                                        c++;
                                        if ((c % 25) == 0)
                                        {
                                            loading.ShowInfo("Loading " + c.ToString() + "/" + npcList.Count.ToString() + " NPCs");
                                        }
                                    }
                                }
                                tcViewer.SelectedTab = tpNPCs;

                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }
    }
}

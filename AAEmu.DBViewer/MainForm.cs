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
using AAEmu.Game.Utils.DB;
using FreeImageAPI;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        private string defaultTitle;
        private AAPak pak = new AAPak("");
        private List<string> possibleLanguageIDs = new List<string>();

        class GameTranslation
        {
            public long idx = 0;
            public string table = string.Empty ;
            public string field = string.Empty ;
            public string value = string.Empty ;
        }
        Dictionary<string, GameTranslation> CurrentTranslations = new Dictionary<string, GameTranslation>();

        class GameItem
        {
            // Actual DB entries
            public long id = 0;
            public string name = string.Empty;
            public long catgegory_id = 1 ;
            public long level = 1 ;
            public string description = string.Empty;
            public long price = 0;
            public long refund = 0;
            public long max_stack_size = 1;
            public long icon_id = 1;
            public bool sellable = false;
            public long fixed_grade = -1;
            public long use_skill_id = 0;

            // Helpers
            public string nameLocalized = string.Empty;
            public string descriptionLocalized = string.Empty;
            public string SearchString = string.Empty;
        }

        class GameSkills
        {
            // Actual DB entries
            public long id = 0;
            public string name = string.Empty;
            public string desc = string.Empty;
            public string web_desc = string.Empty;
            public long cost = 0;
            public long icon_id = 0;
            public bool show = false;
            public long cooldown_time = 0;
            public long casting_time = 0;
            public bool ignore_global_cooldown = false;
            public bool default_gcd = true;
            public long custom_gcd = 0 ;
            public long effect_delay = 0;
            public long ability_id = 0;
            public long mana_cost = 0;
            public long timing_id = 0;
            public long consume_lp = 0;
            public bool first_reagent_only = false;

            // Helpers
            public string nameLocalized = string.Empty;
            public string descriptionLocalized = string.Empty;
            public string webDescriptionLocalized = string.Empty;
            public string SearchString = string.Empty;
        }

        class GameNPC
        {
            // Actual DB entries
            public long id = 0;
            public string name = string.Empty;
            public long char_race_id = 0;
            public long npc_grade_id = 0;
            public long npc_kind_id = 0;
            public long level = 0;
            public long faction_id = 0;
            public long model_id = 0;

            // Helpers
            public string nameLocalized = string.Empty;
            public string SearchString = string.Empty;
        }

        class GameSkillItems
        {
            public long id = 0;
            public long skill_id = 0;
            public long item_id = 0;
            public long amount = 0;
        }

        class GameZone
        {
            public long id = 0;
            public string name = string.Empty;
            public long zone_key = 0;
            public long group_id = 0;
            public bool closed = false;
            public string display_text = string.Empty;
            public long faction_id = 0;
            public long zone_climate_id = 0;
            public bool abox_show = false; // no idea what this is, seems to be always set to false

            // Helpers
            public string display_textLocalized = string.Empty;
            public string SearchString = string.Empty;
        }

        class GameZone_Groups
        {
            public long id = 0;
            public string name = string.Empty;
            public double x = 0;
            public double y = 0;
            public double w = 0;
            public double h = 0;
            public long image_map = 0;
            public long sound_id = 0;
            public long target_id = 0;
            public string display_text = string.Empty;
            public long faction_chat_region_id = 0;
            public long sound_pack_id = 0;
            public long pirate_desperado = 0;
            public long fishing_sea_loot_pack_id = 0;
            public long fishing_land_loot_pack_id = 0;
            public long buff_id = 0;

            // Helpers
            public string display_textLocalized = string.Empty;
            public string SearchString = string.Empty;
        }

        Dictionary<long, GameItem> DB_Items = new Dictionary<long, GameItem>();
        Dictionary<long, GameSkills> DB_Skills = new Dictionary<long, GameSkills>();
        Dictionary<long, GameNPC> DB_NPCs = new Dictionary<long, GameNPC>();
        Dictionary<long, string> DB_Icons = new Dictionary<long, string>();
        Dictionary<long, GameSkillItems> DB_Skill_Reagents = new Dictionary<long, GameSkillItems>();
        Dictionary<long, GameSkillItems> DB_Skill_Products = new Dictionary<long, GameSkillItems>();
        Dictionary<long, GameZone> DB_Zones = new Dictionary<long, GameZone>();
        Dictionary<long, GameZone_Groups> DB_Zone_Groups = new Dictionary<long, GameZone_Groups>();

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
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pak != null)
                pak.ClosePak();
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

        private double GetDouble(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return 0;
            else
                return reader.GetDouble(fieldname);
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
                        DB_Icons.Clear();
                        while (reader.Read())
                        {
                            DB_Icons.Add(GetInt64(reader, "id"), GetString(reader, "filename"));
                        }
                    }
                }
            }
        }

        private void LoadTranslations(string lng)
        {
            string sql = "SELECT * FROM localized_texts ORDER BY tbl_name, tbl_column_name, idx";

            List<string> columnNames = null ;

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    CurrentTranslations.Clear();

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
                            CurrentTranslations.Add(k, t);
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

        private bool DBValueToBool(string val)
        {
            return ((val != null) && ((val == "t") || (val == "T") || (val == "1")));
        }

        private void LoadZones()
        {
            string sql = "SELECT * FROM zones ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    DB_Zones.Clear();

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
                            t.closed = DBValueToBool(GetString(reader, "closed"));
                            t.display_text = GetString(reader, "display_text");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.zone_climate_id = GetInt64(reader, "zone_climate_id");
                            t.abox_show = DBValueToBool(GetString(reader, "abox_show"));

                            if (t.display_text != string.Empty)
                                t.display_textLocalized = GetTranslationByID(t.id, "zones", "display_text", t.display_text);
                            else
                                t.display_textLocalized = "" ;
                            t.SearchString = t.name + " " + t.display_text + " " + t.display_textLocalized ;
                            t.SearchString = t.SearchString.ToLower();

                            DB_Zones.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            sql = "SELECT * FROM zone_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    DB_Zone_Groups.Clear();

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
                            t.x = GetDouble(reader, "x");
                            t.y = GetDouble(reader, "y");
                            t.w = GetDouble(reader, "w");
                            t.h = GetDouble(reader, "h");
                            t.image_map = GetInt64(reader, "image_map");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.target_id = GetInt64(reader, "target_id");
                            t.display_text = GetString(reader, "display_text");
                            t.faction_chat_region_id = GetInt64(reader, "faction_chat_region_id");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.pirate_desperado = GetInt64(reader, "pirate_desperado");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.pirate_desperado = GetInt64(reader, "pirate_desperado");
                            t.fishing_sea_loot_pack_id = GetInt64(reader, "fishing_sea_loot_pack_id");
                            t.fishing_land_loot_pack_id = GetInt64(reader, "fishing_land_loot_pack_id");
                            t.buff_id = GetInt64(reader, "buff_id");

                            if (t.display_text != string.Empty)
                                t.display_textLocalized = GetTranslationByID(t.id, "zone_groups", "display_text", t.display_text);
                            else
                                t.display_textLocalized = "";
                            t.SearchString = t.name + " " + t.display_text + " " + t.display_textLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            DB_Zone_Groups.Add(t.id, t);
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
                    DB_Items.Clear();

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
                            t.sellable = DBValueToBool(GetString(reader, "sellable"));
                            t.fixed_grade = GetInt64(reader, "fixed_grade");
                            t.use_skill_id = GetInt64(reader, "use_skill_id");

                            t.nameLocalized = GetTranslationByID(t.id, "items", "name", t.name);
                            t.descriptionLocalized = GetTranslationByID(t.id, "items", "description", t.descriptionLocalized);

                            t.SearchString = t.name + " " + t.description + " " + t.nameLocalized + " " + t.descriptionLocalized ;
                            t.SearchString = t.SearchString.ToLower();

                            DB_Items.Add(t.id, t);
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
                    DB_Skills.Clear();

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
                            t.show = DBValueToBool(GetString(reader, "show"));
                            t.cooldown_time = GetInt64(reader, "cooldown_time");
                            t.casting_time = GetInt64(reader, "casting_time");
                            t.ignore_global_cooldown = DBValueToBool(GetString(reader, "ignore_global_cooldown"));
                            t.effect_delay = GetInt64(reader, "effect_delay");
                            t.ability_id = GetInt64(reader, "ability_id");
                            t.mana_cost = GetInt64(reader, "mana_cost");
                            t.timing_id = GetInt64(reader, "timing_id");
                            t.consume_lp = GetInt64(reader, "consume_lp");
                            t.default_gcd = DBValueToBool(GetString(reader, "default_gcd")); ;
                            t.custom_gcd = GetInt64(reader,"custom_gcd");
                            t.first_reagent_only = DBValueToBool(GetString(reader, "first_reagent_only")); ;

                            t.nameLocalized = GetTranslationByID(t.id, "skills", "name", t.name);
                            t.descriptionLocalized = GetTranslationByID(t.id, "skills", "desc", t.descriptionLocalized);
                            if (readWebDesc)
                                t.webDescriptionLocalized = GetTranslationByID(t.id, "skills", "web_desc", t.webDescriptionLocalized);
                            else
                                t.webDescriptionLocalized = string.Empty;

                            t.SearchString = t.name + " " + t.desc + " " + t.nameLocalized + " " + t.descriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            DB_Skills.Add(t.id, t);
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
                    DB_Skill_Reagents.Clear();

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

                            DB_Skill_Reagents.Add(t.id, t);
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
                    DB_Skill_Products.Clear();

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

                            DB_Skill_Products.Add(t.id, t);
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
                        DB_NPCs.Clear();
                        while (reader.Read())
                        {
                            var t = new GameNPC();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.char_race_id = GetInt64(reader, "char_race_id");
                            t.npc_grade_id = GetInt64(reader, "npc_grade_id");
                            t.npc_kind_id = GetInt64(reader, "npc_kind_id");
                            t.level = GetInt64(reader, "level");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.model_id = GetInt64(reader, "model_id");
                            t.nameLocalized = GetTranslationByID(t.id, "npcs", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized ;
                            t.SearchString = t.SearchString.ToLower();
                            DB_NPCs.Add(t.id, t);
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
            foreach (var item in DB_Items)
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
            if (CurrentTranslations.TryGetValue(k, out GameTranslation val))
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
                            if ((fieldNameStr == "ITEM_NAME") && (DB_Items.TryGetValue(itemVal, out GameItem item)))
                            {
                                rt.AppendText(item.nameLocalized);
                            }
                            else
                            if ((fieldNameStr == "NPC_NAME") && (DB_NPCs.TryGetValue(itemVal, out GameNPC npc)))
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
                if (DB_Icons.TryGetValue(icon_id, out var iconname))
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

        private void ShowDBItem(long idx)
        {
            if (DB_Items.TryGetValue(idx,out var item))
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
            if (DB_Skills.TryGetValue(idx, out var skill))
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
                foreach(var p in DB_Skill_Products)
                {
                    if (p.Value.skill_id == idx)
                    {
                        var line = dgvSkillProducts.Rows.Add();
                        var row = dgvSkillProducts.Rows[line];
                        row.Cells[0].Value = p.Value.item_id.ToString();
                        if (DB_Items.TryGetValue(p.Value.item_id, out var item))
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
                foreach (var p in DB_Skill_Reagents)
                {
                    if (p.Value.skill_id == idx)
                    {
                        var line = dgvSkillReagents.Rows.Add();
                        var row = dgvSkillReagents.Rows[line];
                        row.Cells[0].Value = p.Value.item_id.ToString();
                        if (DB_Items.TryGetValue(p.Value.item_id, out var item))
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
                loading.ShowInfo("Loading: Icon info");
                LoadIcons();
                loading.ShowInfo("Loading: Zones");
                LoadZones();
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
            foreach (var skill in DB_Skills)
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
            if (DB_Skills.TryGetValue(skillindex, out var skill))
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
            if (DB_Items.TryGetValue(id, out var item))
            {
                dgvSkills.Rows.Clear();
                dgvSkillReagents.Rows.Clear();
                dgvSkillProducts.Rows.Clear();
                if (item.use_skill_id > 0)
                    AddSkillLine(item.use_skill_id);

                foreach(var p in DB_Skill_Reagents)
                {
                    if (p.Value.item_id == id)
                        AddSkillLine(p.Value.skill_id);
                }
                foreach (var p in DB_Skill_Products)
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
                    lCurrentDataInfo.Text = command.CommandText;
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

            dgvZones.Rows.Clear();
            foreach(var t in DB_Zones)
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

                }
            }
        }

        private void TZonesSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearchZones.Enabled = (tZonesSearch.Text != string.Empty);
        }

        private void BtnZonesShowAll_Click(object sender, EventArgs e)
        {
            dgvZones.Rows.Clear();
            foreach (var t in DB_Zones)
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
            ShowSelectedData("zones", "(id = " + val.ToString() + ")", "id ASC");
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private static void AddCustomTranslation(string table, string field, long idx, string val)
    {
        // Try overriding if empty
        foreach (var tl in AaDb.DbTranslations)
        {
            if ((tl.Value.Idx == idx) && (tl.Value.Table == table) && (tl.Value.Field == field))
            {
                if (tl.Value.Value == string.Empty)
                {
                    tl.Value.Value = val;
                }

                return;
            }
        }

        // Not in table yet, add it
        GameTranslation t = new GameTranslation();
        t.Idx = idx;
        t.Table = table;
        t.Field = field;
        t.Value = val;
        string k = t.Table + ":" + t.Field + ":" + t.Idx.ToString();
        AaDb.DbTranslations.Add(k, t);
    }

    private static void AddCustomTranslations()
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


    private void LoadTranslations(string lng)
    {
        List<string> columnNames = null;

        if (AllTableNames.GetValueOrDefault("localized_texts") != SQLite.SQLiteFileName)
        {
            return;
        }

        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AaDb.DbTranslations.Clear();

                command.CommandText = @"SELECT * FROM localized_texts ORDER BY tbl_name, tbl_column_name, idx";
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
                        t.Idx = GetInt64(reader, "idx");
                        t.Table = GetString(reader, "tbl_name");
                        t.Field = GetString(reader, "tbl_column_name");

                        t.Value = GetString(reader, lng);
                        string k = t.Table + ":" + t.Field + ":" + t.Idx.ToString();
                        AaDb.DbTranslations.Add(k, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;

                }
            }
        }

        try
        {
            cbItemSearchLanguage.Enabled = false;

            List<string> availableLng = new List<string>();
            foreach (var l in PossibleLanguageIDs)
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

        if (cbItemSearchLanguage.Items.Contains(lng))
            cbItemSearchLanguage.Text = lng;
        cbItemSearchLanguage.Enabled = (cbItemSearchLanguage.Items.Count > 1);
        Properties.Settings.Default.DefaultGameLanguage = lng;
    }

    private void LoadUiTexts()
    {
        if (AllTableNames.GetValueOrDefault("ui_texts") != SQLite.SQLiteFileName)
            return;
        var catCounter = new Dictionary<long, long>();

        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ui_texts ORDER BY id ASC";
        command.Prepare();
        using var reader = new SQLiteWrapperReader(command.ExecuteReader());
        AaDb.DbIcons.Clear();
        while (reader.Read())
        {
            var t = new GameUiTexts();

            t.Id = GetInt64(reader, "id");
            t.Key = GetString(reader, "key");
            t.Text = GetString(reader, "text");
            t.CategoryId = GetInt64(reader, "category_id");

            catCounter.TryAdd(t.CategoryId, 1);
                            
            t.InCategoryIdx = catCounter[t.CategoryId];
            catCounter[t.CategoryId]++;

            AaDb.DbUiTexts.Add(t.Id, t);
        }
    }
}
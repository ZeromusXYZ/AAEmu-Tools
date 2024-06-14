using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm
    {
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
                            t.or_unit_reqs = GetBool(reader, "or_unit_reqs");

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
                            ShowDbItem(itemIdx);
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
                ShowDbItem(firstResult);
                AddToSearchHistory(cbItemSearch, searchTextLower);
            }
        }

        private void ShowDbItem(long idx)
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

                if (item.item_armors?.id > 0)
                {
                    var requiresArmor = GetItemArmorRequirements(item.item_armors.id);
                    if (requiresArmor.Any())
                        fullDescription += $"\n\n|ni;Requires {(item.item_armors.or_unit_reqs ? "Any" : "All")} of|r";
                    
                    foreach (var req in requiresArmor)
                    {
                        fullDescription += $"\n{req.kind_id} - Value1: {req.value1}, Value2: {req.value2}";
                    }
                }

                FormattedTextToRichtEdit(fullDescription, rtItemDesc);
                lItemLevel.Text = item.level.ToString();
                IconIdToLabel(item.icon_id, itemIcon);
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

        private static string VisualizeDropRate(long dropRate, long dropRateMax, bool alwaysDrop, double diceRate = 1.0, double packRate = 1.0)
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

        private void ShowDbLootByItem(long itemId)
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

        private void ShowLootGroupData(long lootsPackId, long lootsGroup)
        {
            LLootGroupPackID.Text = lootsPackId.ToString();
            LLootPackGroupNumber.Text = lootsGroup.ToString();
            if (AADB.DB_Loot_Pack_Dropping_Npc.Any(lp => lp.Value.loot_pack_id == lootsPackId))
                btnFindLootNpc.Tag = (long)lootsPackId;
            else
                btnFindLootNpc.Tag = (long)0;
        }

        private void ShowDbLootById(long lootId, bool isFirstResult, bool isAltLine, bool isDefaultPack = true, int numberOfNonDefaultPacks = 1)
        {
            if (AADB.DB_Loots.Count <= 0)
            {
                MessageBox.Show($"Unable to search for loot pack {lootId} because no loot data has been loaded");
                return;
            }

            if (numberOfNonDefaultPacks <= 1)
                isDefaultPack = true;

            // var packRate = isDefaultPack ? 1.0 : 1.0 / (double)numberOfNonDefaultPacks;

            var inGroupWeights = new Dictionary<long, long>();

            var loots = AADB.DB_Loots.Where(l => l.Value.loot_pack_id == lootId).OrderBy(l => l.Value.group);
            var lootgroups = AADB.DB_Loot_Groups.Where(l => l.Value.pack_id == lootId).OrderBy(l => l.Value.group_no);

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
            ShowDbItem(long.Parse(val.ToString()));
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

            ShowDbLootByItem(long.Parse(val.ToString()));
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

            ShowDbLootById(searchID, true, false);
        }

        private void TLootSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLootSearch_Click(null, null);
            }
        }

        private void CbItemSearchFilterChanged(object sender, EventArgs e)
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

        private void BtnShowNpcLoot_Click(object sender, EventArgs e)
        {
            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

            var packs = AADB.DB_Loot_Pack_Dropping_Npc.Where(lp => lp.Value.npc_id == searchId).ToList();

            var nonDefaultPackCount = packs.Count(p => p.Value.default_pack == false);

            for (var c = 0; c < packs.Count; c++)
                ShowDbLootById(
                    packs[c].Value.loot_pack_id,
                    (c == 0),
                    (c % 2) != 0,
                    packs[c].Value.default_pack,
                    nonDefaultPackCount);
            if (packs.Count > 0)
                tcViewer.SelectedTab = tpLoot;
        }

        private void BtnFindLootNpc_Click(object sender, EventArgs e)
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
                    ShowDbNpcInfo(z.id);
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
    }
}

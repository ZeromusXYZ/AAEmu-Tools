using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadItems()
    {
        var hasItemPriceTable = AllTableNames.GetValueOrDefault("item_prices") == SQLite.SQLiteFileName;

        if (AllTableNames.GetValueOrDefault("item_grades") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item_grades ORDER BY grade_order ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameItemGrades();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");

                            var colArgbString = GetString(reader, "color_argb");
                            if (int.TryParse(colArgbString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var colArgb))
                                t.ColorArgb = Color.FromArgb(colArgb);

                            /*
                            var colArgbSecondString = GetString(reader, "color_argb_second");
                            if (int.TryParse(colArgbSecondString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var colArgbSecond))
                                t.ColorArgbSecond = Color.FromArgb(colArgbSecond);
                            */

                            t.GradeOrder = GetInt64(reader, "grade_order");
                            t.IconId = GetInt64(reader, "icon_id");
                            t.StatMultiplier = GetInt64(reader, "stat_multiplier");
                            t.RefundMultiplier = GetInt64(reader, "refund_multiplier");
                            t.VarHoldableDps = GetFloat(reader, "var_holdable_dps");
                            t.VarHoldableArmor = GetFloat(reader, "var_holdable_armor");
                            t.VarHoldableMagicDps = GetFloat(reader, "var_holdable_magic_dps");
                            t.VarWearableArmor = GetFloat(reader, "var_wearable_armor");
                            t.VarWearableMagicResistance = GetFloat(reader, "var_wearable_magic_resistance");
                            t.VarHoldableHealDps = GetFloat(reader, "var_holdable_heal_dps");
                            t.DurabilityValue = GetFloat(reader, "durability_value");
                            t.UpgradeRatio = GetInt64(reader, "upgrade_ratio");
                            /*
                            t.GradeEnchantSuccessRatio = GetInt64(reader, "grade_enchant_success_ratio");
                            t.GradeEnchantGreatSuccessRatio = GetInt64(reader, "grade_enchant_great_success_ratio");
                            t.GradeEnchantBreakRatio = GetInt64(reader, "grade_enchant_break_ratio");
                            t.GradeEnchantDowngradeRatio = GetInt64(reader, "grade_enchant_downgrade_ratio");
                            t.GradeEnchantCost = GetInt64(reader, "grade_enchant_cost");
                            t.GradeEnchantDowngradeMin = GetInt64(reader, "grade_enchant_downgrade_min");
                            t.GradeEnchantDowngradeMax = GetInt64(reader, "grade_enchant_downgrade_max");
                            t.CurrencyId = GetInt64(reader, "currency_id");
                            */

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "item_grades", "name", t.Name);
                            AaDb.DbItemGrades.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("item_grade_distributions") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item_grade_distributions ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var columns = reader.GetColumnNames();

                        while (reader.Read())
                        {
                            var t = new GameItemGradeDistributions();
                            t.Id = GetInt64(reader, "id");
                            // t.Name = GetString(reader, "name");

                            foreach (var column in columns)
                            {
                                if (column.StartsWith("weight_"))
                                {
                                    if (long.TryParse(column[7..], out var weightId))
                                    {
                                        t.Weights.TryAdd(weightId, GetInt64(reader, $"weight_{weightId}"));
                                    }
                                }
                            }

                            AaDb.DbItemGradeDistributions.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("items") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM items ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameItem();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.CategoryId = GetInt64(reader, "category_id");
                            t.Description = GetString(reader, "description");
                            t.Price = hasItemPriceTable ? 0 : GetInt64(reader, "price");
                            t.Refund = hasItemPriceTable ? 0 : GetInt64(reader, "refund");
                            t.MaxStackSize = GetInt64(reader, "max_stack_size");
                            t.IconId = GetInt64(reader, "icon_id");
                            t.Sellable = GetBool(reader, "sellable");
                            t.FixedGrade = GetInt64(reader, "fixed_grade");
                            t.UseSkillId = GetInt64(reader, "use_skill_id");
                            t.ImplId = GetInt64(reader, "impl_id");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "items", "name", t.Name);
                            t.DescriptionLocalized =
                                AaDb.GetTranslationById(t.Id, "items", "description", t.Description);

                            t.SearchString = t.Name + " " + t.Description + " " + t.NameLocalized + " " +
                                             t.DescriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AaDb.DbItems.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }
    }

    private void LoadItemArmors()
    {
        if (AllTableNames.GetValueOrDefault("item_armors") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item_armors ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameItemArmors();
                            t.Id = GetInt64(reader, "id");
                            t.ItemId = GetInt64(reader, "item_id");
                            t.SlotTypeId = GetInt64(reader, "slot_type_id");
                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");

                            AaDb.DbItemArmors.Add(t.Id, t);
                            if (AaDb.DbItems.TryGetValue(t.ItemId, out var item))
                                item.ItemArmors = t;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }
    }

    private void LoadItemWeapons()
    {
        if (AllTableNames.GetValueOrDefault("item_weapons") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item_weapons ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameItemWeapons();
                            t.Id = GetInt64(reader, "id");
                            t.ItemId = GetInt64(reader, "item_id");
                            t.HoldableId = GetInt64(reader, "holdable_id");
                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");

                            AaDb.DbItemWeapons.Add(t.Id, t);
                            if (AaDb.DbItems.TryGetValue(t.ItemId, out var item))
                                item.ItemWeapons = t;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }
    }

    private void LoadItemCategories()
    {
        if (AllTableNames.GetValueOrDefault("item_categories") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item_categories ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameItemCategories t = new GameItemCategories();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "item_categories", "name", t.Name);

                            AaDb.DbItemsCategories.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }

        cbItemSearchItemCategoryTypeList.DataSource = null;
        cbItemSearchItemCategoryTypeList.Items.Clear();
        List<GameItemCategories> cats = new List<GameItemCategories>();
        cats.Add(new GameItemCategories()); // Blank line
        foreach (var t in AaDb.DbItemsCategories)
            cats.Add(t.Value);
        cbItemSearchItemCategoryTypeList.DataSource = cats;
        cbItemSearchItemCategoryTypeList.DisplayMember = "DisplayListName";
        cbItemSearchItemCategoryTypeList.ValueMember = "DisplayListValue";

        cbItemSearchItemCategoryTypeList.SelectedIndex = 0;
        cbItemSearchItemArmorSlotTypeList.SelectedIndex = 0;
    }

    private void DoItemSearch()
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
            loadform.ShowInfo($"Scanning {AaDb.DbItems.Count} items");
            var i = 0;

            dgvItem.Hide();

            foreach (var item in AaDb.DbItems)
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
                    if (filterType != item.Value.ImplId)
                        addThis = false;
                }

                if (addThis && (cbItemSearchItemCategoryTypeList.SelectedIndex > 0))
                {
                    if (long.TryParse(cbItemSearchItemCategoryTypeList.SelectedValue.ToString(), out var cId))
                        if (cId != item.Value.CategoryId)
                            addThis = false;
                }

                if (addThis && (cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0))
                {
                    if (item.Value.ItemArmors == null)
                        addThis = false;
                    else if (cbItemSearchItemArmorSlotTypeList.SelectedIndex != item.Value.ItemArmors.SlotTypeId)
                    {
                        addThis = false;
                    }
                }

                if (addThis)
                {
                    int line = dgvItem.Rows.Add();
                    var row = dgvItem.Rows[line];
                    long itemIdx = item.Value.Id;
                    if (firstResult < 0)
                        firstResult = itemIdx;
                    row.Cells[0].Value = itemIdx.ToString();
                    row.Cells[1].Value = item.Value.NameLocalized;

                    if (showFirst)
                    {
                        showFirst = false;
                        ShowDbItem(itemIdx);
                    }

                    if ((dgvItem.Rows.Count % 25) == 0)
                        loadform.ShowInfo($"Scanning {i}/{AaDb.DbItems.Count} items");

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
        if (AaDb.DbItems.TryGetValue(idx, out var item))
        {
            lItemID.Text = idx.ToString();
            lItemImplId.Text = ((GameItemImplId)item.ImplId).ToString() + " (" + item.ImplId.ToString() + ")";
            lItemName.Text = item.NameLocalized;
            lItemCategory.Text = AaDb.GetTranslationById(item.CategoryId, "item_categories", "name") + " (" +
                                 item.CategoryId.ToString() + ")";
            var fullDescription = item.DescriptionLocalized;
            if (AaDb.DbSkills.TryGetValue(item.UseSkillId, out var useSkill))
            {
                if (!string.IsNullOrWhiteSpace(fullDescription))
                    fullDescription += "\r\r";
                fullDescription += "[START_DESCRIPTION]" + useSkill.DescriptionLocalized;
            }

            if (item.ItemArmors?.Id > 0)
            {
                var requiresArmor = GetItemArmorRequirements(item.ItemArmors.Id);
                if (requiresArmor.Any())
                    fullDescription += $"\n\n|ni;Equipping this armor requires {(item.ItemArmors.OrUnitReqs ? "Any" : "All")} of the following|r";
                    
                foreach (var req in requiresArmor)
                {
                    fullDescription += $"\n{req.KindId} - Value1: {req.Value1}, Value2: {req.Value2}";
                }
            }

            if (item.ItemWeapons?.Id > 0)
            {
                var requiresWeapon = GetItemWeaponRequirements(item.ItemWeapons.Id);
                if (requiresWeapon.Any())
                    fullDescription += $"\n\n|ni;Equipping this weapon requires {(item.ItemWeapons.OrUnitReqs ? "Any" : "All")} of the following|r";

                foreach (var req in requiresWeapon)
                {
                    fullDescription += $"\n{req.KindId} - Value1: {req.Value1}, Value2: {req.Value2}";
                }
            }

            FormattedTextToRichtEdit(fullDescription, rtItemDesc);
            lItemLevel.Text = item.Level.ToString();
            IconIdToLabel(item.IconId, itemIcon);
            btnFindItemSkill.Enabled = true; // (item.use_skill_id > 0);
            var gmAddItem = "/item add self " + item.Id.ToString();
            gmAddItem += " " + item.MaxStackSize.ToString();
            if (item.FixedGrade >= 0)
                gmAddItem += " " + item.FixedGrade.ToString();
            lItemTags.Text = TagsAsString(idx, AaDb.DbTaggedItems);
            lItemAddGMCommand.Text = gmAddItem;

            ShowSelectedData("items", "(id = " + idx.ToString() + ")", "id ASC");
        }
        else
        {
            lItemID.Text = idx.ToString();
            lItemImplId.Text = "";
            lItemName.Text = @"<not found>";
            lItemCategory.Text = "";
            rtItemDesc.Clear();
            lItemLevel.Text = "";
            itemIcon.Text = @"???";
            btnFindItemSkill.Enabled = false;
            lItemTags.Text = @"???";
            lItemAddGMCommand.Text = @"/item add self ???";
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
        if (AaDb.DbLoots.Count <= 0)
        {
            MessageBox.Show($"Unable to search for Item {itemId} because no loot data has been loaded");
            return;
        }
        var groupWeights = new Dictionary<long, long>();

        var itemLoots = AaDb.DbLoots.Where(l => l.Value.ItemId == itemId);

        dgvLoot.Rows.Clear();
        foreach (var itemLoot in itemLoots)
        {

            var loots = AaDb.DbLoots.Where(l => (l.Value.LootPackId == itemLoot.Value.LootPackId) && (l.Value.Group == itemLoot.Value.Group));
            // Get Loot weights

            long dropRateTotal = 0;
            foreach (var loot in loots)
                dropRateTotal += loot.Value.DropRate;

            int line = dgvLoot.Rows.Add();
            var row = dgvLoot.Rows[line];

            row.Cells[0].Value = itemLoot.Value.Id.ToString();
            row.Cells[1].Value = itemLoot.Value.LootPackId.ToString();
            row.Cells[2].Value = itemLoot.Value.ItemId.ToString();
            row.Cells[3].Value = AaDb.GetTranslationById(itemLoot.Value.ItemId, "items", "name");
            row.Cells[4].Value = VisualizeDropRate(itemLoot.Value.DropRate, dropRateTotal, itemLoot.Value.AlwaysDrop);
            row.Cells[5].Value = VisualizeAmounts(itemLoot.Value.MinAmount, itemLoot.Value.MaxAmount, itemLoot.Value.ItemId);
            row.Cells[6].Value = itemLoot.Value.GradeId.ToString();
            row.Cells[7].Value = itemLoot.Value.AlwaysDrop.ToString();
            row.Cells[8].Value = itemLoot.Value.Group.ToString();
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
        if (AaDb.DbLootPackDroppingNpc.Any(lp => lp.Value.LootPackId == lootsPackId))
            btnFindLootNpc.Tag = (long)lootsPackId;
        else
            btnFindLootNpc.Tag = (long)0;
    }

    private void ShowDbLootById(long lootId, bool isFirstResult, bool isAltLine, bool isDefaultPack = true, int numberOfNonDefaultPacks = 1)
    {
        if (AaDb.DbLoots.Count <= 0)
        {
            MessageBox.Show($"Unable to search for loot pack {lootId} because no loot data has been loaded");
            return;
        }

        if (numberOfNonDefaultPacks <= 1)
            isDefaultPack = true;

        // var packRate = isDefaultPack ? 1.0 : 1.0 / (double)numberOfNonDefaultPacks;

        var inGroupWeights = new Dictionary<long, long>();

        var loots = AaDb.DbLoots.Where(l => l.Value.LootPackId == lootId).OrderBy(l => l.Value.Group);
        var lootgroups = AaDb.DbLootGroups.Where(l => l.Value.PackId == lootId).OrderBy(l => l.Value.GroupNo);

        var withVal = lootgroups.Where(g => g.Value.DropRate > 1);
        var groupsDropRateTotal = withVal.Sum(g => g.Value.DropRate);
        //var groupsDropRateTotal = lootgroups.Sum(g => g.Value.drop_rate);
        if (groupsDropRateTotal <= 0)
            groupsDropRateTotal = 0;

        // Get Loot weights
        foreach (var loot in loots)
        {
            long newVal = 0;
            if (inGroupWeights.TryGetValue(loot.Value.Group, out var weight))
            {
                newVal = weight;
                inGroupWeights.Remove(loot.Value.Group);
            }

            newVal += loot.Value.DropRate;
            inGroupWeights.Add(loot.Value.Group, newVal);
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
            if (!inGroupWeights.TryGetValue(loot.Group, out var dropRateTotal))
                dropRateTotal = 1;

            if (dropRateTotal == 0)
                dropRateTotal = 1;

            if (lastGroup != loot.Group)
            {
                lastGroup = loot.Group;
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
            var dices = AaDb.DbLootActAbilityGroups.Where(l =>
                (l.Value.LootPackId == loot.LootPackId) && (l.Value.LootGroupId == loot.Group));
            foreach (var dice in dices)
            {
                var diceAverage = (dice.Value.MinDice + dice.Value.MaxDice) / 2;
                diceRate = diceAverage / 10000.0;
            }

            var groupsDropRate = lootgroups.FirstOrDefault(g => g.Value.GroupNo == loot.Group).Value?.DropRate ?? 0;
            var packRate = isDefaultPack || (groupsDropRate <= 1) ? 1.0 : groupsDropRate / groupsDropRateTotal;
            // TODO: Fix me

            row.Cells[0].Value = loot.Id.ToString();
            row.Cells[1].Value = loot.LootPackId.ToString();
            row.Cells[2].Value = loot.ItemId.ToString();
            row.Cells[3].Value = AaDb.GetTranslationById(loot.ItemId, "items", "name");
            row.Cells[4].Value = VisualizeDropRate(loot.DropRate, dropRateTotal, loot.AlwaysDrop, diceRate, packRate);
            row.Cells[5].Value = VisualizeAmounts(loot.MinAmount, loot.MaxAmount, loot.ItemId);
            // row.Cells[5].Value = loot.max_amount == loot.min_amount ? loot.min_amount.ToString() : loot.min_amount.ToString() + "~" + loot.max_amount.ToString();
            row.Cells[6].Value = loot.GradeId.ToString();
            row.Cells[7].Value = loot.AlwaysDrop.ToString();
            row.Cells[8].Value = loot.Group.ToString();
            row.Cells[9].Value = groupsDropRate.ToString() + " / " + groupsDropRateTotal.ToString();
        }
    }

    private void DoItemSelectionChanged()
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

    private void LoadLoots()
    {
        using (var connection = SQLite.CreateConnection())
        {
            if (AllTableNames.GetValueOrDefault("loot_groups") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loot_groups ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootGroup();
                            t.Id = GetInt64(reader, "id");
                            t.PackId = GetInt64(reader, "pack_id");
                            t.GroupNo = GetInt64(reader, "group_no");
                            t.DropRate = GetInt64(reader, "drop_rate");
                            t.ItemGradeDistributionId = GetInt64(reader, "item_grade_distribution_id");

                            AaDb.DbLootGroups.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("loots") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loots ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLoot();
                            t.Id = GetInt64(reader, "id");
                            t.Group = GetInt64(reader, "group");
                            t.DropRate = GetInt64(reader, "drop_rate");
                            t.GradeId = GetInt64(reader, "grade_id");
                            t.ItemId = GetInt64(reader, "item_id");
                            t.LootPackId = GetInt64(reader, "loot_pack_id");
                            t.MinAmount = GetInt64(reader, "min_amount");
                            t.MaxAmount = GetInt64(reader, "max_amount");
                            t.AlwaysDrop = GetBool(reader, "always_drop");

                            AaDb.DbLoots.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // NPC Loot References
            if (AllTableNames.GetValueOrDefault("loot_pack_dropping_npcs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loot_pack_dropping_npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootPackDroppingNpc();
                            t.Id = GetInt64(reader, "id");
                            t.NpcId = GetInt64(reader, "npc_id");
                            t.LootPackId = GetInt64(reader, "loot_pack_id");
                            t.DefaultPack = GetBool(reader, "default_pack");

                            AaDb.DbLootPackDroppingNpc.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("loot_actability_groups") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loot_actability_groups ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameLootActAbilityGroup();
                            t.Id = GetInt64(reader, "id");
                            t.LootPackId = GetInt64(reader, "loot_pack_id");
                            t.LootGroupId = GetInt64(reader, "loot_group_id");
                            t.MaxDice = GetInt64(reader, "max_dice");
                            t.MinDice = GetInt64(reader, "min_dice");

                            AaDb.DbLootActAbilityGroups.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }
    }

    private void DoFindItemInLoot()
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

    private void DoLootSelectionChanged()
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

    private void DoLootSearch()
    {
        string searchText = tLootSearch.Text;
        if (searchText == string.Empty)
            return;
        long searchID;
        if (!long.TryParse(searchText, out searchID))
            return;

        ShowDbLootById(searchID, true, false);
    }

    private void DoShowNpcLoot(long searchId)
    {
        var packs = AaDb.DbLootPackDroppingNpc.Where(lp => lp.Value.NpcId == searchId).ToList();

        var nonDefaultPackCount = packs.Count(p => p.Value.DefaultPack == false);

        for (var c = 0; c < packs.Count; c++)
            ShowDbLootById(
                packs[c].Value.LootPackId,
                (c == 0),
                (c % 2) != 0,
                packs[c].Value.DefaultPack,
                nonDefaultPackCount);
        if (packs.Count > 0)
            tcViewer.SelectedTab = tpLoot;
    }

    private void DoFindLootNpc(long searchId)
    {

        var packs = AaDb.DbLootPackDroppingNpc.Where(lp => lp.Value.LootPackId == searchId).ToList();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvNPCs.Rows.Clear();
        int c = 0;
        foreach (var t in packs)
        {
            if (!AaDb.DbNpCs.TryGetValue(t.Value.NpcId, out var z))
                continue;

            var line = dgvNPCs.Rows.Add();
            var row = dgvNPCs.Rows[line];

            row.Cells[0].Value = z.Id.ToString();
            row.Cells[1].Value = z.NameLocalized;
            row.Cells[2].Value = z.Level.ToString();
            row.Cells[3].Value = z.NpcKindId.ToString();
            row.Cells[4].Value = z.NpcGradeId.ToString();
            row.Cells[5].Value = AaDb.GetFactionName(z.FactionId, true);
            row.Cells[6].Value = "???";

            if (c == 0)
            {
                ShowDbNpcInfo(z.Id);
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
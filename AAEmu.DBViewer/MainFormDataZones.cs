using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm
    {
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

            // Conflict Zones
            sql = "SELECT * FROM conflict_zones ORDER BY zone_group_id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_ConflictZones.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameConflictZones();
                            t.zone_group_id = GetInt64(reader, "zone_group_id");
                            t.num_kills_0 = GetInt64(reader, "num_kills_0");
                            t.num_kills_1 = GetInt64(reader, "num_kills_1");
                            t.num_kills_2 = GetInt64(reader, "num_kills_2");
                            t.num_kills_3 = GetInt64(reader, "num_kills_3");
                            t.num_kills_4 = GetInt64(reader, "num_kills_4");
                            t.no_kill_min_0 = GetInt64(reader, "no_kill_min_0");
                            t.no_kill_min_1 = GetInt64(reader, "no_kill_min_1");
                            t.no_kill_min_2 = GetInt64(reader, "no_kill_min_2");
                            t.no_kill_min_3 = GetInt64(reader, "no_kill_min_3");
                            t.no_kill_min_4 = GetInt64(reader, "no_kill_min_4");
                            t.conflict_min = GetInt64(reader, "conflict_min");
                            t.war_min = GetInt64(reader, "war_min");
                            t.peace_min = GetInt64(reader, "peace_min");
                            t.peace_protected_faction_id = GetInt64(reader, "peace_protected_faction_id");
                            t.nuia_return_point_id = GetInt64(reader, "nuia_return_point_id");
                            t.harihara_return_point_id = GetInt64(reader, "harihara_return_point_id");
                            t.war_tower_def_id = GetInt64(reader, "war_tower_def_id");
                            t.peace_tower_def_id = GetInt64(reader, "peace_tower_def_id");
                            t.closed = GetBool(reader, "closed");

                            AADB.DB_ConflictZones.Add(t.zone_group_id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadZoneGroupBannedTags()
        {
            var sql = "SELECT * FROM zone_group_banned_tags ORDER BY id ASC";

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

        private static GameZone_Groups GetZoneGroupById(long zoneGroupId)
        {
            if (AADB.DB_Zone_Groups.TryGetValue(zoneGroupId, out var zg))
                return zg;
            else
                return null;
        }

        private void ShowDbZone(long idx)
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

                var zg = GetZoneGroupById(zone.group_id);
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

        private void BtnZonesSearch_Click(object sender, EventArgs e)
        {
            var searchText = tZonesSearch.Text.ToLower();
            if (searchText == string.Empty)
                return;
            if (!long.TryParse(searchText, out var searchId))
                searchId = -1;

            var first = true;
            dgvZones.Rows.Clear();
            foreach (var t in AADB.DB_Zones)
            {
                var z = t.Value;
                if ((z.id == searchId) || (z.zone_key == searchId) || (z.group_id == searchId) ||
                    (z.SearchString.IndexOf(searchText, StringComparison.InvariantCulture) >= 0))
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
                        ShowDbZone(z.id);
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
            var first = true;
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
                    ShowDbZone(z.id);
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

            ShowDbZone(long.Parse(val.ToString()));
        }

        private void BtnZoneGroupsFishLoot_Click(object sender, EventArgs e)
        {
            if ((sender != null) && (sender is Button))
            {
                if (sender is Button b)
                {
                    var lootId = (long)b.Tag;

                    if (lootId > 0)
                    {
                        ShowDbLootById(lootId, true, false);
                        tcViewer.SelectedTab = tpLoot;
                    }
                }
            }
        }

        private List<MapSpawnLocation> GetNpcSpawnsInZoneGroup(long zoneGroupId, bool uniqueOnly = false, List<long> filterByIDs = null, string instanceName = "main_world")
        {
            List<MapSpawnLocation> res = new List<MapSpawnLocation>();
            var zg = GetZoneGroupById(zoneGroupId);
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

        private void BtnFindNpcsInZone_Click(object sender, EventArgs e)
        {
            List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

            if ((sender != null) && (sender is Button))
            {
                Button b = (sender as Button);
                if (b != null)
                {
                    long ZoneGroupId = (long)b.Tag;
                    var zg = GetZoneGroupById(ZoneGroupId);
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

                        npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.id, false));

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

        private void BtnFindQuestsInZone_Click(object sender, EventArgs e)
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

        private void LabelZoneGroupRestrictions_Click(object sender, EventArgs e)
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

        private List<MapSpawnLocation> GetDoodadSpawnsInZoneGroup(long zoneGroupId, bool uniqueOnly = false, long filterByID = -1, string instanceName = "main_world")
        {
            List<MapSpawnLocation> res = new List<MapSpawnLocation>();
            var zg = GetZoneGroupById(zoneGroupId);
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

        private void BtnShowDoodadOnMap_Click(object sender, EventArgs e)
        {
            PrepareWorldXml(false);
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

        private void BtnFindAllSubZone_Click(object sender, EventArgs e)
        {
            PrepareWorldXml(false);
            var allAreas = new List<MapViewPath>();

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            foreach (var zv in AADB.DB_Zones)
                AddSubZones(ref allAreas, zv.Value);

            if (allAreas.Count <= 0)
                MessageBox.Show("No subzone found ?");
            else
            {
                var map = MapViewForm.GetMap();
                map.Show();

                // We no longer have the housing by zone button, so we no longer need to ask, just clear it
                //if (map.GetHousingCount() > 0)
                //    if (MessageBox.Show("Keep current housing areas ?", "Add Housing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                map.ClearSubZone();

                foreach (var p in allAreas)
                {
                    map.AddSubZone(p);
                }


                map.tsbShowSubzone.Checked = true;
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;


        }

        private void BtnFindDoodadsInZone_Click(object sender, EventArgs e)
        {
            var doodadList = new List<MapSpawnLocation>();
            if (sender is Button b)
            {
                var zoneGroupId = (long)b.Tag;
                var zg = GetZoneGroupById(zoneGroupId);
                if (zg != null)
                {
                    zg.GamePakZoneDoodadsDat();
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
}

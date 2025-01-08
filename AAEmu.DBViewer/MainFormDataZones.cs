using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm
    {
        private void LoadZones()
        {
            // Zones
            if (AllTableNames.GetValueOrDefault("zones") == SQLite.SQLiteFileName)
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM zones ORDER BY id ASC";
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
                                t.Id = GetInt64(reader, "id");
                                t.Name = GetString(reader, "name");
                                t.ZoneKey = GetInt64(reader, "zone_key");
                                t.GroupId = GetInt64(reader, "group_id");
                                t.Closed = GetBool(reader, "closed");
                                t.DisplayText = GetString(reader, "display_text");
                                t.FactionId = GetInt64(reader, "faction_id");
                                t.ZoneClimateId = GetInt64(reader, "zone_climate_id");

                                if (hasAbox_show)
                                    t.AboxShow = GetBool(reader, "abox_show");
                                else
                                    t.AboxShow = false;

                                if (t.DisplayText != string.Empty)
                                    t.DisplayTextLocalized =
                                        AaDb.GetTranslationById(t.Id, "zones", "display_text", t.DisplayText);
                                else
                                    t.DisplayTextLocalized = "";
                                t.SearchString = t.Name + " " + t.DisplayText + " " + t.DisplayTextLocalized;
                                t.SearchString = t.SearchString.ToLower();

                                AaDb.DbZones.Add(t.Id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;

                        }
                    }
                }
            }

            // Zone_Groups
            if (AllTableNames.GetValueOrDefault("zone_groups") == SQLite.SQLiteFileName)
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM zone_groups ORDER BY id ASC";
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;
                            var columnNames = reader.GetColumnNames();
                            var hasImageMap = (columnNames.IndexOf("image_map") >= 0);

                            while (reader.Read())
                            {
                                GameZoneGroups t = new GameZoneGroups();
                                t.Id = GetInt64(reader, "id");
                                t.Name = GetString(reader, "name");
                                var x = GetFloat(reader, "x");
                                var y = GetFloat(reader, "y");
                                var w = GetFloat(reader, "w");
                                var h = GetFloat(reader, "h");
                                t.PosAndSize = new RectangleF(x, y, w, h);
                                t.ImageMap = hasImageMap ? GetInt64(reader, "image_map") : 0;
                                t.SoundId = GetInt64(reader, "sound_id");
                                t.TargetId = GetInt64(reader, "target_id");
                                t.DisplayText = GetString(reader, "display_text");
                                t.FactionChatRegionId = GetInt64(reader, "faction_chat_region_id");
                                t.SoundPackId = GetInt64(reader, "sound_pack_id");
                                t.PirateDesperado = GetBool(reader, "pirate_desperado");
                                t.FishingSeaLootPackId = GetInt64(reader, "fishing_sea_loot_pack_id");
                                t.FishingLandLootPackId = GetInt64(reader, "fishing_land_loot_pack_id");
                                t.BuffId = GetInt64(reader, "buff_id");

                                if (t.DisplayText != string.Empty)
                                    t.DisplayTextLocalized = AaDb.GetTranslationById(t.Id, "zone_groups", "display_text",
                                        t.DisplayText);
                                else
                                    t.DisplayTextLocalized = "";
                                t.SearchString = t.Name + " " + t.DisplayText + " " + t.DisplayTextLocalized;
                                t.SearchString = t.SearchString.ToLower();

                                AaDb.DbZoneGroups.Add(t.Id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;

                        }
                    }
                }
            }

            // World_Groups
            if (AllTableNames.GetValueOrDefault("world_groups") == SQLite.SQLiteFileName)
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM world_groups ORDER BY id ASC";
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;
                            var columnNames = reader.GetColumnNames();
                            var hasImageMap = (columnNames.IndexOf("image_map") >= 0);
                            var hasMapTargetType = (columnNames.Contains("map_target_type"));

                            while (reader.Read())
                            {
                                GameWorldGroups t = new GameWorldGroups();
                                t.Id = GetInt64(reader, "id");
                                t.Name = GetString(reader, "name");
                                int x = (int)GetInt64(reader, "x");
                                int y = (int)GetInt64(reader, "y");
                                int w = (int)GetInt64(reader, "w");
                                int h = (int)GetInt64(reader, "h");
                                int ix = (int)GetInt64(reader, "image_x");
                                int iy = (int)GetInt64(reader, "image_y");
                                int iw = (int)GetInt64(reader, "image_w");
                                int ih = (int)GetInt64(reader, "image_h");
                                t.PosAndSize = new Rectangle(x, y, w, h);
                                t.ImagePosAndSize = new Rectangle(ix, iy, iw, ih);
                                t.ImageMap = hasImageMap ? GetInt64(reader, "image_map") : 0;
                                if (hasMapTargetType)
                                {
                                    // Newer format
                                    t.MapTargetId = GetInt64(reader, "map_target_id");
                                    t.MapTargetType = GetString(reader, "map_target_type");
                                }
                                else
                                {
                                    // Old format
                                    t.TargetId = GetInt64(reader, "target_id");
                                }

                                t.SearchString = t.Name.ToLower();

                                AaDb.DbWorldGroups.Add(t.Id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;

                        }
                    }
                }
            }

            // Conflict Zones
            if (AllTableNames.GetValueOrDefault("conflict_zones") == SQLite.SQLiteFileName)
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM conflict_zones ORDER BY zone_group_id ASC";
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;

                            while (reader.Read())
                            {
                                var t = new GameConflictZones();
                                t.ZoneGroupId = GetInt64(reader, "zone_group_id");
                                t.NumKills0 = GetInt64(reader, "num_kills_0");
                                t.NumKills1 = GetInt64(reader, "num_kills_1");
                                t.NumKills2 = GetInt64(reader, "num_kills_2");
                                t.NumKills3 = GetInt64(reader, "num_kills_3");
                                t.NumKills4 = GetInt64(reader, "num_kills_4");
                                t.NoKillMin0 = GetInt64(reader, "no_kill_min_0");
                                t.NoKillMin1 = GetInt64(reader, "no_kill_min_1");
                                t.NoKillMin2 = GetInt64(reader, "no_kill_min_2");
                                t.NoKillMin3 = GetInt64(reader, "no_kill_min_3");
                                t.NoKillMin4 = GetInt64(reader, "no_kill_min_4");
                                t.ConflictMin = GetInt64(reader, "conflict_min");
                                t.WarMin = GetInt64(reader, "war_min");
                                t.PeaceMin = GetInt64(reader, "peace_min");
                                t.PeaceProtectedFactionId = GetInt64(reader, "peace_protected_faction_id");
                                t.NuiaReturnPointId = GetInt64(reader, "nuia_return_point_id");
                                t.HariharaReturnPointId = GetInt64(reader, "harihara_return_point_id");
                                t.WarTowerDefId = GetInt64(reader, "war_tower_def_id");
                                t.PeaceTowerDefId = GetInt64(reader, "peace_tower_def_id");
                                t.Closed = GetBool(reader, "closed");

                                AaDb.DbConflictZones.Add(t.ZoneGroupId, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }
            }
        }

        private void LoadZoneGroupBannedTags()
        {
            if (AllTableNames.GetValueOrDefault("zone_group_banned_tags") == SQLite.SQLiteFileName)
            {
                using var connection = SQLite.CreateConnection();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM zone_group_banned_tags ORDER BY id ASC";
                command.Prepare();
                using var reader = new SQLiteWrapperReader(command.ExecuteReader());
                Application.UseWaitCursor = true;
                Cursor = Cursors.WaitCursor;
                var colNames = reader.GetColumnNames();

                while (reader.Read())
                {
                    if (!reader.IsDBNull("id"))
                    {
                        GameZoneGroupBannedTags t = new GameZoneGroupBannedTags();
                        t.Id = GetInt64(reader, "id");
                        t.ZoneGroupId = GetInt64(reader, "zone_group_id");
                        t.TagId = GetInt64(reader, "tag_id");
                        if (colNames.Contains("banned_periods_id"))
                            t.BannedPeriodsId = GetInt64(reader, "banned_periods_id");
                        else if (colNames.Contains("banned_periods"))
                            t.BannedPeriodsId = GetInt64(reader, "banned_periods");
                        else
                            t.BannedPeriodsId = 0;
                        if (colNames.Contains("usage"))
                            t.Usage = GetString(reader, "usage");
                        else
                            t.Usage = string.Empty;
                        AaDb.DbZoneGroupBannedTags.Add(t.Id, t);
                    }
                }

                Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
            }
        }

        private static GameZoneGroups GetZoneGroupById(long zoneGroupId)
        {
            if (AaDb.DbZoneGroups.TryGetValue(zoneGroupId, out var zg))
                return zg;
            else
                return null;
        }

        private void ShowDbZone(long idx)
        {
            bool blank_zone_groups = false;
            bool blank_world_groups = false;
            if (AaDb.DbZones.TryGetValue(idx, out var zone))
            {
                // From Zones
                lZoneID.Text = zone.Id.ToString();
                if (zone.Closed)
                    lZoneDisplayName.Text = zone.DisplayTextLocalized + " (closed)";
                else
                    lZoneDisplayName.Text = zone.DisplayTextLocalized;
                lZoneName.Text = zone.Name;
                lZoneKey.Text = zone.ZoneKey.ToString();
                lZoneGroupID.Text = zone.GroupId.ToString();
                lZoneFactionID.Text = zone.FactionId.ToString();
                btnFindQuestsInZone.Tag = zone.Id;
                btnFindQuestsInZone.Enabled = (zone.Id > 0);
                btnFindTransferPathsInZone.Tag = zone.ZoneKey;

                var zg = GetZoneGroupById(zone.GroupId);
                // From Zone_Groups
                if (zg != null)
                {
                    lZoneGroupsDisplayName.Text = zg.DisplayTextLocalized;
                    lZoneGroupsName.Text = zg.Name;
                    string zoneNPCFile = zg.GamePakZoneNpCsDat();
                    if ((Pak.IsOpen) && (Pak.FileExists(zoneNPCFile)))
                    {
                        btnFindNPCsInZone.Tag = zg.Id;
                        btnFindNPCsInZone.Enabled = true;
                    }
                    else
                    {
                        btnFindNPCsInZone.Tag = null;
                        btnFindNPCsInZone.Enabled = false;
                    }

                    string zoneDoodadFile = zg.GamePakZoneDoodadsDat();
                    if ((Pak.IsOpen) && (Pak.FileExists(zoneDoodadFile)))
                    {
                        btnFindDoodadsInZone.Tag = zg.Id;
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
                    lZoneGroupsImageMap.Text = zg.ImageMap.ToString();
                    lZoneGroupsSoundID.Text = zg.SoundId.ToString();
                    lZoneGroupsSoundPackID.Text = zg.SoundPackId.ToString();
                    lZoneGroupsTargetID.Text = zg.TargetId.ToString();
                    lZoneGroupsFactionChatID.Text = zg.FactionChatRegionId.ToString();
                    lZoneGroupsPirateDesperado.Text = zg.PirateDesperado.ToString();
                    lZoneGroupsBuffID.Text = zg.BuffId.ToString();
                    if (zg.FishingSeaLootPackId > 0)
                    {
                        btnZoneGroupsSaltWaterFish.Tag = zg.FishingSeaLootPackId;
                        btnZoneGroupsSaltWaterFish.Enabled = true;
                    }
                    else
                    {
                        btnZoneGroupsSaltWaterFish.Tag = 0;
                        btnZoneGroupsSaltWaterFish.Enabled = false;
                    }

                    if (zg.FishingLandLootPackId > 0)
                    {
                        btnZoneGroupsFreshWaterFish.Tag = zg.FishingLandLootPackId;
                        btnZoneGroupsFreshWaterFish.Enabled = true;
                    }
                    else
                    {
                        btnZoneGroupsFreshWaterFish.Tag = 0;
                        btnZoneGroupsFreshWaterFish.Enabled = false;
                    }

                    var bannedTagsCount = 0;
                    var bannedInfo = string.Empty;
                    foreach (var b in AaDb.DbZoneGroupBannedTags)
                    {
                        if (b.Value.ZoneGroupId == zg.Id)
                        {
                            bannedTagsCount++;
                            if (AaDb.DbTags.TryGetValue(b.Value.TagId, out var tag))
                            {
                                bannedInfo += tag.Id + " - " + tag.NameLocalized + "\r\n";
                            }
                            else
                            {
                                bannedInfo += tag.Id + " - [Unknown Tag]\r\n";
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
                        labelZoneGroupRestrictions.Tag = zg.Id;
                        mainFormToolTip.ToolTipTitle = "Banned ZoneGroup Tags";
                        mainFormToolTip.SetToolTip(labelZoneGroupRestrictions, bannedInfo);
                    }

                    // From World_Group
                    if (AaDb.DbWorldGroups.TryGetValue(zg.TargetId, out var wg))
                    {
                        lWorldGroupName.Text = wg.Name;
                        lWorldGroupSizeAndPos.Text = "X:" + wg.PosAndSize.X.ToString() + " Y:" +
                                                     wg.PosAndSize.Y.ToString() + "  W:" +
                                                     wg.PosAndSize.Width.ToString() + " H:" +
                                                     wg.PosAndSize.Height.ToString();
                        lWorldGroupImageSizeAndPos.Text = "X:" + wg.ImagePosAndSize.X.ToString() + " Y:" +
                                                          wg.ImagePosAndSize.Y.ToString() + "  W:" +
                                                          wg.ImagePosAndSize.Width.ToString() + " H:" +
                                                          wg.ImagePosAndSize.Height.ToString();
                        lWorldGroupImageMap.Text = wg.ImageMap.ToString();
                        lWorldGroupTargetID.Text = wg.TargetId.ToString();
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
                var inst = MapViewWorldXML.FindInstanceByZoneKey(zone.ZoneKey);
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
            foreach (var t in AaDb.DbZones)
            {
                var z = t.Value;
                if ((z.Id == searchId) || (z.ZoneKey == searchId) || (z.GroupId == searchId) ||
                    (z.SearchString.IndexOf(searchText, StringComparison.InvariantCulture) >= 0))
                {
                    var line = dgvZones.Rows.Add();
                    var row = dgvZones.Rows[line];

                    row.Cells[0].Value = z.Id.ToString();
                    row.Cells[1].Value = z.Name;
                    row.Cells[2].Value = z.GroupId.ToString();
                    row.Cells[3].Value = z.ZoneKey.ToString();
                    row.Cells[4].Value = z.DisplayTextLocalized;
                    row.Cells[5].Value = z.Closed.ToString();

                    if (first)
                    {
                        first = false;
                        ShowDbZone(z.Id);
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
            foreach (var t in AaDb.DbZones)
            {
                var z = t.Value;
                var line = dgvZones.Rows.Add();
                var row = dgvZones.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.Name;
                row.Cells[2].Value = z.GroupId.ToString();
                row.Cells[3].Value = z.ZoneKey.ToString();
                row.Cells[4].Value = z.DisplayTextLocalized;
                row.Cells[5].Value = z.Closed.ToString();

                if (first)
                {
                    first = false;
                    ShowDbZone(z.Id);
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
            if ((zg != null) && (Pak.IsOpen) && (Pak.FileExists(zg.GamePakZoneNpCsDat(instanceName))))
            {
                // Open .dat file and read it's contents
                using (var fs = Pak.ExportFileAsStream(zg.GamePakZoneNpCsDat(instanceName)))
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
                        ZoneGroupFile = zg.GamePakZoneNpCsDat();

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

                        npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.Id, false));

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
                                    if (AaDb.DbNpCs.TryGetValue(npc.id, out var z))
                                    {
                                        var line = dgvNPCs.Rows.Add();
                                        var row = dgvNPCs.Rows[line];

                                        row.Cells[0].Value = z.Id.ToString();
                                        row.Cells[1].Value = z.NameLocalized;
                                        row.Cells[2].Value = z.Level.ToString();
                                        row.Cells[3].Value = z.NpcKindId.ToString();
                                        row.Cells[4].Value = z.NpcGradeId.ToString();
                                        row.Cells[5].Value = AaDb.GetFactionName(z.FactionId, true);

                                        var npc_spawner_npcs = AaDb.GetNpcSpawnerNpcsByNpcId(z.Id);

                                        if (npc_spawner_npcs.Count > 0)
                                        {
                                            if (npc_spawner_npcs.Count == 1)
                                                row.Cells[6].Value = string.Format("Used by 1 spawner, {0}",
                                                    npc_spawner_npcs[0].NpcSpawnerId);
                                            else
                                            {
                                                var ids = npc_spawner_npcs.Select(a => a.NpcSpawnerId).ToList();
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
                                            z.NameLocalized + " (" + npc.id.ToString() + ")",
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

                    foreach (var quest in AaDb.DbQuestContexts)
                    {
                        var q = quest.Value;
                        if (q.ZoneId == zoneid)
                        {
                            if (first)
                            {
                                first = false;
                                dgvQuests.Rows.Clear();
                            }

                            var line = dgvQuests.Rows.Add();
                            var row = dgvQuests.Rows[line];

                            row.Cells[0].Value = q.Id.ToString();
                            row.Cells[1].Value = q.NameLocalized;
                            row.Cells[2].Value = q.Level.ToString();
                            if (AaDb.DbZones.TryGetValue(q.ZoneId, out var z))
                            {
                                if (AaDb.DbZoneGroups.TryGetValue(z.GroupId, out var zg))
                                    row.Cells[3].Value = zg.DisplayTextLocalized;
                                else
                                    row.Cells[3].Value = z.DisplayTextLocalized;
                            }
                            else
                                row.Cells[3].Value = q.ZoneId.ToString();

                            if (AaDb.DbQuestCategories.TryGetValue(q.CategoryId, out var qc))
                                row.Cells[4].Value = qc.NameLocalized;
                            else
                                row.Cells[4].Value = q.CategoryId.ToString();
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
            foreach (var b in AaDb.DbZoneGroupBannedTags)
            {
                if (b.Value.ZoneGroupId == zoneGroupId)
                {
                    if (AaDb.DbTags.TryGetValue(b.Value.TagId, out var tag))
                    {
                        bannedInfo += tag.Id + " - " + tag.NameLocalized + "\r\n";
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
            if ((zg != null) && (Pak.IsOpen) && (Pak.FileExists(zg.GamePakZoneDoodadsDat(instanceName))))
            {
                // Open .dat file and read it's contents
                using (var fs = Pak.ExportFileAsStream(zg.GamePakZoneDoodadsDat(instanceName)))
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
                loading.ShowInfo("Searching in zones: " + AaDb.DbZoneGroups.Count.ToString());
                loading.Show();

                var zoneCount = 0;
                foreach (var zgv in AaDb.DbZoneGroups)
                {
                    var zg = zgv.Value;
                    if (zg != null)
                    {
                        zoneCount++;
                        loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                         AaDb.DbZoneGroups.Count.ToString());
                        doodadList.AddRange(GetDoodadSpawnsInZoneGroup(zg.Id, false));
                    }
                }

                if (doodadList.Count > 0)
                {
                    // Add to NPC list
                    foreach (var doodad in doodadList)
                    {
                        if (doodad.id != searchId)
                            continue;
                        if (AaDb.DbDoodadAlmighties.TryGetValue(doodad.id, out var z))
                        {
                            map.AddPoI(doodad.x, doodad.y, doodad.z,
                                z.NameLocalized + " (" + doodad.id.ToString() + ")",
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

            foreach (var zv in AaDb.DbZones)
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

                    doodadList.AddRange(GetDoodadSpawnsInZoneGroup(zg.Id));

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
                                if (AaDb.DbDoodadAlmighties.TryGetValue(doodad.id, out var z))
                                {
                                    var line = dgvDoodads.Rows.Add();
                                    var row = dgvDoodads.Rows[line];

                                    row.Cells[0].Value = z.Id.ToString();
                                    row.Cells[1].Value = z.NameLocalized;
                                    row.Cells[2].Value = z.MgmtSpawn.ToString();
                                    row.Cells[3].Value = z.GroupId.ToString();
                                    row.Cells[4].Value = z.Percent.ToString();
                                    row.Cells[5].Value = AaDb.GetFactionName(z.FactionId, true);
                                    row.Cells[6].Value = z.ModelKindId.ToString();
                                    row.Cells[7].Value = z.Model.ToString();
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
                                        z.NameLocalized + " (" + doodad.id.ToString() + ")", Color.Yellow, 0f,
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

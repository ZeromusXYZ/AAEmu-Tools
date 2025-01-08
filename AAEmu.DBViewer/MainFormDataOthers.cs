using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml;
using AAEmu.DBViewer.DbDefs;
using AAEmu.DBViewer.JsonData;
using AAEmu.DBViewer.utils;
using AAEmu.Game.Models.Game.World;
using AAEmu.Game.Utils.DB;
using Newtonsoft.Json;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadFactions()
    {
        if (AllTableNames.GetValueOrDefault("system_factions") != SQLite.SQLiteFileName)
            return;

        using var connection = SQLite.CreateConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM system_factions ORDER BY id ASC";
            command.Prepare();
            using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
            {
                var hasDiplomacyLinkId = reader.GetColumnNames().Contains("diplomacy_link_id");
                while (reader.Read())
                {
                    var t = new GameSystemFaction();
                    // Actual DB entries
                    t.Id = GetInt64(reader, "id");
                    t.Name = GetString(reader, "name");
                    t.OwnerName = GetString(reader, "owner_name");
                    t.OwnerTypeId = GetInt64(reader, "owner_type_id");
                    t.OwnerId = GetInt64(reader, "owner_id");
                    t.PoliticalSystemId = GetInt64(reader, "political_system_id");
                    t.MotherId = GetInt64(reader, "mother_id");
                    t.AggroLink = GetBool(reader, "aggro_link");
                    t.GuardHelp = GetBool(reader, "guard_help");
                    t.IsDiplomacyTgt = GetBool(reader, "is_diplomacy_tgt");
                    t.DiplomacyLinkId = hasDiplomacyLinkId ? GetInt64(reader, "diplomacy_link_id") : 0;

                    t.NameLocalized = AaDb.GetTranslationById(t.Id, "system_factions", "name", t.Name);
                    t.SearchString = t.Name + " " + t.NameLocalized + " " + t.OwnerName;
                    t.SearchString = t.SearchString.ToLower();
                    AaDb.DbGameSystemFactions.Add(t.Id, t);
                }
            }
        }

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM system_faction_relations ORDER BY id ASC";
            command.Prepare();
            using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
            {
                while (reader.Read())
                {
                    var t = new GameSystemFactionRelation();
                    // Actual DB entries
                    t.Id = GetInt64(reader, "id");
                    t.Faction1Id = GetInt64(reader, "faction1_id");
                    t.Faction2Id = GetInt64(reader, "faction2_id");
                    t.StateId = GetInt64(reader, "state_id");
                    AaDb.DbGameSystemFactionRelations.Add(t.Id, t);
                }
            }
        }
    }

    private void LoadTags()
    {
        using var connection = SQLite.CreateConnection();
        // Tag Names
        if (AllTableNames.GetValueOrDefault("tags") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
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
                        t.Id = GetInt64(reader, "id");
                        t.Name = GetString(reader, "name");
                        if (hasDesc)
                            t.Desc = GetString(reader, "desc");

                        t.NameLocalized = AaDb.GetTranslationById(t.Id, "tags", "name", t.Name);
                        t.DescLocalized = AaDb.GetTranslationById(t.Id, "tags", "desc", t.Desc);

                        t.SearchString = t.Name + " " + t.NameLocalized + " " + t.Desc + " " + t.DescLocalized;
                        t.SearchString = t.SearchString.ToLower();

                        AaDb.DbTags.Add(t.Id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // Buff Tags
        if (AllTableNames.GetValueOrDefault("tagged_buffs") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
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
                            t.Id = GetInt64(reader, "id");
                            t.TagId = GetInt64(reader, "tag_id");
                            t.TargetId = GetInt64(reader, "buff_id");
                            AaDb.DbTaggedBuffs.Add(t.Id, t);
                        }
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // Items Tags
        if (AllTableNames.GetValueOrDefault("tagged_items") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
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
                            t.Id = GetInt64(reader, "id");
                            t.TagId = GetInt64(reader, "tag_id");
                            t.TargetId = GetInt64(reader, "item_id");
                            AaDb.DbTaggedItems.Add(t.Id, t);
                        }
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // NPC Tags
        if (AllTableNames.GetValueOrDefault("tagged_npcs") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
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
                            t.Id = GetInt64(reader, "id");
                            t.TagId = GetInt64(reader, "tag_id");
                            t.TargetId = GetInt64(reader, "npc_id");
                            AaDb.DbTaggedNpCs.Add(t.Id, t);
                        }
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // Skill Tags
        if (AllTableNames.GetValueOrDefault("tagged_skills") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
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
                            t.Id = GetInt64(reader, "id");
                            t.TagId = GetInt64(reader, "tag_id");
                            t.TargetId = GetInt64(reader, "skill_id");
                            AaDb.DbTaggedSkills.Add(t.Id, t);
                        }
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }
    }

    private void LoadIcons()
    {
        if (AllTableNames.GetValueOrDefault("icons") != SQLite.SQLiteFileName)
            return;

        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM icons ORDER BY id ASC";
        command.Prepare();
        using var reader = new SQLiteWrapperReader(command.ExecuteReader());
        while (reader.Read())
        {
            AaDb.DbIcons.Add(GetInt64(reader, "id"), GetString(reader, "filename"));
        }
    }

    private int IconIdToLabel(long iconId, Label iconImgLabel)
    {
        var cachedImageIndex = -1;
        if (Pak.IsOpen)
        {
            if (AaDb.DbIcons.TryGetValue(iconId, out var iconname))
            {
                var fn = "game/ui/icon/" + iconname;

                if (Pak.FileExists(fn))
                {
                    try
                    {
                        // Is it cached already ?
                        cachedImageIndex = ilIcons.Images.IndexOfKey(iconId.ToString());
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
                            // Load from Pak if not cached
                            var fStream = Pak.ExportFileAsStream(fn);
                            var bmp = Tools.BitmapUtil.ReadDDSFromStream(fStream);

                            if (iconImgLabel != null)
                            {
                                iconImgLabel.Image = bmp;
                                iconImgLabel.Text = "";
                            }

                            // If not loaded into image list yet, do it now
                            ilIcons.Images.Add(iconId.ToString(), bmp);
                            ilMiniIcons.Images.Add(iconId.ToString(), bmp);
                            cachedImageIndex = ilIcons.Images.IndexOfKey(iconId.ToString());
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
                iconImgLabel.Text = iconId.ToString() + "?";
            }
        }
        else if (iconImgLabel != null)
        {
            iconImgLabel.Image = null;
            iconImgLabel.Text = iconId.ToString();
        }

        return cachedImageIndex;
    }

    private static string TagsAsString(long targetId, Dictionary<long, GameTaggedValues> lookupTable)
    {
        var tags = from t in lookupTable
            where t.Value.TargetId == targetId
            select t.Value;
        var s = string.Empty;
        foreach (var t in tags)
        {
            if (s.Length > 0)
                s += " , ";
            if (AaDb.DbTags.TryGetValue(t.TagId, out var taglookup))
                s += taglookup.NameLocalized + " ";
            s += "(" + t.TagId.ToString() + ")";
        }

        return s;
    }

    private void LoadTrades()
    {
        if (AllTableNames.GetValueOrDefault("specialities") != SQLite.SQLiteFileName)
            return;

        var sourceZones = new List<long>();

        using (var connection = SQLite.CreateConnection())
        {
            // Tag Names
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM specialties ORDER BY id ASC";
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        GameSpecialties t = new GameSpecialties();
                        t.Id = GetInt64(reader, "id");
                        t.RowZoneGroupId = GetInt64(reader, "row_zone_group_id");
                        t.ColZoneGroupId = GetInt64(reader, "col_zone_group_id");
                        t.Ratio = GetInt64(reader, "ratio");
                        t.Profit = GetInt64(reader, "profit");
                        t.VendorExist = GetBool(reader, "vendor_exist");

                        AaDb.DbSpecialities.Add(t.Id, t);

                        if (!sourceZones.Contains(t.RowZoneGroupId))
                            sourceZones.Add(t.RowZoneGroupId);
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
            if (AaDb.DbZoneGroups.TryGetValue(z, out var zone))
                lbTradeSource.Items.Add(zone);
        }

    }

    private void DoSearchFaction()
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
        foreach (var t in AaDb.DbGameSystemFactions)
        {
            var z = t.Value;
            if ((z.Id == searchID) || (z.OwnerId == searchID) || (z.MotherId == searchID) ||
                (z.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvFactions.Rows.Add();
                var row = dgvFactions.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.NameLocalized;
                if (z.MotherId != 0)
                {
                    row.Cells[2].Value = AaDb.GetFactionName(z.MotherId, true);
                }
                else
                {
                    row.Cells[2].Value = string.Empty;
                }

                if ((z.OwnerTypeId == 1) && AaDb.DbNpCs.TryGetValue(z.OwnerId, out var ownerNpc))
                {
                    row.Cells[3].Value = ownerNpc.NameLocalized;
                }
                else
                {
                    row.Cells[3].Value = z.OwnerName + " (" + z.OwnerId.ToString() + ")";
                }
                string d = string.Empty;
                if (z.IsDiplomacyTgt)
                    d += "Is Target ";
                if (z.DiplomacyLinkId != 0)
                {
                    d += AaDb.GetFactionName(z.DiplomacyLinkId, true);
                }

                row.Cells[4].Value = d;

                if (first)
                {
                    first = false;
                    ShowDbFaction(z.Id);
                }
            }

        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void DoShowAllFactions()
    {
        bool first = true;
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvFactions.Rows.Clear();
        foreach (var t in AaDb.DbGameSystemFactions)
        {
            var z = t.Value;
            var line = dgvFactions.Rows.Add();
            var row = dgvFactions.Rows[line];

            row.Cells[0].Value = z.Id.ToString();
            row.Cells[1].Value = z.NameLocalized;
            if (z.MotherId != 0)
            {
                row.Cells[2].Value = AaDb.GetFactionName(z.MotherId, true);
            }
            else
            {
                row.Cells[2].Value = string.Empty;
            }

            if ((z.OwnerTypeId == 1) && AaDb.DbNpCs.TryGetValue(z.OwnerId, out var ownerNpc))
            {
                row.Cells[3].Value = ownerNpc.NameLocalized;
            }
            else
            {
                row.Cells[3].Value = z.OwnerName + " (" + z.OwnerId.ToString() + ")";
            }

            string d = string.Empty;
            if (z.IsDiplomacyTgt)
                d += "Is Target ";
            if (z.DiplomacyLinkId != 0)
            {
                d += AaDb.GetFactionName(z.DiplomacyLinkId, true);
            }

            row.Cells[4].Value = d;

            if (first)
            {
                first = false;
                ShowDbFaction(z.Id);
            }
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void ShowDbFaction(long id)
    {
        if (AaDb.DbGameSystemFactions.TryGetValue(id, out var f))
        {
            lFactionID.Text = f.Id.ToString();
            lFactionName.Text = f.NameLocalized;
            lFactionOwnerName.Text = f.OwnerName;
            lFactionOwnerTypeID.Text = f.OwnerTypeId.ToString();
            lFactionOwnerID.Text = f.OwnerId.ToString();
            LFactionPoliticalSystemID.Text = f.PoliticalSystemId.ToString();
            lFactionMotherID.Text = f.MotherId.ToString();
            lFactionAggroLink.Text = f.AggroLink.ToString();
            lFactionGuardLink.Text = f.GuardHelp.ToString();
            lFactionIsDiplomacyTarget.Text = f.IsDiplomacyTgt.ToString();
            lFactionDiplomacyLinkID.Text = f.DiplomacyLinkId.ToString();

            AaDb.SetFactionRelationLabel(f, 148, ref lFactionHostileNuia);
            AaDb.SetFactionRelationLabel(f, 149, ref lFactionHostileHaranya);
            AaDb.SetFactionRelationLabel(f, 114, ref lFactionHostilePirate);
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

    private void DoShowNPCsOnMap(long searchId)
    {
        PrepareWorldXml(false);
        var map = MapViewForm.GetMap();
        map.Show();

        List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

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
                    npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.Id, false));
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
                    if (AaDb.DbNpCs.TryGetValue(npc.id, out var z))
                    {
                        map.AddPoI(npc.x, npc.y, npc.z, z.NameLocalized + " (" + npc.id.ToString() + ")",
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

    private static void AddCustomPath(ref List<MapViewPath> allPaths, Stream fileStream, string rootNodeName = "/Objects/Entity", string PointsNodeName = "Points/Point")
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
                var attribs = XmlHelper.ReadNodeAttributes(block);

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
                        var pointattribs = XmlHelper.ReadNodeAttributes(pointxml);
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

                    allPaths.Add(newPath);

                }
            }
        }
        catch (Exception x)
        {
            MessageBox.Show(x.Message);
        }
    }

    private void AddTransferPath(ref List<MapViewPath> allPaths, GameZone zone)
    {
        if (zone != null)
        {
            var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.ZoneKey);

            // If it's not in the world.xml, it's probably not a real zone anyway
            if (worldOff == null)
                return;

            if (!Pak.IsOpen || !Pak.FileExists(zone.GamePakZoneTransferPathXml))
            {
                // MessageBox.Show("No path file found for this zone");
                return;
            }
            // MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

            int pathsFound = 0;
            try
            {
                var fs = Pak.ExportFileAsStream(zone.GamePakZoneTransferPathXml);

                var _doc = new XmlDocument();
                _doc.Load(fs);
                var _allTransferBlocks = _doc.SelectNodes("/Objects/Transfer");
                for (var i = 0; i < _allTransferBlocks.Count; i++)
                {
                    var block = _allTransferBlocks[i];
                    var attribs = XmlHelper.ReadNodeAttributes(block);

                    if (attribs.TryGetValue("name", out var blockName))
                    {

                        var newPath = new MapViewPath();
                        newPath.PathName = blockName;

                        foreach (var tp in AaDb.DbTransferPaths)
                            if (tp.PathName == blockName)
                                newPath.TypeId = tp.OwnerId;

                        long model = 0;
                        if (AaDb.DbTransfers.TryGetValue(newPath.TypeId, out var transfer))
                        {
                            model = transfer.ModelId;
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
                                               model.ToString() + " z:" + zone.ZoneKey.ToString() + ")";
                        else
                            newPath.PathName = blockName + "(t:" + newPath.TypeId.ToString() + " z:" +
                                               zone.ZoneKey.ToString() + ")";

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
                            var pointattribs = XmlHelper.ReadNodeAttributes(pointxml);
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

                        allPaths.Add(newPath);

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

    private void AddSubZones(ref List<MapViewPath> allAreas, GameZone zone)
    {
        if (zone != null)
        {
            var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.ZoneKey);

            // If it's not in the world.xml, it's probably not a real zone anyway
            if (worldOff == null)
                return;

            if (!Pak.IsOpen || !Pak.FileExists(zone.GamePakSubZoneXml))
            {
                // MessageBox.Show("No path file found for this zone");
                return;
            }
            //MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

            int areasFound = 0;
            try
            {
                var fs = Pak.ExportFileAsStream(zone.GamePakSubZoneXml);

                var _doc = new XmlDocument();
                _doc.Load(fs);
                var _allHousingBlocks = _doc.SelectNodes("/Objects/Entity");
                for (var i = 0; i < _allHousingBlocks.Count; i++)
                {
                    var block = _allHousingBlocks[i];
                    var entityAttribs = XmlHelper.ReadNodeAttributes(block);

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
                            var areaAttribs = XmlHelper.ReadNodeAttributes(areaNode);
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
                                               zone.ZoneKey.ToString() + ")";

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
                                var pointattribs = XmlHelper.ReadNodeAttributes(pointxml);
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

                            allAreas.Add(newArea);

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

    private void AddHousingZones(ref List<MapViewPath> allAreas, GameZone zone)
    {
        if (zone != null)
        {
            var worldOff = MapViewWorldXML.main_world.GetZoneByKey(zone.ZoneKey);

            // If it's not in the world.xml, it's probably not a real zone anyway
            if (worldOff == null)
                return;

            if (!Pak.IsOpen || !Pak.FileExists(zone.GamePakZoneHousingXml))
            {
                // MessageBox.Show("No path file found for this zone");
                return;
            }
            // MessageBox.Show("Loading: " + zone.GamePakZoneTransferPathXML);

            int areasFound = 0;
            try
            {
                var fs = Pak.ExportFileAsStream(zone.GamePakZoneHousingXml);

                var _doc = new XmlDocument();
                _doc.Load(fs);
                var _allHousingBlocks = _doc.SelectNodes("/Objects/Entity");
                for (var i = 0; i < _allHousingBlocks.Count; i++)
                {
                    var block = _allHousingBlocks[i];
                    var entityAttribs = XmlHelper.ReadNodeAttributes(block);

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
                            var areaAttribs = XmlHelper.ReadNodeAttributes(areaNode);
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
                                               zone.ZoneKey.ToString() + ")";

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
                                var pointattribs = XmlHelper.ReadNodeAttributes(pointxml);
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

                            allAreas.Add(newArea);

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

    private void DoFindTransferPathsInZone(long searchId)
    {
        PrepareWorldXml(false);

        List<MapViewPath> allPaths = new List<MapViewPath>();

        foreach (var zv in AaDb.DbZones)
            if (zv.Value.ZoneKey == searchId)
                AddTransferPath(ref allPaths, zv.Value);

        if (allPaths.Count <= 0)
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
            foreach (var p in allPaths)
                map.AddPath(p);
            map.tsbNamesPoI.Checked = true;
            map.FocusAll(false, true, false);
        }
    }

    private void DoExportDataForVieweD()
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
            foreach (var npc in AaDb.DbNpCs)
                NPCList.Add(npc.Key.ToString() + ";" + npc.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "npcs.txt"), NPCList);

            // Doodad
            var DoodadList = new List<string>();
            foreach (var doodad in AaDb.DbDoodadAlmighties)
                DoodadList.Add(doodad.Key.ToString() + ";" + doodad.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "doodads.txt"), DoodadList);

            // Skill
            var SkillList = new List<string>();
            foreach (var skill in AaDb.DbSkills)
                SkillList.Add(skill.Key.ToString() + ";" + skill.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "skills.txt"), SkillList);

            // Items
            var ItemList = new List<string>();
            foreach (var item in AaDb.DbItems)
                ItemList.Add(item.Key.ToString() + ";" + item.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "items.txt"), ItemList);

            // Zone Groups
            var ZoneGroupList = new List<string>();
            foreach (var zonegroup in AaDb.DbZoneGroups)
                ZoneGroupList.Add(zonegroup.Key.ToString() + ";" + zonegroup.Value.DisplayTextLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "zonegroups.txt"), ZoneGroupList);

            // Zone Keys
            var ZoneKeysList = new List<string>();
            foreach (var zonekey in AaDb.DbZones)
                ZoneKeysList.Add(zonekey.Value.ZoneKey.ToString() + ";" + zonekey.Value.DisplayTextLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "zonekeys.txt"), ZoneKeysList);

            // Factions
            var FactionList = new List<string>();
            foreach (var faction in AaDb.DbGameSystemFactions)
                FactionList.Add(faction.Key.ToString() + ";" + faction.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "factions.txt"), FactionList);

            // Quest Names
            var QuestList = new List<string>();
            foreach (var quest in AaDb.DbQuestContexts)
                QuestList.Add(quest.Key.ToString() + ";" + quest.Value.NameLocalized);
            File.WriteAllLines(Path.Combine(LookupExportPath, "quests.txt"), QuestList);


            MessageBox.Show("Done exporting", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception x)
        {
            MessageBox.Show("Export Failed !\r\n" + x.Message);
            return;
        }
    }

    private void DoExportNpcSpawnData()
    {
        PrepareWorldXml(false);

        foreach (var inst in MapViewWorldXML.instances)
        {
            var NPCList = new List<NpcExportData>();
            var i = 0;

            var zoneGroups = new List<long>();
            foreach (var z in inst.zones)
            {
                var zone = AaDb.GetZoneByKey(z.Value.zone_key);
                if (zone != null)
                    if (!zoneGroups.Contains(zone.GroupId))
                        zoneGroups.Add(zone.GroupId);
            }

            foreach (var zoneGroupId in zoneGroups)
            {
                if (!AaDb.DbZoneGroups.TryGetValue(zoneGroupId, out var zoneGroup))
                    continue;

                var npcs = GetNpcSpawnsInZoneGroup(zoneGroup.Id, false, null, inst.WorldName);
                foreach (var npc in npcs)
                {
                    i++;
                    var newNPC = new NpcExportData();
                    newNPC.Id = i;
                    newNPC.Count = 1;
                    newNPC.UnitId = npc.id;
                    if (AaDb.DbNpCs.TryGetValue(npc.id, out var vNPC))
                        newNPC.Title = vNPC.NameLocalized;
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

    private void DoFindAllTransferPaths()
    {
        PrepareWorldXml(false);
        var allPaths = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        foreach (var zv in AaDb.DbZones)
            AddTransferPath(ref allPaths, zv.Value);

        if (allPaths.Count <= 0)
            MessageBox.Show("No paths found ?");
        else
        {
            var map = MapViewForm.GetMap();
            map.Show();

            if (map.GetPathCount() > 0)
                if (MessageBox.Show("Keep current paths ?", "Add Transfers", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    map.ClearPaths();

            foreach (var p in allPaths)
                map.AddPath(p);

            map.tsbShowPath.Checked = true;
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    public void PrepareWorldXml(bool overwrite)
    {
        if (overwrite || (MapViewWorldXML.instances == null) || (MapViewWorldXML.instances.Count < 0))
        {
            if (!Pak.IsOpen)
            {
                MessageBox.Show(@"Game Pak file was not loaded !");
                return;
            }

            MapViewWorldXML.instances = new List<MapViewWorldXML>();

            foreach (var pfi in Pak.Files)
            {
                if (pfi.Name.EndsWith("/world.xml") && pfi.Name.StartsWith("game/worlds/"))
                {
                    var splitName = pfi.Name.ToLower().Split('/');
                    if (splitName.Count() != 4)
                        continue;
                    var thisInstanceName = splitName[2];

                    var inst = new MapViewWorldXML();
                    if (inst.LoadFromStream(
                            Pak.ExportFileAsStream("game/worlds/" + thisInstanceName + "/world.xml")))
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

    private void DoFindAllHousing()
    {
        PrepareWorldXml(false);
        List<MapViewPath> allAreas = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        foreach (var zv in AaDb.DbZones)
            AddHousingZones(ref allAreas, zv.Value);

        if (allAreas.Count <= 0)
            MessageBox.Show("No housing found ?");
        else
        {
            var map = MapViewForm.GetMap();
            map.Show();

            // We no longer have the housing by zone button, so we no longer need to ask, just clear it
            //if (map.GetHousingCount() > 0)
            //    if (MessageBox.Show("Keep current housing areas ?", "Add Housing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            map.ClearHousing();

            foreach (var p in allAreas)
                map.AddHousing(p);

            map.tsbShowHousing.Checked = true;
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;

    }

    private void DoLoadCustomPaths()
    {
        List<MapViewPath> allAreas = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        if (ofdCustomPaths.ShowDialog() == DialogResult.OK)
        {
            using (var fs = new FileStream(ofdCustomPaths.FileName, FileMode.Open, FileAccess.Read))
            {
                AddCustomPath(ref allAreas, fs, "/Objects/Entity", "Area/Points/Point");
            }
        }
        else
        {
            return;
        }


        if (allAreas.Count <= 0)
            MessageBox.Show("Nothing to show ?");
        else
        {
            var map = MapViewForm.GetMap();
            map.Show();
            if (map.GetPathCount() > 0)
                if (MessageBox.Show("Keep current paths ?", "Add Custom Path", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    map.ClearPaths();

            foreach (var p in allAreas)
                map.AddPath(p);

            map.FocusAll(false, true, false);
            map.tsbShowPath.Checked = true;
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;

    }

    private void DoLoadCustomAAEmuJson()
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
                            if (isNPCs && AaDb.DbNpCs.TryGetValue(spawner.UnitId, out var npc))
                            {
                                ni.Name = npc.NameLocalized;
                                ni.PoIColor = Color.Yellow;
                                ni.TypeId = npc.Id;
                                ni.SourceObject = npc;
                            }
                            else if (isDoodads &&
                                     AaDb.DbDoodadAlmighties.TryGetValue(spawner.UnitId, out var doodad))
                            {
                                ni.Name = doodad.NameLocalized;
                                ni.PoIColor = Color.DarkGreen;
                                ni.TypeId = doodad.Id;
                                ni.SourceObject = doodad;
                            }
                            else if (isTransfers && AaDb.DbTransfers.TryGetValue(spawner.UnitId, out var transfer))
                            {
                                ni.Name = "Model: " + transfer.ModelId.ToString();
                                ni.PoIColor = Color.Navy;
                                ni.TypeId = transfer.Id;
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
        var fs = Pak.ExportFileAsStream(fileName);

        var _doc = new XmlDocument();
        _doc.Load(fs);
        var _allEntityBlocks = _doc.SelectNodes("/Mission/Objects/Entity");
        var cellPos = new Vector3(cellX * 1024, cellY * 1024, 0);

        for (var i = 0; i < _allEntityBlocks.Count; i++)
        {
            var block = _allEntityBlocks[i];
            var attribs = XmlHelper.ReadNodeAttributes(block);
            if (attribs.TryGetValue("entityclass", out var entityClass))
            {
                if (entityClass == "AreaShape")
                {

                    var areaName = attribs["name"];
                    var entityId = attribs["entityid"];
                    var posString = attribs["pos"];
                    var areaPos = XmlHelper.ReadXmlPos(posString);

                    var mapPath = new MapViewPath();
                    mapPath.PathName = areaName + " (" + entityId + ")";

                    var areaBlock = block.SelectSingleNode("Area");
                    if (areaBlock == null)
                        continue; // this shape has no area defined

                    var areaAttribs = XmlHelper.ReadNodeAttributes(areaBlock);
                    var areaHeight = areaAttribs["height"]; // not used in the this viewer

                    var pointBlocks = areaBlock.SelectNodes("Points/Point");

                    var firstPos = Vector3.Zero;
                    for (var p = 0; p < pointBlocks.Count; p++)
                    {
                        var pointAttribs = XmlHelper.ReadNodeAttributes(pointBlocks[p]);
                        var pointPos = XmlHelper.ReadXmlPos(pointAttribs["pos"]);
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

    private void DoShowEntityAreaShape()
    {
        if (!Pak.IsOpen)
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
                if (Pak.FileExists(fn))
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

    private void DoTradeSourceSelectedIndexChanged()
    {
        lbTradeDestination.Items.Clear();
        if (!(lbTradeSource.SelectedItem is GameZoneGroups sourceZone))
            return;

        foreach (var z in AaDb.DbSpecialities)
        {
            if ((z.Value.VendorExist) && (z.Value.RowZoneGroupId == sourceZone.Id))
            {
                if (AaDb.DbZoneGroups.TryGetValue(z.Value.ColZoneGroupId, out var destZone))
                    lbTradeDestination.Items.Add(destZone);
            }
        }

    }

    private void DoTradeDestinationSelectedIndexChanged()
    {
        if (!(lbTradeSource.SelectedItem is GameZoneGroups sourceZone))
            return;
        if (!(lbTradeDestination.SelectedItem is GameZoneGroups destZone))
            return;

        lTradeRoute.Text = "-";
        foreach (var z in AaDb.DbSpecialities)
        {
            if ((z.Value.VendorExist) && (z.Value.RowZoneGroupId == sourceZone.Id) &&
                (z.Value.ColZoneGroupId == destZone.Id))
            {
                lTradeRoute.Text = sourceZone.ToString() + " => " + destZone.ToString();
                lTradeProfit.Text = z.Value.Profit.ToString();
                lTradeRatio.Text = z.Value.Ratio.ToString();
            }
        }

    }

    private void DoExportDoodadSpawnData()
    {
        PrepareWorldXml(false);

        foreach (var inst in MapViewWorldXML.instances)
        {
            var DoodadList = new List<DoodadExportData>();
            var i = 0;

            var zoneGroups = new List<long>();
            foreach (var z in inst.zones)
            {
                var zone = AaDb.GetZoneByKey(z.Value.zone_key);
                if (zone != null)
                    if (!zoneGroups.Contains(zone.GroupId))
                        zoneGroups.Add(zone.GroupId);
            }

            foreach (var zoneGroupId in zoneGroups)
            {
                if (!AaDb.DbZoneGroups.TryGetValue(zoneGroupId, out var zoneGroup))
                    continue;

                var npcs = GetDoodadSpawnsInZoneGroup(zoneGroup.Id, false, -1, inst.WorldName);
                foreach (var npc in npcs)
                {
                    i++;
                    var newDoodad = new DoodadExportData();
                    newDoodad.Id = i;
                    newDoodad.UnitId = npc.id;
                    newDoodad.Position.X = npc.x;
                    newDoodad.Position.Y = npc.y;
                    newDoodad.Position.Z = npc.z;
                    if (AaDb.DbDoodadAlmighties.TryGetValue(npc.id, out var vDoodad))
                        newDoodad.Title = vDoodad.NameLocalized;
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

    private void ShowDbTag(long tag)
    {
        tvTagInfo.Nodes.Clear();
        if (tag <= 0)
            return;

        ShowSelectedData("tags", "id = " + tag.ToString(), "id");

        TreeNode groupNode = null;

        groupNode = null;
        var buffs = from i in AaDb.DbTaggedBuffs.Values
            where i.TagId == tag
            select i;
        foreach (var i in buffs)
        {
            if (groupNode == null)
                groupNode = tvTagInfo.Nodes.Add("Buffs");
            AddCustomPropertyNode("buff_id", i.TargetId.ToString(), false, groupNode);
        }

        groupNode = null;
        var items = from i in AaDb.DbTaggedItems.Values
            where i.TagId == tag
            select i;
        foreach (var i in items)
        {
            if (groupNode == null)
                groupNode = tvTagInfo.Nodes.Add("Items");
            AddCustomPropertyNode("item_id", i.TargetId.ToString(), false, groupNode);
        }

        groupNode = null;
        var npcs = from i in AaDb.DbTaggedNpCs.Values
            where i.TagId == tag
            select i;
        foreach (var i in npcs)
        {
            if (groupNode == null)
                groupNode = tvTagInfo.Nodes.Add("NPCs");
            AddCustomPropertyNode("npc_id", i.TargetId.ToString(), false, groupNode);
        }

        groupNode = null;
        var skills = from i in AaDb.DbTaggedSkills.Values
            where i.TagId == tag
            select i;
        foreach (var i in skills)
        {
            if (groupNode == null)
                groupNode = tvTagInfo.Nodes.Add("Skills");
            AddCustomPropertyNode("skill_id", i.TargetId.ToString(), false, groupNode);
        }

        groupNode = null;
        var zones = from i in AaDb.DbZoneGroupBannedTags.Values
            where i.TagId == tag
            select i;
        foreach (var i in zones)
        {
            if (groupNode == null)
                groupNode = tvTagInfo.Nodes.Add("Zone Groups (banned tags)");
            AddCustomPropertyNode("zone_group_id", i.ZoneGroupId.ToString(), false, groupNode);
        }

        // expand if only one group is showing
        if (tvTagInfo.Nodes.Count == 1)
            tvTagInfo.ExpandAll();
    }

    private void DoSearchTags()
    {
        var searchText = tSearchTags.Text.ToLower();
        if (searchText == string.Empty)
            return;
        if (!long.TryParse(searchText, out var searchId))
            searchId = -1;

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvTags.Rows.Clear();
        int c = 0;
        foreach (var t in AaDb.DbTags)
        {
            var z = t.Value;
            if ((z.Id == searchId) || (z.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvTags.Rows.Add();
                var row = dgvTags.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.NameLocalized;

                if (c == 0)
                {
                    ShowDbTag(z.Id);
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

    private void DoTagsSelectionChanged()
    {
        if (dgvTags.SelectedRows.Count <= 0)
            return;
        var row = dgvTags.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var val = row.Cells[0].Value;
        if (val == null)
            return;
        ShowDbTag(long.Parse(val.ToString() ?? "0"));
    }

    private void DoLoadAAEmuWater()
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
    private void DoLoadExportedUnitMovement()
    {
        var filter = tExportedObjFilter.Text.Trim();
        List<MapViewPath> allPaths = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        if (ofdLoadUnitMovementDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var jsonString = File.ReadAllText(ofdLoadUnitMovementDialog.FileName);
                var jsonObjects = JsonConvert.DeserializeObject<List<UnitMovementData>>(jsonString);
                var unitMovements = new Dictionary<long, List<UnitMovementData>>();
                // Group by ObjId into a dictionary
                foreach (var movement in jsonObjects)
                {
                    if (!string.IsNullOrWhiteSpace(filter) && !movement.Id.ToString().Contains(filter))
                        continue;

                    // Ignore if any of the coords are near zero (moving on a parent object)
                    if ((movement.X <= 128f) || (movement.Y <= 128f) || (movement.Z >= 4192f))
                        continue;


                    if (!unitMovements.TryGetValue(movement.Id, out var unitMoveList))
                    {
                        unitMoveList = new List<UnitMovementData>();
                        unitMovements.Add(movement.Id, unitMoveList);
                    }
                    unitMoveList.Add(movement);
                }

                foreach (var (objId, moveList)in unitMovements)
                {
                    var newPath = new MapViewPath();
                    newPath.PathName = objId.ToString();
                    newPath.TypeId = objId;
                    foreach (var unitMovementData in moveList)
                    {
                        newPath.allpoints.Add(new Vector3(unitMovementData.X, unitMovementData.Y, unitMovementData.Z));
                    }
                    allPaths.Add(newPath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                if (MessageBox.Show("Keep current paths ?", "Add Unit Movement", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    map.ClearPaths();

            foreach (var p in allPaths)
                map.AddPath(p);

            map.FocusAll(false, true, false);
            map.tsbShowPath.Checked = true;
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void LoadAchievements()
    {
        if (AllTableNames.GetValueOrDefault("achievements") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                // Achievements
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM achievements ORDER BY priority ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        var hasCategory = (reader.GetColumnNames()?.IndexOf("category_id") >= 0);
                        var hasIsActive = (reader.GetColumnNames()?.IndexOf("is_active") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameAchievements();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.Summary = GetString(reader, "summary");
                            t.Description = GetString(reader, "description");
                            t.CategoryId = hasCategory ? GetInt64(reader, "category_id") : 0;
                            t.SubCategoryId = hasCategory ? GetInt64(reader, "sub_category_id") : 0;
                            t.ParentAchievementId = GetInt64(reader, "parent_achievement_id");
                            t.IsActive = hasIsActive && GetBool(reader, "is_active");
                            t.IsHidden = GetBool(reader, "is_hidden");
                            t.Priority = GetInt64(reader, "priority");
                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");
                            t.CompleteOr = GetBool(reader, "complete_or");
                            t.CompleteNum = GetInt64(reader, "complete_num");
                            t.ItemId = GetInt64(reader, "item_id");
                            t.IconId = GetInt64(reader, "icon_id");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "achievements", "name", t.Name);
                            t.DescriptionLocalized = AaDb.GetTranslationById(t.Id, "achievements", "description", t.Description);
                            t.SummaryLocalized = AaDb.GetTranslationById(t.Id, "achievements", "summary", t.Summary);

                            t.SearchString = t.Id.ToString() + " " + t.Name + " " + t.Description + " " + t.Summary + " " + 
                                             t.NameLocalized + " " + t.DescriptionLocalized + " " + t.SummaryLocalized;

                            AaDb.DbAchievements.Add(t.Id, t);

                            if (!AaDb.CompiledGroupedAchievements.TryGetValue(t.CategoryId, out var categoryGroups))
                            {
                                categoryGroups = new Dictionary<long, Dictionary<long, GameAchievements>>();
                                AaDb.CompiledGroupedAchievements.Add(t.CategoryId,categoryGroups);
                            }

                            if (!categoryGroups.TryGetValue(t.SubCategoryId, out var subCategoryGroups))
                            {
                                subCategoryGroups = new Dictionary<long, GameAchievements>();
                                categoryGroups.Add(t.SubCategoryId, subCategoryGroups);
                            }

                            subCategoryGroups.TryAdd(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }
        }

        if (AllTableNames.GetValueOrDefault("achievement_objectives") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                // Achievements
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM achievement_objectives ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameAchievementObjectives();
                            t.Id = GetInt64(reader, "id");
                            t.AchievementId = GetInt64(reader, "achievement_id");
                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");
                            t.RecordId = GetInt64(reader, "record_id");
                            AaDb.DbAchievementObjectives.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }
        }
    }

    private void UpdateAchievementTree()
    {
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        // ui_texts: achievement_category

        TvAchievements.Nodes.Clear();
        TvAchievements.ImageList = ilMiniIcons;
        foreach (var (categoryId, categoryGroup) in AaDb.CompiledGroupedAchievements.OrderBy(x => x.Key))
        {
            var catName = AaDb.GetTranslatedUiText($"achievement_category_{categoryId}", 45, $"Category {categoryId}");

            var groupNode = TvAchievements.Nodes.Add(catName);
            groupNode.ImageIndex = IconIdToLabel(11158, null);
            groupNode.SelectedImageIndex = groupNode.ImageIndex;
            foreach (var (subGroupId, subCategoryGroup) in categoryGroup.OrderBy(x => x.Key))
            {
                var subCatName = AaDb.GetTranslatedUiText($"achievement_category_{categoryId}_{subGroupId}", 45, $"Sub-Category {subGroupId}");

                var subGroupNode = groupNode.Nodes.Add(subCatName);
                subGroupNode.ImageIndex = IconIdToLabel(11158, null);
                subGroupNode.SelectedImageIndex = subGroupNode.ImageIndex;
                foreach (var (key, achievement) in subCategoryGroup.OrderBy(x => x.Key))
                {
                    if (!string.IsNullOrWhiteSpace(TSearchAchievements.Text) && !achievement.SearchString.Contains(TSearchAchievements.Text, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    var entryName = $"{key}: {achievement.NameLocalized}";

                    TreeNode parentNode = subGroupNode;
                    foreach (TreeNode sgn in subGroupNode.Nodes)
                    {
                        if (sgn?.Tag is not GameAchievements a)
                            continue;
                        if (a.Id == achievement.ParentAchievementId)
                        {
                            parentNode = sgn;
                            break;
                        }
                    }

                    var achievementNode = parentNode.Nodes.Add(entryName);
                    achievementNode.Tag = achievement;
                    achievementNode.ImageIndex = IconIdToLabel(achievement.IconId, null);
                    achievementNode.SelectedImageIndex = achievementNode.ImageIndex;

                    //achievementNode.Nodes.Add($"Description: {achievement.DescriptionLocalized}");
                    //achievementNode.Nodes.Add($"Summary: {achievement.SummaryLocalized}");

                    if (achievement.IsHidden)
                        achievementNode.ForeColor = Color.LightGray;
                }

                if (subGroupNode.Nodes.Count <= 0)
                {
                    groupNode.Nodes.Remove(subGroupNode);
                }
            }

            if (groupNode.Nodes.Count <= 0)
            {
                TvAchievements.Nodes.Remove(groupNode);
            }
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void ShowDbAchievement(GameAchievements a)
    {
        RtAchievementInfo.Clear();
        var t = string.Empty;
        if (a != null)
        {
            IconIdToLabel(a.IconId, LAchievementIcon);

            if (AaDb.DbItems.TryGetValue(a.ItemId, out var item))
            {
                IconIdToLabel(item.IconId, LAchievementReward);
            }
            else
            {
                item = null;
                LAchievementReward.Image = null;
                LAchievementReward.Text = "No Reward";
            }

            t = $"|nd;{a.NameLocalized}|r ( {a.Id} )\r" +
                $"Category: {a.CategoryId} - {a.SubCategoryId}\r" +
                $"\rSummary:\r" +
                $"{a.SummaryLocalized}\r" +
                $"\rDescription:\r" +
                $"{a.DescriptionLocalized}\r" +
                $"\r";

            if (item != null)
            {
                t += $"Reward:\r|ni;{item.NameLocalized}|r ( {item.Id} )\r" +
                     $"\r";
            }

            var objectives = AaDb.DbAchievementObjectives.Values.Where(x => x.AchievementId == a.Id).ToList();
            if (objectives.Any())
            {
                // No idea why CompleteOr == true => ALL of the objectives
                t += $"{(a.CompleteOr ? "|nd;ALL|r" : "|ni;Any|r")} of objectives{(a.CompleteNum > 1 ? $" x |ni;{a.CompleteNum}|r" : "")}:\r";

                foreach (var objective in objectives)
                {
                    t +=
                        $"Id:{objective.Id}, RecordId: {objective.RecordId} (unit_reqs: {(objective.OrUnitReqs ? "Any" : "ALL")})\r";
                }

                t += "\r";
            }

            var requiresAchievements = AaDb.DbAchievements.Values.Where(x => x.ParentAchievementId == a.Id).ToList();
            if (requiresAchievements.Any())
            {
                t += $"|nd;Required|r Achievements:\r";

                foreach (var childAchievement in requiresAchievements)
                {
                    t += $"{childAchievement.NameLocalized} ( {childAchievement.Id} ) - {childAchievement.SummaryLocalized}\r";
                }

                t += "\r";
            }

        }
        else
        {
            LAchievementIcon.Image = null;
            LAchievementIcon.Text = "???";
            LAchievementReward.Image = null;
            LAchievementReward.Text = "No Reward";
        }

        FormattedTextToRichtEdit(t, RtAchievementInfo);
    }
}

internal class UnitMovementData
{
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.Always)]
    public long Id { get; set; }
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public long TemplateId { get; set; }
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public string Name { get; set; } = string.Empty;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float X { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.Always)]
    public float Y { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.Always)]
    public float Z { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.Always)]
    public float RotX { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float RotY { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float RotZ { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float VelX { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float VelY { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float VelZ { get; set; } = 0f;
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, Required = Required.DisallowNull)]
    public float Scale {get; set; } = 1f;
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Xml;
using AAEmu.DBDefs;
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
                        t.SearchString = t.name + " " + t.nameLocalized + " " + t.owner_name;
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

    private int IconIdToLabel(long iconId, Label iconImgLabel)
    {
        var cachedImageIndex = -1;
        if (pak.IsOpen)
        {
            if (AADB.DB_Icons.TryGetValue(iconId, out var iconname))
            {
                var fn = "game/ui/icon/" + iconname;

                if (pak.FileExists(fn))
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
                            // Load from pak if not cached
                            var fStream = pak.ExportFileAsStream(fn);
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
            where t.Value.target_id == targetId
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

                if ((z.owner_type_id == 1) && AADB.DB_NPCs.TryGetValue(z.owner_id, out var ownerNpc))
                {
                    row.Cells[3].Value = ownerNpc.nameLocalized;
                }
                else
                {
                    row.Cells[3].Value = z.owner_name + " (" + z.owner_id.ToString() + ")";
                }
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
                    ShowDbFaction(z.id);
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

            if ((z.owner_type_id == 1) && AADB.DB_NPCs.TryGetValue(z.owner_id, out var ownerNpc))
            {
                row.Cells[3].Value = ownerNpc.nameLocalized;
            }
            else
            {
                row.Cells[3].Value = z.owner_name + " (" + z.owner_id.ToString() + ")";
            }

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
                ShowDbFaction(z.id);
            }
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void ShowDbFaction(long id)
    {
        if (AADB.DB_GameSystem_Factions.TryGetValue(id, out var f))
        {
            lFactionID.Text = f.id.ToString();
            lFactionName.Text = f.nameLocalized;
            lFactionOwnerName.Text = f.owner_name;
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
                    npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.id, false));
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
                    var attribs = XmlHelper.ReadNodeAttributes(block);

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

        foreach (var zv in AADB.DB_Zones)
            if (zv.Value.zone_key == searchId)
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
                var zone = AADB.GetZoneByKey(z.Value.zone_key);
                if (zone != null)
                    if (!zoneGroups.Contains(zone.group_id))
                        zoneGroups.Add(zone.group_id);
            }

            foreach (var zoneGroupId in zoneGroups)
            {
                if (!AADB.DB_Zone_Groups.TryGetValue(zoneGroupId, out var zoneGroup))
                    continue;

                var npcs = GetNpcSpawnsInZoneGroup(zoneGroup.id, false, null, inst.WorldName);
                foreach (var npc in npcs)
                {
                    i++;
                    var newNPC = new NpcExportData();
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

    private void DoFindAllTransferPaths()
    {
        PrepareWorldXml(false);
        var allPaths = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        foreach (var zv in AADB.DB_Zones)
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
            if (!pak.IsOpen)
            {
                MessageBox.Show(@"Game pak file was not loaded !");
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

    private void DoFindAllHousing()
    {
        PrepareWorldXml(false);
        List<MapViewPath> allAreas = new List<MapViewPath>();

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        foreach (var zv in AADB.DB_Zones)
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

    private void DoTradeSourceSelectedIndexChanged()
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

    private void DoTradeDestinationSelectedIndexChanged()
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

    private void ShowDbTag(long tag)
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
        foreach (var t in AADB.DB_Tags)
        {
            var z = t.Value;
            if ((z.id == searchId) || (z.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvTags.Rows.Add();
                var row = dgvTags.Rows[line];

                row.Cells[0].Value = z.id.ToString();
                row.Cells[1].Value = z.nameLocalized;

                if (c == 0)
                {
                    ShowDbTag(z.id);
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
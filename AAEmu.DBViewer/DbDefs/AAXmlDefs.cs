using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace AAEmu.DBViewer.DbDefs;

public static class AAXmlDefs
{
    /// <summary>List of NPCSpawner data loaded from .g file</summary>
    public static Dictionary<uint, List<NpcSpawnerGFileData>> NpcSpawnersByZoneKey { get; } = [];

    public static bool AddNpcSpawner(uint zoneKey, int id, int type, string areaType, List<Vector3> points)
    {
        var spawner = new NpcSpawnerGFileData(zoneKey)
        {
            Id = id,
            Type = type,
            AreaType = areaType,
            Points = points.ToList() // make sure this is a copy
        };
        if (!NpcSpawnersByZoneKey.TryGetValue(zoneKey, out var spawnerList))
        {
            spawnerList = [];
            NpcSpawnersByZoneKey.Add(zoneKey, spawnerList);
        }
        spawnerList.Add(spawner);
        return true;
    }

    public static List<NpcSpawnerGFileData> LoadNpcSpawnerData(uint zoneKey, string[] lines)
    {
        NpcSpawnersByZoneKey.Remove(zoneKey); // Always remote old zone data on reading new one
        var currentSpawnerId = 0;
        var currentSpawnerType = 0;
        var currentSpawnerAreaType = "";
        var currentPoints = new List<Vector3>();

        var xmlZone = MapViewWorldXML.main_world.zones.GetValueOrDefault(zoneKey);
        var zoneOffset = new Vector3(xmlZone.originCellX * 1024f, xmlZone.originCellY * 1024f, 0f);

        var areaPointsMode = false;
        foreach (var line in lines)
        {
            var trimmedLine = line.ToLower().Replace(",", "").TrimStart(' ').TrimStart('\t');
            var l = trimmedLine.Split(' ');
            if (l.Length == 1 && l[0] == "spawner")
            {
                // Add previous path if still open
                if (currentSpawnerId != 0 && currentPoints.Count > 0)
                {
                    AddNpcSpawner(zoneKey, currentSpawnerId, currentSpawnerType, currentSpawnerAreaType, currentPoints);
                }

                currentSpawnerId = 0;
                currentSpawnerType = 0;
                currentSpawnerAreaType = "";
                currentPoints.Clear();
                continue;
            }

            if (currentSpawnerId == 0 && l.Length == 2 && l[0] == "spawnerid")
            {
                if (!int.TryParse(l[1], out currentSpawnerId))
                    currentSpawnerId = 0;
                continue;
            }

            if (currentSpawnerType == 0 && l.Length == 2 && l[0] == "spawnertype")
            {
                if (!int.TryParse(l[1], out currentSpawnerType))
                    currentSpawnerType = 0;
                continue;
            }

            if (currentSpawnerType == 0 && l.Length == 2 && l[0] == "spawnareatype")
            {
                currentSpawnerAreaType = l[1];
                continue;
            }

            if (currentSpawnerType != 0 && l.Length == 1 && l[0] == "points")
            {
                currentPoints.Clear();
                areaPointsMode = true;
                continue;
            }

            if (l.Length == 1 && l[0] == "point")
            {
                // don't need to do anything here
                areaPointsMode = true;
                continue;
            }

            if (l.Length == 2 && l[0] == "zrot")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 1 && l[0] == "paths")
            {
                areaPointsMode = false;
                // don't need to do anything here
                continue;
            }

            if (l.Length == 7 && l[0] == "path")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 1 && l[0] == "triinfos")
            {
                areaPointsMode = false;
                // don't need to do anything here
                continue;
            }

            if (l.Length == 1 && l[0] == "triinfo")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 2 && l[0] == "roamingarea")
            {
                areaPointsMode = false;
                // don't need to do anything here
                continue;
            }

            if (l.Length == 9 && l[0] == "v1")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 9 && l[0] == "v2")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 9 && l[0] == "v3")
            {
                // don't need to do anything here
                continue;
            }

            if (l.Length == 2 && l[0] == "arearate")
            {
                // don't need to do anything here
                continue;
            }

            if (currentSpawnerId != 0 && l.Length == 9 && l[0] == "pos" && l[1] == "(" && l[2] == "x" &&
                l[4] == "y" && l[6] == "z" && l[8] == ")")
            {
                if (areaPointsMode)
                {
                    if (
                        !float.TryParse(l[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var posX) ||
                        !float.TryParse(l[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var posY) ||
                        !float.TryParse(l[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var posZ)
                    )
                        continue; // invalid

                    currentPoints.Add(new Vector3(posX + zoneOffset.X, posY + zoneOffset.Y, posZ + zoneOffset.Z));
                }
                continue;
            }

            // If we reach here, we have a complete spawner definition
            if (currentSpawnerId != 0 && currentPoints.Count > 0)
            {
                AddNpcSpawner(zoneKey, currentSpawnerId, currentSpawnerType, currentSpawnerAreaType, currentPoints);
                currentSpawnerId = 0;
                currentSpawnerType = 0;
                currentSpawnerAreaType = "";
                currentPoints.Clear();
            }
        }

        // Add last path if still open
        if (currentSpawnerId != 0 && currentPoints.Count > 0)
        {
            AddNpcSpawner(zoneKey, currentSpawnerId, currentSpawnerType, currentSpawnerAreaType, currentPoints);
            currentSpawnerId = 0;
            currentSpawnerType = 0;
            currentSpawnerAreaType = "";
            currentPoints.Clear();
        }

        return NpcSpawnersByZoneKey.GetValueOrDefault(zoneKey) ?? [];
    }
}
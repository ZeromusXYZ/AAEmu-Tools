using System.Collections.Generic;
using System.Numerics;

namespace AAEmu.DBViewer.DbDefs;

public class NpcSpawnerGFileData(uint zoneKey)
{
    public uint ParentZoneKey { get; init; } = zoneKey;
    public int Id { get; set; }
    public int Type { get; set; }
    public string AreaType { get; set; }
    public List<Vector3> Points { get; set; } = [];
}
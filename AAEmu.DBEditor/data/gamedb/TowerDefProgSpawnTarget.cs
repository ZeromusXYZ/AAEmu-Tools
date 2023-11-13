using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class TowerDefProgSpawnTarget
{
    public long? Id { get; set; }

    public long? TowerDefProgId { get; set; }

    public long? SpawnTargetId { get; set; }

    public string SpawnTargetType { get; set; }

    public byte[] DespawnOnNextStep { get; set; }
}

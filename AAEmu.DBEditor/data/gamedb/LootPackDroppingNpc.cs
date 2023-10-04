using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class LootPackDroppingNpc
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public long? LootPackId { get; set; }

    public string DefaultPack { get; set; }
}

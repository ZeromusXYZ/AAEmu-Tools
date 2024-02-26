using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class LootActabilityGroup
{
    public long? Id { get; set; }

    public long? LootPackId { get; set; }

    public long? LootGroupId { get; set; }

    public long? MaxDice { get; set; }

    public long? MinDice { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class GainLootPackItemEffect
{
    public long? Id { get; set; }

    public long? LootPackId { get; set; }

    public byte[] ConsumeSourceItem { get; set; }

    public long? ConsumeItemId { get; set; }

    public long? ConsumeCount { get; set; }

    public byte[] InheritGrade { get; set; }
}

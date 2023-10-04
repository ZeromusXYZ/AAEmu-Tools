using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BuffTickEffect
{
    public long? Id { get; set; }

    public long? BuffId { get; set; }

    public long? EffectId { get; set; }

    public long? TargetBuffTagId { get; set; }

    public long? TargetNobuffTagId { get; set; }
}

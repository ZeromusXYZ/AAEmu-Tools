using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BuffTrigger
{
    public long? Id { get; set; }

    public long? BuffId { get; set; }

    public long? EventId { get; set; }

    public byte[] EffectOnSource { get; set; }

    public long? EffectId { get; set; }

    public byte[] UseDamageAmount { get; set; }

    public byte[] UseOriginalSource { get; set; }

    public long? TargetBuffTagId { get; set; }

    public long? TargetNoBuffTagId { get; set; }

    public byte[] Synergy { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SiegePlan
{
    public long? Id { get; set; }

    public long? ZoneGroupId { get; set; }

    public byte[] WeekStart { get; set; }
}

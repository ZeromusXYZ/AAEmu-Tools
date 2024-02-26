using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ZoneGroupBannedTag
{
    public long? Id { get; set; }

    public long? ZoneGroupId { get; set; }

    public long? TagId { get; set; }

    public long? BannedPeriodsId { get; set; }

    public string Usage { get; set; }
}

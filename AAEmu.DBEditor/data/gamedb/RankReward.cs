using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class RankReward
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string Comment { get; set; }

    public long? AppellationId { get; set; }

    public long? ItemId { get; set; }

    public long? ItemGradeId { get; set; }

    public long? ItemCount { get; set; }

    public long? Weeks { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class RankRewardLink
{
    public long? Id { get; set; }

    public long? RankId { get; set; }

    public long? RankScopeId { get; set; }

    public long? RankRewardId { get; set; }
}

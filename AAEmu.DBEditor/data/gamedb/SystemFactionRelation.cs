using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SystemFactionRelation
{
    public long? Id { get; set; }

    public long? Faction1Id { get; set; }

    public long? Faction2Id { get; set; }

    public long? StateId { get; set; }
}

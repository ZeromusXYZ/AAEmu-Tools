using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpcAggroLink
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public long? AggroLinkId { get; set; }
}

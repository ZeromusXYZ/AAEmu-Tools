using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncAreaTrigger
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public byte[] IsEnter { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncSpawnMgmt
{
    public long? Id { get; set; }

    public long? GroupId { get; set; }

    public byte[] Spawn { get; set; }

    public long? ZoneId { get; set; }
}

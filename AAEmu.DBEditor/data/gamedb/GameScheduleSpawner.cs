using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class GameScheduleSpawner
{
    public long? Id { get; set; }

    public long? GameScheduleId { get; set; }

    public long? SpawnerId { get; set; }
}

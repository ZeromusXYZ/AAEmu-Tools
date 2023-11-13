using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class RepairableSlafe
{
    public long? Id { get; set; }

    public long? RepairSlaveEffectId { get; set; }

    public long? SlaveId { get; set; }
}

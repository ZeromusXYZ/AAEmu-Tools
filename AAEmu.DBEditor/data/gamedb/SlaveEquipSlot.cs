using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SlaveEquipSlot
{
    public long? Id { get; set; }

    public long? SlaveId { get; set; }

    public long? AttachPointId { get; set; }

    public long? EquipSlotId { get; set; }

    public long? RequireSlotId { get; set; }
}

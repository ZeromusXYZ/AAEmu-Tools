using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class AllowToEquipSlot
{
    public long? Id { get; set; }

    public long? SlaveEquipmentEquipSlotPackId { get; set; }

    public long? EquipSlotId { get; set; }
}

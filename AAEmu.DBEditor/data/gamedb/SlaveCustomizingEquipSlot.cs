using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SlaveCustomizingEquipSlot
{
    public long? Id { get; set; }

    public long? SlaveCustomizingId { get; set; }

    public long? EquipSlotId { get; set; }

    public string EquipSlotName { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Wearable
{
    public long? Id { get; set; }

    public long? ArmorTypeId { get; set; }

    public long? SlotTypeId { get; set; }

    public long? ArmorBp { get; set; }

    public long? MagicResistanceBp { get; set; }
}

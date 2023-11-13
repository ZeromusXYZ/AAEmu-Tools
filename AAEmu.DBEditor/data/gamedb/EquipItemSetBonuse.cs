using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class EquipItemSetBonuse
{
    public long? Id { get; set; }

    public long? EquipItemSetId { get; set; }

    public long? NumPieces { get; set; }

    public long? BuffId { get; set; }

    public long? ProcId { get; set; }
}

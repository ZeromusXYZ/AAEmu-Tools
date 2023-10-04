using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UnitModifier
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? UnitAttributeId { get; set; }

    public long? UnitModifierTypeId { get; set; }

    public long? Value { get; set; }

    public long? LinearLevelBonus { get; set; }
}

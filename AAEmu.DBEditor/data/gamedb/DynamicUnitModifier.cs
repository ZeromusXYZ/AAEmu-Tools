using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DynamicUnitModifier
{
    public long? Id { get; set; }

    public long? BuffId { get; set; }

    public long? UnitAttributeId { get; set; }

    public long? UnitModifierTypeId { get; set; }

    public long? FuncId { get; set; }

    public string FuncType { get; set; }
}

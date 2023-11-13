using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UnitFormula
{
    public long? Id { get; set; }

    public string Formula { get; set; }

    public long? KindId { get; set; }

    public long? OwnerTypeId { get; set; }
}

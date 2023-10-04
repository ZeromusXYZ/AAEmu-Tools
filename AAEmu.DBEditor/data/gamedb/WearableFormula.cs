using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class WearableFormula
{
    public long? Id { get; set; }

    public long? KindId { get; set; }

    public string Formula { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Plot
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? TargetTypeId { get; set; }
}

using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DispelEffect
{
    public long? Id { get; set; }

    public long? DispelCount { get; set; }

    public long? CureCount { get; set; }

    public long? BuffTagId { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BuffToleranceStep
{
    public long? Id { get; set; }

    public long? BuffToleranceId { get; set; }

    public long? HitChance { get; set; }

    public long? TimeReduction { get; set; }
}
